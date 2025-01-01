using HUUTRUNG.Models.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;



namespace HUUTRUNG.Models.ViewModel.Customer
{
    public class ShoppingCartVM
    {
        //customer
        [ValidateNever]
        public Comic Comic { get; set; }
        public IPagedList<Comic> ComicListPagination { get; set; }
        [ValidateNever]
        public List<Comic> ComicList { get; set; }
        [ValidateNever]
        public List<ComicCategory> TypeComicList { get; set; }
        [ValidateNever]
        public List<String> WriterComicList { get; set; }
        [ValidateNever]
        public List<Comic> ComicFreeList { get; set; }
        [ValidateNever]
        public List<Comic> ComicNewList { get; set; }
        [ValidateNever]
        public List<Comic> ComicRelatedSeriesList { get; set; }
        [ValidateNever]
        public ShoppingCart ShoppingCart { get; set; }

        public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }

        public OrderHeader OrderHeader { get; set; }

        public Movie MovieMain {get;set;}



	}
}
