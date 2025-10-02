using System.Net.Mime;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Font = Microsoft.Maui.Font;

namespace lab1;

public enum Operation
{
    Add,
    Subtract,
    Multiply,
    Divide
}

public partial class MainPage : ContentPage
{
    Operation? operation = null;

    public MainPage()
    {
        InitializeComponent();
    }

    private async Task<CancellationTokenSource> ShowToast(string text, bool sementic = true)
    {
        if (sementic) SemanticScreenReader.Announce(text);

        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        var toast = Toast.Make(text, ToastDuration.Short, 16);
        await toast.Show(cancellationTokenSource.Token);
        return cancellationTokenSource;
    }

    private async void CalculateBtn_OnClicked(object? sender, EventArgs e)
    {
        var firstText = FirstEntry?.Text;
        var secondText = SecondEntry?.Text;

        if (string.IsNullOrWhiteSpace(firstText))
        {
            FirstEntry?.Focus();
            await ShowToast("Uzupełnij pierwsze pole");
            return;
        }

        if (string.IsNullOrWhiteSpace(secondText))
        {
            SecondEntry?.Focus();
            await ShowToast("Uzupełnij drugie pole");
            return;
        }

        if (operation is null)
        {
            await ShowToast("Wybierz działanie");
            return;
        }

        var culture = System.Globalization.CultureInfo.GetCultureInfo("pl-PL");
        if (!double.TryParse(firstText, System.Globalization.NumberStyles.Float, culture, out var a))
        {
            FirstEntry?.Focus();
            await ShowToast("Nieprawidłowa liczba w pierwszym polu");
            return;
        }

        if (!double.TryParse(secondText, System.Globalization.NumberStyles.Float, culture, out var b))
        {
            SecondEntry?.Focus();
            await ShowToast("Nieprawidłowa liczba w drugim polu");
            return;
        }

        double result;
        switch (operation)
        {
            case Operation.Add:
                result = a + b;
                break;
            case Operation.Subtract:
                result = a - b;
                break;
            case Operation.Multiply:
                result = a * b;
                break;
            case Operation.Divide:
                if (b == 0)
                {
                    await ShowToast("Nie można dzielić przez zero");
                    return;
                }

                result = a / b;
                break;
            default:
                return;
        }

        ResultLabel.Text = string.Concat("= ", result.ToString("N2", culture));
    }

    private void AddBtn_OnClicked(object? sender, EventArgs e)
    {
        OperationLabel.Text = "+";
        operation = Operation.Add;
    }

    private void SubtractBtn_OnClicked(object? sender, EventArgs e)
    {
        OperationLabel.Text = "−";
        operation = Operation.Subtract;
    }

    private void MultiplyBtn_OnClicked(object? sender, EventArgs e)
    {
        OperationLabel.Text = "×";
        operation = Operation.Multiply;
    }

    private void DivideBtn_OnClicked(object? sender, EventArgs e)
    {
        OperationLabel.Text = "÷";
        operation = Operation.Divide;
    }

    private void InputEntry_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        if (sender is not Entry entry)
            return;

        string newText = e.NewTextValue ?? string.Empty;

        var filtered = new System.Text.StringBuilder();
        foreach (char c in newText)
        {
            if (char.IsDigit(c))
            {
                filtered.Append(c);
                continue;
            }

            if (c == '.' || c == ',')
            {
                filtered.Append(',');
                continue;
            }
        }

        if (filtered.ToString() == newText)
        {
            return;
        }

        entry.Text = filtered.ToString();
    }

    private void ClearBtn_OnClicked(object? sender, EventArgs e)
    {
        FirstEntry.Text = string.Empty;
        SecondEntry.Text = string.Empty;
        ResultLabel.Text = string.Empty;
        OperationLabel.Text = string.Empty;
        operation = null;
        FirstEntry.Focus();
    }
}