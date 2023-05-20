using System.ComponentModel.DataAnnotations;

namespace WebSignup.Models
{
	public class User
	{
		public User()
		{
		}
		[Required(ErrorMessage ="Please Enter User Name.")]
        public string UserName { get; set; }
		[Required(ErrorMessage = "Please Enter Password.")]
		public string Password { get; set; }
		[Required(ErrorMessage = "Please Enter Confirm Password.")]
		[Compare(nameof(Password),ErrorMessage ="Password does not match.")]
		public string ConfirmPassword { get; set; }
		[Required(ErrorMessage = "Please Enter Email.")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Please Enter Phone Number.")]
		public string Phone { get; set; }
        public string Address { get; set; }
    }
}
