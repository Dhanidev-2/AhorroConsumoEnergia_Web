using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerceraEntrega.Models;

namespace TerceraEntrega
{
    public class FuncionesCalculo
    {
      
        public static void CalcularValorPagarEnergia(List<ListaUsuario> usuario)
        {
            Console.Write("Ingrese el número de cédula del cliente: ");
            int cedula = Convert.ToInt32(Console.ReadLine());

            var clienteEncontrado = usuario.Find(x => x.Cedula == cedula);

            if (clienteEncontrado != null)
            {
                int valorPagarEnergia = ValorPagarEnergia(clienteEncontrado.meta_ahorro_energia, clienteEncontrado.consumo_actual_energia);
                Console.WriteLine($"Valor a pagar por servicios de energía: ${valorPagarEnergia}");
            }
            else
            {
                Console.WriteLine("Cliente no encontrado.");
            }
        }

        public static int ValorPagarEnergia(int metaAhorroEnergia, int consumoActualEnergia)
        {
            int costoKilovatio = 850;
            int valorParcial = consumoActualEnergia * costoKilovatio;
            int valorIncentivo = (metaAhorroEnergia - consumoActualEnergia) * costoKilovatio;
            return valorParcial - valorIncentivo;
        }


        public static void CalcularValorPagarAgua(List<ListaUsuario> usuario)
        {
            Console.Write("Ingrese el número de cédula del cliente: ");
            int cedula = Convert.ToInt32(Console.ReadLine());

            var clienteEncontrado = usuario.Find(x => x.Cedula == cedula);

            if (clienteEncontrado != null)
            {
                int valorPagarAgua = ValorPagarAgua(clienteEncontrado.promedio_consumo_agua, clienteEncontrado.consumo_actual_agua);
                Console.WriteLine($"Valor a pagar por servicios de energía: ${valorPagarAgua}");
            }
            else
            {
                Console.WriteLine("Cliente no encontrado.");
            }
        }

        public static int ValorPagarAgua(int promedioConsumoAgua, int consumoActualAgua)
        {
            int costoMetroCubicoAgua = 4600;
            if (consumoActualAgua <= promedioConsumoAgua)
            {
                return consumoActualAgua * costoMetroCubicoAgua;
            }
            else
            {
                int excesoAgua = consumoActualAgua - promedioConsumoAgua;
                return (promedioConsumoAgua * costoMetroCubicoAgua) + (excesoAgua * 2 * costoMetroCubicoAgua);
            }
        }


        public static void CalcularPromedioConsumoEnergiaClientes(List<ListaUsuario> usuarios)
        {
            double promedioConsumoEnergia = PromedioConsumoEnergia(usuarios);
            Console.WriteLine($"Promedio del consumo actual de energía: {promedioConsumoEnergia}");
        }

        public static double PromedioConsumoEnergia(List<ListaUsuario> usuarios)
        {
            int totalConsumoEnergia = 0;
            int totalClientes = usuarios.Count;

            foreach (var usuario in usuarios)
            {
                totalConsumoEnergia += usuario.consumo_actual_energia;
            }

            return (double)totalConsumoEnergia / totalClientes;
        }


        public static void CalcularValorPagarFactura(List<ListaUsuario> usuario)
        {
            Console.Write("Ingrese el número de cédula del cliente: ");
            int cedula = Convert.ToInt32(Console.ReadLine());

            var clienteEncontrado = usuario.Find(x => x.Cedula == cedula);

            if (clienteEncontrado != null)
            {
                int valorPagarEnergia = ValorPagarEnergia(clienteEncontrado.meta_ahorro_energia, clienteEncontrado.consumo_actual_energia);
                int valorPagarAgua = ValorPagarAgua(clienteEncontrado.promedio_consumo_agua, clienteEncontrado.consumo_actual_agua);

                int totalFacturaServicios = ValorPagarFactura(valorPagarEnergia, valorPagarAgua);

                Console.WriteLine($"Valor a pagar por servicios de energía: ${totalFacturaServicios}");
            }
            else
            {
                Console.WriteLine("Cliente no encontrado.");
            }
        }

