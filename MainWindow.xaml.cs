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

namespace Tic_Tac_Toe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Private Members

        // Array that holds the results of the cells in the current game
        private MarkType[] Results;

        // True if it is player 1's turn (X), False if player 2's turn (O)
        private bool Player1Turn;

        // True if the game has ended
        private bool GameEnded;

        private bool Winner = false;

        #endregion

        #region Main Window Default Constructor
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }

        #endregion


        private void NewGame()
        {
            // Creates new array for the 9 free cells
            Results = new MarkType[9];

            for (int i = 0; i < Results.Length; i++)
            {
                Results[i] = MarkType.Free;
            }

            Player1Turn = true;

            // B/c everything in grid are buttons can Cast type Button to all children within playArea
            // Converts the children of playArea from a UIElement to Button
            playArea.Children.Cast<Button>().ToList().ForEach(button =>
            {

                //Changes Content, Background, and Foreground to default values
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });

            GameEnded = false;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            // Starts a new game on the click after its finished
            if(GameEnded)
            {
                NewGame();
                Winner = false;
                return;
            }

            // Casts sender as a button
            var button = (Button)sender;

            // Finds the buttons position in the array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);

            // Button click does not do anything if cell already has a value in it
            if(Results[index] != MarkType.Free)
            {
                return;
            }

            // Sets the cell Value based on which players turn it is
            Results[index] = Player1Turn ? MarkType.Cross : MarkType.Circle;

            // Sets button text to the result
            button.Content = Player1Turn ? "X" : "O";

            // Sets Circle color to red
            if (!Player1Turn)
            {
                button.Foreground = Brushes.Red;
            }

            // Toggles Player1Turn between true and false with ^=
            Player1Turn ^= true;

            CheckForWinner();
        }

        private void CheckForWinner()
        {
            /* Checks for horizontal wins
            /  Single & is a bitwise operator which checks Results[0] if its true or false. Then it moves on to Results[1], finally Results[2]. So if Results[0] = true and Results[1] = false, then 
            /  Results[0] and Results[1] = false. Checks if the three values are the same
           */

            #region Horizontal Wins
            // Checks Row 0 
            if (Results[0] != MarkType.Free && (Results[0] & Results[1] & Results[2]) == Results[0])
            {
                // Game ends
                GameEnded = true;
                Winner = true;

                // Highlights winning row
                topLeft.Background = topMid.Background = topRight.Background = Brushes.Green;
            }

            // Checks Row 1
            if (Results[3] != MarkType.Free && (Results[3] & Results[4] & Results[5]) == Results[3])
            {
                // Game ends
                GameEnded = true;
                Winner = true;

                // Highlights winning row
                midLeft.Background = midMid.Background = midRight.Background = Brushes.Green;
            }

            // Checks Row 2
            if (Results[6] != MarkType.Free && (Results[6] & Results[7] & Results[8]) == Results[6])
            {
                // Game ends
                GameEnded = true;
                Winner = true;

                // Highlights winning row
                botLeft.Background = botMid.Background = botRight.Background = Brushes.Green;
            }

            #endregion


            #region Vertical Wins
            // Checks Column 0
            if (Results[0] != MarkType.Free && (Results[0] & Results[3] & Results[6]) == Results[0])
            {
                // Game ends
                GameEnded = true;
                Winner = true;

                // Highlights winning row
                topLeft.Background = midLeft.Background = botLeft.Background = Brushes.Green;
            }

            // Checks Column 1
            if (Results[1] != MarkType.Free && (Results[1] & Results[4] & Results[7]) == Results[1])
            {
                // Game ends
                GameEnded = true;
                Winner = true;

                // Highlights winning row
                topMid.Background = midMid.Background = botMid.Background = Brushes.Green;
            }

            // Checks Column 2
            if (Results[2] != MarkType.Free && (Results[2] & Results[5] & Results[8]) == Results[2])
            {
                // Game ends
                GameEnded = true;
                Winner = true;

                // Highlights winning row
                topRight.Background = midRight.Background = botRight.Background = Brushes.Green;
            }

            #endregion

            #region Diagonal Wins

            // Checks Diagonal 
            // Checks Top Left to Bottom Right
            if (Results[0] != MarkType.Free && (Results[0] & Results[4] & Results[8]) == Results[0])
            {
                // Game ends
                GameEnded = true;
                Winner = true;

                // Highlights winning row
                topLeft.Background = midMid.Background = botRight.Background = Brushes.Green;
            }

            // Checks Top Right to Bottom Left
            if (Results[2] != MarkType.Free && (Results[2] & Results[4] & Results[6]) == Results[2])
            {
                // Game ends
                GameEnded = true;
                Winner = true;

                // Highlights winning row
                topRight.Background = midMid.Background = botLeft.Background = Brushes.Green;
            }

            #endregion


            #region No Winner
            // If all cells are filled with no winner, then game ends and all cells turn orange
            if (Winner == true)
            {
                GameEnded = true;
            }                
            else if (!Results.Any(result => result == MarkType.Free))
            {
                GameEnded = true;

                // Turns all cells orange 
                playArea.Children.Cast<Button>().ToList().ForEach(button =>
                { 
                    button.Background = Brushes.Orange;
                   
                });

                MessageBox.Show("No Winner! Click anywhere to play again!");
            }

            #endregion

           
        }


    }
}
