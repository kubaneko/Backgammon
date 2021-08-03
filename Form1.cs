using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Backgammon
{
    public partial class Form1 : Form
    {
        Game game = new Game();
        Gamestate gamestate;
        Engine engine = new Engine();
        public Form1()
        {
            int color = game.NewToPlay();
            gamestate = new Gamestate(color);
            InitializeComponent();
            engine.SetBorders(splitter1.Width + splitter1.Location.X, this.Width - splitter2.Location.X);
            engine.ySetBorder(this.Height - splitter1.Location.Y - splitter1.Height);
            engine.SetBoardx(this.Width);
            engine.SetBoardy(this.Height);
        }

        private void Dice_Click(object sender, EventArgs e)
        {
            if (game.Roll())
            {
                engine.Roll(game.GetDice1(), game.GetDice2(), pictureBox2, pictureBox3, label11);
                game.GenNextMoves(gamestate);
                engine.SetSelect(null,game.GetNextMoves());
                IfTurnOver();
                Render();
            }
            else
            {
                Deselect();
            }
        }

        private void NGame_Click(object sender, EventArgs e)
        {
            gamestate = new Gamestate(game.NewToPlay());
            game.Reset();
            game.ClearNext();
            engine.ClearSelect();
            engine.SetSelect(null,game.GetNextMoves());
            Render();
        }

        private void Resign_Click(object sender, EventArgs e)
        {
            game.SetResult( gamestate.GetColor());
            Deselect();
        }

        private void PlayComp_CheckedChanged(object sender, EventArgs e)
        {
            game.Computer = checkBox1.Checked;
            Deselect();
        }

        private void FormShown(object sender, EventArgs e)
        {
            engine.SetBoardx(this.Width);
            engine.SetBoardy(this.Height);
            Render();
        }

        private void FormResize(object sender, EventArgs e)
        {
            engine.SetBoardx(this.Width);
            engine.SetBoardy(this.Height);
            Render();
        }

        private void FormClicked(object sender, MouseEventArgs e)
        {
            int? x = engine.ClickedTile(e.X, e.Y);
            if (x != null && !game.GameOver() && game.GetRolled())
            {
                if (game.GetNextMoves().Contains((int)x))
                {
                    if (game.GetSelected() != null)
                    {
                        game.PlayValidTo((int)x, gamestate);
                        game.SetSelected(null);
                        game.GenNextMoves(gamestate);
                        engine.ClearPictures(WScoreBox, BScoreBox, BBarBox, WBarBox);
                        engine.SetSelect(null,game.GetNextMoves());
                        IfTurnOver();
                    }
                    else
                    {
                        game.SetSelected((int)x);
                        game.GenNextMoves(gamestate);
                        engine.SetSelect(new HashSet<int> { (int)x }, game.GetNextMoves());
                    }
                }
                else
                {
                    Deselect();
                }
            }
            else
            {
                Deselect();
            }
            Render();

        }
        void Render()
        {
            using (Graphics g = CreateGraphics())
            {
                engine.RenderBoard(gamestate, g);
                engine.RenderBarScore(WScoreLabel, gamestate.GetWScore(), BScoreLabel, gamestate.GetBScore(), WBarLabel, gamestate.GetWBar(), BBarLabel, gamestate.GetBBar());
                engine.RenderDice(game.GetDice1(), game.GetDice2(), pictureBox2, pictureBox3, game.GetDouble(), label11);
                engine.RenderStressed(g,
                    gamestate.GetColor() == 1 ? WBarBox : BBarBox,
                    gamestate.GetColor() == 1 ? WScoreBox : BScoreBox);
                engine.SetTurn(TurnBox, gamestate.GetColor());
            }
        }



        private void label1_Click(object sender, EventArgs e)
        {
            if (gamestate.GetColor() == -1)
            {
                SelectBar(-1);
            }
            else
            {
                Deselect();
            }
            Render();

        }



        private void BBarBox_Click(object sender, EventArgs e)
        {
            if (gamestate.GetColor() == -1)
            {
                SelectBar(-1);
            }
            else
            {
                Deselect();
            }
            Render();

        }

        private void BBarLabel_Click(object sender, EventArgs e)
        {
            if (gamestate.GetColor() == -1)
            {
                SelectBar(-1);
            }
            else
            {
                Deselect();
            }
            Render();

        }
        private void BBar_Click(object sender, EventArgs e)
        {
            if (gamestate.GetColor() == -1)
            {
                SelectBar(-1);
            }
            else
            {
                Deselect();
            }
            Render();

        }
        private void WBar_Click(object sender, EventArgs e)
        {
            if (gamestate.GetColor() == 1)
            {
                SelectBar(1);
            }
            else
            {
                Deselect();
            }
            Render();

        }
        private void WBarLabel_Click(object sender, EventArgs e)
        {
            if (gamestate.GetColor() == 1)
            {
                SelectBar(1);
            }
            else
            {
                Deselect();
            }
            Render();

        }

        private void WBarBox_Click(object sender, EventArgs e)
        {
            if (gamestate.GetColor() == 1)
            {
                SelectBar(1);
            }
            else
            {
                Deselect();
            }
            Render();

        }

        private void label_Click(object sender, EventArgs e)
        {
            if (gamestate.GetColor() == 1)
            {
                PlayToScore(1);
            }
            else
            {
                Deselect();
            }
            Render();

        }

        private void WScoreBox_Click(object sender, EventArgs e)
        {
            if (gamestate.GetColor() == 1)
            {
                PlayToScore(1);
            }
            else
            {
                Deselect();
            }
            Render();

        }
        private void WScoreLabel_Click(object sender, EventArgs e)
        {
            if (gamestate.GetColor() == 1)
            {
                PlayToScore(1);
            }
            else
            {
                Deselect();
            }
            Render();

        }
        private void label2_Click(object sender, EventArgs e)
        {
            if (gamestate.GetColor() == -1)
            {
                PlayToScore(-1);
            }
            else
            {
                Deselect();
            }
            Render();

        }

        private void BScoreLabel_Click(object sender, EventArgs e)
        {
            if (gamestate.GetColor() == -1)
            {
                PlayToScore(-1);
            }
            else
            {
                Deselect();
            }
            Render();

        }

        private void BScoreBox_Click(object sender, EventArgs e)
        {
            if (gamestate.GetColor() == -1)
            {
                PlayToScore(-1);
            }
            else
            {
                Deselect();
            }
            Render();

        }



        void SelectBar(int color)
        {
            int firstindex = (24 + color) % 25;
            if (!game.GameOver() && game.GetRolled())
            {
                if (game.GetNextMoves().Contains(firstindex-color))
                {
                    if (game.GetSelected() == null)
                    {
                        game.SetSelected(firstindex - color);
                        game.GenNextMoves(gamestate);
                        engine.ClearPictures(WScoreBox, BScoreBox, BBarBox, WBarBox);
                        engine.SetSelect(new HashSet<int> {firstindex-color }, game.GetNextMoves());
                    }
                }
                else
                {
                    Deselect();
                }
            }
            else
            {
                Deselect();
            }
            Render();
        }
        void PlayToScore(int color)
        {
            int lastindex = (24 - color) % 25;
            if ( !game.GameOver() && game.GetRolled())
            {
                if (game.GetNextMoves().Contains(lastindex+color))
                {
                    if (game.GetSelected() != null)
                    {
                        game.PlayValidTo(lastindex + color, gamestate);
                        game.SetSelected(null);
                        game.GenNextMoves(gamestate);
                        engine.ClearPictures(WScoreBox, BScoreBox, BBarBox, WBarBox);
                        engine.SetSelect(null, game.GetNextMoves());
                        IfTurnOver();
                    }
                    else
                    {
                        Deselect();
                    }
                }
                else
                {
                    Deselect();
                }
            }
            else
            {
                Deselect();
            }
        }

        void IfTurnOver()
        {
            if (!game.RemainMoves())
            {
                gamestate.Turn();
                game.Turn();
                engine.ClearSelect();
                engine.SetTurn(TurnBox, gamestate.GetColor());
                engine.SetInfo("No more moves Your turn is over");
            }
        }

        void Deselect()
        {
            if (game.GetSelected() != null)
            {
                game.SetSelected(null);
                game.GenNextMoves(gamestate);
                engine.ClearPictures(WScoreBox, BScoreBox, BBarBox, WBarBox);
                engine.SetSelect(null,game.GetNextMoves());
            }
        }
    }
}
