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
    /// Clase que representa a un equipo
    /// </summary>
    class Equipo
    {
        // Propiedades
        /// <summary>
        /// Campo que contiene el minimo de los jugadores
        /// </summary>
        public static int minJugadores { set; get; }

        /// <summary>
        /// Campo de soporte del numero maximo de movimientos
        /// </summary>
        internal static int _mNM { set; get; }

        /// <summary>
        /// Campo que contiene el maximo numero de movimientos , no puede ser 0
        /// </summary>
        public static int maxNumeroMovimientos { set { _mNM = (value <= 0 ? 10 : value); } get { return _mNM; } }

        /// <summary>
        /// Campo que contiene los movimientos realizados por el equipo, el setter es privado
        /// </summary>
        public int movimientos { private set; get; }

        /// <summary>
        /// Campo que contiene el nombre del equipo, el setter es privado
        /// </summary>
        public string nombreEquipo { private set; get; }

        /// <summary>
        /// Lista de todos los jugadores del equipo
        /// </summary>
        private List<Hada.Jugador> lista_jugadores; 

        /// <summary>
        /// Lista de los jugadores con el maximo numero de faltas
        /// </summary>
        private List<Hada.Jugador> lista_expulsados;

        /// <summary>
        /// Lista de los jugadores con el maximo de faltas
        /// </summary>
        private List<Hada.Jugador> lista_lesionados;

        /// <summary>
        /// Lista de los jugadores con el minimo de energia
        /// </summary>
        private List<Hada.Jugador> lista_retirados; 

        //Metodos

        /// <summary>
        /// Constructor parametrico del equipo
        /// </summary>
        /// <param name="nj">numero de jugadores</param>
        /// <param name="nom">nombre del equipo</param>
        public Equipo(int nj, string nom) 
        {
            this.lista_jugadores = new List<Hada.Jugador>(nj); // !! ????? No dicen nada de una lista, pero es necesaria para almacenar los 
            this.lista_expulsados = new List<Hada.Jugador>(nj);
            this.lista_lesionados = new List<Hada.Jugador>(nj);
            this.lista_retirados  = new List<Hada.Jugador>(nj); 
            for (int i = 0; i < nj; i++) {
                this.lista_jugadores.Add(new Hada.Jugador("Palyer_"+i,0,0,50,0));
                this.lista_jugadores[i].amonestacionesMaximoExcedido += cuandoAmonestacionesMaximoExcedido;
                this.lista_jugadores[i].faltasMaximoExcedido += cuandoFaltasMaximoExcedido;
                this.lista_jugadores[i].energiaMinimaExcedida += cuandoEnergiaMinimaExcedida;
            }
            this.nombreEquipo = nom;
        }

        /// <summary>
        /// Mueve todos los jugadores del equipo que se puedan mover
        /// </summary>
        /// <returns>
        /// true en caso de que haya jugadores que aun se puedan mover
        /// </returns>
        public bool moverJugadores() 
        {
            bool ret = false;
            if (this.lista_jugadores.Count < Hada.Equipo.minJugadores) {
                return ret;
            }
            int jugadores_jugables = 0;
            foreach (Hada.Jugador j in this.lista_jugadores) {
                if (j.todoOk() == true) { j.mover(); } // mueve el jugador si toca
                if (j.todoOk() == true) { jugadores_jugables++; } // si se puede seguir moviendo después, cuenta ese jugador como todavia jugable
            }
            if (jugadores_jugables >= Hada.Equipo.minJugadores) { ret = true; this.movimientos++; }
            return ret;
        }

        /// <summary>
        /// Mueve en bucle a todos los jugadores
        /// </summary>
        public void moverJugadoresEnBucle() 
        {
            while (this.moverJugadores()) ;
        }

        /// <summary>
        /// Suma todos los puntos acumulados por el equipo
        /// </summary>
        /// <returns>
        /// Suma de los puntos
        /// </returns>
        public int sumarPuntos() 
        {
            int suma = 0;
            foreach (Hada.Jugador j in this.lista_jugadores){
                suma+=j.puntos;
            }
            return suma;
        }

        /// <summary>
        /// Devuelve la lista de todos los jugadores expulsados por amonestaciones
        /// </summary>
        /// <returns>Lista de jugadores</returns>
        public List<Hada.Jugador> getJugadoresExcedenLimiteAmonestaciones()
        {
            return this.lista_expulsados;
        }

        /// <summary>
        /// Devuelve la lista de todos los jugadores expulsados por lesiones
        /// </summary>
        /// <returns>Lista de jugadores</returns>
        public List<Hada.Jugador> getJugadoresExcedenLimiteFaltas() 
        {
            return this.lista_lesionados;
        }

        /// <summary>
        /// Devuelve la lista de todos los jugadores expulsados por cansancio
        /// </summary>
        /// <returns>Lista de jugadores</returns>
        public List<Hada.Jugador> getJugadoresExcedenMinimoEnergia() 
        {
            return this.lista_retirados;
        }

        /// <summary>
        /// Devuelve un string formateado con toda la informacion del jugador
        /// </summary>
        /// <returns>string formateado</returns>
        override public string ToString() 
        {
            string str = "["+this.nombreEquipo+"] Puntos: "+this.sumarPuntos()+
                "; Expulsados: "+this.getJugadoresExcedenLimiteAmonestaciones().Count +
                "; Lesionados: "+this.getJugadoresExcedenLimiteFaltas().Count +
                "; Retirados: " +this.getJugadoresExcedenMinimoEnergia().Count;
            foreach (Hada.Jugador j in this.lista_jugadores){
                str += '\n';
                str += j.ToString();
            }
            return str;
        }

        // Handlers

        /// <summary>
        /// Handler para el evento de amonestaciones maximas alcanzadas
        /// </summary>
        /// <param name="sender">Jugador que lanza el evento</param>
        /// <param name="args">Numero de amonestaciones que la lanzan</param>
        private void cuandoAmonestacionesMaximoExcedido(Object sender, Hada.AmonestacionesMaximoExcedidoArgs args) 
        {
            Hada.Jugador j = (Hada.Jugador)sender;
            int amonestaciones = args.amonestaciones;
            Console.Out.WriteLine("¡¡Número máximo excedido de amonestaciones. Jugador expulsado!!");
            Console.Out.WriteLine("Jugador: " + j.nombre);
            Console.Out.WriteLine("Equipo: " + this.nombreEquipo);
            Console.Out.WriteLine("Amonestaciones: " + amonestaciones);
            this.lista_expulsados.Add(j);
        }

        /// <summary>
        /// Handler para el evento de faltas maximas alcanzadas
        /// </summary>
        /// <param name="sender">Jugador que lanza el evento</param>
        /// <param name="args">Numero de faltas que la lanzan</param>
        private void cuandoFaltasMaximoExcedido(Object sender, Hada.FaltasMaximoExcedidoArgs args)
        {
            Hada.Jugador j = (Hada.Jugador)sender;
            int faltas = args.faltas;
            Console.Out.WriteLine("¡¡Número máximo excedido de faltas recibidas. Jugador lesionado!!");
            Console.Out.WriteLine("Jugador: " + j.nombre);
            Console.Out.WriteLine("Equipo: " + this.nombreEquipo);
            Console.Out.WriteLine("Faltas: " + faltas);
            this.lista_lesionados.Add(j);
        }

        /// <summary>
        /// Handler para el evento de energia minima alcanzada
        /// </summary>
        /// <param name="sender">Jugador que lanza la excepcion</param>
        /// <param name="args">Energia que la lanza</param>
        private void cuandoEnergiaMinimaExcedida(Object sender, Hada.EnergiaMinimaExcedidaArgs args)
        {
            Hada.Jugador j = (Hada.Jugador)sender;
            int energia = args.energia;
            Console.Out.WriteLine("¡¡Energía mínima excedida. Jugador retirado!!");
            Console.Out.WriteLine("Jugador: " + j.nombre);
            Console.Out.WriteLine("Equipo: " + this.nombreEquipo);
            Console.Out.WriteLine("Energía: " + energia + "%");
            this.lista_retirados.Add(j);
        }
    }
}
