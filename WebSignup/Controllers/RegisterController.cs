using Microsoft.AspNetCore.Mvc;
using WebSignup.DAL;
using WebSignup.Models;
using WebSignup.RegistrationDAL;
using WebSignup.CommonUtility;
using WebSignup.DBContext;

namespace WebSignup.Controllers
{
    public class RegisterController : Controller
	{
		private readonly ILogger<RegisterController> _logger;
		public readonly DataAccessLayer _dbAccess = null;
		public RegisterController(ILogger<RegisterController> logger,MyConnection myConnection)
		{
			_logger = logger;
            _dbAccess = DataAccessLayer.getInstance(myConnection);
		}
		//[Route("/Register/RegisterUser")]
		//SignUp/SignUp")]
		public IActionResult RegisterUser()
		{
			return View();
		}
		
		[Route("/Register/RegisterUser")]
		[HttpPost]
		public IActionResult RegisterUser(User lUser)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			if (lUser.Password == null)
			{
				return View();
			}
			UserEntity userEntity = new UserEntity();
            userEntity.AddUser(lUser);
			int userId = _dbAccess.dbAddUser(userEntity);
            if (userId > 0)
			{
				UsersCred luserCred = new UsersCred();
				string lstrPassword = EncryptUtility.EncodePasswordToBase64(lUser.ConfirmPassword);
				lUser.Password = lUser.ConfirmPassword = "";
				luserCred.createNewUserCredentials(userEntity, lstrPassword);
				_dbAccess.dbCreateUserCredentials(luserCred);
				if (luserCred.ID > 0)
				{
					return RedirectToAction("Login", "Index");
				}
			}
			return View();
		}
	}
}
