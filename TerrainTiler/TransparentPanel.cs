using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TerrainTiler
{
    class TransparentPanel: Panel
    {
        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }
    }
}
