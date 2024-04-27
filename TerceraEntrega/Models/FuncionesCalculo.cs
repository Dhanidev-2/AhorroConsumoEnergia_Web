using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerceraEntrega.Models
{
    public class FuncionesCalculo
    {

        public static int CalcularValorPagarEnergia(List<ListaUsuario> usuarios, int cedula)
        {
            var clienteEncontrado = usuarios.Find(x => x.Cedula == cedula);

            if (clienteEncontrado != null)
            {
                return ValorPagarEnergia(clienteEncontrado.meta_ahorro_energia, clienteEncontrado.consumo_actual_energia);
            }
            else
            {
                // Devolvemos un valor predeterminado en caso de que el cliente no sea encontrado
                return 0;
            }
        }


        public static int ValorPagarEnergia(int metaAhorroEnergia, int consumoActualEnergia)
        {
            int costoKilovatio = 850;
            int valorParcial = consumoActualEnergia * costoKilovatio;
            int valorIncentivo = (metaAhorroEnergia - consumoActualEnergia) * costoKilovatio;
            return valorParcial - valorIncentivo;
        }


        public static int CalcularValorPagarAgua(List<ListaUsuario> usuarios, int cedula)
        {
            var clienteEncontrado = usuarios.Find(x => x.Cedula == cedula);

            if (clienteEncontrado != null)
            {
                return ValorPagarAgua(clienteEncontrado.Promedio_consumo_agua, clienteEncontrado.Consumo_actual_agua);
            }
            else
            {
                // Devolvemos un valor predeterminado en caso de que el cliente no sea encontrado
                return 0;
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




        public static double CalcularPromedioConsumoEnergiaClientes(List<ListaUsuario> usuarios)
        {
            return PromedioConsumoEnergia(usuarios);
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


        public static int CalcularValorPagarFactura(List<ListaUsuario> usuarios, int cedula)
        {
            var clienteEncontrado = usuarios.Find(x => x.Cedula == cedula);

            if (clienteEncontrado != null)
            {
                int valorPagarEnergia = ValorPagarEnergia(clienteEncontrado.meta_ahorro_energia, clienteEncontrado.consumo_actual_energia);
                int valorPagarAgua = ValorPagarAgua(clienteEncontrado.promedio_consumo_agua, clienteEncontrado.consumo_actual_agua);

                return ValorPagarFactura(valorPagarEnergia, valorPagarAgua);
            }
            else
            {
                // Devolvemos un valor predeterminado en caso de que el cliente no sea encontrado
                return 0;
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



        public static Dictionary<int, double> CalcularPorcentajeExcesoAguaPorEstrato(List<ListaUsuario> usuarios)
        {
            Dictionary<int, double> porcentajeExcesoAguaPorEstrato = new Dictionary<int, double>();

            // Inicializar todos los estratos con 0% de exceso de agua
            foreach (var usuario in usuarios)
            {
                int estrato = usuario.estrato;
                if (!porcentajeExcesoAguaPorEstrato.ContainsKey(estrato))
                {
                    porcentajeExcesoAguaPorEstrato[estrato] = 0;
                }
            }

            // Cálculo del porcentaje de exceso de agua por estrato
            foreach (var usuario in usuarios)
            {
                int promedioConsumoAgua = usuario.promedio_consumo_agua;
                int consumoActualAgua = usuario.consumo_actual_agua;

                if (consumoActualAgua > promedioConsumoAgua)
                {
                    int excesoAgua = consumoActualAgua - promedioConsumoAgua;
                    int estrato = usuario.estrato;

                    // Calcular el porcentaje de exceso de agua para este usuario y sumarlo al total del estrato
                    double porcentajeExceso = (excesoAgua / (double)promedioConsumoAgua) * 100;
                    porcentajeExcesoAguaPorEstrato[estrato] += porcentajeExceso;
                }
            }

            return porcentajeExcesoAguaPorEstrato;
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


        public static int CalcularConsumoMayorPromedioAgua(List<ListaUsuario> usuarios)
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




        public static ListaUsuario CalcularMayorDesfaseEnergia(List<ListaUsuario> usuarios)
        {
            ListaUsuario usuarioMayorDesfase = MayorDesfase(usuarios);

            return usuarioMayorDesfase;
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


        public static int CalcularEstratoAhorroMayorCantidadAgua(List<ListaUsuario> usuarios)
        {
            return EstratoAhorroMayorCantidadAgua(usuarios);
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

        public static ListaUsuario CalcularMayorPeriodoConsumoAgua(List<ListaUsuario> usuarios, int periodoConsumo)
        {
            ListaUsuario usuarioMayorConsumo = MayorPeriodoConsumoAgua(usuarios, periodoConsumo);
            return usuarioMayorConsumo;
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
