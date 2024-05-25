//Tarea 1 | Programación II
//Alessandro Arce Chaves

using TareaProgra.Aumentos;

namespace TareaProgra
{
    public class Program
    {
        public static void Main()
        {
            Program program = new Program();

            Console.WriteLine("Tarea 1 - Programación II");
            var opcion = program.InsertarOpcion();
            if (opcion == 1) 
            {
                //Se utiliza el método principal para los aumentos
                AumentosSalariales aumentos = new AumentosSalariales();
                aumentos.RegistrarAumento();
            }
            else if (opcion == 2)
            {
                VentaEntradas ventas = new VentaEntradas();
                ventas.RegistrarVenta();
            }
        }
        public int InsertarOpcion()
        {
            int opcion;
            while (true)
            {
                try
                {
                    Console.WriteLine("-Digite 1 para sistema de aumentos");
                    Console.WriteLine("-Digite 2 para sistema de venta de entradas");
                    opcion = int.Parse(Console.ReadLine());

                    if (0 >= opcion || opcion >= 3)
                    {
                        throw new Exception("Debe ingresar una opción válida"); //Mensaje que tendrá la excepción

                    }
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error: Solo se permite el ingreso de números");
                    Thread.Sleep(2500);
                    Console.WriteLine("");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    Thread.Sleep(2500);
                    Console.WriteLine("");
                }
            }
            return opcion;
        }

    }

}
