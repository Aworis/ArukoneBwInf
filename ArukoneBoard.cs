using ArukoneKonsole.Controllers;
using ArukoneKonsole.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ArukoneKonsole
{
    public class ArukoneBoard
    {
        public const int MinBoardsize = 4;
        public const int MaxBoardsize = 30;
        public const int MinChainLength = 3;

        public readonly int BoardSize;
        public readonly int[,] UnsolvedGame;

        public int NumberOfChains { get; set; }
        public int[,] SolvedGame { get; set; }

        public ArukoneBoard(int boardSize)
        {
            BoardSize = boardSize;
            SolvedGame = new int[BoardSize, BoardSize];
            UnsolvedGame = new int[BoardSize, BoardSize];
        }
    }
}