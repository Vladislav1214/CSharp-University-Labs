using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab_2
{
    public class GameLogic
    {
        public readonly int BoardSize = 4;
        public int[,] Board { get; private set; }
        public int Score { get; private set; }
        public bool IsGameOver { get; private set; }

        private Random _random = new Random();

        public GameLogic()
        {
            Board = new int[BoardSize, BoardSize];
            Score = 0;
            IsGameOver = false;
        }

        public void StartNewGame()
        {
            Board = new int[BoardSize, BoardSize];
            Score = 0;
            IsGameOver = false;

            AddRandomTile();
            AddRandomTile();
        }

        public void AddRandomTile()
        {
            var emptyCells = new List<Tuple<int, int>>();
            for (int r = 0; r < BoardSize; r++)
            {
                for (int c = 0; c < BoardSize; c++)
                {
                    if (Board[r, c] == 0)
                    {
                        emptyCells.Add(Tuple.Create(r, c));
                    }
                }
            }

            if (emptyCells.Any())
            {
                var cell = emptyCells[_random.Next(emptyCells.Count)];
                Board[cell.Item1, cell.Item2] = _random.Next(10) < 9 ? 2 : 4;
            }
        }

        public bool Move(Direction direction)
        {
            if (IsGameOver) return false;

            int[,] boardBeforeMove = (int[,])Board.Clone();

            for (int i = 0; i < (int)direction; i++)
            {
                RotateBoard();
            }

            for (int r = 0; r < BoardSize; r++)
            {
                int[] row = GetRow(r);
                int[] mergedRow = MergeRow(row);
                SetRow(r, mergedRow);
            }

            for (int i = 0; i < 4 - (int)direction; i++)
            {
                RotateBoard();
            }

            bool boardChanged = !AreBoardsEqual(boardBeforeMove, Board);

            if (boardChanged)
            {
                AddRandomTile();
            }

            CheckForGameOver();

            return boardChanged;
        }

        private void CheckForGameOver()
        {
            for (int r = 0; r < BoardSize; r++)
            {
                for (int c = 0; c < BoardSize; c++)
                {
                    if (Board[r, c] == 0)
                    {
                        IsGameOver = false;
                        return;
                    }
                }
            }

            for (int r = 0; r < BoardSize; r++)
            {
                for (int c = 0; c < BoardSize; c++)
                {
                    if ((c < BoardSize - 1 && Board[r, c] == Board[r, c + 1]) ||
                        (r < BoardSize - 1 && Board[r, c] == Board[r + 1, c]))
                    {
                        IsGameOver = false;
                        return;
                    }
                }
            }

            IsGameOver = true;
        }

        private int[] MergeRow(int[] row)
        {
            var nonZero = row.Where(x => x != 0).ToList();
            var newRow = new List<int>();

            for (int i = 0; i < nonZero.Count; i++)
            {
                if (i + 1 < nonZero.Count && nonZero[i] == nonZero[i + 1])
                {
                    int mergedValue = nonZero[i] * 2;
                    newRow.Add(mergedValue);
                    Score += mergedValue;
                    i++;
                }
                else
                {
                    newRow.Add(nonZero[i]);
                }
            }

            while (newRow.Count < BoardSize)
            {
                newRow.Add(0);
            }

            return newRow.ToArray();
        }

        private void RotateBoard()
        {
            int[,] newBoard = new int[BoardSize, BoardSize];
            for (int r = 0; r < BoardSize; r++)
            {
                for (int c = 0; c < BoardSize; c++)
                {
                    newBoard[r, c] = Board[c, BoardSize - 1 - r];
                }
            }
            Board = newBoard;
        }

        private int[] GetRow(int rowIndex)
        {
            int[] row = new int[BoardSize];
            for (int c = 0; c < BoardSize; c++)
            {
                row[c] = Board[rowIndex, c];
            }
            return row;
        }

        private void SetRow(int rowIndex, int[] row)
        {
            for (int c = 0; c < BoardSize; c++)
            {
                Board[rowIndex, c] = row[c];
            }
        }

        private bool AreBoardsEqual(int[,] board1, int[,] board2)
        {
            for (int r = 0; r < BoardSize; r++)
            {
                for (int c = 0; c < BoardSize; c++)
                {
                    if (board1[r, c] != board2[r, c])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }

    public enum Direction
    {
        Left = 0,
        Up = 1,
        Right = 2,
        Down = 3
    }
}
