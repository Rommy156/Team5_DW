using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;

namespace MohawkTerminalGame
{
    public class TerminalGame
    {
        // Place your variables here
        Music bgm;
        Sound sfx1;
        Sound sfx2;
        Sound sfx3;
        Sound clickSfx;

        HashSet<string> visited = new HashSet<string>();
        private bool IntroPlayed = false;
        private bool gameOver = false;
        /// Run once before Execute begins
        public void Setup()
        {

            // Program configuration
            Program.TerminalExecuteMode = TerminalExecuteMode.ExecuteLoop;
            Program.TerminalInputMode = TerminalInputMode.KeyboardReadAndReadLine;
            //set title
            Terminal.SetTitle("Detective");
           
            // Hide raylib console output
            Terminal.BackgroundColor = ConsoleColor.Black;
            Terminal.CursorVisible = false;

            Audio.Initialize();
            // Load audio files. This must happen AFTER initializing audio.
            // If you are so inclined, you can move Audio.Initialize() into Program beside Input.InitInputThread()
            Terminal.RoboTypeIntervalMilliseconds = 50; // robo type interval 50 milliseconds
            Terminal.UseRoboType = true; // slow typing
            Terminal.WriteWithWordBreaks = true; // donbreak around wors, don't cut them off
            Terminal.WordBreakCharacter = ' '; // break on spaces
            //play music and sfx
            bgm = Audio.LoadMusic("assets/audio/country.mp3");
            bgm.Looping = true;
            Audio.Play(bgm);
            clickSfx = Audio.LoadSound("assets/audio/click.wav");
            Terminal.Clear();

            // Move curosr to overwrite previously drawn (black) text
            Terminal.SetCursorPosition(0, 0);
            Terminal.ResetColor();
            Terminal.CursorVisible = true;
        }

        // Execute() runs based on Program.TerminalExecuteMode (assign to it in Setup).
        //  ExecuteOnce: runs only once. Once Execute() is done, program closes.
        //  ExecuteLoop: runs in infinite loop. Next iteration starts at the top of Execute().
        //  ExecuteTime: runs at timed intervals (eg. "FPS"). Code tries to run at Program.TargetFPS.
        //               Code must finish within the alloted time frame for this to work well.
        public void Execute()
        {
            PlayIntro();
            FirstLocation();
            
        }
        private void PlayIntro()

        {
            Terminal.Clear();
            //Page 1
            Terminal.WriteLine("My name is Miles Lambert, a private eye within Cross City.");
            Terminal.WriteLine("Recently I have been hired to solve a murder.");
            Terminal.WriteLine("");
            Terminal.ReadLine();
            Audio.Play(clickSfx);
            Terminal.Clear();
            //-----------ascii art
            //Page 2
            Terminal.RoboTypeIntervalMilliseconds = 40;
            Terminal.WriteLine("The victim, a young man named Casey Wentz, was murdered last night at a local general store, Large Grizzly General Store.");
            Terminal.WriteLine("Reports say the kid was just buying some snacks after a long shift…");
            Terminal.WriteLine("");
            Terminal.ReadLine();
            Terminal.Clear();
            //----------ascii art
            //Page 3 
            Terminal.WriteLine("Casey’s body was brought to the morgue early this morning, from what the coroner has said, the death was pretty grizzley…");
            Terminal.ReadLine();
            Terminal.Clear();
            //-------ascii art

            //Page 4
            Terminal.WriteLine("The attacker disappeared right after the attack, I need to find the perpetrator… and fast…");
            Terminal.WriteLine("");
            Terminal.WriteLine("The longer he’s out there the more dangerous the streets grow…");
            Terminal.WriteLine("");
            Terminal.WriteLine("But… something about this all feels off…");
            Terminal.WriteLine("");
            Terminal.WriteLine("I have x days to solve this case, and so many places to go…");
            Terminal.WriteLine("");
            Terminal.WriteLine("I gotta be smart on where to go… Can’t let this guy get away…");
            Terminal.ReadLine();
            //--------ascii art
        }
        private void FirstLocation()
        {
            //new game menu
            Terminal.Clear();
            Terminal.RoboTypeIntervalMilliseconds = 30;
            //Page 4.5
            Terminal.WriteLine("");
            Terminal.WriteLine("Where to go?");
            Terminal.WriteLine("Hospital or Morgue");
            Terminal.WriteLine("");
            Terminal.WriteLine("[H] Hospital");
            Terminal.WriteLine("[M] for Morgue");
            Terminal.ReadLine();

            string answer = Terminal.ReadAndClearLine();

            if (answer.Equals("h", StringComparison.OrdinalIgnoreCase))
            {
                VisitHospital();
            }
            else if (answer.Equals("m", StringComparison.OrdinalIgnoreCase))
            {
                VisitMorgue();
            }
            else
            {
                Terminal.WriteLine("Invalid choice.");
                Terminal.ReadLine();
                FirstLocation();
            }
        }

        private void ChooseNextLocation()
        {

        }


        private void VisitHospital()
        {
            Terminal.WriteLine("Silverstein Hospital… That's where Casey Wentz was brought, it’s a shame the kid died the way he did…");
            Terminal.WriteLine("The hospital halls are cold and detached, yet perhaps coming here wasn’t such a bad idea…");
            Terminal.WriteLine("Ran into the kid’s family at the hospital… The mom, she was pretty frazzled… but she mentioned something interesting, a recent fight... ");
            Terminal.WriteLine("Apparently the kid had been pretty on edge… said some pretty venomous things to his own mother…");
            Terminal.WriteLine("The fight was apparently pretty awful, Mom tried asking him what was going on but just started screaming 'bout owning money… this led mom to start screamin’ too…");
            Terminal.WriteLine("Families fight for sure… sometimes it gets ugly… but is it enough to justify murder…?");
            Terminal.WriteLine("I’m not sure…");
            Terminal.WriteLine("Options to go:");
        }
        private void VisitMorgue()
        {
            Terminal.WriteLine("");
            Terminal.WriteLine("");
            Terminal.WriteLine("");
            Terminal.WriteLine("");
            Terminal.WriteLine("");
        }
        private void VisitGeneralStore()
        {
            Terminal.WriteLine("");
            Terminal.WriteLine("");
            Terminal.WriteLine("");
            Terminal.WriteLine("");
            Terminal.WriteLine("");
        }
        private void VisitBank()
        {
            Terminal.WriteLine("");
            Terminal.WriteLine("");
            Terminal.WriteLine("");
            Terminal.WriteLine("");
            Terminal.WriteLine("");
        }
        private void VisitSchool()
        {
            Terminal.WriteLine("");
            Terminal.WriteLine("");
            Terminal.WriteLine("");
            Terminal.WriteLine("");
            Terminal.WriteLine("");
        }
        private void VisitCourtHouse()
        {
            Terminal.WriteLine("");
            Terminal.WriteLine("");
            Terminal.WriteLine("");
            Terminal.WriteLine("");
            Terminal.WriteLine("");
        }
        private void VisitBar()
        {
            Terminal.WriteLine("");
            Terminal.WriteLine("");
            Terminal.WriteLine("");
            Terminal.WriteLine("");
            Terminal.WriteLine("");
        }
        private void VisitManor()
        {
            Terminal.WriteLine("");
            Terminal.WriteLine("");
            Terminal.WriteLine("");
            Terminal.WriteLine("");
            Terminal.WriteLine("");
        }
        private void VisitOffice()
        {
            Terminal.WriteLine("");
            Terminal.WriteLine("");
            Terminal.WriteLine("");
            Terminal.WriteLine("");
            Terminal.WriteLine("");
        }
        
    }
}