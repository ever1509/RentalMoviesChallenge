using System;
using System.Collections.Generic;
using System.Text;
using RentalMovies.Infrastucture.Mail.EmailConfiguration;

namespace RentalMovies.Infrastucture.Mail
{
    public interface IEmailService
    {
        void Send(EmailMessage emailMessage);
        List<EmailMessage> ReceiveEmail(int maxCount = 10);

    }
}
