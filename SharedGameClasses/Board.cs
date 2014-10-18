﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharedGameClasses {
    /// <summary>
    /// Models a game board consisting of different types of squares
    /// </summary>
    public static class Board {

        public const int NUMBER_OF_SQUARES = 40;  // Value doesn't include start or finish squares.
        public const int START_SQUARE_NUMBER = 0;
        public const int FINISH_SQUARE_NUMBER = NUMBER_OF_SQUARES-1;

        private static Square[] squares = new Square[NUMBER_OF_SQUARES + 2];  // The array of squares is the Board
        public static Square[] Squares {
            get {
                return squares;
            }
        }

        public static Square StartSquare {
            get {
                return squares[START_SQUARE_NUMBER];
            }
        }
     
        /// <summary>
        /// 
        /// Initialises a board consisting of a mix of Ordinary Squares,
        ///     Bad Investment Squares and Lottery Win Squares.
        /// The board has two 'non-board' squares:
        ///     a start square; and
        ///     a finish square.
        ///     This is to comply with the Hare and Tortoise requirements.
        /// The start square is to be used for initialisation, play is not yet on the board.
        /// The finish square is to be used for termination, players cannot move past this square.
        /// Pre:  none
        /// Post: board is constructed with each 
        /// </summary>
        public static void SetUpBoard() {
            // Create the start square
            Board.squares[0] = new Square(0, "Start");
           
            // Create the 40 squares which make up the board
            // some of the squares will be LotteryWinSquares, 
            // others will be BadInvestmentsSquares 
            // with most being just ordinary squares
            //
            for (int i = 1; i <= 40; i++)
            {

                if (i == 40 || i == 30 || i ==20 || i == 10)
                {
                    Board.squares[i] = new Square(i, "LotteryWinSquare");
                    
                }
                else if (i == 35 || i == 25 || i == 15 || i == 5)
                {
                    Board.squares[i] = new Square(i, "BadInvestmentSquare");
                    
                }
                else
                {
                    squares[i] = new Square(i, "Ordinary");
                    
                }

            }
            //Create the finish square
            squares[41] = new Square(41, "Finish");
            
        } // end SetUpBoard

    } //end class Board
}