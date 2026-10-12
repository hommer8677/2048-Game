using _2048.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Input;
using System.Windows.Shapes;

namespace _2048.VM
{
    public class GameViewModel: ViewModelBase
    {
        private Model model;
        public event Action<string> GameEnded;
        private int score;
        private int highScore = 0;
        private bool isGameOver;
        private string[] gameGrid = new string[16];

        public int Score
        {
            get { return score; }
            set { SetProperty(ref score, value); }
        }

        public int HighScore
        {
            get { return highScore; }
            set { SetProperty(ref highScore, value); }
        }

        public bool IsGameOver
        {
            get { return isGameOver; }
            set { SetProperty(ref isGameOver, value); }
        }

        public string[] GameGrid
        {
            get { return gameGrid; }
            set { SetProperty(ref gameGrid, value); }
        }
        public GameViewModel(Model gameModel)
        {
            model = gameModel;
            gameGrid = new string[16];
            ReadScore();
            InitNewGame();
        }
        public void InitNewGame()
        {
            Score = 0;
            IsGameOver = false;

            model.NewNum();
            model.NewNum();

            SyncWithModel();
        }
        public void OnKeyPress(Key e)
        {
            if (IsGameOver) return;

            switch (e)
            {
                case Key.Up:
                    model.MoveUp();
                    break;
                case Key.Down:
                    model.MoveDown();
                    break;
                case Key.Right:
                    model.MoveRight();
                    break;
                case Key.Left:
                    model.MoveLeft();
                    break;
                default: return;
            }

            model.NewNum();
            SyncWithModel();
            Score = model.NowScore();
            if (Score > HighScore) { HighScore = Score; WriteRecord(); }
            Notify();

            if (model.CanMove() == false && model.EmptyCellsNumber() == 0)
            {
                IsGameOver = true;
                WriteRecord();
                GameEnded?.Invoke($"Поздравляем! Игра завершена.\nВаш текущий счёт: {Score}\nНачать заново?");
            }
        }
        private void SyncWithModel()
        {
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    int index = (row * 4) + col;
                    int val = model.Arr[row][col];

                    gameGrid[index] = val == 0 ? "" : val.ToString();
                }
            }
        }
        private void Notify()
        {
            OnPropertyChanged("Score");
            OnPropertyChanged("GameGrid");
            OnPropertyChanged("HighScore");
        }
        private void WriteRecord()
        {
            string fileName = "score.txt";
            File.WriteAllText(fileName, HighScore.ToString());
        }
        private void ReadScore()
        {
            string fileName = "score.txt";
            if (File.Exists(fileName))
            {
                try
                {
                    HighScore = int.Parse(File.ReadAllText(fileName));
                }catch(Exception e) { WriteRecord(); }
            }
        }
    }
}