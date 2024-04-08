using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Maui.Model
{
    public static class ContactRepo
    {
        public static List<Contacts> _contacts = new List<Contacts>()
        {
            new Contacts { ContactId = 1, Name="Daniel Stennett", Phone="0789456123", Address="1 Take Street", EmailAddress="pontipak@hotmail.com"},
            new Contacts { ContactId = 2, Name="Timi Stennett", EmailAddress="claypool@yahoo.ca"},
            new Contacts { ContactId = 3, Name="Alyssa Stennett", Phone="06547896325",Address="3 Take Street"}
        };

        public static List<Contacts> GetContacts() => _contacts;

        public static Contacts GetContactById(int contactID)
        {
            var contact = _contacts.FirstOrDefault(x => x.ContactId == contactID);
            if (contact != null)
            {
                return new Contacts
                {   
                    ContactId = contact.ContactId,
                    Name = contact.Name,
                    Phone = contact.Phone,
                    Address = contact.Address,
                    EmailAddress = contact.EmailAddress
                };
            }
            return null;
        }

        public static void AddContact(Contacts contact)
        {
            var maxID = _contacts.Max(x => x.ContactId);
            contact.ContactId = maxID + 1;
            _contacts.Add(contact);
        }

        public static void UpdateContactById(int contactID, Contacts contact)
        {
            if (contactID != contact.ContactId) return;

            var updateContact = _contacts.FirstOrDefault(x => x.ContactId == contactID);
            if (updateContact != null)
            {
                updateContact.Name = contact.Name;
                updateContact.Address = contact.Address;
                updateContact.Phone = contact.Phone;
                updateContact.EmailAddress = contact.EmailAddress;
            }
        }

        public static void DeleteContact(int contactID)
        {
            var deleteContact = _contacts.FirstOrDefault(x => x.ContactId == contactID);
            if (deleteContact != null)
            {
                _contacts.Remove(deleteContact);
            }
        }

        public static List<Contacts> SearchContacts(string filteredText)
        {
            var contacts = _contacts.Where(x => !string.IsNullOrWhiteSpace(x.Name) && x.Name.Contains(filteredText, StringComparison.OrdinalIgnoreCase))?.ToList();

            if (contacts == null || contacts.Count <= 0)
            {
                contacts = _contacts.Where(x => !string.IsNullOrWhiteSpace(x.EmailAddress) && x.EmailAddress.Contains(filteredText, StringComparison.OrdinalIgnoreCase))?.ToList();
            }
            else
            {
                return contacts;
            }

            if (contacts == null || contacts.Count <= 0)
            {
                contacts = _contacts.Where(x => !string.IsNullOrWhiteSpace(x.Phone) && x.Phone.Contains(filteredText, StringComparison.OrdinalIgnoreCase))?.ToList();
            }
            else
            {
                return contacts;
            }

            if (contacts == null || contacts.Count <= 0)
            {
                contacts = _contacts.Where(x => !string.IsNullOrWhiteSpace(x.Address) && x.Address.Contains(filteredText, StringComparison.OrdinalIgnoreCase))?.ToList();
            }
            else
            {
                return contacts;
            }

            return contacts;

        }

    }
}
