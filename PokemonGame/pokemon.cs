using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGame
{

    public class Pokemon 
    {
        public string PokeName { get; set; }
        public PokemonType Type { get; set; }
        public double Speed { get; set; }
        public double Defense { get; set; }
        public double Attack { get; set; }
        public double HitPoint { get; set; }
        public double ExperiencePoint { get; set; }

        public double RemainingHitPoint { get; set; }



        public Pokemon(string pokeName, double Attack, double Defense, double Speed, double HitPoint, PokemonType Type)
        {
            this.PokeName = pokeName;
            this.Attack = Attack;
            this.Defense = Defense;
            this.Speed = Speed;
            this.HitPoint = HitPoint;
            this.ExperiencePoint = 1;
            this.Type = Type;
            this.RemainingHitPoint = HitPoint;
        }

        public Pokemon() { }

        public void ShowPokemonInfo()
        {
            Console.WriteLine(Constants.decoration);
            Console.WriteLine($"PokeName: {this.PokeName}");
            Console.WriteLine($"Attack: {this.Attack}");
            Console.WriteLine($"Defense: {this.Defense}");
            Console.WriteLine($"Speed: {this.Speed}");
            Console.WriteLine($"Hit Point: {this.HitPoint}");
            Console.WriteLine($"Experience Point: {this.ExperiencePoint}");
            Console.WriteLine($"Type: {this.Type}");
            Console.WriteLine(Constants.decoration);
        }



        public void IncreaseHitPoint(int i)
        {
            this.RemainingHitPoint += i;
            if (RemainingHitPoint > HitPoint)
                RemainingHitPoint = HitPoint;
        }

        public void DecreaseHitPoint(double i)
        {
            RemainingHitPoint -= i;
        }

        public void IncreaseExperiencePoint()
        {
            this.ExperiencePoint++;
        }




    }
}
