using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebSignup.CommonUtility;
using WebSignup.DBContext;
using WebSignup.Models;
using WebSignup.RegistrationDAL;

namespace WebSignup.DAL
{
    [Table("Users")]
	public class UserEntity
	{

		[Key]
		public int UserID { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
		public string Address { get; set; }
		public string Phone { get; set; }
        public void AddUser(User lUser)
		{ 
			UserName = lUser.UserName;
			Address = lUser.Address;
			Phone = lUser.Phone;
			Email = lUser.Email;	
		}

	}
}
