using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon
{
    class RandomPlayer
    {
        /* class responsible for generating and playing random moves and remembering played moves
         */

        Random r = new Random();
        // Whether it is active
        bool PlaysAsBlack = false;
        // whether it plays all moves left in a turn or plays a move when a button is pressed
        bool PlaysMPM = true;
        // sets to remember and display from to where were the moves played
        HashSet<int> From = new HashSet<int>();
        HashSet<int> To = new HashSet<int>();

        public bool PlaysAsBlack1 { get => PlaysAsBlack; set => PlaysAsBlack = value; }
        public bool PlaysAtOnce1 { get => PlaysMPM; set => PlaysMPM = value; }

        // if it plays as black it plays a random move and updates the hashsets to display the move

        public void PlayMove(Game game, Gamestate gamestate)
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
                game.SetSelected(null);
            }

        }
        // Finishes the turn by playing randomly selected remaining moves and adds them to Hashsets
        public void FinishTurn(Game game, Gamestate gamestate)
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
                r = GetRandomMove(game.GetNextMoves());
                game.PlayValidTo(r, gamestate);
                To.Add(r);
                game.SetSelected(null);
                game.GenNextMoves(gamestate);
            }
        }

        // returns random move from NextMOves Hashset

        int GetRandomMove(HashSet<int> moves)
        {
            return moves.ToArray()[r.Next(0, moves.Count)];
        }

        // Getters for the Hashsets

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
