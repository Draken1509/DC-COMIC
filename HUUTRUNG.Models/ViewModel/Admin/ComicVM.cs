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
    public class ComicVM
    {
        //admin
        public Comic Comic { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> TypeComicList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> SeriesList { get; set; }

    }
}
