using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGame
{
    public static class Constants
    {
        public const string decoration = "+------------------------+";
        public const string welcomeMenu = "Welcome to the Poke Game!\n" + decoration + "\n[1] Continue\n[2] New Game\n" + decoration;
        public const string notImplementedError = "Not implemented yet! sory:(";
        public const string notOptionError = "There is no option like this!";
        public const string waitForOption = "Please Type Your Choice:";
        public const string starterPokemonMenu = "Welcome to the game\nHere are your starter poke options:\n" + decoration + "\n[1] Squirtle\n[2] Bulbasaur\n[3] Charmander\n" + decoration;
        public const string menuChoices1= "1";
        public const string menuChoices2 = "2";
        public const string menuChoices3 = "3";
        public const string menuChoices4 = "4";
        public const string newPokeMsg = "Congratulations! You have a new pokemon now! Here it is:";
        public const string exitMsg = "Goodbye! ";
        public const string generalMenu = "What do you want to do next?\n[1] New Battle!\n[2] Show my pokemons\n[3] Save and Exit!\n[4] Exit";
        public const string noPokemonLeftError = "You have no pokemon left! Game Over!";

        public const int captureUpperLimit = 255;
        public const int captureNormalPokeFactor = 12;

        public const int potionHealthIncrease = 10;

        public const int starterPotionNumber = 5;
        public const int starterPokeBallNumber = 5;

        public const string choosePokeMsg = "Please choose your pokemon:";

        public const string newOpponentMsg = "You have a new opponent";
        public const string actionMenu = "Action Menu:\n[1] Attack!\n[2] Use Potion!\n[3] Change Pokemon!";
        public const string changePokemonMsg = "You have successfully changes your pokemon!";
        public const string useItemMenu = "Item Menu:\n" + decoration + "\n[1] Potion\n[2] PokeBall";
        public const string noPotionError = "You have no potion left!";
        public const string captureError = "Sorry, you couldn't capture the pokemon :(";
        public const string noPokeballError = "You have no pokeball left!";
        public const string battleOverMsg = "The battle is over!";

        public const string doubleEffectiveMsg = "The damage is doubled!";
        public const string halfEffectiveMsg = "The damage is halved!";
    }
}
