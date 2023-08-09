using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Tamagotchi.Services.PokemonService;
using static Tamagotchi.Controller.TamagotchiController;
using System.Xml.Linq;
using Tamagotchi.Model;
using Tamagotchi.Services;
using Tamagotchi.Controller;
using Tamagotchi.Utils;

namespace Tamagotchi.View
{
    internal class TamagotchiView
    {
        public static void Welcome()
        {
            Divider();
            Console.WriteLine("Bem-vindo(a) ao -->TAMAGOTCHI<--");
            Divider();
            Console.WriteLine("Qual o seu nome?");
            PlayerName = Console.ReadLine();
        }

        public static void Menu()
        {
            AskPlayer();
            Console.WriteLine("----------------- MENU -----------------");
            Console.WriteLine("1 - Adotar um mascote virtual");
            Console.WriteLine("2 - Ver seus mascotes");
            Console.WriteLine("3 - Sair");

            int option = int.Parse(Console.ReadLine());

            switch (option)
            {
                case 1:
                    Console.WriteLine("Escolha um pokemon:");
                    Divider();
                    ListPokemons();
                    Divider();
                    var chosenPokemon = AskForAPokemon();
                    if (chosenPokemon != null)
                    {
                        PokemonMenu();
                    }
                    break;
                case 2:
                    Divider();
                    ListPlayerPokemons();
                    Divider();
                    Menu();
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Opção inválida");
                    Menu();
                    break;
            }
        }

        private static void AskPlayer()
        {
            Console.WriteLine($"{PlayerName}, o que você deseja:");
        }

        private static void HandlePokemonMenuOption(int option)
        {
            switch (option)
            {
                case 1:
                    Divider();
                    Console.WriteLine(SelectedPokemon.ToString());
                    PokemonMenu();
                    break;
                case 2:
                    Divider();
                    AdoptPokemon(SelectedPokemon);
                    Divider();
                    Menu();
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Opção inválida");
                    Menu();
                    break;
            }
        }

        static void Divider()
        {
            Console.WriteLine("----------------------------------------");
        }

        static void PokemonMenu()
        {
            Divider();
            Console.WriteLine($"{PlayerName}, você deseja:");
            Console.WriteLine($"1 - Saber mais sobre o {TamagotchiUtils.Capitalize(SelectedPokemon.Name)}");
            Console.WriteLine($"2 - Adotar {TamagotchiUtils.Capitalize(SelectedPokemon.Name)}");
            Console.WriteLine("3 - Voltar");

            int pokemonMenuOption = int.Parse(Console.ReadLine());
            HandlePokemonMenuOption(pokemonMenuOption);
        }
    }
}
