using Infrastructure.Data.Entities;

namespace Business.Models.Request.Functional
{
    public class RegisterDto
    {

        public string Email { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public Gender Gender { get; set; } = Gender.Unknown!;
        public string Phone { get; set; }


    }
}
