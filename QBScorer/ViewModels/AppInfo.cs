using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QBScorer
{
    public class AppInfo : ViewModelBase
    {

        //what the fuck is a mvvm

        private ObservableCollection<Config> _configList;
        private ScoreboardProperties _scoreboardProperties;
        private double _rowHeight;
        
        public ObservableCollection<Config> ConfigList 
        {
            get => _configList;
            set => SetProperty(ref _configList, value);
        }

        public ScoreboardProperties ScoreboardProperties
        {
            get => _scoreboardProperties;
            set => SetProperty(ref _scoreboardProperties, value);
        }

        public double RowHeight
        {
            get => _rowHeight;
            set => SetProperty(ref _rowHeight, value);
        }

        

    }
}
