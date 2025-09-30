using System.Globalization;

namespace DigitronMaui;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnOpClicked(object sender, EventArgs e)
    {
        if (!TryGetInputs(out decimal a, out decimal b))
            return;

        string op = (sender as Button)?.CommandParameter?.ToString() ?? "";
        try
        {
            decimal result = op switch
            {
                "+" => a + b,
                "-" => a - b,
                "*" => a * b,
                "/" => b == 0 ? throw new DivideByZeroException() : a / b,
                _ => 0
            };

            LblResult.Text = result.ToString(CultureInfo.CurrentCulture);
        }
        catch (DivideByZeroException)
        {
            await DisplayAlert("Greška", "Deljenje nulom nije dozvoljeno.", "OK");
        }
    }

    private bool TryGetInputs(out decimal a, out decimal b)
    {
        var culture = CultureInfo.CurrentCulture;

        bool okA = decimal.TryParse(EntryA.Text, NumberStyles.Number, culture, out a);
        bool okB = decimal.TryParse(EntryB.Text, NumberStyles.Number, culture, out b);

        if (!okA || !okB)
        {
            DisplayAlert("Neispravan unos",
                "Unesite ispravne brojeve (tačka/zarez prema regionalnim postavkama).",
                "OK");
            return false;
        }
        return true;
    }

    private void OnClearClicked(object sender, EventArgs e)
    {
        EntryA.Text = "";
        EntryB.Text = "";
        LblResult.Text = "-";
        EntryA.Focus();
    }
}
