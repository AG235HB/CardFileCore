using System.ComponentModel.DataAnnotations;

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
