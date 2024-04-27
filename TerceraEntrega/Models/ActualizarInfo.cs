using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TerceraEntrega.Models
{
    public class ActualizarInfo
    {
        public void CambiarNombre(ListaUsuario usuario, string nombre)
        {
            usuario.Nombre = nombre; 
        }

        public void CambiarApellido(ListaUsuario usuario, string apellido)
        {
            usuario.Apellido = apellido;
        }

        public void CAmbiarPeriodoConsumo(ListaUsuario usuario, int Periodo_consumo)
        {
            usuario.Periodo_consumo = Periodo_consumo;

        }

        public void CambiarEstrato(ListaUsuario usuario, int estrato)
        {
            usuario.Estrato = estrato;

        }

        public void CambiarMetaAhorroEnergia(ListaUsuario usuario, int meta_ahorro_energia)
        {
            usuario.Meta_ahorro_energia = meta_ahorro_energia;
        }

        public void CambiarConsumoEnergia(ListaUsuario usuario, int consumo_actual_energia)
        {
            usuario.Consumo_actual_energia = consumo_actual_energia;
        }

        public void CambiarPromedioAgua(ListaUsuario usuario, int promedio_consumo_agua)
        {
            usuario.Promedio_consumo_agua = promedio_consumo_agua;
        }

        public void CambiarConsumoAgua(ListaUsuario usuario, int consumo_actual_agua)
        {
            usuario.consumo_actual_agua = consumo_actual_agua;
        }



    }
}