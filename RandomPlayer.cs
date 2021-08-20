using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon
{
    class RandomPlayer
    {
        Random r = new Random();
        bool PlaysAsBlack = false;
        bool PlaysMPM = true;
        HashSet<int> From = new HashSet<int>();
        HashSet<int> To = new HashSet<int>();

        public bool PlaysAsBlack1 { get => PlaysAsBlack; set => PlaysAsBlack = value; }
        public bool PlaysAtOnce1 { get => PlaysMPM; set => PlaysMPM = value; }

        public void PlayMove(Game game,Gamestate gamestate)
        {
            if (PlaysAsBlack1 && gamestate.GetColor()==-1)
            {
                From.Clear();
                To.Clear();
                game.SetSelected(null);
                game.GenNextMoves(gamestate);
                if (game.RemainMoves())
                {
                    int r = GetRandomMove(game.GetNextMoves());
                    game.SetSelected(r);
                    From.Add(r);
                    game.GenNextMoves(gamestate);
                    r = GetRandomMove(game.GetNextMoves());
                    game.PlayValidTo(r, gamestate);
                    To.Add(r);
                }
            }
        }
        public void FinishTurn(Game game,Gamestate gamestate)
        {
            if (PlaysAsBlack1 && gamestate.GetColor() == -1)
            {
                From.Clear();
                To.Clear();
                game.SetSelected(null);
                game.GenNextMoves(gamestate);
                while (game.RemainMoves())
                {
                    int r = GetRandomMove(game.GetNextMoves());
                    game.SetSelected(r);
                    From.Add(r);
                    game.GenNextMoves(gamestate);
                    r= GetRandomMove(game.GetNextMoves());
                    game.PlayValidTo(r,gamestate);
                    To.Add(r);
                    game.SetSelected(null);
                    game.GenNextMoves(gamestate);
                }
            }
        }

        int GetRandomMove(HashSet<int> moves)
        {
            return moves.ToArray()[r.Next(0, moves.Count)];
        }

        public HashSet<int> GetFrom()
        {
            return From;
        }
        public HashSet<int> GetTo()
        {
            return To;
        }
    }
}
