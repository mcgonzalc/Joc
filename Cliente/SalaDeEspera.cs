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
using Timer = System.Windows.Forms.Timer;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Cliente
{
  
    public partial class SalaDeEspera : Form
    {
        Socket server;
        bool listacargada = false;
        List<Juego> ListaVentanasJuego = new List<Juego>();
        string Usuario, JugadorContrincante;
        public SalaDeEspera(Socket server, string Usuario)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            this.server = server;
            this.Usuario = Usuario;
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
                    TablaUsuariosConectados.Refresh();
                }

                TablaUsuariosConectados.Refresh();

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
        
        //Qué hacer en caso de recibir algun mensaje por parte del servidor para empezar una partida
        public void GestionesInicioPartida(string Gestion, string JugadorContrincante)
        {
            this.JugadorContrincante = JugadorContrincante;

            //Qué sucede cuando recibimos una invitación a una partida
            if (Gestion == "RECIBIR")
            {
                //Deshabilitamos el botón de invitación para evitar que el jugador invite a alguien mientras está recibiendo una invitación
                BotonInvitacion.Enabled = false;

                TiempoLimiteInvitacion.Enabled = true;
                TiempoLimiteInvitacion.Start();

                DialogResult PeticionDuelo = MessageBox.Show("Has recibido una solicitud de partida por parte de " + JugadorContrincante + ", ¿deseas aceptar su petición?", "Invitación recibida", MessageBoxButtons.YesNo);
                if (PeticionDuelo == DialogResult.Yes)
                {
                    //Enviamos un mensaje al servidor para indicar de que aceptamos la partida
                    string mensaje = "7/ACEPTAR/" + JugadorContrincante;
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);

                    TiempoLimiteInvitacion.Stop();

                    //Abrimos el juego puesto que el jugador ya ha aceptado la partida
                    ListaVentanasJuego.Clear();
                    Juego Juego = new Juego();
                    ListaVentanasJuego.Add(Juego);
                    Juego.ShowDialog();
                }
                else if (PeticionDuelo == DialogResult.No)
                {
                    //Enviamos un mensaje al servidor para indicar de que rechazamos la partida
                    string mensaje = "7/RECHAZAR/" + JugadorContrincante;
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);

                    TiempoLimiteInvitacion.Stop();

                    //Habilitamos el botón de invitación para que el jugador pueda invitar a otra persona
                    BotonInvitacion.Enabled = true;
                }
            }
            //Qué sucede cuando el contrincante ha aceptado la partida
            if (Gestion == "ACEPTADO")
            {
                TiempoLimiteInvitacion.Stop();
                TiempoLimiteInvitacion.Enabled = false;

                //Abrimos el juego puesto que el otro jugador ha aceptado la partida
                ListaVentanasJuego.Clear();
                Juego Juego = new Juego();
                ListaVentanasJuego.Add(Juego);
                Juego.ShowDialog();
            }
            //Qué sucede cuando el contrincante ha rechazado la partida
            if (Gestion == "RECHAZADO")
            {
                TiempoLimiteInvitacion.Stop();
                TiempoLimiteInvitacion.Enabled = false;

                MessageBox.Show("El usuario " + JugadorContrincante + " ha rechazado tu solicitud de partida.");
                BotonInvitacion.Enabled = true;
            }
        }
        private void SalaDeEspera_Load(object sender, EventArgs e)
        {
            //Enviamos un mensaje al servidor para actualizar la lista de conectados para la nueva ventana
            string mensaje = "6/";
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }

        private void BotonInvitacion_Click(object sender, EventArgs e)
        {
            //Comprobamos si ya hay algún jugador seleccionado de la tabla de conectados y no es el nombre del usuario logueado en este cliente
            if (TablaUsuariosConectados.CurrentCell.Value != null && Convert.ToString(TablaUsuariosConectados.CurrentCell.Value) != Usuario)
            {
                //Enviamos un mensaje al servidor para solicitar una partida
                string mensaje = "7/ENVIAR/";
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje + Convert.ToString(TablaUsuariosConectados.CurrentCell.Value));
                server.Send(msg);

                //Deshabilitamos el botón para poder evitar más de una invitación a la vez
                BotonInvitacion.Enabled = false;

                TiempoLimiteInvitacion.Enabled = true;
                TiempoLimiteInvitacion.Start();
            }

            //Indicamos al usuario de seleccionar un jugador para poder realizar el proceso de invitación
            else
            {
                MessageBox.Show("No has seleccionado ningún jugador válido para invitar, selecciona a uno para empezar la partida");
            }
        }

        private void TiempoLimiteInvitacion_Tick(object sender, EventArgs e)
        {
            TiempoLimiteInvitacion.Stop();

            bool JugadorEncontrado = false;

            while (JugadorEncontrado == false)
            {
                for (int i = 0; i < TablaUsuariosConectados.RowCount; i++)
                {
                    if (Convert.ToString(TablaUsuariosConectados.Rows[i].Cells[0].Value) == JugadorContrincante)
                    {
                        JugadorEncontrado = true;
                    }
                }
            }
            
            if (JugadorEncontrado == false)
            {
                MessageBox.Show("El oponente se ha retirado, ya puedes volver a empezar otra partida con otro jugador");
                BotonInvitacion.Enabled = true;
            }

            TiempoLimiteInvitacion.Enabled = false;
        }
    }
}
