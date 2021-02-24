using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Programa principal");

            // Asignamos las variables estaticas de Jugador
            Jugador.maxAmonestaciones = 6; // Numero maximo de amonestaciones permitidas para ser expulsado
            Jugador.maxFaltas = 8; // Numero maximo de faltas recibidas para lesionarse
            Jugador.minEnergia = 1; // Energia del jugador
            Jugador.rand = new Random();

            // Asignamos las variables estaticas de Equipo
            Equipo.maxNumeroMovimientos = 8; // numero máximo de movimientos del partido
            Equipo.minJugadores = 2; // numero minimo de jugadores permitido por equipo

            // Prueba 1 (Ejemplo de como comprobar que el jugador se mueve correctamente)
            //ProbarMoverJugador();

            // JUGAR PARTIDO
            
            // Creamos 2 equipos de 6 jugadores
            Equipo equipoA = new Equipo(6, "equipo_A");
            Equipo equipoB = new Equipo(6, "equipo_B");

            // Generamos la partida
            int estado = GenerarPartida(equipoA, equipoB);

            // Mostrar el resultado del partido en funcion del estado
            Console.WriteLine("------------------------------------------------");
            MostrarResultado(equipoA, equipoB, estado);

            Console.WriteLine("Pulse una tecla para finalizar");
            Console.ReadLine();
        }

        /* Juega el partido. Devuelve el estado en que quedará el partido
        1 -> Equipo B pierde porque no puede mover
        2 -> Equipo A pierde porque no puede mover   
        3 -> Llegamos al final del partido, por lo que compararemos puntos después
        4 -> Los dos equipos pierden, por lo tanto empatan
        */

        public static int GenerarPartida(Equipo equipoA, Equipo equipoB)
        {
            bool estadoA, estadoB;
            int estado = -1;
            
            do
            {
                estadoA = equipoA.moverJugadores();
                estadoB = equipoB.moverJugadores();

                Console.WriteLine("Movimiento " + equipoA.movimientos);

                Console.WriteLine(equipoA);
                Console.WriteLine(equipoB);

                if (!estadoA)
                {
                    if (!estadoB)
                        estado = 4; // Los dos equipos pierden por lo tanto empatan
                    else
                    {
                        // Equipo A pierde porque no puede mover
                        estado = 2;
                    }
                    break;
                }
                else
                {
                    if (!estadoB)
                    {
                        // Equipo B pierde porque no puede mover
                        estado = 1;
                        break;
                    }
                }


                // En caso de llegar al numero maximo de movimiento, comparar puntos
                if (equipoA.movimientos>Equipo.maxNumeroMovimientos)
                {
                    estado = 3;
                    break;
                }

            } while (estadoA && estadoB);


            return estado;     
        }

        // Muestra el resultado del partido según el estado
        public static void MostrarResultado(Equipo eA, Equipo eB, int estado)
        {
            switch (estado)
            {
                case 1: // Gana el equipo A (porque el equipo B ha sido descalificado)
                    Console.WriteLine("Gana el [" + eA.nombreEquipo + "] utilizando " + eA.movimientos + " movimientos.");
                    break;
                case 2: // Gana el equipo B (porque el equipo A ha sido descalificado)
                    Console.WriteLine("Gana el [" + eB.nombreEquipo + "] utilizando " + eB.movimientos + " movimientos.");
                    break;
                case 3:
                    // Hemos llegado al final de la partida (maxNumeroMoviimentos)
                    int puntosA = eA.sumarPuntos();
                    int puntosB = eB.sumarPuntos();

                    // Empate
                    if (puntosA == puntosB)
                    {
                        Console.WriteLine("El [" + eA.nombreEquipo + "] y el [" + eB.nombreEquipo + "] han empatado utilizando " + eA.movimientos + " movimientos.");
                    }
                    else
                    {
                        // Gana A
                        if (puntosA > puntosB)
                        {
                            Console.WriteLine("Gana el equipo [" + eA.nombreEquipo + "] utilizando " + eA.movimientos + " movimientos.");
                        }
                        else // Gana B
                            Console.WriteLine("Gana el equipo [" + eB.nombreEquipo + "] utilizando " + eB.movimientos + " movimientos.");

                    }

                    break;
                case 4: // Los dos equipos quedan descalificados por lo tanto quedan empatados
                    Console.WriteLine("El [" + eA.nombreEquipo + "] y el [" + eB.nombreEquipo + "] han empatado utilizando " + eA.movimientos + " movimientos (descalificados).");
                    break;

            }

            // Mostrar el estado de los equipos
            Console.WriteLine(eA);
            Console.WriteLine();
            Console.WriteLine(eB);

        }

        // Probar que el jugador se mueve correctamente
        public static void ProbarMoverJugador()
        {
            string nombre = "Leo Messi";
            int amonestaciones = 0;
            int faltas = 0;
            int energia = 50;
            int puntos = 0;

            Jugador j = new Jugador(nombre, amonestaciones, faltas, energia, puntos);

            // Estado inicial
            Console.WriteLine("Estado inicial del jugador");
            Console.WriteLine(j);
            Console.WriteLine();

            // Movimiento manual
            Console.WriteLine("Estado después de mover manualmente");
            j.incAmonestaciones();
            j.incFaltas();
            j.decEnergia();
            j.incPuntos();

            Console.WriteLine(j);
            Console.WriteLine();

            // Movimiento automatico del jugador
            Console.WriteLine("Estado después de mover automáticamente (funcion mover())");
            j.mover();
            Console.WriteLine(j);
            Console.WriteLine();
        }
    }
}
