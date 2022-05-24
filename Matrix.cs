using System;
using System.Collections.Generic;
using System.Linq;

namespace Задание__2
{
    public class Matrix
    {
        public int[,] matrix;

        public int count;

        public delegate bool Function(int row, int column);
        
        public Matrix(int count = 10) => this.resetMatrix(count);

        public Matrix newMatrix()
        {
            for (int i = 0; i < this.count; ++i)
                for (int j = 0; j < this.count; ++j)
                    matrix[i, j] = new Random().Next(0, 100);
            return this;
        }

        public Matrix resetMatrix(int count = 10)
        {
            this.matrix = new int[count, count];
            this.count = count;
            for (int i = 0; i < count; ++i)
                for (int j = 0; j < count; ++j)
                    matrix[i, j] = new Random().Next(0, 100);
            return this;
        }
        public int getMatrix(int i, int j) => (i >= 0) ? ((j >= 0) ? matrix[i, j] : -1) : -1;

        private int[] getDataFromMatrix(Function f)
        {
            int summa = 0, minimal = 100, maximal = 0;
            for (int row = 0; row <= this.count; ++row)
            {
                for (int column = 0; column <= this.count; ++column)
                {
                    if (f(row, column))
                    {
                        var elem = this.matrix[row, column];
                        summa += elem;
                        if (elem > maximal) { maximal = elem; }
                        if (elem < minimal) { minimal = elem; }
                    }
                }
            }
            return new[]{summa, maximal, minimal}.ToArray();
        }

        public bool sideD(int row, int column)             => (column == this.count - row - 1 && column != this.count && row != this.count);
        public bool mainD(int row, int column)             => (row == column && column != this.count && row != this.count);
        public bool topT(int row, int column)              => (row < (this.count / 2 + 1) && column >= row && column <= (this.count - row - 1));
        public bool bottomT(int row, int column)           => (row >= this.count / 2 && row < this.count && column >= this.count - row - 1 && column <= row);
        public bool leftT(int row, int column)             => (column < (this.count / 2 + 1) && row >= column && row <= (this.count - column - 1));
        public bool rightT(int row, int column)            => (column < this.count && column >= this.count / 2 && row >= (this.count - column - 1) && row <= column);

        public int[] getSideDiagonal()   => this.getDataFromMatrix(this.sideD);
        public int[] getMainDiagonal()   => this.getDataFromMatrix(this.mainD);

        public int[] getTopTriangle()    => this.getDataFromMatrix(this.topT);
        public int[] getBottomTriangle() => this.getDataFromMatrix(this.bottomT);
        public int[] getLeftTriangle()   => this.getDataFromMatrix(this.leftT);
        public int[] getRightTriangle()  => this.getDataFromMatrix(this.rightT);

        // public int[] getTopTriangle()
        // {
        //     int summa = 0, minimal = 100, maximal = 0;
        //     for (int row = 0; row < this.count / 2 + 1; ++row)
        //     {
        //         for (int column = row; column <= this.count - row - 1; ++column)
        //         {
        //             var elem = this.matrix[row, column];
        //             summa += elem;
        //             if (minimal > elem) { minimal = elem; }
        //             if (maximal < elem) { maximal = elem; }
        //         }
        //     }
        //     return new[]{summa, maximal, minimal}.ToArray();
        // }

        // public int[] getBottomTriangle()
        // {
        //     int summa = 0, minimal = 100, maximal = 0;
        //     for (int row = this.count / 2; row < this.count; ++row)
        //     {
        //         for (int column = this.count - row - 1; column <= row; ++column)
        //         {
        //             var elem = this.matrix[row, column];
        //             summa += elem;
        //             if (minimal > elem) { minimal = elem; }
        //             if (maximal < elem) { maximal = elem; }
        //         }
        //     }
        //     return new[]{summa, maximal, minimal}.ToArray();
        // }

        // public int[] getLeftTriangle()
        // {
        //     int summa = 0, maximal = 0, minimal = 100;
        //     for (int column = 0; column < this.count / 2 + 1; ++column)
        //     {
        //         for (int row = column; row <= this.count - column - 1; ++row)
        //         {
        //             var elem = this.matrix[row, column];
        //             summa += elem;
        //             if (minimal > elem) { minimal = elem; }
        //             if (maximal < elem) { maximal = elem; }
        //         }
        //     }
        //     return new[]{summa, maximal, minimal}.ToArray();
        // }

        // public int[] getRightTriangle()
        // {
        //     int summa = 0, maximal = 0, minimal = 100;
        //     for (int column = this.count / 2; column < this.count; ++column)
        //     {
        //         for (int row = this.count - column - 1; row <= column; ++row)
        //         {
        //             var elem = this.matrix[row, column];
        //             summa += elem;
        //             if (minimal > elem) { minimal = elem; }
        //             if (maximal < elem) { maximal = elem; }
        //         }
        //     }
        //     return new[]{summa, maximal, minimal}.ToArray();
        // }
    }
}