using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Monopoly
{
    internal class Method
    {
        private static int Dice(int dice)
        {//random number method for dice

            Random d = new Random();
            if (dice == 6)
            {
                int dice6 = d.Next(1, 7);
                return dice6;
            }
            else
            {
                int dice3 = d.Next(1, 4);
                return dice3;
            }
        }
        public static void LocationFind(int player, int location0, int location1, int money0, int money1, out int location, out int money, out int diceRoll, out string playerName)
        {

            //who's the current player
            switch (player)
            {
                case 1:
                    playerName = "Boot";
                    money = money0;
                    location = location0;
                    break;
                default:
                    playerName = "Dog";
                    money = money1;
                    location = location1;
                    break;
            }

            //making dice roll
            diceRoll = Dice(6);

        }
        public static void TileIndex(int diceRoll, int location, int player, out int newLocation, out string propertyName, out int rent, out int specialCase)
        {

            switch (location)
            {
                case 21:
                    propertyName = "Jail";
                    rent = 0;
                    specialCase = 3;

                    if (diceRoll >= 3)
                    {
                        //if dice role is 3 or above, change location to 0 + 3 of dice roll = visiting jail
                        newLocation = 3;
                        rent = 0;
                        specialCase = 1;
                    }
                    else
                    {
                        //stuck in jail
                        rent = 0;
                        newLocation = 21;
                        specialCase = 2;
                    }

                    break;
                case 1:
                    propertyName = "Go";
                    rent = 0;
                    newLocation = location;
                    specialCase = 0;
                    break;
                case 2:
                    propertyName = "Old Kent Road";
                    rent = 21;
                    newLocation = location;
                    specialCase = 0;
                    break;
                case 3:
                    propertyName = "Visiting Jail";
                    rent = 0;
                    newLocation = location;
                    specialCase = 4;
                    break;
                case 4:
                    propertyName = "The Strand";
                    rent = 80;
                    newLocation = location;
                    specialCase = 0;
                    break;
                case 5:
                    propertyName = "Chance";

                    int d3 = Dice(3);

                    if (d3 == 1)
                    {
                        //win lottery
                        rent = 0;
                        newLocation = location;
                        specialCase = 5;
                    }
                    else if (d3 == 2)
                    {
                        //lose casino
                        rent = 0;
                        newLocation = location;
                        specialCase = 6;
                    }
                    else //d3 == 3
                    {
                        //sends player to 21: Jail
                        rent = 0;
                        newLocation = 21;
                        specialCase = 7;
                    }
                    break;
                case 6:
                    propertyName = "Fleet Street";
                    rent = 160;
                    newLocation = location;
                    specialCase = 0;
                    break;
                case 7:
                    propertyName = "Oxford Street";
                    rent = 180;
                    newLocation = location;
                    specialCase = 0;
                    break;
                case 8:
                    propertyName = "Go to Jail!";
                    rent = 0;
                    //sends player to 21: Jail
                    newLocation = 21;
                    specialCase = 8;
                    break;
                case 9:
                    propertyName = "Mayfair";
                    rent = 250;
                    newLocation = location;
                    specialCase = 0;
                    break;
                case 10:
                    propertyName = "Park Lane";
                    rent = 250;
                    newLocation = location;
                    specialCase = 0;
                    break;
                case 11:
                    propertyName = "Whitechapel";
                    rent = 280;
                    newLocation = location;
                    specialCase = 0;
                    break;
                case 12:
                    propertyName = "Vine Street";
                    rent = 500;
                    newLocation = location;
                    specialCase = 0;
                    break;
                default:
                    propertyName = "N/A";
                    rent = 0;
                    newLocation = location;
                    specialCase = 0;
                    break;


            }
        }
        public static void SpecialCase(int specialCase, string playerName, out int newMoney)
        {
            newMoney = 0;
            if (specialCase == 8)
            {
                Console.WriteLine($"{playerName} is going to jail!");                
            }
            if (specialCase == 4)
            {
                Console.WriteLine($"{playerName} is visiting jail.");
            }
            if (specialCase == 3)
            {
                Console.WriteLine($"{playerName} is in jail.");
            }
            if (specialCase == 1)
            {
                Console.WriteLine($"{playerName} has been released from jail!");
            }
            if (specialCase == 2)
            {
                Console.WriteLine($"{playerName} is stuck in jail!");
            }
            if (specialCase == 5)
            {
                Console.WriteLine($"{playerName} wins the lottery and is given £2000!");
                newMoney += 2000;
            }
            if (specialCase == 6)
            {
                Console.WriteLine($"{playerName} gets a black eye and loses £2000 trying to double up in the casino.");
                newMoney -= 2000;
            }
            if (specialCase == 7)
            {
                Console.WriteLine($"{playerName} was sent to jail for a bank robbery gone wrong.");
            }


        }
    }
    internal class Display
    {
        public static void Board(int player, int location, int diceRoll)
        {
            string[] array = new string[12];
            int count = 0;
            foreach (string a in array)
            { array[count] = "  "; count++; }

            if ((location >= 0) && (location <= array.Length))
            {
                switch (player)
                {
                    case 1:
                        array[location - 1] = "▓▄";
                        break;
                    case 2:
                        array[location - 1] = "█@";
                        break;
                    default:
                        array[location - 1] = "??";
                        break;
                }

                Console.WriteLine($" _______________________________\r\n|Go     |KentRd |Jail   |Strand |\r\n|  {array[0]}   |  {array[1]}   |  {array[2]}   |  {array[3]}   |\r\n|_______|_______|_______|_______|\r\n|VineSt |               |Chance |\r\n|  {array[11]}   |               |  {array[4]}   |\r\n|_______|      [{diceRoll}]      |_______|\r\n|WhiteCh|               |FleetSt|\r\n|  {array[10]}   |               |  {array[5]}   |\r\n|_______|_______________|_______|\r\n|ParkLn |Mayfair|GoJail |Oxford |\r\n|  {array[9]}   |  {array[8]}   |  {array[7]}   |  {array[6]}   |\r\n|_______|_______|_______|_______|\r\n");
                

            }
            else if (location == 21)//21 is jail
            {
                Console.WriteLine(" ==============================\r\n||    ||    ||STUCK||    ||    ||\r\n||    ||    ||     ||    ||    ||\r\n||    ||    ||     ||    ||    ||\r\n||    ||    ||-----||    ||    ||");

                if (diceRoll == 2)
                {
                    Console.WriteLine("||    ||    || █▀█ ||    ||    ||\r\n||====||====|| ░▄▀ ||====||====||\r\n||    ||    || █▄▄ ||    ||    ||");
                }
                else if (diceRoll == 1)
                {
                    Console.WriteLine("||    ||    || ▄█░ ||    ||    ||\r\n||====||====|| ░█░ ||====||====||\r\n||    ||    || ▄█▄ ||    ||    ||");
                }
                else
                {
                    Console.WriteLine("\n             |  " + diceRoll + "  |\n");
                }

                Console.WriteLine("||    ||    ||-----||    ||    ||\r\n||    ||    ||     ||    ||    ||\r\n||    ||    ||     ||    ||    ||\r\n||    ||    ||JAIL!||    ||    ||\r\n ===============================\r\n");
            }

            /* test display functions
            foreach (string b in array)
            { Console.WriteLine(b); }
            */

        }
    }
    internal class Player
    {
        public int ID;
        public int money;
        public int location;
    }
    internal class Program
    {    
        static void Main(string[] args)
        {
            string version = "0.4";
            int gameMode = 1;

            //Home screen
            string logo = "██████████████████████████████████████████████████\r\n█▄─▀█▀─▄█─▄▄─█▄─▀█▄─▄█─▄▄─█▄─▄▄─█─▄▄─█▄─▄███▄─█─▄█\r\n██─█▄█─██─██─██─█▄▀─██─██─██─▄▄▄█─██─██─██▀██▄─▄██\r\n▀▄▄▄▀▄▄▄▀▄▄▄▄▀▄▄▄▀▀▄▄▀▄▄▄▄▀▄▄▄▀▀▀▄▄▄▄▀▄▄▄▄▄▀▀▄▄▄▀";
            Console.Write($"{logo}█████████version.{version}███");
            Console.WriteLine("\n\n Using the dice, move around a 12 tile Monopoly board.\r\n Collect properties, avoid jail-time, and win big!\r\n\r\n The board consists of 1 go, 1 chance, 1 go to jail, 1 jail and 8 property tiles.\r\n The game has two players: Boot and Dog.\r\n All players start on GO and finish at the end of round 20.\r\n All players start with £2000.\n\n Epilepsy warning: some flashing\n\n There are three game modes:\n 1 - Player vs. Player\n 2 - Player vs. A.I.\n 3 - A.I. vs. A.I.\n\n Enter game mode:");

            gameMode = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            //variables:
            Player Boot = new Player();
            Boot.ID = 1;
            Boot.money = 2000;
            Boot.location = 1;

            Player Dog = new Player();
            Dog.ID = 2;
            Dog.money = 2000;
            Dog.location = 1;

            int[] players = { Boot.ID, Dog.ID };
            string playerName;
            int diceRoll;
            int rent;
            int money;
            int newMoney = 0;
            int location;
            int newLocation;
            bool goTrigger = false;
            string propertyName;
            int specialCase;
            //array with what player owns which tile, -1 means non-ownable tile
            int[] ownership = new int[21] { -1, 0, -1, 0, -1, 0, 0, -1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1 };

            Method object1 = new Method();

            for (int round = 1; round <= 20; round++)
            {

                foreach (int player in players)
                {
                    Method.LocationFind(player, Boot.location, Dog.location, Boot.money, Dog.money, out location, out money, out diceRoll, out playerName);

                    //first location of round
                    Method.TileIndex(diceRoll, location, player, out newLocation, out propertyName, out rent, out specialCase);
                    location = newLocation;

                    if (gameMode != 3)
                    {
                        //Console write . temp
                        Console.WriteLine($"{logo}______ROUND: {round}/20\n");
                        Display.Board(player, location, diceRoll);

                        Console.WriteLine($"Player name: {playerName}\nMoney: £{money}\nDice roll:{diceRoll}\nLocation name: {propertyName}\nRent: {rent}\n");

                        switch (gameMode)
                        {
                            default:
                                Console.WriteLine("█ PRESS ENTER TO ROLL █");
                                while (Console.ReadKey().Key != ConsoleKey.Enter) ;
                                break;
                            case 2:
                                if (player == 1)
                                {
                                    Console.WriteLine("█ PRESS ENTER TO ROLL █");
                                    while (Console.ReadKey().Key != ConsoleKey.Enter) ;
                                }
                                break;
                        }


                        Console.Clear();
                    }

                    // diceroll count to location. 20 exception
                    while ((diceRoll > 0) && (location != 21))
                    {
                        location++;
                        diceRoll--;

                        //display animation:

                        if (gameMode != 3)
                        {
                            Console.SetCursorPosition(0, 5);
                            Display.Board(player, location, diceRoll);
                            Thread.Sleep(200);
                            Console.Clear();
                        }
                        
                        //for when player passes go
                        //if location ID is over 12, -12
                        if (location > 12)
                        {
                            location -= 12;
                            goTrigger = true;
                        }
                    }


                    Console.WriteLine($"{logo}______ROUND: {round}/20\n");
                    Display.Board(player, location, diceRoll);

                    Console.WriteLine("MESSAGES:");
                    Method.TileIndex(diceRoll, location, player, out newLocation, out propertyName, out rent, out specialCase);
                    location = newLocation;

                    //setting ownership on the ownership array. ownership -1 because of how array indexing works
                    if (location != 20)
                    {
                        if (ownership[location - 1] == 0)
                        {
                            if (money >= (rent * 10))
                            {
                                ownership[location - 1] = player;
                                //ownership is set to player ID in the ownership array, cost is taken:
                                money -= (rent * 10);
                                Console.WriteLine($"{playerName} has purchased {propertyName} for £{rent * 10}");
                            }
                            else
                            {
                                Console.WriteLine($"{playerName} does not have the funds for {propertyName}");
                            }
                        }
                        else if (ownership[location - 1] != player)
                        {
                            money -= rent;
                            if (ownership[location - 1] == 1)
                            {
                                Boot.money += rent;
                                Console.WriteLine($"{playerName} pays Boot £{rent} for staying at {propertyName}");
                            }
                            else if (ownership[location - 1] == 2)
                            {
                                Dog.money += rent;
                                Console.WriteLine($"{playerName} pays Dog £{rent} for staying at {propertyName}");
                            }

                        }
                        else //(ownership[location - 1] == player)
                        {
                            Console.WriteLine($"{playerName} is visiting their own property, {propertyName}");
                        }
                    }

                    if (goTrigger == true)
                    {
                        Console.WriteLine($"{playerName} is awarded £200 for passing/landing on GO!");
                        money += 200;
                        goTrigger = false;
                    }
                    //Special case messages / scenarios
                    Method.SpecialCase(specialCase, playerName, out newMoney);
                    money += newMoney;

                    //Console write . temp
                    if (gameMode != 3) 
                    {
                        Console.WriteLine($"\nPlayer name: {playerName}\nMoney: £{money}\nDice roll:{diceRoll}\nLocation name: {propertyName}\nRent: {rent}\n");

                        Console.WriteLine("[   NEXT PLAYER PRESS SPACEBAR   ]");

                        //skips when AI vs AI gamemode
                        while (Console.ReadKey().Key != ConsoleKey.Spacebar) ;
                        Console.Clear();
                    }
                    //save variables
                    switch (player)
                    {
                        case 1:
                            Boot.location = location;
                            Boot.money = money;
                            break;
                        default://player 2
                            Dog.location = location;
                            Dog.money = money;
                            break;
                    }

                }
            }
            //End screen

            Console.Clear();
            Console.WriteLine($"\n GAME OVER\n =========\n Boot has ended with £{Boot.money}\n Dog has ended with £{Dog.money}\n");
            if (Boot.money > Dog.money)
            { Console.WriteLine(" BOOT WINS!!!"); }
            else if (Boot.money < Dog.money)
            { Console.WriteLine(" DOG WINS!!!"); }
            else
            { Console.WriteLine( "DRAW"); }
            Console.WriteLine("Thank you for playing!\nC# console MONOPOLY by Chloe Hughes-Penzer");
        }       
    }
}
