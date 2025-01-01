using HUUTRUNG.Models.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HUUTRUNG.Models.ViewModel.Customer
{
    public class NewsVM
	{
        //customer
        [ValidateNever]
        public News News { get; set; }

        [ValidateNever]
        public IEnumerable<News> OffcialAndLatestNewsList { get; set; }
        [ValidateNever]
        public IEnumerable<News> FeaturesNewsList { get; set; }
        [ValidateNever]
        public IEnumerable<News> InterviewsNewsList { get; set; }




	}
}
