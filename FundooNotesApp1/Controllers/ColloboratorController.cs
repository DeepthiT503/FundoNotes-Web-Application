using BussinessLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models;
using RespositoryLayer.Entity;

namespace FundooNotesApp1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColloboratorController : ControllerBase
    {
        private readonly IBussinessColloboratory bussinessColloboratory;
        private readonly IConfiguration configuration;
        public ColloboratorController(IBussinessColloboratory bussinessColloboratory, IConfiguration configuration)
        {
            this.bussinessColloboratory = bussinessColloboratory;
            this.configuration = configuration;
        }
        [HttpPost]
        [Route("Colloborator")]
        public IActionResult colloborate(int c_id, string c_Email, int notesId, int UserId)
        {
            var result = bussinessColloboratory.AddColloboratory(c_id, c_Email, notesId, UserId);
            if (result != null)
            {
                return Ok(new ResponseModel<Colloboratory> { Success = true, Message = "Colloborate Successful", Data = result });
            }
            else
            {
                // _logger.LogInformation("Out from  the Register");
                return BadRequest(new ResponseModel<Colloboratory> { Success = false, Message = "Colloborate Not Successful"});
            }
        }
    }
}
