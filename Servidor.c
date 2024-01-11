#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <pthread.h>
#include <mysql.h>
#include <time.h>
//#include <my_global.h> //Libreria especifica para produccion

//Estructuras para los usuarios conectados
typedef struct {
	
	char Nombre[80];
	int Socket;
	
}Conectado;

typedef struct {
	
	Conectado Conectados[100];
	int NumJugadoresConectados;
	
}ListaConectados;

typedef struct {
	
	int NumeroPartida;
	char Jugador1[80];
	char Jugador2[80];
	int PuntosJugador1;
	int PuntosJugador2;
	
}PartidaActiva;

typedef struct {
	
	PartidaActiva PartidasActivas[50];
	
}ListaPartidasActivas;

int ResultadoConsulta;
// Estructura especial para almacenar resultados de consultas 
MYSQL_RES *resultado;
MYSQL_ROW row;
char ConsultaResultante[1250];
ListaConectados ListaUsuariosConectados;
ListaPartidasActivas ListaPartidasPreparadas;
int sockets[100];
int SocketsOcupados;
int PosicionSocket;
int SocketVacioEncontrado;

//Estructura necesaria para acceso excluyente
pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;


int AnadirJugadorConectado(ListaConectados *ListaJugadoresConectados, char NombreNuevo[80], int Socket)
{
	//Si la lista ya esta llena, indicar que no hay espacio para mas jugadores
	if (ListaJugadoresConectados->NumJugadoresConectados == 100)
	{
		return -1;
	}
	else
	{
		//Si hay espacio, introducimos el nuevo jugador en la lista de conectados
		strcpy(ListaJugadoresConectados->Conectados[ListaJugadoresConectados->NumJugadoresConectados].Nombre, NombreNuevo);
		ListaJugadoresConectados->Conectados[ListaJugadoresConectados->NumJugadoresConectados].Socket = Socket;
		
		//Aumentamos en 1 el numero total de jugadores
		ListaJugadoresConectados->NumJugadoresConectados++;
		return 0;
	}
}

int BuscarSocketJugador(ListaConectados *ListaJugadoresConectados, char NombreaBuscar[80])
{
	int JugadorEncontrado = 0;
	int i = 0;
	
	//Se hace una busqueda mientras no nos pasemos del limite de la estructura o hasta que encontremos el jugador
	while (JugadorEncontrado != 1 && ListaJugadoresConectados->NumJugadoresConectados > i)
	{
		if (strcmp(ListaJugadoresConectados->Conectados[i].Nombre, NombreaBuscar) == 0)
		{
			JugadorEncontrado = 1;
		}
		
		else
		{
			i++;
		}
	}
	
	//Si se encuentra el jugador deseado, se devuelve el socket del jugador
	if (JugadorEncontrado == 1)
	{
		return ListaJugadoresConectados->Conectados[i].Socket;
	}
	
	//Si no se encuentra el jugador deseado, se devuelve un -1
	else
	{
		return -1;
	}
}

int BuscarPosicionJugador(ListaConectados *ListaJugadoresConectados, char NombreaBuscar[80])
{	
	//Se hace una busqueda mientras no nos pasemos del limite de la estructura o hasta que encontremos el jugador
	for (int i = 0; i < ListaJugadoresConectados->NumJugadoresConectados; i++)
	{
		if (strcmp(ListaJugadoresConectados->Conectados[i].Nombre, NombreaBuscar) == 0)
		{
			//Si se encuentra el jugador deseado, se devuelve el numero del jugador
			return i;
		}
	}
	
	//Si no se encuentra el jugador deseado, se devuelve un -1
	return -1;
}

int EliminarJugadorConectado(ListaConectados *ListaJugadoresConectados, char NombreaBorrar[80])
{
	int PosicionJugadoraEliminar = BuscarPosicionJugador(ListaJugadoresConectados, NombreaBorrar);
	
	//Que hacer en caso de que se encuentre al jugador
	if (PosicionJugadoraEliminar != -1)
	{
		for (int i = PosicionJugadoraEliminar; i < ListaJugadoresConectados->NumJugadoresConectados; i++)
		{
			ListaJugadoresConectados->Conectados[i] = ListaJugadoresConectados->Conectados[i+1];
		}
		
		ListaJugadoresConectados->NumJugadoresConectados = (ListaJugadoresConectados->NumJugadoresConectados)-1;
		return 0;
	}
	//Que hacer en caso de que no se encuentre al jugador a eliminar
	else
	{
		return -1;
	}
}

