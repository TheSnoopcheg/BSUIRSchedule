using BSUIRSchedule.Classes;
using BSUIRSchedule.Models;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BSUIRSchedule.ViewModels
{
    public class NoteViewModel : ViewModelBase, INoteViewModel
    {
        public event Action NotesChanged;
        readonly INoteModel _model;
        public NoteViewModel(INoteModel noteModel)
        {
            _model = noteModel;
            _model.NotesChanged += () => NotesChanged.Invoke();
        }

        public async Task<bool> SetNotes(string? title, string? url)
        {
            Title = title!;
            if (int.TryParse(title, out int id))
                IsEmployeeNote = false;
            else
                IsEmployeeNote = true;
            return await _model.LoadNotesAsync(url);
        }

        public bool IsNotesEmpty { get => Notes.Count == 0 ? true : false; }

        #region Commands

        private ICommand? addNote;
        public ICommand AddNote
        {
            get
            {
                return addNote ??
                    (addNote = new RelayCommand(obj =>
                    {
                        _model.AddNote(new Note(NoteDate, NoteText));
                        NoteDate = null;
                        NoteText = string.Empty;
                    }));
            }
        }
        private ICommand? editNote;
        public ICommand EditNote
        {
            get
            {
                return editNote ??
                    (editNote = new RelayCommand(obj =>
                    {
                        if (!IsEditing)
                        {
                            IsEditing = true;
                            NoteText = SelectedNote.Content;
                            NoteDate = SelectedNote.Date;
                        }
                        else
                        {
                            _model.EditNote(SelectedNote, new Note(NoteDate, NoteText));
                            IsEditing = false;
                            NoteDate = null;
                            NoteText = string.Empty;
                        }
                    }));
            }
        }
        private ICommand? deleteNote;
        public ICommand DeleteNote
        {
            get
            {
                return deleteNote ??
                    (deleteNote = new RelayCommand(obj =>
                    {
                        if(obj is Note v)
                        {
                            _model.RemoveNote(v);
                            if (IsEditing)
                            {
                                IsEditing = false;
                                NoteDate = null;
                                NoteText = string.Empty;
                            }
                        }
                    }));
            }
        }

        #endregion
        private string? _noteText;
        public string? NoteText
        {
            get => _noteText;
            set => this.RaiseAndSetIfChanged(ref _noteText, value);
        }
        private DateTime? _noteDate;
        public DateTime? NoteDate
        {
            get => _noteDate;
            set => this.RaiseAndSetIfChanged(ref _noteDate, value);
        }
        public ObservableCollection<Note> Notes => _model.Notes;
        private bool _isEditing;
        public bool IsEditing
        {
            get => _isEditing;
            set => this.RaiseAndSetIfChanged(ref _isEditing, value);
        }
        private Note _selectedNote;
        public Note SelectedNote
        {
            get => _selectedNote;
            set => this.RaiseAndSetIfChanged(ref _selectedNote, value);
        }
        public bool IsEmployeeNote { get; private set; }
        public string Title { get; private set; }
    }
}