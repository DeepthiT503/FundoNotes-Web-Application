using ModelLayer.Models;
using RespositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;

namespace BussinessLayer.Interface
{
    public interface IUserBussiness
    {
       public UserEntity UserRegister(RegisterModel register);
        public UserEntity GetUsersById(int userId);
        public List<UserEntity> GetAllUser();
        public UserEntity GetUserEntityByFirstName(string firstName);
        public UserEntity UpdateEntity(int userId, RegisterModel updatedusers);
        public UserEntity UpdateUserEntitys(RegisterModel register);


        public List<UserEntity> GetFirstNameOFUsers();

        public int DeleteNote(int userId);

        public String Login(LoginModel loginmodel);
        public Task<string> ForgotPassword(string emailTo, IBus bus);

        public String ResetPassword(string email, string password, string confirm_password);



    }
}
