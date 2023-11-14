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

        Timer t1 = new Timer();
        int velocidad = 20;
        Timer t2 = new Timer();
        private void Juego_KeyDown(object sender, KeyEventArgs e)
        {
            t1.Interval = 20;
            t1.Tick += new EventHandler(Temporizador_Tick);
            t2.Interval = 20;
            t2.Tick += new EventHandler(Temporizador_Tick);

            if (e.KeyCode == Keys.W)
            {
                Jugador1.Top += velocidad;
                t1.Start();
            }
            if (e.KeyCode == Keys.Up)
            {
                Jugador2.Top += velocidad;
                t2.Start();
            }

        }

        private void Temporizador_Tick(object sender, EventArgs e)
        {
            if (Jugador1.Top + Jugador1.Height < this.Height)
            {
                Jugador1.Top += 5;
            }
            else 
            {
                t1.Stop();
            }
            if (Jugador2.Top + Jugador2.Height < this.Height)
            {
                Jugador2.Top += 5;
            }
            else
                t2.Stop();
        }
    }
}
