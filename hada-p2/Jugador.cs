using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    class Jugador
    {
        /// Campos

        public static int maxAmonestaciones { get; set; }
        public static int maxFaltas { get; set; }
        public static int minEnergia { get; set; }
        public static Random rand { private get; set; }
        public string nombre { get; private set; }
        public int puntos { get; set; }
        private int _amonestaciones { set; get; }
        private int amonestaciones {
            set { amonestaciones = _amonestaciones = (value > maxAmonestaciones ? throw new amonestacionesMaximoExcedido : (value < 0 ? 0 : value)); }
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

        /// Metodos

        public Jugador(string nombre, int amonestaciones, int faltas, int energia, int puntos)
        {
            this.nombre = nombre;
            this.amonestaciones = amonestaciones;
            this.faltas = faltas;
            this.energia = energia;
            this.puntos = puntos;
        }
        public void incAmonestaciones()
        {
            this.amonestaciones += rand.Next(0, 2 + 1);
        }
        public void incFaltas()
        {
            this.faltas += rand.Next(0, 3 + 1);
        }
        public void decEnergia()
        {
            this.energia -= rand.Next(1, 7 + 1);
        }
        public void incPuntos()
        {
            this.puntos += rand.Next(0, 3 + 1);
        }
        public bool todoOk()
        {
            bool ret = false;
            if (this.amonestaciones < Jugador.maxAmonestaciones && this.energia >= Jugador.minEnergia && this.faltas <= Jugador.maxFaltas)
                ret = true;
            return ret;
        }
        public void mover()
        {
            if (this.todoOk() == true)
            {
                // TODO -> cambiar posicion del jugador en el tablero
                this.incAmonestaciones();
                this.incFaltas();
                this.incPuntos();
                this.decEnergia();
            }
        }
        override public string toString()
        {
            return "[" + this.nombre + "] Puntos: " + this.puntos + "; Amonestaciones: " + this.amonestaciones + "; Faltas: " + this.faltas + "; Energia:" + this.energia + "%; Ok:" + this.todoOk() + "\n";
        }
        
    }
}
