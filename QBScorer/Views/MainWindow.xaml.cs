using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;
using System.Reflection;
using System.Reflection.Metadata;
using System.Windows.Media.Animation;
using System.DirectoryServices.ActiveDirectory;
using System.Collections.ObjectModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Windows.Media.Media3D;
using System.Diagnostics.Tracing;
using System.Diagnostics;
using System.Drawing;
using System;
using System.Globalization;
using System.Windows.Controls.Primitives;

namespace QBScorer
{

    // i would like to leave a testimonial in the small chance that someone ever sees this:
    // this code is awful
    // it does not use the mvvm pattern
    // or data binding
    // which youre supposed to do for wpf programs
    // but that shit was so confusing that i genuinely just gave up and built the layout in the code-behind

    //will attempt to migrate to mvvm before adding other features
    //could use this: https://rachel53461.wordpress.com/2011/09/17/wpf-grids-rowcolumn-count-properties/

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Config current_config = new Config();
        private ObservableCollection<Config> config_list = new ObservableCollection<Config>(); //stores titles of each config
        public static RoutedCommand SelectConfig = new RoutedCommand();
        public static RoutedCommand CheckQuestion = new RoutedCommand();
        public string ConfigDirectory;
        public AppInfo appInfo { get; set; }
        //public ScoreboardProperties appInfo.ScoreboardProperties { get; set; }
        public MainWindow()
        {

            this.appInfo = new AppInfo();
            this.appInfo.ScoreboardProperties = new ScoreboardProperties();
            this.ConfigDirectory = "C:\\ProgramData\\QBScorer\\SavedConfigs";
            Directory.CreateDirectory(this.ConfigDirectory);
            //Debug.WriteLine(this.ConfigDirectory);

            this.UpdateConfigFiles();

            this.appInfo.ConfigList = this.config_list;



            foreach (Config config in this.appInfo.ConfigList)
            {
                if (config.Title == "default")
                {
                    this.current_config = config;
                    SetScoreboardProperties(config);
                    //GenerateScoreboardProperties(this.current_config);
                }
            }

            this.DataContext = this.appInfo;

          


            InitializeComponent();
            this.KeyDown += new KeyEventHandler(MainWindow_KeyDown);

            var scoreboardElement = (Grid)this.FindName("Scoreboard");
            if (scoreboardElement is Grid)
            {
                double Height = ((scoreboardElement.ActualHeight - (SystemParameters.ScrollHeight + this.appInfo.ScoreboardProperties.TotalRows)) / this.appInfo.ScoreboardProperties.TotalRows);
                if (Height > 0)
                {
                    this.appInfo.RowHeight = Height;

                }
            }

            //this.CreateScoreGrid(this.current_config);

            //Debug.WriteLine("Testing");

        }
        void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.WindowStyle == WindowStyle.None)
            {
                if (e.Key == Key.Escape)
                {
                    // Exit fullscreen
                    this.ResizeMode = ResizeMode.CanResize;
                    this.WindowStyle = WindowStyle.SingleBorderWindow;
                    //this.WindowState = WindowState.Normal;
                    var MenuBar = (Menu)this.FindName("MenuBar");
                    if (MenuBar is Menu)
                    {
                        MenuBar.Visibility = Visibility.Visible;
                    }

                }

            }
            
        }

        public void UpdateConfigFiles()
        {
            try
            {
                this.config_list = new ObservableCollection<Config>();
                //int i = 0;
                foreach (string config_file in Directory.EnumerateFiles(this.ConfigDirectory, "*.json"))
                {
                    
                    string json_config = System.IO.File.ReadAllText(config_file);
                    Config temp_config = JsonConvert.DeserializeObject<Config>(json_config);

                    //overwrites configs with the same titles
                    var q = this.config_list.IndexOf(this.config_list.Where(X => X.Title == temp_config.Title).FirstOrDefault());
                    if (q > -1)
                    {
                        this.config_list[q] = temp_config;
                    }
                    else
                    {
                        this.config_list.Add(temp_config);
                    }
                    this.appInfo.ConfigList = this.config_list;
                    //this.SetScoreboardProperties(this.current_config);
                    //Debug.WriteLine(temp_config.Title);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error in getting config files: " + e.Message);
            }
        }

        protected void OnWindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            /*
            double newWindowHeight = e.NewSize.Height;
            double newWindowWidth = e.NewSize.Width;
            double prevWindowHeight = e.PreviousSize.Height;
            double prevWindowWidth = e.PreviousSize.Width;
            */
            //this.appInfo.RowHeight = newWindowHeight;
            var scoreboardElement = (Grid)this.FindName("Scoreboard");
            if (scoreboardElement is Grid)
            {
                double Height = ((scoreboardElement.ActualHeight - (SystemParameters.ScrollHeight + this.appInfo.ScoreboardProperties.TotalRows)) / this.appInfo.ScoreboardProperties.TotalRows);
                if (Height > 0)
                {
                    this.appInfo.RowHeight = Height;

                }
            }

        }

        private void ExecutedCheckQuestion(object sender, ExecutedRoutedEventArgs e)
        {
            //need to figure out how to differentiate checked boxes
            //Debug.WriteLine("");
            //Debug.WriteLine("Question Checked.");
            var cmdParam = e.Parameter as CheckboxProperties;
            if (cmdParam != null)
            {
                //Debug.WriteLine("Checked? " + cmdParam.IsChecked);
                //Debug.WriteLine("Team:" + cmdParam.TeamID);
                //Debug.WriteLine("Round:" + cmdParam.RoundName);

                foreach (var round in this.appInfo.ScoreboardProperties.Rounds)
                {
                    if (cmdParam.RoundName == round.RoundProperties.RoundName) 
                    {
                        int i = 0;
                        foreach(var team in round.RoundProperties.TeamRows)
                        {
                            if(cmdParam.TeamID == team.TeamID)
                            {
                                int score = Convert.ToInt32(this.appInfo.ScoreboardProperties.TeamRowSummaries[i].Score);
                                if (Convert.ToBoolean(cmdParam.IsChecked))
                                {
                                    score -= cmdParam.Points;
                                    this.appInfo.ScoreboardProperties.Rounds[cmdParam.rIndex].RoundProperties.TeamRows[i].Questions[cmdParam.qIndex].CheckboxProperties.IsChecked = false;
                                    //this.appInfo.ScoreboardProperties.TeamRows[i].Rounds[cmdParam.rIndex].RoundProperties.Questions[cmdParam.qIndex].CheckboxProperties.IsChecked = false;

                                }
                                else
                                {
                                    score += cmdParam.Points;
                                    this.appInfo.ScoreboardProperties.Rounds[cmdParam.rIndex].RoundProperties.TeamRows[i].Questions[cmdParam.qIndex].CheckboxProperties.IsChecked = true;
                                    //this.appInfo.ScoreboardProperties.TeamRows[i].Rounds[cmdParam.rIndex].RoundProperties.Questions[cmdParam.qIndex].CheckboxProperties.IsChecked = true;
                                }
                                this.appInfo.ScoreboardProperties.TeamRowSummaries[i].Score = Convert.ToString(score);
                                //Debug.WriteLine("Points: " + this.appInfo.ScoreboardProperties.TeamRowSummaries[i].Score);

                                break;
                            }
                            i++;
                        }
                    }

                }

            }

        }

        private void CanExecuteCheckQuestion(object sender, CanExecuteRoutedEventArgs e)
        {
            Control target = e.Source as Control;

            //Debug.WriteLine("Command Not YetExecuted.");

            if (target != null)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }

        private void ExecutedSelectConfig(object sender, ExecutedRoutedEventArgs e)
        {
            //Debug.WriteLine("Command Executed.");
            var cmdParam = e.Parameter as String;
            if (cmdParam != null)
            {
                
                foreach (Config config in this.appInfo.ConfigList)
                {
                    if (config.Title == cmdParam)
                    {
                        this.current_config = config;
                        this.SetScoreboardProperties(this.current_config);
                        //Debug.WriteLine ("Number of temas:" + this.appInfo.ScoreboardProperties.TeamRowSummaries.Count);
                        var scoreboardElement = (Grid)this.FindName("Scoreboard");
                        if (scoreboardElement is Grid)
                        {
                            double Height = ((scoreboardElement.ActualHeight - (SystemParameters.ScrollHeight + this.appInfo.ScoreboardProperties.TotalRows)) / this.appInfo.ScoreboardProperties.TotalRows);
                            if (Height > 0)
                            {
                                this.appInfo.RowHeight = Height;
                            }
                        }
                    }
                }
            }
        }

        private void CanExecuteSelectConfig(object sender, CanExecuteRoutedEventArgs e)
        {
            Control target = e.Source as Control;

            //Debug.WriteLine("Command Not YetExecuted.");

            if (target != null)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }

        public void SetScoreboardProperties(Config config)
        {
            this.appInfo.ScoreboardProperties = new ScoreboardProperties();

            var TotalQuestions = 0;
            foreach (var round in config.Rounds)
            {
                TotalQuestions += round.Questions;
            }
            this.appInfo.ScoreboardProperties.TotalRows = config.Teams.Count + 1;
            this.appInfo.ScoreboardProperties.TotalCols = TotalQuestions + 2;
            this.appInfo.ScoreboardProperties.Rounds = new ObservableCollection<RoundClass>();
            this.appInfo.ScoreboardProperties.TeamRowSummaries = new ObservableCollection<TeamRowSummary>();
            int rIndex = 0;
            foreach(var team in config.Teams)
            {
                TeamRowSummary teamRowSummary = new TeamRowSummary();
                teamRowSummary.TeamID = team.TeamID;
                teamRowSummary.TeamName = team.TeamName;
                teamRowSummary.Score = "0";
                this.appInfo.ScoreboardProperties.TeamRowSummaries.Add(teamRowSummary);
            }

            foreach (var round in config.Rounds)
            {
                //set basic round properties
                RoundClass roundClass = new RoundClass();
                RoundProperties roundProperties = new RoundProperties();
                roundProperties.RoundName = round.RoundName;
                roundProperties.QuestionsCount = round.Questions;
                roundProperties.Points = round.Points;

                roundProperties.TeamRows = new ObservableCollection<TeamRow>();
                
                //set the teams in every round
                foreach (var team in config.Teams)
                {
                    TeamRow teamRow = new TeamRow();
                    teamRow.TeamName = team.TeamName;
                    teamRow.TeamID = team.TeamID;
                    //teamRow.Score = "0";//might chaneg this in the future
                    //teamRow.ScoreID = team.TeamID + "_score";

                    //set questions per team per round
                    teamRow.Questions = new ObservableCollection<Question>();
                    
                    for (int i = 0; i< round.Questions; i++)
                    {
                        Question question = new Question();
                        CheckboxProperties questionProp = new CheckboxProperties();
                        questionProp.Team = team.TeamName;
                        questionProp.TeamID = team.TeamID;
                        questionProp.RoundName = round.RoundName;
                        questionProp.Points = round.Points;
                        questionProp.IsChecked = false;
                        questionProp.CheckBoxName = team.TeamID + "_question" + Convert.ToString(i); //might need to change the indexing of i for this
                        //questionProp.ScoreID = teamRow.ScoreID;
                        questionProp.qIndex = i;
                        questionProp.rIndex = rIndex;

                        question.CheckboxProperties = questionProp;
                        teamRow.Questions.Add(question);
                    }

                    roundProperties.TeamRows.Add(teamRow);
                }
                rIndex++;

                roundClass.RoundProperties = roundProperties;
                this.appInfo.ScoreboardProperties.Rounds.Add(roundClass);
            }

            //this.appInfo.ScoreboardProperties = this.appInfo.ScoreboardProperties;

        }
        
        private void NewButtonClick(object sender, RoutedEventArgs e)
        {
            ConfigWindow configWindow = new ConfigWindow(this);
            configWindow.Owner = this;
            configWindow.Show();
        }

        private void EditButtonClick(object sender, RoutedEventArgs e)
        {
            ConfigWindow configWindow = new ConfigWindow(this, this.current_config);
            configWindow.Owner = this;
            configWindow.Show();
        }

        private void FullScreenButtonClick(object sender, RoutedEventArgs e)
        {
            if (this.WindowStyle == WindowStyle.None)
            {
                // Exit fullscreen
                this.ResizeMode = ResizeMode.CanResize;
                this.WindowStyle = WindowStyle.SingleBorderWindow;
                //this.WindowState = WindowState.Normal;
                var MenuBar = (Menu)this.FindName("MenuBar");
                if (MenuBar is Menu)
                {
                    MenuBar.Visibility = Visibility.Visible;
                }
            }
            else
            {
                // Enter fullscreen
                this.ResizeMode = ResizeMode.NoResize;
                this.WindowStyle = WindowStyle.None;
                this.WindowState = WindowState.Normal;
                this.WindowState = WindowState.Maximized;

                var MenuBar = (Menu)this.FindName("MenuBar");
                if (MenuBar is Menu)
                {
                    MenuBar.Visibility = Visibility.Collapsed;
                }

            }
        }

        private void reset_button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void save_button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void delete_button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}