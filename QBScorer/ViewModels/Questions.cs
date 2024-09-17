using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QBScorer
{
    public class Question : ViewModelBase
    {

        private CheckboxProperties _CheckboxProperties;

        public CheckboxProperties CheckboxProperties
        {
            get => _CheckboxProperties;
            set => SetProperty(ref _CheckboxProperties, value);
        }

    }
    public class CheckboxProperties : ViewModelBase
    {
        private string _Team;
        private string _TeamID;
        private string _RoundName;
        private int _Points;
        private bool? _IsChecked;
        private string _CheckBoxName;
        private string _ScoreID;
        private int _qIndex;
        private int _rIndex;

        public string Team
        {
            get => _Team;
            set => SetProperty(ref _Team, value);
        }
        public string TeamID
        {
            get => _TeamID;
            set => SetProperty(ref _TeamID, value);
        }
        public string RoundName
        {
            get => _RoundName;
            set => SetProperty(ref _RoundName, value);
        }
        public int Points
        {
            get => _Points;
            set => SetProperty(ref _Points, value);
        }
        public bool? IsChecked
        {
            get => _IsChecked;
            set => SetProperty(ref _IsChecked, value);
        }
        public string CheckBoxName
        {
            get => _CheckBoxName;
            set => SetProperty(ref _CheckBoxName, value);
        }
        public string ScoreID
        {
            get => _ScoreID;
            set => SetProperty(ref _ScoreID, value);
        }
        public int qIndex
        {
            get => _qIndex;
            set => SetProperty(ref _qIndex, value);
        }
        public int rIndex
        {
            get => _rIndex;
            set => SetProperty(ref _rIndex, value);
        }
    }
}
