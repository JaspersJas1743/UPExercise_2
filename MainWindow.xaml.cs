using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Задание__2
{
    public static class Theme { public static bool isDarkTheme = false; }

    public partial class MainWindow : Window
    {
        private static int count = 10;
        private static Matrix matrix = new Matrix(count);

        public MainWindow()
        {
            InitializeComponent();
            setMatrix().setData();
        }
    
        private MainWindow setMatrix()
        {
            MatrixGrid.Rows = count;
            MatrixGrid.Columns = count;
            for (int i = 0; i < count; ++i)
            {
                for (int j = 0; j < count; ++j)
                {
                    var txt = new TextBox();
                    txt.Style = (Style)this.FindResource("MyTextBox");
                    txt.Text = matrix.getMatrix(i, j).ToString();
                    txt.FontSize = 12 + 15 - count;
                    MatrixGrid.Children.Add(txt);
                }
            }
            matrixElem.Text = count.ToString();
            return this;
        }
        
        private void resetMatrix(object sender, RoutedEventArgs e) => this.reset();

        private MainWindow reset()
        {
            try
            {
                int tableRAC;
                try { tableRAC = Convert.ToInt32(matrixElem.Text); } catch { throw new Exception("Некорректное значение для матрицы"); }
                if (!Enumerable.Range(3, 13).Contains(tableRAC)) { throw new Exception("Матрциа может быть в диапазоне [3, 15]"); }
                if (tableRAC != count)
                {
                    MatrixGrid.Children.Clear();
                    count = tableRAC;
                    matrix.resetMatrix(count);
                    setMatrix().setData();
                } else { newRandomValue().setData(); }
            } catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка преобразований", MessageBoxButton.OK, MessageBoxImage.Error); }
            return this;
        }

        private MainWindow setData()
        {
            var mainD       = matrix.getMainDiagonal();
            var sideD       = matrix.getSideDiagonal();
            var topT        = matrix.getTopTriangle();
            var bottomT     = matrix.getBottomTriangle();
            var leftT       = matrix.getLeftTriangle();
            var rightT      = matrix.getRightTriangle();

            sumMain.Text    = $"Сумма элементов: {mainD[0]}";
            sumSide.Text    = $"Сумма элементов: {sideD[0]}";
            sumTop.Text     = $"Сумма элементов: {topT[0]}";
            sumBottom.Text  = $"Сумма элементов: {bottomT[0]}";
            sumLeft.Text    = $"Сумма элементов: {leftT[0]}";
            sumRight.Text   = $"Сумма элементов: {rightT[0]}";
            
            maxMain.Text    = $"Максимальный элемент: {mainD[1]}";
            maxSide.Text    = $"Максимальный элемент: {sideD[1]}";
            maxTop.Text     = $"Максимальный элемент: {topT[1]}";
            maxBottom.Text  = $"Максимальный элемент: {bottomT[1]}";
            maxLeft.Text    = $"Максимальный элемент: {leftT[1]}";
            maxRight.Text   = $"Максимальный элемент: {rightT[1]}";
            
            minMain.Text    = $"Минимальный элемент: {mainD[2]}";
            minSide.Text    = $"Минимальный элемент: {sideD[2]}";
            minTop.Text     = $"Минимальный элемент: {topT[2]}";
            minBottom.Text  = $"Минимальный элемент: {bottomT[2]}";
            minLeft.Text    = $"Минимальный элемент: {leftT[2]}";
            minRight.Text   = $"Минимальный элемент: {rightT[2]}";
            
            return this;
        }

        private void newRandomValueClick(object sender, RoutedEventArgs e) => newRandomValue().setData();

        private MainWindow newRandomValue()
        {
            int row = 0, column = 0;
            matrix.newMatrix();
            foreach (TextBox box in MatrixGrid.Children)
            {
                box.Text = matrix.getMatrix(row, column).ToString();
                ++column;
                if (column >= count) { ++row; column = 0; }
            }

            return this;
        }

        public delegate bool Function(int row, int column);

        private void HighlightMainDiagonal(object sender, MouseEventArgs e)     => this.Highlight(bool (int row, int column) => (row == column), (Style)this.FindResource("MyHighlightTextBox"));
        private void ReturnMainDiagonal(object sender, MouseEventArgs e)        => this.Highlight(bool (int row, int column) => (row == column), (Style)this.FindResource("MyTextBox"));

        private void HighlightSideDiagonal(object sender, MouseEventArgs e)     => this.Highlight(bool (int row, int column) => (column == (matrix.count - row - 1)), (Style)this.FindResource("MyHighlightTextBox"));
        private void ReturnSideDiagonal(object sender, MouseEventArgs e)        => this.Highlight(bool (int row, int column) => (column == (matrix.count - row - 1)), (Style)this.FindResource("MyTextBox"));

        private void HighlightBottomTriangle(object sender, MouseEventArgs e)   => this.Highlight(bool (int row, int column) => (row >= matrix.count / 2 && row < matrix.count && column >= matrix.count - row - 1 && column <= row), (Style)this.FindResource("MyHighlightTextBox"));
        private void ReturnBottomTriangle(object sender, MouseEventArgs e)      => this.Highlight(bool (int row, int column) => (row >= matrix.count / 2 && row < matrix.count && column >= matrix.count - row - 1 && column <= row), (Style)this.FindResource("MyTextBox"));

        private void HighlightTopTriangle(object sender, MouseEventArgs e)      => this.Highlight(bool (int row, int column) => (row < (matrix.count / 2 + 1) && column >= row && column <= (matrix.count - row - 1)), (Style)this.FindResource("MyHighlightTextBox"));
        private void ReturnTopTriangle(object sender, MouseEventArgs e)         => this.Highlight(bool (int row, int column) => (row < (matrix.count / 2 + 1) && column >= row && column <= (matrix.count - row - 1)), (Style)this.FindResource("MyTextBox"));

        private void HighlightRightTriangle(object sender, MouseEventArgs e)    => this.Highlight(bool (int row, int column) => (column < matrix.count && column >= matrix.count / 2 && row >= (matrix.count - column - 1) && row <= column), (Style)this.FindResource("MyHighlightTextBox"));
        private void ReturnRightTriangle(object sender, MouseEventArgs e)       => this.Highlight(bool (int row, int column) => (column < matrix.count && column >= matrix.count / 2 && row >= (matrix.count - column - 1) && row <= column), (Style)this.FindResource("MyTextBox"));

        private void HighlightLeftTriangle(object sender, MouseEventArgs e)    => this.Highlight(bool (int row, int column) => (column < (matrix.count / 2 + 1) && row >= column && row <= (matrix.count - column - 1)), (Style)this.FindResource("MyHighlightTextBox"));
        private void ReturnLeftTriangle(object sender, MouseEventArgs e)       => this.Highlight(bool (int row, int column) => (column < (matrix.count / 2 + 1) && row >= column && row <= (matrix.count - column - 1)), (Style)this.FindResource("MyTextBox"));

        private MainWindow Highlight(Function func, Style style)
        {
            int row = 0, column = 0;
            foreach (TextBox box in MatrixGrid.Children)
            {
                if (func(row, column)) { box.Style = style; }
                ++column;
                if (column >= count) { ++row; column = 0; }
            }
            return this;
        }

        private void NewTheme(object sender, RoutedEventArgs e)
        {
            Uri uri;
            if (!Theme.isDarkTheme) { uri = new Uri(@"..\Themes\DarkTheme.xaml", UriKind.Relative); }
            else { uri = new Uri(@"..\Themes\LightTheme.xaml", UriKind.Relative); }
            Theme.isDarkTheme = !(Theme.isDarkTheme);
            ResourceDictionary? resDict = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resDict);
            this.Highlight(bool (int row, int column) => true, (Style)this.FindResource("MyTextBox"));
        }

        private void BoxKeyDown(object sender, KeyEventArgs e) { if (e.Key == Key.Enter) { this.reset(); } }

        private void Exit(object sender, RoutedEventArgs e)     => Application.Current.Shutdown();

        private void Deact(object sender, RoutedEventArgs e)    => Application.Current.MainWindow.WindowState = WindowState.Minimized;

        private void Drag(object sender, RoutedEventArgs e) { if (Mouse.LeftButton == MouseButtonState.Pressed) { Application.Current.MainWindow.DragMove(); } }
    }
}