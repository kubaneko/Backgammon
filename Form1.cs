using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Backgammon
{
    public partial class Form1 : Form
    {
        /* The WinForm
         */

        Game game = new Game();
        Gamestate gamestate;
        Engine engine = new Engine();
        //constant
        const int MAXTILE = 23;
        public Form1()
        {
            // getting new game started and graphics ready
            gamestate = new Gamestate(game.NewToPlay());
            InitializeComponent();
            engine.SetBorders(splitter1.Width + splitter1.Location.X, this.Width - splitter2.Location.X);
            engine.ySetBorder(this.Height - splitter1.Location.Y - splitter1.Height);
            engine.xSetWindowSize(this.Width);
            engine.ySetWindowSize(this.Height);
        }

        // BUTTONS

        // Rolling the dice
        private void Dice_Click(object sender, EventArgs e)
        {
            // if we have not rolled the dice we eill
            if (game.Roll())
            {
                engine.PlayDice();
                // prepare moves to be selected/stressed
                game.GenNextMoves(gamestate);
                engine.SetSelect(null, game.GetNextMoves());
                if ((int)game.GetDice1() == (int)game.GetDice2())
                {
                    engine.SetDouble(game.GetDouble());
                }
                else
                {
                    engine.ClearInfo();
                }
                IfTurnOver();
                Render();
            }
            else
            {
                Deselect();
            }
        }
        // Setting up a new game
        private void NGame_Click(object sender, EventArgs e)
        {
            gamestate = new Gamestate(game.NewToPlay());
            game.Reset();
            game.ClearNext();
            engine.ClearSelect();
            engine.ClearInfo();
            engine.SetSelect(null, game.GetNextMoves());
            Render();
        }

        // resigning
        private void Resign_Click(object sender, EventArgs e)
        {
            if (!game.GameOver())
            {
                game.SetResult(gamestate.GetColor());
                engine.SetResult(gamestate.GetColor() * -1);
                game.SetRolled(true);
                Deselect();
                Render();
            }
        }

        //Resizing and stuff

        // sets new window size
        private void FormShown(object sender, EventArgs e)
        {
            engine.xSetWindowSize(this.Width);
            engine.ySetWindowSize(this.Height);
            Render();
        }

        private void FormResize(object sender, EventArgs e)
        {
            engine.xSetWindowSize(this.Width);
            engine.ySetWindowSize(this.Height);
            Render();
        }

        // main function for use of the game
        private void FormClicked(object sender, MouseEventArgs e)
        {
            // we get the tile clicked
            int? x = engine.ClickedTile(e.X, e.Y);
            if (x != null && !game.GameOver() && game.GetRolled() && game.GetNextMoves().Contains((int)x))
            {
                // if the move is within board and makes sense to click on the tile
                // we look whether selecting the tile is valid
                if (game.GetSelected() != null)
                {
                    // we move the stone reset stressed and next tiles if the turn is not over we shown the new ones
                    game.PlayValidTo((int)x, gamestate);
                    engine.PlayMoved();
                    if (game.GetDouble() > 0)
                    {
                        engine.SetDouble(game.GetDouble());
                    }
                    game.SetSelected(null);
                    engine.ClearSelect();
                    game.GenNextMoves(gamestate);
                    engine.ClearPictures(WScoreBox, BScoreBox, BBarBox, WBarBox);
                    engine.SetSelect(null, game.GetNextMoves());
                    IfTurnOver();
                }
                else
                {
                    // we select the stone and generate moves for it
                    game.SetSelected((int)x);
                    game.GenNextMoves(gamestate);
                    engine.SetSelect(new HashSet<int> { (int)x }, game.GetNextMoves());
                }
                Render();
                return;
            }
            // if the click does not make much sense we deselect and continue
            Deselect();
            Render();
        }

        // LABEL AND PICTUREBOX CONTROLS

        // selecting bar or not
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

        // moving to Score or not
        private void WScore_Click(object sender, EventArgs e)
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


        private void BScore_Click(object sender, EventArgs e)
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

        // method that moves from bar - similar to FormClicked

        void SelectBar(int color)
        {
            int firstindex = (MAXTILE + 1 + color) % (MAXTILE + 2);
            if (!game.GameOver() && game.GetRolled() && game.GetNextMoves().Contains(firstindex - color))
            {
                if (game.GetSelected() == null)
                {
                    game.SetSelected(firstindex - color);
                    game.GenNextMoves(gamestate);
                    engine.ClearPictures(WScoreBox, BScoreBox, BBarBox, WBarBox);
                    engine.SetSelect(new HashSet<int> { firstindex - color }, game.GetNextMoves());
                    return;
                }
            }
            Deselect();
        }

        // method that scores - similar to FormClicked

        void PlayToScore(int color)
        {
            int lastindex = (MAXTILE + 1 - color) % (MAXTILE + 2);
            if (!game.GameOver() && game.GetRolled())
            {
                if (game.GetNextMoves().Contains(lastindex + color) && game.GetSelected() != null)
                {
                    game.PlayValidTo((int)lastindex + color, gamestate);
                    engine.PlayMoved();
                    if (game.GameOver())
                    {
                        // GameOver must change during this function or the resign function
                        // in this case the player in turn won
                        engine.SetResult(gamestate.GetColor());
                        engine.ClearPictures(WScoreBox, BScoreBox, BBarBox, WBarBox);
                        engine.ClearSelect();
                        game.SetRolled(true);
                    }
                    else
                    {
                        if (game.GetDouble() > 0)
                        {
                            engine.SetDouble(game.GetDouble());
                        }
                        game.SetSelected(null);
                        engine.ClearSelect();
                        game.GenNextMoves(gamestate);
                        engine.ClearPictures(WScoreBox, BScoreBox, BBarBox, WBarBox);
                        engine.SetSelect(null, game.GetNextMoves());
                        IfTurnOver();
                    }
                    return;
                }

            }
            Deselect();
        }

        //METHODS TO SAVE SPACE

        // If there are no more moves we change the turn and inform the player
        void IfTurnOver()
        {
            if (!game.RemainMoves())
            {
                gamestate.Turn();
                game.Turn();
                engine.ClearSelect();
                engine.SetTurn(TurnBox, gamestate.GetColor());
            }
        }
        // nulls the selected tile andgenerates new tiles to be selected
        void Deselect()
        {
            if (game.GetSelected() != null)
            {
                game.SetSelected(null);
                engine.ClearSelect();
                game.GenNextMoves(gamestate);
                engine.ClearPictures(WScoreBox, BScoreBox, BBarBox, WBarBox);
                engine.SetSelect(null, game.GetNextMoves());
            }
        }
        void Render()
        {
            // rendering everything method only because there are a lot of controlls that need to be passed
            using (Graphics g = CreateGraphics())
            {
                engine.RenderBoard(gamestate, g);
                engine.RenderBarScore(WScoreLabel, gamestate.GetWScore(), BScoreLabel, gamestate.GetBScore(), WBarLabel, gamestate.GetWBar(), BBarLabel, gamestate.GetBBar());
                engine.RenderDice(game.GetDice1(), game.GetDice2(), pictureBox2, pictureBox3, game.GetDouble());
                engine.RenderInfo(label11);
                engine.RenderStressed(g,
                    gamestate.GetColor() == 1 ? WBarBox : BBarBox,
                    gamestate.GetColor() == 1 ? WScoreBox : BScoreBox,
                    gamestate);
            }
        }
    }
}
