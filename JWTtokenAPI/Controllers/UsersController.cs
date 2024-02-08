using JWTtokenAPI.BAL;
using JWTtokenAPI.Models;

using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using System.Runtime.ConstrainedExecution;

namespace JWTtokenAPI.Controllers
{
    

    [Route("api/[controller]")]
    [ApiController] // Add this attribute to make it an ApiController
    public class UsersController : Controller
    {
        

        [HttpGet]
        public IActionResult Index()
        {
            User_BALbase bal = new User_BALbase();
            List<UserModel> per = bal.Selectall();
            Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();
            if (per.Count > 0 && per != null)
            {
                data.Add("status", true);
                data.Add("message", "data not found");
                data.Add("data", per);
                return Ok(data);
            }
            else
            {
                data.Add("status", false);
                data.Add("message", "data not found");
                data.Add("data", null);
                return NotFound(data);

            }

        }

        [HttpGet("{UserID}")]

        public IActionResult Getbyid(int UserID)
        {
            User_BALbase bal = new User_BALbase();
            UserModel user = bal.SellectByID(UserID);
            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();
            if (user.UserId != 0)
            {
                response.Add("status", true);
                response.Add("message", "data found");
                response.Add("data", user);
                return Ok(response);

            }
            else
            {
                response.Add("status", false);
                response.Add("message", "data not found");
                response.Add("data", null);
                return NotFound(response);

            }

        }

        [HttpDelete]
        public IActionResult DeleteById(int UserID)
        {

            User_BALbase bal = new User_BALbase();
            bool IsSuccess = bal.DeleteById(UserID);
            Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();

            if (IsSuccess)
            {
                data.Add("status", true);
                data.Add("message", "data DELETE");
                return Ok(data);
            }
            else
            {
                data.Add("status", false);
                data.Add("message", "data not DELETE");
                return NotFound(data);

            }
        }

        [HttpPost]
        public IActionResult Insert(UserModel UserModel)
        {
            {
                User_BALbase bal = new User_BALbase();
                bool IsSuccess = bal.Insert(UserModel);
                Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();
                if (IsSuccess)
                {
                    response.Add("status", true);
                    response.Add("message", "Data Inserted Successfully.");
                    return Ok(response);
                }
                else
                {
                    response.Add("status", true);
                    response.Add("message", "Some error has been occured.");
                    return NotFound(response);
                }
            }
        }

        [HttpPut]
        public IActionResult put(UserModel UserModel)
        {
            User_BALbase user = new User_BALbase();
            bool issuccess = user.Update(UserModel);

            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();

            if (issuccess)
            {
                response.Add("status", true);
                response.Add("message", "data updated successfully");
                return Ok(response);
            }
            else
            {
                response.Add("status", false);
                response.Add("message", "error occur");
                return Ok(response);
            }
        }


    }
}
