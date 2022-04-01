

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary
{
    public struct TestResult
    {
        public int stupidBotPoints;
        public int stupidBotHighScore;
        public int ehBotPoints;
        public int ehBotHighScore;
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
                result.stupidBotPoints = stupidBot.score;
            }

            if (ehBot.gameNumber < numGames)
            {
                ehBot.MoveLocal();
                result.ehBotHighScore = ehBot.highScore;
                result.ehBotPoints = ehBot.score;
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
