using System;
using System.Collections.Generic;

namespace PokemonGame
{
    public class Battle
    {
        public Pokemon TrainersPoke { get; set; }
        public Pokemon WildPoke { get; set; }

        static private readonly Random _random = new Random();

        public double trainerEfectiveness;

        public double wildEfectiveness;

        private bool isBattleOver = false;

        public Battle(Pokemon TrainersPoke, Pokemon WildPoke)
        {
            this.TrainersPoke = TrainersPoke;
            this.WildPoke = WildPoke;
            trainerEfectiveness = ProcessPokemonType.Instance.FindEffectiveness(TrainersPoke.Type.ToString(),WildPoke.Type.ToString());
            wildEfectiveness = ProcessPokemonType.Instance.FindEffectiveness(WildPoke.Type.ToString(), TrainersPoke.Type.ToString());
        }

     
        public void DisplayBattleInfo()
        {
            Console.WriteLine(Constants.newOpponentMsg);
            Console.WriteLine($"Your opponent is {WildPoke.PokeName}");

        }

        public void DisplayPokeInfos()
        {
            Console.WriteLine(Constants.decoration);
            Console.WriteLine($"{TrainersPoke.PokeName}: {TrainersPoke.RemainingHitPoint}/{TrainersPoke.HitPoint}");
            Console.WriteLine($"{WildPoke.PokeName}: {WildPoke.RemainingHitPoint}/{WildPoke.HitPoint}");
            Console.WriteLine(Constants.decoration);
            
        }
        public void StartBattle()
        {

            DisplayBattleInfo();
            bool first = true;
            bool trainersTurn = false;
            this.HpFinishedEventHandlerInstance += Battle_hpFinishedEventHandler; ;

            do
            {
                Console.WriteLine();
                Console.WriteLine();
                DisplayPokeInfos();

                if (first)
                {
                    if (TrainersPoke.Speed >= WildPoke.Speed)
                    {
                        ChooseActionType(TrainersPoke, WildPoke);
                    }
                    else
                    {
                        trainersTurn = true;
                        Attack(WildPoke, TrainersPoke, wildEfectiveness);
                    }
                    first = false;
                }
                else
                {
                    if (trainersTurn)
                    {
                        trainersTurn = false;
                        ChooseActionType(TrainersPoke, WildPoke);


                    }
                    else
                    {
                        trainersTurn = true;
                        Attack(WildPoke, TrainersPoke, wildEfectiveness);
                    }
                }

            } while (!isBattleOver);

        }
        public void ChooseActionType(Pokemon attacker, Pokemon defender)
        {
            Console.WriteLine(Constants.actionMenu);


            string choice;
            bool valid = false;
            do
            {
                Console.WriteLine(Constants.waitForOption);
                choice = Console.ReadLine();
                if (choice == Constants.menuChoices1)
                {
                    valid = true;
                    Attack(attacker, defender, trainerEfectiveness);
                }
                else if (choice == Constants.menuChoices2)
                {
                    valid = true;
                    UseItem();
                }
                else if (choice == Constants.menuChoices3)
                {
                    valid = true;
                    ChangePokemon();
                }
                else
                {
                    Console.WriteLine(Constants.notOptionError);

                }
            } while (!valid);



        }

        public void UsePotion()
        {
            Potion potion = new Potion();
            potion.Use(TrainersPoke, WildPoke);
            Console.WriteLine($"Now you have {TrainersPoke.RemainingHitPoint} HitPoint!");

        }

        public void UsePokeBall(PokeBall pokeBall)
        {
            pokeBall.Use(TrainersPoke, WildPoke);

            HpFinishedEventArgs hpFinishedEventArgs = new HpFinishedEventArgs()
            {
                RemainingHp = WildPoke.RemainingHitPoint,
                Loser = WildPoke,
                Type = FinishType.pokeBall,
                Winner = TrainersPoke



            };
            OnHpFinished(hpFinishedEventArgs);
        }

        public void UseItem()
        {
            Console.WriteLine(Constants.useItemMenu);
            string choice;
            bool valid = false;
            do
            {
                choice = Console.ReadLine();
                if (choice == Constants.menuChoices1)
                {
                    if (Trainer.Instance.HavePotion())
                    {
                        UsePotion();
                        valid = true;
                    }
                    else
                    {
                        Console.WriteLine(Constants.noPotionError);
                    }
                }
                else if (choice == Constants.menuChoices2)
                {
                    PokeBall pokeBall = new PokeBall();
                    valid = true;

                    if (Trainer.Instance.HavePokeBall())
                    {
                        if (pokeBall.IsCaptureSuccessfull(TrainersPoke))
                        {
                            UsePokeBall(pokeBall);
                        }
                        else
                        {
                            Console.WriteLine(Constants.captureError);
                        }

                    }
                    else
                    {
                        Console.WriteLine(Constants.noPokeballError);
                    }
                }

            } while (!valid);

        }

        public void DisplayAttackMessage(double effectiveness)
        {
            switch (effectiveness)
            {
                case 2:
                    Console.WriteLine(Constants.doubleEffectiveMsg);
                    break;
                case (1 / 2):
                    Console.WriteLine(Constants.halfEffectiveMsg);
                    break;
                default:
                    break;
            }
        }
        public void Attack(Pokemon attacker, Pokemon defender, double effectiveness)
        {
            double mult1 = (attacker.ExperiencePoint * 2) / 5;
            double mult2 = attacker.Attack / attacker.Defense;
            double modifier = effectiveness * _random.NextDouble() * (4.0 - 0.85) + 0.85;
            double damage = (mult1 * mult2 + 2) * modifier;
            damage = Math.Round(damage);

            defender.DecreaseHitPoint(damage);
            defender.RemainingHitPoint = Math.Round(defender.RemainingHitPoint, 2);
            DisplayAttackMessage(effectiveness);
            Console.WriteLine($"{attacker.PokeName} gave {damage} damage!");

            if (defender.RemainingHitPoint <= 0)
            {
                HpFinishedEventArgs hpFinishedEventArgs = new HpFinishedEventArgs()
                {
                    RemainingHp = defender.RemainingHitPoint,
                    Loser = defender,
                    Type = FinishType.normal,
                    Winner = attacker



                };
                OnHpFinished(hpFinishedEventArgs);

            }

        }

        public void ChangePokemon()
        {
            TrainersPoke = Trainer.Instance.ChoosePokemon();
            Console.WriteLine(Constants.changePokemonMsg);
        }

        

        private void Battle_hpFinishedEventHandler(object sender, HpFinishedEventArgs e)
        {

            Console.WriteLine(Constants.battleOverMsg);
            Console.WriteLine($"{e.Winner.PokeName} won the battle!");
            isBattleOver = true;
            e.Winner.IncreaseExperiencePoint();

            if (e.Loser == TrainersPoke)
                Trainer.Instance.DeletePokemon(e.Loser);
            



            if (e.Type == FinishType.pokeBall)
            {
                Console.WriteLine($"You have now captured {e.Loser.PokeName}");
            }


        }


        private void OnHpFinished(HpFinishedEventArgs hpFinishedEventArgs)
        {
            HpFinishedEventHandlerInstance?.Invoke(this, hpFinishedEventArgs);
        }

        public event HpFinishedEventHandler HpFinishedEventHandlerInstance;






    }
}
