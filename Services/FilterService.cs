namespace MyMoney.Services;

public class FilterService
{
    public event Action? OnFiltersChanged;

    public string SelectedCategory { get; private set; } = string.Empty;
    public int SelectedMonth { get; private set; }

    public void SetFilters(string category, int month)
    {
        SelectedCategory = category;
        SelectedMonth = month;
        OnFiltersChanged?.Invoke(); // Notifica os componentes
    }
}
