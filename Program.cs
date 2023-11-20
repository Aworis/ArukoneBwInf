using ArukoneKonsole.Controllers;

namespace ArukoneKonsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            UserInput.AskForBoardsize();

            ArukoneController arukone = new ArukoneController(UserInput.IntputBoardsize);

            UserOutput.CreateArukoneTxtFile(arukone, arukone.arukoneBoard.UnsolvedGame, UserOutput.UnsolvedTxtPath);
            UserOutput.CreateArukoneTxtFile(arukone, arukone.arukoneBoard.SolvedGame, UserOutput.SolvedTxtPath);

            UserOutput.GiveFeedback();
        }
    }
}