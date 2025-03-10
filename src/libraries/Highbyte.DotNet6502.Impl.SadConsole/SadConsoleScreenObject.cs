using Highbyte.DotNet6502.Systems;
using Console = SadConsole.Console;

namespace Highbyte.DotNet6502.Impl.SadConsole;

public class SadConsoleScreenObject : ScreenObject
{
    public Console ScreenConsole { get; }

    public SadConsoleScreenObject(ITextMode textMode, IScreen screen, SadConsoleConfig sadConsoleConfig)
    {
        ScreenConsole = CreateScreenConsole(textMode, screen, sadConsoleConfig);
    }

    private Console CreateScreenConsole(ITextMode textMode, IScreen screen, SadConsoleConfig sadConsoleConfig)
    {
        // Setup console screen
        // int totalCols = (textMode.TextCols + (textMode.BorderCols * 2));
        // int totalRows = (textMode.TextRows + (textMode.BorderRows * 2));
        int totalCols = textMode.TextCols;
        int totalRows = textMode.TextRows;
        if(screen.HasBorder)
        {
            totalCols += (screen.VisibleLeftRightBorderWidth / textMode.CharacterWidth) * 2;
            totalRows += (screen.VisibleTopBottomBorderHeight / textMode.CharacterHeight) * 2;
        }

        var console = new Console(totalCols, totalRows)
        {
            DefaultForeground = Color.White,
            DefaultBackground = Color.Black
        };
        //screen.Position = new Point(VisibleLeftRightBorderWidth, VisibleTopBottomBorderHeight);

        // TODO: Better way to map numeric scale value to SadConsole.Font.FontSizes enum?
        var fontSize = sadConsoleConfig.FontScale switch
        {
            1 => IFont.Sizes.One,
            2 => IFont.Sizes.Two,
            3 => IFont.Sizes.Three,
            _ => IFont.Sizes.One,
        };
        console.FontSize = console.Font.GetFontSize(fontSize);

        console.Clear();
        console.Cursor.IsEnabled = false;
        console.Cursor.IsVisible = false;

        console.Parent = this;
        return console;
    }
    public void DrawCharacter(int x, int y, int sadConsoleCharCode, Color fgColor, Color bgColor)
    {
        ScreenConsole.SetGlyph(x, y, sadConsoleCharCode, fgColor, bgColor);
    }
}
