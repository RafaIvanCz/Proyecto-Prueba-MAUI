using BNails.Views;

namespace BNails
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        private void OnShowPasswordCheckBox(object sender,CheckedChangedEventArgs e)
        {
            txtPassword.IsPassword = !e.Value;
        }

        private async void TapRegistrate_Tapped(object sender,TappedEventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(Registro));
        }
    }
}
