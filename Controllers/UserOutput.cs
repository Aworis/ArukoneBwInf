using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArukoneKonsole.Controllers
{
    public static class UserOutput
    {
        public static string UnsolvedTxtPath { get; } = Path.Combine(Environment.CurrentDirectory, "ArukoneOutput", "unsolved.txt");
        public static string SolvedTxtPath { get; } = Path.Combine(Environment.CurrentDirectory, "ArukoneOutput", "solved.txt");

        public static void CreateArukoneTxtFile(ArukoneController arukoneController, int[,] arukoneArray, string filePath)
        {
            try
            {
                using StreamWriter writer = new StreamWriter(filePath);
                writer.WriteLine(arukoneController.arukoneBoard.BoardSize);
                writer.WriteLine(arukoneController.arukoneBoard.NumberOfChains);

                int rows = arukoneArray.GetLength(0);
                int columns = arukoneArray.GetLength(1);

                for (int i = rows - 1; i >= 0; i--)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        string whiteSpace = (arukoneArray[i, j] >= 10) ? " " : "  ";

                        writer.Write(arukoneArray[i, j] + whiteSpace);
                    }
                    writer.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fehler beim Schreiben in die Datei: " + ex.Message);
                Console.ReadLine();
            }
        }

        public static void PrintArukoneResults(int[,] arukoneArray, string stringForNull)
        {
            int rows = arukoneArray.GetLength(0);
            int columns = arukoneArray.GetLength(1);

            Console.WriteLine("_________________________");
            for (int i = rows - 1; i >= 0; i--)
            {
                for (int j = 0; j < columns; j++)
                {
                    string whiteSpace = (arukoneArray[i, j] >= 10) ? " " : "  ";
                    string cellValue = (arukoneArray[i, j] == 0) ? stringForNull : arukoneArray[i, j].ToString();

                    Console.Write(cellValue + whiteSpace);
                }
                Console.WriteLine();
            }
        }

        public static void GiveFeedback()
        {
            Console.WriteLine("Die Konsolenanwendung wurde durchgeführt.");

            if (File.Exists(UnsolvedTxtPath) && File.Exists(SolvedTxtPath))
            {
                Console.WriteLine("Es wurde folgende Datei gefunden: " + UnsolvedTxtPath);
                Console.WriteLine("Es wurde folgende Datei gefunden: " + SolvedTxtPath);
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Etwas ist schief gelaufen. Es konnten keine Textdatein gefunden werden.");
                Console.ReadLine();
            }
        }
    }
}
