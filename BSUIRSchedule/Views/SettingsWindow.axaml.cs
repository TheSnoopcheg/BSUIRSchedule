using Avalonia.Input;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System;

namespace BSUIRSchedule.Views;

public partial class SettingsWindow : Window
{
    public SettingsWindow()
    {
        InitializeComponent();
    }

    private void Window_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if(OperatingSystem.IsWindows())
            this.BeginMoveDrag(e);
    }

    private void CloseButton_Click(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }
}