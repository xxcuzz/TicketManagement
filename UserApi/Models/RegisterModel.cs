using System.ComponentModel.DataAnnotations;

namespace UserApi.Models
{
    public class RegisterModel
    {
        public string FirstName { get; set; }

        public string Surname { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
