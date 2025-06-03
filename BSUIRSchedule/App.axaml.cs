using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using BSUIRSchedule.Models;
using BSUIRSchedule.Services;
using BSUIRSchedule.Themes.ColorDictionaries;
using BSUIRSchedule.ViewModels;
using BSUIRSchedule.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;

namespace BSUIRSchedule;

public partial class App : Application
{
    public IServiceProvider ServiceProvider { get; private set; }
    private IMainViewModel _mainViewModel;
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);

        LoadTheme();
    }

    public override void OnFrameworkInitializationCompleted()
    {
        ConfigureServices();
        Langs.Language.Culture = new CultureInfo("en-US");
        _mainViewModel = ServiceProvider.GetService<IMainViewModel>();
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = _mainViewModel
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = _mainViewModel
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void ConfigureServices()
    {
        var services = new ServiceCollection();
        services.AddSingleton<App>();

        services.AddSingleton<IMainModel, MainModel>();
        services.AddSingleton<IScheduleSearchModel, ScheduleSearchModel>();
        services.AddSingleton<IAnnouncementModel, AnnouncementModel>();
        services.AddSingleton<INoteModel, NoteModel>();

        services.AddSingleton<IMainViewModel, MainViewModel>();
        services.AddSingleton<IScheduleSearchViewModel, ScheduleSearchViewModel>();
        services.AddSingleton<INoteViewModel, NoteViewModel>();
        services.AddSingleton<IAnnouncementViewModel, AnnouncementViewModel>();

        services.AddSingleton<INetworkService, NetworkService>();
        services.AddSingleton<IInternetService, InternetService>();

        services.AddTransient<INoteService, NoteService>();
        services.AddTransient<IFavoriteSchedulesService, FavoriteSchedulesService>();
        services.AddTransient<IAnnouncementService, AnnouncementService>();
        services.AddTransient<IScheduleService, ScheduleService>();
        ServiceProvider = services.BuildServiceProvider();
    }

    private void LoadTheme()
    {
        Styles.Add(new IISTheme());
    }
}