void ObtenerListaJugadoresConectados(ListaConectados *ListaJugadoresConectados, char ListaResultante[1000])
{
	pthread_mutex_lock(&mutex);
	//Ponemos el numero de jugadores totales en el vector resultante
	sprintf(ListaResultante, "%d", ListaJugadoresConectados->NumJugadoresConectados);
	
	//Hacemos un bucle hasta llegar hasta el ultimo jugador
	for(int i = 0; i < ListaJugadoresConectados->NumJugadoresConectados; i++)
	{
		strcat(ListaResultante, ",");
		strcat(ListaResultante, ListaJugadoresConectados->Conectados[i].Nombre);
	}
	pthread_mutex_unlock(&mutex);
}

void RegistrarCuenta(MYSQL *conn, char Usuario[80], char Contrasena[80], char Respuesta[512])
{	
	pthread_mutex_lock(&mutex);
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	char ConsultaResultante[1250];
	int ResultadoConsulta;
	
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
				printf ("Error al consultar datos de la base %u %s\n", mysql_errno(conn), mysql_error(conn));
				sprintf(Respuesta, "2/%s/ERROR", Usuario);
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
					sprintf(Respuesta, "2/%s/SI", Usuario);
				}
				else if (ResultadoConsulta != 0)
				{
					printf ("Error al introducir datos en la base %u %s\n", mysql_errno(conn), mysql_error(conn));
					sprintf(Respuesta, "2/%s/ERROR", Usuario);
				}
				
			}
			
		}
		
		//Si se encuentra un usuario con ese nombre
		else if (row != NULL)
		{
			sprintf(Respuesta, "2/%s/NO", Usuario);
		}
	}
	
	pthread_mutex_unlock(&mutex);
}

void IniciarSesion(MYSQL *conn, char Usuario[80], char Contrasena[80], int Socket, char Respuesta[512])
{
	//Comprobamos que el usuario ya esta registrado previamente
	strcpy (ConsultaResultante,"SELECT Jugador.Nombre FROM Jugador WHERE Jugador.Nombre = '");
	strcat (ConsultaResultante, Usuario);
	strcat (ConsultaResultante,"' AND Jugador.Contrasena = '");
	strcat (ConsultaResultante, Contrasena);
	strcat (ConsultaResultante,"'");
	int ResultadoConsulta = mysql_query (conn, ConsultaResultante);
	
	//Si vemos que no nos sale ningun usuario, informamos
	if (ResultadoConsulta != 0)
	{
		printf ("Error al consultar datos de la base %u %s\n", mysql_errno(conn), mysql_error(conn));
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
			sprintf(Respuesta, "1/%s/NO", Usuario);
		}
		
		//Si se encuentra un usuario con ese nombre
		else if (row != NULL)
		{
			//Se hace un string del primer valor obtenido de la
			//columna generada con la consulta
			strcpy(Respuesta, row[0]);
			
			//Se comprueba que el nombre obtenido es el del usuario
			//introducido
			if(strcmp(Respuesta, Usuario) == 0)
			{
				pthread_mutex_lock(&mutex);
				ResultadoConsulta = AnadirJugadorConectado(&ListaUsuariosConectados, Usuario, Socket);
				pthread_mutex_unlock(&mutex);
				
				//Aceptamos el inicio de sesión basandonos en si hay espacio suficiente en el servidor para mas personas
				if (ResultadoConsulta == 0)
				{
					sprintf(Respuesta, "1/%s/SI", Usuario);
				}
				else if (ResultadoConsulta == -1)
				{
					sprintf(Respuesta, "1/%s/NO", Usuario);
				}
			}
			
			//Para evitar errores, en caso de que salga algun resultado,
			//se hace de todos modos la comparacion para asegurarse que esta
			//todo bien
			else if(strcmp(Respuesta, Usuario) != 0)
			{
				sprintf(Respuesta, "1/%s/NO", Usuario);
			}
		}
	}
}

