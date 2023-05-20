using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WebSignup.DBContext;
using WebSignup.Models;
using WebSignup.RegistrationDAL;

namespace WebSignup.Controllers
{
	public class LoginController : Controller
	{
        IConfiguration _configuration;
        public readonly DataAccessLayer _dbAccess = null;

        public LoginController(MyConnection myConnection, IConfiguration configuration)
        {
            _dbAccess = DataAccessLayer.getInstance(myConnection);
            _configuration = configuration;
        }
        [HttpGet]
		[AllowAnonymous]
		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		[AllowAnonymous]
		public IActionResult Login(User user)
		{
			if(_dbAccess.dbAuthenticateUser(user.UserName, user.Password))
			{
                var tokenString = GenerateJSONWebToken(user);
                if(tokenString != null )
                {
                    return View(tokenString);
                }
            }
            return View();
        }
        private string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
              _configuration["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
