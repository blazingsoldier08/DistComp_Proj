using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistComp_Proj.Components
{
    public class BSSAlgorithm
    {
        // 3. When a message is delivered, the clock is updated.
        // 1. All messages are time stamped by the sending process.
        // 2. 
        private int[] vectorClock = new int[10] {0,0,0,0,0,0,0,0,0,0};
        int localProcessID;
        public BSSAlgorithm(int localProcessNum)
        {
            localProcessID = localProcessNum;
        }
        public void IncrementClock()
        {

        }
        public void QueueMessage()
        {

        }
        public void CheckQueue()
        {

        }

    }
}
