using RestSharp;
using System;
using System.Collections.Generic;
using System.Text.Json;
using Tamagotchi;

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

    public static RestResponse GetPokemon(string term)
    {
        var client = new RestClient($"https://pokeapi.co/api/v2/pokemon/{term.ToLower()}");
        var request = new RestRequest("", Method.Get);
        var response = client.Execute(request);

        return response;
    }

    public static void ShowAdoptedPokemons()
    {
        if (adoptedPokemons.Count == 0)
        {
            Console.WriteLine("Lista vazia");
            return;
        }

        Console.WriteLine("----------- Pokemons Adotados ----------");

        foreach (var pokemon in adoptedPokemons)
        {
            Console.WriteLine($"{Capitalize(pokemon.name)}");
        }
    }

    public static void PokemonInfo(Pokemon pokemon)
    {
        Console.WriteLine($"Nome Pokemon: {Capitalize(pokemon.name)}");
        Console.WriteLine($"Altura: {pokemon.height}");
        Console.WriteLine($"Peso: {pokemon.weight}");
        Console.WriteLine("Habilidades:");
        foreach (var ability in pokemon.abilities)
        {
            Console.WriteLine(Capitalize(ability.ability.name));
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