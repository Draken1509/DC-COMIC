using HUUTRUNG.Models.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HUUTRUNG.Models.ViewModel.Admin
{
    public class NewsVMA
    {
        //admin
        public News News { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> TypeNewsList { get; set; }      

    }
}
