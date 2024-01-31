using RespositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Interface
{
    public interface IBussinessColloboratory
    {
        public Colloboratory AddColloboratory(int c_id, string c_Email, int notesId, int UserId);

    }
}
