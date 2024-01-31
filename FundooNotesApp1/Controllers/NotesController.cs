using BussinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models;
using RespositoryLayer.Entity;
using RespositoryLayer.Migrations;


namespace FundooNotesApp1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesBussiness notesBussiness;
        private IWebHostEnvironment _webHostEnvironment;

        public NotesController(INotesBussiness notesBussiness, IWebHostEnvironment webHostEnvironment)
        {
            this.notesBussiness = notesBussiness;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpPost]
        [Authorize]
        public ActionResult CreateNote([FromForm]NotesModel notes)
        {
            try
            {
                int userId = int.Parse(User.Claims.Where(x => x.Type == "userId").FirstOrDefault().Value);
                var result = notesBussiness.AddUsernotes(notes, userId);
                if (result != null)
                {
                    return Ok(new ResponseModel<NotesEntity> { Success = true, Message = "Notes Creation Successful", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<NotesEntity> { Success = false, Message = "Notes Creation not Successful" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult GetNotesEntityById(int id)
        {
            try
            {
                int userId = int.Parse(User.Claims.Where(x => x.Type == "userId").FirstOrDefault().Value);
                NotesEntity result = notesBussiness.GetNotesEntityById(id, userId);
                if (result != null)
                {
                    return Ok(new ResponseModel<NotesEntity> { Success = true, Message = "Retrieve Successful", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<NotesEntity> { Success = false, Message ="Retrive not Successful" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{title}")]
        public ActionResult GetNotesEntityByTitle(string title)
        {
            try
            {
                NotesEntity result = notesBussiness.GetNotesEntityByTitle(title);
                if (result != null)
                {
                    return Ok(new ResponseModel<NotesEntity> { Success = true, Message = "Retrieve Successful", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<NotesEntity> { Success = false, Message = "Retrive not Successful" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("getting all notesid of particular userid")]
        [Authorize]
        public ActionResult <IEnumerable<NotesEntity>> GetAllNotes()
        {
            int userId1 = int.Parse(User.Claims.Where(x => x.Type == "userId").FirstOrDefault().Value);

            var result = notesBussiness.GetAllNotes(userId1);
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
        [Route("getting all the notes")]
        public ActionResult<IEnumerable<NotesEntity>> GetAllNotesFromNotes()
        {

            var result = notesBussiness.GetAllNotesFromNotes();
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(new ResponseModel<NotesEntity> { Success = false, Message = "Retrive not Successful" });
            }
        }

        [HttpDelete("{id}")]

        public ActionResult<int> DeleteNote(int id)
        {
           
                int userId2 = int.Parse(User.Claims.Where(x => x.Type == "userId").FirstOrDefault().Value);

                NotesEntity result = notesBussiness.GetNotesEntityById(id, userId2);
                if (result != null) { 
                return Ok(result);
            }
            else
            {
                return BadRequest(new ResponseModel<NotesEntity> { Success = false, Message = "Retrive not Successful" });

            }
        }
       
        [HttpPut]
        public ActionResult Updatenotes(int notesId, NotesModel updatedNotes)
        {
            try
            {
                var result = notesBussiness.Updatenotes(notesId, updatedNotes);
                if (result != null)
                {
                    return Ok(new ResponseModel<NotesEntity> { Success = true, Message = "Updated Successful", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<NotesEntity> { Success = false, Message = "Update not Successful" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
       
    }
}
