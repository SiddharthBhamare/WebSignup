using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebSignup.DAL;
using WebSignup.DBContext;
using WebSignup.Models;

namespace WebSignup.RegistrationDAL
{
    public class UsersCred
	{
		[Key]
        public int ID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public void createNewUserCredentials(UserEntity userEntity,string astrPassword)
        {
            this.UserID = userEntity.UserID;
            this.UserName = userEntity.UserName;
            this.Password = astrPassword;     
          
        }
       
    }
}
