using System;
using System.Collections.Generic;
using System.Linq;

namespace Backgammon
{
    public class Game
    {
        /* Class in charge of the logic of the game
         * Bar is before the first tile for the player
         * Score is after the last one
         */

        // Selected Tile
        int? Selected;
        // Tiles that can be selected next
        HashSet<int> NextMoves = new HashSet<int>();
        // Has the player roll dice in this turn
        bool rolled = false;
        // the dice
        Random r = new Random();
        // number of moves in a double left
        int Double = 0;
        // if the dice are not a double they are nulled after being used
        int? dice1;
        int? dice2;
        // Win Conditions
        bool BlackWon = false;
        bool WhiteWon = false;
        // constants
        const int MAXTILE = 23;

        // Returns if the game is over
        public bool GameOver()
        {
            return BlackWon || WhiteWon;
        }

        // Determines what player starts the game in turn
        public int NewToPlay()
        {
            int i = r.Next(0, 2);
            if (i == 1)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
        // sets who won by color
        public void SetResult(int? color)
        {
            if (color == null)
            {
                return;
            }
            if (color == 1)
            {
                WhiteWon = true;

            }
            else
            {
                BlackWon = true;
            }
        }
        // getters for the players winning conditions
        public bool GetWhiteWon()
        {
            return WhiteWon;
        }

        public bool GetBlackWon()
        {
            return BlackWon;
        }

        // Rolls the dice

        public bool Roll()
        {
            bool roll = !rolled;
            if (!rolled)
            {
                dice1 = r.Next(1, 7);
                dice2 = r.Next(1, 7);
                rolled = true;
                if (dice1 == dice2)
                {
                    Double = 4;
                }
            }
            return roll;
        }

        // Getters for dice
        public int? GetDice1()
        {
            return dice1;
        }
        public int? GetDice2()
        {
            return dice2;
        }

        // Tells whether there are moves remaining
        public bool RemainMoves()
        {
            return ((dice1 != null || dice2 != null) && !(dice1 != null && dice2 != null && dice1 == dice2 && Double == 0) && NextMoves.Count() > 0);
        }
        // resets for next turn
        public void Turn()
        {
            ClearNext();
            rolled = false;
            Double = 0;
        }

        // Returns if a move is valid
        public bool ValidMoveTo(Gamestate game, int from, int to)
        {
            if (to > MAXTILE || to < 0)
            {
                // Scoring or moving stones from bar
                if (game.HomeRowFull())
                {
                    if (game.IsLastInHomerow(from))
                    {
                        return true;
                    }
                    else
                    {
                        if (to == MAXTILE + 1 || to == -1)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            // moves within the board
            if (game.GetTile(to) * game.GetColor() + 2 > 0)
            {
                return true;
            }
            return false;
        }
        public void GenNextMoves(Gamestate gamestate)
        {
            int color = gamestate.GetColor();
            int firstindex = ((MAXTILE + 1) + color) % (MAXTILE + 2);
            // First Tile for the player (after bar)
            int lastindex = ((MAXTILE + 1) - color) % (MAXTILE + 2);
            ClearNext();
            if (Selected == null)
            {
                // Then we have to generate tiles that can be played from (there is a dice move from them)
                if (gamestate.BarEmpty())
                {
                    // we can play however we want
                    for (int i = firstindex; i * color < (lastindex + color) * color; i += color)
                    {
                        if (gamestate.GetTile(i) * color > 0)
                        {
                            if (ExistsDiceMove(i, gamestate))
                            {
                                NextMoves.Add(i);
                            }
                        }
                    }
                }
                else
                {
                    // we have to play from bar
                    if (ExistsDiceMove(firstindex - color, gamestate))
                    {
                        NextMoves.Add(firstindex - color);
                    }
                }
            }
            else
            {
                // gets dice moves from selected there always should be some
                GetDiceMoves((int)Selected, gamestate, NextMoves);
            }
        }
        public void PlayValidTo(int to, Gamestate state)
        {
            // Plays a valid move from selected to To
            int color = state.GetColor();
            int firstindex = ((MAXTILE + 1) + color) % (MAXTILE + 2);
            int lastindex = ((MAXTILE + 1) - color) % (MAXTILE + 2);
            // if the moves is to score
            if (color * to > lastindex * color)
            {
                state.MoveToScore((int)Selected);
                // checks whether the gameresult changed
                SetResult(state.Won());
            }
            else
            {
                // checks whether selected is the bar
                if ((firstindex - color) * color < (int)Selected * color)
                {
                    state.Move((int)Selected, to);

                }
                else
                {
                    state.MoveFromBar(to);

                }
            }
            // uses the dice used for the throw
            NullDice((int)Selected, to, color);
        }

        // nulls the dice responsible for the throw or subtracts from double
        void NullDice(int From, int To, int color)
        {
            if (Double == 0)
            {
                if (dice1 == null)
                {
                    dice2 = null;
                    return;
                }
                if (dice2 == null)
                {
                    dice1 = null;
                    return;
                }
                if ((To - From) * color == dice1)
                {
                    dice1 = null;
                    return;
                }
                if ((To - From) * color == dice2)
                {
                    dice2 = null;
                    return;
                }
                if (dice1 > dice2)
                {
                    dice1 = null;
                    return;
                }
                dice2 = null;
                return;

            }
            Double -= 1;
            return;
        }


        // returns whether there is a valid dice move
        bool ExistsDiceMove(int From, Gamestate gamestate)
        {
            if (dice1 != null && ValidMoveTo(gamestate, From, From + (int)dice1 * gamestate.GetColor()))
            {
                return true;
            }
            if (dice2 != null && ValidMoveTo(gamestate, From, From + (int)dice2 * gamestate.GetColor()))
            {
                return true;
            }
            return false;
        }
        // sets NextMoves valid dice moves from selected
        void GetDiceMoves(int From, Gamestate gamestate, HashSet<int> moves)
        {
            if (dice1 != null && ValidMoveTo(gamestate, From, From + (int)dice1 * gamestate.GetColor()))
            {
                moves.Add(NormalizeMove(From + (int)dice1 * gamestate.GetColor()));
            }
            if (Double == 0 && dice2 != null && ValidMoveTo(gamestate, From, From + (int)dice2 * gamestate.GetColor()))
            {
                moves.Add(NormalizeMove(From + (int)dice2 * gamestate.GetColor()));
            }
        }
        // Makes the move be from MAXTILE+1 to MINTILE-1
        int NormalizeMove(int i)
        {
            if (i > MAXTILE)
            {
                return MAXTILE + 1;
            }
            if (i < 0)
            {
                return -1;
            }
            return i;
        }

        // resets for new game
        public void Reset()
        {
            rolled = false;
            dice1 = null;
            dice2 = null;
            BlackWon = false;
            WhiteWon = false;
        }
        // Some getters and setters
        public HashSet<int> GetNextMoves()
        {
            return NextMoves;
        }
        public void ClearNext()
        {
            NextMoves.Clear();
        }
        public int? GetSelected()
        {
            return Selected;
        }
        public void SetSelected(int? i)
        {
            Selected = i;
        }
        public bool GetRolled()
        {
            return rolled;
        }
        public int GetDouble()
        {
            return Double;
        }
        // used to disable rolling if the game ended
        public void SetRolled(bool Rolled)
        {
            rolled = Rolled;
        }
    }
}
