using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArukoneKonsole.Controllers
{
    public class ArukoneCalculator
    {
        private const double divisor = 2;

        private readonly double dividend;

        public ArukoneCalculator(int arukoneBoardSize)
        {
            dividend = arukoneBoardSize;
        }

        public int CalculateNumberOfChains()
        {
            double result = Math.Ceiling(dividend / divisor);
            int pairOfNumbers = (int)result;

            return pairOfNumbers;
        }
    }
}
