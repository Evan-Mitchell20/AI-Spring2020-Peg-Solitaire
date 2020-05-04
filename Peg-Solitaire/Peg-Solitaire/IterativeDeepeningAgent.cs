using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peg_Solitaire
{
    class IterativeDeepeningAgent : Agent
    {
        private GameState gameState;

        public IterativeDeepeningAgent(GameState startState)
        {
            gameState = startState;
        }

        public override List<List<List<int>>> Solve()
        {
            int depthcount = 0, max_depth = 10;
            Queue<List<List<List<int>>>> moveQueue = new Queue<List<List<List<int>>>>();
            Queue<GameState> stateQueue = new Queue<GameState>();
            List<GameState> explored = new List<GameState>();
            List<GameState> frontier = new List<GameState>();
            List<List<List<int>>> returnList = new List<List<List<int>>>();
            GameState frontState;
            List<GameState> children;
            List<List<List<int>>> frontMoveList;
            List<List<List<int>>> nextMoveList;
            stateQueue.Enqueue(gameState);
            moveQueue.Enqueue(new List<List<List<int>>>());
            //if we are already in goal at beginning
            if (gameState.IsGoalState())
                return new List<List<List<int>>>();
            for (int i = 0; i < max_depth; i++)
            {
                while (stateQueue.Count > 0 && depthcount >= 0)
                {
                    depthcount++;
                    frontState = stateQueue.Dequeue();
                    frontMoveList = moveQueue.Dequeue();
                    explored.Add(frontState);
                    if (frontState.IsGoalState())
                    {
                        return frontMoveList;
                    }
                    children = frontState.GetSuccessors();
                    foreach (GameState indivChild in children)
                    {
                        if ((!explored.Contains(indivChild)) && (!frontier.Contains(indivChild)))
                        {
                            stateQueue.Enqueue(indivChild);
                            nextMoveList = new List<List<List<int>>>(frontMoveList)
                        {
                            frontState.NextMoves()[children.IndexOf(indivChild)]
                        };
                            moveQueue.Enqueue(nextMoveList);
                            frontier.Add(indivChild);
                        }
                    }
                }
            }

        }
    }
}
