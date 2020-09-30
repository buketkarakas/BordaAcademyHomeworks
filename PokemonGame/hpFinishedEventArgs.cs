using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGame
{
    public enum FinishType { normal, pokeBall };

    public class HpFinishedEventArgs: EventArgs
    {
        public double RemainingHp { get; set; }

        public Pokemon Winner { get; set; }

        public Pokemon Loser { get; set; }

        public FinishType Type { get; set; }

    }
}
