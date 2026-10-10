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

        // Обычные поля для хранения данных
        private int score;
        private int highScore = 0;
        private bool isGameOver;
        private string[] gameGrid = new string[16];

        // Свойства, которые отдают данные в интерфейс
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

        // Конструктор
        public GameViewModel(Model gameModel)
        {
            model = gameModel;
            gameGrid = new string[16]; // Создаем сетку 4 на 4
            ReadScore();
            InitNewGame();
        }

        // Запуск новой игры
        public void InitNewGame()
        {
            Score = 0;
            IsGameOver = false;

            model.NewNum();
            model.NewNum();

            SyncWithModel();
        }

        // Метод, который вызывается при нажатии клавиш
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
                default: break;
            }

            model.NewNum();
            SyncWithModel();
            Score = model.NowScore();
            if (Score > HighScore) HighScore = Score;
            Notify();

            if (model.CanMove() == false && model.EmptyCellsNumber() == 0)
            {
                IsGameOver = true;
                WriteRecord();
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

                    // Если ноль — делаем плитку визуально пустой
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