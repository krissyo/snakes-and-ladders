using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Drawing;

using System.ComponentModel;  // for BindingList.

namespace SharedGameClasses {
    /// <summary>
    /// Plays a game called Hare and the Tortoise
    /// </summary>
    public static class HareAndTortoiseGame {
        
        // Minimum and maximum players per game
        private const int MIN_PLAYERS = 2;
        public const int MAX_PLAYERS = 6;

        // The dice
        private static Die die1 = new Die(), die2 = new Die();

        // A BindingList is like an array that can grow and shrink. 
        // 
        // Using a BindingList will make it easier to implement the GUI with a DataGridView
        private static BindingList<Player> players = new BindingList<Player>();
        public static BindingList<Player> Players {
            get {
                return players;
            }
        }

        
        private static int numberOfPlayers = 6;  // The value 6 is purely to avoid compiler errors.

        public static int NumberOfPlayers {
            get {
                return numberOfPlayers;
            }
            set {
                numberOfPlayers = value;
            }
        }

        // Is the current game finished?
        private static bool finished = false;
        public static bool Finished {
            get {
                return finished;
            }
        }

        /// Some default player names.  
        /// 
        /// These are purely for testing purposes and when initialising the players at the start
        /// 
        /// These values are intended to be read-only.  I.e. the program code should never update this array.
        private static string[] defaultNames = { "One", "Two", "Three", "Four", "Five", "Six" };

        // Some colours for the players' tokens (or "pieces"). 
        private static Brush[] playerTokenColours = new Brush[MAX_PLAYERS] { Brushes.Black, Brushes.Red, 
                                                              Brushes.Gold, Brushes.GreenYellow, 
                                                              Brushes.Fuchsia, Brushes.White };

                

        /// <summary>
        /// Initialises each of the players and adds them to the players BindingList.
        /// This method is called only once, when the game first startsfrom HareAndTortoiseForm.
        ///
        /// Pre:  none.
        /// Post: all the game's players are initialised.
        /// </summary>
        public static void InitialiseAllThePlayers() {
 
            for (int i = 0; i < HareAndTortoiseGame.numberOfPlayers; i++)
            {
                
                // creating a player and assigning a value
                Player player = new Player(defaultNames[i],Board.StartSquare);
                player.PlayerTokenColour = playerTokenColours[i];
                // adds player to list of players
                HareAndTortoiseGame.Players.Add(player);

            }
        } // end InitialiseAllThePlayers


        /// <summary>
        /// Puts all the players on the Start square.
        /// Pre:  none.
        /// Post: the game is reset as though it is being played for the first time.
        /// </summary>
        public static void SetPlayersAtTheStart()
        {

            for (int i = 0; i < HareAndTortoiseGame.numberOfPlayers; i++)
            {
                HareAndTortoiseGame.Players[i].Location = Board.Squares[36];
            }

        } // end SetPlayersAtTheStart
    } //end class HareAndTortoiseGame
}
