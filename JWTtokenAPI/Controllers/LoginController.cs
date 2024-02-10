using JWTtokenAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTtokenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase 
    {
        public String ConString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("MyConnectionString");

        private readonly IConfiguration _config;
        public LoginController(IConfiguration config)
        {
            _config = config;
        }


        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login( User userLogin)
        {
            
           
            var user = Authenticate(userLogin);
            
            if (user != null)
            {
                var token = GenerateToken(user);
               
                Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();
                if (token != null)
                {
                    response.Add("status", true);
                    response.Add("message", "Token Genrated");
                    response.Add("token",  token );
                    response.Add("currentUser", user);
                    return Ok(response);
                }
                else
                {
                    response.Add("status", false);
                    response.Add("message", "SomeError occure");
                }
                return Ok(token);
            }

            return NotFound("user not found");
        }

        private string GenerateToken(UserRagister user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.Email,user.Email)
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        

        



        private UserRagister Authenticate(User userLogin)
        {

            UserModel model = SellectByUser(userLogin);
            if (model != null)
            {
                UserRagister currentUser = new UserRagister() { Id = model.UserId, Username = model.name, Email = model.email };
                return currentUser;
            }
            else
            {
                return null;
            }
            
            
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
                if(UserModel.UserId != 0)
                {
                    return UserModel;
                }
                else
                {
                    return null;
                }
                

            }

            catch (Exception ex)
            {

                return null;
            }
        }


    }
}
