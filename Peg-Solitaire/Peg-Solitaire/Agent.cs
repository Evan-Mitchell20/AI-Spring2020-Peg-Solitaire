using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peg_Solitaire
{
    class Agent
    {
        private GameState gameState;

        /// <summary>
        /// Stub for solve function of inherited classes
        /// </summary>
        /// <returns> Nexted list containing a move sequence to a solution. </returns>
        public virtual List<List<List<int>>> Solve()
        {
            throw new NotImplementedException();
        }
    }
}
