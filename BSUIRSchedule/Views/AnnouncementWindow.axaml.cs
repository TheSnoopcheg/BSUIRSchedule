using Avalonia.Interactivity;
using Avalonia.ReactiveUI;
using BSUIRSchedule.ViewModels;
using System;

namespace BSUIRSchedule.Views;

public partial class AnnouncementWindow : ReactiveWindow<IAnnouncementViewModel>
{
    public AnnouncementWindow()
    {
        InitializeComponent();
    }

    private void CloseButton_Click(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void Window_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        if(OperatingSystem.IsWindows())
            BeginMoveDrag(e);
    }
}