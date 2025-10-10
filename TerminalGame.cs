using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security.Principal;
using TerminalGameWithAudio;

namespace MohawkTerminalGame
{

    public class TerminalGame
    {
        // Sound and audio variables 
        Music bgm;
        Sound clickSfx;
        //Location variables
        Location currentLocation;
        Location hospital;
        Location morgue;
        Location generalStore;
        Location courtHouse;
        Location bank;
        Location school;
        Location bar;
        Location manor;
        Location office;
        Location intro;
        //game state variables
        bool IntroPlayed = false;
        int daysPassed = 0;
        int maxDays = 5;
        bool gameOver = false;
        //location visit tracking
        HashSet<Location> visitedLocations = new HashSet<Location>();

        /// Run once before Execute begins
        public void Setup()
        {
            // Program configuration
            Program.TerminalExecuteMode = TerminalExecuteMode.ExecuteLoop;
            Program.TerminalInputMode = TerminalInputMode.KeyboardReadAndReadLine;
            //set title
            Terminal.SetTitle("Case of the murder arond the corner");
            Terminal.SetWindowSize(1200, 800);

            // Hide raylib console output
            Terminal.BackgroundColor = ConsoleColor.Black;
            Terminal.CursorVisible = false;
            Terminal.RoboTypeIntervalMilliseconds = 50; // robo type interval 50 milliseconds
            Terminal.UseRoboType = true; // slow typing
            Terminal.WriteWithWordBreaks = true; // donbreak around wors, don't cut them off
            Terminal.WordBreakCharacter = ' '; // break on spaces
            Audio.Initialize();
            // Load audio files.
            bgm = Audio.LoadMusic("assets/audio/bgm.mp3");
            bgm.Looping = true;
            Audio.Play(bgm);
            clickSfx = Audio.LoadSound("assets/audio/click.wav");
            Terminal.Clear();

            //set up locations and paths
            SetupLocations();
            currentLocation = intro;
            Terminal.Clear();
            // Move curosr to overwrite previously drawn (black) text
            Terminal.SetCursorPosition(0, 0);
            Terminal.ResetColor();
            Terminal.CursorVisible = true;
        }
        private void SetupLocations()
        {
            //-----------ascii art--------------------[Intro]
            Terminal.UseRoboType = false;
            Terminal.WriteLine();
            Terminal.RoboTypeIntervalMilliseconds = 20;

            intro = new Location
            {
                Name = "Detective’s Office",
                asciiArt = File.ReadAllText("assets/text/Detective-at-desk.txt"),
                Description = "It’s another long night in Cross City. Time to get to work...",
                VisitedDescription = "I’ve already gone through this part. No reason to linger.",
            };

            Terminal.Clear();
            //-----------ascii art--------------------[Hospital]
            Terminal.UseRoboType = false;

            Terminal.WriteLine();
            hospital = new Location
            {
                Name = "Hospital",
                asciiArt = File.ReadAllText("assets/text/Hospital.txt"),
                Description = "Silverstein Hospital",
                VisitedDescription = "I’ve already gone through this part. No reason to linger.",
                Dialogue = @"Silverstein Hospital… That's where Casey Wentz was brought, it’s a shame the kid died the way he did… 
The hospital halls are cold and detached, yet perhaps coming here wasn’t such a bad idea… 

Ran into the kid’s family at the hospital… The mom, she was pretty frazzled… but she mentioned something interesting, a recent fight... 

Apparently the kid had been pretty on edge… said some pretty venomous things to his own mother…

The fight was apparently pretty awful, Mom tried asking him what was going on but just started screaming 'bout owing money… this led mom to start screamin’ too…

Families fight for sure… sometimes it gets ugly… but is it enough to justify murder…?

I’m not sure…

"
            };

            Terminal.Clear();
            //-----------ascii art--------------------[Morgue]
            Terminal.WriteLine();
            //morgue location narrative
            morgue = new Location
            {
                Name = "Morgue",
                asciiArt = File.ReadAllText("assets/text/Morgue.txt"),
                Description = "City Morgue",
                VisitedDescription = "I’ve already gone through this part. No reason to linger.",
                Dialogue = @"Walking into the morgue the stench of death hit me, not literally, 

but… in the way that hangs over your head for the days to come, how these people do this is beyond me…

Regardless, it’s part of the job, and there is a crime to be solved… and a coroner to speak with…

Meeting up with the coroner was like any other meeting, the old man looks far past his days, yet keeps an eerie demeanour… more so today than others…

Regardless, it's what you gotta do in this line of work… though I did spot something in the coroner’s office while he stepped out…

He had a browser open up on his laptop, something 'bout psychiatric help for violent thoughts… not sure if this means anything, but from how he was talkin’ he seemed pretty eager to cut this kid open… kept talkin’ about the glass lacerations across the body…

Fan of his job… or something more malicious…?"


            }; Terminal.RoboTypeIntervalMilliseconds = 20;

            Terminal.Clear();
            //-----------ascii art--------------------[General Store]

            //general store location narrative
            generalStore = new Location
            {
                Name = "General Store",
                asciiArt = File.ReadAllText("assets/text/Corner-Store.txt"),
                Description = "Large Grizzly General Store",
                VisitedDescription = "I’ve already gone through this part. No reason to linger.",
                Dialogue = @"Walking into that corner store was surprisingly rougher than anyone would have suspected,despite the effort, and the cleaning it's clear that something horrible happened here…

Talking with the owner he still seemed shaken by the events, but he was still able to drop some useful information… something 'bout another young man…

There was this other kid, Jason Feltman, he was there the day of the murder, looked like he had fallen on hard times, startin’ to get desperate… 
 
Couple that with the fact that Casey was clearly more well off than him… and desperation makes people do some stupid things…

The owner said he thinks he saw him eyeing Casey’s wallet, but the old man also wasn’t wearin’ his glasses…
"
            };
            Terminal.Clear();
            //-----------ascii art--------------------[Court House]
            Terminal.RoboTypeIntervalMilliseconds = 20;
            //court house location narrative
            courtHouse = new Location
            {
                Name = "Court House",
                asciiArt = File.ReadAllText("assets/text/Courthouse.txt"),
                Description = "City Court House",
                VisitedDescription = "I’ve already gone through this part. No reason to linger.",
                Dialogue = @"Walking into that corner store was surprisingly rougher than anyone would have suspected,despite the effort, and the cleaning it's clear that something horrible happened here…
Talking with the owner he still seemed shaken by the events, but he was still able to drop some useful information… something 'bout another young man…

There was this other kid, Jason Feltman, he was there the day of the murder, looked like he had fallen on hard times, startin’ to get desperate… 

Couple that with the fact that Casey was clearly more well off than him… and desperation makes people do some stupid things…

The owner said he thinks he saw him eyeing Casey’s wallet, but the old man also wasn’t wearin’ his glasses…
"
            };

            Terminal.Clear();
            //-----------ascii art--------------------[Bank]
            Terminal.RoboTypeIntervalMilliseconds = 20;
            //bank location narrative
            bank = new Location
            {
                Name = "Bank",
                asciiArt = File.ReadAllText("assets/text/Bank.txt"),
                Description = "First National Bank",
                VisitedDescription = "I’ve already gone through this part. No reason to linger.",
                Dialogue = @"Walking into the bank with such reasons as I did felt awful… but I was given a tip off that Casey had drawn out a large sum of money…

The bank teller was clearly concerned, maybe a slight bit nervous, I don’t blame her… but she remained helpful regardless… Mentioned something she overheard on her break…

Apparently, while she was on her break, outside having a smoke, she saw Casey talking to some guy, her curiosity got the better of her and she began eavesdropping…

The dude seemed to be Casey’s coworker, and an aggressive one at that… was going off 'bout the money Casey owed…

Angry Coworker? And owed money… seems like a recipe for disaster… but without any corroboration… hmmm… 
"
            };

            Terminal.Clear();
            //-----------ascii art--------------------[School]
            Terminal.RoboTypeIntervalMilliseconds = 0;
            string schoolAscii = File.ReadAllText("assets/text/College.txt");
            Terminal.WriteLine(schoolAscii);
            Terminal.WriteLine();

            //school location narrative
            school = new Location
            {
                Name = "School",
                asciiArt = File.ReadAllText("assets/text/College.txt"),
                Description = "City College",
                VisitedDescription = "I’ve already gone through this part. No reason to linger.",
                Dialogue = @"Figured heading to Casey’s college could have proved some use… I figured, could find a professor, maybe a classmate. Someone who knew the kid…

turns out I was right..

I was able to catch the kid’s girlfriend, or… I suppose, his former girlfriend… she was just leavin’ class.

Talkin’ with his girlfriend opened my eyes on somethings… That kid was really hatin’ work, was talking 'bout quitting…

Apparently his one coworker wasn’t happy 'bout that, they were pretty swamped at the bar as is…

Not sure if that is worth killin’ a person over though…
"

            };


            Terminal.Clear();
            //-----------ascii art--------------------[Bar]

            //bar location narrative
            bar = new Location
            {
                Name = "Seedy Bar",
                asciiArt = File.ReadAllText("assets/text/Bar.txt"),
                Description = "Seedy Bar",
                VisitedDescription = "I’ve already gone through this part. No reason to linger.",
                Dialogue = @" Coheed’s, a dingy lil bar… this is where Casey worked… Walkin’ in things instantly felt off, people like me aren’t typically welcomed here…

Regardless, I got a job to do… And the kid’s old coworkers could be a great place for info.

Askin’ the bartender ‘bout Casey seemed to sour his face. I asked him more ‘bout the kid and he seemed to not have much good things to say ‘bout him…

He began goin’ on a bit of a tangent, but stopped himself, likely after realizin’ they were talkin’ to a cop…

Regardless… He said a lot of things that raised some suspicions… 
"
            };

            Terminal.Clear();
            //-----------ascii art--------------------[Manor]

            //manor location narrative
            manor = new Location
            {
                Name = "Rich Manor",
                asciiArt = File.ReadAllText("assets/text/Manor.txt"),
                Description = "Rich Manor",
                VisitedDescription = "I’ve already gone through this part. No reason to linger.",
                Dialogue = @"My intel pointed me to a manor in the more well off part of town. Upon arriving things seemed normal, no issues… but I needed to talk to the owner, just to be sure…

Got talking to the owner of the house though, after like 20 minutes of waitin’

After talkin’ to the man I learned that Casey’s mom worked as a cleaner at his manor

He seemed pretty forth comin’ all things considered… Talked a lot about the things Casey’s mom would talk about.

A lot of complainin’ about her son… especially recently, was hearin’ a lot of things about Casey’s recent erratic behaviour…

She was apparently really upset with the kid, saying some real heinous shit…

She was mad sure… but apparently he was real frustrated from work, something about it affecting his mood… not sure if that justifies a murder…
"
            };

            Terminal.Clear();
            //-----------ascii art--------------------[Detective Office]

            Terminal.WriteLine();
            //office location narrative
            office = new Location
            {
                Name = "Detective Office",
                asciiArt = File.ReadAllText("assets/text/Detective-at-Desk.txt"),
                Description = "Detective Office",
                VisitedDescription = "I’ve already gone through this part. No reason to linger.",
                Dialogue = @"Back at my office… There’s no more time…
 
I gotta make a choice…

Who murdered Casey…?
"

            };
            //connect locations
            //intro paths
            intro.Paths.Add("1", generalStore);
            intro.Paths.Add("2", morgue);

            //general store paths
            generalStore.Paths.Add("1", courtHouse);
            generalStore.Paths.Add("2", bank);
            generalStore.Paths.Add("3", school);
            generalStore.Paths.Add("4", bar);
            generalStore.Paths.Add("5", hospital);
            //bank paths
            bank.Paths.Add("1", manor);
            bank.Paths.Add("2", school);
            bank.Paths.Add("3", bar);
            bank.Paths.Add("4", hospital);
            bank.Paths.Add("5", courtHouse);
            bank.Paths.Add("6", generalStore);
            //school paths
            school.Paths.Add("1", manor);
            school.Paths.Add("2", bar);
            school.Paths.Add("3", bank);
            school.Paths.Add("4", courtHouse);
            //manor paths
            manor.Paths.Add("1", morgue);
            manor.Paths.Add("2", hospital);
            manor.Paths.Add("3", bank);
            manor.Paths.Add("4", school);
            manor.Paths.Add("5", bar);
            manor.Paths.Add("6", generalStore);
            manor.Paths.Add("7", courtHouse);

            //morgue paths
            morgue.Paths.Add("1", bar);
            morgue.Paths.Add("2", hospital);
            morgue.Paths.Add("3", manor);
            morgue.Paths.Add("4", bank);
            morgue.Paths.Add("5", school);
            

            //court house paths
            courtHouse.Paths.Add("1", manor);
            courtHouse.Paths.Add("2", hospital);
            courtHouse.Paths.Add("3", school);
            courtHouse.Paths.Add("4", bar);
            courtHouse.Paths.Add("5",bank);
            //hospital paths
            hospital.Paths.Add("1", generalStore);
            hospital.Paths.Add("2", morgue);
            hospital.Paths.Add("3", school);
            hospital.Paths.Add("4", courtHouse);
            hospital.Paths.Add("5", bank);
            //bar paths
            bar.Paths.Add("1", manor);
            bar.Paths.Add("2", hospital);
            bar.Paths.Add("3", school);

        }



