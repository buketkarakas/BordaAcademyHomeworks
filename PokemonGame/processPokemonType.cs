using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;

namespace PokemonGame
{
    public class ProcessPokemonType
    {
        private static ProcessPokemonType instance = null;

        private readonly string filePath = @"C:\Users\Buket\source\repos\PokemonGame\PokemonGame\PokemonTypes.txt";
        private readonly Dictionary<string, int> matrixIndexes = new Dictionary<string, int>();
        private readonly double[,] typeMatrix = new double[9, 9];
        public static ProcessPokemonType Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProcessPokemonType();
                }
                return instance;
            }
        }





        public ProcessPokemonType()
        {
            InitializeMatrixIndex();
            InitializeMatrix();
            GetData();
        }

        private void InitializeMatrixIndex()
        {
            matrixIndexes.Add("Grass", 0);
            matrixIndexes.Add("Fire", 1);
            matrixIndexes.Add("Water", 2);
            matrixIndexes.Add("Bug", 3);
            matrixIndexes.Add("Flying", 4);
            matrixIndexes.Add("Normal", 5);
            matrixIndexes.Add("Poison", 6);
            matrixIndexes.Add("Electric", 7);
            matrixIndexes.Add("Ground", 8);
        }

        private void InitializeMatrix()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    typeMatrix[i, j] = 1;
                }
            }
        }

        public void PrintMatrix()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write(typeMatrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        private void InsertData(string line, double val, int index)
        {
            string[] words = line.Split(" ");
            foreach (string word in words)
            {
                if (word != "")
                {
                    int index2 = matrixIndexes[word];
                    typeMatrix[index, index2] = val;
                }
            }
        }
        private void GetData()
        {
            string[] lines = File.ReadAllLines(filePath);
            int count = 1;
            int currIndex = -1;
            foreach (string line in lines)
            {
                if (count % 3 == 1)
                {
                    currIndex = matrixIndexes[line];
                    count++;
                }
                else if (count % 3 == 2)
                {
                    InsertData(line, 2, currIndex);
                    count++;
                }
                else
                {
                    InsertData(line, 0.5, currIndex);
                    currIndex = -1;
                    count = 1;
                }
            }

        }

        public double FindEffectiveness(string attackType,string defendType)
        {
            return typeMatrix[matrixIndexes[attackType], matrixIndexes[defendType]];
        }

    }
}
