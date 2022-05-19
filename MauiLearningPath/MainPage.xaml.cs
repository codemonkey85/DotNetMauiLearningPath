namespace MauiLearningPath;

public partial class MainPage : ContentPage
{
    public MainPage() => InitializeComponent();

    private string translatedNumber;

    private void OnTranslate(object sender, EventArgs e)
    {
        var enteredNumber = PhoneNumberText.Text;
        translatedNumber = PhonewordTranslator.ToNumber(enteredNumber);

        if (!string.IsNullOrEmpty(translatedNumber))
        {
            CallButton.IsEnabled = true;
            CallButton.Text = $"Call {translatedNumber}";
        }
        else
        {
            CallButton.IsEnabled = false;
            CallButton.Text = "Call";
        }
    }

    private async void OnCall(object sender, EventArgs e)
    {
        if (await DisplayAlert(
            "Dial a Number",
            $"Would you like to call {translatedNumber}?",
            "Yes",
            "No"))
        {
            try
            {
                PhoneDialer.Open(translatedNumber);
            }
            catch (ArgumentNullException)
            {
                await DisplayAlert("Unable to dial", "Phone number was not valid.", "OK");
            }
            catch (FeatureNotSupportedException)
            {
                await DisplayAlert("Unable to dial", "Phone dialing not supported.", "OK");
            }
            catch (Exception)
            {
                // Other error has occurred.
                await DisplayAlert("Unable to dial", "Phone dialing failed.", "OK");
            }
        }
    }
}
