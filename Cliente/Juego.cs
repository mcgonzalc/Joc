using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliente
{
    public partial class Juego : Form
    {
        public Juego()
        {
            InitializeComponent();
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void Juego_KeyPress(object sender, KeyPressEventArgs e)
        {
            int x = Jugador1.Location.X;
            int y = Jugador1.Location.Y;
            if (e.KeyChar=='D'||e.KeyChar=='d')
            {
                x += 10;
            }
            if (e.KeyChar == 'A' || e.KeyChar == 'a')
            {
                x -= 10;
            }
            if (x <0)
            {
                x = 0;
            }
            if (x > 450)
            {
                x = 450;
            }
            Point movimiento = new Point(x, y);
            Jugador1.Location = movimiento;
         

        }

        private void Juego_KeyDown(object sender, KeyEventArgs e)
        {
            int y = Jugador1.Location.Y;
            int x = Jugador1.Location.X;
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.W)
            {
                y -= 100;
            }
            Point movimiento = new Point(x, y);
            Jugador1.Location = movimiento;

        }
    }
}
