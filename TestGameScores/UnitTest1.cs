using System;
using Xunit;
using Task1;
using Xunit.Abstractions;

namespace TestTask1
{
    public class UnitTest1
    {

        private readonly ITestOutputHelper output;

        public UnitTest1(ITestOutputHelper output)
        {
            this.output = output;
        }
        static Game generateGame()
        {
            Game game = new Game();
            game.gameStamps = new GameStamp[Game.TIMESTAMPS_COUNT];

            GameStamp currentStamp = new GameStamp(0, 0, 0);
            for (int i = 0; i < Game.TIMESTAMPS_COUNT; i++)
            {
                game.gameStamps[i] = currentStamp;
                currentStamp = game.generateGameStamp(currentStamp);
            }

            return game;
        }

        Game game = generateGame();

        [Fact]

        public void IsZero()
        {
            int my_offset = 0;
            Score myScore = game.getScore(my_offset);
            Assert.Equal(0, myScore.home);
            Assert.Equal(0, myScore.away);
            output.WriteLine($"{myScore}");
        }


        [Fact]
        public void IsNegative()
        {
            int my_offset = -40000;
            Score myScore = game.getScore(my_offset);
            Assert.Equal(0, myScore.home);
            Assert.Equal(0, myScore.away);
            output.WriteLine($"{myScore}");
           

        }

        [Fact]
        public void InArray()
        {
            int my_offset = 75001; // this value < max(array)      
            Score myScore = game.getScore(my_offset);
            output.WriteLine($"{myScore}");

        }

        [Fact]
        public void NotInArray()
        {
            int my_offset = 900000; // this value > max(array)
            Score myScore = game.getScore(my_offset);
            output.WriteLine($"{myScore}");

        }
        



    }
}
