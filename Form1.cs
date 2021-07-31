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
        Engine engine=new Engine();
        public Form1()
        {
            int color = game.NewToPlay();
            gamestate = new Gamestate(color);
            InitializeComponent();
            engine.SetBorders(splitter1.Width+splitter1.Location.X,this.Width-splitter2.Location.X);
            engine.ySetBorder(this.Height-splitter1.Location.Y-splitter1.Height);
            engine.SetBoardx(this.Width);
            engine.SetBoardy(this.Height);
        }

        private void Dice_Click(object sender, EventArgs e)
        {
            if (game.Roll())
            {
                engine.Roll(game.GetDice1(), game.GetDice2(), pictureBox2, pictureBox3, label11);
                game.GenNextMoves(gamestate);
                engine.SetSelect(game.GetNextMoves(), null);
                Render();
            }
        }

        private void NGame_Click(object sender, EventArgs e)
        {
            gamestate = new Gamestate(game.NewToPlay());
            game.Reset();
            engine.RenderBoard(gamestate, this.CreateGraphics());
            engine.RenderBarScore(gamestate.GetColor() == 1 ? WScoreLabel : BScoreLabel, gamestate.GetScore(), gamestate.GetColor() == 1 ? BBarLabel : WBarLabel, gamestate.GetEnemyBar(), gamestate.GetColor() == 1 ? WBarLabel : BBarLabel, gamestate.GetMyBar());
        }

        private void Resign_Click(object sender, EventArgs e)
        {
            game.Gameover1 = true;
        }

        private void BBar_Click(object sender, EventArgs e)
        {
            engine.ClearSelect(WScoreBox,BScoreBox,WBarBox,BBarBox);
            game.ClearNext();
            game.GenNextMoves(gamestate);
        }

        private void WBar_Click(object sender, EventArgs e)
        {
            engine.ClearSelect(WScoreBox, BScoreBox, WBarBox, BBarBox);
            game.ClearNext();
            game.GenNextMoves(gamestate);
        }

        private void PlayComp_CheckedChanged(object sender, EventArgs e)
        {
            game.Computer = checkBox1.Checked;
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
            if (x != null && !game.Gameover1 && game.GetRolled())
            {
                if (game.GetNextMoves().Contains((int)x))
                {
                    if (game.GetSelected() != null)
                    {
                        game.PlayValidTo((int)x, gamestate);
                        game.SetSelected(null);
                        game.GenNextMoves(gamestate);
                        engine.SetSelect(game.GetNextMoves(), null);
                        if (!game.RemainMoves())
                        {
                            gamestate.Turn();
                            game.Turn();
                            engine.SetTurn(TurnBox,gamestate.GetColor());
                            label11.Text = "No more moves Your turn is over";
                        }
                    }
                    else
                    {
                        game.SetSelected((int)x);
                        game.GenNextMoves(gamestate);
                        engine.SetSelect(new HashSet<int> {(int)x},game.GetNextMoves());
                        Render();
                    }
                }
            }


        }
        void Render()
        {
            using (Graphics g = CreateGraphics())
            {
                engine.RenderBoard(gamestate, g);
                engine.RenderBarScore(gamestate.GetColor() == 1 ? WScoreLabel : BScoreLabel, gamestate.GetScore(),
                    gamestate.GetColor() == 1 ? WBarLabel : BBarLabel, gamestate.GetMyBar(),
                    gamestate.GetColor() == 1 ? WBarLabel : BBarLabel, gamestate.GetEnemyBar());
                engine.RenderDice(game.GetDice1(), game.GetDice2(), pictureBox2, pictureBox3, game.GetDouble(), label11);
                engine.RenderStressed(g,
                    gamestate.GetColor() == 1 ? WBarBox : BBarBox,
                    gamestate.GetColor() == 1 ? WScoreBox : BScoreBox);
                engine.SetTurn(TurnBox, gamestate.GetColor());
            }
        }
    }
}
