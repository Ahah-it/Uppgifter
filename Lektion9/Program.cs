using System;
using System.Threading;
using System.Linq;
using System.Collections.Generic;

namespace Lektion9
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> personer;
            List<Tjuv> fängslade = new List<Tjuv>();

            int width = 64;
            int height = 32;

            int antalPoliser = 27;
            int antalMedborgare = 65;
            int antalTjuvar = 38;

            Initialize(out personer, width, height, antalPoliser, antalMedborgare, antalTjuvar);


            PrintKarta(personer, width, height);
            Random rng = new Random();
            while (true)
            {
                Console.Clear();
                foreach (Person p in personer)
                {
                    p.Move(width, height);
                    p.Direction.X = rng.Next(-1, 1);
                    p.Direction.Y = rng.Next(-1, 1);

                }

                PrintKarta(personer, width, height);
                Stjäl(personer);
                Beslagta(personer);

                UppdateraFängelse(personer, fängslade);
                PrintFängelse(fängslade);

                Thread.Sleep(500);
            }
        }

        private static void PrintFängelse(List<Tjuv> fängslade)
        {
            Console.WriteLine($"Antal Tjuvar i Fängelse: {fängslade.Count}");
            Console.WriteLine("Tid tills fångar släpps");


            foreach (Tjuv t in fängslade)
            {
                Console.WriteLine($"{t.Släpps.Subtract(DateTime.Now).Seconds}");

            }
        }

        private static void UppdateraFängelse(List<Person> personer, List<Tjuv> fängslade)
        {
            //Lägg till märkta tjuvar i fängelse.
            foreach (Tjuv t in personer.OfType<Tjuv>())
            {
                if (t.Fängelse)
                {
                    fängslade.Add(t);
                }
            }

            //Ta Bort märkta tjuvar från personer.
            for (int i = 0; i < personer.Count; i++)
            {
                if (personer[i] is Tjuv)
                {
                    Tjuv tmp = personer[i] as Tjuv;
                    if (tmp.Fängelse)
                    {
                        personer.RemoveAt(i);
                    }
                }
            }

            //Släpp Tjuvar från fängelse och lägg till dem till personer.
            for (int i = 0; i < fängslade.Count; i++)
            {
                if (DateTime.Compare(fängslade[i].Släpps, DateTime.Now) <= 0)
                {

                    fängslade[i].SläppFri();
                    personer.Add(fängslade[i]);
                    fängslade.RemoveAt(i);
                }
            }
        }

        private static void PrintKarta(List<Person> personer, int width, int height)
        {
            char[,] karta = GenereraKarta(personer, width, height);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Console.Write($"{karta[j, i]}");
                }
                Console.WriteLine("");
            }



        }

        private static char[,] GenereraKarta(List<Person> personer, int width, int height)
        {
            char[,] karta = new char[width, height];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    karta[j, i] = '.';
                }

            }

            foreach (Person p in personer)
            {
                if (p is Polis)
                    karta[p.Position.X, p.Position.Y] = 'P';
                if (p is Medborgare)
                    karta[p.Position.X, p.Position.Y] = 'M';
                if (p is Tjuv)
                    karta[p.Position.X, p.Position.Y] = 'T';
            }

            return karta;
        }

        private static void Initialize(out List<Person> personer, int width, int height, int antalPoliser, int antalMedborgare, int antalTjuvar)
        {
            personer = new List<Person>();

            Random rng = new Random();


            for (int i = 0; i < antalPoliser; i++)
            {
                Direction dir = new Direction(rng.Next(-1, 1), rng.Next(-1, 1));
                Position pos = new Position(rng.Next(0, width), rng.Next(0, height));
                personer.Add(new Polis(pos, dir));
            }

            for (int i = 0; i < antalMedborgare; i++)
            {
                Direction dir = new Direction(rng.Next(-1, 1), rng.Next(-1, 1));
                Position pos = new Position(rng.Next(0, width), rng.Next(0, height));
                Medborgare tmp = new Medborgare(pos,dir);
                tmp.Items.Push(Item.Klocka);
                tmp.Items.Push(Item.Nycklar);
                tmp.Items.Push(Item.Pengar);
                tmp.Items.Push(Item.Mobil);

                personer.Add(tmp);
            }

            for (int i = 0; i < antalTjuvar; i++)
            {
                Direction dir = new Direction(rng.Next(-1, 1), rng.Next(-1, 1));
                Position pos = new Position(rng.Next(0, width), rng.Next(0, height));
                personer.Add(new Tjuv(pos, dir));
            }
        }

        private static void Beslagta(List<Person> personer)
        {
            foreach (Polis p in personer.OfType<Polis>())
            {
                foreach (Tjuv t in personer.OfType<Tjuv>())
                {
                    if (p.Position == t.Position)
                    {
                        string beslagtagit = "";
                        while (!t.Items.IsEmpty())
                        {
                            p.Items.Push(t.Items.Pop());
                            beslagtagit += p.Items.Peek().ToString() + ",";
                            t.Fängsla();
                        }
                        if (beslagtagit != "")
                        {
                            Console.WriteLine("Polis beslagtog: " + beslagtagit);
                        }
                    }

                }
            }
        }

        private static void Stjäl(List<Person> personer)
        {

            foreach (Tjuv tjuv in personer.OfType<Tjuv>())
            {
                foreach (Medborgare m in personer.OfType<Medborgare>())
                {
                    if (m.Position == tjuv.Position)
                    {
                        if (!m.Items.IsEmpty())
                        {
                            tjuv.Items.Push(m.Items.Pop());
                            Console.WriteLine($"En tjuv stal: {tjuv.Items.Peek()}");

                        }
                    }
                }
            }
        }
    }
}
