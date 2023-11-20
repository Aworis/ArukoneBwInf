using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArukoneKonsole.Controllers
{
    public static class UserInput
    {
        public static int IntputBoardsize { get; private set; }

        public static void AskForBoardsize()
        {
            Console.Write("Gebe für die Spielfeldgröße eine Zahl zwischen 4 und 30 ein: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int boardSize) &&
                boardSize >= ArukoneBoard.MinBoardsize &&
                boardSize <= ArukoneBoard.MaxBoardsize)
            {
                IntputBoardsize = boardSize;
            }
            else
            {
                Console.WriteLine("Ungültige Eingabe. Bitte gebe eine ganze Zahl zwischen 4 und 30 ein ein.");
                AskForBoardsize();
            }
        }
    }
}
