using System.ComponentModel.DataAnnotations;

namespace WebAPI_Payments.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public int PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public byte[] Password { get; set; }
        public byte[] PasswordKey { get; set; }
        public double Budget { get; set; }
        public bool IsVerified { get; set; }
    }
}
