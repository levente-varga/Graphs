using System.Windows.Forms;

public class DoubleBufferedPanel : Panel
{
    public DoubleBufferedPanel() : base()
    {
        DoubleBuffered = true;
    }
}
