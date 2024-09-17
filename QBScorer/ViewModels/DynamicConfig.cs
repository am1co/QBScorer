using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QBScorer
{
    public class DynamicConfig : ViewModelBase
    {
        private string _Title;
        private ObservableCollection<DynamicTeam> _Teams;
        private ObservableCollection<DynamicRound> _Rounds;

        public string Title
        {
            get => _Title;
            set => SetProperty(ref _Title, value);
        }
        public ObservableCollection<DynamicTeam> Teams
        {
            get => _Teams;
            set => SetProperty(ref _Teams, value);
        }
        public ObservableCollection<DynamicRound> Rounds
        {
            get => _Rounds;
            set => SetProperty(ref _Rounds, value);
        }

    }
    public class DynamicRound : ViewModelBase
    {
        private string _RoundName;
        private int _Questions;
        private int _Points;
        private string _RoundID;
        public string RoundName
        {
            get => _RoundName;
            set => SetProperty(ref _RoundName, value);
        }
        public int Questions
        {
            get => _Questions;
            set => SetProperty(ref _Questions, value);
        }
        public int Points
        {
            get => _Points;
            set => SetProperty(ref _Points, value);
        }
        public string RoundID
        {
            get => _RoundID;
            set => SetProperty(ref _RoundID, value);
        }
    }

    public class DynamicTeam : ViewModelBase
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
}
