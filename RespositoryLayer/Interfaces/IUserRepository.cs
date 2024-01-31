using MassTransit;
using ModelLayer.Models;
using RepositoryLayer.Entity;
using RespositoryLayer.Entity;
using RespositoryLayer.Services;

namespace RespositoryLayer.Interfaces
{
    public interface IUserRepository
    {
       public UserEntity UserRegister(RegisterModel register);
        //  public NotesEntity notesEntity(NotesModel notes);
        public int DeleteNote(int userId);

        UserEntity GetUserById(int id);
        public List<UserEntity> GetAllUser();
        public UserEntity GetUserEntityByFirstName(string firstName);
        public UserEntity UpdateEntity(int userId, RegisterModel updatedusers);
        public UserEntity UpdateUserEntitys(RegisterModel register);

        public List<UserEntity> GetFirstNameOFUsers();

        public String Login(LoginModel loginmodel);
        public Task<string> ForgotPassword(string emailTo,IBus bus);
        public String ResetPassword(string email, string password, string confirm_password);





    }
}