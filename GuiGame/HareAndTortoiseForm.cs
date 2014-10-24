using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using System.Diagnostics;

using SharedGameClasses;

namespace GuiGame {

    /// <summary>
    /// The form that displays the GUI of the game named HareAndTortoise.
    /// </summary>
    public partial class HareAndTortoiseForm : Form {

        // Specify the numbers of rows and columns on the screen
        const int NUM_OF_ROWS = 7;
        const int NUM_OF_COLUMNS = 6;
        // current player in the roll loop
        private int current_player = 0;

        // When we update what's on the screen, we show the movement of players 
        // by removing them from their old squares and adding them to their new squares.
        // This enum makes it clearer that we need to do both.
        enum TypeOfGuiUpdate { AddPlayer, RemovePlayer };

        //private int players = new BindingList<Player>();
        /// <summary>
        /// Constructor with initialising parameters.
        /// Pre:  none.
        /// Post: the form is initialised, ready for the game to start.
        /// </summary>
        public HareAndTortoiseForm() {
            InitializeComponent();
            HareAndTortoiseGame.NumberOfPlayers = HareAndTortoiseGame.MAX_PLAYERS; // Max players, by default.
            Board.SetUpBoard();
            HareAndTortoiseGame.InitialiseAllThePlayers();
            SetupTheGui();
            ResetGame();
        }

        /// <summary>
        /// Set up the GUI when the game is first displayed on the screen.
        /// 
        /// This method is almost complete. It should only be changed by adding one line:
        ///     to set the initial ComboBox selection to "6"; 
        /// 
        /// Pre:  the form contains the controls needed for the game.
        /// Post: the game is ready for the user(s) to play.
        /// </summary>
        private void SetupTheGui() {
            CancelButton = exitButton;  // Allow the Esc key to close the form.
            ResizeGameBoard();
            SetupGameBoard();

            //####################### set intitial ComboBox Seletion to 6 here ####################################
            
            SetupPlayersDataGridView();
              
        }// end SetupTheGui


        /// <summary>
        /// Resizes the entire form, so that the individual squares have their correct size, 
        /// as specified by SquareControl.SQUARE_SIZE.  
        /// This method allows us to set the entire form's size to approximately correct value 
        /// when using Visual Studio's Designer, rather than having to get its size correct to the last pixel.
        /// Pre:  none.
        /// Post: the board has the correct size.
        /// </summary>
        private void ResizeGameBoard() {

            const int SQUARE_SIZE = SquareControl.SQUARE_SIZE;
            int currentHeight = boardTableLayoutPanel.Size.Height;
            int currentWidth = boardTableLayoutPanel.Size.Width;
            int desiredHeight = SQUARE_SIZE * NUM_OF_ROWS;
            int desiredWidth = SQUARE_SIZE * NUM_OF_COLUMNS;
            int increaseInHeight = desiredHeight - currentHeight;
            int increaseInWidth = desiredWidth - currentWidth;
            this.Size += new Size(increaseInWidth, increaseInHeight);
            boardTableLayoutPanel.Size = new Size(desiredWidth, desiredHeight);
        }

