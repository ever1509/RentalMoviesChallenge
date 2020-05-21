using System.Collections.Generic;
using RentalMovies.Infrastructure.Mail.EmailConfiguration;

namespace RentalMovies.Infrastructure.Mail
{
    public interface IEmailService
    {
        void Send(EmailMessage emailMessage);
        List<EmailMessage> ReceiveEmail(int maxCount = 10);

    }
}
