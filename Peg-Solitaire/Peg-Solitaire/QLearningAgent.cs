using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peg_Solitaire
{
    class QLearningAgent : Agent
    {
        private GameState gameState;
        private int totalExpandedStates;

        public QLearningAgent(GameState startState)
        {
            gameState = startState;
            totalExpandedStates = 0;
        }

        public override List<List<List<int>>> Solve(bool isTimeout, DateTime timeout)
        {
            GameState currentState = gameState;
            GameState nextState;
            List<List<int>> move;
            List<List<List<int>>> moveList = new List<List<List<int>>>();

            while (DateTime.Now <= timeout)
            {
                currentState = gameState;
                while(DateTime.Now <= timeout)
                {
                    move = currentState.GetMoveFromQValue();
                    nextState = currentState.NextState(move);
                    if(nextState.IsGoalState())
                    {
                        currentState.UpdateQvalue(move, 15);
                        break;
                    }
                    else if(nextState.NextMoves().Count == 0)
                    {
                        currentState.UpdateQvalue(move, 1-currentState.GetPegsLeft());
                        break;
                    }
                    else
                    {
                        currentState.UpdateQvalue(move, 1);
                    }
                    currentState = nextState;
                }
            }
            currentState = gameState;
            while(!currentState.IsGoalState() && currentState.NextMoves().Count != 0)
            {
                List<List<int>> bestMove = currentState.GetBestQMove();
                moveList.Add(bestMove);
                currentState = currentState.NextState(bestMove);
            }
            return moveList;
        }

        public override int getTotalExpandedStates()
        {
            return totalExpandedStates;
        }

    }
}
