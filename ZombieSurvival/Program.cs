﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieSurvival
{
    class Program
    {
       
        static void Main(string[] args)
        {
            Random r = new Random();
            int difficulty = r.Next(1, 5);//Determines difficulty

            Console.ForegroundColor = ConsoleColor.DarkGreen;//sets spooky green text
            Console.WriteLine("As the world crumbles to the infected, you wake up");//Intro
            Console.WriteLine("'What is my name?' You think...");
            string name = Console.ReadLine();
            Survivor player = new Survivor(name,r.Next(5,11),r.Next(1,4)); //Player character
            Console.Clear();
            Console.WriteLine($"Ah my name is {player.name}.");
            clearCon();
            
            Console.WriteLine("While trying to gather my thoughts, I notice zombie(s) heading towards my direction.");
            Console.WriteLine($"Looks like there are {difficulty} zombie(s) moving my direction."); //lets the player know the difficulty of the area without directly saying
            Console.WriteLine("Almost out of fear I instinctively run away.");
            clearCon();
            

            
            //end of intro 
            bool menuLoop = true;
            int day = 0;
            List<Survivor> survivorsList = new List<Survivor>();
            survivorsList.Add(player);
            TownList townlist = new TownList();
            Town currentTown = townlist.GetTownBasedOnDifficulty(difficulty);
            int deadCount = 0;//Goes up for every dead member
            Console.WriteLine("After running for who knows how long, I end up running out of energy.");
            Console.WriteLine($"I decide to stay at {currentTown.name} and set up camp.");
            clearCon();
            //start of game
            while (menuLoop)
            {
                int aggro = 0;
                survivorsList.ForEach(x =>
                {
                    aggro += x.luck;
                });
                aggro += currentTown.danger * 2;
                Console.WriteLine($"Day {day}. Currently in {currentTown.name}");
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1.Scavenge locally");
                Console.WriteLine("2.Look for zombies");
                Console.WriteLine("3.Change locations");
                Console.WriteLine("4.Rest");
                Console.WriteLine("5.Stats");
                bool finishday = true;
                while (finishday)
                {
                    string x = Console.ReadLine();
                    
                    switch (x)
                    {
                        case "1": //scavenge locally
                            finishday = false;
                            break;
                        case "2": //look zombies

                            finishday = false;
                            break;
                        case "3"://search towns
                            finishday = false;
                            Town result = townlist.GetNewTown(currentTown);
                            Console.WriteLine($"You find yourself at {result.name}. Do you want to move here? y/n");
                            bool loop = true;
                            while (loop)
                            {
                                string choice = Console.ReadLine();
                                switch (choice.ToLower())
                                {
                                    case "y":
                                        currentTown = result;
                                        loop = false;
                                        break;
                                    case "n":
                                        loop = false;
                                        break;
                                    default:
                                        Console.WriteLine("Please enter y or n.");
                                        break;
                                }
                            }
                            break;
                        case "4": //rest / heal
                            finishday = false;
                            survivorsList.ForEach(person => //heals all
                            {
                                person.hp = person.maxHp;
                            });
                            Console.WriteLine("You've decided that it would be a good time to rest. All health has been restored.");
                            break;
                        case "5": //stats
                            Console.WriteLine("---------------------------------------------");
                            Console.WriteLine("Survivor stats");
                            survivorsList.ForEach(person =>
                            {
                                Console.WriteLine($"{person.name}: HP:{person.hp}/{person.maxHp} Damage:{person.damage}");
                            });
                            Console.WriteLine("---------------------------------------------");
                            Console.WriteLine("Group stats");
                            Console.WriteLine($"Group threat:{aggro}");
                            Console.WriteLine($"Area danger:{currentTown.danger}");
                            Console.WriteLine($"Dead group members:{deadCount}");
                            Console.WriteLine("---------------------------------------------");
                            Console.WriteLine("What would you like to do?");
                            break;
                        default: //covers wrong options
                            Console.WriteLine("Please enter a number 1-5.");
                            break;
                    }
                    
                }
                clearCon();
                day++; //adds to day counter
            }

            Console.WriteLine("Game Over");
            Console.ReadLine();
        }
        static void clearCon()
        {
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
