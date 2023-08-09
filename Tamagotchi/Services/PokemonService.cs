using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tamagotchi.Model;
using Tamagotchi.Utils;
using static Program;

namespace Tamagotchi.Services
{
    internal class PokemonService
    {
        private const string POKE_API_URL = "https://pokeapi.co/api/v2/pokemon";

        public static void GetPokemons()
        {
            var client = new RestClient(POKE_API_URL);
            var request = new RestRequest("", Method.Get);
            var response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var json = JsonSerializer.Deserialize<PokeListAPIResponse>(response.Content);
                foreach (var pokemon in json.results)
                {
                    var newPokemon = new Pokemon(pokemon.name, pokemon.height, pokemon.weight);

                    Console.WriteLine($"{TamagotchiUtils.Capitalize(newPokemon.Name)}");
                }
            }
            else
            {
                Console.WriteLine(response.ErrorMessage);
            }
        }

        public static RestResponse GetPokemon(string term)
        {
            var client = new RestClient($"{POKE_API_URL}/{term.ToLower()}");
            var request = new RestRequest("", Method.Get);
            var response = client.Execute(request);

            return response;
        }

        public record struct PokeListAPIResponse(
            int count,
            PokemonRecord[] results
        );

        public record struct PokemonRecord(
            string name,
            int height,
            int weight,
            PokemonAbilitiesRecord[] abilities
        );

        public record struct PokemonAbilitiesRecord(
            PokemonAbilityRecord ability
        );

        public record struct PokemonAbilityRecord(
            string name
        );
    }
}
