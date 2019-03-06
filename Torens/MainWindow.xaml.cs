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

namespace Torens
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Button ClickedPiece;
        Button LastClickedPiece;
        bool HasClicked;
        bool FirstClicked;

        bool[] Column0 = new bool[9];
        bool[] Column1 = new bool[9];
        bool[] Column2 = new bool[9];

        public MainWindow()
        {
            InitializeComponent();
            HasClicked = false;
            FirstClicked = false;
            InitBoolArray(Column0, true);
            InitBoolArray(Column0, false);
            InitBoolArray(Column0, false);
        }

        public void UpdatePieces(bool[] CurrentColumn, int ColumnNumber)
        {
            for (int i = 0; i < CurrentColumn.Length; i++)
            {
                Button Piece = FindName("Piece_" + i.ToString() + "_" + ColumnNumber.ToString()) as Button;
                Piece.IsEnabled = CurrentColumn[i];
            }
        }
        /// <summary>
        /// Take a bool array and a bool to set the array to
        /// </summary>
        /// <param name="Array"></param>
        /// <param name="Initializer"></param>
        /// <returns></returns>
        public bool[] InitBoolArray(bool[] Array, bool Initializer) {

            for (int i = 0; i < Array.Length; i++)
            {
                Array[i] = Initializer;
            }

            return Array;
        }

        public void MovePiece(object sender, RoutedEventArgs e) {
            if (FirstClicked)
            {
                LastClickedPiece = ClickedPiece;
            }
            else {
                FirstClicked = true;
            }
            ClickedPiece = sender as Button;

            if (HasClicked)
            {
                ClickedPiece.IsEnabled = true;
                LastClickedPiece.IsEnabled = false;
                HasClicked = false;
            }
            else {
                HasClicked = true;
            }
        }
    }
}
