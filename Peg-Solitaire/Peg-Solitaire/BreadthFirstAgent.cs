﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Peg_Solitaire
{
    class BreadthFirstAgent : Agent
    {
        private GameState gameState;
        DateTime timeWait = DateTime.Now.AddSeconds(10);

        public BreadthFirstAgent(GameState startState)
        {
            gameState = startState;
        }

        /// <summary>
        /// Attempts to solve the game state using a breadth-first search algorithm.
        /// If a solution is found, the move sequence to the solution is returned.
        /// Each move is a list of coordinate pairs formatted as follows:
        /// The first pair is the row and column of the peg to be moved
        /// The second pair is the row and column of the peg to be jumped
        /// The third pair is the row and column of the new peg location
        /// Example output:
        /// [[[2,0],[1,0],[0,0]],[[2,2],[1,1],[0,0]]...]
        /// If no solution is found, a no solution exception is thrown.
        /// </summary>
        /// <returns> Nexted list containing a move sequence to a solution. </returns>
        public override List<List<List<int>>> Solve()
        {
            Queue<List<List<List<int>>>> moveQueue = new Queue<List<List<List<int>>>>();
            Queue<GameState> stateQueue = new Queue<GameState>();
            List<GameState> explored = new List<GameState>();
            List<GameState> frontier = new List<GameState>();
            List<List<List<int>>> returnList = new List<List<List<int>>>();
            GameState frontState;
            List<GameState> children;
            List<List<List<int>>> frontMoveList;
            List<List<List<int>>> nextMoveList;

            if (gameState.IsGoalState())
                return new List<List<List<int>>>();
            stateQueue.Enqueue(gameState);
            moveQueue.Enqueue(new List<List<List<int>>>());
            while (stateQueue.Count > 0)
            {
                Cursor.Current = Cursors.WaitCursor;
                if (timeWait <= DateTime.Now)
                {
                    MessageBox.Show("No solution found by breadth first search.");
                    System.Diagnostics.Process.Start(Application.ExecutablePath);
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                    break;
                }
                frontState = stateQueue.Dequeue();
                frontMoveList = moveQueue.Dequeue();
                explored.Add(frontState);
                if(frontState.IsGoalState())
                {
                    return frontMoveList;
                }
                children = frontState.GetSuccessors();
                foreach (GameState indivChild in children)
                {
                    if((!explored.Contains(indivChild)) && (!frontier.Contains(indivChild)))
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
            throw new Exception("No solution found by breadth first search.");
        }
    }
}
