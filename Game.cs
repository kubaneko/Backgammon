using System;
using System.Collections.Generic;
using System.Linq;

namespace Backgammon
{
    public class Game
    {
        int? Selected;
        HashSet<int> NextMoves = new HashSet<int>();
        bool rolled = false;
        Random r = new Random();
        int Double = 0;
        int? dice1;
        int? dice2;
        bool BlackWon = false;
        bool WhiteWon = false;
        const int MAXTILE = 23;

        public bool GameOver()
        {
            return BlackWon || WhiteWon;
        }
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
        public bool GetWhiteWon()
        {
            return WhiteWon;
        }

        public bool GetBlackWon()
        {
            return BlackWon;
        }

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
        public void SetRolled(bool Rolled)
        {
            rolled = Rolled;
        }

        public int? GetDice1()
        {
            return dice1;
        }
        public int? GetDice2()
        {
            return dice2;
        }

        public bool ValidMoveTo(Gamestate game, int from, int to)
        {
            if (to > MAXTILE || to < 0)
            {
                if (game.HomeRowFull())
                {
                    if (game.IsLastInHomerow(from))
                    {
                        return true;
                    }
                    else
                    {
                        if (to == MAXTILE + 1 || to == - 1)
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
            int firstindex = ((MAXTILE + 1) + color) % (MAXTILE + 2);
            int lastindex = ((MAXTILE + 1) - color) % (MAXTILE + 2);
            ClearNext();
            // First Tile for the player (after bar)
            if (Selected == null)
            {
                if (gamestate.BarEmpty())
                {
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
                    if (ExistsDiceMove(firstindex - color, gamestate))
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
        public void PlayValidTo(int to, Gamestate state)
        {
            int color = state.GetColor();
            int firstindex = ((MAXTILE + 1) + color) % (MAXTILE + 2);
            int lastindex = ((MAXTILE + 1) - color) % (MAXTILE + 2);
            if (color * to > lastindex * color)
            {
                state.MoveToScore((int)Selected);
                SetResult(state.Won());
            }
            else
            {
                if ((firstindex - color) * color < (int)Selected * color)
                {
                    state.Move((int)Selected, to);

                }
                else
                {
                    state.MoveFromBar(to);

                }
            }
            NullDice((int)Selected, to, color);
        }
        public void Turn()
        {
            ClearNext();
            rolled = false;
            Double = 0;
        }
        public void Reset()
        {
            rolled = false;
            dice1 = null;
            dice2 = null;
            BlackWon = false;
            WhiteWon = false;
        }
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
        int NormalizeMove(int i)
        {
            if (i > MAXTILE)
            {
                return MAXTILE + 1;
            }
            if (i < 0)
            {
                return  - 1;
            }
            return i;
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
            return ((dice1 != null || dice2 != null) && !(dice1 != null && dice2 != null && dice1 == dice2 && Double == 0) && NextMoves.Count() > 0);
        }
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
