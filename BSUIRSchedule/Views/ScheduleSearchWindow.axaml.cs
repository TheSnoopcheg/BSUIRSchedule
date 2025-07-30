using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.ReactiveUI;
using BSUIRSchedule.ViewModels;
using System;

namespace BSUIRSchedule;

public partial class ScheduleSearchWindow : ReactiveWindow<IScheduleSearchViewModel>
{
    public ScheduleSearchWindow()
    {
        InitializeComponent();
    }
    private void SearchBox_TextChanged(object? sender, RoutedEventArgs e)
    {
        if (searchBox.Text.Length > 1)
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

    private void Window_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (OperatingSystem.IsWindows())
            BeginMoveDrag(e);
    }

    private void Window_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Escape)
            this.Close(false);
    }
}