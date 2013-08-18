using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NimLearning
{
    [Serializable]
    public class MoveScore
    {
        public int MoveCount { get; private set; }
        private double sum;

        public MoveScore()
        {
            MoveCount = 0;
            sum = 0;
        }

        public void AddScore(double score)
        {
            sum += score;
            MoveCount++;
        }

        public double GetAverage()
        {
            return MoveCount == 0 ? 0 : sum / MoveCount;
        }
    }
}
