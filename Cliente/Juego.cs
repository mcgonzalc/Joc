using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliente
{
    public partial class Juego : Form
    {
        bool JugadorLocal;
        Socket server;
        public Juego(bool JugadorLocal, Socket server)
        {
            InitializeComponent();
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.JugadorLocal = JugadorLocal;
            this.server = server;
        }
        
        private int velocidadSalto = 10;
        private int velocidadReboteX = 10;
        private int velocidadReboteY = 20;
        private int gravedad = 3;
        private int alturaSalto = 100;    
        private int sueloY;               
        private bool enSalto = false;  //Variable para verificar si el personaje está en el aire
        
        //Variables para guardar los puntos de cada jugador
        private int MarcadorLocal = 0;
        private int MarcadorVisitante = 0;

        //Tiempo que ha transcurrido en la partida
        private int TiempoPartida = 0;

        private void Juego_KeyPress(object sender, KeyPressEventArgs e) // Movimineto si dejas pulsada la tecla
        {
            if (JugadorLocal == true)
            {
                int x = JugadorIzquierda.Location.X;
                int y = JugadorIzquierda.Location.Y;
                if (e.KeyChar == 'D' || e.KeyChar == 'd')
                {
                    x += 10;
                }
                if (e.KeyChar == 'A' || e.KeyChar == 'a')
                {
                    x -= 10;
                }
                if (x < 66)
                {
                    x = 66;
                }
                if (x > 987 - 76)
                {
                    x = 987 - 76;
                }
                Point movimiento = new Point(x, y); // Creamos el nuevo punto a donde movimos el jugador
                JugadorIzquierda.Location = movimiento;

                string mensaje = "10/" + Convert.ToString(JugadorIzquierda.Location.X) + "/" + Convert.ToString(JugadorIzquierda.Location.Y);
                //Enviamos al servidor la petición deseada
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            if (JugadorLocal == false)
            {
                int x = JugadorDerecha.Location.X;
                int y = JugadorDerecha.Location.Y;
                if (e.KeyChar == 'D' || e.KeyChar == 'd')
                {
                    x += 10;
                }
                if (e.KeyChar == 'A' || e.KeyChar == 'a')
                {
                    x -= 10;
                }
                if (x < 66)
                {
                    x = 66;
                }
                if (x > 975 - 76)
                {
                    x = 975 - 76;
                }
                Point movimiento = new Point(x, y); // Creamos el nuevo punto a donde movimos el jugador
                JugadorDerecha.Location = movimiento;

                string mensaje = "10/" + Convert.ToString(JugadorDerecha.Location.X) + "/" + Convert.ToString(JugadorDerecha.Location.Y);
                //Enviamos al servidor la petición deseada
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
        }

        private void Juego_KeyDown(object sender, KeyEventArgs e) // Moviento al pulsar la tecla
        {
            if (e.KeyCode == Keys.Space && !enSalto)
            {
                enSalto = true;
                TimerSalto.Start();
            }
        }
        private void Juego_Load(object sender, EventArgs e)
        {
            sueloY = 250;  // Posicion Y del juador - altura de la imagen del jugador: 352-72
            MoverPelota(); // Mueve la pelota           
            VerificarColisiones(); // Verifica colisiones y realiza el rebote si es necesario
            ColisionConJugadorDerecha(pelota, JugadorDerecha);
            ColisionConJugadorIzquierda(pelota, JugadorIzquierda);
        }
        
        private bool GolPorteriaIzquierda( PictureBox pb1, PictureBox pb2) // Checkea si hay colision entre la porteria y la pelota
        {
            Rectangle rec1 = new Rectangle(pb1.Location , pb1.Size);
            Rectangle rec2 = new Rectangle(pb2.Location, pb2.Size);
            bool HayGol = rec1.IntersectsWith(rec2); 
            if (HayGol)
            {
                return true;
            }

            else
                return false;
        }
        private bool GolPorteriaDerecha(PictureBox pb1, PictureBox pb2) // Checkea si hay colision entre la porteria y la pelota
        {
            Rectangle rec1 = new Rectangle(pb1.Location, pb1.Size);
            Rectangle rec2 = new Rectangle(pb2.Location, pb2.Size);
            bool HayGol = rec1.IntersectsWith(rec2);
            if (HayGol)
            {
                return true;
            }

            else
                return false;
        }
        private bool ColisionConJugadorDerecha(PictureBox pb1, PictureBox pb2) // checkea si hay colison con el JugadorDerecha y la pelota 
        {
            TimerPelota.Enabled = true;
            TimerPelota.Start();
            Rectangle rect1 = new Rectangle(pb1.Location, pb1.Size);
            Rectangle rect2 = new Rectangle(pb2.Location, pb2.Size);

            bool hayColision = rect1.IntersectsWith(rect2);
            return hayColision;

        }
        private bool ColisionConJugadorIzquierda(PictureBox pb1, PictureBox pb2) // checkea si hay colison con el JugadorIzquierda y la pelota 
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
           
            if (pelota.Location.Y > 100)
            {
                AplicarGravedad();
                if (pelota.Location.Y == 250)
                {
                    InvertirGravedad();
                }
            }

            // Verifica la colisión con el JugadorDerecha
            if (ColisionConJugadorDerecha(JugadorDerecha, pelota))
            {
                // Realiza alguna acción en caso de colisión con el JugadorDerecha
                // Por ejemplo, cambiar la dirección de la pelota o ajustar su posición
                velocidadReboteX = -velocidadReboteX; // Cambia la dirección en el eje X
                RealizarRebote(true, false); // Ajusta la posición de la pelota para evitar que pase al JugadorDerecha
            }
            // Verifica la colisión con el JugadorDerecha
           if (ColisionConJugadorIzquierda(JugadorIzquierda, pelota))
            {
                // Realiza alguna acción en caso de colisión con el JugadorIzquierda
                // Por ejemplo, cambiar la dirección de la pelota o ajustar su posición
                velocidadReboteX = -velocidadReboteX; // Cambia la dirección en el eje X
                RealizarRebote(true, false); // Ajusta la posición de la pelota para evitar que pase al JugadorDerecha
            }
            if (GolPorteriaIzquierda(pelota, porteria) == true)
            {
                pelota.Location = new Point(350, 250); // Si hay gol cambiamos la posicion a la inicial
                MarcadorVisitante = MarcadorVisitante + 1;
            }
            if ( GolPorteriaDerecha ( pelota, porteria2)== true)
            {
                pelota.Location = new Point(350, 250); // Si hay gol cambiamos la posicion a la inicial
                MarcadorLocal = MarcadorLocal + 1;
            }
            Local.Text = Convert.ToString(MarcadorLocal);
            Visitante.Text = Convert.ToString(MarcadorVisitante);
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
            if (pelota.Location.Y < 70)
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
           
        }

        private void AplicarGravedad()
        {
            velocidadReboteY -= gravedad;
        }
        private void InvertirGravedad()
        {
            velocidadReboteY += gravedad;
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
            pelota.Location = new Point(pelota.Location.X, 70);
        }

        //Funcíón que determina qué sucede cuando avanza el tiempo durante la partida
        private void TimerPartida_Tick(object sender, EventArgs e)
        {
            TiempoPartida++;
            Duracion.Text = Convert.ToString(TiempoPartida);
            if (TiempoPartida == 120)
            {
                TimerPartida.Stop();
                TimerSalto.Stop();

                //Determinamos el mensaje postpartido que ha de salir en función del resultado
                if ((MarcadorLocal > MarcadorVisitante) && (JugadorLocal == true))
                {
                    MessageBox.Show("¡Has ganado, enhorabuena!");
                }

                else if ((MarcadorLocal < MarcadorVisitante) && (JugadorLocal == true))
                {
                    MessageBox.Show("No has ganado, lástima.");
                }

                else if ((MarcadorLocal > MarcadorVisitante) && (JugadorLocal == false))
                {
                    MessageBox.Show("No has ganado, lástima.");
                }

                else if ((MarcadorLocal < MarcadorVisitante) && (JugadorLocal == false))
                {
                    MessageBox.Show("¡Has ganado, enhorabuena!");
                }

                //Qué decir en caso de empate
                else if ((MarcadorLocal == MarcadorVisitante))
                {
                    MessageBox.Show("Vaya, habéis empatado...");
                }

                //El jugador local envía el resultado final al servidor para guardar los datos
                if (JugadorLocal == true)
                {
                    string mensaje = "9/" + Convert.ToString(MarcadorLocal) + "/" + Convert.ToString(MarcadorVisitante);

                    // Enviamos al servidor la petición deseada
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);
                }
                
                //Cerramos la ventana, puesto que la partida ya ha terminado
                Close();
            }
        }

        //Función que calcula el tiempo que un jugador ha de estar en el aire tras empezar un salto
        private void TimerSalto_Tick(object sender, EventArgs e)
        {
            if (JugadorLocal == true)
            {

                JugadorIzquierda.Top -= velocidadSalto;
                int x = JugadorIzquierda.Location.X;
                if (JugadorIzquierda.Top < sueloY - alturaSalto)
                {
                    velocidadSalto = -velocidadSalto;
                }

                if (JugadorIzquierda.Location.Y > sueloY)
                {
                    TimerSalto.Stop();
                    enSalto = false;
                    velocidadSalto = Math.Abs(velocidadSalto);
                    JugadorIzquierda.Location = new Point(x, sueloY);
                }
                string mensaje = "10/" + Convert.ToString(JugadorIzquierda.Location.X) + "/" + Convert.ToString(JugadorIzquierda.Location.Y);
                //Enviamos al servidor la petición deseada
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }

            if (JugadorLocal == false)
            {

                JugadorDerecha.Top -= velocidadSalto;
                int x = JugadorDerecha.Location.X;
                if (JugadorDerecha.Top < sueloY - alturaSalto)
                {
                    velocidadSalto = -velocidadSalto;
                }

                if (JugadorDerecha.Location.Y > sueloY)
                {
                    TimerSalto.Stop();
                    enSalto = false;
                    velocidadSalto = Math.Abs(velocidadSalto);
                    JugadorDerecha.Location = new Point(x, sueloY);
                }
                string mensaje = "10/" + Convert.ToString(JugadorDerecha.Location.X) + "/" + Convert.ToString(JugadorDerecha.Location.Y);
                //Enviamos al servidor la petición deseada
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
        }
        
        public void ActualizarPosicionRival(int PosicionX, int PosicionY)
        {
            
            if (JugadorLocal == true)
            {
                JugadorDerecha.Invoke(new Action(() =>
                {
                    JugadorDerecha.Location = new Point(PosicionX, PosicionY);
                }));
            }
            else if (JugadorLocal == false)
            {
                JugadorIzquierda.Invoke(new Action(() =>
                {
                    JugadorIzquierda.Location = new Point(PosicionX, PosicionY);
                }));
            }
        }
    }
}