        /// <summary>
        /// Creates each SquareControl and adds it to the boardTableLayoutPanel that displays the board.
        /// Pre:  none.
        /// Post: the boardTableLayoutPanel contains all the SquareControl objects for displaying the board.
        /// </summary>
        private void SetupGameBoard() {
            
            // ########################### Code needs to be written to perform the following  ###############################################
            /*
             *   taking each Square of Baord separately create a SquareContol object containing that Square (look at the Constructor for SquareControl)
             *   
             *   when it is either the Start Square or the Finish Square set the BackColor of the SquareControl to BurlyWood
             *   
             *   DO NOT set the BackColor of any other Square Control 
             * 
             *   Call MapSquareNumtoScreenRowAnd Column  to determine the row and column position of the SquareControl on the TableLayoutPanel
             *   
             *   Add the Control to the TaleLayoutPanel
             * 
             */
            //HareAndTortoiseGame.InitialiseAllThePlayers();
            int row = 0;
            int col = 0;
            int n = Board.Squares.Length - NUM_OF_COLUMNS ;
            SquareControl[] squarecontrol = new SquareControl[42];
            for (int i = Board.Squares.Length-1; i >= 0; i--)
            {
                // making a new square control to display the squares
                squarecontrol[i] = new SquareControl(Board.Squares[n], HareAndTortoiseGame.Players);
                // setting the current squares location
                Board.Squares[i].Location = n;

                // adding square control to layout panel 
                boardTableLayoutPanel.Controls.Add(squarecontrol[i]);
                // calculate n which is the location of the square
                row++;
                if (col % 2 == 0)
                {
                    n++;
                }
                else
                {
                    n--;
                }
                if (row > NUM_OF_COLUMNS-1)
                {
                    if (n == 42)
                    {
                        n = 35;
                    }
                    else if (n == 29)
                    {
                        n = 24;
                    }
                    else if (n == 30)
                    {
                        n = 23;
                    }
                    else if (n == 17)
                    {
                        n = 12;
                    }
                    else if(n==18){
                        n = 11;
                    }
                    else if (n == 5)
                    {
                        n = 0;
                    }
                    row = 0;
                    col++;
                }
            }
            ResetGame();
        }// SetupGameBaord


        /// <summary>
        /// Tells the players DataGridView to get its data from the hareAndTortoiseGame.Players BindingList.
        /// Pre:  players DataGridView exists on the form.
        ///       HareAndTortoiseGame.Players BindingList is not null.
        /// Post: players DataGridView displays the correct rows and columns.
        /// </summary>
        private void SetupPlayersDataGridView()
        {

            // binding player class to data grid
            dataGridView.DataSource = HareAndTortoiseGame.Players;
        }


        /// <summary>
        /// Resets the game, including putting all the players on the Start square.
        /// This requires updating what is displayed in the GUI, 
        /// as well as resetting the attrtibutes of HareAndTortoiseGame .
        /// This method is used by both the Reset button and 
        /// when a new value is chosen in the Number of Players ComboBox.
        /// Pre:  none.
        /// Post: the form displays the game in the same state as when the program first starts 
        ///       (except that any user names that the player has entered are not reset).
        /// </summary>
        private void ResetGame() {

            // removes players from the board
            ResetPlayersInfoInDataGridView();
            UpdatePlayersGuiLocations(TypeOfGuiUpdate.RemovePlayer);
            // places players location at the start of the board
            HareAndTortoiseGame.SetPlayersAtTheStart();
            // resets game controls
            groupBox1.Visible = true;
            // adds all players the board
            UpdatePlayersGuiLocations(TypeOfGuiUpdate.AddPlayer);
        }


        /// <summary>
        /// At several places in the program's code, it is necessary to update the GUI board,
        /// so that player's tokens (or "pieces") are removed from their old squares
        /// or added to their new squares. E.g. when all players are moved back to the Start.
        /// 
        /// For each of the players, this method is to use the GetSquareNumberOfPlayer method to find out 
        /// which square number the player is on currently, then use the SquareControlAt method
        /// to find the corresponding SquareControl, and then update that SquareControl so that it
        /// knows whether the player is on that square or not.
        /// 
        /// Moving all players from their old to their new squares requires this method to be called twice: 
        /// once with the parameter typeOfGuiUpdate set to RemovePlayer, and once with it set to AddPlayer.
        /// In between those two calls, the players locations must be changed by using one or more methods 
        /// in the HareAndTortoiseGame class. Otherwise, you won't see any change on the screen.
        /// 
        /// Because this method moves ALL players, it should NOT be used when animating a SINGLE player's
        /// movements from square to square.
        /// 
        /// 
        /// Post: the GUI board is updated to match the locations of all Players objects.
        /// </summary>
        /// <param name="typeOfGuiUpdate">Specifies whether all the players are being removed 
        /// from their old squares or added to their new squares</param>
        private void UpdatePlayersGuiLocations(TypeOfGuiUpdate typeOfGuiUpdate)
        {

            // stores current square player is in
            int squarenum;
            SquareControl sq;
            for (int i = 0; i < HareAndTortoiseGame.NumberOfPlayers; i++)
            {
                squarenum = GetSquareNumberOfPlayer(i);
                sq = SquareControlAt(squarenum);
                
                // remove player from SquareControl
                if (typeOfGuiUpdate == TypeOfGuiUpdate.RemovePlayer)
                {
                    sq.ContainsPlayers[i] = false;
                }
                // adds player to SquareControl
                else if (typeOfGuiUpdate == TypeOfGuiUpdate.AddPlayer)
                {
                    sq.ContainsPlayers[i] = true;
                }
            }
            RefreshBoardTablePanelLayout(); // Must be the last line in this method. DO NOT put it inside a loop.
        }// end UpdatePlayersGuiLocations



