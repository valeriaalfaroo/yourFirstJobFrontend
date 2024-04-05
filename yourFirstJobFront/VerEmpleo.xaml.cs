namespace yourFirstJobFront;

public partial class VerEmpleo : ContentPage
{
	public VerEmpleo()
	{
		InitializeComponent();
	}
    private void Homebtn_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MainPage());
    }
   
    private void Searchbtn_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MainPage());
    }

    private void Applicationbtn_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MainPage());
    }

    private void Profilebtn_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MainPage());
    }

    private void btnAplicar_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Aplicar()); 
    }
}