using InternetBank.Domain.Accounts.Entities;
using InternetBank.Domain.Accounts.ValueObjects;
using InternetBank.Domain.Repositories;
using InternetBank.Domain.Transactions.Entities;
using InternetBank.Infrastructure.Data;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using MimeKit;

namespace InternetBank.Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
        public bool SendEmailForCompleteTransaction(string destinationEmail, double Amount, string DestinationCardNumber, string SourceCardNumber, string UserId)
        {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("tesstsetapp23@outlook.com"));
                email.To.Add(MailboxAddress.Parse(destinationEmail));
                email.Subject = "Transaction completed";
                email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                { Text = $"Congratulations!.. \namount : {Amount} \n----Destination Card Number : {DestinationCardNumber} \n----Source Card Number : {SourceCardNumber}" };

                using var smtp = new SmtpClient();
                smtp.Connect("smtp-mail.outlook.com",
                             587,
                             MailKit.Security.SecureSocketOptions.StartTls);
                smtp.Authenticate("tesstsetapp23@outlook.com", "Emailfortest6650");
                smtp.Send(email);
                smtp.Disconnect(true);
                return true;
        }

        public bool SendEmailForCreatingAccount(string destinationEmail,
                                                    string accountType,
                                                    double amount,
                                                    string accountNumber,
                                                    string cardNumber)
        {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("tesstsetapp23@outlook.com"));
                email.To.Add(MailboxAddress.Parse(destinationEmail));
                email.Subject = "Account Created";
                email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                { Text = $"Congratulations!.. \naccount type : {accountType} \n----amount : {amount} \n----account number : {accountNumber} \n-----card number :{cardNumber} " };

                using var smtp = new SmtpClient();
                smtp.Connect("smtp-mail.outlook.com",
                             587,
                             MailKit.Security.SecureSocketOptions.StartTls);
                smtp.Authenticate("tesstsetapp23@outlook.com", "Emailfortest6650");
                smtp.Send(email);
                smtp.Disconnect(true);
                return true;
        }

        public bool SendEmailForCreatingTransaction(string destinationEmail, double Amount, string DestinationCardNumber, string SourceCardNumber, string UserId)
        {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("tesstsetapp23@outlook.com"));
                email.To.Add(MailboxAddress.Parse(destinationEmail));
                email.Subject = "Transaction created";
                email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                { Text = $"Congratulations!.. \namount : {Amount} \n----Destination Card Number : {DestinationCardNumber} \n----Source Card Number : {SourceCardNumber}" };

                using var smtp = new SmtpClient();
                smtp.Connect("smtp-mail.outlook.com",
                             587,
                             MailKit.Security.SecureSocketOptions.StartTls);
                smtp.Authenticate("tesstsetapp23@outlook.com", "Emailfortest6650");
                smtp.Send(email);
                smtp.Disconnect(true);
                return true;
        }

        public string SendOTP(string receptor, double amount, Otp otp)
        {

                var time = TimeOnly.FromDateTime(DateTime.UtcNow);
                var strTime = time.Hour.ToString() + ":" + time.Minute.ToString() + ":" + time.Second.ToString();
                var api = new Kavenegar.KavenegarApi("79677737392F5164527849523448454B45654F702F3476416F65665159796E4F342F3146754536453966773D");
                var res = api.VerifyLookup(receptor,
                                           otp.Value.ToString(),
                                           amount.ToString(),
                                           strTime,
                                           "OtpSimulatorTest");
                return res.StatusText;
        }
}
