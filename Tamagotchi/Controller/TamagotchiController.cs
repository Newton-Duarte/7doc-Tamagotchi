using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tamagotchi.Model;
using Tamagotchi.Utils;
using static Tamagotchi.Services.PokemonService;

namespace Tamagotchi.Controller
{
    internal class TamagotchiController
    {
        public static string PlayerName { get; set; }
        public static List<Pokemon> PlayerPokemons = new List<Pokemon>();
        public static Pokemon SelectedPokemon { get; set; }

        public static void AdoptPokemon(Pokemon pokemon)
        {
            PlayerPokemons.Add(pokemon);
            Console.WriteLine("Pokemon adotado com sucesso!");
        }

        public static void ListPokemons()
        {
            GetPokemons();
        }

        public static Pokemon? AskForAPokemon()
        {
            string answer = Console.ReadLine();
            var response = GetPokemon(answer);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var pokemonRecord = JsonSerializer.Deserialize<PokemonRecord>(response.Content);
                var abilities = new List<string>();
                foreach (var ability in pokemonRecord.abilities)
                {
                    abilities.Add(ability.ability.name);
                }
                SelectedPokemon = new Pokemon(pokemonRecord.name, pokemonRecord.height, pokemonRecord.weight, abilities);

                return SelectedPokemon;
            }
            else
            {
                var errorMessage = response.ErrorMessage;
                Console.WriteLine(errorMessage != null ? errorMessage : "Pokemon não encontrado");
                return null;
            }
        }

        public static void ListPlayerPokemons()
        {
            if (PlayerPokemons.Count == 0)
            {
                Console.WriteLine("Você não possui nenhum pokemon!");
                return;
            }

            Console.WriteLine("----------- Pokemons Adotados ----------");

            foreach (var pokemon in PlayerPokemons)
            {
                Console.WriteLine($"{TamagotchiUtils.Capitalize(pokemon.Name)}");
            }
        }
    }
}
