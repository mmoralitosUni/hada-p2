using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    class Equipo
    {
        /// Propiedades
        public static int minJugadores { set; get; }
        public static int maxNumeroMovimientos { set; get; }
        private int movimientos { set; get; }
        public string nombreEquipo { private set; get; }

        private List<Hada.Jugador> lista_jugadores; // <---- FALTA ESTO???????
        private List<Hada.Jugador> lista_expulsados; // <---- FALTA ESTO???????
        private List<Hada.Jugador> lista_lesionados; // <---- FALTA ESTO???????
        private List<Hada.Jugador> lista_retirados; // <---- FALTA ESTO???????

        ///Metodos
        public Equipo(int nj, string nom) 
        {
            this.lista_jugadores = new List<Hada.Jugador>(nj); // !! ????? No dicen nada de una lista, pero es necesaria para almacenar los jugadores
            for (int i = 0; i < nj; i++) {
                this.lista_jugadores.Append(new Hada.Jugador("Jugador_"+i,0,0,50,0));
                this.lista_jugadores[i].amonestacionesMaximoExcedido += cuandoAmonestacionesMaximoExcedido;
                this.lista_jugadores[i].faltasMaximoExcedido += cuandoFaltasMaximoExcedido;
                this.lista_jugadores[i].energiaMinimaExcedida += cuandoEnergiaMinimaExcedida;
            }
            this.nombreEquipo = nom;
        }

        public bool moverJugadores() 
        {
            bool ret = false;
            if (this.lista_jugadores.Count < Hada.Equipo.minJugadores) {
                return ret;
            }
            int jugadores_jugables = 0;
            foreach (Hada.Jugador j in this.lista_jugadores) {
                if (j.todoOk()) j.mover(); // mueve el jugador si toca
                if (j.todoOk()) jugadores_jugables++; // si se puede seguir moviendo después, cuenta ese jugador como todavia jugable
            }
            if (jugadores_jugables >= Hada.Equipo.minJugadores) { ret = true; this.movimientos++; }
            return ret;
        }
        public void moverJugadoresEnBucle() 
        {
            while (this.moverJugadores()) ;
        }
        public int sumarPuntos() 
        {
            int suma = 0;
            foreach (Hada.Jugador j in this.lista_jugadores){
                suma+=j.puntos;
            }
            return suma;
        }
        public List<Hada.Jugador> getJugadoresExcedenLimiteAmonestaciones()
        {
            return this.lista_expulsados;
        }
        public List<Hada.Jugador> getJugadoresExcedenLimiteFaltas() 
        {
            return this.lista_lesionados;
        }
        public List<Hada.Jugador> getJugadoresExcedenMinimoEnergia() 
        {
            return this.lista_retirados;
        }
        override public string ToString() 
        {
            string str = "["+this.nombreEquipo+"] Puntos: "+this.sumarPuntos()+
                "; Expulsados: "+this.getJugadoresExcedenLimiteAmonestaciones().Count +
                "; Lesionados: "+this.getJugadoresExcedenLimiteFaltas().Count +
                "; Retirados: " +this.getJugadoresExcedenMinimoEnergia().Count + "\n";
            foreach (Hada.Jugador j in this.lista_jugadores){
                str.Concat( j.ToString());
                str.Append('\n');
            }
            return str;
        }

        // Handlers
        private void cuandoAmonestacionesMaximoExcedido(Object sender, Hada.AmonestacionesMaximoExcedidoArgs args) 
        {
            Hada.Jugador j = (Hada.Jugador)sender;
            int amonestaciones = args.amonestaciones;
            Console.Out.WriteLine("¡¡Número máximo excedido de amonestaciones. Jugador expulsado!!");
            Console.Out.WriteLine("Jugador: " + j.nombre);
            Console.Out.WriteLine("Equipo: " + this.nombreEquipo);
            Console.Out.WriteLine("Amonestaciones: " + amonestaciones);
            this.lista_expulsados.Append(j);
        }
        private void cuandoFaltasMaximoExcedido(Object sender, Hada.FaltasMaximoExcedidoArgs args)
        {
            Hada.Jugador j = (Hada.Jugador)sender;
            int faltas = args.faltas;
            Console.Out.WriteLine("¡¡Número máximo excedido de faltas recibidas. Jugador lesionado!!");
            Console.Out.WriteLine("Jugador: " + j.nombre);
            Console.Out.WriteLine("Equipo: " + this.nombreEquipo);
            Console.Out.WriteLine("Faltas: " + faltas);
            this.lista_lesionados.Append(j);
        }
        private void cuandoEnergiaMinimaExcedida(Object sender, Hada.EnergiaMinimaExcedidaArgs args)
        {
            Hada.Jugador j = (Hada.Jugador)sender;
            int energia = args.energia;
            Console.Out.WriteLine("¡¡Energía mínima excedida. Jugador retirado!!");
            Console.Out.WriteLine("Jugador: " + j.nombre);
            Console.Out.WriteLine("Equipo: " + this.nombreEquipo);
            Console.Out.WriteLine("Energía: " + energia + "%");
            this.lista_retirados.Append(j);
        }
    }
}
