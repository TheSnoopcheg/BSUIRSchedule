using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;

namespace BSUIRSchedule.Views;

public partial class NotesWindow : Window
{
    public NotesWindow()
    {
        InitializeComponent();
    }

    private void Window_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        if(OperatingSystem.IsWindows())
            this.BeginMoveDrag(e);
    }
    private void CloseButton_Click(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }
}