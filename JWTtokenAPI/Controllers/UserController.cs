using JWTtokenAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace JWTtokenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        
        [Route("Admin")]
        [HttpGet]
        public ActionResult AdminEndPoint()
        {
            var currentUser = GetCurrentUser();
            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();
            if (currentUser != null) {
                response.Add("status", true);
                response.Add("message", "User Fetched");
                response.Add("data", currentUser);
                return Ok(response);
            }
            else
            {
                response.Add("status", false);
                response.Add("message", "User Not Found");
                response.Add("data", null);
                return NotFound(response);
            }
            
        }
        private UserRagister GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;
                return new UserRagister
                {
                    Id = Convert.ToInt32(userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value),
                    Username = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value,                                      
                    Email = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value
                };
            }
            return null;
        }




    }
}
