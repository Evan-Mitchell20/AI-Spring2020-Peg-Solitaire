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
        public frm_menu()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CenterToScreen();

            // Code below just for debug, will need to be moved
            GameState theGame = TriangleGames.BasicTriangle(5);
            //List<List<List<int>>> nextMoves = theGame.nextMoves();
            //GameState newState = theGame.nextState(nextMoves[0]);
            //List<GameState> successors = theGame.getSuccessors();
            DepthFirstAgent depthFirstAgent = new DepthFirstAgent(theGame);
            BreadthFirstAgent breadthFirstAgent = new BreadthFirstAgent(theGame);
            List<List<List<int>>> movesToWin = depthFirstAgent.Solve();
            //List<List<List<int>>> movesToWin = breadthFirstAgent.Solve();
        }
    }
}
