
using System;
using System.Collections.Generic;

namespace Lektion6
{
    class Program
    {
        static void Main(string[] args)
        {


            var diskotek = new List<Person>();
            diskotek.Add(new Person(true, 0, false));

             for(int i=0; i < 10000; i++) {
                diskotek.Add(new Person(false, 0, false));
             }


             Console.WriteLine($"{diskotek.FindAll(p => p.Immun == false).Count}");


            int timme = 0;
            while(true) {

                timme++;

                foreach(Person p in diskotek) {
                    p.Frisk(timme);
                }


                //Antalet smittade personer som ej är immuna.
                var resultat = diskotek.FindAll(p => p.Smittad == true && p.Immun == false).Count;

                Console.WriteLine($"Timme: {timme}, Antal smittade: {resultat}");

                //Smitta endast personer som är friska och inte immuna.
                for(int i=0; i < resultat; i++) {
                    var t = diskotek.Find(p => p.Smittad == false && p.Immun == false);
                    if (!(t is null)) {
                        t.Smitta(timme);
                    }
                }

                if(diskotek.FindAll(p => p.Immun == false).Count == 0) {
                    break;
                }

            }
        }

    }


    class Person {

        private bool smittad;
        public bool Smittad { get => smittad; private set => smittad = value; }

        public int SmittadNär { get => smittadNär; private set => smittadNär = value; }
        private int smittadNär;

        public bool Immun { get => immun; private set => immun = value; }
        private bool immun;

        public Person(bool Smittad, int SmittadNär, bool Immun) {
            this.Smittad = Smittad;
            this.SmittadNär = SmittadNär;
            this.Immun = Immun;
        }


        public bool Smitta(int timme) {
            if (Immun) {
                return false;
            } else {
                SmittadNär = timme;
                Smittad = true;
                return true;
            }
        }

        public void Frisk(int nuvarandeTimme) {
            if ( nuvarandeTimme - 5 >= SmittadNär && Immun == false && Smittad == true) {
                Smittad = false;
                Immun = true;
            }
        }
    }
}
