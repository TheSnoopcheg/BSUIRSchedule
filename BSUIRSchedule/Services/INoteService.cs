using BSUIRSchedule.Classes;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace BSUIRSchedule.Services
{
    public interface INoteService
    {
        Task<ObservableCollection<Note>> LoadNotesAsync(string? path);
        Task SaveNotesAsync(ObservableCollection<Note> notes, string? path);
    }
}
