using System;
using System.IO;

namespace MohawkTerminalGame
{
    public class TerminalGame
    {
        // Place your variables here
        Music bgm;
        Sound sfx1;
        Sound sfx2;
        Sound sfx3;

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

            bgm = Audio.LoadMusic("assets/audio/country.mp3");
            bgm.Looping = true;
            Audio.Play(bgm);
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
            //Page 1
            Terminal.WriteLine("My name is Miles Lambert, a private eye within Cross City.");
            Terminal.WriteLine("Recently I have been hired to solve a murder.");
            Terminal.WriteLine("");
            Terminal.ReadLine();
            Terminal.Beep();
            Terminal.Clear();
            //Page 2
            Terminal.RoboTypeIntervalMilliseconds = 40;
            Terminal.WriteLine("The victim, a young man named Casey Wentz, was murdered last night at a local general store, Large Grizzly General Store.");
            Terminal.WriteLine("Reports say the kid was just buying some snacks after a long shift…");
            Terminal.WriteLine("");
            Terminal.Beep();
            Terminal.ReadLine();
            Terminal.Clear();
            //Page 3 
            Terminal.WriteLine("Casey’s body was brought to the morgue early this morning, from what the coroner has said, the death was pretty grizzley…");
            //Page 4
            Terminal.WriteLine("The attacker disappeared right after the attack, I need to find the perpetrator… and fast…");
            Terminal.WriteLine("");
            Terminal.WriteLine("The longer he’s out there the more dangerous the streets grow…");
            Terminal.WriteLine("");
            Terminal.WriteLine("But… something about this all feels off…");
            Terminal.WriteLine("");
            Terminal.WriteLine("I have _ days to solve this case, and so many places to go…");
            Terminal.WriteLine("");
            Terminal.WriteLine("I gotta be smart on where to go… Can’t let this guy get away…");
            Terminal.ReadLine();
            Terminal.WriteLine("Where to go?");
            Terminal.WriteLine("Hospital or Morgue or General Store");
            Terminal.WriteLine("");
            Terminal.WriteLine("Type H for Hospital or M for Morgue or G");
            Terminal.ReadLine();
            string answer = Terminal.ReadAndClearLine();
            if (answer.ToLower().Equals("h") || answer.ToUpper().Equals("h")) 
            {
                
            }
            if (answer.ToLower().Equals("m") || answer.ToUpper().Equals("M"))
            {

            }
            if (answer.ToLower().Equals("g") || answer.ToUpper().Equals("G"))
            {

            }





        }

    }
}