void ObtenerPuntuacionJugador(MYSQL *conn, char Usuario[80], char Respuesta[512])
{
	int PuntosTotales = 0;
	
	pthread_mutex_lock(&mutex);
	ResultadoConsulta = mysql_query (conn, ConsultaResultante);
	if (ResultadoConsulta != 0)
	{
		printf ("Error al consultar datos de la base %u %s\n", mysql_errno(conn), mysql_error(conn));
	}
	
	//Recogemos el resultado de la consulta en una tabla virtual MySQL
	resultado = mysql_store_result (conn);
	
	//Recogemos el resultado de la primera fila
	row = mysql_fetch_row (resultado);
	
	//Analizamos para empezar la primera fila para saber si hemos obtenido resultados con la consulta
	if (row == NULL)
	{
		printf ("No se han obtenido datos en la consulta\n");
	}
	
	//En caso de obtener resultados, se analiza cada fila hasta llegar
	//a la primera fila con un valor nulo
	else
		while (row != NULL)
	{
			//Convertimos a int la columna 0, que es la que contiene
			//los puntos de la partida analizada
			
			int PuntosPartida = atoi(row[0]);
			
			PuntosTotales = PuntosTotales + PuntosPartida;
			
			//Obtenemos la siguiente fila para el siguiente loop
			row = mysql_fetch_row (resultado);
	}
		pthread_mutex_unlock(&mutex);
		sprintf(Respuesta, "3/%d", PuntosTotales);
}

void ObtenerPartidasGanadasJugador(MYSQL *conn, char Usuario[80], char Respuesta[512])
{
	int PartidasGanadas = 0;
	pthread_mutex_lock(&mutex);
	ResultadoConsulta = mysql_query (conn, ConsultaResultante);
	if (ResultadoConsulta != 0)
	{
		printf ("Error al consultar datos de la base %u %s\n", mysql_errno(conn), mysql_error(conn));
	}
	
	//Recogemos el resultado de la consulta en una
	//tabla virtual MySQL
	resultado = mysql_store_result (conn);
	
	//Recogemos el resultado de la primera fila
	row = mysql_fetch_row (resultado);
	
	//Analizamos para empezar la primera fila para saber
	//si hemos obtenido resultados con la consulta
	if (row == NULL)
	{
		printf ("No se han obtenido datos en la consulta\n");
	}
	
	//En caso de obtener resultados, se analiza cada fila hasta llegar
	//a la primera fila con un valor nulo
	else
		while (row != NULL)
	{
			//Sumamos 1 partida ganada por cada fila analizada
			PartidasGanadas++;
			
			//Obtenemos la siguiente fila para el siguiente loop
			row = mysql_fetch_row (resultado);
	}
		
		pthread_mutex_unlock(&mutex);
		sprintf(Respuesta, "4/%d", PartidasGanadas);
}

void ObtenerPartidasJugadasJugador(MYSQL *conn, char Usuario[80], char Respuesta[512])
{
	int PartidasJugadas = 0;
	
	ResultadoConsulta = mysql_query (conn, ConsultaResultante);
	
	if (ResultadoConsulta != 0)
	{
		printf ("Error al consultar datos de la base %u %s\n", mysql_errno(conn), mysql_error(conn));
	}
	
	//Recogemos el resultado de la consulta en una
	//tabla virtual MySQL
	resultado = mysql_store_result (conn);
	
	//Recogemos el resultado de la primera fila
	row = mysql_fetch_row (resultado);
	
	//Analizamos para empezar la primera fila para saber
	//si hemos obtenido resultados con la consulta
	if (row == NULL)
		printf ("No se han obtenido datos en la consulta\n");
	
	//En caso de obtener resultados, se analiza cada fila hasta llegar
	//a la primera fila con un valor nulo
	else
		while (row != NULL)
	{
			//Sumamos 1 partida ganada por cada fila analizada
			PartidasJugadas++;
			
			//Obtenemos la siguiente fila para el siguiente loop
			row = mysql_fetch_row (resultado);
	}
		
		sprintf(Respuesta, "5/%d", PartidasJugadas);
}

