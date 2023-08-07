using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Program;
using static Tamagotchi.Services.PokemonService;

namespace Tamagotchi.Model
{
    internal class Pokemon
    {
        public string Name { get; set; }
        public int Height { get; set; }
        public int Weigth { get; set; }
        public List<string> Abilities { get; set; }

        public Pokemon(string name, int height, int weight)
        {
            Name = name;
            Height = height;
            Weigth = weight;
        }

        public Pokemon(string name, int height, int weight, List<string> abilities) 
        {
            Name = name;
            Height = height;
            Weigth = weight;
            Abilities = abilities;
        }

        public override string ToString()
        {

            return @$"
                Nome Pokemon: {Capitalize(Name)}
                Altura: {Height}
                Peso: {Weigth}
                Habilidades: {String.Join(",", Abilities)}
            ";
        }
    }
}
