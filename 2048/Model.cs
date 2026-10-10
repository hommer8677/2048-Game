using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Windows.Navigation;

namespace _2048
{
    public class Model
    {
        public List<List<int>> Arr { get; private set; } = new List<List<int>>()
        {
            new List<int> { 0, 0, 0, 0 },
            new List<int> { 0, 0, 0, 0 },
            new List<int> { 0, 0, 0, 0 },
            new List<int> { 0, 0, 0, 0 }
        };

        Random rnd = new Random();
        public Model()
        {
            
        }
        public int EmptyCellsNumber()
        {
            int cells = 0;
            for(int i = 0; i < Arr.Count; i++)
            {
                for(int j = 0; j < Arr[i].Count; j++) if(Arr[i][j] == 0) cells++;
            }
            return cells;
        }
        public void NewNum()
        {
            int emptyCells = EmptyCellsNumber();
            if (emptyCells == 0) return;

            int cell = rnd.Next(emptyCells); //выбрал пустую клетку по "порядковому номеру"
            int cellIdx = 0;

            for(int i = 0; i < Arr.Count; i++)
            {
                for(int j = 0; j < Arr[i].Count; j++)
                {
                    if(Arr[i][j] == 0)
                    {
                        if(cellIdx == cell)
                        {
                            Arr[i][j] = rnd.Next(10) == 0 ? 4 : 2;
                            return;
                        }
                        cellIdx++;
                    }
                }
            }
        }

        public bool MoveUp()
        {
            bool moved = false;
            //перебираю столбцы сверху вниз и пытаюсь двигать элементы вверх
            for(int col = 0; col < 4; col++)
            {
                bool[] merged = new bool[Arr.Count];
                for (int row = 1; row<Arr.Count; row++) //нет смысла двигать верхнюю строку
                {
                    if (Arr[row][col] == 0) continue;
                    int r = row;
                    
                    while(r > 0 && Arr[r - 1][col] == 0)
                    {
                        (Arr[r - 1][col], Arr[r][col]) = (Arr[r][col], Arr[r - 1][col]);
                        r--;
                        moved = true;
                    }
                    if(r>0 && Arr[r - 1][col] == Arr[r][col] && !merged[r - 1])
                    {
                        Arr[r - 1][col] *= 2;
                        Arr[r][col] = 0;
                        merged[r - 1] = true;
                        moved = true;
                    }
                }
            }
            return moved;
        }
        public bool MoveDown()
        {
            bool moved = false;
            for (int col = 0; col < 4; col++)
            {
                bool[] merged = new bool[Arr.Count];
                for (int row = 2; row >= 0; row--) //нет смысла двигать нижнюю строку
                {
                    if (Arr[row][col] == 0) continue;
                    int r = row;

                    while (r < 3 && Arr[r + 1][col] == 0)
                    {
                        (Arr[r + 1][col], Arr[r][col]) = (Arr[r][col], Arr[r + 1][col]);
                        r++;
                        moved = true;
                    }
                    if (r < 3 && Arr[r + 1][col] == Arr[r][col] && !merged[r + 1])
                    {
                        Arr[r + 1][col] *= 2;
                        Arr[r][col] = 0;
                        merged[r + 1] = true;
                        moved = true;
                    }
                }
            }
            return moved;
        }
        public bool MoveRight()
        {
            bool moved = false;
            for (int row = 0; row < Arr.Count; row++)
            {
                bool[] merged = new bool[Arr[row].Count];

                for (int col = 2; col >= 0; col--)
                {
                    if (Arr[row][col] == 0) continue;
                    int c = col;

                    while (c < 3)
                    {
                        if (Arr[row][c + 1] == 0)
                        {
                            (Arr[row][c + 1], Arr[row][c]) = (Arr[row][c], Arr[row][c + 1]);
                            c++;
                            moved = true;
                        }
                        else if (Arr[row][c + 1] == Arr[row][c] && !merged[c + 1])
                        {
                            Arr[row][c + 1] *= 2;
                            Arr[row][c] = 0;
                            merged[c + 1] = true;
                            moved = true;
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            return moved;
        }

        public bool MoveLeft()
        {
            bool moved = false;
            for (int row = 0; row < Arr.Count; row++)
            {
                bool[] merged = new bool[Arr[row].Count];
                for (int col = 1; col < Arr[row].Count; col++)
                {
                    if (Arr[row][col] == 0) continue;
                    int c = col;

                    while (c > 0 && Arr[row][c - 1] == 0)
                    {
                        (Arr[row][c - 1], Arr[row][c]) = (Arr[row][c], Arr[row][c - 1]);
                        c--;
                        moved = true;
                    }

                    if (c > 0 && Arr[row][c - 1] == Arr[row][c] && !merged[c-1])
                    {
                        Arr[row][c - 1] *= 2;
                        Arr[row][c] = 0;
                        merged[c - 1] = true;
                        moved = true;
                    }
                }
            }
            return moved;
        }

        public bool CanMove() => EmptyCellsNumber() == 0 ? false : true;
        public int NowScore()
        {
            int score = 2;
            for(int i = 0; i < Arr.Count; i++)
            {
                if(Arr[i].Max() > score) score = Arr[i].Max();
            }
            return score;
        }
    }
}
