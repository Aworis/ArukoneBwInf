using ArukoneKonsole.Enums;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ArukoneKonsole.Controllers
{
    public class ArukoneController
    {
        public readonly ArukoneBoard arukoneBoard;
        public ArukoneCalculator arukoneCalculator;
        private readonly Random randomGenerator;

        public ArukoneController(int boardSize)
        {
            randomGenerator = new Random();
            arukoneBoard = new ArukoneBoard(boardSize);
            arukoneCalculator = new ArukoneCalculator(arukoneBoard.BoardSize);
            arukoneBoard.NumberOfChains = arukoneCalculator.CalculateNumberOfChains();
            CreateSolvableGame();
        }

        public void CreateSolvableGame()
        {
            var numberOfChains = arukoneBoard.NumberOfChains;
            var chainLength = 0;
            var maxAttempts = 100;
            var failedAttempts = 0;

            for (int i = 0; i < numberOfChains; i++)
            {
                var chainLinkNumber = i + 1;

                chainLength = PlaceChainLinks(chainLinkNumber);

                if (chainLength < ArukoneBoard.MinChainLength)
                {
                    ResetArray(arukoneBoard.SolvedGame);
                    ResetArray(arukoneBoard.UnsolvedGame);
                    i = -1;
                    failedAttempts++;

                    if (failedAttempts >= maxAttempts)
                    {
                        Console.WriteLine("Maximale Anzahl von Versuchen erreicht. Abbruch.");
                        break;
                    }
                }
            }
        }

        private (int XCoordinate, int YCoordinate) GetRandomCoordinates()
        {
            int xCoordinate = randomGenerator.Next(arukoneBoard.BoardSize);
            int yCoordinate = randomGenerator.Next(arukoneBoard.BoardSize);

            return (xCoordinate, yCoordinate);
        }
        
        private (int newXCoordinate, int newYCoordinate) GetAdjacentCoordinates(int xCoordinate, int yCoordinate, Directions direction)
        {
            if (direction == Directions.down)
            {
                return (xCoordinate, yCoordinate - 1);
            }
            else if (direction == Directions.up)
            {
                return (xCoordinate, yCoordinate + 1);
            }
            else if (direction == Directions.left)
            {
                return (xCoordinate - 1, yCoordinate);
            }
            else if (direction == Directions.right)
            {
                return (xCoordinate + 1, yCoordinate);
            }
            else
            {
                throw new InvalidOperationException("Ungültige Richtung für randomDirection.");
            }
        }

        private Directions GetRandomDirection()
        {

            var enumLength = Enum.GetValues(typeof(Directions)).Length;
            var index = randomGenerator.Next(enumLength);

            switch (index)
            {
                case 0:
                    return Directions.up;
                case 1:
                    return Directions.down;
                case 2:
                    return Directions.left;
                case 3:
                    return Directions.right;
                default:
                    throw new ArgumentOutOfRangeException("Ungültiger Index von 'enum Directions'.");
            }
        }

        private bool CheckIfPositionIsAvailable(int xCoordinate, int yCoordinate)
        {
            if (xCoordinate >= 0 && xCoordinate < arukoneBoard.BoardSize &&     // Checks if it is within the x-axis on the game board.
                yCoordinate >= 0 && yCoordinate < arukoneBoard.BoardSize &&     // Checks if it is within the y-axis on the game board.
                arukoneBoard.SolvedGame[yCoordinate, xCoordinate] == 0)         // Checks if the new field is not occupied yet.
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckIfChainNotNearby(int[,] coordinateSystem, int currentXCoordinate, int currentYCoordinate, int chainLink)
        {
            var numberOfSameChainLinks = 0;

            // Check surrounding Arukone fields in different directions
            // Diagonals not needed
            int[][] directionOffsets = new int[][] {
                //new int[] { -1, -1 }, // top left
                //new int[] { -1, 1 },  // top right
                //new int[] { 1, -1 },  // bottom left
                //new int[] { 1, 1 },   // bottom right
                new int[] { -1, 0 },    // top
                new int[] { 1, 0 },     // bottom
                new int[] { 0, -1 },    // left
                new int[] { 0, 1 }      // right
            };

            foreach (var offset in directionOffsets)
            {
                var x = currentXCoordinate + offset[1];
                var y = currentYCoordinate + offset[0];
                if (x >= 0 && x < coordinateSystem.GetLength(0) &&
                    y >= 0 && y < coordinateSystem.GetLength(0) &&
                    coordinateSystem[y, x] == chainLink)
                {
                    numberOfSameChainLinks++;
                    if (numberOfSameChainLinks > 1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private int PlaceChainLinks(int chainLinkNumber)
        {
            var maxAttempts = 1500;
            var failedAttempts = 0;

            (var xCoordinate, var yCoordinate) = GetRandomCoordinates();
            int newXCoordinate;
            int newYCoordinate;

            var randomMaxChainLength = randomGenerator.Next(arukoneBoard.BoardSize * 3, arukoneBoard.BoardSize * 4);
            int chainLength = 0;

            // Stop when maxAttempts are reached
            if (failedAttempts >= maxAttempts)
            {
                return chainLength;
            }

            // Place first chain link
            while (chainLength < 1 && failedAttempts <= maxAttempts)
            {
                if (CheckIfPositionIsAvailable(xCoordinate, yCoordinate))
                {
                    // Place first chain link in UnsolvedGame Array
                    arukoneBoard.UnsolvedGame[yCoordinate, xCoordinate] = chainLinkNumber;
                    arukoneBoard.SolvedGame[yCoordinate, xCoordinate] = chainLinkNumber;
                    chainLength++;
                    failedAttempts = 0;
                }
                else
                {
                    (xCoordinate, yCoordinate) = GetRandomCoordinates();
                    failedAttempts++;

                    if (failedAttempts >= maxAttempts)
                    {
                        return chainLength;
                    }
                }
            }

            // Place remaining chain links
            for (int i = 0; i < randomMaxChainLength; i++)
            {
                var newChainLinkIsSet = false;
                
                while (!newChainLinkIsSet && chainLength > 0 && chainLength < randomMaxChainLength)
                {
                    var randomDirection = GetRandomDirection();
                    (newXCoordinate, newYCoordinate) = GetAdjacentCoordinates(xCoordinate, yCoordinate, randomDirection);

                    if (CheckIfPositionIsAvailable(newXCoordinate, newYCoordinate) &&
                        CheckIfChainNotNearby(arukoneBoard.SolvedGame, newXCoordinate, newYCoordinate, chainLinkNumber))
                    {
                        xCoordinate = newXCoordinate;
                        yCoordinate = newYCoordinate;
                        arukoneBoard.SolvedGame[yCoordinate, xCoordinate] = chainLinkNumber;
                        chainLength++;
                        newChainLinkIsSet = true;
                        failedAttempts = 0;
                    }
                    else
                    {
                        failedAttempts++;

                        if (failedAttempts >= maxAttempts)
                        {
                            // Place last chain link in UnsolvedGame Array
                            arukoneBoard.UnsolvedGame[yCoordinate, xCoordinate] = chainLinkNumber;
                            return chainLength;
                        }
                    }
                }
            }

            // Place last chain link in UnsolvedGame Array
            arukoneBoard.UnsolvedGame[yCoordinate, xCoordinate] = chainLinkNumber;
            return chainLength;
        }

        private void ResetArray(int[,] coordinateSystem)
        {
            var rows = coordinateSystem.GetLength(0);
            var columns = coordinateSystem.GetLength(1);
            var replacement = 0;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    coordinateSystem[i, j] = replacement;
                }
            }
        }
    }
}
