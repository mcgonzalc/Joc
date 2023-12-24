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
        private int velocidadSalto = 10; 
        private int alturaSalto = 100;    
        private int sueloY;               
        private bool enSalto = false;     // Variable para verificar si el personaje está en el aire
       

        private void Juego_KeyPress(object sender, KeyPressEventArgs e) // Movimineto si dejas pulsada la tecla
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
            Point movimiento = new Point(x, y); // Creamos el nuevo punto a donde movimos el jugador
            Jugador1.Location = movimiento;
         

        }


        private void Juego_KeyDown(object sender, KeyEventArgs e) // Moviento al pulsar la tecla
        {
          

            if (e.KeyCode == Keys.Space && !enSalto)
            {
                enSalto = true;
                TimerSalto.Start();
            }

        }
       
        private void Temporizador_Tick(object sender, EventArgs e)
        {
            
            Jugador1.Top -= velocidadSalto;
            int x = Jugador1.Location.X;
            if ( Jugador1.Top < sueloY - alturaSalto)
            {
                velocidadSalto = -velocidadSalto;
            }

            if (Jugador1.Location.Y> sueloY)
            {
                TimerSalto.Stop();
                enSalto = false;
                velocidadSalto = Math.Abs(velocidadSalto);
                Jugador1.Location = new Point(x, sueloY);
            }
        }

        private void Juego_Load(object sender, EventArgs e)
        {
            sueloY = 165;  // Posicion Y del juador - altura de la imagen del jugador: 237-72
        }
    }
}
