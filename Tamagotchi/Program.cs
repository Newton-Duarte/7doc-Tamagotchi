using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

class Program
{
    public static string name = "";
    public static List<Pokemon> adoptedPokemons = new List<Pokemon>();

    static void Main(string[] args)
    {
        Console.WriteLine("Qual o seu nome?");
        name = Console.ReadLine();

        Menu.Open();
    }

    public static void GetPokemons()
    {
        var client = new RestClient($"https://pokeapi.co/api/v2/pokemon");
        var request = new RestRequest("", Method.Get);
        var response = client.Execute(request);

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            var json = JsonSerializer.Deserialize<PokeListAPIResponse>(response.Content);
            foreach (var pokemon in json.results)
            {
                Console.WriteLine($"{Capitalize(pokemon.name)}");
            }
        }
        else
        {
            Console.WriteLine(response.ErrorMessage);
        }
    }

    private static void GetPokemon(string term)
    {
        var client = new RestClient($"https://pokeapi.co/api/v2/pokemon/{term}");
        var request = new RestRequest("", Method.Get);
        var response = client.Execute(request);

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            var json = JsonSerializer.Deserialize<Pokemon>(response.Content);

            Console.WriteLine($"Nome Pokemon: {Capitalize(json.name)}");
            Console.WriteLine($"Altura: {json.height}");
            Console.WriteLine($"Peso: {json.weight}");
            Console.WriteLine("Habilidades:");
            foreach (var ability in json.abilities)
            {
                Console.WriteLine(Capitalize(ability.ability.name));
            }
        }
        else
        {
            Console.WriteLine(response.ErrorMessage);
        }
    }

    private static string Capitalize(string str)
    {
        if (str == null) return "-";
        return $"{str[0].ToString().ToUpper()}{str.Substring(1)}";
    }

    public record struct PokeListAPIResponse(
        int count,
        Pokemon[] results
    );

    public record struct Pokemon (
        string name,
        int height,
        int weight,
        PokemonAbilities[] abilities
    );

    public record struct PokemonAbility (
        string name
    );

    public record struct PokemonAbilities(
        PokemonAbility ability
    );

    public record struct PokemonType (
        string name    
    );

    public record struct PokemonTypes(
        int slot,
        PokemonType[] types
    );
}