using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
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
                case 20:
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
                        newLocation = 20;
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
                    rent = 20;
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
                        //sends player to 20: Jail
                        rent = 0;
                        newLocation = 20;
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
                    //sends player to 20: Jail
                    newLocation = 20;
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
        public static void SpecialCase(int specialCase, string playerName, int money)
        {
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
                money += 2000;
            }
            if (specialCase == 6)
            {
                Console.WriteLine($"{playerName} gets a black eye and loses £2000 trying to double up in the casino.");
                money -= 2000;
            }
            if (specialCase == 7)
            {
                Console.WriteLine($"{playerName} was sent to jail for a bank robbery gone wrong.");
            }

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
            //Home screen
            Console.WriteLine("MONOPOLY");
            Console.WriteLine("\nHow to play:\nblah blah blah");
            Console.WriteLine("\n[         PRESS SPACEBAR         ]");

            while (Console.ReadKey().Key != ConsoleKey.Spacebar) ;
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
            int location;
            int newLocation;
            string propertyName;
            int specialCase;
            //array with what player owns which tile, -1 means non-property tile
            int[] ownership = new int[20] { -1, 0, -1, 0, -1, 0, 0, -1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1 };

            Method object1 = new Method();

            for (int round = 1; round <= 20; round++)
            {

                foreach (int player in players)
                {
                    Method.LocationFind(player, Boot.location, Dog.location, Boot.money, Dog.money, out location, out money, out diceRoll, out playerName);

                    //first location of round
                    Method.TileIndex(diceRoll, location, player, out newLocation, out propertyName, out rent, out specialCase);
                    location = newLocation;

                    //Console write . temp
                    Console.WriteLine($"\nMONOPOLY      round: {round}");
                    Console.WriteLine($"player name: {playerName}");
                    Console.WriteLine($"location ID: {location}");
                    Console.WriteLine($"money: {money}");
                    Console.WriteLine($"dice roll:{diceRoll}");
                    Console.WriteLine($"location name: {propertyName}");
                    Console.WriteLine($"rent: {rent}");
                    Console.WriteLine("\n PRESS ENTER TO CONTINUE");

                    // diceroll count to location. 20 exception
                    while ((diceRoll > 0) && (location != 20))
                    {
                        location++;
                        diceRoll--;
                        //for when player passes go
                        //if location ID is over 12, -12
                        if (location > 12)
                        {
                            money += 200;
                            location -= 12;
                            Console.WriteLine($"{playerName} is awarded £200 for passing/landing on GO!");
                        }
                    }


                    while (Console.ReadKey().Key != ConsoleKey.Enter) ;
                    Console.Clear();

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
                                Console.WriteLine($"{playerName} has purchased {propertyName} for {rent * 10}");
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

                    //Special case messages / scenarios
                    Method.SpecialCase(specialCase, playerName, money);


                    //Console write . temp
                    Console.WriteLine($"\nMONOPOLY      round: {round}");
                    Console.WriteLine($"player name: {playerName}");
                    Console.WriteLine($"location ID: {location}");
                    Console.WriteLine($"money: {money}");
                    Console.WriteLine($"dice roll: {diceRoll}");
                    Console.WriteLine($"location name: {propertyName}");
                    Console.WriteLine($"rent: {rent}");
                    Console.WriteLine("\n[   NEXT PLAYER PRESS SPACEBAR   ]");

                    while (Console.ReadKey().Key != ConsoleKey.Spacebar) ;
                    Console.Clear();

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

            Console.WriteLine($"\nGAME OVER\nBoot has ended with £{Boot.money}\nDog has ended with £{Dog.money}");
            if (Boot.money > Dog.money)
            { Console.WriteLine("BOOT WINS"); }
            else if (Boot.money < Dog.money)
            { Console.WriteLine("DOG WINS"); }
            else
            { Console.WriteLine("DRAW"); }
        }       
    }
}
