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
    }


    class Program
    {
        static Random rnd = new Random();
        static List<Fish> to = new List<Fish>();
        static void Main()
        {
            InitTo();
            KiirTo();
            Console.ReadKey(true);
        }

        private static void KiirTo()
        {
            throw new NotImplementedException();
        }

        private static void InitTo()
        {
            
        }
    }
}
