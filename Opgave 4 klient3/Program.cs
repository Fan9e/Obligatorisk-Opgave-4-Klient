
using System;
using System.IO;
using System.Net.Sockets;

class TcpClientApp
{
	static void Main(string[] args)
	{
		try
		{
			TcpClient client = new TcpClient("127.0.0.1", 8888); // Opretter  en TCP forbindelse til sever, IP-adresse og port
			NetworkStream ns = client.GetStream(); // Opret en netværkstream der kan sende og modtage data via netværket fra TCP-forbindelsen.
            StreamReader reader = new StreamReader(ns);// Opret en veriable=reader til at modtage tekstdata fra netværksstrømmen.
			StreamWriter writer = new StreamWriter(ns) { AutoFlush = true }; //opretter enveriable=writer til at sende tekstdata oer nætværkstream
			//AutoFlush =True (gør at data sendes med det samme)

			string command = "";// Initialiserer en tom streng variabel til at gemme brugerens kommandoer.


            Console.WriteLine("Connected to server. Type 'stop' to disconnect.");

			// While (man ikke skriver stop)"
			while (command.ToLower() != "stop")
			{
				// Spørg brugeren om kommandoen
				Console.Write("Enter command (Random, Add, Subtract, Stop): ");//brugeren skal intaste en komando
				command = Console.ReadLine();//Readine læser command fra Consolen og gemer det i veriablen command
				writer.WriteLine(command); // Send kommando til serveren via nætverksstreamen (writer)

				if (command.ToLower() == "stop")
					break; // Hvis brugeren skriver "stop", afslut forbindelsen

				// Modtag besked fra serveren: "Input numbers" og læser det
				Console.WriteLine(reader.ReadLine());

				// Spørg brugeren om to tal
				Console.Write("Enter first number: ");
				string num1 = Console.ReadLine(); //læs det første tak
				Console.Write("Enter second number: ");
				string num2 = Console.ReadLine();//læs andet tal

				// Send tallene til serveren
				writer.WriteLine($"{num1} {num2}");

				// Modtag og udskriv resultatet fra serveren
				string result = reader.ReadLine();//de tal som variablen reader har fundet frem til bbliver ´gemt i result
				Console.WriteLine($"Result from server: {result}");//result bliver udskrevet
			}

			client.Close(); // Luk forbindelsen, når "stop" er indtastet
			Console.WriteLine("Disconnected from server.");
		}
		catch (Exception e)//hvis der sker en exception bliver den fanget
		{
			Console.WriteLine($"Error: {e.Message}");//en error besked bliver givet hvis der sker en fejl

        }
    }
}
