using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Backgammon
{
    public class Engine
    {
        /* Class in charge of graphic appearance of the game
         */

        // Tiles to be stressed
        HashSet<int> Select = new HashSet<int>();
        HashSet<int> ToSelect = new HashSet<int>();
        //Graphics and sounds
        Bitmap white = Backgammon.Properties.Resources.WDot;
        Bitmap black = Backgammon.Properties.Resources.BDot;
        System.Media.SoundPlayer player = new System.Media.SoundPlayer(Backgammon.Properties.Resources.dice);
        System.Media.SoundPlayer player2 = new System.Media.SoundPlayer(Backgammon.Properties.Resources.moved);
        Dictionary<int, Bitmap> dice = new Dictionary<int, Bitmap>
        {
            [1] = Backgammon.Properties.Resources.Alea_1,
            [2] = Backgammon.Properties.Resources.Alea_2,
            [3] = Backgammon.Properties.Resources.Alea_3,
            [4] = Backgammon.Properties.Resources.Alea_4,
            [5] = Backgammon.Properties.Resources.Alea_5,
            [6] = Backgammon.Properties.Resources.Alea_6
        };
        // relative sizes of pieces of the board to the size of the board
        double xFractionOfStrip = (double)152 / 2048;
        double xFractionOfBorder = (double)87 / 2048;
        double xFractionBar = (double)50 / 2048;
        double yFractionOfBorder = (double)80 / 1449;
        double yFractionOfStrip = (double)(1449 - 160) / (2 * 1449);
        // size of the displayed board
        int BoardSizex;
        int BoardSizey;
        // sizes of things around the board 
        int Menu1;
        int Menu2;
        int yBorder;
        // information to be displayed
        string info = "";


        // Calculating Boardsize
        public void SetBorders(int s1, int s2)
        {
            Menu1 = s1;
            Menu2 = s2;
        }
        public void ySetBorder(int y)
        {
            yBorder = y;
        }
        public void xSetWindowSize(int x)
        {
            BoardSizex = x - Menu1 - Menu2;
        }
        public void ySetWindowSize(int y)
        {
            BoardSizey = y - yBorder;
        }
        public void RenderBoard(Gamestate state, Graphics g)
        {
            // Draws the board
            g.DrawImage(Backgammon.Properties.Resources.Board, new Rectangle(Menu1, 0, BoardSizex, BoardSizey));
            if (BoardSizey != 0)
            {
                // size of the stones
                double StonePixels = BoardSizex * xFractionOfStrip;
                double FractionStone = StonePixels / BoardSizey;
                // size of the gaps between stones so they all fit
                double yDiffPixels = (yFractionOfStrip * BoardSizey - StonePixels) / 14;
                // displaying stones for every tile
                for (int i = 0; i < Constants.MAXTILE + 1; ++i)
                {
                    // their color and coordinates
                    int colour = Math.Sign(state.GetTile(i));
                    double x = xGetTileCoordinates(i);
                    double y = (1 - (i / 12) * (yFractionOfStrip * 2 - FractionStone) - FractionStone - yFractionOfBorder) * BoardSizey;
                    // displaying concrete stones moved by yDiffPixels
                    for (int j = 0; j < Math.Abs(state.GetTile(i)); ++j)
                    {
                        if (colour == 1)
                        {
                            g.DrawImage(white, new RectangleF((float)x, (float)y + j * (float)yDiffPixels * Math.Sign(i - 11.5), (float)StonePixels, (float)StonePixels));
                        }
                        else
                        {
                            if (colour == -1)
                            {
                                g.DrawImage(black, new RectangleF((float)x, (float)y + j * (float)yDiffPixels * Math.Sign(i - 11.5), (float)StonePixels, (float)StonePixels));
                            }
                        }
                    }
                }
            }
        }

        // changes turn visually
        public void RenderTurn(PictureBox turn, int color)
        {
            info = "Your turn is Over";
            if (color == 1)
            {
                turn.Image = white;
            }
            else
            {
                turn.Image = black;
            }
        }


        // Renders Dice
        public void RenderDice(int? dice1, int? dice2, PictureBox d1, PictureBox d2, int? Double)
        {
            if (dice1 == null)
            {
                d1.Image = null;
            }
            else
            {
                d1.Image = dice[(int)dice1];
            }
            if (dice2 == null)
            {
                d2.Image = null;
            }
            else
            {
                d2.Image = dice[(int)dice2];
            }
        }
        // Renders Info
        public void RenderInfo(Label InfoBar)
        {
            InfoBar.Text = info;
        }
        // Renders Bar and score
        public void RenderBarScore(Label WScore, int WS, Label BScore, int BS, Label BBar, int BB, Label WBar, int WB)
        {
            WScore.Text = WS.ToString();
            BBar.Text = BB.ToString();
            WBar.Text = WB.ToString();
            BScore.Text = BS.ToString();
        }

        // Renders stressed tiles and stresses bar and score if needed
        public void RenderStressed(Graphics g, PictureBox Bar, PictureBox Score, Gamestate gamestate)
        {
            if (Select != null)
            {
                Color S = (Select.Count == 1) ? Color.Red : Color.Blue;
                foreach (int i in Select)
                {
                    if (i < 0 || i > Constants.MAXTILE)
                    {
                        Bar.BackColor = S;
                    }
                    else
                    {
                        DrawStress(g, S, i);
                    }
                }
            }
            if (ToSelect != null)
            {
                Color ToS = Color.Green;
                foreach (int j in ToSelect)
                {
                    if (j == 0 - 1 || j == Constants.MAXTILE + 1)
                    {
                        int color = gamestate.GetColor();
                        int bar = (Constants.MAXTILE + 1 + color) % (Constants.MAXTILE + 2) - color;
                        if (j == bar)
                        {
                            Bar.BackColor = ToS;
                        }
                        else
                        {
                            Score.BackColor = ToS;
                        }
                    }
                    else
                    {
                        DrawStress(g, ToS, j);
                    }
                }
            }
        }
        // draws the rectagles used for stressing
        private void DrawStress(Graphics g, Color ToS, int j)
        {
            Rectangle rect = new Rectangle((int)Math.Ceiling(xGetTileCoordinates(j)),
                j > 11 ? 0 : (int)Math.Ceiling(BoardSizey * (1 - yFractionOfBorder)),
                (int)Math.Floor(BoardSizex * xFractionOfStrip),
                (int)Math.Floor(BoardSizey * yFractionOfBorder));
            using (Pen p = new Pen(ToS))
            {
                g.DrawRectangle(p, rect);
                g.FillRectangle(p.Brush, rect);
            }
        }

        // Clears the stress off stressable controlls

        public void ClearPictures(PictureBox WScore, PictureBox BScore, PictureBox BBar, PictureBox WBar)
        {
            WScore.BackColor = Color.Transparent;
            BScore.BackColor = Color.Transparent;
            WBar.BackColor = Color.Transparent;
            BBar.BackColor = Color.Transparent;
        }

        // plays sound effects
        public void PlayMoved()
        {
            player2.Play();
        }

        public void PlayDice()
        {
            player.Play();
        }

        // returns Tile that was clicked or null if no tile was clicked
        public int? ClickedTile(int x, int y)
        {
            int column;
            if (Menu1 + BoardSizex * xFractionOfBorder < x && x < BoardSizex + Menu1 - BoardSizex * xFractionOfBorder && (x < (6 * xFractionOfStrip + xFractionOfBorder) * BoardSizex + Menu1 || x > (6 * xFractionOfStrip + xFractionOfBorder + xFractionBar) * BoardSizex + Menu1))
            {
                if (x < (6 * xFractionOfStrip + xFractionOfBorder) * BoardSizex + Menu1)
                {
                    column = (int)Math.Floor((x - Menu1 - BoardSizex * xFractionOfBorder) / (xFractionOfStrip * BoardSizex));
                }
                else
                {
                    column = (int)Math.Floor((x - Menu1 - BoardSizex * (xFractionOfBorder + xFractionBar)) / (xFractionOfStrip * BoardSizex));
                }
                if (column > 11 || column < 0)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
            int line = 2 * (2 * y / BoardSizey) - 1;
            return (int)(11.5 - column * line - (double)line / 2);
        }
        // Returns x coordinates of a tile
        double xGetTileCoordinates(int tile)
        {
            return ((Math.Abs(tile - 11.5) - 0.5) * xFractionOfStrip + xFractionOfBorder + xFractionBar * ((int)(Math.Abs((double)tile - 11.5) - 0.5) / 6)) * BoardSizex + Menu1;
        }

        // sets stressed tiles/constrols)
        public void SetSelect(HashSet<int> S, HashSet<int> ToS)
        {
            Select = S;
            ToSelect = ToS;
        }
        // clears the stressed tiles/controls
        public void ClearSelect()
        {
            Select = null;
            ToSelect = null;
        }
        // Clears the info
        public void ClearInfo()
        {
            info = "";
        }
        // Sets the number of moves to be played if you rolled a double
        public void SetDouble(int k)
        {
            info = "Double " + k.ToString();
        }
        // Sets info for result
        public void SetResult(int color)
        {
            if (color == 1)
            {
                info = "WHITE WON";
            }
            else
            {
                if (color == -1)
                {
                    info = "BLACK WON";
                }
            }
        }
    }
}
