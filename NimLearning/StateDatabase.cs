using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace NimLearning
{
    public static class StateDatabase
    {
        private static Dictionary<String, MoveScore> stateDictionary = new Dictionary<string,MoveScore>();

        public static void AddScore(string key, double value)
        {
            if (!stateDictionary.ContainsKey(key))
            {
                stateDictionary.Add(key, new MoveScore());
            }

            stateDictionary[key].AddScore(value);
        }

        public static double GetAverage(string key)
        {
			if (!stateDictionary.ContainsKey(key))
				stateDictionary[key] = new MoveScore();

            return stateDictionary[key].GetAverage();
        }

        public static void Serialize(String filename)
        {
            using (FileStream fileStream = new FileStream(filename, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fileStream, stateDictionary);
            }
        }

        public static void Deserialize(String filename)
        {
            if (File.Exists(filename))
            {
                using (FileStream fileStream = new FileStream(filename, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    stateDictionary = (Dictionary<String, MoveScore>)formatter.Deserialize(fileStream);
                }
            }
        }

        public static void PrintStats()
        {
            int statsCount = 0;

            foreach (string key in stateDictionary.Keys)
            {
                Console.Write("\nState: " + key);
                Console.WriteLine("\tScore: " + GetAverage(key));
                statsCount += stateDictionary[key].MoveCount;
            }

            Console.WriteLine("\n# of Game States: " + stateDictionary.Keys.Count);
            Console.WriteLine("# of Scores: " + statsCount);
        }
    }
}