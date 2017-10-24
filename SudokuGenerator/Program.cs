using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Board myBoard = new Board(16);
            myBoard.Fill();
            Console.WriteLine(myBoard);
            Console.ReadLine();
        }
    }

    public class Board
    {
        private int[,] _arr;
        private int _size;
        private int _sqrtSize;

        public Board(int size)
        {
            if(Math.Sqrt(size) % 1 != 0)
            {
                throw new Exception("Board sizes must be perfect squares!");
            }
            _arr = new int[size,size];
            _size = size;
            _sqrtSize = (int)Math.Sqrt(size);
        }

        public void Fill()
        {
            FillHelper(0, 0);
        }

        private bool FillHelper(int x, int y)
        {
            int[] possibilities = Enumerable.Range(1, _size).ToArray();
            new Random().Shuffle(possibilities);
            for(int i=0; i<_size; i++)
            {
                _arr[x, y] = possibilities[i];
                if (!Check())
                {
                    continue;
                }
                if(x+1 == _size && y+1 == _size)
                {
                    // We've reached the last cell and filled it
                    return true;
                }

                int newX = x + 1;
                int newY = y;
                if(newX == _size)
                {
                    newX = 0;
                    newY++;
                }
                if (!FillHelper(newX, newY))
                {
                    continue;
                }
                else
                {
                    // Keep returning back
                    return true;
                }
            }
            _arr[x, y] = 0;
            return false;
        }

        private bool Check()
        {
            int[] check = new int[_size];

            // Check all rows for duplicates (excluding zero)
            for (int x=0; x<_size; x++)
            {
                for(int y = 0; y<_size; y++)
                {
                    check[y] = _arr[x, y];
                }
                if(!NoDuplicates(check))
                {
                    return false;
                }
            }

            // Check all columns for duplicates (excluding zero)
            for (int y = 0; y < _size; y++)
            {
                for (int x = 0; x < _size; x++)
                {
                    check[x] = _arr[x, y];
                }
                if (!NoDuplicates(check))
                {
                    return false;
                }
            }

            // Check all boxes for duplicates
            for (int xBox = 0; xBox < _sqrtSize; xBox++)
            {
                for (int yBox = 0; yBox < _sqrtSize; yBox++)
                {
                    int count = 0;
                    for(int x=xBox * _sqrtSize; x < (xBox + 1) * _sqrtSize; x++)
                    {
                        for (int y = yBox * _sqrtSize; y < (yBox + 1) * _sqrtSize; y++)
                        {
                            check[count++] = _arr[x, y];
                        }
                    }
                    if (!NoDuplicates(check))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool NoDuplicates(int[] check)
        {
            return !check.Where(i => i != 0).GroupBy(x => x).Any(g => g.Count() > 1);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            int rowLength = _arr.GetLength(0);
            int colLength = _arr.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    sb.Append(string.Format("{0:X} ", _arr[i, j]-1));
                }

                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }
    }
}
