using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Namespace de la practica, agrupa las clases Jugador, Equipo, los EventHandlerArgs y el Program
/// </summary>
namespace Hada
{
    /// <summary>
    /// Clase que representa al jugador
    /// </summary>
    class Jugador
    {
        // Campos
        
        /// <summary>
        /// Campo que contiene el maximo de amonestaciones
        /// </summary>
        public static int maxAmonestaciones { get; set; }

        /// <summary>
        /// Campo que contiene el numero maximo de faltas
        /// </summary>
        public static int maxFaltas { get; set; }

        /// <summary>
        /// Campo que contiene el minimo de energia
        /// </summary>
        public static int minEnergia { get; set; }

        /// <summary>
        /// Campo que contiene la clase random para obtener un numero aleatorio
        /// </summary>
        public static Random rand { private get; set; }

        /// <summary>
        /// Campo que contiene el nombre del jugador
        /// </summary>
        public string nombre { get; private set; }

        /// <summary>
        /// Campo que contiene los puntos marcados
        /// </summary>
        public int puntos { get; set; }

        /// <summary>
        /// Campo de respaldo para las amonestaciones
        /// </summary>
        internal int _amonestaciones { set; get; }

        /// <summary>
        /// Campo que contine las amonestaciones recividas, no puede ser menor que 0 no mayor que maxAmonestaciones
        /// </summary>
        private int amonestaciones {
            set
            {
                if (value < 0)
                {
                    _amonestaciones = 0;
                }
                else { _amonestaciones = value; }
                if (value > Hada.Jugador.maxAmonestaciones)
                {
                    Hada.AmonestacionesMaximoExcedidoArgs args = new Hada.AmonestacionesMaximoExcedidoArgs(value);
                    EventHandler<Hada.AmonestacionesMaximoExcedidoArgs> handler = this.amonestacionesMaximoExcedido;
                    handler(this, args);
                }
            }
            get { return _amonestaciones; }
        }

        /// <summary>
        /// Campo de respaldo de faltas
        /// </summary>
        internal int _faltas { get; set; }

        /// <summary>
        /// Campo que contiene las faltas, no puede ser mayor que maxFaltas
        /// </summary>
        private int faltas {
            set 
            {
                _faltas = value;
                if (value > Hada.Jugador.maxFaltas)
                {
                    Hada.FaltasMaximoExcedidoArgs args = new Hada.FaltasMaximoExcedidoArgs(value);
                    EventHandler<Hada.FaltasMaximoExcedidoArgs> handler = this.faltasMaximoExcedido;
                    handler(this, args);
                }
            }
            get { return _faltas; }
        }

        /// <summary>
        /// Campo de respaldo para energia
        /// </summary>
        internal int _energia { get; set; }

        /// <summary>
        /// Campo que contiene la energia del jugador, no puede ser menor que 0 ni mayor que 100
        /// </summary>
        private int energia {
            set
            {
                if (value < 0)
                {
                    _energia = 0;
                }
                else if (value > 100)
                {
                    _energia = 100;
                }
                else { _energia = value; }
                if (value < minEnergia)
                {
                    Hada.EnergiaMinimaExcedidaArgs args = new Hada.EnergiaMinimaExcedidaArgs(value);
                    EventHandler<Hada.EnergiaMinimaExcedidaArgs> handler = this.energiaMinimaExcedida;
                    handler(this, args);
                }
            }
            get { return _energia; }
        }

        // Metodos

        /// <summary>
        /// Constructor parametrico del jugador
        /// </summary>
        /// <param name="nombre">Nombre del jugador</param>
        /// <param name="amonestaciones">Numero de amonestaciones</param>
        /// <param name="faltas">Numero de faltas</param>
        /// <param name="energia">Numero de energia</param>
        /// <param name="puntos">Numero de puntos</param>
        public Jugador(string nombre, int amonestaciones, int faltas, int energia, int puntos)
        {
            this.nombre = nombre;
            this.amonestaciones = amonestaciones;
            this.faltas = faltas;
            this.energia = energia;
            this.puntos = puntos;
        }

