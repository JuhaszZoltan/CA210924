using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA210924
{
    enum Species
    {
        //NotDefined,
        Drum,
        Bluefish,
        Cobia,
        Bass,
        Grouper,
        Permit,
        Ladyfish,
        Trout,
        Redfish,
        Clownfish,
    }

    class Fish
    {
        private float _weight;
        private bool _isWeightSet = false;
        private bool _predator;
        private bool _isPredatorSet = false;
        private int _top;
        private int _depth;

        public float Weight
        {
            get => _weight;
            set
            {
                if (value < .5F) throw new Exception("hiba: túl kicsi a hal tömege");
                if (value > 40F) throw new Exception("hiba: túl nagy a hal");
                if (_isWeightSet && value > _weight * 1.1F) throw new Exception("hiba: ennyit nem hízhat a hal");
                if (_isWeightSet && value < _weight * .9F) throw new Exception("hiba: ennyit nem csökkenhet a hal súlya");
                _weight = value;
                _isWeightSet = true;
            }
        }
        public bool Predator
        {
            get => _predator;
            set
            {
                if (_isPredatorSet) throw new Exception("hiba: már be van állítva az étkezési szokás");
                _predator = value;
                _isPredatorSet = true;
            }
        }
        public int Top
        {
            get => _top;
            set
            {
                if (value < 0) throw new Exception("hiba: nincs3enek lebegő halak");
                if (value > 400) throw new Exception("hiba: túl mélyen van a hal úszási mélységének felső korlátja");
                _top = value;
            }
        }
        public int Depth
        {
            get => _depth;
            set
            {
                if (value < 10) throw new Exception("hiba: túl keskeny úszási sáv");
                if (value > 400) throw new Exception("hiba: túl szélesz úszási sáv");
                _depth = value;
            }
        }
        public Species Species { get; set; }

        public int DpT => Top + Depth;
    }


    class Program
    {
        static Random rnd = new Random();
        static List<Fish> to = new List<Fish>();
        static void Main()
        {
            InitTo(100);
            KiirTo();
            DbRagadozo();
            LegnagyobbHal();
            MelysekbenUszoHalakSzama(melyseg: 1.1F);
            FoLoop();
            KiirTo();
            Jelentes();
            Console.ReadKey(true);
        }

        static List<Fish> megevettHalak = new List<Fish>();

        private static void Jelentes()
        {
            float osszSuly = 0F;
            foreach (var h in megevettHalak)
                osszSuly += h.Weight;
            Console.WriteLine($"Összesen {osszSuly}Kg  növényevőt ettek meg ({megevettHalak.Count} db halat)");
        }

        private static void FoLoop()
        {
            for (int i = 0; i < 100; i++)
            {
                int x = rnd.Next(to.Count);
                int y = rnd.Next(to.Count);


                //bool harmincSzazalek = rnd.Next(100) < 30;
                bool harmincSzazalek = true;

                bool beTudUszni = 
                    to[x].DpT >= to[y].Top 
                    && to[y].DpT >= to[x].Top;

                bool egyikRagadozo = to[x].Predator != to[y].Predator;

                if (harmincSzazalek && beTudUszni && egyikRagadozo)
                {
                    Fish ragadozo;
                    Fish novenyevo;

                    if(to[x].Predator)
                    {
                        ragadozo = to[x];
                        novenyevo = to[y];
                    }
                    else
                    {
                        ragadozo = to[y];
                        novenyevo = to[x];
                    }

                    megevettHalak.Add(novenyevo);
                    to.Remove(novenyevo);
                    if(ragadozo.Weight <= 36) ragadozo.Weight *= 1.09F;
                }
            }
        }

        private static void MelysekbenUszoHalakSzama(float melyseg)
        {
            int db = 0;
            foreach (var h in to)
                if (h.Top <= melyseg * 100 && (h.Top + h.Depth) >= melyseg * 100) db++;
            Console.WriteLine($"Összesen {db} db hal képes {melyseg}m mélységben úszni");
            Console.WriteLine("-------------------");
        }

        private static void LegnagyobbHal()
        {
            int maxIndex = 0;
            for (int i = 1; i < to.Count; i++)
                if (to[i].Weight > to[maxIndex].Weight) maxIndex = i;
            Console.WriteLine($"A legnagyobb súlyú hal súlya {to[maxIndex].Weight}Kg");
            Console.WriteLine("-------------------");
        }

        private static void DbRagadozo()
        {
            int db = 0;
            foreach (var h in to) if (h.Predator) db++;
            Console.WriteLine($"Összesen {db} db ragadozó hal van a tóban");
            Console.WriteLine("-------------------");
        }

        private static void KiirTo()
        {
            Console.WriteLine("-------------------");

            foreach (var h in to)
            {
                //if (h.Predator) Console.ForegroundColor = ConsoleColor.Red;
                //else Console.ForegroundColor = ConsoleColor.Green;

                Console.ForegroundColor = 
                    h.Predator ? ConsoleColor.Red : ConsoleColor.Green;

                Console.WriteLine("[{4,2}]{0,-9} {1,4:0.0}Kg [{2,3}-{3,3}]cm",
                    h.Species, h.Weight, h.Top, h.Top + h.Depth, to.IndexOf(h));
            }
            Console.ResetColor();

            Console.WriteLine("-------------------");
        }

        private static void InitTo(int halakSzama)
        {
            for (int i = 0; i < halakSzama; i++)
            {
                to.Add(new Fish()
                {
                    Weight = rnd.Next(1, 81) / 2F,
                    Predator = rnd.Next(100) >= 90,
                    Top = rnd.Next(401),
                    Depth = rnd.Next(10, 401),
                    Species = (Species)rnd.Next(Enum.GetNames(typeof(Species)).Length),
                });
            }
        }
    }
}