        public static int ValorPagarFactura(int valorPagarEnergia, int valorPagarAgua)
        {
            int totalFactura = valorPagarEnergia + valorPagarAgua;

            return totalFactura;
        }


        public static int CalcularDescuentosEnergia(List<ListaUsuario> usuarios)
        {
            int totalDescuentosEnergia = 0;
            bool hayDescuentos = false;

            foreach (var usuario in usuarios)
            {
                int metaAhorroEnergia = usuario.meta_ahorro_energia;
                int consumoActualEnergia = usuario.consumo_actual_energia;

                if (consumoActualEnergia <= metaAhorroEnergia)
                {
                    // Valor a pagar sin descuento
                    int costoKilovatio = 850;
                    int valorParcial = consumoActualEnergia * costoKilovatio;

                    // Valor a pagar con descuento
                    int valorIncentivo = (metaAhorroEnergia - consumoActualEnergia) * costoKilovatio;
                    int valorPagarConDescuento = valorParcial - valorIncentivo;

                    int descuento = valorParcial - valorPagarConDescuento;

                    totalDescuentosEnergia += descuento;
                    hayDescuentos = true;
                }
            }

            if (!hayDescuentos)
            {
                Console.WriteLine("No hay clientes con descuento.");
            }

            return totalDescuentosEnergia;
        }


        public static int CalcularExcesoAgua(List<ListaUsuario> usuarios)
        {
            int totalExcesoAgua = 0;

            foreach (var usuario in usuarios)
            {
                int promedioConsumoAgua = usuario.promedio_consumo_agua;
                int consumoActualAgua = usuario.consumo_actual_agua;

                if (consumoActualAgua > promedioConsumoAgua)
                {
                    // Cantidad de metros cúbicos consumidos por encima del promedio
                    int excesoAgua = consumoActualAgua - promedioConsumoAgua;

                    totalExcesoAgua += excesoAgua;
                }
            }

            return totalExcesoAgua;
        }


        public static void CalcularPorcentajeExcesoAguaPorEstrato(List<ListaUsuario> usuarios)
        {
            Dictionary<int, double> porcentajeExcesoAguaPorEstrato = PorcentajeExcesoAguaPorEstrato(usuarios);

            Console.WriteLine("Porcentaje de consumo excesivo de agua por estrato:");
            foreach (var estrato in porcentajeExcesoAguaPorEstrato.Keys)
            {
                Console.WriteLine($"Estrato {estrato}: {porcentajeExcesoAguaPorEstrato[estrato]}%");
            }
        }

        public static Dictionary<int, double> PorcentajeExcesoAguaPorEstrato(List<ListaUsuario> usuarios)
        {
            Dictionary<int, int> totalExcesoAguaPorEstrato = new Dictionary<int, int>();
            int totalExcesoAgua = 0;

            foreach (var usuario in usuarios)
            {
                int estrato = usuario.estrato;
                int promedioConsumoAgua = usuario.promedio_consumo_agua;
                int consumoActualAgua = usuario.consumo_actual_agua;

                // Verifica si el estrato ya está en los diccionarios, si no lo crea
                if (!totalExcesoAguaPorEstrato.ContainsKey(estrato))
                {
                    totalExcesoAguaPorEstrato[estrato] = 0;
                }

                if (consumoActualAgua > promedioConsumoAgua)
                {
                    // Metros cúbicos consumidos por encima del promedio
                    int excesoAgua = consumoActualAgua - promedioConsumoAgua;

                    totalExcesoAguaPorEstrato[estrato] += excesoAgua;
                    totalExcesoAgua += excesoAgua;
                }
            }

            Dictionary<int, double> porcentajeExcesoAguaPorEstrato = new Dictionary<int, double>();
            foreach (var estrato in totalExcesoAguaPorEstrato.Keys)
            {
                double porcentaje = (double)totalExcesoAguaPorEstrato[estrato] / totalExcesoAgua * 100;
                porcentajeExcesoAguaPorEstrato[estrato] = porcentaje;
            }

            return porcentajeExcesoAguaPorEstrato;
        }


