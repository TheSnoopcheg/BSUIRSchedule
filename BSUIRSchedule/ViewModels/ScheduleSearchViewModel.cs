using BSUIRSchedule.Classes;
using BSUIRSchedule.Models;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace BSUIRSchedule.ViewModels
{
    public class ScheduleSearchViewModel : ViewModelBase, IScheduleSearchViewModel
    {
        private readonly IScheduleSearchModel _model;
        public SearchResponse? SearchResponse
        {
            get { return _model.SearchResponse; }
            set {  _model.SearchResponse = value; }
        }
        public ObservableCollection<SearchResponse> Responses => _model.Responses;
        public string Input
        {
            get { return _model.Input; }
            set {  _model.Input = value; }
        }
        public ScheduleSearchViewModel(IScheduleSearchModel scheduleSearchModel)
        {
            _model = scheduleSearchModel;
            scheduleSearchModel.ResponsesChanged += () => { this.RaisePropertyChanged(nameof(Responses)); };
        }
        public void ClearInput()
        {
            Input = string.Empty;
            SearchResponse = null;
        }
    }
}
