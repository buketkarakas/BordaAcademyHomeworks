using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PokemonGame
{
    public interface IGame
    {
        void StartGame();
        void MainMenu();

        void SaveGame();

    }
    public class NewGame : IGame
    {
        Battle battle;
        public NewGame() { }

        public void StartGame()
        {
            ChooseStarterPokemon();
            MainMenu();
        }
        public void ChooseStarterPokemon()
        {
            Console.Clear();
            Console.WriteLine(Constants.starterPokemonMenu);
            string choice;
            bool valid = false;
            Pokemon pokemon = new Pokemon();

            do
            {
                Console.WriteLine(Constants.waitForOption);

                choice = Console.ReadLine();

                if (choice == Constants.menuChoices1)
                {

                    pokemon = new Pokemon("Squirtle", 48, 65, 43, 44, PokemonType.Water);
                    valid = true;
                }
                else if (choice == Constants.menuChoices2)
                {
                    pokemon = new Pokemon("Bulbasaur", 49, 49, 45, 45, PokemonType.Grass);
                    valid = true;
                }
                else if (choice == Constants.menuChoices3)
                {
                    pokemon = new Pokemon("Charmander", 52, 43, 65, 39, PokemonType.Fire);
                    valid = true;
                }
                else
                {
                    Console.WriteLine(Constants.notOptionError);
                }

            } while (!valid);
            Console.Clear();
            Trainer.Instance.AddPokemon(pokemon);
            Console.WriteLine(Constants.newPokeMsg);
            pokemon.ShowPokemonInfo();

        }


        public void MainMenu()
        {
            string choice;
            do
            {
                Console.WriteLine(Constants.generalMenu);
                choice = Console.ReadLine();
                if (choice == Constants.menuChoices1)
                {
                    Pokemon opp = WildPokemonFactory.Create();
                    Pokemon trainerPoke = Trainer.Instance.ChoosePokemon();

                    battle = new Battle(trainerPoke, opp);

                    battle.StartBattle();
                }
                else if (choice == Constants.menuChoices2)
                {
                    Console.Clear();
                    Trainer.Instance.DisplayPokemonInfos();
                }
                else if( choice == Constants.menuChoices3)
                {
                    SaveGame();
                }

                if (!Trainer.Instance.IsThereAnyPokemon())
                {
                    Console.WriteLine(Constants.noPokemonLeftError);
                    choice = "4";
                }

            } while (choice != Constants.menuChoices4 && choice != Constants.menuChoices3);

            Console.WriteLine(Constants.exitMsg);
        }

        public void SaveGame()
        {
            string JsonTrainer = JsonConvert.SerializeObject(Trainer.Instance);
            string filePath = @"C:\Users\Buket\source\repos\PokemonGame\PokemonGame\SavedTrainerInfo.txt";

            using (var tw = new StreamWriter(filePath, false))
            {
                tw.WriteLine(JsonTrainer.ToString());
                tw.Close();
            }
        }

    }

    public class SavedGame : NewGame, IGame
    {
       
        public new void StartGame()
        {
            string filePath = @"C:\Users\Buket\source\repos\PokemonGame\PokemonGame\SavedTrainerInfo.txt";
            string JsonString = File.ReadAllText(filePath);
            JsonSerializer serializer = new JsonSerializer();
            var obj= JsonConvert.DeserializeObject<Trainer>(JsonString);
            Trainer.Instance.Pokeballs = obj.Pokeballs;
            Trainer.Instance.Potions = obj.Potions;
            Trainer.Instance.Pokemons = obj.Pokemons;
           
            MainMenu();
            
        }
    }
}
