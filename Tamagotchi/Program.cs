using RestSharp;
using System;
using System.Collections.Generic;
using System.Text.Json;
using Tamagotchi;
using Tamagotchi.Model;

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
            Console.WriteLine($"{Capitalize(pokemon.Name)}");
        }
    }

    public static string Capitalize(string str)
    {
        if (str == null) return "-";
        return $"{str[0].ToString().ToUpper()}{str.Substring(1)}";
    }
}