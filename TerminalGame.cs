using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
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
            Terminal.SetTitle("Detective");
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
            bgm = Audio.LoadMusic("assets/audio/bgm.wav");
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
            Terminal.RoboTypeIntervalMilliseconds = 0;
            string introAscii = File.ReadAllText("assets/text/detective.txt");
            Terminal.WriteLine(introAscii);
            Terminal.WriteLine();
            intro = new Location
            {
                Name = "Detective’s Office",
                Description = "It’s another long night in Cross City. Time to get to work...",
                Dialogue = "Where to go?\n[1] Large Grizzly General Store\n[2] City Morgue"
            };

            Terminal.Clear();
            //-----------ascii art--------------------[Hospital]
            Terminal.RoboTypeIntervalMilliseconds = 0;
            string hospitalAscii = File.ReadAllText("assets/text/hospital.txt");
            Terminal.WriteLine(hospitalAscii);
            Terminal.WriteLine();
            hospital = new Location
            {
                Name = "Hospital",
                Description = "Silverstein Hospital",
                VisitedDescription = "I’ve already gone through this part. No reason to linger.",
                Dialogue = ""
            };
            Terminal.Clear();
            //-----------ascii art--------------------[Morgue]
            Terminal.RoboTypeIntervalMilliseconds = 0;
            string morgueAscii = File.ReadAllText("assets/text/morgue.txt");
            Terminal.WriteLine(morgueAscii);
            Terminal.WriteLine();
            //morgue location narrative
            morgue = new Location
            {
                Name = "Morgue",
                Description = "City Morgue",
                VisitedDescription = "I’ve already gone through this part. No reason to linger.",
                Dialogue = @"Walking into the morgue the stench of death hit me, not literally, 

but… in the way that hangs over your head for the days to come, how these people do this is beyond me…

Regardless, it’s part of the job, and there is a crime to be solved… and a coroner to speak with…

Meeting up with the coroner was like any other meeting, the old man looks far past his days, yet keeps an eerie demeanour… more so today than others…

Regardless, it's what you gotta do in this line of work… though I did spot something in the coroner’s office while he stepped out…

He had a browser open up on his laptop, something 'bout psychiatric help for violent thoughts… not sure if this means anything, but from how he was talkin’ he seemed pretty eager to cut this kid open… kept talkin’ about the glass lacerations across the body…

Fan of his job… or something more malicious…?"


            };
            Terminal.Clear();
            //-----------ascii art--------------------[General Store]
            
            //general store location narrative
            generalStore = new Location
            {
                Name = "General Store",
                Description = "Large Grizzly General Store",
                VisitedDescription = "I’ve already gone through this part. No reason to linger.",
                Dialogue = ""
            };
            Terminal.Clear();
            //-----------ascii art--------------------[Court House]
            
            //court house location narrative
            courtHouse = new Location
            {
                Name = "Court House",
                Description = "City Court House",
                VisitedDescription = "I’ve already gone through this part. No reason to linger.",
                Dialogue = ""
            };

            Terminal.Clear();
            //-----------ascii art--------------------[Bank]
            Terminal.RoboTypeIntervalMilliseconds = 0;
            //bank location narrative
            bank = new Location
            {
                Name = "Bank",
                Description = "First National Bank",
                VisitedDescription = "I’ve already gone through this part. No reason to linger.",
                Dialogue = ""
            };

            Terminal.Clear();
            //-----------ascii art--------------------[School]
            Terminal.RoboTypeIntervalMilliseconds = 0;
            string schoolAscii = File.ReadAllText("assets/text/college.txt");
            Terminal.WriteLine(schoolAscii);
            Terminal.WriteLine();
            //school location narrative
            school = new Location
            {
                Name = "School",
                Description = "City College",
                VisitedDescription = "I’ve already gone through this part. No reason to linger.",
                Dialogue = ""
            };

            Terminal.Clear();
            //-----------ascii art--------------------[Bar]
            
            //bar location narrative
            bar = new Location
            {
                Name = "Seedy Bar",
                Description = "Seedy Bar",
                VisitedDescription = "I’ve already gone through this part. No reason to linger.",
                Dialogue = ""
            };

            Terminal.Clear();
            //-----------ascii art--------------------[Manor]
            
            //manor location narrative
            manor = new Location
            {
                Name = "Rich Manor",
                Description = "Rich Manor",
                VisitedDescription = "I’ve already gone through this part. No reason to linger.",
                Dialogue = ""
            };

            Terminal.Clear();
            //-----------ascii art--------------------[Detective Office]
            
            Terminal.WriteLine();
            //office location narrative
            office = new Location
            {
                Name = "Detective Office",
                Description = "Detective Office",
                VisitedDescription = "I’ve already gone through this part. No reason to linger.",
                Dialogue = ""
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
            //hospital paths
            hospital.Paths.Add("1", generalStore);
            hospital.Paths.Add("2", morgue);
            hospital.Paths.Add("3", school);
            hospital.Paths.Add("4", courtHouse);

            //court house paths
            courtHouse.Paths.Add("1", manor);
            courtHouse.Paths.Add("2", hospital);
            courtHouse.Paths.Add("3", school);
            courtHouse.Paths.Add("4", bar);
            courtHouse.Paths.Add("5", bank);
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
            Terminal.RoboTypeIntervalMilliseconds = 0;
            string detectiveAscii = File.ReadAllText("assets/text/detective.txt");
            Terminal.WriteLine(detectiveAscii);
            Terminal.WriteLine();
            Terminal.RoboTypeIntervalMilliseconds = 30;
            Terminal.WriteLine("My name is Miles Lambert, a private eye within Cross City.");
            Terminal.WriteLine("Recently I have been hired to solve a murder.");
            Terminal.WriteLine("");
            Terminal.ReadLine();
            Audio.Play(clickSfx);
            Terminal.Clear();

            //Page 2
            Terminal.RoboTypeIntervalMilliseconds = 0;
            //string collegeAscii = File.ReadAllText("assets/text/college.txt");
            //Terminal.WriteLine(collegeAscii);
            //Terminal.RoboTypeIntervalMilliseconds = 40;
            Terminal.WriteLine("The victim, a young man named Casey Wentz, was murdered last night at a local general store, Large Grizzly General Store.");
            Terminal.WriteLine("Reports say the kid was just buying some snacks after a long shift…");
            Terminal.WriteLine("");
            Terminal.ReadLine();
            Terminal.Clear();
            //----------ascii art----------------------
            //Page 3 
            Terminal.WriteLine("Casey’s body was brought to the morgue early this morning, from what the coroner has said, the death was pretty grizzley…");
            Terminal.ReadLine();
            Terminal.Clear();
            //-------ascii art----------------------

            //Page 4
            Terminal.WriteLine("The attacker disappeared right after the attack, I need to find the perpetrator… and fast…");
            Terminal.WriteLine("");
            Terminal.WriteLine("The longer he’s out there the more dangerous the streets grow…");
            Terminal.WriteLine("");
            Terminal.WriteLine("But… something about this all feels off…");
            Terminal.WriteLine("");
            Terminal.WriteLine("I have 5 days to solve this case, and so many places to go…");
            Terminal.WriteLine("");
            Terminal.WriteLine("I gotta be smart on where to go… Can’t let this guy get away…");
            Terminal.ReadLine();
            //--------ascii art---------------
        }
        private void VisitLocation(Location location)
        {
            while (true)
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
                    Terminal.WriteLine($"{location.Name}");
                    Terminal.WriteLine($"Day {daysPassed} of {maxDays}");
                    Terminal.WriteLine(location.Description);
                    Terminal.WriteLine("");
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
            }
        }
        private void StartExploration(Location start)
        {
            currentLocation = start;
            while (!gameOver)
            {
                VisitLocation(currentLocation);
                //check for game over
                if (daysPassed > maxDays)
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

        }
        /*
        private void FirstLocation()
        {
            //new game menu
            Terminal.Clear();
            Terminal.RoboTypeIntervalMilliseconds = 30;
            //Page 4.5
            Terminal.WriteLine("");
            Terminal.WriteLine("Where to go?");
            Terminal.WriteLine("General Store or Morgue");
            Terminal.WriteLine("");
            Terminal.WriteLine("[1] General Store");
            Terminal.WriteLine("[2] Morgue");

            string answer = Terminal.ReadAndClearLine();
            if (answer == "1" )
            {
                VisitGeneralStore();
            }
            else if (answer =="2")
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
            Terminal.Clear();
            //Page 1 (Hospital Interior)
            Terminal.WriteLine("Silverstein Hospital… That's where Casey Wentz was brought, it’s a shame the kid died the way he did…");
            Terminal.WriteLine("The hospital halls are cold and detached, yet perhaps coming here wasn’t such a bad idea…");

            //Page 2 (Casey's Mother)
            Terminal.WriteLine("Ran into the kid’s family at the hospital… The mom, she was pretty frazzled… but she mentioned something interesting, a recent fight... ");
            Terminal.WriteLine("Apparently the kid had been pretty on edge… said some pretty venomous things to his own mother…");
            Terminal.WriteLine("The fight was apparently pretty awful, Mom tried asking him what was going on but just started screaming 'bout owning money… this led mom to start screamin’ too…");
            Terminal.WriteLine("Families fight for sure… sometimes it gets ugly… but is it enough to justify murder…?");
            Terminal.WriteLine("I’m not sure…");

            //Page 2.5 (Select where to go)
            Terminal.WriteLine("Options to go:");
            Terminal.WriteLine("General Store or Morgue");
            Terminal.WriteLine("");
            Terminal.WriteLine("[1] General Store");
            Terminal.WriteLine("[2] Morgue");

            string answer = Terminal.ReadAndClearLine();
            if (answer == "1")
            {
                VisitGeneralStore();
            }
            else if (answer == "2")
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
        private void VisitMorgue()
        {
            Terminal.Clear();
            //Page 1 (Morgue)
            Terminal.WriteLine("Walking into the morgue the stench of death hit me, not literally, but… in the way that hangs over your head for the days to come, how these people do this is beyond me…");
            Terminal.WriteLine("Regardless, it’s part of the job, and there is a crime to be solved… and a coroner to speak with...");
            
            //Page 2 (Smiling Coroner)
            Terminal.WriteLine("Meeting up with the coroner was like any other meeting, the old man looks far past his days, yet keeps an eerie demeanour… more so today than others…");
            Terminal.WriteLine("Regardless, it's what you gotta do in this line of work… though I did spy something in the coroner’s office while he stepped out…");
            Terminal.WriteLine("He had a browser open up on his laptop, something 'bout psychiatric help for violent thoughts… not sure if this means anything, but from how he was talkin’ he seemed pretty eager to cut this kid open…");
            Terminal.WriteLine("Fan of his job… or something more malicious…?");

            //Page 2.5 (Select where to go)
            Terminal.WriteLine("Where to go?");
        }
        private void VisitGeneralStore()
        {
            Terminal.Clear();
            //Page 1 (General Store)
            Terminal.WriteLine("Walking into that corner store was surprisingly rougher than anyone would have suspected,despite the effort the, and the cleaning it's clear that something horrible happened here…");
            Terminal.WriteLine("Talking with the owner he still seemed shaken by the events, but he was still able to drop some useful information… something 'bout another young man…");

            //Page 2 (Store Owner)
            Terminal.WriteLine("There was this other kid, he was there the other day, looked like he had fallen on hard times, startin’ to get desperate… ");
            Terminal.WriteLine("Couple that with the fact that Casey was clearly more well off than him… and desperation makes people do some stupid things…");
            Terminal.WriteLine("The owner said he thinks he saw him eyeing Casey’s wallet, but he also said he wasn’t wearin’ his glasses…");

            //Page 2.5 (Select where to go)
            Terminal.WriteLine("Where to go?");
            Terminal.WriteLine("Court House or Bank");
            Terminal.WriteLine("");
            Terminal.WriteLine("[1] Court House");
            Terminal.WriteLine("[2] Bank");

            string answer = Terminal.ReadAndClearLine();
            if (answer == "1")
            {
                VisitCourtHouse();
            }
            else if (answer == "2")
            {
                VisitBank();
            }
            else
            {
                Terminal.WriteLine("Invalid choice.");
                Terminal.ReadLine();
            }
        }
        
        private void VisitBank()
        {
            Terminal.Clear();
            //Page 1 (Bank)
            Terminal.WriteLine("Walking into the bank with such reasons as I did felt awful… but I was given a tip off that Casey had drawn out a large sum of money…");
            Terminal.WriteLine("The bank teller was clearly concerned, maybe a slight bit nervous, I don’t blame her… but she remained helpful regardless… Mentioned something she overheard on her break…");

            //Page 2 (Bank Teller)
            Terminal.WriteLine("Apparently, while she was on her break, outside having a smoke, she saw Casey talking to some guy, her curiosity got the better of her and she began eavesdropping…");
            Terminal.WriteLine("The dude seemed to be Casey’s coworker, and an aggressive one at that… was going off 'bout the money Casey owed…");
            Terminal.WriteLine("Angry Coworker? And owed money… seems like a recipe for disaster… but without any corroboration… hmmm…");

            //Page 2.5 (Select where to go)
            Terminal.WriteLine("Where to go?");
            Terminal.WriteLine("Rich Manor or School");
            Terminal.WriteLine("");
            Terminal.WriteLine("[1] Rich Manor");
            Terminal.WriteLine("[2] School");

            string answer = Terminal.ReadAndClearLine();
            if (answer == "1")
            {
                VisitManor();
            }
            else if (answer == "2")
            {
                VisitSchool();
            }
            else
            {
                Terminal.WriteLine("Invalid choice.");
                Terminal.ReadLine();
            }
        }
        private void VisitSchool()
        {
            Terminal.Clear();
            //Page 1 (College)
            Terminal.WriteLine("Figured heading to Casey’s college could have proved some use… I figured, could find a professor, maybe a classmate. Someone who knew the kid…");
            Terminal.WriteLine("turns out I was right..");

            //Page 2 (Young Woman)
            Terminal.WriteLine("I was able to catch the kid’s girlfriend, or… I suppose, his former girlfriend… she was just leavin’ class.");
            Terminal.WriteLine("Talkin’ with his girlfriend opened my eyes on somethings… That kid was really hatin’ work, was talking 'bout quitting…");
            Terminal.WriteLine("Apparently his one coworker wasn’t happy 'bout that, they were pretty swamped at the bar as is…\r\n");
            Terminal.WriteLine("Not sure if that is worth killin’ a person over though…");

            //Page 2.5 (Select where to go)
            Terminal.WriteLine("Where to go?");
            Terminal.WriteLine("Rich Manor or Seedy Bar");
            Terminal.WriteLine("");
            Terminal.WriteLine("[1] Rich Manor");
            Terminal.WriteLine("[2] Seedy Bar");

            string answer = Terminal.ReadAndClearLine();
            if (answer == "1")
            {
                VisitManor();
            }
            else if (answer == "2")
            {
                VisitBar();
            }
            else
            {
                Terminal.WriteLine("Invalid choice.");
                Terminal.ReadLine();
            }
        }
        private void VisitCourtHouse()
        {
            Terminal.Clear();
            //Page 1 (Court House)
            Terminal.WriteLine("Going to the courthouse, it initially felt like a waste of time. But my leads pointed me here, and  they haven’t steered me wrong before… Perhaps there really is something worthwhile here…");
            Terminal.WriteLine("It was only after I reached the halls of the courthouse… did I really realize why I was sent here.");

            //Page 2 (Young Man)
            Terminal.WriteLine("Turns out that this other kid who had some bad blood with Casey was there. I managed to snag him for an interview, after some pesterin’");
            Terminal.WriteLine("After I got him talkin’ though, he was tellin’ me about the arguments he and Casey would get into. He mentioned that the two weren’t always like this, but Casey was getting erratic recently, and it was driving everyone mad…");
            Terminal.WriteLine("The kid seems annoyed with Casey, sure. But to resort to murder..?");
            Terminal.WriteLine("Seems extreme…");

            //Page 2.5 (Select where to go)
            Terminal.WriteLine("Where to go?");
        }
        private void VisitBar()
        {
            Terminal.Clear();
            //Page 1 (The Bar)
            Terminal.WriteLine("Coheed’s, a dingy lil bar… this is where Casey worked… Walkin’ in things instantly felt off, people like me aren’t typically welcomed here…");
            Terminal.WriteLine("Regardless, I got a job to do… ");
            Terminal.WriteLine("And the kid’s old coworkers could be a great place for info.");

            //Page 2 (Bartender)
            Terminal.WriteLine("Askin’ the bartender ‘bout Casey seemed to sour his face. I asked him more ‘bout the kid and he seemed to not have much good things to say ‘bout him…");
            Terminal.WriteLine("He began goin’ on a bit of a tangent, but stopped himself, likely after realizin’ they were talkin’ to a cop…");
            Terminal.WriteLine("Regardless… He said a lot of things that raised some suspicions…");

            //Page 2.5 (Select Where to go)
            Terminal.WriteLine("Where to go?");
        }
        private void VisitManor()
        {
            Terminal.Clear();
            //Page 1 (Manor)
            Terminal.WriteLine("My intel pointed me to a manor in the more well off part of town. Upon arriving things seemed normal, no issues… but I needed to talk to the owner, just to be sure…");
            Terminal.WriteLine("Got talking to the owner of the house though, after like 20 minutes of waitin’");
            Terminal.WriteLine("After talkin’ to the man I learned that Casey’s mom worked as a cleaner at his manor");

            //Page 2 (Rich Man)
            Terminal.WriteLine("He seemed pretty forth comin’ all things considered… Talked a lot about the things Casey’s mom would talk about.");
            Terminal.WriteLine("A lot of complainin’ about her son… especially recently, was hearin’ a lot of things about Casey’s recent erratic behaviour…");
            Terminal.WriteLine("She was apparently really upset with the kid, saying some real heinous shit…");
            Terminal.WriteLine("She was mad sure… but apparently he was real frustrated from work, something about it affecting his mood… not sure if that justifies a murder…");

            //Page 2.5 (Select where to go)
            Terminal.WriteLine("Where to go?");
        }
        private void VisitOffice()
        {
            //Page 1 (Detective Sitting at Desk)
            Terminal.WriteLine("Back at my office… There’s no more time…");
            Terminal.WriteLine("I gotta make a choice…");
            Terminal.WriteLine("Who murdered Casey…?");

            //Page 2 (Casey's Mom Accused)
            Terminal.WriteLine("Later that day, I accused Ms. Wentz of the murder…");
            Terminal.WriteLine("After some further digging…");
            Terminal.WriteLine("I was wrong…");
            Terminal.WriteLine("Whoever took Casey’s life is gone now… And I’ve failed…");

            //Page 3 (Young Man Accused)
            Terminal.WriteLine("Later that day, I accused that young man of the murder…");
            Terminal.WriteLine("After some further digging…");
            Terminal.WriteLine("I was wrong…");
            Terminal.WriteLine("Whoever took Casey’s life is gone now… And I’ve failed…");

            //Page 4 (Coworker Accused)
            Terminal.WriteLine("Later that day, I accused Casey’s coworker of the murder…");
            Terminal.WriteLine("After some further digging…");
            Terminal.WriteLine("The pieces started to add up…");
            Terminal.WriteLine("And before long we got a confession…");
            Terminal.WriteLine("Casey’s killer has been brought to justice… and his family can rest easy knowing he’s behind bars.");
        }
     */
    }
}