using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntusWindowDataModel.ViewModel
{
    public class vmOrders
    {
        public int? OrderId { get; set; }

        [Required, StringLength(50, ErrorMessage = "Length can't be greater than 50 characters")]
        public string? OrderName { get; set; }

        [Required, StringLength(5, ErrorMessage = "Length can't be greater than 5 characters")]
        public string? State { get; set; }
    }
}
