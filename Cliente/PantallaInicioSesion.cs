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
using System.Runtime.Remoting.Channels;

namespace Cliente
{
    public partial class PantallaSesionUsuario : Form
    {

        Socket server;
        Thread threadlogueo, threadsalaespera;
        delegate void DelegadoParaPonerTexto(string texto);

        List<SalaDeEspera> ListaVentanasDeEspera = new List<SalaDeEspera>();
        public PantallaSesionUsuario()
        {
            InitializeComponent();
        }

        public void AtenderServidor()
        {
            while (true)
            {
                //Recibimos mensaje del servidor
                byte[] msg2 = new byte[500];
                server.Receive(msg2);

                string MensajeLimpio = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                //Creamos un vector con cada trozo del mensaje recibido (cada cosa que va por cada / es un "trozo")
                string[] TrozosRespuesta = MensajeLimpio.Split('/');
                int codigo = 0;
                string RespuestaServidor;

                //El primer trozo es el código de la operación realizada
                codigo = Convert.ToInt32(TrozosRespuesta[0]);
                switch (codigo)
                {
                    case 1:  //Queremos iniciar sesión en nuestra cuenta
                          RespuestaServidor = TrozosRespuesta[2];
                          if (RespuestaServidor == "SI")
                            {
                                MessageBox.Show("Sesión iniciada correctamente, saludos " + TrozosRespuesta[1]);
                                //Arrancamos el thread que atenderá los mensajes del servidor
                                ThreadStart ts = delegate
                                {
                                    AbrirSaladeEspera();
                                };
                                threadsalaespera = new Thread(ts);
                                threadsalaespera.Start();
                                Usuario.Invoke(new Action(() =>
                                {
                                    Usuario.ReadOnly = true;
                                }));
                                BotonInicioSesion.Invoke(new Action(() =>
                                {
                                    BotonInicioSesion.Enabled = false;
                                }));
                                BotonRegistroCuenta.Invoke(new Action(() =>
                                {
                                    BotonRegistroCuenta.Enabled = false;
                                }));
                            }
                          else if (RespuestaServidor == "NO")
                            {
                                MessageBox.Show("Combinación de usuario y contraseña incorrecta");
                            }
                        break;

                    case 2:  //Queremos crearnos una nueva cuenta
                            RespuestaServidor = TrozosRespuesta[2];
                            if (RespuestaServidor == "SI")
                            {
                                MessageBox.Show("Cuenta creada satisfactoriamente, saludos " + TrozosRespuesta[1]);
                                if (threadlogueo.IsAlive == true)
                                {
                                    threadlogueo.Abort();
                                }
                                this.BackColor = Color.Gray;
                                server.Shutdown(SocketShutdown.Both);
                                server.Close();
                            }
                            else if (RespuestaServidor == "NO")
                            {
                                MessageBox.Show("El nombre de usuario facilitado ya existe, prueba con otro que esté disponible");
                            }
                            else if (RespuestaServidor == "ERROR")
                            {
                            MessageBox.Show("Ha ocurrido un error inesperado, prueba de intentarlo hacer más tarde");
                            }
                        break;

                    case 3:
                        RespuestaServidor = TrozosRespuesta[1];
                        ListaVentanasDeEspera[0].ModificarResultadoConsulta(RespuestaServidor);
                        break;

                    case 4:
                        RespuestaServidor = TrozosRespuesta[1];
                        ListaVentanasDeEspera[0].ModificarResultadoConsulta(RespuestaServidor);
                        break;

                    case 5:
                        RespuestaServidor = TrozosRespuesta[1];
                        ListaVentanasDeEspera[0].ModificarResultadoConsulta(RespuestaServidor);
                        break;

                    case 6:
                        RespuestaServidor = TrozosRespuesta[1];
                        ListaVentanasDeEspera[0].ActualizarListaConectados(RespuestaServidor);
                        break;

                    case 7:
                        RespuestaServidor = TrozosRespuesta[1];
                        string JugadorContrincante = TrozosRespuesta[2];
                        ListaVentanasDeEspera[0].GestionesInicioPartida(RespuestaServidor, JugadorContrincante);
                        break;
                }
            }
        }
        public void AbrirSaladeEspera()
        {
            SalaDeEspera SaladeEspera = new SalaDeEspera(server, Usuario.Text);
            ListaVentanasDeEspera.Add(SaladeEspera);
            SaladeEspera.ShowDialog();
        }
        private void OpcionInicioSesion_CheckedChanged(object sender, EventArgs e)
        {
            if (OpcionInicioSesion.Checked == true)
            {
                BotonInicioSesion.Invoke(new Action(() =>
                {
                    BotonInicioSesion.Enabled = true;
                }));
                BotonRegistroCuenta.Invoke(new Action(() =>
                {
                    BotonRegistroCuenta.Enabled = false;
                }));
            }
        }

        private void OpcionCuentaNueva_CheckedChanged(object sender, EventArgs e)
        {
            if (OpcionCuentaNueva.Checked == true)
            {
                BotonInicioSesion.Invoke(new Action(() =>
                {
                    BotonInicioSesion.Enabled = false;
                }));
                BotonRegistroCuenta.Invoke(new Action(() =>
                {
                    BotonRegistroCuenta.Enabled = true;
                }));
            }
        }

