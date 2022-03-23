

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

    public static class LordOfTheBots
    {
        public static MovementBot movementBot;
        public static StupidBot stupidBot;
        public static ehBot ehBot;

        public static bool bigYEET;

        static LordOfTheBots()
        {
            ehBot = new ehBot();
            movementBot = new MovementBot();
            stupidBot = new StupidBot();
        }

        public static TestResult testBots(int numGames)
        {
            TestResult result = new TestResult();

            return result;
        }

        public static void Move(Direction direction)
        {

            movementBot.Move(direction);
        }

        public static void MoveStupid()
        {
            stupidBot.Move();
        }

        public static void MoveEh()
        {
            ehBot.Move();
        }
    }
}
