using RestSharp;
using System;
using System.Collections.Generic;
using System.Text.Json;
using Tamagotchi;
using Tamagotchi.Model;
using Tamagotchi.View;

class Program
{
    static void Main(string[] args)
    {
        TamagotchiView.Welcome();
        TamagotchiView.Menu();
    }
}