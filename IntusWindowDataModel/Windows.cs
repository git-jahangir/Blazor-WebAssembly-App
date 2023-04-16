using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntusWindowDataModel
{
    [Table("Windows", Schema = "dbo")]
    public class Windows
    {
        [Required, Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WindowId { get; set; }


        [Required, StringLength(10, ErrorMessage = "Length can't be greater than 10 characters")]
        public string? WindowName { get; set; }


        [Required, Range(1, 15, ErrorMessage = "Total element must be between 1 and 15")]
        public int QuantityOfWindows { get; set; }


        [Required, Range(1, 5, ErrorMessage ="Total element must be between 1 and 5")]
        public int TotalSubElements { get; set; }


        [Required, ForeignKey("Orders")]
        public int OrderId { get; set; }
        public virtual Orders? Orders { get; set; }
    }
}