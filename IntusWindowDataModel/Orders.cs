using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntusWindowDataModel
{
    [Table("Orders", Schema = "dbo")]
    public class Orders
    {
        [Required, Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }


        [Required, StringLength(50, ErrorMessage ="Length can't be greater than 50 characters")]
        public string? OrderName { get; set; }


        [Required, StringLength(5, ErrorMessage = "Length can't be greater than 5 characters")]
        public string? State { get; set; }
    }
}