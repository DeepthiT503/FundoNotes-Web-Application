using BussinessLayer.Interface;
using BussinessLayer.Services;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models;
using RepositoryLayer.Entity;
using RespositoryLayer.Entity;
using RespositoryLayer.Services;

namespace FundooNotesApp1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Usercontroller : ControllerBase
    {
        private readonly IUserBussiness userBussiness;

        private readonly IBus bus;
         private readonly ILogger<Usercontroller> _logger;

        public Usercontroller(IUserBussiness userBussiness,IBus bus, ILogger<Usercontroller> logger)
        {
            this.userBussiness = userBussiness;
            this.bus = bus;
            _logger = logger;
        }
        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterModel model)
        {
            _logger.LogInformation("Inside the Register");
            var result = userBussiness.UserRegister(model);
            if (result != null)
            {
                return Ok(new ResponseModel<UserEntity> { Success = true, Message = "Register Successful", Data = result });
            }
            else
            {
                // _logger.LogInformation("Out from  the Register");
                return BadRequest(new ResponseModel<UserEntity> { Success = false, Message = "Register not Successful" });
            }
        }


        [HttpGet]
        [Route("getting particular userid")]
        public IActionResult GetActionResult(int id)
        {
            UserEntity user = userBussiness.GetUsersById(id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(user);
            }
        }
        [HttpGet]
        [Route("getting all the users")]
        public ActionResult<IEnumerable<NotesEntity>> GetAllUser()
        {

            var result = userBussiness.GetAllUser();
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(new ResponseModel<NotesEntity> { Success = false, Message = "Retrive not Successful" });
            }
        }
        [HttpGet]
        [Route("FirstName")]
        public ActionResult GetUserEntityByFirstName(string firstName)
        {
            try
            {
                var result = userBussiness.GetUserEntityByFirstName(firstName);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(new ResponseModel<UserEntity> { Success = false, Message = "Retrieve not Successful" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("FirstName starts with S")]
        public ActionResult GetFirstNameOFUsers()
        {
            try
            {
                var result = userBussiness.GetFirstNameOFUsers();
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(new ResponseModel<UserEntity> { Success = false, Message = "Not Successful" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public ActionResult UpdateEntity(int userId, RegisterModel updatedusers)
        {
            try
            {
                var result = userBussiness.UpdateEntity(userId, updatedusers);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(new ResponseModel<UserEntity> { Success = false, Message = "Update not Successful" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("creating it if not there")]
        public ActionResult UpdateUserEntitys(RegisterModel register)
        {
            try
            {
                var result = userBussiness.UpdateUserEntitys(register);
                if (result != null)
                {
                    return Ok(new ResponseModel<UserEntity> { Success = true, Message = "Updated Successful" });
                }
                else
                {
                    return BadRequest(new ResponseModel<UserEntity> { Success = false, Message = "Update not Successful" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }



        [HttpDelete("{id}")]
        public ActionResult<int> DeleteNote(int id)
        {
            try
            {
                var result = userBussiness.DeleteNote(id);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }



        [HttpPost]
        [Route("login")]
        public IActionResult Login1(LoginModel model)
        {
            var result = userBussiness.Login(model);
            if (result != null)
            {
                return Ok(new ResponseModel<string> { Success = true, Message = "Login Successful", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "Login not Successful", Data = result });
            }
        }

        [HttpPost]
        [Route("forgot password")]
        public IActionResult ForgotPassword(string emailTo)
        {
            try
            {
                var result = userBussiness.ForgotPassword(emailTo,bus);
                if (result != null)
                {
                    return Ok(new { Success = true, Message = "token generated successfully and Authentication Notification send to " , data = result});
                }
                else
                {
                    return BadRequest(new { Success = false, message = "Token not Generated"});
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("reset password")]
        //
        
        public IActionResult ResetPasswords(string email, string password, string confirm_password)
        {
            try { 
            var result = userBussiness.ResetPassword(email, password, confirm_password);
            if (result != null)
            {
                return Ok(new { Success = true, Message = "token generated successfully and Authentication Notification send to ", data = result });
            }
            else
            {
                return BadRequest(new { Success = false, message = "Token not Generated" });
            }
        }
        catch (Exception ex)
            {
                return BadRequest(ex.Message);
    }
}

    }

}
