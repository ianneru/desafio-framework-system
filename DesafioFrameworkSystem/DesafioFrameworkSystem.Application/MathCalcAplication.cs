using System;
using System.Collections.Generic;

namespace DesafioFrameworkSystem.Application
{
    public class MathCalcApplication
    {
        private readonly int _numeroEntrada;
        public int[] NumeroDivisores {  get; set; }
        public int[] DivisoresPrimos { get; set; }

        public MathCalcApplication(int numeroEntrada)
        {
            if (numeroEntrada > 0)
                _numeroEntrada = numeroEntrada;
            else
                throw new Exception("Não é um número natural válido");

            NumerosDivisores();

            NumerosDivisoresPrimos();
        }

        private void NumerosDivisores()
        {
            var listaDivisores = new List<int>();

            for (var divisor = 1; divisor <= _numeroEntrada; divisor++)
            {
                var divisaoResto = _numeroEntrada % divisor;

                if (divisaoResto == 0)
                    listaDivisores.Add(divisor);
            }

            NumeroDivisores = listaDivisores.ToArray();
        }

        private void NumerosDivisoresPrimos()
        {
            var divisor = 0;
            var listaPrimos = new List<int>();

            for (var contador = 1; contador <= _numeroEntrada; contador++)
            {
                if ((_numeroEntrada % contador) == 0)
                {
                    divisor++;

                    listaPrimos.Add(contador);
                }

                if (divisor > 2)
                    break;
            }

            DivisoresPrimos = listaPrimos.ToArray();
        }
    }
}
