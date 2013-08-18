using ConsoleTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NimLearning
{
    public class HumanPlayer : Player
    {
        public GameState GetMove(GameState state)
        {
            Console.WriteLine("Human Turn");

			bool rowIsInvalid;
			int rowIndex;

			do
			{
				rowIndex = ConsoleIO.GetInt("Select row: ", 1, 3) - 1;
				rowIsInvalid = state[rowIndex] == 0;
			} while (rowIsInvalid);

			int count = ConsoleIO.GetInt("Select count: ", 1, state[rowIndex]);

			state[rowIndex] -= count;

            return state;
        }
    }
}
