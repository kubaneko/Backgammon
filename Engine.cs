﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Backgammon
{
    public class Engine
    {
        HashSet<int> Select=new HashSet<int> ();
        HashSet<int> ToSelect=new HashSet<int> ();
        Bitmap white = Backgammon.Properties.Resources.WDot;
        Bitmap black= Backgammon.Properties.Resources.BDot;
        System.Media.SoundPlayer player;
        Dictionary<int, Bitmap> dice = new Dictionary<int, Bitmap>
        {
            [1] = Backgammon.Properties.Resources.Alea_1,
            [2] = Backgammon.Properties.Resources.Alea_2,
            [3] = Backgammon.Properties.Resources.Alea_3,
            [4] = Backgammon.Properties.Resources.Alea_4,
            [5] = Backgammon.Properties.Resources.Alea_5,
            [6] = Backgammon.Properties.Resources.Alea_6
        };
        double xFractionOfStrip = (double)152 / 2048;
        double xFractionOfBorder = (double)87 / 2048;
        double xFractionBar= (double)50 / 2048;
        double yFractionOfBorder = (double)80 / 1449;
        double yFractionOfStrip = (double)(1449 - 160) / (2 * 1449);
        int BoardSizex;
        int BoardSizey;
        int Border1;
        int Border2;
        int yBorder;

        public Engine()
        {
            player = new System.Media.SoundPlayer(Backgammon.Properties.Resources.dice);
        }

        public void RenderBoard(Gamestate state,Graphics g)
        {
            g.DrawImage(Backgammon.Properties.Resources.Board, new Rectangle(Border1, 0, BoardSizex, BoardSizey));
            if (BoardSizey!=0)
            {
                double DiskPixels = BoardSizex * xFractionOfStrip;
                double FractionDisky = DiskPixels / BoardSizey;
                double yDiffPixels = (yFractionOfStrip * BoardSizey - DiskPixels) / 14;
                for (int i = 0; i < 24; ++i)
                {
                    int colour = Math.Sign(state.GetTile(i));
                    double x = xGetTileCoordinates(i);
                    double y = (1 - (i / 12) * (yFractionOfStrip * 2 - FractionDisky) - FractionDisky - yFractionOfBorder) * BoardSizey;
                    for (int j = 0; j < Math.Abs(state.GetTile(i)); ++j)
                    {
                        if (colour == 1)
                        {
                            g.DrawImage(white, new RectangleF((float)x, (float)y + j * (float)yDiffPixels * Math.Sign(i - 11.5), (float)DiskPixels, (float)DiskPixels));
                        }
                        else
                        {
                            if (colour == -1)
                            {
                                g.DrawImage(black, new RectangleF((float)x, (float)y + j * (float)yDiffPixels * Math.Sign(i - 11.5), (float)DiskPixels, (float)DiskPixels));
                            }
                        }
                    }
                }
            }
        }



        public void Roll(int? dice1,int? dice2, PictureBox d1, PictureBox d2, Label l)
        {
            player.Play();
            RenderDice(dice1, dice2, d1, d2,4,l);
        }
        public void SetTurn(PictureBox turn,int color)
        {
            if (color==1)
            {
                turn.Image = white;
            }
            else
            {
                turn.Image = black;
            }
        }
        public void SetBoardx(int x)
        {
            BoardSizex = x- Border1 - Border2;
        }
        public void SetBoardy(int y)
        {
            BoardSizey = y-yBorder;
        }
        public void SetBorders(int s1, int s2)
        {
            Border1 = s1;
            Border2 = s2;
        }
        public void ySetBorder(int y)
        {
            yBorder = y;
        }
        public void RenderDice(int? dice1, int? dice2, PictureBox d1, PictureBox d2,int? Double,Label DoubleText)
        {
            bool CanBeDouble = true;
            if (dice1 == null)
            {
                using (Graphics g = d1.CreateGraphics())
                {
                    g.Clear(Color.Black);
                }
                CanBeDouble = false;
            }
            else
            {
                d1.Image = dice[(int)dice1];
            }
            if (dice2 == null)
            {
                using (Graphics g = d2.CreateGraphics())
                {
                    g.Clear(Color.Black);
                }
                CanBeDouble = false;
            }
            else
            {
                d2.Image = dice[(int)dice2];
            }
            if (CanBeDouble && dice1==dice2)
            {
                DoubleText.Text = "Double " + Double.ToString();
            }
            else
            {
                DoubleText.Text = "";
            }
        }
        public void RenderBarScore(Label MyScore, int MS,Label EnemyBar,int EB,Label MyBar,int MB)
        {
            MyScore.Text = MS.ToString();
            EnemyBar.Text = EB.ToString();
            MyBar.Text = MB.ToString();
        }
        public int? ClickedTile(int x, int y)
        {
            int column;
            if (Border1+BoardSizex*xFractionOfBorder < x && x < BoardSizex + Border1- BoardSizex * xFractionOfBorder &&(x<(6*xFractionOfStrip+xFractionOfBorder)*BoardSizex+ Border1 || x>(6 * xFractionOfStrip + xFractionOfBorder+xFractionBar)*BoardSizex+Border1))
            {
                if (x < (6 * xFractionOfStrip + xFractionOfBorder) * BoardSizex+Border1)
                {
                    column = (int)Math.Floor((x - Border1 - BoardSizex * xFractionOfBorder) / (xFractionOfStrip * BoardSizex));
                }
                else
                {
                    column = (int)Math.Floor((x - Border1 - BoardSizex * (xFractionOfBorder+xFractionBar)) / (xFractionOfStrip * BoardSizex));
                }
                if (column >11 || column < 0)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
            int line=2*(2*y/BoardSizey)-1;
            return (int)(11.5-column*line-(double)line/2);
        }

        double xGetTileCoordinates(int tile)
        {
            return ((Math.Abs(tile - 11.5) - 0.5) * xFractionOfStrip + xFractionOfBorder + xFractionBar * ((int)(Math.Abs((double)tile - 11.5) - 0.5) / 6)) * BoardSizex+ Border1;
        }

        public void RenderStressed(Graphics g, PictureBox Bar, PictureBox Score)
        {
            Color S = (Select.Count == 1) ? Color.Red : Color.Blue;
            Color ToS = Color.Green;
            foreach (int i in Select)
            {
                if (i < 0 || i > 23)
                {
                    Bar.BackColor = S;
                }
                else
                {
                    DrawStress(g, S, i);
                }
            }
            foreach (int j in ToSelect)
            {
                if (j < 0 || j > 23)
                {
                    Score.BackColor = ToS;
                }
                else
                {
                    DrawStress(g, ToS, j);
                }
            }
        }

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

        public void ClearSelect(PictureBox WScore, PictureBox BScore, PictureBox BBar, PictureBox WBar)
        {
            Select.Clear();
            ToSelect.Clear();
            ClearPictures(WScore,BScore,WBar,BBar);
        }
        public void SetSelect(HashSet<int> S, HashSet<int> ToS)
        {
            if (S != null)
            {
                Select = S;
            }
            if (ToS != null)
            {
                ToSelect = ToS;
            }
        }
        void ClearPictures(PictureBox WScore,PictureBox BScore,PictureBox BBar,PictureBox WBar)
        {
            WScore.BackColor = Color.Transparent;
            BScore.BackColor = Color.Transparent;
            WBar.BackColor = Color.Transparent;
            BBar.BackColor = Color.Transparent;
        }
    }
}
