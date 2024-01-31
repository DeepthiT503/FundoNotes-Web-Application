using Automatonymous.Binders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ModelLayer;
using ModelLayer.Models;
using RespositoryLayer.Context;
using RespositoryLayer.Entity;
using RespositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace RespositoryLayer.Services
{
    public class UserNotes : IUserNotes
    {
        private readonly FundoContext fundoContext;
        private readonly IConfiguration configuration;
        public UserNotes(FundoContext fundoContext, IConfiguration configuration)
        {
            this.fundoContext = fundoContext;
            this.configuration = configuration;
        }
        public NotesEntity AddUsernotes(NotesModel notesmodel, int userId)
        {
            if (userId != 0)

            {
                IEnumerable<ImageEntity> imageEntity = null;
                var user = fundoContext.Users.FirstOrDefault(x => x.UserId == userId);
                if (user != null)
                {
                    NotesEntity entity1 = new NotesEntity();
                    entity1.Title = notesmodel.Title;
                    entity1.Description = notesmodel.Description;
                    entity1.Color = notesmodel.Color;
                    entity1.Remainder = notesmodel.Remainder;
                    entity1.IsArchive = notesmodel.IsArchive;
                    entity1.IsPinned = notesmodel.IsPinned;
                    entity1.IsTrash = notesmodel.IsTrash;
                    entity1.UserId = userId;
                    entity1.CreatedAt = DateTime.Now;
                    entity1.ModifiedAt = DateTime.Now;

                    fundoContext.notes.Add(entity1);
                    fundoContext.SaveChanges();

                    if (notesmodel.ImagePaths != null)
                    {
                        imageEntity = AddImages(entity1.notesId, userId, notesmodel.ImagePaths);
                    }
                        return entity1;
                    }

                    else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        public NotesEntity GetNotesEntityByTitle(string title)
        {
            if (title != null)
            {
                NotesEntity notesEntitys = fundoContext.notes.FirstOrDefault(x => x.Title == title);
                return notesEntitys;
            }
            return null;
        }
        public NotesEntity GetNotesEntityById(int id, int userId)
        {
            NotesEntity credentials = fundoContext.notes.FirstOrDefault(x => x.notesId == id && x.UserId==userId);
            return credentials;
        }
        public List<NotesEntity> GetAllNotes(int userId){
        if (userId != 0)
        {
        var result = fundoContext.notes.Where(x => x.UserId == userId).ToList();
        return result;
        }

        return null;
        }

        public IEnumerable<NotesEntity> GetAllNotesFromNotes()
        {
            // Assuming fundoContext.notes is a DbSet<NotesEntity>
            var notes = fundoContext.notes.ToList();

            return notes;
        }
        //deleting along with image
        public int DeleteNote(int notesId, int userId)
        {
            var note = fundoContext.notes.FirstOrDefault(x => x.notesId == notesId);
            if (note == null)
                return 0;

            fundoContext.notes.Remove(note);
            var imgRecord = fundoContext.image.Where(x=>x.ImageId==notesId);
            foreach(var i in imgRecord)
            fundoContext.image.Remove(i);
            return fundoContext.SaveChanges();
        }
        public NotesEntity Updatenotes(int notesId, NotesModel updatednotes)
        {
            //            var existingNote = fundoContext.Users.FirstOrDefault(n => n.UserId == userId);

            var existingUser = fundoContext.notes.SingleOrDefault(u => u.notesId == notesId);

            if (existingUser != null)
            {
                //Update properties of the existing user
                existingUser.Title = updatednotes.Title;
                
                fundoContext.SaveChanges();

                return existingUser;
            }

            return null;
        }
        public async Task<string> UploadImage(IFormFile formFile)
        {
            try
            {
                string originalFileName = formFile.FileName;
                string uniqueFileName = $"{Guid.NewGuid()}_{DateTime.Now.Ticks}{Path.GetExtension(originalFileName)}";

                string filePath = Path.Combine(FileHelper.GetFilePath(""), uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(fileStream);
                }

                return uniqueFileName;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public IEnumerable<ImageEntity> AddImages(int noteId, int userId, ICollection<IFormFile> files)
        {
            try
            {
                NotesEntity resNote = null;
                var user = fundoContext.notes.FirstOrDefault(n => n.UserId == userId);
                if (user != null)
                {
                    resNote = fundoContext.notes.Where(n => n.UserId == userId && n.notesId == noteId).FirstOrDefault();
                    if (resNote != null)
                    {
                        IList<ImageEntity> images = new List<ImageEntity>();
                        foreach (var file in files)
                        {
                            ImageEntity img = new ImageEntity();
                            var uploadImageRes = UploadImage(file);
                            img.notesId = noteId;
                            img.ImageUrl = uploadImageRes.ToString();
                            img.ImageName = file.FileName;
                            images.Add(img);
                            fundoContext.image.Add(img);
                            fundoContext.SaveChanges();
                            resNote.ModifiedAt = DateTime.Now;
                            fundoContext.notes.Update(resNote);
                            fundoContext.SaveChanges();
                        }
                        return images;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
                
            }
            catch (Exception)
            {
                throw;
            }

        }
    } }