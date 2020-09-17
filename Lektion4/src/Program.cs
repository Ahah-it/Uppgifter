using System;

namespace Lektion4
{
    class Program
    {
        static void Main(string[] args)
        {
            //    Uppgift1();
            Uppgift2();
        }

        private static void Uppgift2()
        {
            int val = -1;
            int tal1, tal2;
            bool succ = false;

            while (val != 0)
            {
                PrintMeny2();
                Int32.TryParse(Console.ReadLine(), out val);

                Console.Clear();

                switch (val)
                {
                    case 1:

                        Console.Write("Mata in första talet: ");
                        succ = Int32.TryParse(Console.ReadLine(), out tal1);
                        Console.Write("Mata in andra talet: ");
                        succ = Int32.TryParse(Console.ReadLine(), out tal2);

                        if (!succ)
                        {
                            Console.WriteLine("Endast heltal får anges.");
                        }
                        else
                        {
                            Console.WriteLine($"{Addera(tal1, tal2)}");
                        }

                        break;
                    case 2:
                        Console.Write("Ange pris: ");

                        succ = Int32.TryParse(Console.ReadLine(), out tal1);

                        if (!succ)
                        {
                            Console.WriteLine("Endast heltal får anges.");
                        }
                        else
                        {
                            Console.WriteLine($"Pris: {tal1} Moms: {Moms(tal1)}");
                        }

                        break;
                    case 3:
                        Console.Write("Mata in pris: ");
                        succ = Int32.TryParse(Console.ReadLine(), out tal1);
                        Console.Write("Mata in moms: ");
                        succ = Int32.TryParse(Console.ReadLine(), out tal2);

                        if (!succ)
                        {
                            Console.WriteLine("Endast heltal får anges.");
                        }
                        else
                        {
                            Console.WriteLine($"Totalt belopp: {MomsOchBelopp(tal1, tal2)}");
                        }

                        break;
                    case 0:
                        Console.WriteLine("Avslutar...");
                        continue;
                    default:
                        break;
                }
            }

        }

        private static void PrintMeny2()
        {
            Console.WriteLine("1. Addera två tal.");
            Console.WriteLine("2. Beräkna moms.");
            Console.WriteLine("3. Beräkna belopp med moms");
            Console.WriteLine("0. Avsluta");
        }

        private static int Moms(int v)
        {
            return v / 4;
        }

        private static int MomsOchBelopp(int v1, int moms)
        {
            return v1 + v1 * moms / 100;
        }

        private static int Addera(int v1, int v2)
        {
            return v1 + v2;
        }

        private static void Uppgift1()
        {
            int saldo = 0;
            (int, int)[] handelse = new (int, int)[10];

            bool loop = true;
            int val = 1;
            int antal;

            PrintMeny();
            while (loop)
            {
                Int32.TryParse(Console.ReadLine(), out val);
                Console.Clear();
                antal = 0;


                switch (val)
                {
                    case 1:
                        Console.WriteLine($"Insättning");

                        if (Int32.TryParse(Console.ReadLine(), out antal))
                        {
                            saldo = Insattning(saldo, antal);
                            handelse = Push(handelse, saldo, antal);
                            PrintHandelse(handelse);
                        }
                        else
                        {
                            Console.WriteLine("Endast heltal är tillåtna.");
                        }
                        break;
                    case 2:
                        Console.WriteLine($"Uttag");

                        if (Int32.TryParse(Console.ReadLine(), out antal))
                        {
                            antal *= -1;
                            (bool, int) tmp = Uttag(saldo, antal);
                            if (tmp.Item1)
                            {
                                saldo = tmp.Item2;
                                handelse = Push(handelse, saldo, antal);
                                PrintHandelse(handelse);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Endast heltal är tillåtna.");
                        }
                        break;
                    case 3:
                        Saldo(saldo);
                        break;
                    case 4:
                        Console.WriteLine($"Avslutar");
                        loop = false;
                        continue;
                    default:
                        Console.Clear();
                        break;
                }
                PrintMeny();

            }
        }

        static void PrintHandelse((int, int)[] handelse)
        {
            foreach ((int saldo, int antal) in handelse)
            {
                if (antal < 0)
                {
                    Console.WriteLine($"Saldo: {saldo} \t Uttag: {-antal}");
                }
                else if (antal > 0)
                {
                    Console.WriteLine($"Saldo: {saldo} \t Insättning: {antal}");
                }

            }
        }

        static void PrintMeny()
        {
            Console.WriteLine("1. Insättning");
            Console.WriteLine("2. Uttag");
            Console.WriteLine("3. Saldo");
            Console.WriteLine("4. Avsluta");

        }

        static (int, int)[] Push((int, int)[] handelse, int saldo, int antal)
        {
            for (int i = 0; i < handelse.Length - 1; i++)
            {
                handelse[i] = handelse[i + 1];
            }

            handelse[handelse.Length - 1] = (saldo, antal);

            return handelse;
        }

        static int Insattning(int saldo, int antal)
        {
            saldo += antal;
            return saldo;
        }

        static (bool, int) Uttag(int saldo, int antal)
        {
            bool succ = false;
            if (saldo + antal >= 0)
            {
                saldo += antal;
                succ = true;
            }
            else
            {
                Console.WriteLine("Inte tillräckligt med pengar.");
            }
            return (succ, saldo);
        }

        static void Saldo(int saldo)
        {
            Console.WriteLine($"Saldo: {saldo} kr");
        }

    }
}
