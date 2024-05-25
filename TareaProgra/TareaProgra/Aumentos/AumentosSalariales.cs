using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TareaProgra.Aumentos
{
    public class AumentosSalariales
    {
        public void RegistrarAumento()
        {
            //Lista para ir guardando todos los registros.
            List<Empleado> lista = new List<Empleado>();

            while (true)
            {
                Empleado empleado = new();
                Console.Clear();
                Console.WriteLine("******************************************************");
                Console.WriteLine("**           SISTEMA DE AUMENTOS SALARIALES         **");
                Console.WriteLine("******************************************************");
                empleado.Nombre = InsertarNombre();
                empleado.Cedula = InsertarCedula();
                empleado.TipoEmpleado = InsertarTipo();
                empleado.PrecioHora = InsertarPrecioHora();
                empleado.HorasLaboradas = InsertarHoras();

                empleado.SalarioOrdinario = empleado.PrecioHora * empleado.HorasLaboradas;

                if (empleado.TipoEmpleado == 1)
                {
                    empleado.Aumento = 0.15m;
                }
                else if (empleado.TipoEmpleado == 2)
                {
                    empleado.Aumento = 0.1m;
                }
                else if (empleado.TipoEmpleado == 3)
                {
                    empleado.Aumento = 0.05m;
                }

                var montoAumento = empleado.SalarioOrdinario * empleado.Aumento;
                empleado.SalarioBruto = Math.Round(empleado.SalarioOrdinario + montoAumento, 2);

                empleado.CCSS = Math.Round(empleado.SalarioBruto * ((decimal)9.17 / 100), 2);
                empleado.SalarioNeto = Math.Round(empleado.SalarioBruto - empleado.CCSS, 2);

                ImprimirResumen(empleado);

                lista.Add(empleado);
                //Consulta si se desea contunuar ingresando aumentos
                Console.WriteLine("------------------------------------------------------");
                Console.WriteLine("¿Desea registrar otro aumento? (S/N): ");
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
                    Console.WriteLine("-Ingrese el nombre del empleado:");
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
                    Console.WriteLine("-Ingrese el número de cédula del empleado: ");
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

        public int InsertarTipo()
        {
            int tipo;
            while (true)
            {
                try
                {
                    Console.WriteLine("-Ingrese el número que corresponda al tipo de empleado:");
                    Console.WriteLine("1- Operario");
                    Console.WriteLine("2- Técnico");
                    Console.WriteLine("3- Profesional");
                    tipo = int.Parse(Console.ReadLine());

                    if (0 >= tipo || tipo >= 4)
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
            return tipo;
        } 

        public decimal InsertarPrecioHora()
        {
            /* En la línea 107 se define que el formato decimal que se utilizará es el del idioma inglés 
             * ya que según el idioma de la máquina en que se ejecute puede esperar como separador decimal 
             * coma o punto y si no se le manda el que es puede generar errores.*/

            decimal salario;
            while (true)
            {
                try
                {
                    Console.WriteLine("-Ingrese pago por hora del empleado: ");
                    //Se cambian los puntos por comas
                    var valor = Console.ReadLine().Replace(",", ".");

                    var culturaEnUs = new CultureInfo("en-US");
                    salario = decimal.Parse(valor);

                    // se verifica si el salario está dentro del rango válido
                    if (salario < 0)
                    {
                        throw new Exception("El monto debe ser mayor a 0"); //Mensaje que tendrá la excepción
                    }

                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error: Debe ingresar un número (se aceptan decimales)."); //Excepción en caso de no ser float
                    Thread.Sleep(2500);
                    Console.WriteLine("");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message); //Excepción en caso de no ser mayor a 0
                    Thread.Sleep(2500);
                    Console.WriteLine("");
                }
            }

            return (decimal)Math.Round(salario, 2); //Se usa para redondear el número a dos decimales
        }

        public int InsertarHoras()
        {
            int horas;
            while (true)
            {
                try
                {
                    Console.WriteLine("-Ingrese el número de horas laboradas: ");
                    horas = int.Parse(Console.ReadLine());
                    break;
                }
                catch
                {
                    Console.WriteLine("Error: Solo se permite el ingreso de números");
                    Thread.Sleep(2500); 
                    Console.WriteLine("");
                }

            }
            return horas;
        }

        public void ImprimirResumen(Empleado emp)
        {
            #region Cambio de numero tipo empleado a nombre

            string tipoEmpleado = "";
            if (emp.TipoEmpleado == 1)
            {
                tipoEmpleado = "Operario";
            }
            else if (emp.TipoEmpleado == 2)
            {
                tipoEmpleado = "Técnico";
            }
            else if (emp.TipoEmpleado == 3)
            {
                tipoEmpleado = "Profesional";
            }

            #endregion
            var montoAumento = emp.Aumento * emp.SalarioOrdinario;

            Console.Clear();
            Console.WriteLine("******************************************************");
            Console.WriteLine("**            DATOS DEL EMPLEADO INGRESADO          **");
            Console.WriteLine("******************************************************");
            Console.WriteLine("Cédula: {0}", emp.Cedula);
            Console.WriteLine("Nombre: {0}", emp.Nombre);
            Console.WriteLine("Tipo empleado: {0}", tipoEmpleado);
            Console.WriteLine("Salario por hora: {0}", emp.PrecioHora);
            Console.WriteLine("Cantidad de horas: {0}", emp.HorasLaboradas);
            Console.WriteLine("Salario Ordinario: {0}", emp.SalarioOrdinario);
            Console.WriteLine("Aumento ({0}%): {1}", emp.Aumento, Math.Round(montoAumento, 2));
            Console.WriteLine("Salario Bruto: {0}", emp.SalarioBruto);
            Console.WriteLine("Deducción CCSS (9.17%): {1}", emp.Aumento, emp.CCSS);
            Console.WriteLine("Salario Neto: {0}", emp.SalarioNeto);

        }

        public void ImprimirEstadisticas(List<Empleado> empleados)
        {

            #region Operarios
            //Se realizan los cálculos para este tipo
            decimal OpCount = 0;
            decimal OpPromedio = 0;
            decimal OpAcumulado = 0;
            var operarios = empleados.Where(e => e.TipoEmpleado == 1).ToList();

            if (operarios.Count > 0)
            {
                OpCount = operarios.Count;
                OpAcumulado = operarios.Sum(o => o.SalarioNeto);
                OpPromedio = OpAcumulado / OpCount;
            }

            #endregion

            #region Tecnicos
            //Se realizan los cálculos para este tipo
            decimal TecCount = 0;
            decimal TecPromedio = 0;
            decimal TecAcumulado = 0;
            var tecnicos = empleados.Where(e => e.TipoEmpleado == 2).ToList();

            if (tecnicos.Count > 0)
            {
                TecCount = tecnicos.Count;
                TecAcumulado = tecnicos.Sum(o => o.SalarioNeto);
                TecPromedio = TecAcumulado / TecCount;
            }

            #endregion

            #region Profesionales
            //Se realizan los cálculos para este tipo
            decimal ProCount = 0;
            decimal ProPromedio = 0;
            decimal ProAcumulado = 0;
            var profesionales = empleados.Where(e => e.TipoEmpleado == 3).ToList();

            if (profesionales.Count > 0)
            {
                ProCount = profesionales.Count;
                ProAcumulado = profesionales.Sum(o => o.SalarioNeto);
                ProPromedio = ProAcumulado / ProCount;
            }

            #endregion

            Console.Clear();
            Console.WriteLine("******************************************************");
            Console.WriteLine("**               ESTADÍSTICAS GENERALES             **");
            Console.WriteLine("******************************************************");
            Console.WriteLine("Operarios:");
            Console.WriteLine("  -Cantidad: {0}", OpCount);
            Console.WriteLine("  -Salario Neto Promedio: {0}", Math.Round(OpPromedio, 2));
            Console.WriteLine("  -Acumulado de Salarios: {0}", OpAcumulado);
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("Operarios:");
            Console.WriteLine("  -Cantidad: {0}", TecCount);
            Console.WriteLine("  -Salario Neto Promedio: {0}", Math.Round(TecPromedio, 2));
            Console.WriteLine("  -Acumulado de Salarios: {0}", TecAcumulado);
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("Operarios:");
            Console.WriteLine("  -Cantidad: {0}", ProCount);
            Console.WriteLine("  -Salario Neto Promedio: {0}", Math.Round(ProPromedio, 2));
            Console.WriteLine("  -Acumulado de Salarios: {0}", ProAcumulado);
            Console.WriteLine("******************************************************");
            Console.WriteLine("        - Presione cualquier tecla para salir -       ");
            Console.ReadKey();
        }
        #endregion

    }
}