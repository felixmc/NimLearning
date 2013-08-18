using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NimLearning
{
    public class GameState : IEnumerable
    {
        private int[] rows = new int[3];

        public int this[int i]
        {
            get
            {
                return rows[i];
            }
            set
            {
                rows[i] = value;
            }
        }

        public GameState(int firstRowValue = 3, int secondRowValue = 5, int thirdRowValue = 7)
        {
            rows[0] = firstRowValue;
            rows[1] = secondRowValue;
            rows[2] = thirdRowValue;
        }

        public bool IsDone()
        {
            return rows.Sum() == 0;
        }

        public int GameSum()
        {
            return rows.Sum();
        }

        public IEnumerator GetEnumerator()
        {
            List<GameState> outcomes = new List<GameState>();

            // loop though rows
            for (int rowIndex = 0; rowIndex < rows.Length; rowIndex++)
            {
                // loop through possible values for each row
                for (int i = 0; i < rows[rowIndex]; i++)
                {
                    GameState outcome = new GameState();

                    outcome[0] = this[0];
                    outcome[1] = this[1];
                    outcome[2] = this[2];

                    outcome[rowIndex] = i;

                    yield return outcome;
                }
            }
        }

        public void Print()
        {
			const char GAME_PIECE_CHAR = 'X';

            Console.WriteLine();
            for (int rowIndex = 0; rowIndex < rows.Length; rowIndex++)
            {
                for (int pieceIndex = 0; pieceIndex < rows[rowIndex]; pieceIndex++)
                {
                    Console.Write( GAME_PIECE_CHAR );
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public string GetHashString()
        {
            return "" + rows[0] + rows[1] + rows[2];
        }

    }
}