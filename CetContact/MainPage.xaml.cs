using CetContact.ViewModels;

namespace CetContact;

public partial class MainPage : ContentPage
{
    

    public MainPage(ContactViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    
}