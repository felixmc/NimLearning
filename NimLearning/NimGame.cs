using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NimLearning
{
    public class NimGame
    {
        private Tuple<Player, Player> players;
        private Player currentPlayer;
        private GameState currentState;
        private List<String> gameHistory;

        public NimGame(Player firstPlayer, Player secondPlayer, GameState startingGameState = null)
        {
            players = new Tuple<Player, Player>(firstPlayer, secondPlayer);
            currentPlayer = firstPlayer;

   			currentState = startingGameState == null ? new GameState() : startingGameState;

            gameHistory = new List<string>();
            gameHistory.Add(currentState.GetHashString());

            this.Play();
        }

        private void Play()
        {
            while (!currentState.IsDone())
            {
                currentState.Print();
                currentState = currentPlayer.GetMove(currentState);
                gameHistory.Add(currentState.GetHashString());
                AlternatePlayers();
            }

            Console.WriteLine("\nPlayer #" + (currentPlayer == players.Item1 ? "1" : "2") + " won!");
            Console.WriteLine();

            // determines scores and stores them in database
            for (int i = 1; i < gameHistory.Count; i++)
            {
                bool isWinningPlayer = currentPlayer == (i % 2 == 1 ? players.Item1 : players.Item2);

                int scoreSign, totalMoves;

                if (isWinningPlayer)
                {
                    scoreSign = 1;
                    totalMoves = (int) Math.Floor(gameHistory.Count/2.0);
                }
                else
                {
                    scoreSign = -1;
                    totalMoves = (int) Math.Ceiling(gameHistory.Count/2.0);
                }
                
                string key = gameHistory[i];
                StateDatabase.AddScore(key, (1.0 / totalMoves) * scoreSign);
            }
        }

        private void AlternatePlayers()
        {
            currentPlayer = currentPlayer.Equals(players.Item1) ? players.Item2 : players.Item1;
        }
    }
}