        /*** START OF LOW-LEVEL METHODS *****************************************************************************
         * 
         *   The methods in this section are "helper" methods that can be called to do basic things. 
         *   That makes coding easier in other methods of this class.
         *   You should NOT CHANGE these methods, except where otherwise specified in the assignment. 
         *   
         *   ********************************************************************************************************/

        /// <summary>
        /// When the SquareControl objects are updated (when players move to a new square),
        /// the board's TableLayoutPanel is not updated immediately.  
        /// Each time that players move, this method must be called so that the board's TableLayoutPanel 
        /// is told to refresh what it is displaying.
        /// Pre:  none.
        /// Post: the board's TableLayoutPanel shows the latest information 
        ///       from the collection of SquareControl objects in the TableLayoutPanel.
        /// </summary>
        private void RefreshBoardTablePanelLayout(){
            boardTableLayoutPanel.Invalidate(true);
        }//end RefreshBoardTablePanelLayout


        /// <summary>
        /// When the Player objects are updated (location, money, etc.),
        /// the players DataGridView is not updated immediately.  
        /// Each time that those player objects are updated, this method must be called 
        /// so that the players DataGridView is told to refresh what it is displaying.
        /// Pre:  none.
        /// Post: the players DataGridView shows the latest information 
        ///       from the collection of Player objects in the HareAndTortoiseGame.
        /// </summary>
        private void RefreshPlayersInfoInDataGridView() {
            HareAndTortoiseGame.Players.ResetBindings();
        } //end RefreshPlayersInfoInDataGridView

        /// <summary>
        /// When the game ends, the data in the view needs to all reset back
        /// to 0
        /// Pre:  none.
        /// Post: the players DataGridView shows reset stats.
        /// </summary>
        private void ResetPlayersInfoInDataGridView()
        {
            for (int i = 0; i < HareAndTortoiseGame.NumberOfPlayers; i++)
            {
                HareAndTortoiseGame.Players[i].Money = 0;
                HareAndTortoiseGame.Players[i].Winner = false;
            }
            RefreshPlayersInfoInDataGridView();
        } //end ResetPlayersInfoInDataGridView

        /// <summary>
        /// Tells you the current square number that a given player is on.
        /// Pre:  a valid playerNumber is specified.
        /// Post: returns the square number of the square the player is on.
        /// </summary>
        /// <param name="playerNumber">The player number.</param>
        /// <returns>Returns the square number of the playerNumber.</returns>
        private int GetSquareNumberOfPlayer(int playerNumber)
        {
            Square playerSquare = HareAndTortoiseGame.Players[playerNumber].Location;
            return playerSquare.Number;
        } //end GetSquareNumberOfPlayer


        /// <summary>
        /// Tells you which SquareControl object is associated with a given square number.
        /// Pre:  a valid squareNumber is specified; and
        ///       the boardTableLayoutPanel is properly constructed.
        /// Post: the SquareControl object associated with the square number is returned.
        /// </summary>
        /// <param name="squareNumber">The square number.</param>
        /// <returns>Returns the SquareControl object associated with the square number.</returns>
        private SquareControl SquareControlAt(int squareNumber) {
            int rowNumber;
            int columnNumber;
            MapSquareNumToScreenRowAndColumn(squareNumber, out rowNumber, out columnNumber);

            
            return (SquareControl) boardTableLayoutPanel.GetControlFromPosition(columnNumber, rowNumber);

            
        } //end SquareControlAt


