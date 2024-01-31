using BussinessLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models;
using RespositoryLayer.Entity;
using RespositoryLayer.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Services
{
    public class NotesBussiness:INotesBussiness
    {
        private readonly IUserNotes userNotes;
        /*public UserBussiness(IUserRepository userRespository)
        {
            this.userRepository = userRespository;
        }*/
        public NotesBussiness(IUserNotes userNotes)
        {
            this.userNotes = userNotes;
        }
        public NotesEntity AddUsernotes(NotesModel notesmodel, int userId)
        {
            return userNotes.AddUsernotes(notesmodel, userId);
        }
       // public NotesEntity GetNotesEntityById(int id)
        public NotesEntity GetNotesEntityById(int id, int userId)
        {
            return userNotes.GetNotesEntityById(id, userId);
        }
        //        public IEnumerable<NotesEntity> GetAllNotes(int userId)
        //        public List<NotesEntity> GetAllNotes(int userId);

        public List<NotesEntity> GetAllNotes(int userId)
        {
            return userNotes.GetAllNotes(userId);
        }
        public IEnumerable<NotesEntity> GetAllNotesFromNotes()
        {
            return userNotes.GetAllNotesFromNotes();
        }
        public NotesEntity GetNotesEntityByTitle(string title)
        {
            return userNotes.GetNotesEntityByTitle(title);
        }

        public int DeleteNote(int notesId, int userId)
        {
            return userNotes.DeleteNote(notesId, userId);
        }
        public NotesEntity Updatenotes(int notesId, NotesModel updatednotes)
        {
            return userNotes.Updatenotes(notesId, updatednotes);
        }





    }
}
