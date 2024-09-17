using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QBScorer
{
    public class RoundProperties : ViewModelBase
    {
        private string _RoundName;
        private int _QuestionsCount;
        private int _Points; //might not need this(?)
        //private ObservableCollection<Question> _Questions;
        private double _ColumnWidth; //not sure if ill use this
        private ObservableCollection<TeamRow> _TeamRows;

        public string RoundName
        {
            get => _RoundName;
            set => SetProperty(ref _RoundName, value);
        }
        public int QuestionsCount
        {
            get => _QuestionsCount;
            set => SetProperty(ref _QuestionsCount, value);
        }
        public int Points
        {
            get => _Points;
            set => SetProperty(ref _Points, value);
        }
        public double ColumnWidth
        {
            get => _ColumnWidth;
            set => SetProperty<double>(ref _ColumnWidth, value);
        }
        public ObservableCollection<TeamRow> TeamRows
        {
            get => _TeamRows;
            set => SetProperty(ref _TeamRows, value);
        }
        /*
        public ObservableCollection<Question> Questions
        {
            get => _Questions;
            set => SetProperty(ref _Questions, value);
        }*/

    }
    public class RoundClass : ViewModelBase
    {
        private RoundProperties _roundProperties;
        public RoundProperties RoundProperties
        {
            get => _roundProperties;
            set => SetProperty(ref _roundProperties, value);
        }

    }

}