        private void BotonRegistroCuenta_Click(object sender, EventArgs e)
        {
            //Creamos un IPEndPoint con la IP del servidor y puerto del servidor 
            //al que deseamos conectarnos
            IPAddress direc = IPAddress.Parse("192.168.56.102");
            IPEndPoint ipep = new IPEndPoint(direc, 50009);

            //Creamos el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                if ((Contrasena.Text != "") && (Usuario.Text != ""))
                {
                    server.Connect(ipep); //Intentamos conectar el socket
                    this.BackColor = Color.Green;
                    MessageBox.Show("Conectado al servidor correctamente");
                    string mensaje = "2/" + Usuario.Text + '/' + Contrasena.Text;
                    //Enviamos al servidor el nombre tecleado
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);
                    //Arrancamos el thread que atenderá los mensajes del servidor
                    ThreadStart ts = delegate
                    {
                        AtenderServidor();
                    };
                    threadlogueo = new Thread(ts);
                    threadlogueo.Start();
                }

                else
                {
                    MessageBox.Show("No has introducido todos los datos necesarios para loguearte o registrarte");
                }
            }
            catch (SocketException)
            {
                //Si hay excepcion imprimimos error y salimos del programa con return 
                MessageBox.Show("No se ha podido conectar con el servidor");
                return;
            }
        }

        private void BotonInicioSesion_Click(object sender, EventArgs e)
        {
            //Creamos un IPEndPoint con la IP del servidor y puerto del servidor 
            //al que deseamos conectarnos
            IPAddress direc = IPAddress.Parse("192.168.56.102");
            IPEndPoint ipep = new IPEndPoint(direc, 50009);

            //Creamos el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                if ((Contrasena.Text != "") || (Usuario.Text != ""))
                {
                    server.Connect(ipep); //Intentamos conectar el socket
                    this.BackColor = Color.Green;
                    BotonCierreSesion.Invoke(new Action(() =>
                    {
                        BotonCierreSesion.Enabled = true;
                    }));
                    BotonRegistroCuenta.Invoke(new Action(() =>
                    {
                        BotonRegistroCuenta.Enabled = false;
                    }));
                    OpcionInicioSesion.Invoke(new Action(() =>
                    {
                        OpcionInicioSesion.Enabled = false;
                    }));
                    OpcionCuentaNueva.Invoke(new Action(() =>
                    {
                        OpcionCuentaNueva.Enabled = false;
                    }));
                    MessageBox.Show("Conectado al servidor correctamente");
                    string mensaje = "1/" + Usuario.Text + '/' + Contrasena.Text;
                    // Enviamos al servidor el nombre tecleado
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);
                    //Arrancamos el thread que atenderá los mensajes del servidor
                    ThreadStart ts = delegate
                    {
                        AtenderServidor();
                    };
                    threadlogueo = new Thread(ts);
                    threadlogueo.Start();
                }
                else
                {
                    MessageBox.Show("No has introducido todos los datos necesarios para loguearte o registrarte");
                }         
            }
            catch (SocketException)
            {
                //Si hay excepcion imprimimos error y salimos del programa con return 
                MessageBox.Show("No se ha podido conectar con el servidor");
                return;
            }
        }

        private void BotonCierreSesion_Click(object sender, EventArgs e)
        {
            if (this.BackColor == Color.Green)
            {
                //Mensaje de desconexión
                string mensaje = "0/" + Usuario.Text;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                // Nos desconectamos cuando el servidor haya recibido la respuesta final del servidor
                if (threadsalaespera.IsAlive == true)
                {
                    threadsalaespera.Abort();
                }
                if (threadlogueo.IsAlive == true)
                {
                    threadlogueo.Abort();
                }
                this.BackColor = Color.Gray;
                server.Shutdown(SocketShutdown.Both);
                server.Close();
                Usuario.Invoke(new Action(() =>
                {
                    Usuario.ReadOnly = false;
                }));
                OpcionCuentaNueva.Invoke(new Action(() =>
                {
                    OpcionCuentaNueva.Enabled = true;
                }));
                OpcionInicioSesion.Invoke(new Action(() =>
                {
                    OpcionInicioSesion.Enabled = true;
                }));
                //Reseteamos el contador de ventanas a 0
                ListaVentanasDeEspera.Clear();
                BotonCierreSesion.Invoke(new Action(() =>
                {
                    BotonCierreSesion.Enabled = false;
                }));
            }
        }

        private void PantallaSesionUsuario_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.BackColor == Color.Green)
            {
                //Mensaje de desconexión
                string mensaje = "0/" + Usuario.Text;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                // Nos desconectamos cuando el servidor haya recibido la respuesta final del servidor
                if (threadsalaespera.IsAlive == true)
                {
                    threadsalaespera.Abort();
                }
                if (threadlogueo.IsAlive == true)
                {
                    threadlogueo.Abort();
                }
                server.Shutdown(SocketShutdown.Both);
                server.Close();
            }
        }
    }
}
