using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace TicTacToe
{
    public partial class MainWindow : Window
    {
        private Button[,] buttons;
        private bool playerXTurn;
        private bool gameEnded;

        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            buttons = new Button[3, 3] { { button1, button2, button3 },
                                          { button4, button5, button6 },
                                          { button7, button8, button9 } };

            foreach (var button in buttons)
            {
                button.Content = "";
                button.IsEnabled = true;
            }

            playerXTurn = true;
            gameEnded = false;
            UpdateStatus("Ход крестиков");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (gameEnded)
                return;

            var button = (Button)sender;

            if (button.Content.ToString() != "")
                return;

            button.Content = playerXTurn ? "X" : "O";
            button.IsEnabled = false;

            if (CheckForWinner())
            {
                UpdateStatus(playerXTurn ? "КРАСАВА СЛЫШЬ!" : "ЛОООООООХХХ!");
                gameEnded = true;
                return;
            }

            if (CheckForDraw())
            {
                UpdateStatus("Ничья!");
                gameEnded = true;
                return;
            }

            playerXTurn = !playerXTurn;

            if (!playerXTurn)
            {
                MakeRobotMove();
                if (CheckForWinner())
                {
                    UpdateStatus(playerXTurn ? "КРАСАВА СЛЫШЬ!" : "ЛОООООООХХХ!");
                    gameEnded = true;
                    return;
                }

                if (CheckForDraw())
                {
                    UpdateStatus("Ничья!");
                    gameEnded = true;
                    return;
                }

                playerXTurn = true;
            }

            UpdateStatus(playerXTurn ? "Ход крестиков" : "Ход ноликов");
        }

        private void MakeRobotMove()
        {
            var emptyButtons = new List<Button>();
            foreach (var button in buttons)
            {
                if (button.Content.ToString() == "")
                {
                    emptyButtons.Add(button);
                }
            }

            if (emptyButtons.Count > 0)
            {
                Random random = new Random();
                int index = random.Next(emptyButtons.Count);
                emptyButtons[index].Content = "O";
                emptyButtons[index].IsEnabled = false;
            }
        }

        private bool CheckForWinner()
        {
            for (int i = 0; i < 3; i++)
            {
                if (!string.IsNullOrEmpty(buttons[i, 0].Content.ToString()) &&
                    buttons[i, 0].Content == buttons[i, 1].Content &&
                    buttons[i, 1].Content == buttons[i, 2].Content)
                {
                    return true;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                if (!string.IsNullOrEmpty(buttons[0, i].Content.ToString()) &&
                    buttons[0, i].Content == buttons[1, i].Content &&
                    buttons[1, i].Content == buttons[2, i].Content)
                {
                    return true;
                }
            }

            if (!string.IsNullOrEmpty(buttons[0, 0].Content.ToString()) &&
                buttons[0, 0].Content == buttons[1, 1].Content &&
                buttons[1, 1].Content == buttons[2, 2].Content)
            {
                return true;
            }

            if (!string.IsNullOrEmpty(buttons[0, 2].Content.ToString()) &&
                buttons[0, 2].Content == buttons[1, 1].Content &&
                buttons[1, 1].Content == buttons[2, 0].Content)
            {
                return true;
            }

            return false;
        }

        private bool CheckForDraw()
        {
            foreach (var button in buttons)
            {
                if (button.Content.ToString() == "")
                {
                    return false;
                }
            }
            return true;
        }

        private void UpdateStatus(string message)
        {
            statusLabel.Content = message;
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            InitializeGame();
        }
    }
}
