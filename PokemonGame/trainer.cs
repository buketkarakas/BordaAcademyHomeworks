using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGame
{
    public class Trainer
    {
        private static Trainer instance = null;
        public string Name { get; set; }
        public List<Pokemon> Pokemons { get; set; }
        public int Potions { get; set; }
        public int Pokeballs { get; set; }



        public Trainer()
        {
            Pokemons = new List<Pokemon>();
            Potions = Constants.starterPotionNumber;
            Pokeballs = Constants.starterPokeBallNumber;
        }

        public static Trainer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Trainer();
                }
                return instance;
            }
        }



        public void AddPokemon(Pokemon poke)
        {
            Pokemons.Add(poke);
        }

        public void AddPotion(int i = 1)
        {
            Potions += i;
        }


        public void DeletePotion(int i = 1)
        {
            Potions -= i;
        }

        public void AddPokeBall(int i = 1)
        {
            Pokeballs += i;
        }

        public void DeletePokeBall(int i = 1)
        {
            Pokeballs -= i;
        }

        public Pokemon ChoosePokemon()
        {
            Console.WriteLine(Constants.choosePokeMsg);
            int counter = 1;
            Console.WriteLine(Constants.decoration);
            foreach (Pokemon pokemon in Instance.Pokemons)
            {
                Console.WriteLine($"[{counter}] {pokemon.PokeName}");
                counter++;
            }
            Console.WriteLine(Constants.decoration);


            string choice = Console.ReadLine();
            int choice_int = Convert.ToInt32(choice);


            while (!(choice_int > 0 && choice_int <= Instance.Pokemons.Count))
            {
                Console.WriteLine(Constants.notOptionError+" "+Constants.waitForOption);
                choice = Console.ReadLine();
            }
            return Instance.Pokemons[choice_int - 1];
        }

        public bool HavePotion()
        {
            if (Potions > 0)
                return true;
            else
                return false;



        }
        public bool HavePokeBall()
        {
            if (Pokeballs > 0)
                return true;
            else
                return false;
        }

        public void DeletePokemon(Pokemon poke)
        {
            Pokemons.Remove(poke);
        }

        public void DisplayPokemonInfos()
        {
            int counter = 1;
            foreach (Pokemon item in Pokemons)
            {
                Console.WriteLine($"[{counter}]");
                item.ShowPokemonInfo();
                counter++;
            }
        }

        public bool IsThereAnyPokemon()
        {
            return Pokemons.Count != 0;
        }
    }
}
