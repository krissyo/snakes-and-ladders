﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;

namespace SharedGameClasses {
    /// <summary>
    /// Models a player who is currently located on a particular square 
    /// with a certain amount of money.
    /// </summary>
    public class Player {

        private const int INITIAL_AMOUNT = 100;

        // name of the player
        private string name;
        public string Name {
            get {
                return name;
            }
            set {
                name = value;
            }
        }

        // amount of money owned by player
        private int money;
        public int Money {
            get {
                return money;
            }
            set {
                money = value;
            }
        }

        // current square that player is on
        private Square location; 
        public Square Location {
            get {
                return location;
            }
            set {
                location = value;

            }
        }

        // whether the player is a winner, in the current game.
        private bool winner;
        public bool Winner {
            get {
                return winner;
            }
            set {
                winner = value;
            }
        }

        // PlayerTokenColour and PlayerTokenImage provide colours for the players' tokens (or "pieces"). 
        private Brush playerTokenColour;
        public Brush PlayerTokenColour {
            get {
                return playerTokenColour;
            }
            set {
                playerTokenColour = value;
                playerTokenImage = new Bitmap(1, 1);
                using (Graphics g = Graphics.FromImage(PlayerTokenImage)) {
                    g.FillRectangle(playerTokenColour, 0, 0, 1, 1);
                } 
            }
        }

        private Image playerTokenImage;
        public Image PlayerTokenImage {
            get {
                return playerTokenImage;
            }
        }

        /// <summary>
        /// Parameterless constructor.
        /// Do not want the generic default constructor to be used
        /// as there is no way to set the player's name.
        /// This replaces the compiler's generic default constructor.
        /// Pre:  none
        /// Post: ALWAYS throws an ArgumentException.
        /// </summary>
        /// <remarks>NOT TO BE USED!</remarks>
        public Player() {
            throw new ArgumentException("Parameterless constructor invalid.");
        } // end Player constructor

        /// <summary>
        /// Constructor with initialising parameters.
        /// Pre:  name to be used for this player.
        /// Post: this player object has all attributes initialised
        /// </summary>
        /// <param name="name">Name for this player</param>
        public Player(String name, Square initialLocation) {

            //######################### Code needs to be added here ##########################################
            this.name = name;
            this.location = initialLocation;
            this.money = 0;
            this.winner = false;

        } // end Player constructor

        /// <summary>
        /// Rolls the two dice to determine 
        ///     the number of squares to move forward; and
        ///     moves the player's location along the board; and
        ///     obtains the effect of landing on their final square.
        /// Pre:  dice are initialised
        /// Post: the player is moved along the board and the effect
        ///     of the location the player landed on is applied.
        /// </summary>
        /// <param name="d1">first die</param>
        /// <param name="d2">second die</param>
        public void Play(Die d1, Die d2) {

            //######################### Code needs to be added here ##########################################
            int moves = d1.Roll() + d2.Roll();
            Move(moves);
            

        } // end Play.

        /// <summary>
        /// Moves player the required number of squares forward
        /// Pre:  the number of squares to move forward
        /// Post: the player is moved along the board.
        /// NOTE: Refer to Square.cs regarding the NextSquare property.
        /// </summary>
        /// <param name="numberOfSquares">the number of squares to move</param>
        private void Move(int numberOfSquares) {

            //######################### Code needs to be added here ##########################################3
            int num = Board.Squares[location.Number].Location - numberOfSquares;
            if (num > 41)
            {
                num = 41;
            }
            if (num < 0)
            {
                num = 0;
            }
            location = Board.Squares[Board.Squares[num].Location];
            //location = Board.Squares[num];

            if (location.Name == "BadInvestmentSquare")
            {
                if (money >= 25)
                {
                   money -= 25;
                }
            }
            if (location.Name == "LotteryWinSquare")
            {
                money += 10;
            }
            if (location.Name == "Finish")
            {
                winner = true;
            }

            
        } //end Move

        /// <summary>
        /// Increments the player's money by amount
        /// Pre:  amount > 0
        /// Post: the player's money amount is increased.
        /// </summary>
        /// <param name="amount">increment amount</param>
        public void Credit(int amount) {

            Money = Money + amount;

        } //end Credit


        /// <summary>
        /// Decreases the player's money by amount if 
        ///     the player can afford it; otherwise,
        ///     sets the player's money to 0.
        /// Pre:  amount > 0
        /// Post: player's money is decremented by amount if possible
        ///       but final amount is not below zero
        /// </summary>
        /// <param name="amount">decrement amount</param>
        public void Debit(int amount) {

            //######################### Code needs to be added here ##########################################3

        } //end Debit

        
    } //end class Player
}
