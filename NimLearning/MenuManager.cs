using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NimLearning
{
	public class MenuManager
	{
		public delegate void MenuItemAction();
		private Dictionary<String, MenuItemAction> menuActions;

		public int ActionsCount
		{
			get
			{
				return menuActions.Count;
			}
		}

		public MenuManager()
		{
			menuActions = new Dictionary<String, MenuItemAction>();
		}

		public void AddAction (String label, MenuItemAction action)
		{
			menuActions.Add(label, action);
		}

		public void ExecuteAction(String label)
		{
			if (menuActions[label] != null)
				menuActions[label]();
		}

		public void ExecuteAction(int actionIndex)
		{
			menuActions.ElementAt(actionIndex).Value.Invoke();
		}

		public IEnumerator<String> GetMenuLabels()
		{
			return menuActions.Keys.GetEnumerator();
		}
	}
}
