using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backgammon
{
    public class Gamestate
    {
        int[] Desk = new int[24];
        int BBar = 0;
        int WBar = 0;
        int WScore=0;
        int BScore=0;
        int Color = 1;
        // Is equal to one on White players turn and -1 on Black Players Turn

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
            Desk = new int[24] { 2,0,0,0,0,-5,0,-3,0,0,0,5,-5,0,0,0,3,0,5,0,0,0,0,-2 };
            //Desk = new int[24] { -15,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,15 };
            BBar = 0;
            WBar = 0;
            WScore = 0;
            BScore = 0;
        }

        public Gamestate Copy()
        {
            return new Gamestate(Desk, BBar, WBar, WScore, BScore, Color);
        }

        public int GetScore()
        {
            if (Color==1)
            {
                return WScore;
            }
            return BScore;
        }
        public bool IsLastInHomerow(int from)
        {
            int LastIndex = (24 - Color )% 25-5*Color;
            for (int i = LastIndex; i * Color < from * Color; i += Color)
            {
                if (Desk[i] * Color > 0)
                {
                    return false;
                }
            }
            return true;
        }
        public bool HomeRowFull() 
        {
            int DisksInHome = 0;
            int index = (24 - Color) % 25;
            // Index of last Tile for the player in his Home Row
            for (int i = 0; i < 6; ++i)
                {
                    if (Desk[index]*Color > 0)
                        DisksInHome += Color*Desk[index];
                index -= Color;
                }
            return DisksInHome == 15-GetScore();
        }

        public void Score(int From)
        {
            Desk[From] -= Color;
            if (Color==1)
            {
                ++WScore;
            }
            else
            {
                ++BScore;
            }
        }
        public int GetTile(int x)
        {
            return Desk[x];
        }
        public void Move(int From,int To)
        {
            Desk[From] -= Color;
            MoveTo(To);
        }
        public void MoveFromBar(int To)
        {
            int color = GetColor();
            if (Color==1)
            {
                --WBar;
            }
            else
            {
                --BBar;
            }
            MoveTo(To);
        }
        void MoveTo(int To)
        {
            if (Desk[To] * Color < 0)
            {
                Desk[To] += 2 * Color;
                if (Color==1)
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
        public int GetColor()
        {
            return Color;
        }

        public bool BarEmpty()
        {
            if (Color==1)
            {
                return WBar==0;
            }
            else
            {
                return BBar == 0;
            }
        }
        public void Turn()
        {
            Color *= -1;
        }

        public override bool Equals(object obj)
        {
            return obj is Gamestate gamestate &&
                   EqualityComparer<int[]>.Default.Equals(Desk, gamestate.Desk) &&
                   BBar == gamestate.BBar &&
                   WBar == gamestate.WBar &&
                   Color == gamestate.Color;
        }

        public override int GetHashCode()
        {
            int hashCode = 52350545;
            hashCode = hashCode * -1521134295 + EqualityComparer<int[]>.Default.GetHashCode(Desk);
            hashCode = hashCode * -1521134295 + BBar.GetHashCode();
            hashCode = hashCode * -1521134295 + WBar.GetHashCode();
            hashCode = hashCode * -1521134295 + Color.GetHashCode();
            return hashCode;
        }
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
        public int? Won()
        {
            if (WScore==15){
                return 1;
            }
            if (BScore == 15)
            {
                return -1;
            }
            return null;
        }
    }
}