void CrearPartida(ListaPartidasActivas *ListaPartidasPreparadas, char Jugador1[80], char Jugador2[80])
{
	pthread_mutex_lock(&mutex);
	int NumeroDePartida = 0;
	
	//Buscamos un hueco para poner una partida en nuestra tabla de partidas
	while (ListaPartidasPreparadas->PartidasActivas[NumeroDePartida].NumeroPartida != -1)
	{
		NumeroDePartida++;
	}
	
	//Inicializamos la partida con los nombres de los jugadores y reinicializamos el marcador
	ListaPartidasPreparadas->PartidasActivas[NumeroDePartida].NumeroPartida = NumeroDePartida;
	strcpy(ListaPartidasPreparadas->PartidasActivas[NumeroDePartida].Jugador1, Jugador1);
	strcpy(ListaPartidasPreparadas->PartidasActivas[NumeroDePartida].Jugador2, Jugador2);
	ListaPartidasPreparadas->PartidasActivas[NumeroDePartida].PuntosJugador1 = 0;
	ListaPartidasPreparadas->PartidasActivas[NumeroDePartida].PuntosJugador2 = 0;
	pthread_mutex_unlock(&mutex);
}

void ObtenerFechaActual(char Respuesta[200])
{
	time_t now;
	time(&now);
	struct tm *local = localtime(&now);
	
	int Horas = local->tm_hour;         // get hours since midnight (0-23)
    int Minutos = local->tm_min;        // get minutes passed after the hour (0-59)
 
    int Dia = local->tm_mday;            // get day of month (1 to 31)
    int Mes = local->tm_mon + 1;      // get month of year (0 to 11)
    int Ano = local->tm_year + 1900;   // get year since 1900
	
	printf("La fecha guardada para esta partida es: %d/%d/%d %d:%d\n", Dia, Mes, Ano, Horas, Minutos); //Si no funciona, revisar: https://www.techiedelight.com/print-current-date-and-time-in-c/
	sprintf(Respuesta, "%d/%d/%d %d:%d", Dia, Mes, Ano, Horas, Minutos);
}
	
	
void GuardarPartida(MYSQL *conn, char Jugador1[80], int Puntos1, char Jugador2[80], int Puntos2) //WIP
{
	//Obtenemos el numero total de partidas guardadas en la base de datos
	ResultadoConsulta = mysql_query (conn, ConsultaResultante);
	int NumeroPartida = ResultadoConsulta + 1;
	
	//Primero determinamos cual ha sido el ganador de la partida
	char Ganador[80];
	if (Puntos1 > Puntos2)
	{
		sprintf(Ganador, "%s", Jugador1);
	}
	else if (Puntos1 < Puntos2)
	{
		sprintf(Ganador, "%s", Jugador2);
	}
	
	//Que poner en caso de haber un empate
	else
	{
		sprintf(Ganador, "Empate");
	}
	
	//Obtenemos la fecha del sistema a la hora de guardar la partida
	char FechaActual[200];
	ObtenerFechaActual(FechaActual);
	
	//Pasamos la nueva partida a la base de datos
	sprintf(ConsultaResultante, "INSERT INTO Partida VALUES(%d,'%s',120,'%s')", NumeroPartida, FechaActual, Ganador);
	mysql_query (conn, ConsultaResultante);
	
	//Pasamos los resultados individuales de cada jugador de la partida
	//Primero obtenemos el valor del jugador a insertar
	strcpy (ConsultaResultante,"SELECT Jugador.Identificador FROM Jugador WHERE Jugador.Nombre = '");
	strcat (ConsultaResultante, Jugador1);
	strcat (ConsultaResultante, "'");
	ResultadoConsulta = mysql_query (conn, ConsultaResultante);
	
	//Recogemos el resultado de la consulta en una
	//tabla virtual MySQL
	resultado = mysql_store_result (conn);
	
	//Recogemos el resultado de la primera fila
	row = mysql_fetch_row (resultado);
	
	int Identificador = atoi(row[0]);
	
	//Guardamos el resultado del primero jugador en la base de datos
	sprintf(ConsultaResultante, "INSERT INTO Participacion VALUES(%d,%d,%d)", Identificador, NumeroPartida, Puntos1);
	mysql_query (conn, ConsultaResultante);
	
	//Ahora repetimos el mismo proceso para el otro jugador
	strcpy (ConsultaResultante,"SELECT Jugador.Identificador FROM Jugador WHERE Jugador.Nombre = '");
	strcat (ConsultaResultante, Jugador2);
	strcat (ConsultaResultante, "'");
	ResultadoConsulta = mysql_query (conn, ConsultaResultante);
	
	//Recogemos el resultado de la consulta en una
	//tabla virtual MySQL
	resultado = mysql_store_result (conn);
	
	//Recogemos el resultado de la primera fila
	row = mysql_fetch_row (resultado);
	
	Identificador = atoi(row[0]);
	
	//Guardamos el resultado del primero jugador en la base de datos
	sprintf(ConsultaResultante, "INSERT INTO Participacion VALUES(%d,%d,%d)", Identificador, NumeroPartida, Puntos2);
	mysql_query (conn, ConsultaResultante);
}
	