        public static int CalcularConsumoMayorPromedio(List<ListaUsuario> usuarios)
        {
            int contadorClientes = 0;

            foreach (var usuario in usuarios)
            {
                int promedioConsumoAgua = usuario.promedio_consumo_agua;
                int consumoActualAgua = usuario.consumo_actual_agua;

                if (consumoActualAgua > promedioConsumoAgua)
                {
                    contadorClientes++;
                }
            }

            return contadorClientes;
        }


        public static void CalcularMayorDesfase(List<ListaUsuario> usuarios)
        {
            ListaUsuario usuarioMayorDesfase = MayorDesfase(usuarios);

            if (usuarioMayorDesfase != null)
            {
                int desfase = usuarioMayorDesfase.consumo_actual_energia - usuarioMayorDesfase.meta_ahorro_energia;
                Console.WriteLine($"El usuario con mayor desfase es {usuarioMayorDesfase.nombre} con un desfase de {desfase}.");
            }
            else
            {
                Console.WriteLine("No hay usuarios con desfase en el consumo de energía.");
            }
        }

        public static ListaUsuario MayorDesfase(List<ListaUsuario> usuarios)
        {
            ListaUsuario usuarioMayorDesfase = null;
            int mayorDesfase = 0;

            foreach (var usuario in usuarios)
            {
                int metaAhorroEnergia = usuario.meta_ahorro_energia;
                int consumoActualEnergia = usuario.consumo_actual_energia;

                if (consumoActualEnergia > metaAhorroEnergia)
                {
                    int desfase = consumoActualEnergia - metaAhorroEnergia;
                    if (desfase > mayorDesfase)
                    {
                        mayorDesfase = desfase;
                        usuarioMayorDesfase = usuario;
                    }
                }
            }

            return usuarioMayorDesfase;
        }


        public static void CalcularEstratoAhorroMayorCantidadAgua(List<ListaUsuario> usuarios)
        {
            int estratoMayorAhorro = EstratoAhorroMayorCantidadAgua(usuarios);
            Console.WriteLine($"El estrato con mayor cantidad de agua ahorrada es el estrato {estratoMayorAhorro}.");
        }

        public static int EstratoAhorroMayorCantidadAgua(List<ListaUsuario> usuarios)
        {
            Dictionary<int, int> totalAhorroAguaPorEstrato = new Dictionary<int, int>();

            foreach (var usuario in usuarios)
            {
                int estrato = usuario.estrato;
                int promedioConsumoAgua = usuario.promedio_consumo_agua;
                int consumoActualAgua = usuario.consumo_actual_agua;

                if (!totalAhorroAguaPorEstrato.ContainsKey(estrato))
                {
                    totalAhorroAguaPorEstrato[estrato] = 0;
                }

                if (consumoActualAgua < promedioConsumoAgua)
                {
                    int ahorroAgua = promedioConsumoAgua - consumoActualAgua;

                    totalAhorroAguaPorEstrato[estrato] += ahorroAgua;
                }
            }

            // Encuentra el estrato con el mayor ahorro de agua
            var maxAhorro = totalAhorroAguaPorEstrato.Values.Max();
            var estratoMayorAhorro = totalAhorroAguaPorEstrato.First(x => x.Value == maxAhorro).Key;

            return estratoMayorAhorro;
        }


        public static void CalcularEstratoMayorMenorConsumoEnergia(List<ListaUsuario> usuarios)
        {
            int estratoMayorConsumo = EstratoMayorConsumoEnergia(usuarios);
            int estratoMenorConsumo = EstratoMenorConsumoEnergia(usuarios);
            Console.WriteLine($"El estrato con mayor consumo de energía es el estrato {estratoMayorConsumo}.");
            Console.WriteLine($"El estrato con menor consumo de energía es el estrato {estratoMenorConsumo}.");
        }

