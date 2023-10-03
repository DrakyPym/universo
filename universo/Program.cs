using System;

namespace universoAlfabeto
{
    class Program
    {
        static void Main(string[] args)
        {
            string respuesta = "";
           
            while (respuesta != "1")
            {
                Console.WriteLine("Ingrese 1 para calcular de nuevo o 2 para salir");
                respuesta = Console.ReadLine();
                if (respuesta == "1") CalcularUniverso();
                else if (respuesta != "2") Console.WriteLine("No introdujo un número valido, vuelva a intentarlo");
                else break; //la respuesta es 2
                
                respuesta = "";
            }

            Console.WriteLine("Presiona una tecla para continuar...");
            Console.ReadKey();
        }

        static void CalcularUniverso()
        {
            List<string> alfabeto = new() { "0", "1" };

            Console.WriteLine("Ingrese 1 para introducir un número o 2 para generar uno aleatoriamente");

            bool entradaValida = false;
            string respuesta = "";

            while (!entradaValida)
            {
#pragma warning disable CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
                respuesta = Console.ReadLine();
#pragma warning restore CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL

                if (respuesta == "1" || respuesta == "2") entradaValida = true;
                else Console.WriteLine("No introdujo un número valido, vuelva a intentarlo");
            }

            int n = 0; // Numero hasta donde se calculara el universo

            switch (respuesta)
            {
                case "1":  // Case de seleccionar un número
                    Console.WriteLine("Ingrese el número hasta donde desee calcular el universo del alfabeto que contine '0' y '1'");
                    entradaValida = false;

                    while (!entradaValida)
                    {
                        try
                        {
#pragma warning disable CS8604 // Posible argumento de referencia nulo
                            n = int.Parse(Console.ReadLine());
#pragma warning restore CS8604 // Posible argumento de referencia nulo
                            if (n > 1000)
                            {
                                Console.WriteLine("El número no puede ser mayor a 1000");
                                Console.WriteLine("Vuelva a ingresar un número para calcular el universo");
                                entradaValida = false;
                            }
                            else entradaValida = true;
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("No ha introducido un número valido, vuelva a intentarlo");
                        }
                    }
                    break;

                case "2": // Caso de un número aleatorio 
                    Random random = new Random();
                    n = random.Next(0, 1001); // Genera un número entre 0 y 1000 (incluyendo 0 pero excluyendo 1001)
                    Console.WriteLine("Se calculará hasta la potencia " + n);
                    Console.WriteLine("Presiona una tecla para continuar...");
                    Console.ReadKey();
                    break;
                default:
                    Console.WriteLine("Error");
                    break;
            }

            List<string> aux1 = new();
            List<string> aux2 = new();

            // Hago la copia del alfabeto en cada lista
            aux1.AddRange(alfabeto);
            aux2.AddRange(alfabeto);


            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < aux1.Count; j++)
                {
                    aux1[j] = "0" + aux1[j];
                    aux2[j] = "1" + aux2[j];
                }
                aux1.AddRange(aux2);
                alfabeto.AddRange(aux1);
                aux2 = new(aux1);
            }

            // Ruta donde se guardara el universo
            string rutaArchivo = "universoDatos.txt";

            Console.WriteLine("\nDatos:\n");

            try
            {
                using StreamWriter writer = new StreamWriter(rutaArchivo);
                if (n == 0)
                {
                    Console.WriteLine("Σ = {E}");
                    writer.Write("Σ = {E}");
                }
                else
                {
                    Console.Write("Σ = {E");
                    writer.Write("Σ = {E");

                    foreach (var item in alfabeto)
                    {
                        Console.Write(", " + item);
                        writer.Write(", " + item);
                    }

                    Console.Write("}");
                    writer.Write("}");

                    Console.WriteLine("\n\nLos datos se han guardado en el archivo.\n");
                }

            }
            catch (IOException e)
            {
                Console.WriteLine("Ocurrió un error al guardar los datos: " + e.Message);
            }
        }
    }
}