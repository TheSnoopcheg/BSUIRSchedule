using BSUIRSchedule.Classes;
using BSUIRSchedule.Models;
using BSUIRSchedule.Services;
using BSUIRSchedule.Views;
using BSUIRSchedule.Controls;

using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Threading.Tasks;

using ReactiveUI;
using Avalonia.Input;
using Avalonia.Controls.ApplicationLifetimes;

namespace BSUIRSchedule.ViewModels
{
    public class MainViewModel : ViewModelBase, IMainViewModel
    {
        readonly IMainModel _model;
        private readonly IScheduleSearchViewModel _scheduleSearchViewModel;
        private readonly IAnnouncementViewModel _announcementViewModel;
        private readonly INoteViewModel _noteViewModel;

        #region Properties

        private int selectedTab = 0;
        public int SelectedTab
        {
            get => selectedTab;
            set => this.RaiseAndSetIfChanged(ref selectedTab, value);
        }
        public Schedule? Schedule => _model.Schedule;

        public ObservableCollection<FavoriteSchedule> FavoriteSchedules => _model.FavoriteSchedules;
        
        public bool NotesExistenceVisibility
        {
            get => !_noteViewModel.IsNotesEmpty;
        }
        public bool AnnouncementsExistenceVisibility
        {
            get => !_announcementViewModel.IsAnnouncementsEmpty;
        }

        public Cursor ScheduleNameCursor
        {
            get
            {
                if(Schedule == null) return new Cursor(StandardCursorType.Arrow);
                return Schedule.studentGroup == null ? new Cursor(StandardCursorType.Help) : new Cursor(StandardCursorType.Arrow);
            }
        }

        #endregion

        #region Commands

        private ICommand? searchSchedule;
        public ICommand SearchSchedule
        {
            get
            {
                return searchSchedule ??
                    (searchSchedule = new RelayCommand(async obj =>
                    {
                        ScheduleSearchWindow scheduleSearchWindow = new ScheduleSearchWindow();
                        _scheduleSearchViewModel.ClearInput();
                        scheduleSearchWindow.DataContext = _scheduleSearchViewModel;
                        if (App.Current!.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop) return;
                        if (await scheduleSearchWindow.ShowDialog<bool>(desktop.MainWindow!))
                        {
                            SearchResponse? response = _scheduleSearchViewModel.SearchResponse;
                            string? url = string.Empty;
                            if (response == null)
                            {
                                string input = _scheduleSearchViewModel.Input;
                                if (input.Length == 6 && int.TryParse(input, out int res))
                                {
                                    url = input;
                                }
                            }
                            else
                            {
                                url = response.GetUrl();
                            }
                            await LoadScheduleAsync(url, LoadingType.Server);
                        }
                    }));
            }
        }
        
        // command to load schedule from schedule's plate
        private ICommand? loadScheduleFromPlate;
        public ICommand LoadScheduleFromPlate
        {
            get
            {
                return loadScheduleFromPlate ??
                    (loadScheduleFromPlate = new RelayCommand(async obj =>
                    {
                        if (obj is not string url) return;
                        await LoadScheduleAsync(url, LoadingType.Server);
                    }));
            }
        }

        private ICommand? openAnnouncements;
        public ICommand OpenAnnouncements
        {
            get
            {
                return openAnnouncements ??
                    (openAnnouncements = new RelayCommand(async obj =>
                    {
                        if (Schedule != null)
                        {
                            AnnouncementWindow announcementWindow = new AnnouncementWindow();
                            announcementWindow.DataContext = _announcementViewModel; 
                            EventHandler windowCloseHandler = null;
                            windowCloseHandler = async (s, e) =>
                            {
                                announcementWindow.Close();
                                await LoadScheduleAsync(_announcementViewModel.CommandUrl, LoadingType.Server);
                                _announcementViewModel.OnRequestClose -= windowCloseHandler;
                            };
                            _announcementViewModel.OnRequestClose += windowCloseHandler;
                            if (App.Current!.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop) return;
                            await announcementWindow.ShowDialog(desktop.MainWindow!);
                        }
                    }));
            }
        }

        private ICommand? openSettingsWindow;
        public ICommand OpenSettingsWindow
        {
            get
            {
                return openSettingsWindow ??
                    (openSettingsWindow = new RelayCommand(obj =>
                    {
                        SettingsWindow settingsWindow = new SettingsWindow();
                        if (App.Current!.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop) return;
                        settingsWindow.ShowDialog(desktop.MainWindow!);
                    }));
            }
        }

        private ICommand? openNotesWindow;
        public ICommand OpenNotesWindow
        {
            get
            {
                return openNotesWindow ??
                    (openNotesWindow = new RelayCommand(obj =>
                    {
                        if(Schedule != null)
                        {
                            NotesWindow notesWindow = new NotesWindow();
                            notesWindow.DataContext = _noteViewModel;
                            if (App.Current!.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop) return;
                            notesWindow.ShowDialog(desktop.MainWindow!);
                        }
                    }));
            }
        }

        private ICommand? addFavoriteSchedule;
        public ICommand AddFavoriteSchedule
        {
            get
            {
                return addFavoriteSchedule ??
                    (addFavoriteSchedule = new RelayCommand(async obj =>
                    {
                        if (Schedule != null)
                        {
                            if (Schedule.favorited)
                                await _model.DeleteFavoriteScheduleAsync(Schedule);
                            else
                                await _model.AddFavoriteScheduleAsync(Schedule);

                            Schedule.favorited = !Schedule.favorited;
                            Schedule.RaisePropertyChanged(nameof(Schedule.favorited));
                            await _model.SaveRecentScheduleAsync(Schedule);
                            await _noteViewModel.SetNotes(Schedule.GetName(), Schedule.GetUrl());
                        }
                    }));
            }
        }

