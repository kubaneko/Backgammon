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
        RandomPlayer rplayer = new RandomPlayer();

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

        //METHODS TO SAVE SPACE

        // If there are no more moves we change the turn and inform the player and looks whether Random Player should play
        void IfTurnOver()
        {
            if (!game.RemainMoves())
            {
                gamestate.Turn();
                game.Turn();
                Deselect();
                engine.RenderTurn(TurnBox, gamestate.GetColor());
                RPlayerPlays();
            }
        }
        // Checks whether game ended
        // If there are no more moves The computer has ended
        // we do not change the turn because we want to render the played moves correctly and for longer time so the player can see them
        void RPlayerIfTurnGameOver()
        {
            IfGameOver();
            if (!game.RemainMoves() && !game.GameOver())
            {
                engine.RenderTurn(TurnBox, gamestate.GetColor());
            }
        }
        // If the Random Player plays he checks whether he has already rolled the dice and if not he rolls them Renders them
        // Then we chack whether Random Player plays at once if so we let it Finish the Turn then we Check Whether the turn or Game is over
        private void RPlayerPlays()
        {
            if (gamestate.GetColor() == Constants.RPLAYERCOLOR && rplayer.Plays1 && !game.GameOver())
            {
                if (game.Roll())
                {
                    engine.PlayDice();
                    if ((int)game.GetDice1() == (int)game.GetDice2())
                    {
                        engine.SetDouble(game.GetDouble());
                    }
                    else
                    {
                        engine.ClearInfo();
                    }
                    engine.RenderDice(game.GetDice1(), game.GetDice2(), pictureBox2, pictureBox3, game.GetDouble());
                }
                game.GenNextMoves(gamestate);
                if (rplayer.PlaysAtOnce1)
                {
                    rplayer.FinishTurn(game, gamestate);
                    engine.SetSelect(rplayer.GetFrom(), rplayer.GetTo());
                    game.SetSelected(null);
                }

                RPlayerIfTurnGameOver();
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

                // We want the player see what the RPlayer rolled if it plays at once so if it finished the turn we do not render the null dice
                if (!rplayer.Plays1 || !rplayer.PlaysAtOnce1 || gamestate.GetColor() != Constants.RPLAYERCOLOR || game.RemainMoves())
                {
                    engine.RenderDice(game.GetDice1(), game.GetDice2(), pictureBox2, pictureBox3, game.GetDouble());
                }
                engine.RenderInfo(label11);
                engine.RenderStressed(g,
                    gamestate.GetColor() == 1 ? WBarBox : BBarBox,
                    gamestate.GetColor() == 1 ? WScoreBox : BScoreBox,
                    gamestate);
            }
        }


        // method that selects stones from bar and updates graphics

        void SelectBar(int color)
        {
            int firstindex = (Constants.MAXTILE + 1 + color) % (Constants.MAXTILE + 2);
            if (!game.GameOver() && game.GetRolled() && game.GetNextMoves().Contains(firstindex - color))
            {
                if (game.GetSelected() == null)
                {
                    game.SetSelected(firstindex - color);
                    game.GenNextMoves(gamestate);
                    engine.ClearPictures(WScoreBox, BScoreBox, BBarBox, WBarBox);
                    engine.SetSelect(firstindex - color, game.GetNextMoves());
                    return;
                }
            }
            Deselect();
        }

        // method that moves stones to "score" 

        void PlayToScore(int color)
        {
            int lastindex = (Constants.MAXTILE + 1 - color) % (Constants.MAXTILE + 2);
            if (!game.GameOver() && game.GetRolled())
            {
                if (game.GetNextMoves().Contains(lastindex + color) && game.GetSelected() != null)
                {
                    game.PlayValidTo((int)lastindex + color, gamestate);
                    if (game.GetDouble() > 0)
                    {
                        engine.SetDouble(game.GetDouble());
                    }
                    game.SetSelected(null);
                    engine.ClearPictures(WScoreBox, BScoreBox, BBarBox, WBarBox);
                    game.GenNextMoves(gamestate);
                    engine.SetSelect(null, game.GetNextMoves());
                    engine.PlayMoved();
                    IfGameOver();
                    if (!game.GameOver())
                    {
                        IfTurnOver();
                    }
                    return;
                }

            }
            Deselect();
        }

        // Checks whether the Game is over and renders graphics

        private void IfGameOver()
        {
            if (game.GameOver())
            {
                // it should be called on the last turn
                // in this case the player in turn won
                engine.SetResult(gamestate.GetColor());
                engine.ClearPictures(WScoreBox, BScoreBox, BBarBox, WBarBox);
                engine.ClearSelect();
                game.SetRolled(true);
            }
        }

        // BUTTONS

        // Rolling the dice
        private void Dice_Click(object sender, EventArgs e)
        {
            // if we have not rolled the dice we eill
            if (!game.GameOver())
            {
                IfTurnOver();
                if (game.Roll())
                {
                    engine.RenderTurn(TurnBox, gamestate.GetColor());
                    engine.ClearSelect();
                    engine.PlayDice();
                    // prepare moves to be selected/stressed
                    game.GenNextMoves(gamestate);
                    engine.SetSelect(null, game.GetNextMoves());
                    engine.ClearPictures(WScoreBox, BScoreBox, BBarBox, WBarBox);
                    if ((int)game.GetDice1() == (int)game.GetDice2())
                    {
                        engine.SetDouble(game.GetDouble());
                    }
                    else
                    {
                        engine.ClearInfo();
                    }
                    IfTurnOver();
                }
                else
                {
                    Deselect();
                }
                Render();
            }
        }
        // Setting up a new game
        private void NGame_Click(object sender, EventArgs e)
        {
            gamestate = new Gamestate(game.NewToPlay());
            game.Reset();
            engine.Reset(WScoreBox, BScoreBox, BBarBox, WBarBox, TurnBox, gamestate.GetColor());
            RPlayerPlays();
            Render();
        }

        // resigning turn starts after rolling dice, and you can not resign for the Random Player
        private void Resign_Click(object sender, EventArgs e)
        {
            if (!game.GameOver() && (!rplayer.Plays1 || gamestate.GetColor() != Constants.RPLAYERCOLOR) && game.GetRolled())
            {
                game.SetResult(gamestate.GetColor());
                engine.SetResult(gamestate.GetColor() * -1);
                game.SetRolled(true);
                Deselect();
                Render();
            }
        }

        // RPlayer plays a move if it should and updates the graphics

        private void RPlayerNextMove_Click(object sender, EventArgs e)
        {
            if (!game.GameOver())
            {
                Deselect();
                if (gamestate.GetColor() == Constants.RPLAYERCOLOR && rplayer.Plays1)
                {
                    game.GenNextMoves(gamestate);
                    if (game.RemainMoves())
                    {
                        engine.PlayMoved();
                    }
                    rplayer.PlayMove(game, gamestate);
                    engine.ClearPictures(WScoreBox, BScoreBox, BBarBox, WBarBox);
                    engine.SetSelect(rplayer.GetFrom(), rplayer.GetTo());
                    if (game.GetDouble() > 0)
                    {
                        engine.SetDouble(game.GetDouble());
                    }
                    RPlayerIfTurnGameOver();
                }
                Render();
            }
        }

        //Resizing and stuff

        // sets new window size
        private void FormShown(object sender, EventArgs e)
        {
            engine.xSetWindowSize(this.Width);
            engine.ySetWindowSize(this.Height);
            engine.RenderTurn(TurnBox, gamestate.GetColor());
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
            if (!game.GameOver() && (!rplayer.Plays1 || gamestate.GetColor() != Constants.RPLAYERCOLOR))
            {
                // we get the tile clicked
                int? x = engine.ClickedTile(e.X, e.Y);
                if (x != null && game.GetRolled() && game.GetNextMoves().Contains((int)x))
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
                        engine.ClearPictures(WScoreBox, BScoreBox, BBarBox, WBarBox);
                        game.SetSelected(null);
                        game.GenNextMoves(gamestate);
                        engine.SetSelect(null, game.GetNextMoves());
                        IfGameOver();
                        if (!game.GameOver())
                        {
                            IfTurnOver();
                        }
                    }
                    else
                    {
                        // we select the stone and generate moves for it
                        game.SetSelected((int)x);
                        game.GenNextMoves(gamestate);
                        engine.SetSelect((int)x, game.GetNextMoves());
                    }
                    Render();
                    return;
                }
                // if the click does not make much sense we deselect and continue
                Deselect();
                Render();
            }
        }

        // LABEL AND PICTUREBOX CONTROLS

        // selecting bar or not
        private void BBar_Click(object sender, EventArgs e)
        {
            if (gamestate.GetColor() == -1 && (-1!=Constants.RPLAYERCOLOR || !rplayer.Plays1))
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
            if (gamestate.GetColor() == 1 && (1 != Constants.RPLAYERCOLOR || !rplayer.Plays1))
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
            if (gamestate.GetColor() == 1 && (1 != Constants.RPLAYERCOLOR || !rplayer.Plays1))
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
            if (gamestate.GetColor() == -1 && (-1 != Constants.RPLAYERCOLOR || !rplayer.Plays1))
            {
                PlayToScore(-1);
            }
            else
            {
                Deselect();
            }
            Render();

        }

        // Checkboxes

        // Updates the RPlayer settings
        // updates some controlls being enabled
        // If active the RPlayer Plays
        private void RPlayerBlackChanged(object sender, EventArgs e)
        {
            rplayer.Plays1 = checkBox1.Checked;
            if (!checkBox1.Checked)
            {
                checkBox2.Enabled = false;
                button4.Enabled = false;
            }
            else
            {
                checkBox2.Enabled = true;
                if (checkBox2.Checked)
                {
                    button4.Enabled = true;
                }

            }
            if (!game.GameOver())
            {
                Deselect();
                RPlayerPlays();
                Render();
            }
        }

        // Changes whether the RPlayer plays move per move or at once
        // enables/disables the next move button
        // If he should the RPlayer plays

        private void RPlayerMovePerMChanged(object sender, EventArgs e)
        {
            rplayer.PlaysAtOnce1 = !checkBox2.Checked;
            if (!checkBox2.Checked)
            {
                button4.Enabled = false;
            }
            else
            {
                button4.Enabled = true;
            }
            if (!game.GameOver())
            {
                Deselect();
                RPlayerPlays();
                Render();
            }
        }
    }
}
