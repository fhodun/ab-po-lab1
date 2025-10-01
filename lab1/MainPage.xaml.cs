namespace lab1;

public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage()
    {
        InitializeComponent();
    }

    // private void OnCounterClicked(object? sender, EventArgs e)
    // {
    //     count++;
    //
    //     if (count == 1)
    //         CounterBtn.Text = $"Clicked {count} time";
    //     else
    //         CounterBtn.Text = $"Clicked {count} times";
    //
    //     SemanticScreenReader.Announce(CounterBtn.Text);
    // }

    private void CalculateBtn_OnClicked(object? sender, EventArgs e)
    {
    }

    private void AddBtn_OnClicked(object? sender, EventArgs e)
    {
        InputEntry.Text += "+";
    }

    private void SubtractBtn_OnClicked(object? sender, EventArgs e)
    {
        InputEntry.Text += "−";
    }

    private void MultiplyBtn_OnClicked(object? sender, EventArgs e)
    {
        InputEntry.Text += "×";
    }

    private void DivideBtn_OnClicked(object? sender, EventArgs e)
    {
        InputEntry.Text += "÷";
    }

    private void InputEntry_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
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

        InputEntry.Text = filtered.ToString();
    }

    private void ClearBtn_OnClicked(object? sender, EventArgs e)
    {
        InputEntry.Text = string.Empty;
    }
}