void *AtenderCliente (void *socket)
{
	int sock_conn;
	MYSQL *conn;
	int *s;
	s= (int *) socket;
	sock_conn= *s;
	
	char Peticion[1000];
	char Respuesta[1000];
	int ret;
	
	char Usuario[80];
	char UsuarioContrincante[80];
	char UsuarioConsultado[80];
	
	int terminar = 0;
	// Entramos en un bucle para atender todas las peticiones de este cliente
	//hasta que se desconecte
	while (terminar == 0)
	{
		// Ahora recibimos la peticion
		ret=read(sock_conn, Peticion, sizeof(Peticion));
		printf ("Recibido\n");
		
		// Tenemos que anadirle la marca de fin de string 
		// para que no escriba lo que hay despues en el buffer
		Peticion[ret]='\0';
		
		printf ("Peticion: %s\n", Peticion);
		
		//Vamos a ver que quieren
		char *p = strtok(Peticion, "/");
		int CodigoConsulta =  atoi (p);
		
		if (CodigoConsulta != 0)
		{
			//Nos conectamos a la base de datos MySQL
			conn = mysql_init(NULL);
			if (conn==NULL)
			{
				printf ("Error al crear la conexion: %u %s\n", mysql_errno(conn), mysql_error(conn));
			}
			
			//Inicializamos la conexión al servidor MySQL
			conn = mysql_real_connect (conn, "localhost", "root", "mysql", "M3BD", 0, NULL, 0);
			if (conn==NULL)
			{
				printf ("Error al inicializar la conexion: %u %s\n", mysql_errno(conn), mysql_error(conn));
			}
			
		}
		
		switch (CodigoConsulta)
		{
		case 0: //Piden desconectarse del servidor
		{
			p = strtok(NULL, "/");
			strcpy (Usuario, p); //Ya tenemos el usuario
			EliminarJugadorConectado(&ListaUsuariosConectados, Usuario);
			sprintf(Respuesta, "0/%s", Usuario);
			printf("Desconectamos el usuario %s\n", Usuario);
			
			//Actualizamos la lista de conectados para los clientes que ya estaban conectados
			if (0 < ListaUsuariosConectados.NumJugadoresConectados)
			{
				char TablaJugadoresConectados[1000];
				ObtenerListaJugadoresConectados(&ListaUsuariosConectados, TablaJugadoresConectados);
				sprintf(Respuesta, "6/%s", TablaJugadoresConectados);
				
				for (int i = 0; i < 100; i++)
				{
					//Enviamos la actualizacion de la lista de conectados a los sockets que no esten ocupados
					if (sockets[i] != -1 && sockets[i] != sock_conn)
					{
						write(sockets[i], Respuesta, strlen(Respuesta));
					}
				}
			}
			terminar = 1;
		}
		break;
		
		case 1: //Piden iniciar sesion con su cuenta
		{
			char Contrasena[80];
			p = strtok(NULL, "/");
			strcpy (Usuario, p); // Ya tenemos el usuario
			p = strtok(NULL, "/");
			strcpy (Contrasena, p); //Conseguimos la contrasena
			
			printf ("Codigo: %d, Nombre: %s, Contrasena: %s\n", CodigoConsulta, Usuario, Contrasena);
			
			IniciarSesion(conn, Usuario, Contrasena, sock_conn, Respuesta);
			
			//Enviamos la respuesta del servidor al cliente
			printf ("Respuesta: %s\n", Respuesta);
			write (sock_conn, Respuesta, strlen(Respuesta));
			
			//Actualizamos la lista de conectados para los clientes que ya estaban conectados
			if (1 < ListaUsuariosConectados.NumJugadoresConectados)
			{
				char TablaJugadoresConectados[1000];
				ObtenerListaJugadoresConectados(&ListaUsuariosConectados, TablaJugadoresConectados);
				sprintf(Respuesta, "6/%s", TablaJugadoresConectados);
				
				for (int i = 0; i < 100; i++)
				{
					//Enviamos la actualizacion de la lista de conectados a los sockets que no esten ocupados
					if (sockets[i] != -1 && sockets[i] != sock_conn)
					{
						write(sockets[i], Respuesta, strlen(Respuesta));
					}
				}
			}
		}
		break;
		
		case 2: //Piden crearse una nueva cuenta
		{
			char Contrasena[80];
			p = strtok(NULL, "/");
			strcpy (Usuario, p); // Ya tenemos el usuario
			p = strtok(NULL, "/");
			strcpy (Contrasena, p); //Conseguimos la contrasena
			
			printf ("Codigo: %d, Nombre: %s, Contrasena: %s\n", CodigoConsulta, Usuario, Contrasena);
			
			RegistrarCuenta(conn, Usuario, Contrasena, Respuesta);
			
			printf ("Respuesta: %s\n", Respuesta);
			write (sock_conn, Respuesta, strlen(Respuesta));
			terminar = 1;
		}
		break;
		
		case 3: //Piden calcular los puntos que ha obtenido un jugador en todas las partidas
		{
			p = strtok(NULL, "/");
			strcpy (UsuarioConsultado, p); // Ya tenemos el usuario
			
			printf ("Codigo: %d, Nombre: %s\n", CodigoConsulta, UsuarioConsultado);
			
			strcpy (ConsultaResultante,"SELECT Participacion.Puntos FROM Participacion,Jugador WHERE Jugador.Nombre = '");
			strcat (ConsultaResultante, UsuarioConsultado);
			strcat (ConsultaResultante,"' AND Jugador.Identificador = Participacion.Jugador");
			
			ObtenerPuntuacionJugador(conn, UsuarioConsultado, Respuesta);
			printf ("Respuesta: %s\n", Respuesta);
			write (sock_conn, Respuesta, strlen(Respuesta));
		}
		break;
		
		case 4: //Consulta para el numero total de partidas ganadas por un jugador
		{
			p = strtok(NULL, "/");
			strcpy (UsuarioConsultado, p); // Ya tenemos el usuario
			
			printf ("Codigo: %d, Nombre: %s\n", CodigoConsulta, UsuarioConsultado);
			
			//Creamos el string para poder hacer la consulta a MySQL
			//con una variable, que es la lista de partidas ganadas por el jugador buscado
			strcpy (ConsultaResultante,"SELECT Partida.Identificador FROM Partida, Jugador WHERE Jugador.Nombre = '");
			strcat (ConsultaResultante, UsuarioConsultado);
			strcat (ConsultaResultante,"' AND Jugador.Identificador = Partida.Ganador");
			
			ObtenerPartidasGanadasJugador(conn, UsuarioConsultado, Respuesta);
			printf ("Respuesta: %s\n", Respuesta);
			write (sock_conn, Respuesta, strlen(Respuesta));
		}	
		break;
		
		case 5: //Consulta para el numero total de partidas jugadas por un jugador
		{
			p = strtok(NULL, "/");
			strcpy (UsuarioConsultado, p); // Ya tenemos el usuario
			
			printf ("Codigo: %d, Nombre: %s\n", CodigoConsulta, UsuarioConsultado);
			
			strcpy (ConsultaResultante,"SELECT Participacion.Partida FROM Participacion, Jugador WHERE Jugador.Nombre = '");
			strcat (ConsultaResultante, UsuarioConsultado);
			strcat (ConsultaResultante,"' AND Jugador.Identificador = Participacion.Jugador");
			
			ObtenerPartidasJugadasJugador(conn, UsuarioConsultado, Respuesta);
			printf ("Respuesta: %s\n", Respuesta);
			write (sock_conn, Respuesta, strlen(Respuesta));
		}
		break;
		
		case 6: //Consulta para pedir la lista de usuarios conectados
		{
			char TablaJugadoresConectados[1000];
			ObtenerListaJugadoresConectados(&ListaUsuariosConectados, TablaJugadoresConectados);
			sprintf(Respuesta, "6/%s", TablaJugadoresConectados);
			printf ("Respuesta: %s\n", Respuesta);
			write (sock_conn, Respuesta, strlen(Respuesta));
		}
		break;
		
		case 7: //Peticion para gestionar inicio de partida
		{
			char Gestion[20];
			p = strtok(NULL, "/"); //Obtenemos la gestion a realizar
			strcpy (Gestion, p);
			p = strtok(NULL, "/"); //Obtenemos el nombre del contrincante
			strcpy (UsuarioContrincante, p);
			
			int SocketContrincante = BuscarSocketJugador(&ListaUsuariosConectados, UsuarioContrincante);
			
			if (strcmp(Gestion, "ENVIAR") == 0) //El usuario quiere solicitar un duelo
			{
				sprintf(Respuesta, "7/RECIBIR/%s", Usuario);
				printf("Respuesta: %s\n", Respuesta);
				write(SocketContrincante, Respuesta, strlen(Respuesta));
			}
			else if (strcmp(Gestion, "ACEPTAR") == 0) //El usuario quiere aceptar un duelo recibido
			{
				sprintf(Respuesta, "7/ACEPTADO/%s", Usuario);
				write(SocketContrincante, Respuesta, strlen(Respuesta));
				CrearPartida(&ListaPartidasPreparadas, UsuarioContrincante, Usuario);
			}
			else if (strcmp(Gestion, "RECHAZAR") == 0) //El usuario quiere rechazar un duelo recibido
			{
				sprintf(Respuesta, "7/RECHAZADO/%s", Usuario);
				write(SocketContrincante, Respuesta, strlen(Respuesta));
			}
			
			else if (strcmp(Gestion, "EMPEZAR") == 0) //El usuario quiere empezar el duelo creado
			{
				sprintf(Respuesta, "7/EMPEZAR/%s", Usuario);
				write(SocketContrincante, Respuesta, strlen(Respuesta));
			}
		}
		break;
		
		case 8: //Peticion para enviar un mensaje por el chat
		{
			char UsuarioContrincante[80];
			p = strtok(NULL, "/");
			strcpy (UsuarioContrincante, p); // Ya tenemos el usuario para enviar el mensaje
			
			printf ("Codigo: %d, Nombre: %s\n", CodigoConsulta, UsuarioContrincante);
			
			p = strtok(NULL, "/");
			char MensajeAEnviar[900];
			strcpy (MensajeAEnviar, p); // Ya tenemos el mensaje para enviar por el chat
			
			int SocketContrincante = BuscarSocketJugador(&ListaUsuariosConectados, UsuarioContrincante);
			
			sprintf(Respuesta, "8/%s/%s", Usuario, MensajeAEnviar);
			printf ("Respuesta: %s\n", Respuesta);
			write (SocketContrincante, Respuesta, strlen(Respuesta));
		}
		break;
		
		case 9: //Peticion para guardar los datos de la partida
		{
			p = strtok(NULL, "/");
			int PuntosLocal = atoi (p); //Obtenemos los puntos del jugador local
			p = strtok(NULL, "/");
			int PuntosContricante = atoi (p); //Obtenemos los puntos del jugador visitante
			//Buscar comando para obtener el numero total de partidas guardadas en la base de datos (PARA HACER)
			
			strcpy (ConsultaResultante,"SELECT COUNT (*) FROM Partida");
			GuardarPartida (conn, Usuario, PuntosLocal, UsuarioContrincante, PuntosContrincante);
			
		}
		break;
		
		case 10: //Peticion para pasar los datos de posicion del jugador al contrincante
		{
			//Obtenemos las variables de posicion del jugador
			p = strtok(NULL, "/");
			int PosicionX = atoi (p);
			p = strtok(NULL, "/");
			int PosicionY = atoi (p);
			
			//Buscamos el socket del contrincante para poder saber a quien le debemos de enviar la informacion
			int SocketContrincante = BuscarSocketJugador(&ListaUsuariosConectados, UsuarioContrincante);
			
			//Preparamos el string con el mensaje resultante
			sprintf(Respuesta, "10/%d/%d", PosicionX, PosicionY);
			write(SocketContrincante, Respuesta, strlen(Respuesta));
		}
		break;
		}
	}
	mysql_close (conn);
	
	//Indicamos que el socket utilizado para este thread se libera
	for(int i = 0; i < 100; i++)
	{
		if (sockets[i] == sock_conn)
		{
			sockets[i] = -1;
		}
	}
	SocketsOcupados = SocketsOcupados - 1;
	close(sock_conn);
}

