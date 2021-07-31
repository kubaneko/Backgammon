using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backgammon
{
    public class PlayerComputer : Game
    {
        public HashSet<Gamestate> GenerateMoves(Gamestate game,int dice1,int dice2)
        {
            return new HashSet<Gamestate>();
        }
        public List<int> FindBestMove(Gamestate game, int dice1, int dice2)
        {
            return new List<int>();
        }
        public void SetDice(int d1,int d2)
        {
            dice1 = d1;
            dice2 = d2;
        }
    }
}
