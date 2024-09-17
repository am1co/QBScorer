using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QBScorer
{
    // Config myDeserializedClass = JsonConvert.DeserializeObject<Config>(myJsonResponse);
    public class Config
    {
        public string Title { get; set; }
        public List<Team> Teams { get; set; }
        public List<Round> Rounds { get; set; }
    }

    public class Round
    {
        public string RoundName { get; set; }
        public int Questions { get; set; }
        public int Points { get; set; }
        public string RoundID { get; set; }
    }

    public class Team
    {
        public string TeamID { get; set; }
        public string TeamName { get; set; }
        public string Score { get; set; }
    }




}
