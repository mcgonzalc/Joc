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

            Jugador1.Enabled = true;
        }
        
        private int velocidadSalto = 10;
        private int velocidadReboteX = 5;
        private int velocidadReboteY = 5;
        private int velocidadX = 5;
        private int gravedad = 3;
        private double friccion = 0.95;
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
            if (x <66)
            {
                x = 66;
            }
            if (x > 975-76)
            {
                x = 975-76;
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
       
        private void Temporizador_Tick(object sender, EventArgs e) //salto
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
            sueloY = 280;  // Posicion Y del juador - altura de la imagen del jugador: 352-72
            MoverPelota(); // Mueve la pelota           
            VerificarColisiones(); // Verifica colisiones y realiza el rebote si es necesario
            ColisionConJugador1(pelota, Jugador1);
            ColisionConJugador2(pelota, Jugador2);
            
        }
        
        private bool Gol( PictureBox pb1, PictureBox pb2, PictureBox pb3) // Checkea si hay colision entre la porteria y la pelota
        {
            Rectangle rec1 = new Rectangle(pb1.Location , pb1.Size);
            Rectangle rec2 = new Rectangle(pb2.Location, pb2.Size);
            Rectangle rec3 = new Rectangle(pb3.Location, pb3.Size);
            bool HayGol = rec1.IntersectsWith(rec2); 
            bool HayGol2 = rec1.IntersectsWith(rec3);
            if (HayGol || HayGol2)
            {
                return true;
            }
            else
                return false;
            

        }
        private bool ColisionConJugador1(PictureBox pb1, PictureBox pb2) // checkea si hay colison con el jugador1 y la pelota 
        {
            TimerPelota.Enabled = true;
            TimerPelota.Start();
            Rectangle rect1 = new Rectangle(pb1.Location, pb1.Size);
            Rectangle rect2 = new Rectangle(pb2.Location, pb2.Size);

            bool hayColision = rect1.IntersectsWith(rect2);
            return hayColision;

        }
        private bool ColisionConJugador2(PictureBox pb1, PictureBox pb2) // checkea si hay colison con el jugador2 y la pelota 
        {
            TimerPelota.Enabled = true;
            TimerPelota.Start();
            Rectangle rect1 = new Rectangle(pb1.Location, pb1.Size);
            Rectangle rect2 = new Rectangle(pb2.Location, pb2.Size);

            bool hayColision = rect1.IntersectsWith(rect2);
            return hayColision;

        }

        private void TimerPelota_Tick(object sender, EventArgs e)
        {

            MoverPelota(); // Mueve la pelota           
            VerificarColisiones(); // Verifica colisiones y realiza el rebote si es necesario
            AplicarFriccion(); // Frena el movimiento en X en cada rebote.
            if (pelota.Location.Y > 100)
            {
                AplicarGravedad();
            }

            // Verifica la colisión con el jugador1
            if (ColisionConJugador1(Jugador1, pelota))
            {
                // Realiza alguna acción en caso de colisión con el jugador1
                // Por ejemplo, cambiar la dirección de la pelota o ajustar su posición
                velocidadReboteX = -velocidadReboteX; // Cambia la dirección en el eje X
                RealizarRebote(true, false); // Ajusta la posición de la pelota para evitar que pase al jugador1
            }
            // Verifica la colisión con el jugador1
            if (ColisionConJugador2(Jugador2, pelota))
            {
                // Realiza alguna acción en caso de colisión con el jugador2
                // Por ejemplo, cambiar la dirección de la pelota o ajustar su posición
                velocidadReboteX = -velocidadReboteX; // Cambia la dirección en el eje X
                RealizarRebote(true, false); // Ajusta la posición de la pelota para evitar que pase al jugador1
            }
            if (Gol(pelota, porteria, porteria2) == true)
            {
                pelota.Location = new Point(520, 369); // Si hay gol cambiamos la posicion a la inicial
            }

        }
        private void MoverPelota()
        {
            int x = pelota.Location.X;
            int y = pelota.Location.Y;
            
            // Actualiza la posición de la pelota
            pelota.Location = new Point(x - velocidadReboteX, y - velocidadReboteY);
            
        }

        private void VerificarColisiones()
        {
            int maxX = ClientSize.Width - pelota.Width;
           // int maxY = ClientSize.Height - pelota.Height; //250 es el suelo 
            
            // Verifica colisión con el suelo
            if (pelota.Location.Y > 250)
            {
                RealizarRebote(true, false); // Rebote en el eje Y
                AjustarPosicionMaxY(); // Ajusta la posición de la pelota para evitar que pase el suelo
            }

            // Verifica colisión con el techo 
            if (pelota.Location.Y < 0)
            {
                RealizarRebote(true, false); // Rebote en el eje Y
                AjustarPosicionMinY(); // Ajusta la posición de la pelota para evitar que pase el techo
            }

            // Verifica colisión con los bordes laterales
            if (pelota.Location.X < 0 || pelota.Location.X > maxX)
            {
                RealizarRebote(false, true); // Rebote en el eje X
            }
            //Verifica colisión con los jugadores
            if(pelota.Location.X == Jugador1.Location.X || pelota.Location.Y == Jugador1.Location.Y)
            {
                pelota.Location = new Point(pelota.Location.X, pelota.Location.Y);
                RealizarRebote(true, true);
                MoverPelota();
            }
        }

        private void AplicarGravedad()
        {
            velocidadReboteY -= gravedad;
        }
        private void AplicarFriccion()
        {
            pelota.Left += velocidadX;
            velocidadX *= Convert.ToInt32(friccion);
            if (Math.Abs(velocidadX) < 0.1)
            {
                velocidadX = 0;
                TimerPelota.Stop();  // Opcional: detener el temporizador cuando el movimiento se detiene
            }
        }
        private void RealizarRebote(bool enEjeY, bool enEjeX)
        {
            if (enEjeY)
            {
                velocidadReboteY = -velocidadReboteY; // Invierte la dirección del rebote en el eje Y
            }

            if (enEjeX)
            {
                velocidadReboteX = -velocidadReboteX; // Invierte la dirección del rebote en el eje X
            }
        }

        private void AjustarPosicionMaxY()
        {
            pelota.Location = new Point(pelota.Location.X, 250);
        }

        private void AjustarPosicionMinY()
        {
            pelota.Location = new Point(pelota.Location.X, 0);
        }
    }
}
