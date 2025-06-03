using Avalonia.ReactiveUI;
using Avalonia.Interactivity;
using BSUIRSchedule.ViewModels;

namespace BSUIRSchedule;

public partial class ScheduleSearchWindow : ReactiveWindow<IScheduleSearchViewModel>
{
    public ScheduleSearchWindow()
    {
        InitializeComponent();
    }
    private void SearchBox_TextChanged(object? sender, RoutedEventArgs e)
    {
        if(searchBox.Text.Length > 1)
            searchBox.IsDropDownOpen = true;
        else
            searchBox.IsDropDownOpen = false;
    }
    private void SearchBox_ItemSelected(object? sender, RoutedEventArgs e)
    {
        this.Close(true);
    }
    private void AcceptButton_Click(object? sender, RoutedEventArgs e)
    {
        this.Close(true);
    }
    private void CancelButton_Click(object? sender, RoutedEventArgs e)
    {
        this.Close(false);
    }
}