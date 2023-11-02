using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics.Eventing.Reader;

namespace Cliente
{
  
    public partial class SalaDeEspera : Form
    {
        Socket server;
        bool listacargada = false;
        List<Juego> formularios = new List<Juego>();
        public SalaDeEspera(Socket server)
        {
            InitializeComponent();
            this.server = server;
            CheckForIllegalCrossThreadCalls = false;
        }

        private void BotonConsulta_Click(object sender, EventArgs e)
        {
            if ((UsuarioaConsultar.Text != "") && (Consulta.SelectedIndex == 0)) //Queremos saber el número de partidas ganadas del jugador consultado
            {
                string mensaje = "4/" + UsuarioaConsultar.Text;
                // Enviamos al servidor la consulta deseada
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }

            if ((UsuarioaConsultar.Text != "") && (Consulta.SelectedIndex == 1)) //Queremos saber los puntos totales del jugador consultado
            {
                string mensaje = "3/" + UsuarioaConsultar.Text;
                // Enviamos al servidor la consulta deseada
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }

            if ((UsuarioaConsultar.Text != "") && (Consulta.SelectedIndex == 2)) //Queremos saber el número de partidas jugadas del jugador consultado
            {
                string mensaje = "5/" + UsuarioaConsultar.Text;
                // Enviamos al servidor la consulta deseada
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
        }

        //Cómo modificar el texto del resultado de la consulta realizada
        public void ModificarResultadoConsulta(string resultado)
        {
            Resultado.Text = resultado;
        }

        //Cómo actualizar la lista de usuarios conectados
        public void ActualizarListaConectados(string resultado)
        {
            //Creamos un vector con cada trozo del mensaje recibido (cada cosa que va por cada , es un "trozo")
            string[] Usuarios = resultado.Split(',');
            int usuariosconectadosantes = TablaUsuariosConectados.RowCount;
            int usuariosconectadosahora = Convert.ToInt32(Usuarios[0]);
            
            //Comprobamos si antes ya se han puesto datos en la tabla
            if (listacargada == true)
            {   
                //Cambiamos el nombre de las filas que ya tenemos creadas con los usuarios nuevos
                for (int i = 0; i < (usuariosconectadosantes-1); i++)
                {
                    TablaUsuariosConectados.Rows[i].Cells[0].Value = Usuarios[i+1];
                }

                TablaUsuariosConectados.Update();

                //Borramos las filas que no usamos en caso de que ahora tengamos menos usuarios
                if (usuariosconectadosahora < usuariosconectadosantes)
                {
                    for (int i = usuariosconectadosahora; i < usuariosconectadosantes; i++)
                    {
                        TablaUsuariosConectados.Rows.RemoveAt(usuariosconectadosahora);
                        TablaUsuariosConectados.Refresh();
                    }

                    TablaUsuariosConectados.Refresh();
                }

                //Creamos las filas que necesitamos en caso de que ahora dispongamos de más usuarios conectados
                else if (usuariosconectadosahora > usuariosconectadosantes)
                {
                    for (int i = usuariosconectadosantes; i < usuariosconectadosahora; i++)
                    {
                        //Obtenemos el nombre del siguiente usuario conectado
                        string nombrenuevo = Convert.ToString(Usuarios[i+1]);

                        //Creamos una nueva fila para cada usuario que queremos poner
                        TablaUsuariosConectados.Rows.Add(nombrenuevo);
                        TablaUsuariosConectados.Refresh();
                    }
                }
            }

            //Rellenamos la tabla por primera vez
            else if (listacargada == false)
            {
                for (int i = 1; i < (Convert.ToInt32(Usuarios[0]) + 1); i++)
                {
                    //Obtenemos el nombre del siguiente usuario conectado
                    string nombrenuevo = Convert.ToString(Usuarios[i]);

                    //Creamos una nueva fila para cada usuario que queremos poner
                    TablaUsuariosConectados.Rows.Add(nombrenuevo);
                    TablaUsuariosConectados.Refresh();
                    listacargada = true;
                }
            }
        }
        
        //Qué sucede cuando pulsamos el botón de actualizar la lista de usuarios conectados
        private void BotonActTablaConectados_Click(object sender, EventArgs e)
        {
            string mensaje = "6/";
            // Enviamos al servidor la consulta deseada
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }
        public void AbrirJuego()
        {
            Juego juego = new Juego();
            formularios.Add(juego);
            juego.ShowDialog();
        }
        private void Juego_Click(object sender, EventArgs e)
        {
            AbrirJuego();
        }
    }
}
