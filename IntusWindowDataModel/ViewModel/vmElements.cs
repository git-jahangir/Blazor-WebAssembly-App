using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntusWindowDataModel.ViewModel
{
    public class vmElements
    {        
        public int? SubElementId { get; set; }

        [Required, Range(1, 5, ErrorMessage = "Total element must be between 1 and 5")]
        public int Element { get; set; }

        [Required, StringLength(10, ErrorMessage = "Length can't be greater than 10 characters")]
        public string? Type { get; set; }

        [Required, Range(600, 1500, ErrorMessage = "Total element must be between 600 and 1500")]
        public int Width { get; set; }

        [Required, Range(1850, 2200, ErrorMessage = "Total element must be between 1850 and 2200")]
        public int Height { get; set; }

        [Required, Range(1, double.MaxValue, ErrorMessage = "Invalide Selection")]
        public int WindowId { get; set; }

        [Required, Range(1, double.MaxValue, ErrorMessage = "Invalide Selection")]
        public int? OrderId { get; set; }

        public string? WindowName { get; set; }

        public string? OrderName { get; set; }
    }
}
