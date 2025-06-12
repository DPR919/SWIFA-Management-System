using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWIFA_Management_System
{
    class FencerPoolResult
    {
        public int SquadSeed { get; set; }
        public string SquadName { get; set; }
        public string FencerStrip { get; set; }
        public string FencerName { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public double WinPct { get; set; }
        public int TouchesScored { get; set; }
        public int TouchesReceived { get; set; }
        public int Indicator => TouchesScored - TouchesReceived;
    }
}
