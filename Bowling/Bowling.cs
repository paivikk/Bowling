using System;
using System.Collections.Generic;
using System.Linq;

namespace Bowling
{
    class Frame
    {
        /// <summary>
        /// Costructs frame object from the given int(s).
        /// </summary>
        /// <param name="deliveries">List of scores from each delivery in the frame</param>
        public Frame(List<int> deliveries)
        {
            First = deliveries[0];
            Second = deliveries.Count() > 1 ? deliveries[1] : 0;

            BaseScore = deliveries.Sum();

            Strike = First == 10;
            Spare = Strike ? false : First + Second == 10;
        }

        public int BaseScore { get; }
        public int First { get; set; }
        public int Second { get; set; }
        public bool Strike { get; }
        public bool Spare { get; }
    }
    class Game
    {
        private List<Frame> frames;

        /// <summary>
        /// Costructs game object by parsing the given input. 
        /// Basic validation for the input is done in the process.
        /// Input is expected in specified format:
        /// [a1|b1] [a2|b2] [a3|b3] ... [an|bn|cn]
        /// </summary>
        /// <param name="input">Input in expected format</param>
        public Game(string input)
        {
            frames = new List<Frame>();
            input = input.Replace("[", "").Replace("]", "");
            var frameStrings = input.Split(' ');

            for (int i = 0; i < frameStrings.Length; i++)
            {
                List<int> frame = new List<int>();
                foreach (var scoreString in frameStrings[i].Split('|'))
                {
                    if (!int.TryParse(scoreString, out int score))
                        throw new ArgumentException($"Failed to parse '{scoreString}' into int.");
                    if (score < 0 || score > 10) throw new ArgumentException($"Invalid score entry {score} in game input {input}.");

                    frame.Add(score);
                }

                if (frame.Count > 2 && i < frameStrings.Length - 1)
                    throw new ArgumentException($"Invalid amount of frames ({frame.Count}) in game input ({input}) in frame {i}.");
                if (frame.Sum() > 10 && i < frameStrings.Length - 1)
                    throw new ArgumentException($"Invalid score ({frame.Sum()}) in game input ({input}) in frame {i}.");
                if (frame.Count > 3 && i == frameStrings.Length - 1)
                    throw new ArgumentException($"Invalid amount of frames ({frame.Count}) in game input ({input}) in the last frame.");

                frames.Add(new Frame(frame));
            }
        }

        public int GetScore()
        {
            var gameScore = 0;

            for (int i = 0; i < frames.Count(); i++)
            {
                var baseScore = frames[i].BaseScore;

                var bonusScore = 0;

                // calculate bonusScore in case of strike
                if (frames[i].Strike)
                {
                    // all frames until the last two
                    if (i < frames.Count() - 2)
                    {
                        bonusScore = frames[i + 1].Strike ? frames[i + 1].First + frames[i + 2].First : frames[i + 1].BaseScore;
                    }
                    // second to last frame
                    else if (i == frames.Count() - 2)
                    {
                        bonusScore = frames[i + 1].First + frames[i + 1].Second;
                    }

                    // in the last frame, bonus (if present) is already calculated into basescore
                }
                // calculate bonusScore in case of spare
                else if (frames[i].Spare)
                {
                    bonusScore = i < frames.Count() - 1 ? frames[i + 1].First : 0;
                }

                gameScore = gameScore + baseScore + bonusScore;
            }

            return gameScore;
        }

    }
    class Bowling
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine($"Unexpected amount of arguments: {args.Length}, expected 1.\n");
                Console.WriteLine("Usage:\nBowling \"[a1|b1] [a2|b2] [a3|b3] ... [an|bn|cn]\"\n");
                Console.WriteLine("Examples:");
                Console.WriteLine("Bowling \"[7|3] [5|5] [0|10] [1|1] [0|0] [0|0] [0|0] [0|0] [0|0] [0|0]\"");
                Console.WriteLine("Bowling \"[10] [10] [10] [10] [10] [10] [10] [10] [10] [10|10|10]\"");  
                return;
            }

            Game game = new Game(args[0]);
            var score = game.GetScore();

            Console.WriteLine($"Game score: {score}");
        }
    }
}
