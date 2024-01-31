using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models;
using RespositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Interface
{
    public interface INotesBussiness
    {
        public NotesEntity AddUsernotes(NotesModel notesmodel, int userId);
        public NotesEntity GetNotesEntityById(int id, int userId);
        public List<NotesEntity> GetAllNotes(int userId);
        public IEnumerable<NotesEntity> GetAllNotesFromNotes();
        public NotesEntity GetNotesEntityByTitle(string title);


        public int DeleteNote(int notesId, int userId);
        //  public NotesEntity UpdateEntity(int notesId, NotesModel updatedNotes);
        public NotesEntity Updatenotes(int notesId, NotesModel updatednotes);

    }
}
