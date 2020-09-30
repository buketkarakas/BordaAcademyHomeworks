using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;

namespace PokemonGame
{
    public class Program
    {
        static IGame game;
        static public void WelcomePage()
        {
            Console.WriteLine(Constants.welcomeMenu);
            bool valid;
            do
            {
                Console.WriteLine(Constants.waitForOption);
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    valid = true;
                    game = new SavedGame();
                }
                else if (choice == "2")
                {
                    valid = true;
                    game = new NewGame();
                }
                else
                {
                    valid = false;
                    Console.WriteLine(Constants.notOptionError);
                }
            } while (!valid);
        }
        static void Main(string[] args)
        {
            WelcomePage();
            game.StartGame();

        }
    }
}