int main(int argc, char *argv[])
{
	int sock_conn, sock_listen;
	struct sockaddr_in serv_adr;
	
	//Abrimos el socket
	if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0)
		printf("Error creando el socket\n");
	
	
	//Hacemos bind al puerto
	memset(&serv_adr, 0, sizeof(serv_adr)); //Inicializa a cero serv_addr
	serv_adr.sin_family = AF_INET;
	
	//Asocia el socket a cualquiera de las IP de la maquina y 
	//htonl formatea el numero que recibe al formato necesario
	serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
	//Establecemos el puerto de escucha
	serv_adr.sin_port = htons(50008);
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
		printf ("Error al bind\n");
	
	if (listen(sock_listen, 3) < 0)
		printf("Error en el listen\n");
	
	//Ponemos todos los sockets con valor -1 para indicar que estan libres
	for (int i = 0; i < 100; i++)
	{
		sockets[i] = -1;
	}
	
	//Ponemos todos las partidas activas con valor -1 para indicar que sus identificadores estan libres
	for (int i = 0; i < 50; i++)
	{
		ListaPartidasPreparadas.PartidasActivas[i].NumeroPartida = -1;
	}
	
	pthread_t thread;
	SocketsOcupados = 0;
	
	//Hacemos un bucle infinito para cargar los threads a medida que se van creando
	for (;;)
	{
		printf ("Esperando conexion...\n");
		
		sock_conn = accept(sock_listen, NULL, NULL);
		printf ("Cliente conectado\n");
		
		//Buscamos un socket que este vacio para poder meter al siguiente cliente en dicho hueco
		if (SocketsOcupados < 100)
		{
			SocketVacioEncontrado = 0;
			PosicionSocket = 0;
			
			while (SocketVacioEncontrado == 0)
			{
				if (sockets[PosicionSocket] == -1)
				{
					//sock_conn es el socket que usaremos para este cliente
					sockets[PosicionSocket] = sock_conn;
					
					// Crear thread y decirle lo que tiene que hacer
					pthread_create (&thread, NULL, AtenderCliente, &sockets[PosicionSocket]);
					SocketsOcupados=SocketsOcupados+1;
					SocketVacioEncontrado = 1;
				}
				
				PosicionSocket = PosicionSocket + 1;
			}
		}
		else
		{
			printf("Se ha superado el límite de conexiones permitidas de manera simultanea\n");
		}
	}
}
