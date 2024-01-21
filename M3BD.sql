DROP DATABASE IF EXISTS M3BD;
CREATE DATABASE M3BD;
USE M3BD;
CREATE TABLE Jugador (
	Identificador INTEGER PRIMARY KEY NOT NULL,
	Nombre TEXT NOT NULL,
	Contrasena TEXT NOT NULL,
	Estado INTEGER NOT NULL
	)ENGINE = InnoDB;

INSERT INTO Jugador VALUES(1,'Juan','elloco123',1);
INSERT INTO Jugador VALUES(2,'Maria','amiquemecuentas',1);
INSERT INTO Jugador VALUES(3,'Pedro','contrasenasegura',1);
INSERT INTO Jugador VALUES(4,'Luis','xXeldiablillo67Xx',1);
INSERT INTO Jugador VALUES(5,'Julia','todocontrasena',1);

CREATE TABLE Partida (
	Identificador INTEGER PRIMARY KEY NOT NULL,
	FechaFinal TEXT NOT NULL,
	Ganador INTEGER NOT NULL,
	FOREIGN KEY (Ganador) REFERENCES Jugador(Identificador)
	)ENGINE = InnoDB;

INSERT INTO Partida VALUES(1,'12/01/1985 12:05',1);
INSERT INTO Partida VALUES(2,'29/02/2023 21:48',4);
INSERT INTO Partida VALUES(3,'31/08/2000 03:15',2);
INSERT INTO Partida VALUES(4,'31/08/2000 03:15',5);
INSERT INTO Partida VALUES(5,'01/01/1960 04:24',4);

CREATE TABLE Participacion (
	Jugador INTEGER NOT NULL,
	Partida INTEGER NOT NULL,
	Puntos INTEGER NOT NULL,
	FOREIGN KEY (Jugador) REFERENCES Jugador(Identificador),
	FOREIGN KEY (Partida) REFERENCES Partida(Identificador)
	)ENGINE = InnoDB;
							   

INSERT INTO Participacion VALUES(1,1,156);
INSERT INTO Participacion VALUES(3,1,76);
INSERT INTO Participacion VALUES(4,2,231);
INSERT INTO Participacion VALUES(1,2,43);
INSERT INTO Participacion VALUES(2,3,953);
INSERT INTO Participacion VALUES(5,3,653);
INSERT INTO Participacion VALUES(4,4,75);
INSERT INTO Participacion VALUES(5,4,257);
INSERT INTO Participacion VALUES(3,5,145);
INSERT INTO Participacion VALUES(2,5,76);

