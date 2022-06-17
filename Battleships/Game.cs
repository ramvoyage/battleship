using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships
{
    // Imagine a game of battleships.
    //   The player has to guess the location of the opponent's 'ships' on a 10x10 grid
    //   Ships are one unit wide and 2-4 units long, they may be placed vertically or horizontally
    //   The player asks if a given co-ordinate is a hit or a miss
    //   Once all cells representing a ship are hit - that ship is sunk.
    public class Game
    {
        // ships: each string represents a ship in the form first co-ordinate, last co-ordinate
        //   e.g. "3:2,3:5" is a 4 cell ship horizontally across the 4th row from the 3rd to the 6th column
        // guesses: each string represents the co-ordinate of a guess
        //   e.g. "7:0" - misses the ship above, "3:3" hits it.
        // returns: the number of ships sunk by the set of guesses
        public static int Play(string[,] ships, string[] guesses)
        {
            //decleration 
            int count = 0;
            List<string[]> shipsInBattle = ships.Cast<string>().
                GroupBy(x => count++ / ships.GetLength(1)).Select(g => g.ToArray()).ToList();

            string[] shipToGuess = guesses.Select(x => x.ToString()).ToArray();
            List<string[,]> placeTheShip = new List<string[,]>();
            int[,] grid = new int[10, 10];
            int noOfShipsHit = 0;

            //========================get range of coordinates 
            List<string[]> coordinateRanges =  CreateCoordinates(shipsInBattle);
            List<string> list = new List<string>();
            var shipsCoordinates = coordinateRanges.Select(s => s).ToArray();

            //=====================grid  
            for (int row = 0; row < 10; row++){
                for(int col = 0; col <10; col++)
                {
                    //mark the coordinate in grid 
                    string value = $"{row}:{col}";
                    foreach (var coordinate in shipsCoordinates)
                    {
                        bool res = coordinate.Contains(value);
                        if (res)
                        {
                            grid[row, col] = 1;
                        }
                    }
                }
            }

            //=============================guess the ship hit or miss
            foreach (string guess in shipToGuess)
            {
                int rowCoordinate, colCoordinate;
                ConvertCoordinateStringtoInt(guess, out rowCoordinate, out colCoordinate);

                if (grid[rowCoordinate, colCoordinate] != 0)
                {

                    foreach (string[] mships in shipsCoordinates)
                    {
                        bool res = mships.Contains(guess);
                        if (res)
                        {
                            //================ if ship hit remove from grid 
                            foreach (var ship in mships)
                            {

                                ConvertCoordinateStringtoInt(ship, out rowCoordinate, out colCoordinate);

                                grid[rowCoordinate, colCoordinate] = 0;
                            }

                            noOfShipsHit++;
                        }
                    }
                }
            }


            return noOfShipsHit;
        }

        private static void ConvertCoordinateStringtoInt(string ship, out int rowCoordinate, out int colCoordinate)
        {
            var coordinate = ship.Split(':');
            rowCoordinate = int.Parse(coordinate[0]);
            colCoordinate = int.Parse(coordinate[1]);
        }

        private static List<string[]> CreateCoordinates(List<string[]> shipsInBattle)
        {
            List<string[]> coordianteRange = new List<string[]>();
            //var shipsCount = coordianteRange.Count;
            foreach (string[] ship in shipsInBattle)
            {

                string[] coordinate = ship.Select(x => x.ToString()).ToArray();
                string[] firstCoordinateVal = coordinate.First().Split(':', ',');
                var firstCoordinate = int.Parse(coordinate.First().Split(':').FirstOrDefault());
                var lastCoordinate = int.Parse(coordinate.First().Split(':').LastOrDefault());

                //vertical coodinate --- 3:2, 3:5
                if(firstCoordinate < lastCoordinate)
                {
                    List<string> list = new List<string>(); 
                    for (int i = int.Parse(firstCoordinateVal[1]); i <= lastCoordinate; i++)
                    {
        
                        string val = $"{firstCoordinate}:{i}";
                        list.Add(val);
                    }
                    coordianteRange.Add(list.ToArray());

                }
                // horizontal 5:5, 5:1
                if (firstCoordinate > lastCoordinate)
                {
                    List<string> list = new List<string>();
                    for (int j = int.Parse(firstCoordinateVal[1]);j>=lastCoordinate; j--)
                    {
                       
                        string val = $"{firstCoordinate}:{j}";
                        list.Add(val);
                    }
                    coordianteRange.Add(list.ToArray());


                }

            }

            return coordianteRange;

        }
    }
}
