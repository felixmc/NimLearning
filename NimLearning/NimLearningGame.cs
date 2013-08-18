using ConsoleTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NimLearning
{
    public class NimLearningGame
    {
        private static string DATABASE_FILE = "data.bin";
		private MenuManager menu;
		private bool keepPlaying = true;

        public NimLearningGame()
        {
            StateDatabase.Deserialize(DATABASE_FILE);
			menu = new MenuManager();
			SetupMenu();
        }

		private void SetupMenu()
		{
			menu.AddAction("Player vs. AI", new MenuManager.MenuItemAction(HumanGame));
			menu.AddAction("AI vs. AI", new MenuManager.MenuItemAction(AIGame));
			menu.AddAction("View Database Statistics", new MenuManager.MenuItemAction(StateDatabase.PrintStats));
			menu.AddAction("Exit", new MenuManager.MenuItemAction(StopPlaying));
		}

		private void SelectMenuAction()
		{
			Console.WriteLine("\nMenu Options:");

			using (IEnumerator<String> labelIterator = menu.GetMenuLabels())
			{
				int index = 0;
				while (labelIterator.MoveNext())
				{
					Console.WriteLine(++index + ". " + labelIterator.Current);
				}
			}

			menu.ExecuteAction(ConsoleIO.GetInt("\nChoose an option: ", 1, menu.ActionsCount) - 1);
		}

        public void Start()
        {
            do
            {
               SelectMenuAction();
            } while (keepPlaying);

            StateDatabase.Serialize(DATABASE_FILE);
        }

		private void StopPlaying()
		{
			keepPlaying = false;
		}

        public void HumanGame()
        {
            NewGame(new HumanPlayer(), new AIPlayer());
        }

        public void AIGame()
        {
			int gameCount = SelectNumGames();
            for (int i = 0; i < gameCount; i++)
            {
                NewGame(new AIPlayer(), new AIPlayer());
            }
        }

        public void NewGame(Player firstPlayer, Player secondPlayer, GameState startingGameState = null)
        {
            NimGame nimGame = new NimGame(firstPlayer, secondPlayer, startingGameState);            
        }

        private int SelectNumGames()
        {
            return ConsoleIO.GetInt("How many games to play: ", 1);
        }
    }
}