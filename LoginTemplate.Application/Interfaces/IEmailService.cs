using LoginTemplate.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginTemplate.Application.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(Message msg);
    }
}
