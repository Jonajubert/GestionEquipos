using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimerParcial
{
    /// <summary>
    /// Creamos estructura Categoria con atributos y restricciones de edad y cupo
    /// </summary>
    public struct Categoria
    {
        public string Nombre;
        public int EdadMin;
        public int EdadMax;
        public int CupoMinimo;
    }

    /// <summary>
    /// Creamos estructura Equipo con sus atributos 
    /// </summary>
    public struct Equipo
    {
        public int Id;
        public string Nombre;
        public string Club;
        public string NombreCategoria;
    }

    /// <summary>
    /// Creamos estructura Jugador con sus atributos
    /// </summary>
    /// 
    public struct Jugador
    {
        public string DNI;
        public string Nombre;
        public string Apellido;
        public int Edad;
        public int IdEquipo;
        public bool TieneSeguro;
        public bool EstaAfiliado;
    }

    internal class Program
    {
        // LISTAS GLOBALES para guardar en cada caso
        static List<Categoria> listaCategorias = new List<Categoria>();
        static List<Equipo> listaEquipos = new List<Equipo>();
        static List<Jugador> listaJugadores = new List<Jugador>();
        static int contadorEquipos; // Nos va a dar el ID de cada Equipo. Se autoincrementa
       
        /// <summary>
        ///Utilizamos la funcion de carga inicial de categorias para que el sistema funcione. 
        /// </summary>
       
        static void Main(string[] args)
        {
          
         
            InicializarCategorias();
            
            bool continuar = true;

            //Menu con opciones y sus funciones 

            while (continuar)
            {
                /// <summary>
                /// limpiar pantalla con clear()
                /// </summary>
                /// 
                Console.Clear();
                Console.WriteLine("=== MENU DE GESTIÓN DEPORTIVA ===");
                
                Console.WriteLine("--- EQUIPOS ---");
                Console.WriteLine("1. Alta de Equipo");
                Console.WriteLine("2. Baja de Equipo");
                Console.WriteLine("3. Modificar Equipo");
                
                Console.WriteLine("--- JUGADORES ---");
                Console.WriteLine("4. Alta de Jugador");
                Console.WriteLine("5. Baja de Jugador");
                Console.WriteLine("6. Modificar Jugador");
                
                Console.WriteLine("--- REPORTES ---");
                Console.WriteLine("7. Listar Jugadores Asegurados");
                Console.WriteLine("8. Listar Jugadores por Edad");
                Console.WriteLine("9. Listar Jugadores por Categoria");
                Console.WriteLine("10. Mostrar Reporte General");

                Console.WriteLine("------------------");
                Console.WriteLine("0. Salir");
                Console.Write("\nSeleccione una opción: ");

                string opcion = Console.ReadLine();

                //opciones con la aplicacion de funciones para cada caso
                switch (opcion)
                {
                    case "1":
                        AltaEquipo();
                        break;

                    case "2":
                        BajaEquipo();
                        break;

                    case "3":
                        ModificarEquipo();
                        break;

                    case "4":
                        AltaJugador();
                        break;

                    case "5":
                        BajaJugador();
                        break;

                    case "6":
                        ModificarJugador();
                        break;

                    case "7":
                        ListarAsegurados();
                        break;

                    case "8":
                        ListarJugadoresOrdenadosPorEdad();
                        break;

                    case "9":
                        ListarJugadoresAgrupadosPorCategoria();
                        break;

                    case "10":
                        MostrarReporteGeneral();
                        break;

                    case "0":
                        continuar = false;
                        break;

                    default:
                        Console.WriteLine("La opcion indicada no es valida. Intente de nuevo.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // GESTION DE DATOS

        /// <summary>
        /// Creamos funcion para definir categorias y valores predeterminados
        /// </summary>
        static void InicializarCategorias()
        {
            // Limpiamos la lista 
            listaCategorias.Clear();

            // Infantiles: hasta 13 años
            listaCategorias.Add(new Categoria //Generamos una estructura categoría y la agregamos a la lista
            {
                Nombre = "Infantiles",
                EdadMin = 5,
                EdadMax = 13,
                CupoMinimo = 9
            });

            // Cadetes: 13 a 16 años
            listaCategorias.Add(new Categoria
            {
                Nombre = "Cadetes",
                EdadMin = 14,
                EdadMax = 16,
                CupoMinimo = 9
            });

            // Juveniles: 16 a 18 años
            listaCategorias.Add(new Categoria
            {
                Nombre = "Juveniles",
                EdadMin = 17,
                EdadMax = 18,
                CupoMinimo = 9
            });

            // Primera: mayores de 18 años
            listaCategorias.Add(new Categoria
            {
                Nombre = "Primera",
                EdadMin = 19,
                EdadMax = 34,
                CupoMinimo = 9
            });

            // Veteranos: mayores de 35 años
            listaCategorias.Add(new Categoria
            {
                Nombre = "Veteranos",
                EdadMin = 35,
                EdadMax = 99,
                CupoMinimo = 10
            });

        }
        
        /// <summary>
        /// Pedimos los datos al usuario y devolvemos en pantalla 
        /// <param name="idAUsar"> recibe el id de cada equipo </param>
        /// <returns> Devuelve equipo con los datos que ingresa un usuario </returns>
        /// </summary>
        
        static Equipo CargarDatosEquipo(int idAUsar)

        {
            
            Console.WriteLine("Ingrese el nombre del Club: ");
            string club = Console.ReadLine();

            //recorremos listado de categorias y mostramos el nombre con el numero autoincrementandose
            Console.WriteLine("Seleccione una categoría:");
            for (int i = 0; i < listaCategorias.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + listaCategorias[i].Nombre);//i+1 para que una categoria no quede con cero
            }
            //usuario elige categoria por numero

            int opcion = int.Parse(Console.ReadLine());
            //restamos uno a la opcion ya que el indice comenzo en cero
            string cat = listaCategorias[opcion - 1].Nombre;
            
            // Armamos la estructura
            Equipo e;
            e.Id = idAUsar;// Usamos el ID que nos manden (el nuevo o el viejo)
            e.Club = club;
            e.NombreCategoria = cat;
            e.Nombre = club + " Equipo " + idAUsar + " " + cat;//El nombre del club se genera con nombre seleccionado + id (1,2, etc) + categoria 
            return e;
        }

        static void AltaEquipo()
            ///<summary> Solicitamos datos para dar de alta equipo si no existe, lo agregamos a lista </summary>
        {
            contadorEquipos++;
            // Invocamos el procedimiento pasandole el nuevo ID
            Equipo nuevo = CargarDatosEquipo(contadorEquipos);

            // Solo falta validar si el nombre ya existe y agregar
            bool existe = false;

            foreach (Equipo e in listaEquipos)
            {
                // Comparamos el nombre del equipo que acabamos de "cargar" con los nombres que ya están en la lista
                if (e.Nombre == nuevo.Nombre)
                {
                    existe = true;
                    break; // Si lo encontramos, dejamos de buscar y mostramos mensaje de error
                }
            }
            if (existe)
            {
                Console.WriteLine("Error: El equipo '" + nuevo.Nombre + "' ya existe en el sistema.");
                Console.ReadKey();
            }
            else
            {
                //Si nombre del equipo no existe lo damos de alta
                listaEquipos.Add(nuevo);
                Console.WriteLine("Alta de equipo exitosa");
                Console.WriteLine($"Su nombre: {nuevo.Nombre}");
                Console.ReadKey();
            }
        }

        static void BajaEquipo()

            ///<summary> solicitamos ID del equipo a borrar, si no tiene jugadores lo elimino</summary>
        {
            Console.WriteLine("Ingrese el ID del equipo a borrar:");
            int idBuscado = int.Parse(Console.ReadLine());

            // 1. Buscamos el equipo y lo guardamos en una variable temporal
            Equipo equipoEncontrado = new Equipo(); // solo tiene el id
            bool existe = false;

            foreach (Equipo e in listaEquipos)
            {
                if (e.Id == idBuscado)
                {
                    equipoEncontrado = e;//completo el resto de los datos
                    existe = true;//lo encontré
                    break;
                }
            }
            // 2. Si lo encontramos, mostramos y borramos
            if (existe)
            {
                Console.WriteLine("Datos del equipo a borrar:");
                Console.WriteLine("Nombre: " + equipoEncontrado.Nombre + " - Club: " + equipoEncontrado.Club);

                if (EquipoTieneJugadores(equipoEncontrado.Id)) //aca usamos el condicional para ver si tenia jugadores ya que no se podria eliminar el equipo
                {
                    Console.WriteLine("No se puede eliminar: el equipo tiene jugadores asignados.");
                    Console.ReadKey();
                    return;
                }
                // Borramos el objeto directamente
                listaEquipos.Remove(equipoEncontrado);

                Console.WriteLine("Equipo eliminado");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("No se encontro ningun equipo con ese ID.");
                Console.ReadKey();
            }
        }
        /// <summary>solicito ID del equipo y si coincide hago la modificacion con nuevos datos ingresados </summary>
        static void ModificarEquipo()
        
        {
            Console.WriteLine("ID a modificar: ");
            int idBuscado = int.Parse(Console.ReadLine());
            int indice = -1; //utilizamos -1 ya que es una posicion que no aparecera (false)

            // Buscamos la posición usando un for tradicional
            for (int i = 0; i < listaEquipos.Count; i++)
            {
                if (listaEquipos[i].Id == idBuscado)
                {
                    indice = i;
                    break;
                }
            }
            if (indice != -1)
            {
                // Pasamos el ID original para que no cambie
                Equipo equipoEditado = CargarDatosEquipo(idBuscado);//Utilizo la función creada para el alta


                // Pisamos la posicion vieja con la nueva estructura
                listaEquipos[indice] = equipoEditado;
                Console.WriteLine("Modificado con éxito.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("No se pudo encontrar el ID.");
                Console.ReadKey();
            }
        }
        /// <summary>
        /// valida datos ingresados para nuevo jugador y si dni existe da mensje de error, si es valida edad y categ lo agrega a equipos deseados
        /// </summary>
        static void AltaJugador()
        {
            Console.WriteLine("Ingrese DNI del jugador:");
            string dniIngresado = Console.ReadLine();

            //validamos si el DNI ya esta registrado en el sistema 

            foreach (Jugador j in listaJugadores)
            {
                if (j.DNI == dniIngresado)
                {
                    Console.WriteLine("Error: Ya existe un jugador con ese DNI.");
                    Console.ReadKey();
                    return;
                }
            }

            // Averiguamos si ya pertenece a un club antes de seguir
            string clubActual = ObtenerClubDelJugador(dniIngresado);

            Console.Write("Nombre: ");
            string nombreCargado = Console.ReadLine();
            Console.Write("Apellido: ");
            string apellidoCargado = Console.ReadLine();
            Console.Write("Edad: ");
            int edadCargada = int.Parse(Console.ReadLine());

            bool agregarOtroEquipo = true;//esta variable la usamos para darlo de alta en todos los equipos

            while (agregarOtroEquipo)
            {
                Console.WriteLine("\n--- Asignación de Equipo ---");
                Console.Write("Ingrese el ID del equipo: ");
                int idEquipoCargado = int.Parse(Console.ReadLine());

                //Recuperar el equipo y su club/categoria
                Equipo equipoSeleccionado = new Equipo();
                bool equipoExiste = false;
                foreach (Equipo e in listaEquipos)//Busco el equipo para dar de alta el jugador
                {
                    if (e.Id == idEquipoCargado)
                    {
                        equipoSeleccionado = e;
                        equipoExiste = true;
                        break;
                    }
                }
                if (!equipoExiste)
                {
                    Console.WriteLine("Error. El ID de equipo no existe.");
                    continue;
                }
                //VALIDACION DE CLUB:Es del mismo club que ya tenia?
                if (clubActual != "" && clubActual != equipoSeleccionado.Club)
                {
                    Console.WriteLine($"Error: El jugador ya pertenece al club '{clubActual}'. No puede unirse a '{equipoSeleccionado.Club}'.");
                }
                // 3. VALIDACION DE EDAD
                else if (ValidarEdadEnCategoria(edadCargada, equipoSeleccionado.NombreCategoria))
                {
                    Jugador nuevoJugador;
                    nuevoJugador.DNI = dniIngresado;
                    nuevoJugador.Nombre = nombreCargado;
                    nuevoJugador.Apellido = apellidoCargado;
                    nuevoJugador.Edad = edadCargada;
                    nuevoJugador.IdEquipo = idEquipoCargado;

                    Console.Write("¿Tiene seguro? (S/N): ");
                    nuevoJugador.TieneSeguro = Console.ReadLine().ToUpper() == "S";
                    Console.Write("¿Esta afiliado? (S/N): ");
                    nuevoJugador.EstaAfiliado = Console.ReadLine().ToUpper() == "S";

                    listaJugadores.Add(nuevoJugador);
                    clubActual = equipoSeleccionado.Club; // Actualizamos por si era su primer equipo
                    Console.WriteLine("Jugador asignado correctamente.");
                }
                else
                {
                    Console.WriteLine("Error. La edad no corresponde a la categoria.");
                }

                Console.Write("\n¿Desea incluirlo en otro equipo? (S/N): ");
                agregarOtroEquipo = Console.ReadLine().ToUpper() == "S";
            }
        }

        /// <summary>
        /// modifico datos del jugador solicitando dni 
        /// </summary>
        static void ModificarJugador()
        {
            Console.WriteLine("Ingrese DNI del jugador:");
            string dniIngresado = Console.ReadLine();
            bool encontrado = false;

            for (int i = 0; i < listaJugadores.Count; i++)
            {
                if (listaJugadores[i].DNI == dniIngresado)//busco el DNI en la lista de jugadores
                {
                    // Como es estructura, hay que crear uno nuevo o copiarlo, modificarlo y reasignarlo
                    Jugador j = listaJugadores[i];

                    Console.Write("Nuevo Nombre: ");
                    j.Nombre = Console.ReadLine();
                    Console.Write("Nuevo Apellido: ");
                    j.Apellido = Console.ReadLine();
                    Console.Write("Nueva Edad: ");
                    j.Edad = int.Parse(Console.ReadLine());

                    listaJugadores[i] = j; // reasignacion
                    encontrado = true;
                    Console.WriteLine("Datos actualizados.");
                    break;
                }
            }
            if (!encontrado) Console.WriteLine("El DNI no existe.");

            Console.ReadKey();
        }

        /// <summary>
        /// si el dni existe, remuevo el jugador
        /// </summary>
        static void BajaJugador()
        {
            Console.WriteLine("Ingrese DNI:");
            string dni = Console.ReadLine();

            // Elimina todos los registros que coincidan y te devuelve cuantos se borraron en variable de contador
            int contador = 0;

            for (int i = listaJugadores.Count - 1; i >= 0; i--)
            {
                if (listaJugadores[i].DNI == dni)
                {
                    listaJugadores.RemoveAt(i);
                    contador++;
                }
            }

            if (contador > 0)
            {
                Console.WriteLine($"Se elimino al jugador de {contador} equipos.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("No se encontro ningun jugador con ese mismo DNI.");
                Console.ReadKey();
            }
        }
        // ==========================================
        // VALIDACIONES

        /// <summary>
        /// retorna club del jugador por su dni
        /// </summary>
        /// <param name="dni"> dni del jugador </param>
        /// <returns> club si esta registrado o vacio si no esta registrado</returns>
        
        static string ObtenerClubDelJugador(string dni)

        {
            foreach (Jugador j in listaJugadores)
            {
                if (j.DNI == dni)
                {
                    // Buscamos el equipo para saber el club
                    foreach (Equipo e in listaEquipos)
                    {
                        if (e.Id == j.IdEquipo) 
                            return e.Club;
                    }
                }
            }
            return ""; // No tiene club asignado aun

        }


        ///<summary> Retorna verdadero si la edad es valida para la categoria</summary>
        ///<param name="edad"> Edad del jugador </param>
        ///<param name="nomCat"> Categ. del jugador </param>
        ///<returns> true en caso de edad permitida y false en caso falso </returns>
        ///
        static bool ValidarEdadEnCategoria(int edad, string nomCat)
        {
            foreach (Categoria categoria in listaCategorias)
            {
                if (categoria.Nombre == nomCat)
                {
                    if (edad >= categoria.EdadMin && edad <= categoria.EdadMax)
                        return true;  // la edad es valida
                }
            }
            return false;  // no se encontro la categoría o la edad no corresponde
        }
        ///<summary> verifico si equipo tiee jugadores </summary>
        ///<param name="idEquipo"> id del equipo</param>
        ///<returns> devuelve true o false esegun si tiene jugadores asignados</returns>
        static bool EquipoTieneJugadores(int idEquipo)
        {
            foreach (Jugador jugador in listaJugadores)
            {
                if (jugador.IdEquipo == idEquipo)
                    return true;
            }
            return false;
        }

        ///<summary> valida si equipo por id tiene cupo minimo o no</summary>
        ///<param name="idEquipo"> id de equipo a buscar</param>
        ///<returns> true o false segun la condicion (si tiene cupo minimo es true)</returns>
        static bool ValidarCupoMinimo(int idEquipo)
        {
            int contador = 0;
            foreach (Jugador jugador in listaJugadores)
            {
                if (jugador.IdEquipo == idEquipo)
                {
                    contador++;
                }
            }
            foreach (Equipo equipo in listaEquipos)
            {
                if (equipo.Id == idEquipo)
                {
                    foreach (Categoria cat in listaCategorias)
                    {
                        if (cat.Nombre == equipo.NombreCategoria)
                            return contador >= cat.CupoMinimo;
                    }
                }
            }
            return false;
        }

        // REPORTES

        /// <summary>
        /// transforma un valor booleano en "si" o "no" para mostrar en pantalla.
        /// </summary>
        /// <param name="valor">Valor a convertir.</param>
        /// <returns>"Si" si el valor es true, "No" si es falso.</returns>
        static string TextoSiNo(bool valor)
        {
            if (valor == true)
            {
                return "Si";
            }
            else
            {
                return "No";
            }
        }

        
        ///<summary> Devuelve cateogira del jugador buscado</summary> 
        ///<param name="jugador"> jugador a buscar </param>
        ///<returns> devuelve nombre de la categ o sin equipo en caso que sea falso</returns>
        static string ObtenerCategoriaDelJugador(Jugador jugador)
        {
            if (jugador.IdEquipo == 0)
            {
                return "Sin equipo";
            }

            foreach (Equipo equipo in listaEquipos)
            {
                if (equipo.Id == jugador.IdEquipo)
                {
                    return equipo.NombreCategoria;
                }
            }

            return "Sin equipo";
        }

        ///<summary> listar jugadores que tienen seguro </summary>
        static void ListarAsegurados()
        {
            Console.WriteLine("****** JUGADORES ASEGURADOS ******");

            bool hayJugadores = false;

            foreach (Jugador jugador in listaJugadores)
            {
                if (jugador.TieneSeguro == true)
                {
                    hayJugadores = true;

                    Console.WriteLine($"DNI: {jugador.DNI}");
                    Console.WriteLine($"Nombre: {jugador.Nombre} {jugador.Apellido}");
                    Console.WriteLine($"Edad: {jugador.Edad}");
                    Console.WriteLine($"Seguro: {TextoSiNo(jugador.TieneSeguro)}");
                    Console.WriteLine($"Afiliado: {TextoSiNo(jugador.EstaAfiliado)}");
                    Console.WriteLine($"Categoria/s: {ObtenerCategoriaDelJugador(jugador)}");
                    Console.WriteLine("*****************************************");
                }
            }

            if (hayJugadores == false)
            {
                Console.WriteLine("No hay jugadores asegurados registrados.");
            }
            Console.ReadKey();

        }



        ///<summary> lista jugadores de menor a mayor </summary>
        static void ListarJugadoresOrdenadosPorEdad()
        {
            Console.WriteLine("****** JUGADORES ORDENADOS POR EDAD ******");

            if (listaJugadores.Count == 0)
            {
                Console.WriteLine("No hay jugadores registrados.");
                Console.ReadKey();
                return;
            }

            // Se crea una copia de la lista original para evitar modificar la lista original, metodo visto en clase practica

            List<Jugador> jugadoresOrdenados = new List<Jugador>();

            foreach (Jugador jugador in listaJugadores)
            {
                jugadoresOrdenados.Add(jugador);
            }

            // Compara jugadores y los acomoda segun la edad

            for (int i = 0; i < jugadoresOrdenados.Count - 1; i++)
            {
                for (int j = 0; j < jugadoresOrdenados.Count - 1 - i; j++)
                {
                    // Si el jugador actual tiene mas edad que el siguiente, se intercambian.

                    if (jugadoresOrdenados[j].Edad > jugadoresOrdenados[j + 1].Edad)
                    {
                        Jugador auxiliar = jugadoresOrdenados[j];

                        jugadoresOrdenados[j] = jugadoresOrdenados[j + 1];

                        jugadoresOrdenados[j + 1] = auxiliar;
                    }
                }
            }

            // Muestra la lista ya ordenada.
            foreach (Jugador jugador in jugadoresOrdenados)
            {
                Console.WriteLine($"DNI: {jugador.DNI}");
                Console.WriteLine($"Nombre: {jugador.Nombre} {jugador.Apellido}");
                Console.WriteLine($"Edad: {jugador.Edad}");
                Console.WriteLine($"Seguro: {TextoSiNo(jugador.TieneSeguro)}");
                Console.WriteLine($"Afiliado: {TextoSiNo(jugador.EstaAfiliado)}");
                Console.WriteLine($"Categoria: {ObtenerCategoriaDelJugador(jugador)}");
                Console.WriteLine("*****************************************");
             
            }
            Console.ReadKey();
        }

        /// <summary>
        /// Verifica si un jugador pertenece a una categoria determinada segun su equipo asignado.
        /// </summary>
        /// <param name="jugador">El jugador a revisar </param>
        /// <param name="nombreCategoria">El nombre de la categoria para verificar.</param>
        /// <returns>verdadero si el jugador pertenece a la categoria y falso si no pertence a esa categ.</returns>
       
        static bool JugadorPerteneceACategoria(Jugador jugador, string nombreCategoria)
        {
            if (jugador.IdEquipo == 0)
            {
                return false;
            }

            foreach (Equipo equipo in listaEquipos)
            {
                if (equipo.Id == jugador.IdEquipo && equipo.NombreCategoria == nombreCategoria)
                {
                    return true;
                }
           
            }

            return false;

        }

        /// <summary>lista a jugadores por categoria mostrando en pantalla</summary>
        static void ListarJugadoresAgrupadosPorCategoria()
        {
            Console.WriteLine("****** JUGADORES AGRUPADOS POR CATEGORIA ******");

            if (listaJugadores.Count == 0)
            {
                Console.WriteLine("No hay jugadores registrados.");
                Console.ReadKey();
                return;
            }

            foreach (Categoria categoria in listaCategorias)
            {
                Console.WriteLine();
                Console.WriteLine($"**** {categoria.Nombre} ****");

                bool hayJugadoresEnCategoria = false;

                foreach (Jugador jugador in listaJugadores)
                {
                    if (JugadorPerteneceACategoria(jugador, categoria.Nombre) == true)
                    {
                        hayJugadoresEnCategoria = true;

                        Console.WriteLine($"DNI: {jugador.DNI}");
                        Console.WriteLine($"Nombre: {jugador.Nombre} {jugador.Apellido}");
                        Console.WriteLine($"Edad: {jugador.Edad}");
                        Console.WriteLine($"Seguro: {TextoSiNo(jugador.TieneSeguro)}");
                        Console.WriteLine($"Afiliado: {TextoSiNo(jugador.EstaAfiliado)}");
                        Console.WriteLine("*******************************************");
                        
                    }
                }

                if (hayJugadoresEnCategoria == false)
                {
                    Console.WriteLine("No hay jugadores en esta categoria.");
                }

                Console.ReadKey();

            }
        }

        /// <summary> Metodo auxiliar para contar la cantidad de jugadores que hay en una categoria</summary>
        ///
        static int ContarJugadoresPorCategoria(string nombreCategoria)
        {
            int contador = 0;

            foreach (Jugador jugador in listaJugadores)
            {
                if (JugadorPerteneceACategoria(jugador, nombreCategoria) == true)
                {
                    contador++;
                }
            }

            return contador;
        }

        /// <summary> Resumen general con el jugador mas joven, el mas viejo y cantidad de jugadores por categoria y promedio de edad general</summary> 
        static void MostrarReporteGeneral()
        {
            Console.WriteLine("****** REPORTE GENERAL ******");

            if (listaJugadores.Count == 0)
            {
                Console.WriteLine("No hay jugadores registrados.");
                Console.ReadKey();
                return;
            }

            Jugador jugadorMasJoven = listaJugadores[0];
            Jugador jugadorMasViejo = listaJugadores[0];

            int sumaEdades = 0;

            foreach (Jugador jugador in listaJugadores)
            {
                sumaEdades = sumaEdades + jugador.Edad;

                if (jugador.Edad < jugadorMasJoven.Edad)
                {
                    jugadorMasJoven = jugador;
                }

                if (jugador.Edad > jugadorMasViejo.Edad)
                {
                    jugadorMasViejo = jugador;
                }
            }

            double promedioEdad = (double)sumaEdades / listaJugadores.Count;

            Console.WriteLine();
            Console.WriteLine("***Jugador mas joven***");
            Console.WriteLine($"DNI: {jugadorMasJoven.DNI}");
            Console.WriteLine($"Nombre: {jugadorMasJoven.Nombre} {jugadorMasJoven.Apellido}");
            Console.WriteLine($"Edad: {jugadorMasJoven.Edad}");
            Console.WriteLine($"Categoria/s: {ObtenerCategoriaDelJugador(jugadorMasJoven)}");
            Console.WriteLine();
            Console.WriteLine("***Jugador mas viejo***");
            Console.WriteLine($"DNI: {jugadorMasViejo.DNI}");
            Console.WriteLine($"Nombre: {jugadorMasViejo.Nombre} {jugadorMasViejo.Apellido}");
            Console.WriteLine($"Edad: {jugadorMasViejo.Edad}");
            Console.WriteLine($"Categoria/s: {ObtenerCategoriaDelJugador(jugadorMasViejo)}");
            Console.WriteLine("************************************************************");

            // Cantidad de jugadores por categoria
            Console.WriteLine("Cantidad de jugadores por categoria:");

            foreach (Categoria categoria in listaCategorias)
            {
                int cantidad = ContarJugadoresPorCategoria(categoria.Nombre);

                Console.WriteLine($"{categoria.Nombre}: {cantidad}");
            }

            //Promedio de edad general
            Console.WriteLine("Promedio de edad general");

            Console.WriteLine($"Promedio de edad general: {promedioEdad.ToString("0.00")}");

            Console.WriteLine("=========================");
            //Equipos que no cumplen con el cupo minimo
            Console.WriteLine("Equipos que no cumplen con el cupo minimo:");

            bool hayEquiposSinCupo = false;

            foreach (Equipo equipo in listaEquipos)
            {
                if (!ValidarCupoMinimo(equipo.Id)) //usamos el metodo anteriormente creado
                {
                    hayEquiposSinCupo = true;
                    Console.WriteLine($"- {equipo.Nombre} (Club: {equipo.Club} | Categoria: {equipo.NombreCategoria})");
                }
            }

            if (!hayEquiposSinCupo)
            {
                Console.WriteLine("Todos los equipos cumplen el cupo minimo.");
            }

            Console.ReadKey();

        }

    }

}
