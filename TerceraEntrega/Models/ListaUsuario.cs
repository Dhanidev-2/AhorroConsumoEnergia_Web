using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TerceraEntrega.Models
{
    public class ListaUsuario
    {
        public int cedula, periodo_consumo, estrato, meta_ahorro_energia, consumo_actual_energia, promedio_consumo_agua, consumo_actual_agua, consumo_gas;
        public string nombre, apellido;


        public ListaUsuario(int cedula, string nombre, string apellido, int periodo_consumo, int estrato, int meta_ahorro_energia, int consumo_actual_energia, int promedio_consumo_agua, int consumo_actual_agua, int consumo_gas)
        {
            this.Cedula = cedula;
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Periodo_consumo = periodo_consumo;
            this.Estrato = estrato;
            this.Meta_ahorro_energia = meta_ahorro_energia;
            this.Consumo_actual_energia = consumo_actual_energia;
            this.Promedio_consumo_agua = promedio_consumo_agua;
            this.Consumo_actual_agua = consumo_actual_agua;
            this.Consumo_gas = consumo_gas;
        }


        public int Cedula { get => cedula; set => cedula = value; }

        public int Periodo_consumo { get => periodo_consumo; set => periodo_consumo = value; }

        public int Estrato { get => estrato; set => estrato = value; }

        public int Meta_ahorro_energia { get => meta_ahorro_energia; set => meta_ahorro_energia = value; }

        public int Consumo_actual_energia { get => consumo_actual_energia; set => consumo_actual_energia = value; }

        public int Promedio_consumo_agua { get => promedio_consumo_agua; set => promedio_consumo_agua = value; }

        public int Consumo_actual_agua { get => consumo_actual_agua; set => consumo_actual_agua = value; }

        public string Nombre { get => nombre; set => nombre = value; }

        public string Apellido { get => apellido; set => apellido = value; }

        public int Consumo_gas { get => consumo_gas; set => consumo_gas = value; }

    }
}