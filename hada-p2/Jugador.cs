using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    class Jugador
    {
        public static int maxAmonestaciones { get; set; }
        public static int maxFaltas { get; set; }
        public static int minEnergia { get; set; }
        public static Random rand { private get; set; }
        public string nombre { get; private set; }
        public int puntos { get; set; }
        private int _amonestaciones { set;  get; }
        private int amonestaciones { 
            set { amonestaciones = _amonestaciones = (value > maxAmonestaciones ? throw new amonestacionesMaximoExcedido :(value<0?0:value)); }
            get { return _amonestaciones; } 
        }
        private int _faltas { get; set; }
        private int faltas {
            set { faltas = _faltas = (value > maxFaltasRecibidas ? throw new faltasMaximoExcedido : value); }
            get { return _faltas; }
        }
        private int _energia { get; set; }
        private int energia {
            set { energia = _energia = (value < minEnergia ? throw new energiaMinimaExcedida : (value < 0 ? 0 : (value > 100 ? 100 : value))); }
            get { return _energia; }
        }


    }
}
