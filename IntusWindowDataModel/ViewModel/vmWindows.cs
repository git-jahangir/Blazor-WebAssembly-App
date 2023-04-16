using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntusWindowDataModel.ViewModel
{
    public class vmWindows
    {
        public int? WindowId { get; set; }

        [Required, StringLength(10, ErrorMessage = "Length can't be greater than 10 characters")]
        public string? WindowName { get; set; }

        [Required, Range(1, 15, ErrorMessage = "Total element must be between 1 and 15")]
        public int QuantityOfWindows { get; set; }

        [Required, Range(1, 5, ErrorMessage = "Total element must be between 1 and 5")]
        public int TotalSubElements { get; set; }

        [Required, Range(1, double.MaxValue, ErrorMessage = "Invalide Selection")]
        public int OrderId { get; set; }
        public string? OrderName { get; set; }
    }
}
