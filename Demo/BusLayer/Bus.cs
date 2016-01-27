using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.Storage;
using Entity;
using DataLayer;

namespace BusLayer
{
    public class Bus
    {
        public async void ComposeEmail(Windows.ApplicationModel.Contacts.Contact recipient,
        string emailAddress, StorageFile attachmentFile)
        {
            var emailMessage = new Windows.ApplicationModel.Email.EmailMessage();
            emailMessage.Body = "";
            emailMessage.Subject = "[Feedback]";
            if (attachmentFile != null)
            {
                var stream = Windows.Storage.Streams.RandomAccessStreamReference.CreateFromFile(attachmentFile);

                var attachment = new Windows.ApplicationModel.Email.EmailAttachment(
                    attachmentFile.Name,
                    stream);

                emailMessage.Attachments.Add(attachment);
            }

            var email = recipient.Emails.FirstOrDefault<Windows.ApplicationModel.Contacts.ContactEmail>();
            if (email != null)
            {
                var emailRecipient = new Windows.ApplicationModel.Email.EmailRecipient(email.Address);
                emailMessage.To.Add(emailRecipient);
            }
            else
            {
                var emailRecipient = new Windows.ApplicationModel.Email.EmailRecipient(emailAddress);
                emailMessage.To.Add(emailRecipient);

            }

            await Windows.ApplicationModel.Email.EmailManager.ShowComposeNewEmailAsync(emailMessage);

        }
    }
}
