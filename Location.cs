using MohawkTerminalGame;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalGameWithAudio
{

    internal class Location
    {
        //each location has a name, description, and paths to other locations
        public string Name { get; set; }
        public string Description { get; set; }
        public string Dialogue { get; set; }

        public string asciiArt { get; set; }

        public string VisitedDescription { get; set; }
        public Dictionary<string, Location> Paths { get; set; } = new Dictionary<string, Location>();
        public int VisitCount { get; set; } = 0;
        //constructor to initialize the paths dictionary 
        public Location()
        {
            Paths = new Dictionary<string, Location>();
            Dialogue = "";
            VisitedDescription = "";
            asciiArt = "";  
        }
        public void onVisit()
        {
            
            VisitCount++;
        }
        
        }

}
