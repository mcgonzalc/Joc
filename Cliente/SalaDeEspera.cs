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
        bool SaladeEsperaAbierta = false;
        List<Juego> ListaVentanasJuego = new List<Juego>();
        string Usuario, JugadorContrincante;
        public SalaDeEspera(Socket server, string Usuario)
        {
            InitializeComponent();
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
            Resultado.Invoke(new Action(() =>
            {
                Resultado.Text = resultado;
            }));
        }

        //Cómo actualizar la lista de usuarios conectados
        public void ActualizarListaConectados(string resultado)
        {
            //Creamos un vector con cada trozo del mensaje recibido (cada cosa que va por cada , es un "trozo")
            string[] Usuarios = resultado.Split(',');
            int usuariosconectadosantes = TablaUsuariosConectados.RowCount;
            int usuariosconectadosahora = Convert.ToInt32(Usuarios[0]);
            
            //Rellenamos la tabla de usuarios conectados si el formulario está abierto
            if (SaladeEsperaAbierta == true)
            {
                //Comprobamos si antes ya se han puesto datos en la tabla
                if (listacargada == true)
                {
                    //Cambiamos el nombre de las filas que ya tenemos creadas con los usuarios nuevos
                    for (int i = 0; i < (usuariosconectadosantes - 1); i++)
                    {
                        TablaUsuariosConectados.Invoke(new Action(() =>
                        {
                            TablaUsuariosConectados.Rows[i].Cells[0].Value = Usuarios[i + 1];
                            TablaUsuariosConectados.Refresh();
                        }));
                    }

                    //Borramos las filas que no usamos en caso de que ahora tengamos menos usuarios
                    if (usuariosconectadosahora < usuariosconectadosantes)
                    {
                        for (int i = usuariosconectadosahora; i < usuariosconectadosantes; i++)
                        {
                            TablaUsuariosConectados.Invoke(new Action(() =>
                            {
                                TablaUsuariosConectados.Rows.RemoveAt(usuariosconectadosahora);
                                TablaUsuariosConectados.Refresh();
                            }));
                        }
                    }

                    //Creamos las filas que necesitamos en caso de que ahora dispongamos de más usuarios conectados
                    else if (usuariosconectadosahora > usuariosconectadosantes)
                    {
                        for (int i = usuariosconectadosantes; i < usuariosconectadosahora; i++)
                        {
                            //Obtenemos el nombre del siguiente usuario conectado
                            string nombrenuevo = Convert.ToString(Usuarios[i + 1]);

                            //Creamos una nueva fila para cada usuario que queremos poner
                            TablaUsuariosConectados.Invoke(new Action(() =>
                            {
                                TablaUsuariosConectados.Rows.Add(nombrenuevo);
                                TablaUsuariosConectados.Refresh();
                            }));
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
                        TablaUsuariosConectados.Invoke(new Action(() =>
                        {
                            TablaUsuariosConectados.Rows.Add(nombrenuevo);
                            TablaUsuariosConectados.Refresh();
                        }));
                        listacargada = true;
                    }
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
                BotonInvitacion.Invoke(new Action(() =>
                {
                    BotonInvitacion.Enabled = false;
                }));

                DialogResult PeticionDuelo = MessageBox.Show("Has recibido una solicitud de partida por parte de " + JugadorContrincante + ", ¿deseas aceptar su petición?", "Invitación recibida", MessageBoxButtons.YesNo);
                if (PeticionDuelo == DialogResult.Yes)
                {
                    //Enviamos un mensaje al servidor para indicar de que aceptamos la partida
                    string mensaje = "7/ACEPTAR/" + JugadorContrincante;
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);

                    HistorialChat.Invoke(new Action(() =>
                    {
                        HistorialChat.Text = "";
                        HistorialChat.Enabled = true;
                    }));
                    MensajeChatAEnviar.Invoke(new Action(() =>
                    {
                        MensajeChatAEnviar.Enabled = true;
                    }));
                    BotonInicioPartida.Invoke(new Action(() =>
                    {
                        BotonInicioPartida.Enabled = true;
                    }));
                }
                else if (PeticionDuelo == DialogResult.No)
                {
                    //Enviamos un mensaje al servidor para indicar de que rechazamos la partida
                    string mensaje = "7/RECHAZAR/" + JugadorContrincante;
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);

                    //Habilitamos el botón de invitación para que el jugador pueda invitar a otra persona
                    BotonInvitacion.Invoke(new Action(() =>
                    {
                        BotonInvitacion.Enabled = true;
                    }));
                }
            }
            //Qué sucede cuando el contrincante ha aceptado la partida
            if (Gestion == "ACEPTADO")
            {
                HistorialChat.Invoke(new Action(() =>
                {
                    HistorialChat.Text = "";
                    HistorialChat.Enabled = true;
                }));
                MensajeChatAEnviar.Invoke(new Action(() =>
                {
                    MensajeChatAEnviar.Enabled = true;
                }));
                BotonInicioPartida.Invoke(new Action(() =>
                {
                    BotonInicioPartida.Enabled = true;
                }));
            }
            //Qué sucede cuando el contrincante ha rechazado la partida
            if (Gestion == "RECHAZADO")
            {
                MessageBox.Show("El usuario " + JugadorContrincante + " ha rechazado tu solicitud de partida.");
                BotonInvitacion.Invoke(new Action(() =>
                {
                    BotonInvitacion.Enabled = true;
                }));
            }
            if (Gestion == "EMPEZAR")
            {
                //Deshabilitamos el chat y la posibilidad de enviar más mensajes puesto que se empieza la partida
                HistorialChat.Invoke(new Action(() =>
                {
                    HistorialChat.Enabled = false;
                }));

                MensajeChatAEnviar.Invoke(new Action(() =>
                {
                    MensajeChatAEnviar.Enabled = false;
                }));

                BotonInicioPartida.Invoke(new Action(() =>
                {
                    BotonInicioPartida.Enabled = false;
                }));

                //Abrimos el juego puesto que se ha decidido de empezar ya la partida
                if (ListaVentanasJuego.Count < 0)
                {
                    ListaVentanasJuego.Clear();
                }
                    Juego Juego = new Juego(false, server); // abrimos como jugador visitante
                    ListaVentanasJuego.Add(Juego);
                    Juego.ShowDialog();
                
            }
        }
        
        //Procesos de inicialización a la hora de abrir el formulario
        private void SalaDeEspera_Load(object sender, EventArgs e)
        {
            SaladeEsperaAbierta = true;
            //Enviamos un mensaje al servidor para actualizar la lista de conectados para la nueva ventana
            string mensaje = "6/";
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }

        //Código que se ejecuta a la hora de cerrar el formulario
        private void SalaDeEspera_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Indicamos de que la ventana de la sala de espera está cerrada
            SaladeEsperaAbierta = false;
        }

        //Qué sucede cuando pulsamos el botón para enviar un mensaje por el chat
        private void BotonEnviarMensajeChat_Click(object sender, EventArgs e)
        {
            string mensaje = "8/" + JugadorContrincante + "/" + MensajeChatAEnviar.Text;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            GestionarMensajesChat(Usuario, MensajeChatAEnviar.Text);
            MensajeChatAEnviar.Text = "";
        }

        //Función para comprobar si hay un mensaje preparado para poder enviar por al chat
        private void MensajeChatAEnviar_TextChanged(object sender, EventArgs e)
        {
            if (MensajeChatAEnviar.Text != "")
            {
                BotonEnviarMensajeChat.Enabled = true;
            }
            else
            {
                BotonEnviarMensajeChat.Enabled = false;
            }
        }

        //Función para introducir mensajes en el historial del chat
        public void GestionarMensajesChat(string remitente, string mensaje)
        {
            HistorialChat.Invoke(new Action(() =>
            {
                //Comprueba que no haya texto escrito anteriormente en el historial del chat
                if (HistorialChat.Text != "")
                {
                    HistorialChat.Text = HistorialChat.Text + System.Environment.NewLine + remitente + ": " + mensaje;
                }
                else
                {
                    HistorialChat.Text = HistorialChat.Text + remitente + ": " + mensaje;
                }
            }));
        }

        private void BotonInicioPartida_Click(object sender, EventArgs e)
        {
            MensajeChatAEnviar.Text = "";

            //Enviamos un mensaje al servidor para indicar de que empezamos la partida
            string mensaje = "7/EMPEZAR/" + JugadorContrincante;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            //Deshabilitamos el chat y la posibilidad de enviar más mensajes puesto que se empieza la partida
            HistorialChat.Invoke(new Action(() =>
            {
                HistorialChat.Enabled = false;
            }));

            MensajeChatAEnviar.Invoke(new Action(() =>
            {
                MensajeChatAEnviar.Enabled = false;
            }));

            BotonInicioPartida.Invoke(new Action(() =>
            {
                BotonInicioPartida.Enabled = false;
            }));

            //Abrimos el juego puesto que se ha decidido de empezar ya la partida
            if (ListaVentanasJuego.Count < 0)
            {
                ListaVentanasJuego.Clear();
            }
                Juego Juego = new Juego(true, server); // abrimos como jugador local
                ListaVentanasJuego.Add(Juego);
                Juego.ShowDialog();
            
        }

        private void BotonInvitacion_Click(object sender, EventArgs e)
        {
            //Comprobamos si ya hay algún jugador seleccionado de la tabla de conectados y no es el nombre del usuario logueado en este cliente
            if (TablaUsuariosConectados.CurrentCell.Value != null && Convert.ToString(TablaUsuariosConectados.CurrentCell.Value) != Usuario)
            {
                //Enviamos un mensaje al servidor para solicitar una partida
                string mensaje = "7/ENVIAR/" + Convert.ToString(TablaUsuariosConectados.CurrentCell.Value);
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //Deshabilitamos el botón para poder evitar más de una invitación a la vez
                BotonInvitacion.Invoke(new Action(() =>
                {
                    BotonInvitacion.Enabled = false;
                }));
            }

            //Indicamos al usuario de seleccionar un jugador para poder realizar el proceso de invitación
            else
            {
                MessageBox.Show("No has seleccionado ningún jugador válido para invitar, selecciona a uno para empezar la partida");
            }
        }

        public void ActualizarPosicionRival(int PosicionX, int PosicionY)
        {
            ListaVentanasJuego[0].ActualizarPosicionRival(PosicionX, PosicionY);
        }
    }
}