        // Execute() runs based on Program.TerminalExecuteMode (assign to it in Setup).
        //  ExecuteOnce: runs only once. Once Execute() is done, program closes.
        //  ExecuteLoop: runs in infinite loop. Next iteration starts at the top of Execute().
        //  ExecuteTime: runs at timed intervals (eg. "FPS"). Code tries to run at Program.TargetFPS.
        //               Code must finish within the alloted time frame for this to work well.
        public void Execute()
        {
            if (!IntroPlayed)
            {
                PlayIntro();
                IntroPlayed = true;
            }

            StartExploration(currentLocation);
        }
        private void PlayIntro()

        {

            //Page 1 
            //-----------ascii art--------------------
            Terminal.UseRoboType = false;
            string detectiveAscii = File.ReadAllText("assets/text/detective.txt");
            Terminal.WriteLine(detectiveAscii);
            Terminal.WriteLine();
            Terminal.UseRoboType = true;
            Terminal.RoboTypeIntervalMilliseconds = 40;
            Terminal.WriteLine("My name is Miles Lambert, a private eye within Cross City.");
            Terminal.WriteLine("Recently I have been hired to solve a murder.");
            Terminal.WriteLine("Press [Enter] to continue");
            Terminal.WriteLine();
            Terminal.ReadLine();
            Audio.Play(clickSfx);
            Terminal.Clear();

            //Page 2
            Terminal.UseRoboType = false;
            string cornerStoreAscii = File.ReadAllText("assets/text/Corner-Store.txt");
            Terminal.WriteLine(cornerStoreAscii);
            Terminal.UseRoboType = true;
            Terminal.RoboTypeIntervalMilliseconds = 40;
            Terminal.WriteLine("The victim, a young man named Casey Wentz, was murdered last night at a local general store, Large Grizzly General Store.");
            Terminal.WriteLine("Reports say the kid was just buying some snacks after a long shift…");
            Terminal.WriteLine("");
            Terminal.ReadLine();
            Audio.Play(clickSfx);
            Terminal.Clear();
            //----------ascii art----------------------
            //Page 3 
            Terminal.UseRoboType = false;
            string morgueAscii = File.ReadAllText("assets/text/Morgue.txt");
            Terminal.WriteLine(morgueAscii);
            
            Terminal.WriteLine("Casey’s body was brought to the morgue early this morning, from what the coroner has said, the death was pretty grizzley…");
            Terminal.UseRoboType = true;
            Terminal.RoboTypeIntervalMilliseconds = 40;
            Terminal.ReadLine();
            Terminal.Clear();
            Audio.Play(clickSfx);
            //-------ascii art----------------------

            //Page 4
            Terminal.UseRoboType = false;
            Terminal.WriteLine(detectiveAscii);
            Terminal.WriteLine();
            Terminal.UseRoboType = true;
            Terminal.RoboTypeIntervalMilliseconds = 40;
            Terminal.WriteLine("The attacker disappeared right after the attack, I need to find the perpetrator… and fast…");
            Terminal.WriteLine("");
            Terminal.WriteLine("The longer he’s out there the more dangerous the streets grow…");
            Terminal.WriteLine("");
            Terminal.WriteLine("But… something about this all feels off…");
            Terminal.WriteLine("");
            Terminal.WriteLine("I have 5 days to solve this case, and so many places to go…");
            Terminal.WriteLine("");
            Terminal.WriteLine("I gotta be smart on where to go… Can’t let this guy get away…");
            Audio.Play(clickSfx);
            Terminal.UseRoboType = false;
            Terminal.ReadLine();
            //--------ascii art---------------
        }
        private void VisitLocation(Location location)
        {

            {
                Terminal.Clear();
                location.onVisit();
                //check if location has been visited
                bool firstVisit = !visitedLocations.Contains(location);
                //new location visit or revisit logic
                if (firstVisit)
                {
                    visitedLocations.Add(location);
                    daysPassed++;
                    Terminal.WriteLine($"{location.asciiArt}");
                    Terminal.WriteLine($"{location.Name}");
                    Terminal.WriteLine($"Day {daysPassed} of {maxDays}");
                    Terminal.WriteLine(location.Description);
                    Terminal.WriteLine("");
                    Terminal.WriteLine("(Press [Enter] to continue...)");
                    Terminal.ReadLine();
                    Terminal.WriteLine(location.Dialogue);
                    Terminal.WriteLine("");
                }
                else
                {
                    Terminal.WriteLine($"{location.Name} (Revisited)");
                    Terminal.WriteLine($"Day {daysPassed} of {maxDays}");
                    Terminal.WriteLine(location.VisitedDescription);
                    Terminal.WriteLine("");
                    Terminal.WriteLine(location.Dialogue);
                    Terminal.WriteLine("");
                }
                Terminal.ReadLine();
                //Terminal.SetCursorPosition(0, 0);
                //Terminal.WindowHeight = / 2;

            }
        }
        private void StartExploration(Location start)
        {
            currentLocation = start;
            while (!gameOver)
            {
                VisitLocation(currentLocation);
                //check for game over
                if (daysPassed >= maxDays)
                {
                    gameOver = true;
                    VisitOffice();
                    Terminal.WriteLine("Game Over");
                    Terminal.ReadLine();
                    Environment.Exit(0);
                    return;
                }
                Terminal.WriteLine("");
                //display paths to other locations
                Terminal.Clear();
                Terminal.WriteLine("Possible Paths:");
                foreach (var path in currentLocation.Paths)
                {
                    Terminal.WriteLine($"[{path.Key}] {path.Value.Name}");
                }
                Terminal.WriteLine("");
                Terminal.WriteLine("Where should I go?");
                string answer = Terminal.ReadAndClearLine();
                if (currentLocation.Paths.ContainsKey(answer))
                {
                    Audio.Play(clickSfx);
                    currentLocation = currentLocation.Paths[answer];
                }
                else
                {
                    Terminal.WriteLine("Invalid choice.");
                    Terminal.ReadLine();
                }
            }
        }
        private void VisitOffice()
        {
            Terminal.Clear();
            //Page 1 (Detective Sitting at Desk)
            Terminal.WriteLine("Back at my office… There’s no more time…");
            Terminal.WriteLine("I gotta make a choice…");
            Terminal.WriteLine("Who murdered Casey…?");
            Terminal.WriteLine("");
            Terminal.WriteLine("Who do you accuse?");
            Terminal.WriteLine("[1] Casey's Mom");
            Terminal.WriteLine("[2] Co-Worker");
            Terminal.WriteLine("[3] Jason Feltman ");

            string choice = Terminal.ReadAndClearLine();
            //set correct answer
            string correctAnswer = "2";
            if (choice == correctAnswer) 
            {
                Audio.Play(clickSfx);
                Terminal.WriteLine("\r\nLater that day, I accused Casey’s coworker of the murder…\r\n\r\nAfter some further digging…\r\n\r\nThe pieces started to add up… DNA tests, that bank teller’s testimony…\r\n\r\nAnd through that, we got a confession…\r\n\r\nCasey’s killer has been brought to justice… and his family can rest easy knowing he’s behind bars.\r\n\r\nAnd… I finally feel like I atoned for my own kids passing…\r\n");
            }

            else if (choice == "1" ) 
            {
                Audio.Play(clickSfx);
                Terminal.WriteLine("\r\nLater that day, I accused Ms. Wentz of the murder… \r\n \r\nAfter some further digging…\r\n\r\nI was wrong…\r\n\r\nCasey’s mom was released a small while later… but the damage was already done… the pain\r\n\r\nof losin’ her kid, and then being accused drove her a lil insane…\r\n\r\nWhoever took Casey’s life is gone now… And I’ve failed…\r\n\r\nAgain…\r\n");
            }
            else if ( choice == "3")
            {
                Audio.Play(clickSfx);
                Terminal.WriteLine("\r\nLater that day, I accused Jason Feltman of the murder… \r\n\r\nAfter some further digging…\r\nI was wrong…\r\n\r\nOn the day of Feltman's court hearing the kid provided some damning evidence provin’ his innocence… \r\n\r\nMade me look like a complete fool…\r\n\r\nWhoever took Casey’s life is gone now… And I’ve failed…\r\n\r\nAgain…\r\n");
            }
            else
            {
                Terminal.WriteLine("");
                Terminal.WriteLine("Invalid choice. Try again.");
                Terminal.ReadLine();
                VisitOffice(); // Retry if invalid
                return;
            }
        }

    }
}