using System;
using System.Collections.Generic;

namespace FinanceManager.ClientApp.Models
{
    public class ChartData
    {
        public string Label { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public string Color { get; set; } = "#1E88E5"; // Azul default

        /// <summary>
        /// Retorna uma lista de cores padrão para serem usadas em gráficos
        /// </summary>
        /// <returns>Lista com cores em formato hexadecimal</returns>
        public static List<string> GetDefaultColors()
        {
            return new List<string>
            {
                "#1E88E5", // Azul
                "#43A047", // Verde
                "#E53935", // Vermelho
                "#FB8C00", // Laranja
                "#8E24AA", // Roxo
                "#00ACC1", // Ciano
                "#FFD600", // Amarelo
                "#5E35B1", // Roxo escuro
                "#1E88E5", // Azul
                "#43A047", // Verde
                "#E53935", // Vermelho
                "#FB8C00"  // Laranja
            };
        }
    }
}