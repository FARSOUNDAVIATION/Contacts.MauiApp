using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Alerts;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Contacts.Maui.Model;
using Microsoft.Identity.Client;
using Contact = Contacts.Maui.Model.Contacts;
using Microsoft.Identity.Client;


namespace Contacts.Maui.Views;

public partial class ContactsPage : ContentPage
{
	public ContactsPage()
	{
		InitializeComponent();		
    }

    protected override void OnAppearing()
    {
        //Each time the page regains the focus
        base.OnAppearing();

        SearchBar.Text = string.Empty;

        LoadContacts();
    }

    private void btnAdd_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(AddContactPage));
    }

    private void Delete_Clicked(object sender, EventArgs e)
    {
        var menuItem = sender as MenuItem;
        var contact = menuItem.CommandParameter as Contact;
        ContactRepo.DeleteContact(contact.ContactId);

        LoadContacts();
    }

    private void LoadContacts()
    {
        var list = new ObservableCollection<Contact>(ContactRepo.GetContacts());
        listContact.ItemsSource = list;
    }

    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        var contacts = new ObservableCollection<Contact>(ContactRepo.SearchContacts(((SearchBar)sender).Text));
        listContact.ItemsSource = contacts;
    }

    private async void listContact_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        //Display Message
        //var selected = (Contact)e.SelectedItem;
        //DisplayAlert("Name Clicked", string.Format("You just selected {0} from the list", selected.Name), "OK");

        if (listContact.SelectedItem != null)
        {
            await Shell.Current.GoToAsync($"{nameof(EditContactPage)}?Id={((Contact)listContact.SelectedItem).ContactId}");
        }
    }

    private async void Login_Clicked(object sender, EventArgs e)
    {
        try
        {
            var authService = new AuthService();
            var result = await authService.LoginAsync(CancellationToken.None);
            var token = result?.IdToken; // AccessToken also can be used
            if (token != null)
            {
                var handler = new JwtSecurityTokenHandler();
                var data = handler.ReadJwtToken(token);
                var claims = data.Claims.ToList();
                if (data != null)
                {
                    var stringBuilder = new StringBuilder();
                    stringBuilder.AppendLine($"Name: {data.Claims.FirstOrDefault(x => x.Type.Equals("name"))?.Value}");
                    stringBuilder.AppendLine($"Email: {data.Claims.FirstOrDefault(x => x.Type.Equals("preferred_username"))?.Value}");
                    await Toast.Make(stringBuilder.ToString()).Show();
                }
            }
        }
        catch (MsalClientException ex)
        {
            await Toast.Make(ex.Message).Show();
        }
    }
}