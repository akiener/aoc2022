using MyExtensions;

namespace aoc2022 {
    public class CalorieCounting {
        static int MaxCount(IEnumerable<string> lines) {
            return AggregateElfCalories(lines)
                .Max();
        }

        static int TopThree(IEnumerable<string> lines) {
            return AggregateElfCalories(lines)
                .OrderDescending()
                .Take(3)
                .Sum();
        }

        static IEnumerable<int> AggregateElfCalories(IEnumerable<string> lines) {
            return lines.Split(it => it == "")
                .Select(
                    meals => meals.Select(it => int.Parse(it))
                        .Sum()
                );
        }

        IEnumerable<string> exampleInput = null!;
        IEnumerable<string> fileInput = null!;

        [SetUp]
        public void SetUp() {
            exampleInput = @"1000
                             2000
                             3000
                             
                             4000
                             
                             5000
                             6000
                             
                             7000
                             8000
                             9000
                             
                             10000"
                .Split("\n")
                .Select(it => it.Trim());
            fileInput = File.ReadLines("../../../input/day01");
        }

        [Test]
        public void Part1() {
            Assert.AreEqual(24000, CalorieCounting.MaxCount(exampleInput));
            Console.WriteLine(CalorieCounting.MaxCount(fileInput));
        }

        [Test]
        public void Part2() {
            Assert.AreEqual(45000, CalorieCounting.TopThree(exampleInput));
            Console.WriteLine(CalorieCounting.TopThree(fileInput));
        }
    }
}

namespace MyExtensions {
    public static class ListExtensions {
        public static List<List<T>> Split<T>(this IEnumerable<T> list, Func<T, bool> condition) {
            var splits = new List<List<T>>();
            var split = new List<T>();
            splits.Add(split);
            foreach (var item in list)
                if (condition(item)) {
                    split = new List<T>();
                    splits.Add(split);
                }
                else {
                    split.Add(item);
                }

            return splits;
        }
    }
}
