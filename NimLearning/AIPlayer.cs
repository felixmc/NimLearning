using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NimLearning
{
    public class AIPlayer : Player
    {
        private static Random r = new Random();
        public GameState GetMove(GameState currentGameState)
        {
            Console.WriteLine("AI Turn");
            return BestMove(currentGameState);
        }

        private GameState BestMove(GameState currentGameState)
        {
            List<GameState> bestMoves = new List<GameState>();

            foreach (GameState possibleGameState in currentGameState)
            {
                if (possibleGameState.GameSum() == 1)
                {
					bestMoves = new List<GameState>();
					bestMoves.Add(possibleGameState);
					break;
                }

                if (bestMoves.Count == 0)
                {
                    bestMoves.Add(possibleGameState);
                }
                else
                {
                    double bestScore = StateDatabase.GetAverage(possibleGameState.GetHashString());
                    double possibleGameStateScore = StateDatabase.GetAverage(possibleGameState.GetHashString());

                    if (possibleGameStateScore > bestScore)
                    {
                        bestMoves = new List<GameState>();
                        bestMoves.Add(possibleGameState);
                    }
                    else if (possibleGameStateScore == bestScore)
                    {
                        bestMoves.Add(possibleGameState);
                    }
                }
            }

            return bestMoves[r.Next(bestMoves.Count)];
        }
    }
}