        /// <summary>
        /// Incrementa las amonestaciones entre 0 y 2
        /// </summary>
        public void incAmonestaciones()
        {
            this.amonestaciones += rand.Next(0, 2 + 1);
        }

        /// <summary>
        /// Incrementa las faltas entre 0 y 3
        /// </summary>
        public void incFaltas()
        {
            this.faltas += rand.Next(0, 3 + 1);
        }

        /// <summary>
        /// Decrementa la energia entre 1 y 7 
        /// </summary>
        public void decEnergia()
        {
            this.energia -= rand.Next(1, 7 + 1);
        }

        /// <summary>
        /// Incrementa la puntuacion entre 0 y 3
        /// </summary>
        public void incPuntos()
        {
            this.puntos += rand.Next(0, 3 + 1);
        }

        /// <summary>
        /// Comprueba que un jugador puede jugar
        /// </summary>
        /// <returns>true si el jugador puede jugar</returns>
        public bool todoOk()
        {
            if (this.amonestaciones <= Hada.Jugador.maxAmonestaciones) {
                if (this.energia >= Hada.Jugador.minEnergia) {
                    if (this.faltas <= Hada.Jugador.maxFaltas) {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Mueve un jugador, incrementando amonestaciones,Faltas,Puntos y reduciendo la energia
        /// </summary>
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

        /// <summary>
        /// Devuelve un string formateado con toda la informacion del jugador
        /// </summary>
        /// <returns>string formateado</returns>
        override public string ToString()
        {
            return "[" + this.nombre + "] Puntos: " + this.puntos + "; Amonestaciones: " + this.amonestaciones + "; Faltas: " + this.faltas + "; Energia:" + this.energia + "%; Ok:" + this.todoOk();
        }

        // Eventos
        
        /// <summary>
        /// Evento que se lanza cuando se alcanza y superan las amonestaciones maximas
        /// </summary>
        public event EventHandler<Hada.AmonestacionesMaximoExcedidoArgs> amonestacionesMaximoExcedido;

        /// <summary>
        /// Evento que se lanza cuando se alcanza y superan las faltas maximas
        /// </summary>
        public event EventHandler<Hada.FaltasMaximoExcedidoArgs> faltasMaximoExcedido;

        /// <summary>
        /// Evento que se lanza cuando se alcanza y supera la energia minima
        /// </summary>
        public event EventHandler<Hada.EnergiaMinimaExcedidaArgs> energiaMinimaExcedida;
    }

    /// <summary>
    /// Calse que extiene los argumentos de un evento AmonestacionesMaximoExcedido
    /// </summary>
    public class AmonestacionesMaximoExcedidoArgs : EventArgs
    {
        /// <summary>
        /// Numero de amonestaciones
        /// </summary>
        public int amonestaciones { get; }

        /// <summary>
        /// Constructor de los argumentos del evento
        /// </summary>
        /// <param name="value">Valor de las amonestaciones</param>
        public AmonestacionesMaximoExcedidoArgs(int value) { this.amonestaciones = value; }
    }


    /// <summary>
    /// Calse que extiene los argumentos de un evento FaltasMaximoExcedido
    /// </summary>
    public class FaltasMaximoExcedidoArgs : EventArgs
    {
        /// <summary>
        /// Numero de faltas
        /// </summary>
        public int faltas { get; }

        /// <summary>
        /// Constructor de los argumentos del evento
        /// </summary>
        /// <param name="value">Valor de las faltas</param>
        public FaltasMaximoExcedidoArgs(int value) { this.faltas = value; }
    }


    /// <summary>
    /// Calse que extiene los argumentos de un evento EnergiaMinimaExcedida
    /// </summary>
    public class EnergiaMinimaExcedidaArgs : EventArgs
    {
        /// <summary>
        /// Numero de energia
        /// </summary>
        public int energia { get; }

        /// <summary>
        /// Constructor de los argumentos del evento
        /// </summary>
        /// <param name="value">Valo de la energia</param>
        public EnergiaMinimaExcedidaArgs(int value) { this.energia = value; }
    }
}