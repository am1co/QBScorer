using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace QBScorer
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ConfigWindow : Window
    {
        public DynamicConfig Config = new DynamicConfig();
        private MainWindow MainWindow;
        /*
        public ObservableCollection<DynamicTeam> Teams = new ObservableCollection<DynamicTeam>();
        public ObservableCollection<DynamicRound> Rounds = new ObservableCollection<DynamicRound>;*/

        public ConfigWindow(MainWindow mainWindow)
        {
            this.Config = new DynamicConfig();
            this.Config.Title = "title test";
            this.Config.Teams = new ObservableCollection<DynamicTeam>();
            this.Config.Rounds = new ObservableCollection<DynamicRound>();

            //add blank team
            DynamicTeam t = new DynamicTeam();
            t.TeamName = "team name test";
            t.TeamID = "T01";
            t.Score = "0";
            this.Config.Teams.Add(t);

            //add blank round
            DynamicRound r = new DynamicRound();
            r.RoundName = "round name test";
            r.Questions = 0;
            r.Points = 0;
            r.RoundID = "R01";
            this.Config.Rounds.Add(r);

            this.DataContext = this.Config;
            this.MainWindow = mainWindow;
            InitializeComponent();
            
        }
        public ConfigWindow(MainWindow mainWindow, Config config) 
        {
            this.Config = new DynamicConfig();
            this.Config.Title = config.Title;
            this.Config.Teams = new ObservableCollection<DynamicTeam>();
            this.Config.Rounds = new ObservableCollection<DynamicRound>();
            foreach (var teams in config.Teams)
            {
                DynamicTeam t = new DynamicTeam();
                t.TeamID = teams.TeamID;
                t.Score = teams.Score;
                t.TeamName = teams.TeamName;

                this.Config.Teams.Add(t);
            }
            foreach (var rounds in config.Rounds)
            {
                DynamicRound r = new DynamicRound();
                r.RoundID = rounds.RoundID;
                r.RoundName = rounds.RoundName;
                r.Points = rounds.Points;
                r.Questions += rounds.Questions;

                this.Config.Rounds.Add(r);
            }
            this.DataContext = this.Config;
            this.MainWindow = mainWindow;
            //turn the config into the dynamic config (later)
            InitializeComponent();
        }

        private void AddTeam (object sender, RoutedEventArgs e)
        {
            DynamicTeam t = new DynamicTeam();
            t.TeamName = "";
            if (this.Config.Teams.Count < 10)
            {
                t.TeamID = "T0" + Convert.ToString(this.Config.Teams.Count+1);
            } else
            {
                t.TeamID = "T" + Convert.ToString(this.Config.Teams.Count+1);
            }
            t.Score = "0";
            this.Config.Teams.Add(t);
        }

        private void DeleteTeam(object sender, RoutedEventArgs e)
        {
            var teamid = (e.Source as Button).Tag;

            foreach(var t in this.Config.Teams)
            {
                if (t.TeamID == teamid)
                {
                    this.Config.Teams.Remove(t);
                    break;
                }
            }

            //reset ids
            for(int i = 0;  i < this.Config.Teams.Count; i++)
            {
                if (i+1 < 10)
                {
                    this.Config.Teams[i].TeamID = "T0" + Convert.ToString(i + 1);
                }
                else
                {
                    this.Config.Teams[i].TeamID = "T" + Convert.ToString(i + 1);
                }
                
            }

        }

        private void AddRound(object sender, RoutedEventArgs e)
        {
            DynamicRound r = new DynamicRound();
            r.RoundName = "";
            r.Questions = 0;
            r.Points = 0;
            if (this.Config.Rounds.Count < 10)
            {
                r.RoundID = "R0" + Convert.ToString(this.Config.Rounds.Count+1);
            }
            else
            {
                r.RoundID = "R" + Convert.ToString(this.Config.Rounds.Count+1);
            }
            this.Config.Rounds.Add (r);
        }

        private void DeleteRound(object sender, RoutedEventArgs e)
        {
            var roundid = (e.Source as Button).Tag;

            foreach(var r in this.Config.Rounds)
            {
                if (r.RoundID == roundid)
                {
                    this.Config.Rounds.Remove(r);
                    break;
                }
            }

            //reset ids
            for(int i = 0;  i < this.Config.Rounds.Count; i++)
            {
                if (i+1 < 10)
                {
                    this.Config.Rounds[i].RoundID = "R0" + Convert.ToString(i + 1);
                }
                else
                {
                    this.Config.Rounds[i].RoundID = "R" + Convert.ToString(i + 1);
                }
                
            }
        }

        private void SaveConfig(object sender, RoutedEventArgs e)
        {
            //string JsonConfig = JsonConvert.SerializeObject(this.Config);

            // serialize JSON directly to a file
            string filename = string.Join("_", this.Config.Title.Split(Path.GetInvalidFileNameChars()));
            using (StreamWriter file = File.CreateText(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName + "\\SavedConfigs\\"+ filename+".json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, this.Config);
            }
            //this is gonna bug out if two configs have the same titles

            this.MainWindow.UpdateConfigFiles();
            //this.MainWindow.SetScoreboardProperties(this.Config);

            this.Close();
        }

    }
}
