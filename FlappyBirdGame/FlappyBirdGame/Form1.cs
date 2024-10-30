using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlappyBirdGame
{
    public partial class Form1 : Form
    {

        Random random = new Random();
        int gravity = 7;
        int pipeSpeed = 8;
        int score = 0;
        int pipeBottomLocation = 308;
        int pipeTopLocation = -175;
        bool endGame = false;
        int bestScore = 0;

        public Form1()
        {
            InitializeComponent();
        }

        
        void GameOver()
        {
            timer.Stop();
            scoreBoard.Text = score + " Tekrar denemek için E'ye basın!";
            if (score > bestScore)
            {
                bestScore = score;
                lblBestScore.Text = bestScore.ToString();
            }
            endGame = true;

        }
        void RestartGame() 
        {
            endGame = false;
            bird.Location = new Point(90, 232);
            pipeBottom.Left = 650;
            pipeTop.Left = 650;
            score = 0;
            pipeSpeed = 8;
            scoreBoard.Text = score.ToString();
            timer.Start();
        }

        private void GameKeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                gravity = -15;
            }
        }

        private void GameKeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                gravity = 6;
            }
            if (e.KeyCode == Keys.E && endGame)
            {
                RestartGame();
            }
        }

        private void GameTimerEvent(object sender, EventArgs e)
        {
            
            bird.Top += gravity;
            pipeBottom.Left -= pipeSpeed;
            pipeTop.Left -= pipeSpeed;
            scoreBoard.Text = score.ToString();

            int randomLocationValue = random.Next(10, 111);

            
            if (pipeBottom.Right < 0 || (pipeTop.Right < 0))
            {
                pipeBottom.Left = 600;
                pipeBottom.Top = pipeBottomLocation + randomLocationValue;
                pipeTop.Left = 600;
                pipeTop.Top = pipeTopLocation + randomLocationValue;

            }
            

            if (bird.Bounds.IntersectsWith(pipeBottom.Bounds) ||
            bird.Bounds.IntersectsWith(pipeTop.Bounds) ||
             bird.Bounds.IntersectsWith(ground.Bounds))
            {
                GameOver();
            }
            if (bird.Right == pipeTop.Location.X)
            {
                score++;
            }
        }
    }        
}
