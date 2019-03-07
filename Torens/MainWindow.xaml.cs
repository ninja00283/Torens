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
using System.Diagnostics;

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

        int RowsTotal = 6;
        int StartColomn = 1;
        int Moves = 0;

        List<List<int>> Columns = new List<List<int>>();

        /// <summary>
        /// create the list of columns and then initialize all columns.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            HasClicked = false;

            for (int i = 0; i < 3; i++)
			{
                Columns.Add(new List<int>());
			}

            Piece_Button_0.Content = "Pick";
            Piece_Button_1.Content = "Pick";
            Piece_Button_2.Content = "Pick";

            Columns[0] = InitIntList(Columns[0],0);
            Columns[1] = InitIntList(Columns[1],RowsTotal);
            Columns[2] = InitIntList(Columns[2],0);
            UpdatePieces(0);
            UpdatePieces(1);
            UpdatePieces(2);
            InitColour(0, Brushes.Tan);
            InitColour(1, Brushes.Tan);
            InitColour(2, Brushes.Tan);
        }

        /// <summary>
        /// Update the given column and check if the player has won.
        /// </summary>
        /// <param name="columnNumber"> The column to update </param>
        public void UpdatePieces(int columnNumber)
        {
            Debug.Print(columnNumber.ToString() + "___" + Columns[columnNumber].Count.ToString());
            for (int i = 0; i < Columns[columnNumber].Count; i++)
            {
                Button Piece = this.FindName("Piece_" + i.ToString() + "_" + columnNumber.ToString()) as Button;
                Piece.Width = Columns[columnNumber][i];
            }
            for (int i = Columns[columnNumber].Count; i < RowsTotal; i++)
			{
                Button Piece = this.FindName("Piece_" + i.ToString() + "_" + columnNumber.ToString()) as Button;
                Piece.Width = 0;
			}

            if (columnNumber != StartColomn && Columns[columnNumber].Count == RowsTotal) {
                Win();
            }
        }

        /// <summary>
        /// Create a new window to show that the player has won and the moves it took.
        /// </summary>
        void Win() {
            Window1 window1 = new Window1();
            window1.Show();
            window1.MovesLabel.Content = "Moves: "+Moves.ToString();
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            window1.Left = (screenWidth / 2) - (windowWidth / 2);
            window1.Top = (screenHeight / 2) - (windowHeight / 2);
        }

        /// <summary>
        /// Take a int List and an int and add that many rows to the list.
        /// </summary>
        /// <param name="Array"> The array to add to. </param>
        /// <param name="size"> The number of rows to add. </param>
        /// <returns></returns>
        public List<int> InitIntList(List<int> intList, int size) {

            for (int i = 1; i < size+1; i++)
            {
                intList.Add(((size + 1)-i )*15);
            }

            return intList;
        }


        /// <summary>
        /// Sets every row in the selected column to a colour.
        /// </summary>
        /// <param name="currentColumn"> The column to colour in. </param>
        /// <param name="colour"> The colour to use. </param>
        public void InitColour(int currentColumn, Brush colour) {
            for (int i = 0; i < Columns[currentColumn].Count; i++)
            {
                Button Piece = this.FindName("Piece_" + i.ToString() + "_" + currentColumn.ToString()) as Button;
                Piece.Background = colour;
            }
        }

        /// <summary>
        /// Colours in the top piece in the seleced colour.
        /// </summary>
        /// <param name="currentColumn"> The column of the selected piece. </param>
        /// <param name="colour"> The colour to use. </param>
        public void ColourSelected(int currentColumn, Brush colour)
        {
            if (Columns[currentColumn].Count > 0)
            {
                Button Piece = this.FindName("Piece_" + (Columns[currentColumn].Count - 1).ToString() + "_" + currentColumn.ToString()) as Button;
                Piece.Background = colour;
            }
        }
        /// <summary>
        /// Moves the piece from one column to another.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MovePiece(object sender, RoutedEventArgs e) {

            ClickedPiece = sender as Button;

            if (HasClicked)
            {
                int columns0 = Grid.GetColumn(LastClickedPiece);
                int columns1 = Grid.GetColumn(ClickedPiece);
                if (Columns[columns0].Count > 0)
                {
                    if (Columns[columns1].Count > 0)
                    {
                        if (Columns[columns1][Columns[columns1].Count - 1] >= Columns[columns0][Columns[columns0].Count - 1])
                        {
                            Columns[columns1].Add(Columns[columns0][Columns[columns0].Count - 1]);
                            Columns[columns0].RemoveAt(Columns[columns0].Count - 1);
                            Piece_Button_0.Content = "Pick";
                            Piece_Button_1.Content = "Pick";
                            Piece_Button_2.Content = "Pick";
                            HasClicked = false;
                            InitColour(Grid.GetColumn(LastClickedPiece), Brushes.Tan);
                            InitColour(Grid.GetColumn(ClickedPiece), Brushes.Tan);
                            Moves++;
                        }
                    }
                    else
                    {
                        Columns[columns1].Add(Columns[columns0][Columns[columns0].Count - 1]);
                        Columns[columns0].RemoveAt(Columns[columns0].Count - 1);
                        Piece_Button_0.Content = "Pick";
                        Piece_Button_1.Content = "Pick";
                        Piece_Button_2.Content = "Pick";
                        HasClicked = false;
                        InitColour(Grid.GetColumn(LastClickedPiece), Brushes.Tan);
                        InitColour(Grid.GetColumn(ClickedPiece), Brushes.Tan);
                        Moves++;
                    }
                }
                else {
                    HasClicked = false;
                    ColourSelected(Grid.GetColumn(LastClickedPiece), Brushes.Tan);
                    InitColour(Grid.GetColumn(ClickedPiece), Brushes.Tan);
                }


                UpdatePieces(columns0);
                UpdatePieces(columns1);
            }
            else {
                HasClicked = true;
                Piece_Button_0.Content = "Place";
                Piece_Button_1.Content = "Place";
                Piece_Button_2.Content = "Place";

                ColourSelected(Grid.GetColumn(ClickedPiece), Brushes.DeepSkyBlue);

                LastClickedPiece = ClickedPiece;
            }
        }
    }
}
