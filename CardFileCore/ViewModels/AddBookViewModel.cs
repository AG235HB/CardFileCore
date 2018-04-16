using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CardFileCore.ViewModels
{
    public class AddBookViewModel
    {
        [Required]
        [Display(Name ="Название")]
        public string Name { get; set; }

        [Required]
        [Display(Name ="Инвентарный номер")]
        public int Number { get; set; }
    }
}
