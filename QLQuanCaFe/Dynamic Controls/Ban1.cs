using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Design;
using System.Drawing;
namespace Dynamic_Controls
{
    public class Ban1 : Button
    {
       public Button CreateButton(string name, string text, int x, int y, EventHandler clickEvent)
        {
            Button button = new Button();
            button.Name = name;
            button.Text = text;
            button.Location = new System.Drawing.Point(x, y);
            button.Size = new Size(50, 50);
            button.Click += clickEvent;
            return button;
        }
    }
}
