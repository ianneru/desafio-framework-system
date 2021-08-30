using DesafioFrameworkSystem.Application;
using System;
using System.Text;

namespace DesafioFrameworkSystem.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Digite um número natural : ");

            var numeroEntrada = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("-----------------");

            Console.WriteLine("Número de Entrada: {0}", numeroEntrada);

            var mathCalc = new MathCalcApplication(numeroEntrada);

            Console.WriteLine("Números divisores: {0}", new StringBuilder()
                                                            .AppendJoin(" ", mathCalc.NumeroDivisores).ToString());
            Console.WriteLine("Divisores Primos: {0}", new StringBuilder()
                                                            .AppendJoin(" ", mathCalc.DivisoresPrimos).ToString());

            Console.WriteLine("-----------------");

            Console.ReadKey();
        }

        
    }
}
