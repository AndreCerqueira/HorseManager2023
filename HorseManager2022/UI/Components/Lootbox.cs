﻿using HorseManager2022.Enums;
using HorseManager2022.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseManager2022.UI.Components
{
    internal class Lootbox
    {
        // Constants
        private const float HORSE_CHANCE = 0.6f;
        private const int CARD_QUANTITY = 5;
        private const int DURATION = 300;
        private const int POSITION_Y = 13;
        public static readonly int LOOTBOX_PRICE = 500;

        // Properties
        private List<Card> cards { get; set; }
        private int movementCount { get; set; }
        private int delay = 40;


        // Constructor
        public Lootbox()
        {
            cards = new List<Card>();

            for (int i = 0; i < CARD_QUANTITY; i++)
                AddCard();
        }

        
        // Methods
        public void AddCard()
        {
            bool isHorseCard = GameManager.GetRandomDouble() < HORSE_CHANCE;

            if (isHorseCard)
            {
                cards.Add(new Card(GetStartingPosition(), POSITION_Y, new Horse(true), true));
                cards[^1].horse!.price = cards[^1].horse!.price / 2;
            }
            else
            {
                cards.Add(new Card(GetStartingPosition(), POSITION_Y, new Jockey(true), true));
                cards[^1].jockey!.price = cards[^1].jockey!.price / 2;
            }

        }


        public void RemoveFirstCard() => cards.RemoveAt(0);
        

        public int GetStartingPosition()
        {
            int position = 10;

            if (cards.Count > 0)
                position = cards.Last().x + 30;

            return position;
        }
        
        
        public Card Open()
        {
            Arrow arrow = new(21, 52, 1);
            
            Audio.PlayLootboxSong();

            while (movementCount < DURATION)
            {
                Console.Clear();

                arrow.Draw();

                for (int i = cards.Count - 1; i >= 0; i--)
                {
                    Card card = cards[i];

                    card.Move(-5);

                    if (card.x >= 5)
                        card.Draw();
                    else
                    {
                        RemoveFirstCard();
                        AddCard();
                    }
                }

                Thread.Sleep(delay);
                movementCount += 5;

                if (movementCount % 10 == 0)
                    delay += 10;
            }
            
            Card selectedCard = cards[2];

            if (selectedCard.horse != null)
                PlayRaritySound(selectedCard.horse.rarity);
            else if (selectedCard.jockey != null)
                PlayRaritySound(selectedCard.jockey.rarity);

            Console.ReadKey();
            Console.Clear();

            // Remove all cards except the second one
            selectedCard.isSelected = true;
            cards.Clear();
            cards.Add(selectedCard);
            selectedCard.Draw();
            
            Console.ReadKey();

            Audio.PlayTownSong();

            return selectedCard;
        }


        private void PlayRaritySound(Rarity rarity)
        {
            switch (rarity)
            {
                case Rarity.Common:
                    Audio.PlayCommonSong();
                    break;
                case Rarity.Rare:
                    Audio.PlayRareSong();
                    break;
                case Rarity.Epic:
                    Audio.PlayEpicSong();
                    break;
                case Rarity.Legendary:
                    Audio.PlayLegendarySong();
                    break;
                case Rarity.Special:
                    Audio.PlaySpecialSong();
                    break;
                default:
                    break;
            }
        }
        
    }
}
