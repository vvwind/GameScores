using System;

namespace Task1
{
    class App
    {
        static void Main(string[] args)
        {
            Game.task1();

        }
    }
    public struct Score
    {
        public int home;
        public int away;

        public Score(int home, int away)
        {
            this.home = home;
            this.away = away;
        }
        public override string ToString()
        {
            return $"{this.home} - {this.away}";
        }
    }

    public struct GameStamp
    {
        public int offset;
        public Score score;
        public GameStamp(int offset, int home, int away)
        {
            this.offset = offset;
            this.score = new Score(home, away);
        }
    }

    public class Game
    {
        public const int TIMESTAMPS_COUNT = 50000;

        const double PROBABILITY_SCORE_CHANGED = 0.0001;

        const double PROBABILITY_HOME_SCORE = 0.45;

        const int OFFSET_MAX_STEP = 3;

        public static int my_offset = 80001; // setting an offset value

        public GameStamp[] gameStamps;

        public static int home_score = 0;
        public static int away_score = 0;


        public Game()
        {
            this.gameStamps = new GameStamp[] { };
        }

        public Game(GameStamp[] gameStamps)
        {
            this.gameStamps = gameStamps;
        }

        public GameStamp generateGameStamp(GameStamp previousValue)
        {
            Random rand = new Random();

            bool scoreChanged = rand.NextDouble() > 1 - PROBABILITY_SCORE_CHANGED;
            int homeScoreChange = scoreChanged && rand.NextDouble() > 1 - PROBABILITY_HOME_SCORE ? 1 : 0;

            int awayScoreChange = scoreChanged && homeScoreChange == 0 ? 1 : 0;
            int offsetChange = (int)(Math.Floor(rand.NextDouble() * OFFSET_MAX_STEP)) + 1;

            return new GameStamp(
                previousValue.offset + offsetChange,
                previousValue.score.home + homeScoreChange,
                previousValue.score.away + awayScoreChange
                );
        }

        static Game generateGame()
        {
            Game game = new Game();
            game.gameStamps = new GameStamp[TIMESTAMPS_COUNT];

            GameStamp currentStamp = new GameStamp(0, 0, 0);
            for (int i = 0; i < TIMESTAMPS_COUNT; i++)
            {
                game.gameStamps[i] = currentStamp;
                currentStamp = game.generateGameStamp(currentStamp);
            }

            return game;
        }

        public static void task1()
        {
            Game game = generateGame();
            game.printGameStamps();
            Score myScore = game.getScore(my_offset);

            Console.WriteLine($"Result at the moment {my_offset} :");
            Console.WriteLine(myScore);

        }


        void printGameStamps()
        {
            foreach (GameStamp stamp in this.gameStamps)
            {
                Console.WriteLine($"{stamp.offset}: {stamp.score.home}-{stamp.score.away}");
            }

        }
        public Score getScore(int offset)
        {

            Score result = new Score(0, 0);

            if (offset < 0)
            {
                offset = 0;
            }

            foreach (GameStamp stamp in this.gameStamps)
            {


                if (offset == stamp.offset) // returning value from array
                {

                    result.home = stamp.score.home;
                    result.away = stamp.score.away;
                    return result;
                }
                else if (stamp.offset < offset) // saving temporary value
                {
                    result.home = stamp.score.home;
                    result.away = stamp.score.away;

                }

                else if (stamp.offset > offset) // returning last value
                {
                    return result;
                }

            }
            return result;
        }

    }
}