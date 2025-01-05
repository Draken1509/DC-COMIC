using HUUTRUNG.DataAccess.Repository.IRepository;
using HUUTRUNG.Models.Domain;
using HUUTRUNG.Models.ViewModel.Admin;
using HUUTRUNG.Models.ViewModel.Customer;
using HUUTRUNG.Utility;
using HUUTRUNG_WEBAPI.CustomActionFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using System.Security.Claims;

namespace HUUTRUNGWEB.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;
        [BindProperty]  //Chỉ định rằng thuộc tính ShoppingCartVM sẽ được tự động gán giá trị từ dữ liệu trong yêu cầu HTTP. Điều này có thể bao gồm dữ liệu từ query string, form data (POST), hoặc route data.
		public ShoppingCartVM ShoppingCartVM { get; set; }

        public CartController(IUnitOfWork unitOfWork, IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            ShoppingCartVM = new()
            {
                ShoppingCartList= _unitOfWork.ShoppingCart.GetAll(u=>u.ApplicationUserId==userId, includeProperties: "Comic"),                      
                OrderHeader= new()
            };           
            foreach (var cart in ShoppingCartVM.ShoppingCartList)
            {
                cart.Price = getPrice(cart);
                ShoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }

            return View(ShoppingCartVM);
        }
        private double getPrice(ShoppingCart shoppingCart)
        {
            return (double)shoppingCart.Comic.Price;
        }

        public IActionResult Plus(int cartId)
        {
            var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId);
            cartFromDb.Count += 1;
            _unitOfWork.ShoppingCart.Update(cartFromDb);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Minus(int cartId)
        {
            var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId);
            if (cartFromDb.Count <= 1)
            {
                //remove that from cart

                _unitOfWork.ShoppingCart.Remove(cartFromDb);
				//so khoi session cart
				HttpContext.Session.SetInt32(SD.SessionCart, _unitOfWork.ShoppingCart
					.GetAll(u => u.ApplicationUserId == cartFromDb.ApplicationUserId).Count() - 1);
			}
            else
            {
                cartFromDb.Count -= 1;
                _unitOfWork.ShoppingCart.Update(cartFromDb);
            }

            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int cartId)
        {
            var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId);

            _unitOfWork.ShoppingCart.Remove(cartFromDb);

			HttpContext.Session.SetInt32(SD.SessionCart, _unitOfWork.ShoppingCart
			  .GetAll(u => u.ApplicationUserId == cartFromDb.ApplicationUserId).Count() - 1);
			_unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Summary()
        {
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

			ShoppingCartVM = new()
			{
				ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId,
				includeProperties: "Comic"),
				OrderHeader = new()
			};

			ShoppingCartVM.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);
			if (ModelState.IsValid)
			{
				ShoppingCartVM.OrderHeader.Name = ShoppingCartVM.OrderHeader.ApplicationUser.Name;
				ShoppingCartVM.OrderHeader.PhoneNumber = ShoppingCartVM.OrderHeader.ApplicationUser.PhoneNumber;
				ShoppingCartVM.OrderHeader.StreetAddress = ShoppingCartVM.OrderHeader.ApplicationUser.StreetAddress;
				ShoppingCartVM.OrderHeader.City = ShoppingCartVM.OrderHeader.ApplicationUser.City;
				ShoppingCartVM.OrderHeader.State = ShoppingCartVM.OrderHeader.ApplicationUser.Province;
				ShoppingCartVM.OrderHeader.PostalCode = ShoppingCartVM.OrderHeader.ApplicationUser.Postal;
			}
			else
			{
				return View(nameof(Index));
			}		
			foreach (var cart in ShoppingCartVM.ShoppingCartList)
			{
				cart.Price = getPrice(cart);
				ShoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
			}
			return View(ShoppingCartVM);
		}


		[HttpPost]
		[ActionName("Summary")]	
		public IActionResult SummaryPOST()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

			ShoppingCartVM.ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId,
				includeProperties: "Comic");

			ShoppingCartVM.OrderHeader.OrderDate = System.DateTime.Now;
			ShoppingCartVM.OrderHeader.ApplicationUserId = userId;

			ApplicationUser applicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);


			foreach (var cart in ShoppingCartVM.ShoppingCartList)
			{
				cart.Price = getPrice(cart);
				ShoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
			}
			
			if (applicationUser.CompanyId.GetValueOrDefault() == 0)
			{
				//it is a regular customer 
				ShoppingCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusPending;
				ShoppingCartVM.OrderHeader.OrderStatus = SD.StatusPending;
			}
			else
			{
				//it is a company user
				ShoppingCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusDelayedPayment;
				ShoppingCartVM.OrderHeader.OrderStatus = SD.StatusApproved;
			}
			ModelState.Remove("OrderHeader.ApplicationUserId");
			ModelState.Remove("MovieMain");
			ModelState.Remove("ComicListPagination");
			ModelState.Remove("ShoppingCartList");

			if (ModelState.IsValid) {
				_unitOfWork.OrderHeader.Add(ShoppingCartVM.OrderHeader);
				_unitOfWork.Save();
			}
			else
			{
				return View(ShoppingCartVM);
			}
			//_unitOfWork.OrderHeader.Add(ShoppingCartVM.OrderHeader);
		

			foreach (var cart in ShoppingCartVM.ShoppingCartList)
			{
				OrderDetail orderDetail = new()
				{
					ComicId = cart.ComicId,
					OrderHeaderId = ShoppingCartVM.OrderHeader.Id,
					Price = cart.Price,
					Count = cart.Count
				};
				_unitOfWork.OrderDetail.Add(orderDetail);
				_unitOfWork.Save();
			}


			if (applicationUser.CompanyId.GetValueOrDefault() == 0)
			{
				//it is a regular customer account and we need to capture payment
				//stripe logic
				var domain = Request.Scheme + "://" + Request.Host.Value + "/";  // Lay ten mien hien tai
                var options = new SessionCreateOptions
				{
					SuccessUrl = domain + $"customer/cart/OrderConfirmation?id={ShoppingCartVM.OrderHeader.Id}",
					CancelUrl = domain + "customer/cart/index", 
					LineItems = new List<SessionLineItemOptions>(),
					Mode = "payment",
				};

				foreach (var item in ShoppingCartVM.ShoppingCartList)
				{
					var sessionLineItem = new SessionLineItemOptions
					{
						PriceData = new SessionLineItemPriceDataOptions
						{
							UnitAmount = (long)(item.Price * 100), // $20.50 => 2050
							Currency = "usd",
							ProductData = new SessionLineItemPriceDataProductDataOptions
							{
								Name = item.Comic.Name,
							}
						},
						Quantity = item.Count
					};
					options.LineItems.Add(sessionLineItem);
				}

				var service = new SessionService();
				Session session = service.Create(options);  // tao ra 1 phien thanh toan voi cau hinh o tren
				_unitOfWork.OrderHeader.UpdateStripePaymentID(ShoppingCartVM.OrderHeader.Id, session.Id, session.PaymentIntentId);
				_unitOfWork.Save();
				Response.Headers.Add("Location", session.Url);

				return new StatusCodeResult(303);

			}

			return RedirectToAction(nameof(OrderConfirmation), new { id = ShoppingCartVM.OrderHeader.Id }); 		
		}

        public IActionResult OrderConfirmation(int id)
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeader.Get(u => u.Id == id, includeProperties: "ApplicationUser");
            if (orderHeader.PaymentStatus != SD.PaymentStatusDelayedPayment)
            {
                //this is an order by customer

                var service = new SessionService();
                Session session = service.Get(orderHeader.SessionId);

                if (session.PaymentStatus.ToLower() == "paid")
                {
                    _unitOfWork.OrderHeader.UpdateStripePaymentID(id, session.Id, session.PaymentIntentId);
                    _unitOfWork.OrderHeader.UpdateStatus(id, SD.StatusApproved, SD.PaymentStatusApproved);
                    _unitOfWork.Save();
                }
                HttpContext.Session.Clear();

            }

			_emailSender.SendEmailAsync(orderHeader.ApplicationUser.Email, "New Order - Bulky Book",
				$"<p>New Order Created - {orderHeader.Id}</p>");

			List<ShoppingCart> shoppingCarts = _unitOfWork.ShoppingCart
                .GetAll(u => u.ApplicationUserId == orderHeader.ApplicationUserId).ToList();

            _unitOfWork.ShoppingCart.RemoveRange(shoppingCarts);
            _unitOfWork.Save();

            return View(id);
        }


    }
}
