

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary
{
    public struct TestResult
    {
        int stupidBotPoints;
        int ehBotPoints;
    };

    public class LordOfTheBots
    {
        public MovementBot movementBot;
        public StupidBot stupidBot;
        public ehBot ehBot;

        public LordOfTheBots()
        {
            movementBot = new MovementBot();
            //stupidBot = new StupidBot();
            //ehBot = new ehBot();
        }

        public TestResult testBots(int numGames)
        {
            TestResult result = new TestResult();



            return result;
        }

        public void Move(Direction direction)
        {
            movementBot.Move(direction);
        }
    }
}
