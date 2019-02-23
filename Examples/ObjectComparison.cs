using System;
using System.Collections.Generic;
using System.Linq;

namespace Examples
{
    internal class ObjectComparison
    {
        internal static void Do()
        {
            var person1 = new Person
            {
                Id = 1,
                FirstName = "Gita",
                LastName = "Popescu",
                Email = "Ghita@gmail.com",
                Address1 = "Str. Aviatiei, bl. G2",
                Address2 = "et. 3, ap.2"
            };
            var person2 = new Person
            {
                Id = 2,
                FirstName = "Geta",
                LastName = "Popescu",
                Email = "geta@gmail.com",
                Address1 = "Str. Aviatiei, bl. G2",
                Address2 = "et. 3, ap.2"
            };

            CrackHash(person1);

            var areEaqual = PersonCompare(person1, person2);
        }

        private static bool PersonCompare(Person person1, Person person2)
        {
            var person1String = GetHash(person1);
            var person2String = GetHash(person2);

            return person1String == person2String;
        }

        private static int GetHash(Person person)
        {
            var personString = $"{person.FirstName}{person.LastName}{person.Gender}{person.Email}{person.Address1}{person.Address2}";
            return personString.GetHashCode();
        }

        private static void CrackHash(Person person)
        {
            var personString = $"{person.FirstName}{person.LastName}{person.Gender}{person.Email}{person.Address1}{person.Address2}";
            var characters = Enumerable.Range(32, 126).Select(i => (char)i).ToList();
            var collisions = new List<string>();
            for (var i = 0; i < 100; i++)
            {
                var strings = GetStrings("", i);
                var collidingStrings = strings.AsParallel().Where(s => GetHash(person) == s.GetHashCode()).ToArray();
                collisions.AddRange(collidingStrings);
                if (collisions.Count > 10)
                {
                    break;
                }
            }
        }

        public static IEnumerable<string> GetStrings(string str, int maxDepth, int depth=0)
        {
            var characters = Enumerable.Range(32, 126).Select(i => (char)i).ToList();
            if (maxDepth == depth)
            {
                //return characters.Select(c => str + c).ToArray();
                for(int i = 0; i < characters.Count; i++)
                {
                    yield return str + characters[i];
                }
            }
            else
            {
                for (int i = 0; i < characters.Count; i++)
                {
                    foreach (var result in GetStrings(str + characters[i], maxDepth, depth + 1))
                    {
                        yield return result;
                    }
                }
            }
            //return characters.SelectMany(c => GetStrings(str + c,maxDepth,depth+1)).ToArray();
        }
    }

    internal class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
    }
}