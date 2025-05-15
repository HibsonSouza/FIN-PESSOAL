namespace FinanceManager.ClientApp.Models
{
    public class ChartData
    {
        public string Title { get; set; } = string.Empty;
        
        public string Description { get; set; } = string.Empty;
        
        public ChartType Type { get; set; } = ChartType.Bar;
        
        public List<string> Labels { get; set; } = new();
        
        public List<ChartDataset> Datasets { get; set; } = new();
        
        public ChartOptions Options { get; set; } = new();
        
        public static ChartData CreateEmptyChart(string title = "", ChartType type = ChartType.Bar)
        {
            return new ChartData 
            { 
                Title = title,
                Type = type, 
                Datasets = new List<ChartDataset>
                {
                    new ChartDataset
                    {
                        Label = "Sem dados",
                        Data = new List<decimal> { 0 },
                        BackgroundColor = new List<string> { "#e0e0e0" }
                    }
                },
                Labels = new List<string> { "Sem dados" }
            };
        }
    }

    public enum ChartType
    {
        Bar,
        Line,
        Pie,
        Doughnut,
        Radar,
        PolarArea,
        Bubble,
        Scatter
    }

    public class ChartDataset
    {
        public string Label { get; set; } = string.Empty;
        
        public List<decimal> Data { get; set; } = new();
        
        public List<string> BackgroundColor { get; set; } = new();
        
        public List<string> BorderColor { get; set; } = new();
        
        public decimal BorderWidth { get; set; } = 1;
        
        public bool Fill { get; set; } = true;
        
        public string TensionLineBorder { get; set; } = "0.1";
        
        public Dictionary<string, object> AdditionalConfigs { get; set; } = new();
    }

    public class ChartOptions
    {
        public bool Responsive { get; set; } = true;
        
        public bool MaintainAspectRatio { get; set; } = false;
        
        public ChartLegend Legend { get; set; } = new();
        
        public ChartTitle Title { get; set; } = new();
        
        public ChartTooltip Tooltip { get; set; } = new();
        
        public Dictionary<string, object> Scales { get; set; } = new();
    }

    public class ChartLegend
    {
        public bool Display { get; set; } = true;
        
        public string Position { get; set; } = "top";
    }

    public class ChartTitle
    {
        public bool Display { get; set; } = true;
        
        public string Text { get; set; } = string.Empty;
        
        public int FontSize { get; set; } = 16;
    }

    public class ChartTooltip
    {
        public bool Enabled { get; set; } = true;
        
        public string Mode { get; set; } = "index";
        
        public bool Intersect { get; set; } = false;
        
        public Dictionary<string, object> Callbacks { get; set; } = new();
    }
    
    public static class ChartColors
    {
        public static readonly List<string> DefaultColors = new()
        {
            "#4CAF50", // Verde
            "#2196F3", // Azul
            "#F44336", // Vermelho
            "#FF9800", // Laranja
            "#9C27B0", // Roxo
            "#00BCD4", // Ciano
            "#FFEB3B", // Amarelo
            "#795548", // Marrom
            "#607D8B", // Azul Acinzentado
            "#E91E63", // Rosa
            "#3F51B5", // Índigo
            "#009688", // Verde-azulado
            "#FFC107", // Âmbar
            "#673AB7", // Roxo Profundo
            "#03A9F4", // Azul Claro
            "#8BC34A", // Verde Claro
            "#CDDC39", // Lima
            "#9E9E9E", // Cinza
            "#FF5722", // Laranja Profundo
            "#374046"  // Azul Escuro
        };
        
        public static List<string> GetRandomColors(int count)
        {
            var result = new List<string>();
            var random = new Random();
            
            for (int i = 0; i < count; i++)
            {
                if (i < DefaultColors.Count)
                {
                    result.Add(DefaultColors[i]);
                }
                else
                {
                    // Gera uma cor aleatória em formato hexadecimal
                    var color = string.Format("#{0:X6}", random.Next(0x1000000));
                    result.Add(color);
                }
            }
            
            return result;
        }
    }
}