using System;
using System.Collections.Generic;
using System.Text;

namespace MyInfoDAL.EmailRepo
{
    public interface IEmailRepository
    {
        void SendEmail(string emailbody, string Subject);
    }
}
