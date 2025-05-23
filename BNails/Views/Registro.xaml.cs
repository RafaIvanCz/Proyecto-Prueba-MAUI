using System.Text.RegularExpressions;

namespace BNails.Views;

public partial class Registro : ContentPage
{
	public Registro()
	{
		InitializeComponent();
    }

    private async void OnVolverAlLogin(object sender,EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }

    private async void OnRegistrarse(object sender,EventArgs e)
    {
        string? nombre = txtNombre.Text?.Trim();
        string? correo = txtEmail.Text?.Trim();
        string? password = txtPassword.Text;
        string? rePassword = txtRePassword.Text;

        if(string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(rePassword))
        {
            await DisplayAlert("Error", "Tenés que completar todos los campos.","OK");
            return;
        }

        if(!EsCorreoValido(correo))
        {
            await DisplayAlert("Error","El correo electrónico no es válido","OK");
            return;
        }

        if(!EsContraseñaSegura(password))
        {
            await DisplayAlert("Error","La contraseña debe tener al menos 8 caracteres, una mayúscula y un número","OK");
            return;
        }

        if(password != rePassword)
        {
            await DisplayAlert("Error", "Ambas contraseñas deben ser iguales", "OK");
            txtPassword.Text = "";
            txtRePassword.Text = "";
            return;
        }

    }

    private void OnTxtPasswordChanged(object sender,TextChangedEventArgs e)
    {
        string password = e.NewTextValue ?? "";

        if(txtPassword.IsFocused)
            ValidacionesPassword.IsVisible = true;

        MostrarValidacion(lblMinCaracteres,password.Length >= 8);
        MostrarValidacion(lblMayuscula,password.Any(char.IsUpper));
        MostrarValidacion(lblNumero,password.Any(char.IsDigit));

        VerificarCoincidenciaPasswords();

        ValidacionesPassword.IsVisible = lblMinCaracteres.IsVisible || lblMayuscula.IsVisible || lblNumero.IsVisible;
    }

    private void MostrarValidacion(Label label,bool condicion)
    {
        label.IsVisible = !condicion;
    }

    private void OnTxtRePasswordChanged(object sender,TextChangedEventArgs e)
    {
        VerificarCoincidenciaPasswords();
    }

    private void VerificarCoincidenciaPasswords()
    {
        bool sonIguales = txtPassword.Text == txtRePassword.Text;

        lblRePassword.IsVisible = !sonIguales && !string.IsNullOrEmpty(txtRePassword.Text);
    }

    private bool EsCorreoValido(string correo)
    {
        return Regex.IsMatch(correo,
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            RegexOptions.IgnoreCase);
    }

    private bool EsContraseñaSegura(string password)
    {
        return Regex.IsMatch(password,
            @"^(?=.*[A-Z])(?=.*\d).{8,}$");
    }

    private void OnShowPasswordRegistro(object sender,CheckedChangedEventArgs e)
    {
        txtPassword.IsPassword = !e.Value;
        txtRePassword.IsPassword = !e.Value;
    }
}