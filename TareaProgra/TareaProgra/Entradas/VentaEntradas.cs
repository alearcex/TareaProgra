using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TareaProgra.Aumentos
{
    public class VentaEntradas
    {
        public void RegistrarVenta()
        {
            //Lista para ir guardando todos los registros.
            List<Venta> lista = new List<Venta>();
            int numFactura = 1;
            while (true)
            {
                Venta venta = new();
                Console.Clear();
                Console.WriteLine("******************************************************");
                Console.WriteLine("**            SISTEMA DE VENTA DE ENTRADAS          **");
                Console.WriteLine("******************************************************");
                venta.NumeroFactura = numFactura;
                venta.Nombre = InsertarNombre();
                venta.Cedula = InsertarCedula();
                venta.Localidad = InsertarLocalidad();
                venta.Cantidad = InsertarCantidad();

                int precio = 0;
                if (venta.Localidad == 1)
                {
                    precio = 10500;
                }
                else if (venta.Localidad == 2)
                {
                    precio = 20500;
                }
                else if (venta.Localidad == 3)
                {
                    precio = 25500;
                }

                venta.Cargos = venta.Cantidad * 1000;
                venta.Subtotal = venta.Cantidad * precio;
                venta.Total = venta.Subtotal + venta.Cargos;

                ImprimirResumen(venta);

                numFactura++;
                lista.Add(venta);
                //Consulta si se desea contunuar ingresando aumentos
                Console.WriteLine("------------------------------------------------------");
                Console.WriteLine("¿Desea registrar otra venta? (S/N): ");
                string continuar = Console.ReadLine();
                if (continuar.ToUpper() != "S")
                {
                    break;
                }
            }

            ImprimirEstadisticas(lista);
        }

        #region Métodos
        public string InsertarNombre()
        {
            string nombre;
            while (true)
            {
                try
                {
                    Console.WriteLine("-Ingrese el nombre del comprador:");
                    nombre = Console.ReadLine();


                    // Validar que no sea nulo o solo espacios
                    if (string.IsNullOrWhiteSpace(nombre))
                    {
                        throw new Exception("El nombre no puede estar vacío ni consistir solo de espacios.");
                    }


                    // Validar que el nombre solo contenga letras o los espacios de en medio
                    foreach (char caracter in nombre)
                    {
                        if (!char.IsLetter(caracter) && caracter != ' ' && caracter != 'ñ' && caracter != 'Ñ' /*&& !char.IsWhiteSpace(caracter)*/)
                        {
                            throw new Exception("El nombre solo debe contener letras.");
                        }
                    }

                    break; // Salir del bucle si no hay excepciones
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    Thread.Sleep(2500);
                    Console.WriteLine("");
                }
            }
            return nombre;
        }

        public int InsertarCedula()
        {
            int cedula;
            while (true)
            {
                try
                {
                    Console.WriteLine("-Ingrese el número de cédula del comprador: ");
                    cedula = int.Parse(Console.ReadLine());
                    break;
                }
                catch
                {
                    Console.WriteLine("Error: Solo se permite el ingreso de números");
                    Thread.Sleep(2500);
                    Console.WriteLine("");
                }

            }
            return cedula;
        }

        public int InsertarLocalidad()
        {
            int localidad;
            while (true)
            {
                try
                {
                    Console.WriteLine("-Ingrese el número que corresponda a el  :");
                    Console.WriteLine("1- Sol Sur / Sol Norte");
                    Console.WriteLine("2- Sombra Este / Sombra Oeste");
                    Console.WriteLine("3- Preferencial");
                    localidad = int.Parse(Console.ReadLine());

                    if (0 >= localidad || localidad >= 4)
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
            return localidad;
        } 

        public int InsertarCantidad()
        {
            int cantidad;
            while (true)
            {
                try
                {
                    Console.WriteLine("-Ingrese la cantidad de entradas que desea comprar:");
                    cantidad = int.Parse(Console.ReadLine());

                    if (0 >= cantidad)
                    {
                        throw new Exception("El mínimo de compra es una entrada"); //Mensaje que tendrá la excepción

                    }
                    else if (cantidad >= 5)
                    {
                        throw new Exception("El máximo de compra son 4 entradas por cliente"); //Mensaje que tendrá la excepción
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
            return cantidad;
        } 

        public void ImprimirResumen(Venta venta)
        {
            #region Cambio de número localidad a nombre

            string localidad = "";
            if (venta.Localidad == 1)
            {
                localidad = "Sol Sur / Sol Norte";
            }
            else if (venta.Localidad == 2)
            {
                localidad = "Sombra Este / Sombra Oeste";
            }
            else if (venta.Localidad == 3)
            {
                localidad = "Preferencial";
            }

            #endregion

            Console.Clear();
            Console.WriteLine("******************************************************");
            Console.WriteLine("**            FACTURA DE VENTA DE ENTRADAS          **");
            Console.WriteLine("******************************************************");
            Console.WriteLine("Número de factura: {0}", venta.NumeroFactura);
            Console.WriteLine("Nombre del comprador: {0}", venta.Nombre);
            Console.WriteLine("Cédula: {0}", venta.Cedula);
            Console.WriteLine("Localidad: {0}", localidad);
            Console.WriteLine("Cantidad: {0}", venta.Cantidad);
            Console.WriteLine("Subtotal: {0}", venta.Subtotal);
            Console.WriteLine("Cargos por servicio: {0}", venta.Cargos);
            Console.WriteLine("Total: {0}", venta.Total);
        }

        public void ImprimirEstadisticas(List<Venta> ventas)
        {

            #region Sol
            //Se realizan los cálculos para este tipo
            int solCount = 0;
            int solAcumulado = 0;
            var sol = ventas.Where(s => s.Localidad == 1).ToList();

            if (sol.Count > 0)
            {
                solCount = sol.Sum(s => s.Cantidad);
                solAcumulado = sol.Sum(s => s.Subtotal);
            }

            #endregion

            #region Sombra
            //Se realizan los cálculos para este tipo
            int somCount = 0;
            int somAcumulado = 0;
            var sombra = ventas.Where(s => s.Localidad == 2).ToList();

            if (sombra.Count > 0)
            {
                somCount = sombra.Sum(s => s.Cantidad);
                somAcumulado = sombra.Sum(s => s.Subtotal);
            }

            #endregion

            #region Preferenciales
            //Se realizan los cálculos para este tipo
            int preCount = 0;
            int preAcumulado = 0;
            var preferenciales = ventas.Where(p => p.Localidad == 3).ToList();

            if (preferenciales.Count > 0)
            {
                preCount = preferenciales.Sum(p => p.Cantidad);
                preAcumulado = preferenciales.Sum(p => p.Subtotal);
            }

            #endregion

            Console.Clear();
            Console.WriteLine("******************************************************");
            Console.WriteLine("**               ESTADÍSTICAS DE VENTAS             **");
            Console.WriteLine("******************************************************");
            Console.WriteLine("Sol Sur / Sol Norte:");
            Console.WriteLine("  -Cantidad: {0}", solCount);
            Console.WriteLine("  -Acumulado de ventas: {0}", solAcumulado);
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("Sombra Este / Sombra Oeste:");
            Console.WriteLine("  -Cantidad: {0}", somCount);
            Console.WriteLine("  -Acumulado de ventas: {0}", somAcumulado);
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("Preferecial:");
            Console.WriteLine("  -Cantidad: {0}", preCount);
            Console.WriteLine("  -Acumulado de veentas: {0}", preAcumulado);
            Console.WriteLine("******************************************************");
            Console.WriteLine("        - Presione cualquier tecla para salir -       ");
            Console.ReadKey();
        }
        #endregion

    }
}