

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary
{
    public struct TestResult
    {
        public int stupidBotHighScore;
        public int ehBotHighScore;
        public int stupidBotPoints;
        public int ehBotPoints;
    };

    public static class LordOfTheBots
    {
        public static MovementBot movementBot;
        public static StupidBot stupidBot;
        public static ehBot ehBot;

        public static bool bigYEET;


        static LordOfTheBots()
        {
            //movementBot = new MovementBot();
            stupidBot = new StupidBot(false);
            ehBot = new ehBot(false);
        }

        public static TestResult testBots(int numGames)
        {
            TestResult result = new TestResult();

            if (stupidBot.gameNumber < numGames)
            {
                stupidBot.MoveLocal();
                result.stupidBotHighScore = stupidBot.highScore;
                result.ehBotPoints = stupidBot//get current score;
            }

            if (stupidBot.gameNumber < numGames)
            {
                ehBot.MoveLocal();
                result.ehBotPoints = ehBot.highScore;
            }

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
