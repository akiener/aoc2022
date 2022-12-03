using MyExtensions;

namespace aoc2022 {
    public class RucksackReorganization {
        static int FindSharedItems(IEnumerable<string> lines) {
            return lines.ToList()
                .Select(line => {
                    HashSet<char> left = new();
                    HashSet<char> right = new();

                    var halfLength = line.Length / 2;
                    var leftHalf = line[..halfLength];
                    var rightHalf = line[halfLength..];
                    foreach (var c in leftHalf.ToCharArray()) {
                        left.Add(c);
                    }

                    foreach (var c in rightHalf.ToCharArray()) {
                        right.Add(c);
                    }

                    return left.First(c => right.Contains(c))
                        .Let(ToValue);
                })
                .Sum();
        }

        static int FindSharedBadges(IEnumerable<string> lines) {
            return lines
                .ToList()
                .Group3()
                .ToList()
                .Select(threeLines => {
                    HashSet<char> first = new();
                    HashSet<char> second = new();
                    HashSet<char> third = new();

                    foreach (var c in threeLines.First().ToCharArray()) {
                        first.Add(c);
                    }

                    foreach (var c in threeLines[1].ToCharArray()) {
                        second.Add(c);
                    }

                    foreach (var c in threeLines.Last().ToCharArray()) {
                        third.Add(c);
                    }

                    return first.First(c => second.Contains(c) && third.Contains(c))
                        .Let(ToValue);
                })
                .Sum();
        }

        static int ToValue(char c) {
            var value = (int) c;

            if (value >= 97) {
                return value - 96;
            }

            if (value >= 65) {
                return value - 64 + 26;
            }

            throw new Exception("unsupported char value");
        }

        IEnumerable<string> exampleInput = null!;
        IEnumerable<string> fileInput = null!;

        [SetUp]
        public void SetUp() {
            exampleInput = @"vJrwpWtwJgWrhcsFMMfFFhFp
                             jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
                             PmmdzqPrVvPwwTWBwg
                             wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
                             ttgJtRGJQctTZtZT
                             CrZsJsPPZsGzwwsLwLmpwMDw"
                .Split("\n")
                .Select(it => it.Trim());
            fileInput = File.ReadLines("../../../input/day03");
        }

        [Test]
        public void Part1() {
            Assert.AreEqual(157, FindSharedItems(exampleInput));
            Console.WriteLine(FindSharedItems(fileInput));
        }

        [Test]
        public void Part2() {
            Assert.AreEqual(70, FindSharedBadges(exampleInput));
            Console.WriteLine(FindSharedBadges(fileInput));
        }
    }
}

namespace MyExtensions {
    public static class GenericExtension {
        public static TOutput Let<TInput, TOutput>(this TInput value, Func<TInput, TOutput> function) {
            return function(value);
        }

        public static List<List<T>> Group3<T>(this List<T> list) {
            var groups = new List<List<T>>();
            var group = new List<T>();

            var index = 0;
            foreach (var item in list) {
                group.Add(item);

                if (index % 3 == 2) {
                    groups.Add(group);
                    group = new List<T>();
                    index = 0;
                }
                else {
                    index++;
                }
            }

            return groups;
        }
    }
}