        public static int EstratoMayorConsumoEnergia(List<ListaUsuario> usuarios)
        {
            Dictionary<int, int> totalConsumoEnergiaPorEstrato = new Dictionary<int, int>();

            foreach (var usuario in usuarios)
            {
                int estrato = usuario.estrato;
                int consumoActualEnergia = usuario.consumo_actual_energia;

                if (!totalConsumoEnergiaPorEstrato.ContainsKey(estrato))
                {
                    totalConsumoEnergiaPorEstrato[estrato] = 0;
                }

                totalConsumoEnergiaPorEstrato[estrato] += consumoActualEnergia;
            }

            // Encuentra el estrato con el mayor consumo de energía
            var maxConsumo = totalConsumoEnergiaPorEstrato.Values.Max();
            var estratoMayorConsumo = totalConsumoEnergiaPorEstrato.First(x => x.Value == maxConsumo).Key;

            return estratoMayorConsumo;
        }

        public static int EstratoMenorConsumoEnergia(List<ListaUsuario> usuarios)
        {
            Dictionary<int, int> totalConsumoEnergiaPorEstrato = new Dictionary<int, int>();

            foreach (var usuario in usuarios)
            {
                int estrato = usuario.estrato;
                int consumoActualEnergia = usuario.consumo_actual_energia;

                if (!totalConsumoEnergiaPorEstrato.ContainsKey(estrato))
                {
                    totalConsumoEnergiaPorEstrato[estrato] = 0;
                }

                totalConsumoEnergiaPorEstrato[estrato] += consumoActualEnergia;
            }

            // Encuentra el estrato con el menor consumo de energía
            var minConsumo = totalConsumoEnergiaPorEstrato.Values.Min();
            var estratoMenorConsumo = totalConsumoEnergiaPorEstrato.First(x => x.Value == minConsumo).Key;

            return estratoMenorConsumo;
        }


        public static void CalcularValorTotalPago(List<ListaUsuario> usuarios)
        {
            int totalFacturaEnergia = TotalFacturasEnergia(usuarios);
            int totalFacturaAgua = TotalFacturasAgua(usuarios);
            Console.WriteLine($"El total pagado por los clientes por concepto de energía es: ${totalFacturaEnergia}");
            Console.WriteLine($"El total pagado por los clientes por concepto de agua es: ${totalFacturaAgua}");
        }

        public static int TotalFacturasEnergia(List<ListaUsuario> usuarios)
        {
            int totalFacturaEnergia = 0;

            foreach (var usuario in usuarios)
            {
                int valorPagarEnergia = ValorPagarEnergia(usuario.meta_ahorro_energia, usuario.consumo_actual_energia);
                totalFacturaEnergia += valorPagarEnergia;
            }

            return totalFacturaEnergia;
        }

        public static int TotalFacturasAgua(List<ListaUsuario> usuarios)
        {
            int totalFacturaAgua = 0;

            foreach (var usuario in usuarios)
            {
                int valorPagarAgua = ValorPagarAgua(usuario.promedio_consumo_agua, usuario.consumo_actual_agua);
                totalFacturaAgua += valorPagarAgua;
            }

            return totalFacturaAgua;
        }

        public static void CalcularMayorPeriodoConsumoAgua(List<ListaUsuario> usuarios, int periodoConsumo)
        {
            ListaUsuario usuarioMayorConsumo = MayorPeriodoConsumoAgua(usuarios, periodoConsumo);

            if (usuarioMayorConsumo != null)
            {
                Console.WriteLine($"Cédula: {usuarioMayorConsumo.cedula}");
                Console.WriteLine($"Nombre: {usuarioMayorConsumo.nombre}");
                Console.WriteLine($"Apellido: {usuarioMayorConsumo.apellido}");
            }
            else
            {
                Console.WriteLine("No se encontró ningún usuario para el periodo de consumo ingresado.");
            }
        }

        public static ListaUsuario MayorPeriodoConsumoAgua(List<ListaUsuario> usuarios, int periodoConsumo)
        {

            ListaUsuario usuarioMayorConsumo = null;
            int mayorConsumo = 0;

            foreach (var usuario in usuarios)
            {
                if (usuario.periodo_consumo == periodoConsumo)
                {
                    if (usuarioMayorConsumo == null || usuario.consumo_actual_agua > mayorConsumo)
                    {
                        usuarioMayorConsumo = usuario;
                        mayorConsumo = usuario.consumo_actual_agua;
                    }
                }
            }
            return usuarioMayorConsumo;
        }

    }
}