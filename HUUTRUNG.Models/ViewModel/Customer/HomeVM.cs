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
    public class HomeVM
    {
        [ValidateNever]
        public List<Comic> ComicFreeList { get; set; }
        [ValidateNever]
        public Movie MainVideo { get; set; }

    }
}
