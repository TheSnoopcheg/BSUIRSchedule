using Avalonia.Input;
using BSUIRSchedule.Classes;
using System.Collections.ObjectModel;
using System.Windows.Input;
namespace BSUIRSchedule.ViewModels
{
    public interface IMainViewModel
    {
        public Schedule? Schedule { get; }
        public ObservableCollection<FavoriteSchedule> FavoriteSchedules { get; }
        public ICommand SearchSchedule { get; }
        public ICommand OpenAnnouncements {  get; }
        public ICommand OpenSettingsWindow { get; }
        public ICommand AddFavoriteSchedule { get; }
        public ICommand OpenNotesWindow { get; }
        public ICommand DeleteFavoriteSchedule { get; }
        public ICommand LoadFavoriteSchedule { get; }
        public Cursor ScheduleNameCursor { get; }
        public int SelectedTab { get; }
        public bool NotesExistenceVisibility { get; }
        public bool AnnouncementsExistenceVisibility { get; }
    }
}
