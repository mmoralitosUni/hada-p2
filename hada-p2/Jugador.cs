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
            set
            {
                if (value > Jugador.maxAmonestaciones) {
                    AmonestacionesMaximoExcedidoArgs ma = new AmonestacionesMaximoExcedidoArgs(value);
                    // TODO lanzar handler
                }else if (value < 0) {
                    amonestaciones = _amonestaciones = 0;
                }else amonestaciones = _amonestaciones = value;
            }
            get { return _amonestaciones; }
        }
        private int _faltas { get; set; }
        private int faltas {
            set 
            { 
                // if (value > Jugador.maxFaltasRecibidas) { 
                if (value > Jugador.maxFaltas) {
                    FaltasMaximoExcedidoArgs mf = new FaltasMaximoExcedidoArgs(value) ;
                    // TODO lanzar handler
                }else faltas = _faltas = value;
            }
            get { return _faltas; }
        }
        private int _energia { get; set; }
        private int energia {
            set
            {
                if (value < minEnergia){
                    EnergiaMinimaExcedidaArgs em = new EnergiaMinimaExcedidaArgs(value);
                    // TODO lanzar handler
                }else if (value < 0) { 
                    energia = _energia = 0; 
                }else if (value > 100) {
                    energia = _energia = 100; 
                }else energia = _energia = value; 
            }
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

        /// Eventos
        
        public event EventHandler<AmonestacionesMaximoExcedidoArgs> amonestacionesMaximoExcedido;
        public event EventHandler<FaltasMaximoExcedidoArgs> faltasMaximoExcedido;
        public event EventHandler<EnergiaMinimaExcedidaArgs> energiaMinimaExcedida;

        /// 'Handlers'
        /*protected virtual void expulsarAmonestacion(AmonestacionesMaximoExcedidoArgs args)
        {
            EventHandler<AmonestacionesMaximoExcedidoArgs> handler = amonestacionesMaximoExcedido;
            handler(this, args);
        }
        protected virtual void lesionaFaltas(FaltasMaximoExcedidoArgs args)
        {
            EventHandler<FaltasMaximoExcedidoArgs> handler = faltasMaximoExcedido;
            handler(this, args);
        }
        protected virtual void cambiaEnergia(EnergiaMinimaExcedidaArgs args)
        {
            EventHandler<EnergiaMinimaExcedidaArgs> handler = energiaMinimaExcedida;
            handler(this, args);
        }*/
    }
    public class AmonestacionesMaximoExcedidoArgs : EventArgs
    {
        public int amonestaciones { get; }
        public AmonestacionesMaximoExcedidoArgs(int value) { this.amonestaciones = value; }
    }
   
    public class FaltasMaximoExcedidoArgs : EventArgs
    {
        public int faltas { get; }
        public FaltasMaximoExcedidoArgs(int value) { this.faltas = value; }
    }
   
    public class EnergiaMinimaExcedidaArgs : EventArgs
    {
        public int energia { get; }
        public EnergiaMinimaExcedidaArgs(int value) { this.energia = value; }
    }
}