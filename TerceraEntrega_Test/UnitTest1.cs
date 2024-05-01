using TerceraEntrega;
using TerceraEntrega.Models;

namespace TerceraEntrega_Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestvalorPagarEnergia()
        {
            int metaAhorroEnergia = 150;
            int consumoActualEnergia = 180;

            var result = FuncionesCalculo.ValorPagarEnergia(metaAhorroEnergia, consumoActualEnergia);

            int valor_esperado = 178500;

            Assert.AreEqual(valor_esperado, result);
        }

        [TestMethod]
        public void TestValorPagarAgua()
        {
            // se usa de ejemplo uno de los valores de las 3 cedulas, en este caso los valores son de la cedula 3145
            int promedioConsumoAgua = 25;
            int consumoActualAgua = 20;

            var result = FuncionesCalculo.ValorPagarAgua(promedioConsumoAgua, consumoActualAgua);

            int valor_esperado = 92000;

            Assert.AreEqual(valor_esperado, result);
        }

        [TestMethod]
        public void TestPromedioConsumoEnergia()
        {
            List<ListaUsuario> usuarios = new List<ListaUsuario>();
            usuarios.Add(new ListaUsuario(3145, "Usuario1", "Apellido1", 1, 3, 150, 180, 25, 20, 2));
            usuarios.Add(new ListaUsuario(8947, "Usuario2", "Apellido2", 2, 3, 190, 187, 25, 30, 3));
            usuarios.Add(new ListaUsuario(9812, "Usuario3", "Apellido3", 3, 4, 260, 320, 25, 25, 2));

            double promedio = (180 + 187 + 320) / 3.0;

            double actualPromedio = FuncionesCalculo.PromedioConsumoEnergia(usuarios);

            Assert.AreEqual(promedio, actualPromedio);
        }

        [TestMethod]
        public void TestvalorPagarFactura()
        {
            int valorPagarEnergia = 178500;
            int valorPagarAgua = 92000;

            var result = FuncionesCalculo.ValorPagarFactura(valorPagarEnergia, valorPagarAgua);
            int valorEsperado = 270500;

            Assert.AreEqual(valorEsperado, result);
        }

        [TestMethod]
        public void TestCalcularDescuentosEnergia()
        {
            List<ListaUsuario> usuarios = new List<ListaUsuario>();
            usuarios.Add(new ListaUsuario(3145, "Usuario1", "Apellido1", 1, 3, 150, 180, 25, 20, 2));
            usuarios.Add(new ListaUsuario(8947, "Usuario2", "Apellido2", 2, 3, 190, 187, 25, 30, 3));
            usuarios.Add(new ListaUsuario(9812, "Usuario3", "Apellido3", 3, 4, 260, 320, 25, 25, 2));

            int costoKilovatio = 850;
            int descuentoUsuario1 = 0;
            int descuentoUsuario2 = (190 - 187) * costoKilovatio;
            int descuentoUsuario3 = 0;

            int totalDescuentosEnergiaEsperado = descuentoUsuario1 + descuentoUsuario2 + descuentoUsuario3;

            int totalDescuentosEnergiaActual = FuncionesCalculo.CalcularDescuentosEnergia(usuarios);

            Assert.AreEqual(totalDescuentosEnergiaEsperado, totalDescuentosEnergiaActual, "El total de descuentos de energía calculado no es el esperado.");
        }

        [TestMethod]
        public void TestCalcularExcesoAgua()
        {
            List<ListaUsuario> usuarios = new List<ListaUsuario>();
            usuarios.Add(new ListaUsuario(3145, "Usuario1", "Apellido1", 1, 3, 150, 180, 25, 20, 2));
            usuarios.Add(new ListaUsuario(8947, "Usuario2", "Apellido2", 2, 3, 190, 187, 25, 30, 3));
            usuarios.Add(new ListaUsuario(9812, "Usuario3", "Apellido3", 3, 4, 260, 320, 25, 25, 2));

            int excesoAguaUsuario1 = usuarios[0].consumo_actual_agua - usuarios[0].promedio_consumo_agua;
            int excesoAguaUsuario2 = usuarios[1].consumo_actual_agua - usuarios[1].promedio_consumo_agua;
            int excesoAguaUsuario3 = usuarios[2].consumo_actual_agua - usuarios[2].promedio_consumo_agua;

            // Verificación de que el exceso de agua sea positivo, si no, se establece en 0
            if (excesoAguaUsuario1 < 0)
                excesoAguaUsuario1 = 0;
            if (excesoAguaUsuario2 < 0)
                excesoAguaUsuario2 = 0;
            if (excesoAguaUsuario3 < 0)
                excesoAguaUsuario3 = 0;

            int totalExcesoAguaEsperado = excesoAguaUsuario1 + excesoAguaUsuario2 + excesoAguaUsuario3;

            int totalExcesoAguaActual = FuncionesCalculo.CalcularExcesoAgua(usuarios);

            Assert.AreEqual(totalExcesoAguaEsperado, totalExcesoAguaActual);
        }

        [TestMethod]
        public void TestCalcularPorcentajeExcesoAguaPorEstrato()
        {
            List<ListaUsuario> usuarios = new List<ListaUsuario>();
            usuarios.Add(new ListaUsuario(3145, "Usuario1", "Apellido1", 1, 3, 150, 180, 25, 20, 2));
            usuarios.Add(new ListaUsuario(8947, "Usuario2", "Apellido2", 2, 3, 190, 187, 25, 30, 3));
            usuarios.Add(new ListaUsuario(9812, "Usuario3", "Apellido3", 3, 4, 260, 320, 25, 25, 2));

            Dictionary<int, double> porcentajeExcesoAguaPorEstrato = FuncionesCalculo.PorcentajeExcesoAguaPorEstrato(usuarios);

            // Assert
            Assert.AreEqual(2, porcentajeExcesoAguaPorEstrato.Count);
            Assert.IsTrue(porcentajeExcesoAguaPorEstrato.ContainsKey(3)); //verifica si el diccionario contiene clave para ese estrato
            Assert.IsTrue(porcentajeExcesoAguaPorEstrato.ContainsKey(4));
            Assert.AreEqual(100.0, porcentajeExcesoAguaPorEstrato[3], 0.01);
            Assert.AreEqual(0.0, porcentajeExcesoAguaPorEstrato[4], 0.01);
        }

        [TestMethod]
        public void TestCalcularConsumoMayorPromedio()
        {
            List<ListaUsuario> usuarios = new List<ListaUsuario>();
            usuarios.Add(new ListaUsuario(3145, "Usuario1", "Apellido1", 1, 3, 150, 180, 25, 20, 2));
            usuarios.Add(new ListaUsuario(8947, "Usuario2", "Apellido2", 2, 3, 190, 187, 25, 30, 3));
            usuarios.Add(new ListaUsuario(9812, "Usuario3", "Apellido3", 3, 4, 260, 320, 25, 25, 2));

            int contadorClientesEsperado = 1;

            int contadorClientesActual = FuncionesCalculo.CalcularConsumoMayorPromedioAgua(usuarios);

            Assert.AreEqual(contadorClientesEsperado, contadorClientesActual);
        }

        [TestMethod]
        public void TestCalcularMayorDesfase()
        {
            List<ListaUsuario> usuarios = new List<ListaUsuario>();
            usuarios.Add(new ListaUsuario(3145, "Usuario1", "Apellido1", 1, 3, 150, 180, 25, 20, 2));
            usuarios.Add(new ListaUsuario(8947, "Usuario2", "Apellido2", 2, 3, 190, 187, 25, 30, 3));
            usuarios.Add(new ListaUsuario(9812, "Usuario3", "Apellido3", 3, 4, 260, 320, 25, 25, 2));

            ListaUsuario usuarioMayorDesfase = FuncionesCalculo.MayorDesfase(usuarios);

            Assert.IsNotNull(usuarioMayorDesfase);
            Assert.AreEqual("Usuario3", usuarioMayorDesfase.nombre);
        }

        [TestMethod]
        public void TestCalcularEstratoAhorroMayorCantidadAgua()
        {
            List<ListaUsuario> usuarios = new List<ListaUsuario>();
            usuarios.Add(new ListaUsuario(3145, "Usuario1", "Apellido1", 1, 3, 150, 180, 25, 20, 2));
            usuarios.Add(new ListaUsuario(8947, "Usuario2", "Apellido2", 2, 3, 190, 187, 25, 30, 3));
            usuarios.Add(new ListaUsuario(9812, "Usuario3", "Apellido3", 3, 4, 260, 320, 25, 25, 2));

            int estratoMayorAhorro = FuncionesCalculo.EstratoAhorroMayorCantidadAgua(usuarios);

            int estratoEsperado = 3;

            Assert.AreEqual(estratoEsperado, estratoMayorAhorro);
        }

        [TestMethod]
        public void TestEstratoMayorConsumoEnergia()
        {
            List<ListaUsuario> usuarios = new List<ListaUsuario>();
            usuarios.Add(new ListaUsuario(3145, "Usuario1", "Apellido1", 1, 3, 150, 180, 25, 20, 2));
            usuarios.Add(new ListaUsuario(8947, "Usuario2", "Apellido2", 2, 3, 190, 187, 25, 30, 3));
            usuarios.Add(new ListaUsuario(9812, "Usuario3", "Apellido3", 3, 4, 260, 320, 25, 25, 2));

            int estratoMayorConsumo = FuncionesCalculo.EstratoMayorConsumoEnergia(usuarios);

            int estratoEsperado = 3;

            Assert.AreEqual(estratoEsperado, estratoMayorConsumo);
        }

        [TestMethod]
        public void TestEstratoMenorConsumoEnergia()
        {
            List<ListaUsuario> usuarios = new List<ListaUsuario>();
            usuarios.Add(new ListaUsuario(3145, "Usuario1", "Apellido1", 1, 3, 150, 180, 25, 20, 2));
            usuarios.Add(new ListaUsuario(8947, "Usuario2", "Apellido2", 2, 3, 190, 187, 25, 30, 3));
            usuarios.Add(new ListaUsuario(9812, "Usuario3", "Apellido3", 3, 4, 260, 320, 25, 25, 2));

            int estratoMenorConsumo = FuncionesCalculo.EstratoMenorConsumoEnergia(usuarios);

            int estratoEsperado = 4;

            Assert.AreEqual(estratoEsperado, estratoMenorConsumo);
        }

        [TestMethod]
        public void TestTotalFacturasEnergia()
        {
            List<ListaUsuario> usuarios = new List<ListaUsuario>();
            usuarios.Add(new ListaUsuario(3145, "Usuario1", "Apellido1", 1, 3, 150, 180, 25, 20, 2));
            usuarios.Add(new ListaUsuario(8947, "Usuario2", "Apellido2", 2, 3, 190, 187, 25, 30, 3));
            usuarios.Add(new ListaUsuario(9812, "Usuario3", "Apellido3", 3, 4, 260, 320, 25, 25, 2));

            int totalFacturaEnergia = FuncionesCalculo.TotalFacturasEnergia(usuarios);

            int totalEsperado = FuncionesCalculo.ValorPagarEnergia(150, 180) + FuncionesCalculo.ValorPagarEnergia(190, 187) + FuncionesCalculo.ValorPagarEnergia(260, 320);

            Assert.AreEqual(totalEsperado, totalFacturaEnergia);
        }

        [TestMethod]
        public void TestTotalFacturasAgua()
        {
            List<ListaUsuario> usuarios = new List<ListaUsuario>();
            usuarios.Add(new ListaUsuario(3145, "Usuario1", "Apellido1", 1, 3, 150, 180, 25, 20, 2));
            usuarios.Add(new ListaUsuario(8947, "Usuario2", "Apellido2", 2, 3, 190, 187, 25, 30, 3));
            usuarios.Add(new ListaUsuario(9812, "Usuario3", "Apellido3", 3, 4, 260, 320, 25, 25, 2));

            int totalFacturaAgua = FuncionesCalculo.TotalFacturasAgua(usuarios);

            int totalEsperado = FuncionesCalculo.ValorPagarAgua(25, 20) + FuncionesCalculo.ValorPagarAgua(25, 30) + FuncionesCalculo.ValorPagarAgua(25, 25);

            Assert.AreEqual(totalEsperado, totalFacturaAgua);
        }

        [TestMethod]
        public void TestMayorPeriodoConsumoAgua()
        {
            List<ListaUsuario> usuarios = new List<ListaUsuario>();
            usuarios.Add(new ListaUsuario(3145, "Usuario1", "Apellido1", 1, 3, 150, 180, 25, 20, 2));
            usuarios.Add(new ListaUsuario(8947, "Usuario2", "Apellido2", 2, 3, 190, 187, 25, 30, 3));
            usuarios.Add(new ListaUsuario(9812, "Usuario3", "Apellido3", 3, 4, 260, 320, 25, 25, 2));

            ListaUsuario resultado = FuncionesCalculo.MayorPeriodoConsumoAgua(usuarios, 3);

            Assert.IsNotNull(resultado);
            Assert.AreEqual(9812, resultado.cedula);
            Assert.AreEqual("Usuario3", resultado.nombre);
            Assert.AreEqual("Apellido3", resultado.apellido);
        }

        [TestMethod]
        public void TestConsumoGas()
        {
            int consumoGas = 100;

            int valorTotal = FuncionesCalculo.ConsumoGas(consumoGas);

            Assert.AreEqual(254300, valorTotal);
        }

        [TestMethod]
        public void TestCalcularUsuarioMayorPorPeriodoConsumo()
        {
            List<ListaUsuario> usuarios = new List<ListaUsuario>();
            usuarios.Add(new ListaUsuario(3145, "Usuario1", "Apellido1", 1, 3, 150, 180, 25, 20, 2));
            usuarios.Add(new ListaUsuario(8947, "Usuario2", "Apellido2", 1, 3, 190, 187, 25, 30, 3));
            usuarios.Add(new ListaUsuario(9812, "Usuario3", "Apellido3", 2, 4, 260, 320, 25, 25, 2));

            Dictionary<int, ListaUsuario> resultado = FuncionesCalculo.CalcularUsuarioMayorPorPeriodoConsumo(usuarios);

            Assert.IsNotNull(resultado);

            Assert.IsTrue(resultado.ContainsKey(1)); 
            Assert.AreEqual(8947, resultado[1].cedula); 
            Assert.IsTrue(resultado.ContainsKey(2)); 
            Assert.AreEqual(9812, resultado[2].cedula);
        }



    }
}