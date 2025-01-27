using NUnit.Framework;
using TicTacToe;
using System.Windows;

namespace TicTacToeTest
{
    [TestFixture]
    public class TicTacToeTests
    {
        private MainWindow mainWindow;

        [SetUp]
        public void SetUp()
        {
            // Uruchomienie aplikacji WPF na wątku STA
            Thread staThread = new Thread(() =>
            {
                var app = new Application();
                mainWindow = new MainWindow();
                app.Run(mainWindow); // Uruchamiamy MainWindow na wątku STA
            })
            {
                ApartmentState = ApartmentState.STA
            };
            staThread.Start();
            staThread.Join(); // Czekamy na zakończenie wątku
        }

        [Test]
        [STAThread]
        public void TestWinConditionRow()
        {
            // Arrange
            mainWindow.Board = new string[] { "X", "X", "X", null, null, null, null, null, null };

            // Act
            bool isWinner = mainWindow.IsWinner("X");

            // Assert
            Assert.That(isWinner, Is.True);
        }

        [Test]
        [STAThread]
        public void TestWinConditionColumn()
        {
            // Arrange
            mainWindow.Board = new string[] { "O", null, null, "O", null, null, "O", null, null };

            // Act
            bool isWinner = mainWindow.IsWinner("O");

            // Assert
            Assert.That(isWinner, Is.True, "Gracz 'O' powinien wygrać w kolumnie.");
        }

        [Test]
        [STAThread]
        public void TestWinConditionDiagonal()
        {
            // Arrange
            mainWindow.Board = new string[] { "X", null, null, null, "X", null, null, null, "X" };

            // Act
            bool isWinner = mainWindow.IsWinner("X");

            // Assert
            Assert.That(isWinner, Is.True, "Gracz 'X' powinien wygrać na przekątnej.");
        }

        [Test]
        [STAThread]
        public void TestDrawCondition()
        {
            // Arrange
            mainWindow.Board = new string[] { "X", "O", "X", "X", "X", "O", "O", "X", "O" };

            // Act
            bool isDraw = mainWindow.IsDraw();

            // Assert
            Assert.That(isDraw, Is.True, "Gra powinna zakończyć się remisem.");
        }

        [Test]
        [STAThread]
        public void TestBotMakesMove()
        {
            // Arrange
            mainWindow.IsBotGame = true;
            mainWindow.Board = new string[] { "X", null, null, null, "O", null, null, null, null };
            mainWindow.CurrentPlayer = "O";

            // Act
            mainWindow.BotMove();

            // Assert
            bool botMadeMove = mainWindow.Board[1] == "O" || mainWindow.Board[2] == "O" || mainWindow.Board[3] == "O";
            Assert.That(botMadeMove, Is.True, "Bot powinien wykonać ruch.");
        }

        [Test]
        [STAThread]
        public void TestEndGameWithWin()
        {
            // Arrange
            mainWindow.Board = new string[] { "X", "X", "X", null, null, null, null, null, null };

            // Act
            string resultMessage = null;
            mainWindow.GameOver += (sender, message) => resultMessage = message;  // Dodajemy oba argumenty (sender, message)

            // Uruchamiamy metodę w wątku UI
            Application.Current.Dispatcher.Invoke(() =>
            {
                mainWindow.CheckWinCondition("X");
            });

            // Przekonaj się, że komunikat o wygranej został ustawiony
            Assert.That(resultMessage, Is.EqualTo("Gracz X wygrywa!"), "Powinna zostać zwrócona poprawna wiadomość o wygranej.");
        }
    }
}
