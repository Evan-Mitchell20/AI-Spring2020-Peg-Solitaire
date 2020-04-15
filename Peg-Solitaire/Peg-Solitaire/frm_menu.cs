using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Peg_Solitaire
{
    public partial class frm_menu : Form
    {
        Agent selectedAgent;
        GameState selectedGame;
        List<List<List<int>>> movesToWin;
        int moveIndex = 0;


        public frm_menu()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CenterToScreen();

            // Code below just for debug, will need to be moved
            /*
            GameState theGame = TriangleGames.BasicTriangle(5);
            //List<List<List<int>>> nextMoves = theGame.nextMoves();
            //GameState newState = theGame.nextState(nextMoves[0]);
            //List<GameState> successors = theGame.getSuccessors();
            DepthFirstAgent depthFirstAgent = new DepthFirstAgent(theGame);
            BreadthFirstAgent breadthFirstAgent = new BreadthFirstAgent(theGame);
            List<List<List<int>>> movesToWin = depthFirstAgent.Solve();
            //List<List<List<int>>> movesToWin = breadthFirstAgent.Solve();
            */
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // Get the BackgroundWorker that raised this event.
            BackgroundWorker worker = sender as BackgroundWorker;

            // Assign the result of the computation
            // to the Result property of the DoWorkEventArgs
            // object. This is will be available to the 
            // RunWorkerCompleted eventhandler.
            e.Result = selectedAgent.Solve();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void btn_run_Click(object sender, EventArgs e)
        {
            if(cmb_gameSelect.SelectedItem == null || cmb_agentSelect.SelectedItem == null)
            {
                MessageBox.Show("Please Select Game Board and Agent.", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Setup selected game type
            if(cmb_gameSelect.Text == "Triangle, 5 Row")
            {
                selectedGame = TriangleGames.BasicTriangle(5);
            }
            else if (cmb_gameSelect.Text == "Triangle, 6 Row")
            {
                selectedGame = TriangleGames.BasicTriangle(6);
            }
            else if (cmb_gameSelect.Text == "Triangle, 7 Row")
            {
                selectedGame = TriangleGames.BasicTriangle(7);
            }

            // Setup selected agent type
            if(cmb_agentSelect.Text == "Breadth First")
            {
                selectedAgent = new BreadthFirstAgent(selectedGame);
            }
            else if (cmb_agentSelect.Text == "Depth First")
            {
                selectedAgent = new DepthFirstAgent(selectedGame);
            }

            txt_output.Clear();
            movesToWin = selectedAgent.Solve();

            int moveNumber = 0;
            string lineToAdd;
            foreach(List<List<int>> move in movesToWin)
            {
                moveNumber++;
                lineToAdd = string.Format("Action {0}: ", moveNumber);
                txt_output.AppendText(lineToAdd);
                lineToAdd = string.Format("Move Peg ({0},{1}) to ({2},{3}) and Remove Peg ({4},{5})\n", 
                    move[0][0], move[0][1], move[2][0], move[2][1], move[1][0], move[1][1]);
                txt_output.AppendText(lineToAdd);
            }

            MessageBox.Show("Solution found! See animation to the right.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

            moveIndex = 0;
            animationTimer.Enabled = true;

        }

        private void cmb_gameSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            resetPegs();

            if (cmb_gameSelect.Text == "Triangle, 5 Row")
            {
                pnl_tri5.Show();
            }
            else
            {
                pnl_tri5.Hide();
            }
        }

        // Runs the move sequence animation, going through each move in the list, one per time tick.
        private void animationTimer_Tick(object sender, EventArgs e)
        {
            List<List<int>> move = movesToWin[moveIndex];

            if (cmb_gameSelect.Text == "Triangle, 5 Row")
            {
                animateMoveTri5(move);
            }

            moveIndex++;
            if (moveIndex >= movesToWin.Count)
                animationTimer.Enabled = false;
        }

        // Adjust the pegs to reflect the given move
        private void animateMoveTri5(List<List<int>> move)
        {
            // Hide the peg that is moved
            if (move[0][0] == 0 && move[0][1] == 0)
                tri5P0_0.Hide();
            else if (move[0][0] == 1 && move[0][1] == 0)
                tri5P1_0.Hide();
            else if (move[0][0] == 1 && move[0][1] == 1)
                tri5P1_1.Hide();
            else if (move[0][0] == 2 && move[0][1] == 0)
                tri5P2_0.Hide();
            else if (move[0][0] == 2 && move[0][1] == 1)
                tri5P2_1.Hide();
            else if (move[0][0] == 2 && move[0][1] == 2)
                tri5P2_2.Hide();
            else if (move[0][0] == 3 && move[0][1] == 0)
                tri5P3_0.Hide();
            else if (move[0][0] == 3 && move[0][1] == 1)
                tri5P3_1.Hide();
            else if (move[0][0] == 3 && move[0][1] == 2)
                tri5P3_2.Hide();
            else if (move[0][0] == 3 && move[0][1] == 3)
                tri5P3_3.Hide();
            else if (move[0][0] == 4 && move[0][1] == 0)
                tri5P4_0.Hide();
            else if (move[0][0] == 4 && move[0][1] == 1)
                tri5P4_1.Hide();
            else if (move[0][0] == 4 && move[0][1] == 2)
                tri5P4_2.Hide();
            else if (move[0][0] == 4 && move[0][1] == 3)
                tri5P4_3.Hide();
            else if (move[0][0] == 4 && move[0][1] == 4)
                tri5P4_4.Hide();

            // Hide the peg that is jumped
            if (move[1][0] == 0 && move[1][1] == 0)
                tri5P0_0.Hide();
            else if (move[1][0] == 1 && move[1][1] == 0)
                tri5P1_0.Hide();
            else if (move[1][0] == 1 && move[1][1] == 1)
                tri5P1_1.Hide();
            else if (move[1][0] == 2 && move[1][1] == 0)
                tri5P2_0.Hide();
            else if (move[1][0] == 2 && move[1][1] == 1)
                tri5P2_1.Hide();
            else if (move[1][0] == 2 && move[1][1] == 2)
                tri5P2_2.Hide();
            else if (move[1][0] == 3 && move[1][1] == 0)
                tri5P3_0.Hide();
            else if (move[1][0] == 3 && move[1][1] == 1)
                tri5P3_1.Hide();
            else if (move[1][0] == 3 && move[1][1] == 2)
                tri5P3_2.Hide();
            else if (move[1][0] == 3 && move[1][1] == 3)
                tri5P3_3.Hide();
            else if (move[1][0] == 4 && move[1][1] == 0)
                tri5P4_0.Hide();
            else if (move[1][0] == 4 && move[1][1] == 1)
                tri5P4_1.Hide();
            else if (move[1][0] == 4 && move[1][1] == 2)
                tri5P4_2.Hide();
            else if (move[1][0] == 4 && move[1][1] == 3)
                tri5P4_3.Hide();
            else if (move[1][0] == 4 && move[1][1] == 4)
                tri5P4_4.Hide();

            // Show the peg where it landed
            if (move[2][0] == 0 && move[2][1] == 0)
                tri5P0_0.Show();
            else if (move[2][0] == 1 && move[2][1] == 0)
                tri5P1_0.Show();
            else if (move[2][0] == 1 && move[2][1] == 1)
                tri5P1_1.Show();
            else if (move[2][0] == 2 && move[2][1] == 0)
                tri5P2_0.Show();
            else if (move[2][0] == 2 && move[2][1] == 1)
                tri5P2_1.Show();
            else if (move[2][0] == 2 && move[2][1] == 2)
                tri5P2_2.Show();
            else if (move[2][0] == 3 && move[2][1] == 0)
                tri5P3_0.Show();
            else if (move[2][0] == 3 && move[2][1] == 1)
                tri5P3_1.Show();
            else if (move[2][0] == 3 && move[2][1] == 2)
                tri5P3_2.Show();
            else if (move[2][0] == 3 && move[2][1] == 3)
                tri5P3_3.Show();
            else if (move[2][0] == 4 && move[2][1] == 0)
                tri5P4_0.Show();
            else if (move[2][0] == 4 && move[2][1] == 1)
                tri5P4_1.Show();
            else if (move[2][0] == 4 && move[2][1] == 2)
                tri5P4_2.Show();
            else if (move[2][0] == 4 && move[2][1] == 3)
                tri5P4_3.Show();
            else if (move[2][0] == 4 && move[2][1] == 4)
                tri5P4_4.Show();
        }

        private void resetPegs()
        {
            if(cmb_gameSelect.Text == "Triangle, 5 Row")
            {
                resetPegsTri5();
            }
        }

        private void resetPegsTri5()
        {
            tri5P0_0.Hide();
            tri5P1_0.Show();
            tri5P1_1.Show();
            tri5P2_0.Show();
            tri5P2_1.Show();
            tri5P2_2.Show();
            tri5P3_0.Show();
            tri5P3_1.Show();
            tri5P3_2.Show();
            tri5P3_3.Show();
            tri5P4_0.Show();
            tri5P4_1.Show();
            tri5P4_2.Show();
            tri5P4_3.Show();
            tri5P4_4.Show();
        }
    }
}
