using Contacts.Maui.Model;

namespace Contacts.Maui.Views;

public partial class AddContactPage : ContentPage
{
	public AddContactPage()
	{
		InitializeComponent();
	}

    private void contactCtrl_OnSave(object sender, EventArgs e)
    {
        ContactRepo.AddContact(new Model.Contacts
        {
            Name = contactCtrl.Name,
            Address = contactCtrl.Address,
            EmailAddress = contactCtrl.Email,
            Phone = contactCtrl.Phone
        });

        Shell.Current.GoToAsync("..");
    }

    private void contactCtrl_OnCancel(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("..");
    }

    private void contactCtrl_OnError(object sender, string e)
    {
        DisplayAlert("Error", e, "OK");
    }
}