using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntusWindowDataModel
{
    [Table("SubElements", Schema = "dbo")]
    public class SubElements
    {
        [Required, Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubElementId { get; set; }


        [Required, Range(1, 5, ErrorMessage = "Total element must be between 1 and 5")]
        public int Element { get; set; }


        [Required, StringLength(10, ErrorMessage = "Length can't be greater than 10 characters")]
        public string? Type { get; set; }


        [Required, Range(500, 1500, ErrorMessage = "Total element must be between 600 and 1500")]
        public int Width { get; set; }


        [Required, Range(1850, 2200, ErrorMessage = "Total element must be between 1850 and 2200")]        
        public int Height { get; set; }

        
        [Required, ForeignKey("Windows")]
        public int WindowId { get; set; }

        public virtual Windows? Windows { get; set; }
    }
}