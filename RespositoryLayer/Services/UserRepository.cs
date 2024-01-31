using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using ModelLayer.Models;
using RepositoryLayer.Entity;
using RespositoryLayer.Context;
using RespositoryLayer.Entity;
using RespositoryLayer.Interfaces;
using RespositoryLayer.Migrations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RespositoryLayer.Services
{
    public class UserRepository : IUserRepository
    {
       private readonly FundoContext fundoContext;
       private readonly ILogger<UserRepository> _logger;

        //This indicates that the FundoContext instance is being injected into the class through its constructor.
        private readonly IConfiguration configuration;
        public UserRepository(FundoContext fundoContext, IConfiguration configuration, ILogger<UserRepository> logger)
        {
            this.fundoContext = fundoContext;
            this.configuration = configuration;
            _logger = logger;
        }
        public UserEntity UserRegister(RegisterModel register)
        {
            _logger.LogInformation("Inside the RegisterModel");
            UserEntity entity = new UserEntity();
            //Assigning the values from registermodel to the entity
            entity.FirstName = register.FirstName;
            entity.LastName = register.LastName;
            entity.EmailId = register.EmailId;
            entity.password = EncryptPassword(register.Password);
            //It will add the UserEntity to the FundoContext.
            fundoContext.Users.Add(entity);
            fundoContext.SaveChanges();
            return entity;
        }
        public List<UserEntity> GetAllUser()
        {
            _logger.LogInformation("Inside the GetAllUsers()");
            _logger.LogDebug("Inside the Debug........");
            Trace.WriteLine("Inside the Debug........");


            var userdetails = fundoContext.Users.ToList();
             _logger.LogInformation("Outside the GetAllUsers()");

            return userdetails;

        }
        // getting the particular id
        public UserEntity GetUserById(int id)
        {
            try
            {
                _logger.LogWarning("Inside the getuserbyid");
                UserEntity userdetails = fundoContext.Users.FirstOrDefault(x => x.UserId == id);
                return userdetails;

            }
            catch (Exception)
            {
                _logger.LogWarning("Exception occured at getUserId()");
                throw;
            }       }
        // getting the particular FirstName
        public UserEntity GetUserEntityByFirstName(string firstName)
        {
            UserEntity userEntitys = fundoContext.Users.FirstOrDefault(x => x.FirstName == firstName);
            if(userEntitys != null) { 
            return userEntitys;
            }
            return null;
        }
        public UserEntity UpdateEntity(int userId, RegisterModel updatedusers)
        {
            var existingNote = fundoContext.Users.FirstOrDefault(n => n.UserId == userId);
            if (existingNote != null)
            {
                // Update properties of the existing note
                existingNote.LastName = updatedusers.LastName;
                existingNote.EmailId = updatedusers.EmailId;
                existingNote.password = updatedusers.Password;
                fundoContext.SaveChanges();
                return existingNote;

            }
            return null;
        }
        //task method
        //It checks if an existing user with the specified email exists, and if so, updates the user; otherwise, it creates a new user
        public UserEntity UpdateUserEntitys(RegisterModel register)
        {

               var entity = fundoContext.Users.FirstOrDefault(x => x.EmailId == register.EmailId);
            if(entity != null) { 
                entity.FirstName = register.FirstName;
                entity.LastName = register.LastName;
              //  entity.EmailId = register.EmailId;
                entity.password = register.Password;
               // fundoContext.Users.Update(entity);
                fundoContext.SaveChanges();
                return entity;

            }
            else
            {
                UserEntity entity1 = new UserEntity();
                entity1.FirstName = register.FirstName;
                entity1.LastName = register.LastName;
                entity1.EmailId = register.EmailId;
                entity1.password = register.Password;
                fundoContext.Users.Add(entity1);
                fundoContext.SaveChanges();
                return entity1;
            }
        }
        public List<UserEntity> GetFirstNameOFUsers()
        {
            List<UserEntity> userList = new List<UserEntity>();

            var users = fundoContext.Users.ToList();

            if (users.Count > 0)
            {
                foreach (var i in users)
                {
                    if (i.FirstName.StartsWith("S", StringComparison.OrdinalIgnoreCase))
                    {
                        userList.Add(i);
                    }
                }
                return userList;
            }

            return null;
        }

        //deleting the particular id
        public int DeleteNote(int userId)
        {
            var use = fundoContext.notes.FirstOrDefault(x => x.UserId == userId);
            if (use == null)
                return 0;

            fundoContext.notes.Remove(use);

            return fundoContext.SaveChanges();
        }
        // encrypting the Password
        public static string EncryptPassword(string password)
        {
            try
            {
                string strmsg = string.Empty;
                byte[] encode = new byte[password.Length];
                encode = Encoding.UTF8.GetBytes(password);
                strmsg = Convert.ToBase64String(encode);
                return strmsg;
            }
            catch (Exception ex)
            {

                return $"Encryption failed {ex.Message}";
            }
        }
        public static string DecryptPassword(string encryptedPassword)
        {
            try
            {
                byte[] decrypt_password = Convert.FromBase64String(encryptedPassword);
                string originalPassword = Encoding.UTF8.GetString(decrypt_password);
                return originalPassword;
            }
            catch (Exception ex)
            {
                return $"Decryption Failed.! {ex.Message}";
            }
        }
        public String Login(LoginModel loginmodel)
        {
            var checkUser = fundoContext.Users.FirstOrDefault (x => x.EmailId == loginmodel.EmailId);
            if(checkUser != null && DecryptPassword(checkUser.password)==(loginmodel.Password))
            {
                return GenerateToken(checkUser.EmailId, checkUser.UserId);
            }
            else
            {
                return null;
            }
        }
public string GenerateToken(string email,  int userId)
        {
            try
            {
                var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
                var credentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256);
                var claims = new[]
                {
                    new Claim("email", email),
        new Claim("userId", userId.ToString())
        };
                var token = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(configuration["Jwt:Issuer"],
                    configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.Now.AddDays(2),
                    signingCredentials: credentials);


                return new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {

                return ex.Message;
            }


        }
        public async Task<string> ForgotPassword(string emailTo, IBus bus)
        {
            try
            {
                if (String.IsNullOrEmpty(emailTo))
                {
                    return null;
                }
                Sent send = new Sent();
                send.SendingMail(emailTo);
                Uri uri = new Uri("rabbitmq://localhost/NotesEmail_Query");
                var endPoint = await bus.GetSendEndpoint(uri);
                return "success";

            }
            catch (Exception ex)
            {

                return ex.Message;
            }       }
        public String ResetPassword(string email, string password, string confirm_password)
        {
            var result = fundoContext.Users.FirstOrDefault(z => z.EmailId == email);
            if (result != null && password == confirm_password)
            {
                result.password = EncryptPassword(password);
                fundoContext.SaveChanges();
                return result.password;
            }
            return null;
        }


    }
}
