using RespositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RespositoryLayer.Interfaces
{
    public interface IUserColloboratory
    {
        public Colloboratory AddColloboratory(int c_id, string c_Email, int notesId, int UserId);

    }
}
