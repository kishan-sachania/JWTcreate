
using JWTtokenAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTLoginAuthenticationAuthorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SigninController : ControllerBase
    {
        public String ConString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("MyConnectionString");

        private readonly IConfiguration _config;
        public SigninController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login([FromBody] User User)
        {
            var user = Authenticate(User);
            if (user != null)
            {
                var token = GenerateToken(user);
                return Ok(token);
            }

            return NotFound("user not found");
        }

        // To generate token
        private string GenerateToken(UserRagister user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Username),
                new Claim(ClaimTypes.Role,user.Email)
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        //To authenticate user
        private UserRagister Authenticate(User User)
        {
            UserModel model = SellectByUser(User);

            List<UserRagister> Users = new()
            {
                    new UserRagister(){Username=model.name,Password=model.password,Email=model.email}
            };

            var currentUser = Users.FirstOrDefault(x => x.Username.ToLower() == User.Username.ToLower() && x.Password == User.Password);
            if (currentUser != null)
            {
                return currentUser;
            }
            return null;
        }


        [HttpGet]
        public UserModel SellectByUser(User user)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_User_SelectByUsername");
                sqlDatabase.AddInParameter(dbCommand, "@Username", SqlDbType.VarChar, user.Username);
                sqlDatabase.AddInParameter(dbCommand, "@Password", SqlDbType.VarChar, user.Password);
                UserModel UserModel = new UserModel();
                using (IDataReader dr = sqlDatabase.ExecuteReader(dbCommand))
                {
                    while (dr.Read())
                    {
                        UserModel.UserId = Convert.ToInt32(dr["UserID"].ToString());
                        UserModel.name = dr["name"].ToString();
                        UserModel.email = dr["email"].ToString();
                        UserModel.password = dr["password"].ToString();
                        UserModel.location = dr["location"].ToString();
                    }
                }

                return UserModel;

            }

            catch (Exception ex)
            {

                return null;
            }
        }

    }
}