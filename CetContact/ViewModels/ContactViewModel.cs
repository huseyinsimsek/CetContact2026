using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using CetContact.Data;
using CetContact.Models;

namespace CetContact.ViewModels;

public partial class ContactViewModel : ObservableObject
{
    private readonly CetContactDatabase _database;

    public ContactViewModel(CetContactDatabase database)
    {
        _database = database;
        LoadContacts();
    }
    
    // ObservableCollection tells the list on screen when items are added/removed.
    public ObservableCollection<PhoneContact> Contacts { get; } = [];

    // [ObservableProperty] generates the property-changed code for us.
    [ObservableProperty]
    public partial string Name { get; set; } = "";

    [ObservableProperty]
    public partial string Phone { get; set; } = "";

    [ObservableProperty]
    public partial string Email { get; set; } = "";

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SaveButtonText))]
    public partial PhoneContact? SelectedContact { get; set; }

    public string SaveButtonText => SelectedContact == null ? "Add" : "Update";

    // Runs automatically when SelectedContact changes: copy its values into the form.
    partial void OnSelectedContactChanged(PhoneContact? value)
    {
        Name = value?.Name ?? "";
        Phone = value?.PhoneNumber ?? "";
        Email = value?.Email ?? "";
    }

    private void LoadContacts()
    {
        Contacts.Clear();
        foreach (var contact in _database.GetAll())
            Contacts.Add(contact);
    }

    // [RelayCommand] generates a "SaveCommand" property that buttons can bind to.
    [RelayCommand]
    private void Save()
    {
        if (string.IsNullOrWhiteSpace(Name)) return;

        if (SelectedContact == null)
            _database.AddContact(new PhoneContact { Name = Name, PhoneNumber = Phone, Email = Email });
        else
            _database.Update(new PhoneContact { Id = SelectedContact.Id, Name = Name, PhoneNumber = Phone, Email = Email });

        LoadContacts();
        Clear();
    }

    [RelayCommand]
    private  void Delete()
    {
        if (SelectedContact == null) return;

        _database.Delete(SelectedContact.Id);
        LoadContacts();
        Clear();
    }

    [RelayCommand]
    private void Clear()
    {
        SelectedContact = null;
        Name = "";
        Phone = "";
        Email = "";
    }
    
    

}