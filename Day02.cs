namespace aoc2022 {
    public class RockPaperScissors {
        readonly Dictionary<char, int> opponentMoves;
        readonly Dictionary<char, int> ownMoves;
        readonly Dictionary<char, int> desiredOutcomes;
        readonly Dictionary<int, char> indexToOwnMove;

        public RockPaperScissors() {
            opponentMoves = new Dictionary<char, int> {
                {'A', 0},
                {'B', 1},
                {'C', 2},
            };
            ownMoves = new Dictionary<char, int> {
                {'X', 0},
                {'Y', 1},
                {'Z', 2},
            };
            desiredOutcomes = new Dictionary<char, int> {
                {'X', -1},
                {'Y', 0},
                {'Z', 1},
            };

            indexToOwnMove = ownMoves.ToDictionary(it => it.Value, it => it.Key);
        }

        static int PlayStrategyGuide(IEnumerable<string> lines, Func<char, char, int> playFn) {
            return lines.Select(it => it.Split(" "))
                .Select(strings => strings.Select(it => it.ToCharArray()[0]).ToList())
                .Select(it => playFn(it[0], it[1]))
                .Sum();
        }

        int PlayHand(char opponentMove, char ownMove) {
            var opponentValue = opponentMoves[opponentMove];
            var ownValue = ownMoves[ownMove];
            if (opponentValue == ownValue) {
                return ownValue + 1 + 3;
            }

            if ((opponentValue + 1) % 3 == ownValue % 3) {
                return ownValue + 1 + 6;
            }

            return ownValue + 1 + 0;
        }

        int PlayHandV2(char opponentMove, char desiredOutcome) {
            var opponentValue = opponentMoves[opponentMove];
            var ownMoveIndex = (opponentValue + desiredOutcomes[desiredOutcome] + 3) % 3;

            return PlayHand(opponentMove, indexToOwnMove[ownMoveIndex]);
        }

        IEnumerable<string> exampleInput = null!;
        IEnumerable<string> fileInput = null!;

        [SetUp]
        public void SetUp() {
            exampleInput = @"A Y
                             B X
                             C Z"
                .Split("\n")
                .Select(it => it.Trim());
            fileInput = File.ReadLines("../../../input/day02");
        }

        [Test]
        public void Part1() {
            Assert.AreEqual(15, PlayStrategyGuide(exampleInput, PlayHand));
            Console.WriteLine(PlayStrategyGuide(fileInput, PlayHand));
        }

        [Test]
        public void Part2() { 
            Assert.AreEqual(12, PlayStrategyGuide(exampleInput, PlayHandV2));
            Console.WriteLine(PlayStrategyGuide(fileInput, PlayHandV2));
        }
    }
}
