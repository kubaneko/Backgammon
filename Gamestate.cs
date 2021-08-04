using System.Collections.Generic;

namespace Backgammon
{
    public class Gamestate
    {
        /* Class reprezenting the state of the game the methods this class provides does not correct wrong input
         */

        // Playing desk, black stones aare represented by -1 and white ones by 1
        int[] Desk;
        // nuber of disks removed by the opponent
        int BBar = 0;
        int WBar = 0;
        // Score of the players
        int WScore = 0;
        int BScore = 0;
        // Is equal to one on White players turn and -1 on Black Players Turn
        int Color = 1;
        // constants of the game, MAXTILE is maximum tile to be able to play to or from
        const int MAXTILE = 23;
        const int MAXSCORE = 15;

        public Gamestate(int[] desk, int bBar, int wBar, int wScore, int bscore, int color)
        {
            Desk = desk;
            BBar = bBar;
            WBar = wBar;
            WScore = wScore;
            BScore = bscore;
            Color = color;
        }

        public Gamestate(int color)
        {
            Color = color;
            // standard way starting point on the desk
            Desk = new int[MAXTILE+1] { 2, 0, 0, 0, 0, -5, 0, -3, 0, 0, 0, 5, -5, 0, 0, 0, 3, 0, 5, 0, 0, 0, 0, -2 };
            BBar = 0;
            WBar = 0;
            WScore = 0;
            BScore = 0;
        }

        // returns a concrete tile
        public int GetTile(int x)
        {
            return Desk[x];
        }

        // returns whose turn it is
        public int GetColor()
        {
            return Color;
        }

        // Changes the current player in turn
        public void Turn()
        {
            Color *= -1;
        }

        // returns color of the player who won or null
        public int? Won()
        {
            if (WScore == MAXSCORE)
            {
                return 1;
            }
            if (BScore == MAXSCORE)
            {
                return -1;
            }
            return null;
        }

        // returns score of player whose turn it is 
        public int GetScore()
        {
            if (Color == 1)
            {
                return WScore;
            }
            return BScore;
        }


        // Getters for scores and bars
        public int GetWBar()
        {
            return WBar;
        }
        public int GetBBar()
        {
            return BBar;
        }
        public int GetBScore()
        {
            return BScore;
        }
        public int GetWScore()
        {
            return WScore;
        }

        // Returns whether you have to play from bar, if possible, or not
        public bool BarEmpty()
        {
            if (Color == 1)
            {
                return WBar == 0;
            }
            else
            {
                return BBar == 0;
            }
        }

        // Returns if the player whose turn it is can score
        public bool HomeRowFull()
        {
            int DisksInHome = 0;
            int index = ((MAXTILE + 1) - Color) % (MAXTILE + 2);
            // Index of last Tile for the player in his Home Row
            for (int i = 0; i < 6; ++i)
            {
                if (Desk[index] * Color > 0)
                    DisksInHome += Color * Desk[index];
                index -= Color;
            }
            return DisksInHome == MAXSCORE - GetScore();
        }

        // returns whether there are stones before the stone in the homerow
        // used to calculate whether you can use a greater number on the dice to score
        public bool IsLastInHomerow(int stone)
        {
            int LastIndex = ((MAXTILE + 1) - Color) % (MAXTILE + 2) - 5 * Color;
            for (int i = LastIndex; i * Color < stone * Color; i += Color)
            {
                if (Desk[i] * Color > 0)
                {
                    return false;
                }
            }
            return true;
        }
        // removes the stone from its tile and lets the function MoveTo move it
        public void Move(int From, int To)
        {
            Desk[From] -= Color;
            MoveTo(To);
        }
        // Scores for the player in turn
        public void MoveToScore(int From)
        {
            Desk[From] -= Color;
            if (Color == 1)
            {
                ++WScore;
            }
            else
            {
                ++BScore;
            }
        }
        // removes the stone from the bar and lets the function MoveTo move it
        public void MoveFromBar(int To)
        {
            if (Color == 1)
            {
                --WBar;
            }
            else
            {
                --BBar;
            }
            MoveTo(To);
        }
        // The function handles removing enemy stones from play and moving your stones
        void MoveTo(int To)
        {
            if (Desk[To] * Color < 0)
            {
                Desk[To] += 2 * Color;
                if (Color == 1)
                {
                    ++BBar;
                }
                else
                {
                    ++WBar;
                }
            }
            else
            {
                Desk[To] += Color;
            }
        }
    }
}
