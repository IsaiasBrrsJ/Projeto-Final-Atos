using System.Text;
using System.Transactions;

namespace GeradorDeCpf
{
    internal class Program
    {
        static void Main(string[] args)
        {
          var CPFlist =   VerificaSeValido(gerarCPFs());

            if (CPFlist.Count > 0)
            {   
                Console.ForegroundColor= ConsoleColor.Green;    
                Console.WriteLine("Possiveis CPF's válidos");
                Console.ResetColor();
                foreach (var CPFs in CPFlist)
                {
                    Console.WriteLine(CPFs);
                }
            }

            Console.ReadKey();
        }
        static List<string> gerarCPFs()
        {
            List<string> list = new List<string>();
            StringBuilder builder = new StringBuilder();
            builder.Append("6279");
            int cont = 0;
            int onzeDigitos = 4;
            while (list.Count < 50)
            {

                string numGerado = new Random().Next(0, 9).ToString();
                builder.Append(numGerado);

                onzeDigitos++;
                
                if (onzeDigitos == 11)
                {

                    list.Add(builder.ToString());
                    builder.Clear();
                    onzeDigitos = 4;
                }

                
            }


            Console.ReadKey();

            list.RemoveAt(0);

            return list;
        }
        static List<string> VerificaSeValido(List<String> CPFs)
        {
            List<string> cpfsValidos = new List<string>();
            int count = 0;

            var cpfSplit = new string[11];
            var somaPrimeiroDigito = 0;
            bool somouPrimeiroDigito = false;

            var somaSegundoDigito = 0;
            int restoPrimeiroDigito = 0;
            int restoSegundoDigito = 0;

            while (count < CPFs.Count)
            {
                for (int i = 0; i < CPFs.Count; i++)
                {
                    string cpf = CPFs[i];
                    for (int j = 0; j < 11; j++)
                    {
                        cpfSplit[j] = cpf[j].ToString();
                    }


                    for (int k = 0; k < 10; k++)
                    {
                        if (k < 9 && somouPrimeiroDigito == false)
                        {

                            somaPrimeiroDigito += (Convert.ToInt32(cpfSplit[k]) * (k + 1));

                            if (k == 8)
                            {
                                somouPrimeiroDigito = true;
                                k = 0;
                            }
                        }

                        if (somouPrimeiroDigito == true)
                        {

                            somaSegundoDigito += (Convert.ToInt32(cpfSplit[k]) * k);

                        }
                    }


                    restoPrimeiroDigito = somaPrimeiroDigito % 11;
                    restoSegundoDigito = somaSegundoDigito % 11;

                    if (restoPrimeiroDigito >= 10)
                        restoPrimeiroDigito = 0;

                    if (restoSegundoDigito >= 10)
                        restoSegundoDigito = 0;


                    if (cpfSplit[9] == restoPrimeiroDigito.ToString() && cpfSplit[10] == restoSegundoDigito.ToString())
                    {
                        cpfsValidos.Add(cpf);
                    }
                }

                count++;
            }

            return cpfsValidos; 
        }
    }
}