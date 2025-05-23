namespace MudBlazor
{
    public class MudColor
    {
        public MudColor(string hexColor)
        {
            HexColor = hexColor;
        }

        public string HexColor { get; private set; }
        
        // Propriedade Value para compatibilidade com o MudBlazor.Utilities.MudColor
        public string Value => HexColor;

        public static implicit operator string(MudColor color) => color.HexColor;
        public static implicit operator MudColor(string color) => new MudColor(color);

        public override string ToString() => HexColor;
    }
}