using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGame
{
    interface IItem
    {
        public void Use(Pokemon tr,Pokemon wild);
    }
    public class Potion : IItem
    {
        public void Use(Pokemon p,Pokemon wild)
        {
            p.IncreaseHitPoint(Constants.potionHealthIncrease);
            Trainer.Instance.DeletePotion();
        }
    }

    public class PokeBall : IItem
    {
        public void Use(Pokemon p,Pokemon wild)
        {
            Trainer.Instance.AddPokemon(wild);
            Trainer.Instance.DeletePokeBall();
        }

        public bool IsCaptureSuccessfull(Pokemon p)
        {
             Random _random = new Random();
            double f = (p.HitPoint * Constants.captureUpperLimit * 4) / (p.RemainingHitPoint * Constants.captureNormalPokeFactor);
            int M = _random.Next(0, Constants.captureUpperLimit);
            if (f >= M)
            {
                return true;
            }

            return false;
        }
    }
}
