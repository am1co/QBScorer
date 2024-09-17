using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QBScorer
{
    public class TeamRowSummary : ViewModelBase
    {
        private string _TeamID;
        private string _TeamName;
        private string _Score;
        public string TeamID
        {
            get => _TeamID;
            set => SetProperty(ref _TeamID, value);
        }
        public string TeamName
        {
            get => _TeamName;
            set => SetProperty(ref _TeamName, value);
        }
        public string Score
        {
            get => _Score;
            set => SetProperty(ref _Score, value);
        }

    }
    public class TeamRow : ViewModelBase
    {
        private string _TeamID;
        private string _TeamName;
        private ObservableCollection<Question> _Questions;

        public string TeamID
        {
            get => _TeamID;
            set => SetProperty(ref _TeamID, value);
        }
        public string TeamName
        {
            get => _TeamName;
            set => SetProperty(ref _TeamName, value);
        }
        public ObservableCollection<Question> Questions
        {
            get => _Questions;
            set => SetProperty(ref _Questions, value);
        }

    }
}
