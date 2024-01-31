using BussinessLayer.Interface;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RespositoryLayer.Entity;
using RespositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Services
{
    public class BussinessColloboratory:IBussinessColloboratory
    {
        private readonly IUserColloboratory userColloboratory;
        public BussinessColloboratory(IUserColloboratory userColloboratory)
        {
            this.userColloboratory = userColloboratory;
        }

        public Colloboratory AddColloboratory(int c_id, string c_Email, int notesId, int UserId)
        {
            return userColloboratory.AddColloboratory(c_id, c_Email, notesId, UserId);
        }

    }
}
