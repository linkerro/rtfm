using System;
using System.Collections.Generic;
using System.Linq;

namespace MagicTrick
{
    class Program
    {
        static void Main(string[] args)
        {
            var deck = new Deck();
            deck.Shuffle();

        }
    }

    public class Deck
    {
        public List<string> CardList { get; set; }
        public List<string> Cut { get; set; } = new List<string>();
        public Deck()
        {
            var cardSuits = new List<string>
            {
                "♠",
                "♥",
                "♦",
                "♣"
            };

            var cardValues = new List<string>
            {
                "A",
                "2",
                "3",
                "4",
                "5",
                "6",
                "7",
                "8",
                "9",
                "10",
                "J",
                "Q",
                "K"
            };

            CardList = cardSuits
                    .SelectMany(suit => cardValues
                        .Select(cardValue => cardValue + suit))
                    .ToList();
        }

        public void Shuffle()
        {
            var rnJesus = new Random();
            for (int i = 0; i < CardList.Count; i++)
            {
                var temp = CardList[i];
                int randomIndex = rnJesus.Next(51);
                CardList[i] = CardList[randomIndex];
                CardList[randomIndex] = temp;
            }
        }

        public void Pop()
        {
            var card = CardList.First();
            CardList.RemoveAt(0);
            Cut.Insert(0, card);
        }

        public void Pop(int number)
        {
            for (int i = 0; i < number; i++)
            {
                Pop();
            }
        }

        public void Shuffle(int number)
        {
            for (int i = 0; i < number; i++)
            {

            }
        }

        public void Merge()
        {
            CardList.InsertRange(0, Cut);
            Cut = new List<string>();
        }

    }
}
