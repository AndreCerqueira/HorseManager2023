﻿using HorseManager2022.Models;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseManager2022.UI
{
    internal class ScreenMenu : Screen
    {
        // Constructor

        public ScreenMenu(string title, Screen? previousScreen = null)
            : base(title, previousScreen)
        {
            // AddOption(new Option("Start new game", () => StartNewGame()));
            // AddOption(new Option("Load game", () => LoadGame()));
            // AddOption(new Option("Exit", () => Exit()));
        }


        override public Screen? Show(Player? player)
        {
            // Variables
            string title = this.title;
            string mark = "";
            title = title.PadLeft((37 / 2) + (title.Length / 2)).PadRight(37);

            // Wait for option
            Option? selectedOption = WaitForOption(() => {

                // Display Title
                Console.Clear();
                Console.WriteLine("+---------------------------------------+");
                Console.WriteLine("| " + title + " |");
                Console.WriteLine("+---------------------------------------+");
                Console.WriteLine("|                                       |");

                // Display Options
                for (int i = 0; i < this.options.Count; i++)
                {
                    string text = this.options[i].text.PadRight(32, ' ');
                    mark = (i == this.selectedPosition) ? "X" : " ";
                    Console.WriteLine("| [" + mark + "] - " + text + "|");
                    Console.WriteLine("|                                       |");
                }

                // Display Back / Exit Option
                mark = (this.options.Count == this.selectedPosition) ? "X" : " ";
                if (this.isInitialScreen)
                    Console.WriteLine("| [" + mark + "] - Exit                            |");
                else
                    Console.WriteLine("| [" + mark + "] - Back                            |");

                // Close Menu
                Console.WriteLine("|                                       |");
                Console.WriteLine("+---------------------------------------+");

            });
            
            selectedOption.onEnter();
            return selectedOption.nextScreen;

        }


        // Select option from the list
        override public Option? SelectOption()
        {
            // Read key
            ConsoleKeyInfo selectedOption = Console.ReadKey();

            // Check for up / down / enter / arrows keys
            switch (selectedOption.Key)
            {
                case ConsoleKey.UpArrow:
                case ConsoleKey.W:
                    if (this.selectedPosition > 0)
                        this.selectedPosition--;
                    else
                        this.selectedPosition = this.options.Count;
                    break;
                case ConsoleKey.DownArrow:
                case ConsoleKey.S:
                    if (this.selectedPosition < this.options.Count)
                        this.selectedPosition++;
                    else
                        this.selectedPosition = 0;
                    break;
                case ConsoleKey.RightArrow:
                case ConsoleKey.Enter:
                    if (this.selectedPosition == this.options.Count)
                    {
                        return Option.GetBackOption(this.previousScreen);
                    }
                    else
                        return this.options[this.selectedPosition];
                case ConsoleKey.LeftArrow:
                    return Option.GetBackOption(this.previousScreen);
                default:
                    break;
            }

            return null;
        }

}
}
