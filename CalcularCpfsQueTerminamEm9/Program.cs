using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace CalcularCpfsQueTerminamEm9
{
    class Program
    {
        static void Main()
        {
            var quantidadeCpfsQueTerminamEm9 = 0;
            Parallel.For(0, 999999999, cpfAtual =>
            {
                if(CpfInvalidoTrivial(cpfAtual))
                    return;

                var primeiroDigitoCpf = CalcularPrimeiroDigitoCpf(LongToIntArray(cpfAtual, 9));

                cpfAtual = cpfAtual * 10 + primeiroDigitoCpf;

                var segundoDigitoCpf = CalcularSegundoDigitoCpf(LongToIntArray(cpfAtual, 10));

                if(segundoDigitoCpf == 9)
                    Interlocked.Increment(ref quantidadeCpfsQueTerminamEm9);
            });

            

            Console.WriteLine(quantidadeCpfsQueTerminamEm9);
            Console.ReadKey();
        }

        static int CalcularPrimeiroDigitoCpf(int[] cpf)
        {
            var result = 0;
            for (int i = 0; i < 9; i++)
            {
                result += cpf[i] * (10 - i);
            }

            result = 11 - result % 11;

            return (result >= 10) ? 0 : result;
        }

        static int CalcularSegundoDigitoCpf(int[] cpf)
        {
            var result = 0;
            for (int i = 0; i < 10; i++)
            {
                result += cpf[i] * (11 - i);
            }

            result = 11 - result % 11;

            return (result >= 10) ? 0 : result;
        }

        static int[] LongToIntArray(long target, int size)
        {
            var stringInt = target.ToString().PadLeft(size, '0');
            return stringInt.ToCharArray().Select(charInt => charInt - '0').ToArray();
        }

        static bool CpfInvalidoTrivial(long cpf)
        {
            return (cpf is 111111111 or 222222222 or 333333333
                or 444444444 or 555555555 or 666666666 
                or 777777777 or 888888888 or 999999999);
        }
    }
}
