using RespositoryLayer.Context;
using RespositoryLayer.Entity;
using RespositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RespositoryLayer.Services
{
    public class UserColloboratory:IUserColloboratory
    {
        private readonly FundoContext fundoContext;
        public UserColloboratory(FundoContext fundoContext)
        {
            this.fundoContext = fundoContext;
        }
        public Colloboratory AddColloboratory(int c_id, string c_Email, int notesId, int UserId)
        {
            Colloboratory col = new Colloboratory();
            
            col.c_Email = c_Email;
            col.notesId = notesId;
            col.UserId = UserId;
            fundoContext.colloborate.Add(col);
             fundoContext.SaveChanges();
            return col;
        }
    }
}
