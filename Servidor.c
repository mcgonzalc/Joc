#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <pthread.h>
#include <mysql.h>

int contador;

//Estructura necesaria para acceso excluyente
pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;

int i;
int sockets[100];

MYSQL *conn;
int ResultadoConsulta;
// Estructura especial para almacenar resultados de consultas 
MYSQL_RES *resultado;
MYSQL_ROW row;
char ConsultaResultante [250];

void *AtenderCliente (void *socket)
{
	int sock_conn;
	int *s;
	s= (int *) socket;
	sock_conn= *s;
	
	//int socket_conn = * (int *) socket;
	
	char peticion[512];
	char respuesta[512];
	int ret;
	
	
	int terminar =0;
	// Entramos en un bucle para atender todas las peticiones de este cliente
	//hasta que se desconecte
	while (terminar == 0)
	{
		// Ahora recibimos la petici?n
		ret=read(sock_conn, peticion, sizeof(peticion));
		printf ("Recibido\n");
		
		// Tenemos que anadirle la marca de fin de string 
		// para que no escriba lo que hay despues en el buffer
		peticion[ret]='\0';
		
		
		printf ("Peticion: %s\n", peticion);
		
		//Vamos a ver que quieren
		char *p = strtok(peticion, "/");
		int CodigoConsulta =  atoi (p);
		
		if (CodigoConsulta != 0)
		{
			//Nos conectamos a la base de datos MySQL
			conn = mysql_init(NULL);
			if (conn==NULL) {
				printf ("Error al crear la conexion: %u %s\n", 
						mysql_errno(conn), mysql_error(conn));
			}
			
			//Inicializamos la conexión al servidor MySQL
			conn = mysql_real_connect (conn, "localhost","root", "mysql", "Juego", 0, NULL, 0);
			if (conn==NULL) {
				printf ("Error al inicializar la conexion: %u %s\n", 
						mysql_errno(conn), mysql_error(conn));
			}
			
		}
		
		if (CodigoConsulta == 0) //peticion de desconexion
		{
			terminar=1;
		}
		else if (CodigoConsulta == 1) //Piden iniciar sesion con su cuenta
		{
			char Usuario[80];
			char Contrasena[80];
			p = strtok(NULL, "/");
			strcpy (Usuario, p); // Ya tenemos el usuario
			p = strtok(NULL, "/");
			strcpy (Contrasena, p); //Conseguimos la contrasena
			
			printf ("Codigo: %d, Nombre: %s, Contrasena: %s\n", CodigoConsulta, Usuario, Contrasena);
			
			//Comprobamos que el usuario ya está registrado previamente
			
			strcpy (ConsultaResultante,"SELECT Jugador.Nombre FROM Jugador WHERE Jugador.Nombre = '");
			strcat (ConsultaResultante, Usuario);
			strcat (ConsultaResultante,"' AND Jugador.Contrasena = '");
			strcat (ConsultaResultante, Contrasena);
			strcat (ConsultaResultante,"'");
			
			//Si vemos que no nos sale ningun usuario, informamos
			ResultadoConsulta = mysql_query (conn, ConsultaResultante);
			if (ResultadoConsulta != 0)
			{
				printf ("Error al consultar datos de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
			}
			else if (ResultadoConsulta == 0)
			{
				//Recogemos el resultado de la consulta en una
				//tabla virtual MySQL
				resultado = mysql_store_result (conn);
				
				//Recogemos el resultado de la primera fila
				row = mysql_fetch_row (resultado);
				
				//Cerramos la conexión con el servidor
				mysql_close (conn);
				
				//Si no encuentra ningún usuario con ese nombre
				if (row == NULL)
				{
					sprintf(respuesta, "1/%s/NO", Usuario);
				}
				
				//Si se encuentra un usuario con ese nombre
				else if (row != NULL)
				{
					//Se hace un string del primer valor obtenido de la
					//columna generada con la consulta
					strcpy(respuesta, row[0]);
					
					//Se comprueba que el nombre obtenido es el del usuario
					//introducido
					if(strcmp(respuesta, Usuario) == 0)
					{
						sprintf(respuesta, "1/%s/SI", Usuario);
					}
					
					//Para evitar errores, en caso de que salga algun resultado,
					//se hace de todos modos la comparacion para asegurarse que esta
					//todo bien
					else if(strcmp(respuesta, Usuario) != 0)
					{
						sprintf(respuesta, "1/%s/NO", Usuario);
					}
				}
			}
		}
		
		
		else if (CodigoConsulta == 2) //Piden crearse una nueva cuenta
		{
			char Usuario[80];
			char Contrasena[80];
			p = strtok(NULL, "/");
			strcpy (Usuario, p); // Ya tenemos el usuario
			p = strtok(NULL, "/");
			strcpy (Contrasena, p); //Conseguimos la contrasena
			
			printf ("Codigo: %d, Nombre: %s, Contrasena: %s\n", CodigoConsulta, Usuario, Contrasena);
			
			//Comprobamos que el usuario ya está registrado previamente
			char ConsultaResultante [250];
			strcpy (ConsultaResultante,"SELECT Jugador.Nombre FROM Jugador WHERE Jugador.Nombre = '");
			strcat (ConsultaResultante, Usuario);
			strcat (ConsultaResultante,"'");
			
			ResultadoConsulta = mysql_query (conn, ConsultaResultante);
			if (ResultadoConsulta != 0)
			{
				printf ("Error al consultar datos de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
			}
			else if (ResultadoConsulta == 0)
			{
				//Recogemos el resultado de la consulta en una
				//tabla virtual MySQL
				resultado = mysql_store_result (conn);
				
				//Recogemos el resultado de la primera fila
				row = mysql_fetch_row (resultado);
				
				//Si no encuentra ningún usuario con ese nombre
				if (row == NULL)
				{
					//Abrimos otra vez MySQL para poder contar el número total de jugadores
					memset(ConsultaResultante, 0, strlen(ConsultaResultante));
					strcpy (ConsultaResultante,"SELECT Jugador.Identificador FROM Jugador");
					int aperturamysqlconsulta2 = mysql_query (conn, ConsultaResultante);
					if (aperturamysqlconsulta2 != 0)
					{
						printf ("Error al consultar datos de la base %u %s\n",
								mysql_errno(conn), mysql_error(conn));
						sprintf(respuesta, "2/%s/ERROR", Usuario);
					}
					else if (aperturamysqlconsulta2 == 0)
					{
						resultado = mysql_store_result (conn);
						row = mysql_fetch_row (resultado);
						int NumeroJugadoresInicial;
						char NumeroJugadoresFinal[100];
						
						while(row != NULL)
						{
							NumeroJugadoresInicial = atoi(row[0]);
							
							//Obtenemos la siguiente fila para el siguiente loop
							row = mysql_fetch_row (resultado);
						}
						
						NumeroJugadoresInicial++;
						sprintf(NumeroJugadoresFinal, "%d", NumeroJugadoresInicial);
						
						memset(ConsultaResultante, 0, strlen(ConsultaResultante));
						strcpy (ConsultaResultante, "INSERT INTO Jugador VALUES(");
						strcat (ConsultaResultante, NumeroJugadoresFinal);
						strcat (ConsultaResultante, ", '");
						strcat (ConsultaResultante, Usuario);
						strcat (ConsultaResultante, "', '");
						strcat (ConsultaResultante, Contrasena);
						strcat (ConsultaResultante, "')");
						
						ResultadoConsulta = mysql_query (conn, ConsultaResultante);
						if (ResultadoConsulta == 0)
						{
							sprintf(respuesta, "2/%s/SI", Usuario);
						}
						else if (ResultadoConsulta != 0)
						{
							printf ("Error al introducir datos en la base %u %s\n",
									mysql_errno(conn), mysql_error(conn));
							sprintf(respuesta, "2/%s/ERROR", Usuario);
						}
						
					}
					
				}
				
				//Si se encuentra un usuario con ese nombre
				else if (row != NULL)
				{
					sprintf(respuesta, "2/%s/NO", Usuario);
				}
			}	
		}
		if (CodigoConsulta !=0)
		{
			printf ("Respuesta: %s\n", respuesta);
			// Enviamos respuesta
			write (sock_conn,respuesta, strlen(respuesta));
		}
		if ((CodigoConsulta ==1) || (CodigoConsulta==2) || (CodigoConsulta==3) || (CodigoConsulta==4) || (CodigoConsulta==5))
		{
			pthread_mutex_lock( &mutex ); //No me interrumpas ahora
			contador = contador +1;
			pthread_mutex_unlock( &mutex); //ya puedes interrumpirme
			// notificar a todos los clientes conectados
			char notificacion[20];
			sprintf (notificacion, "6/%d",contador);
			int j;
			for (j=0; j< i; j++)
				write (sockets[j],notificacion, strlen(notificacion));
			
		}
	}
	// Se acabo el servicio para este cliente
	close(sock_conn); 
	
}


int main(int argc, char *argv[])
{
	
	int sock_conn, sock_listen;
	struct sockaddr_in serv_adr;
	
	// INICIALITZACIONS
	// Obrim el socket
	if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0)
		printf("Error creant socket");
	// Fem el bind al port
	
	
	memset(&serv_adr, 0, sizeof(serv_adr));// inicialitza a zero serv_addr
	serv_adr.sin_family = AF_INET;
	
	// asocia el socket a cualquiera de las IP de la m?quina. 
	//htonl formatea el numero que recibe al formato necesario
	serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
	// establecemos el puerto de escucha
	serv_adr.sin_port = htons(9050);
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
		printf ("Error al bind");
	
	if (listen(sock_listen, 3) < 0)
		printf("Error en el Listen");
	
	contador =0;
	
	pthread_t thread;
	i=0;
	for (;;){
		printf ("Escuchando\n");
		
		sock_conn = accept(sock_listen, NULL, NULL);
		printf ("He recibido conexion\n");
		
		sockets[i] =sock_conn;
		//sock_conn es el socket que usaremos para este cliente
		
		// Crear thead y decirle lo que tiene que hacer
		
		pthread_create (&thread, NULL, AtenderCliente,&sockets[i]);
		i=i+1;
		
	}
	
	
	
}
