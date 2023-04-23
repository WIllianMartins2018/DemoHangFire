using System.ComponentModel.DataAnnotations;

namespace DemoHangFire.Models
{
    public class UserModel
    {
        [Key]
        public Guid IdUser { get; set; }
        [Required]
        public string  NameUser { get; set; }
        [Required]
        public bool IsChecked { get; set; }
    }
}
