using System.Linq;
using System.Windows.Controls;
using System.Windows;

namespace TicTacToe
{
    public partial class MainWindow : Window
    {
        // Właściwości gry
        public string[] Board { get; set; } = new string[9];
        public string CurrentPlayer { get; set; } = "X";
        public bool IsBotGame { get; set; } = false;

        private string playerX = "X";
        private string playerO = "O";

        // Event do obsługi zakończenia gry
        public event EventHandler<string> GameOver = delegate { };

        public MainWindow()
        {
            InitializeComponent();
        }

        // Start gry dwuosobowej
        private void StartTwoPlayerGame(object sender, RoutedEventArgs e)
        {
            IsBotGame = false;
            playerX = "X";
            playerO = "O";
            MainMenu.Visibility = Visibility.Collapsed;
            MainGrid.Visibility = Visibility.Visible;
            ResetGame();
        }

        // Start gry z botem
        private void StartBotGame(object sender, RoutedEventArgs e)
        {
            IsBotGame = true;
            playerX = "X";
            playerO = "O";
            MainMenu.Visibility = Visibility.Collapsed;
            MainGrid.Visibility = Visibility.Visible;
            ResetGame();
        }

        // Obsługuje kliknięcie przycisku na planszy
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int index = int.Parse(button.Name.Replace("Button", ""));

            if (Board[index] == null)
            {
                Board[index] = CurrentPlayer;
                button.Content = CurrentPlayer;

                if (IsWinner(CurrentPlayer))
                {
                    EndGame($"Gracz {CurrentPlayer} wygrywa!");
                    return;
                }

                if (IsDraw())
                {
                    EndGame("Remis!");
                    return;
                }

                CurrentPlayer = CurrentPlayer == "X" ? "O" : "X";

                if (IsBotGame && CurrentPlayer == "O")
                {
                    BotMove();
                }
            }
        }

        // Bot wykonuje swój ruch
        public void BotMove()
        {
            int botMove = GetBestMove();
            if (botMove >= 0)
            {
                Board[botMove] = "O";
                Button button = (Button)MainGrid.Children[botMove];
                button.Content = "O";

                if (IsWinner("O"))
                {
                    EndGame("Bot wygrywa!");
                    return;
                }

                if (IsDraw())
                {
                    EndGame("Remis!");
                    return;
                }

                CurrentPlayer = "X";
            }
        }

        // Znajduje najlepszy ruch dla bota
        private int GetBestMove()
        {
            int bestScore = int.MinValue;
            int move = -1;

            for (int i = 0; i < Board.Length; i++)
            {
                if (Board[i] == null)
                {
                    Board[i] = "O"; // Symulacja ruchu bota
                    int score = Minimax(false); // Sprawdzamy wynik ruchu
                    Board[i] = null; // Cofamy symulację

                    if (score > bestScore)
                    {
                        bestScore = score;
                        move = i;
                    }
                }
            }

            return move;
        }

        // Algorytm minimax do oceny ruchów bota
        private int Minimax(bool isMaximizing)
        {
            if (IsWinner("O")) return 1;
            if (IsWinner("X")) return -1;
            if (IsDraw()) return 0;

            int bestScore = isMaximizing ? int.MinValue : int.MaxValue;

            for (int i = 0; i < Board.Length; i++)
            {
                if (Board[i] == null)
                {
                    Board[i] = isMaximizing ? "O" : "X"; // Symulacja ruchu
                    int score = Minimax(!isMaximizing);
                    Board[i] = null; // Cofamy symulację

                    bestScore = isMaximizing
                        ? System.Math.Max(score, bestScore)
                        : System.Math.Min(score, bestScore);
                }
            }

            return bestScore;
        }

        // Sprawdza, czy dany gracz wygrał
        public bool IsWinner(string player)
        {
            // Przykładowa logika sprawdzania wygranej
            string[][] winConditions = new string[][]
            {
                new string[] { Board[0], Board[1], Board[2] },
                new string[] { Board[3], Board[4], Board[5] },
                new string[] { Board[6], Board[7], Board[8] },
                new string[] { Board[0], Board[3], Board[6] },
                new string[] { Board[1], Board[4], Board[7] },
                new string[] { Board[2], Board[5], Board[8] },
                new string[] { Board[0], Board[4], Board[8] },
                new string[] { Board[2], Board[4], Board[6] },
            };

            foreach (var condition in winConditions)
            {
                System.Diagnostics.Debug.WriteLine($"Sprawdzanie warunku: {condition[0]}, {condition[1]}, {condition[2]}");

                if (condition[0] == player && condition[1] == player && condition[2] == player)
                {
                    return true;
                }
            }

            return false;
        }

        // Sprawdza, czy gra zakończyła się remisem
        public bool IsDraw()
        {
            return Board.All(cell => cell != null) && !IsWinner("X") && !IsWinner("O");
        }

        // Sprawdza warunki wygranej i kończy grę
        public void CheckWinCondition(string player)
        {
            if (IsWinner(player))
            {
                EndGame($"Gracz {player} wygrywa!");
            }
            else if (IsDraw())
            {
                EndGame("Remis!");
            }
        }

        // Kończy grę i wywołuje zdarzenie
        private void EndGame(string message)
        {
            GameOver?.Invoke(this, message);
            MessageBox.Show(message);
            ReturnToMenu();
        }

        // Przechodzi do menu głównego
        private void ReturnToMenu()
        {
            MainMenu.Visibility = Visibility.Visible;
            MainGrid.Visibility = Visibility.Collapsed;
            ResetGame();
        }

        // Resetuje stan gry
        private void ResetGame()
        {
            CurrentPlayer = "X";
            Board = new string[9];

            MainGrid.Children.OfType<Button>().ToList().ForEach(button => button.Content = "");
        }
    }
}