        // command to add favorite schedule without presentation
        private ICommand? addFavoriteScheduleWP;
        public ICommand AddFavoriteScheduleWP
        {
            get
            {
                return addFavoriteScheduleWP ??
                    (addFavoriteScheduleWP = new RelayCommand(async obj =>
                    {
                        //ScheduleSearchWindow scheduleSearchWindow = new ScheduleSearchWindow();
                        //_scheduleSearchViewModel.ClearInput();
                        //scheduleSearchWindow.DataContext = _scheduleSearchViewModel;
                        //if (scheduleSearchWindow.ShowDialog() == true)
                        //{
                        //    SearchResponse? response = _scheduleSearchViewModel.SearchResponse;
                        //    string? url = string.Empty;
                            //if (response == null)
                            //{
                            //    string input = _scheduleSearchViewModel.Input;
                            //    if (input.Length == 6 && int.TryParse(input, out int res))
                            //    {
                            //        url = input;
                            //    }
                            //}
                            //else
                            //{
                            //    url = response.GetUrl();
                            //}
                            //await LoadScheduleAsync(url, LoadingType.Server);

                        //    if (Schedule == null) return;
                        //    if (Schedule.GetUrl() != response.GetUrl()) return;

                        //    _model.Schedule!.favorited = true;
                        //    Schedule.favorited = true;
                        //    await _model.SaveRecentScheduleAsync(Schedule);
                        //    OnPropertyChanged(nameof(Schedule));
                        //}
                    }));
            }
        }

        private ICommand? loadFavoriteSchedule;
        public ICommand LoadFavoriteSchedule
        {
            get
            {
                return loadFavoriteSchedule ??
                    (loadFavoriteSchedule = new RelayCommand(async obj =>
                    {
                        if (obj is RoundButton rb && rb.DataContext is FavoriteSchedule fs)
                        {
                            await LoadScheduleAsync(fs.UrlId, LoadingType.Local);
                        }
                    }));
            }
        }

        private ICommand? deleteFavoriteSchedule;
        public ICommand DeleteFavoriteSchedule
        {
            get
            {
                return deleteFavoriteSchedule ??
                    (deleteFavoriteSchedule = new RelayCommand(async obj =>
                    {
                        if (obj is RoundButton rb && rb.DataContext is FavoriteSchedule fs)
                        {
                            await _model.DeleteFavoriteScheduleAsync(fs);
                            OnScheduleUnFavorited(fs);
                        }
                    }));
            }
        }

        #endregion

        #region Methods

        private async Task LoadScheduleAsync(string? url, LoadingType loadingType)
        {
            if (await _model.LoadScheduleAsync(url, loadingType))
                await OnScheduleLoaded();
        }

        private async void LoadRecentSchedule()
        {
            await LoadScheduleAsync("recent", LoadingType.Local);
        }
        private async void OnScheduleUnFavorited(FavoriteSchedule schedule)
        {
            if (Schedule == null) return;
            if (Schedule.GetUrl() == schedule.UrlId)
            {
                Schedule!.favorited = false;
                this.RaisePropertyChanged(nameof(Schedule));
                await _model.SaveRecentScheduleAsync(Schedule);
            }
        }

        private void SetCurrentTab()
        {
            if (Schedule!.exams?.Count > 0 && DateTime.TryParse(Schedule!.startExamsDate, out DateTime startExamsDate)
                && DateTime.TryParse(Schedule!.endExamsDate, out DateTime endExamsDate) && startExamsDate <= DateTime.Today
                && endExamsDate >= DateTime.Today)
            {
                SelectedTab = 1;
            }
            else if (Schedule.previousDailyLessons?.Count > 0 && (Schedule.dailyLessons?.Count ?? 0) == 0)
            {
                SelectedTab = 2;
            }
            else
            {
                SelectedTab = 0;
            }
        }

        private async Task OnScheduleLoaded()
        {
            if (Schedule == null) return;
            this.RaisePropertyChanged(nameof(Schedule));
            string? url = Schedule.GetUrl();
            string? name = Schedule.GetName();
            this.RaisePropertyChanged(nameof(ScheduleNameCursor));

            SetCurrentTab();

            if (_model.IsScheduleFavorited(url!))
            {
                Schedule!.favorited = true;
                Schedule.RaisePropertyChanged(nameof(Schedule.favorited));
                if (await _noteViewModel.SetNotes(name, url))
                    this.RaisePropertyChanged(nameof(NotesExistenceVisibility));
                if (await _model.UpdateScheduleAsync())
                {
                    this.RaisePropertyChanged(nameof(Schedule));
                }
            }

            if (await _announcementViewModel.SetAnnouncements(name, url))
                this.RaisePropertyChanged(nameof(AnnouncementsExistenceVisibility));
        }

        #endregion
        
        public MainViewModel(IMainModel mainWindowModel,
                             IScheduleSearchViewModel scheduleSearchViewModel,
                             IAnnouncementViewModel announcementViewModel,
                             INoteViewModel noteViewModel)
        {
            
            _model = mainWindowModel;
            _scheduleSearchViewModel = scheduleSearchViewModel;
            _announcementViewModel = announcementViewModel;
            _noteViewModel = noteViewModel;

            _noteViewModel.NotesChanged += () => this.RaisePropertyChanged(nameof(NotesExistenceVisibility));

            LoadRecentSchedule();
        }

    }
}