using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QBScorer
{
    public class ScoreboardProperties : ViewModelBase
    {
        private int _totalRows;
        private int _totalCols;
        private ObservableCollection<TeamRowSummary> _TeamRowSummaries;
        private ObservableCollection<RoundClass> _Rounds;


        public int TotalRows 
        { 
            get => _totalRows; 
            set => SetProperty(ref _totalRows, value); 
        }
        public int TotalCols
        {
            get => _totalCols;
            set => SetProperty(ref _totalCols, value);
        }
        
        public ObservableCollection<TeamRowSummary> TeamRowSummaries
        {
            get => _TeamRowSummaries;
            set => SetProperty(ref _TeamRowSummaries, value);
        }
        public ObservableCollection<RoundClass> Rounds
        {
            get => _Rounds;
            set => SetProperty(ref _Rounds, value);
        }
    }


    

   
}