        /// <summary>
        /// For a given square number, tells you the corresponding row and column numbers.
        /// Pre:  none.
        /// Post: returns the row and column numbers, via "out" parameters.
        /// </summary>
        /// <param name="squareNumber">The input square number.</param>
        /// <param name="rowNumber">The output row number.</param>
        /// <param name="columnNumber">The output column number.</param>
        private static void MapSquareNumToScreenRowAndColumn(int squareNumber, out int rowNumber, out int columnNumber) {

            // ######################## Add more code to this method and replace the next two lines by something more sensible.  ###############################
            rowNumber = 0;      // Use 0 to make the compiler happy for now.
            columnNumber = 0;   // Use 0 to make the compiler happy for now.
            for (int i = 0; i < squareNumber; i++)
            {
                columnNumber++;
                if (columnNumber == NUM_OF_COLUMNS)
                {
                    rowNumber++;
                    columnNumber = 0;
                }
            }

        }//end MapSquareNumToScreenRowAndColumn

        /*** END OF LOW-LEVEL METHODS **********************************************/



        /*** START OF EVENT-HANDLING METHODS ***/
        // ####################### Place EVENT HANDLER Methods to this area of code  ##################################################
        /// <summary>
        /// Handle the Exit button being clicked.
        /// Pre:  the Exit button is clicked.
        /// Post: the game is closed.
        /// </summary>
        private void exitButton_Click(object sender, EventArgs e)
        {
            // Terminate immediately, rather than calling Close(), 
            // so that we don't have problems with any animations that are running at the same time.
            Environment.Exit(0);  
        }

        private Die die1 = new Die(1);
        private Die die2 = new Die(1);

        // handles the button click of Roll Dice
        private void btnRollDice_Click(object sender, EventArgs e)
        {
            // removes all players from the board
            UpdatePlayersGuiLocations(TypeOfGuiUpdate.RemovePlayer);
            for (int i = 0; i < HareAndTortoiseGame.NumberOfPlayers; i++)
            {
                HareAndTortoiseGame.Players[i].Play(die1, die2);
            } 
            // adds all players back to the board
            UpdatePlayersGuiLocations(TypeOfGuiUpdate.AddPlayer);
            // updates the data grid view
            RefreshPlayersInfoInDataGridView();
        }
        
        // handles the button click of Reset Game 
        private void button1_Click(object sender, EventArgs e)
        {
            ResetGame();
        }

        // handles the drop down and selection of the number of players 
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePlayersGuiLocations(TypeOfGuiUpdate.RemovePlayer);
            HareAndTortoiseGame.NumberOfPlayers = int.Parse( comboBox1.SelectedItem.ToString());
            ResetGame();
        }

        // handles the button click of Click Next Player's Roll 
        private void btnNextRoll_Click(object sender, EventArgs e)
        {
            UpdatePlayersGuiLocations(TypeOfGuiUpdate.RemovePlayer);
            HareAndTortoiseGame.Players[current_player].Play(die1, die2);
            // goes to the next players turn
            current_player++;
            // goes to the first players turn 
            if (current_player >= HareAndTortoiseGame.NumberOfPlayers)
            {
                current_player = 0;
            }
            // after a roll this updates the players location on the board
            UpdatePlayersGuiLocations(TypeOfGuiUpdate.AddPlayer);
            RefreshPlayersInfoInDataGridView();
        }
        
        // handles the Single Step radio button: Yes
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            btnRollDice.Enabled = false;
            btnNextRoll.Enabled = true;

            ResetGame();
            groupBox1.Visible = false;
        }

        // handles the Single Step radio button: No
        private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
        {
            btnRollDice.Enabled = true;
            btnNextRoll.Enabled = false;

            ResetGame();
            groupBox1.Visible = false;
        }

        private void splitContainer_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }



        /*** END OF EVENT-HANDLING METHODS ***/
    }
}

