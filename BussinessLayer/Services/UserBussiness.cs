using BussinessLayer.Interface;
using MassTransit;
using ModelLayer.Models;
using RepositoryLayer.Entity;
using RespositoryLayer.Entity;
using RespositoryLayer.Interfaces;
using RespositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Services
{
    public class UserBussiness:IUserBussiness
    {
        private readonly IUserRepository userRepository;
        public UserBussiness(IUserRepository userRespository)
        {
            this.userRepository = userRespository;
        }
        public  UserEntity UserRegister(RegisterModel register)
        {
            return userRepository.UserRegister(register);
        }
        //public NotesEntity AddUsernotes(NotesModel notesmodel)
        //{
        //   return userRepository.AddUsernotes(notesmodel);
        //}
        public UserEntity GetUsersById(int userId)
        {
           return userRepository.GetUserById(userId);
        }
        public List<UserEntity> GetAllUser()
        {
            return userRepository.GetAllUser();
        }
        public UserEntity GetUserEntityByFirstName(string firstName)
        {
            return userRepository.GetUserEntityByFirstName(firstName);
        }
        public UserEntity UpdateEntity(int userId, RegisterModel updatedusers)
        {
            return userRepository.UpdateEntity(userId, updatedusers);
        }

        public int DeleteNote(int userId)
        {
            return userRepository.DeleteNote(userId);
        }


        public string  Login(LoginModel loginmodel)
        {
            return userRepository.Login(loginmodel);
        }
        public async Task<string> ForgotPassword(string emailTo, IBus bus)
        {
            return  await userRepository.ForgotPassword(emailTo, bus);
        }
        
        public UserEntity UpdateUserEntitys(RegisterModel register)
        {
            return userRepository.UpdateUserEntitys(register);
         }
        public List<UserEntity> GetFirstNameOFUsers()
        {
            return userRepository.GetFirstNameOFUsers();
        }
        public String ResetPassword(string email, string password, string confirm_password)
        {
            return userRepository.ResetPassword(email, password, confirm_password);
        }


    }
}
