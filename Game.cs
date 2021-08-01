﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backgammon
{
    public class Game
    {
        protected int? Selected;
        protected HashSet<int> NextMoves=new HashSet<int>();
        protected bool rolled = false;
        protected Random r = new Random();
        protected bool computer;
        protected int Double = 0;
        protected int? dice1;
        protected int? dice2;
        protected bool Gameover = false;

        public bool Computer { get => computer; set => computer = value; }
        public bool Gameover1 { get => Gameover; set => Gameover = value; }

        public bool Roll()
        {
            bool roll = !rolled;
            if (!rolled)
            {
                dice1 = r.Next(1, 6);
                dice2 = r.Next(1, 6);
                rolled = true;
                if (dice1 == dice2)
                {
                    Double = 4;
                }
            }
            return roll;
        }

        public int? GetDice1()
        {
            return dice1;
        }
        public int? GetDice2()
        {
            return dice2;
        }

        public bool ValidMoveTo(Gamestate game, int to)
        {
            if (to > 23 || to < 0)
            {
                if (game.HomeRowFull())
                {
                    if (game.IsLastInHomerow(to))
                    {
                        return true;
                    }
                    else
                    {
                        if (to == 24 || to == -1)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            if (game.GetTile(to) * game.GetColor() + 2 > 0)
            {
                return true;
            }
            return false;
        }
        public void GenNextMoves(Gamestate gamestate)
        {
            int color = gamestate.GetColor();
            int firstindex = (24 + color) % 25;
            int lastindex= (24 - color) % 25;
            ClearNext();
            // First Tile for the player (after bar)
            if (Selected == null)
            {
                if (gamestate.BarEmpty())
                {
                    for (int i=firstindex;i*color<(lastindex+ color)*color; i += color)
                    {
                        if (gamestate.GetTile(i)*color>0)
                        {
                            if (ExistsDiceMove(i, gamestate) && color * lastindex > i * color)
                            {
                                NextMoves.Add(i);
                            }
                            else
                            {
                                NextMoves.Add(lastindex + color);
                            }
                        }
                    }
                }
                else
                {
                    if (ExistsDiceMove(firstindex-color, gamestate))
                    {
                        NextMoves.Add(firstindex - color);
                    }
                }
            }
            else
            {
                GetDiceMoves((int)Selected, gamestate, NextMoves);
            }
        }
        public void PlayValidTo(int to,Gamestate state)
        {
            int color = state.GetColor();
            int firstindex = (24 + color) % 25;
            int lastindex = (24 - color) % 25;
            if (color*to>lastindex)
            {
                state.Score((int)Selected);
            }
            else
            {
                if ((firstindex - color) * color < to)
                {
                    state.Move((int)Selected, to);

                }
                else
                {
                    state.MoveFromBar(to);

                }
            }
            NullDice((int)Selected,to,color);
        }
        public void Turn()
        {
            rolled = false;
        }
        public void Reset()
        {
            rolled = false;
            dice1 = null;
            dice2 = null;
        }
        bool ExistsDiceMove(int From,Gamestate gamestate)
        {

            if (dice1 != null && ValidMoveTo(gamestate,From + (int) dice1 * gamestate.GetColor()))
            {
                return true;
            }
            if (dice2 != null && ValidMoveTo(gamestate,From + (int)dice2 * gamestate.GetColor()))
            {
                return true;
            }
            return false;
        }
        void GetDiceMoves(int From, Gamestate gamestate, HashSet<int> moves)
        {
            if (dice1 != null && ValidMoveTo(gamestate,From + (int)dice1 * gamestate.GetColor()))
            {
                moves.Add(From + (int)dice1 * gamestate.GetColor());
            }
            if (Double == 0 && dice2 != null && ValidMoveTo(gamestate,From + (int)dice2 * gamestate.GetColor()))
            {
                moves.Add(From + (int)dice2 * gamestate.GetColor());
            }
        }
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
        public bool RemainMoves()
        {
            return NextMoves.Count != 0;
        }
        public int NewToPlay()
        {
            if (r.Next(0, 1) == 1)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
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
    }
}
