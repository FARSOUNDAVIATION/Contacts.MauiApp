using CommunityToolkit.Maui.Behaviors;
using Contacts.Maui.Model;
using Microsoft.Maui.ApplicationModel.Communication;
using Contact = Contacts.Maui.Model.Contacts;

namespace Contacts.Maui.Views;

[QueryProperty(nameof(ContactId), "Id")]
public partial class EditContactPage : ContentPage
{
    public Contact contact;
    public EditContactPage()
	{
		InitializeComponent();
	}

    public string ContactId
    {
        set 
        { 
            contact = ContactRepo.GetContactById(int.Parse(value));
            if (contact != null)
            {
                contactCtrl.Name = contact.Name;
                contactCtrl.Address = contact.Address;
                contactCtrl.Phone = contact.Phone;
                contactCtrl.Email = contact.EmailAddress;
            }           
        }
    }

    private void contactCtrl_OnSave(object sender, EventArgs e)
    {
        contact.Name = contactCtrl.Name;
        contact.Address = contactCtrl.Address;
        contact.Phone = contactCtrl.Phone;
        contact.EmailAddress = contactCtrl.Email;

        ContactRepo.UpdateContactById(contact.ContactId, contact);
        Shell.Current.GoToAsync("..");
    }

    private void controlCtrl_OnCancel(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("..");
    }

    private void contactCtrl_OnError(object sender, string e)
    {
        DisplayAlert("Error", e, "OK");
    }

    
}