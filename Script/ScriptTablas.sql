
-- Tabla Usuario
CREATE TABLE Usuario(
    Nombre VARCHAR(20) NOT NULL,
    Apellido1 VARCHAR(20) NOT NULL,
    Apellido2 VARCHAR(20),
    Fecha_nacimiento DATE NOT NULL,
    Fecha_actual DATETIME, -- Cambiado a DATETIME
    Nacionalidad VARCHAR(20) NOT NULL,
    Foto VARCHAR(250),
    NombreUsuario VARCHAR(15) NOT NULL, 
    Contrasena VARCHAR(15) NOT NULL,
    PRIMARY KEY(NombreUsuario),
    UNIQUE (Contrasena),
    Edad AS DATEDIFF(YEAR, Fecha_nacimiento, GETDATE()) 
);



-- Tabla Amigo
CREATE TABLE Amigo (
  UsuarioOrigen VARCHAR(15) NOT NULL,
  UsuarioDestino VARCHAR(15) NOT NULL,
  PRIMARY KEY (UsuarioOrigen, UsuarioDestino),
  FOREIGN KEY (UsuarioOrigen) REFERENCES Usuario(NombreUsuario),
  FOREIGN KEY (UsuarioDestino) REFERENCES Usuario(NombreUsuario)
);


--Actividad 
CREATE TABLE Actividad (
	ActividadID INT IDENTITY(1,1) NOT NULL,
	TipoActividad VARCHAR(20) NOT NULL, 
	Kilometraje INT NOT NULL, 
	Altitud INT NOT NULL, 
	Ruta VARCHAR(20) NOT NULL, 
	FechaHora DATETIME NOT NULL, 
	Duracion INT NOT NULL, 
	NombreUsuario VARCHAR(15) NOT NULL,
	PRIMARY KEY(ActividadID),
	FOREIGN KEY (NombreUsuario) REFERENCES Usuario(NombreUsuario) 
);



--Reto 
CREATE TABLE Reto (
	Privacidad VARCHAR(20) NOT NULL,
	Periodo INT NOT NULL, 
	TipoActividad VARCHAR(20) NOT NULL, 
	Altitud VARCHAR(2),
	Fondo VARCHAR(2),
	NombreReto VARCHAR(20) NOT NULL,
	PRIMARY KEY(NombreReto)
);


--Carrera
CREATE TABLE Carrera(
	Costo int NOT NULL, 
	Modalidad VARCHAR(20) NOT NULL,
	FechaCarrera DATE NOT NULL,
	Recorrido VARCHAR(20) NOT NULL,
	NombreCarrera VARCHAR(20) NOT NULL,
	PRIMARY KEY (NombreCarrera)
);


--Patrocinador 
CREATE TABLE Patrocinador(
	NombreLegal VARCHAR(20) NOT NULL, 
	Logo VARCHAR(250),
	Telefono INT,
	NombreComercial VARCHAR(20) NOT NULL,
	PRIMARY KEY(NombreComercial)
);



--Grupo
CREATE TABLE Grupo( 
	NombreGrupo VARCHAR(20) NOT NULL,
	Descripcion VARCHAR(50),
	Administrador VARCHAR(20) NOT NULL,
	Creacion DATE,
	GrupoID VARCHAR(20) NOT NULL, 
	PRIMARY KEY (GrupoID),
	UNIQUE(NombreGrupo)
);




--Comentario 
CREATE TABLE Comentario(
	Contenido VARCHAR(100) NOT NULL,
	FechaPublicacion DATE,
	NombreUsuario VARCHAR(15),
	ActividadID INT,
	ComentarioID INT PRIMARY KEY NOT NULL,
	FOREIGN KEY (NombreUsuario) REFERENCES Usuario(NombreUsuario),
	FOREIGN KEY (ActividadID) REFERENCES Actividad(ActividadID)
);



--Categoria 
CREATE TABLE Categoria (
    NombreCategoria VARCHAR(20) NOT NULL,
    DescripcionCategoria VARCHAR(100),
    NombreCarrera VARCHAR(20) NOT NULL,
    PRIMARY KEY (NombreCategoria, NombreCarrera),
    FOREIGN KEY (NombreCarrera) REFERENCES Carrera(NombreCarrera)
);


--Usuarios por reto 
CREATE TABLE UsuariosPorReto (
    NombreUsuario VARCHAR(15),
    NombreReto VARCHAR(20),
    PRIMARY KEY (NombreUsuario, NombreReto),
    FOREIGN KEY (NombreUsuario) REFERENCES Usuario(NombreUsuario),
    FOREIGN KEY (NombreReto) REFERENCES Reto(NombreReto)
);

SELECT * FROM Reto;
--UsuariosPorCarrera
CREATE TABLE UsuariosPorCarrera (
    NombreUsuario VARCHAR(15),
    NombreCarrera VARCHAR(20),
    PRIMARY KEY (NombreUsuario, NombreCarrera),
    FOREIGN KEY (NombreUsuario) REFERENCES Usuario(NombreUsuario),
    FOREIGN KEY (NombreCarrera) REFERENCES Carrera(NombreCarrera)
);

--UsuariosPorGrupo
CREATE TABLE UsuariosPorGrupo(
    NombreUsuario VARCHAR(15),
    GrupoID VARCHAR(20),
    PRIMARY KEY (NombreUsuario, GrupoID),
    FOREIGN KEY (NombreUsuario) REFERENCES Usuario(NombreUsuario),
    FOREIGN KEY (GrupoID) REFERENCES Grupo(GrupoID)
);


--PatrocinadoresPorReto
CREATE TABLE PatrocinadoresPorReto(
    NombreReto VARCHAR(20),
    NombreComercial VARCHAR(20),
    PRIMARY KEY (NombreReto, NombreComercial),
    FOREIGN KEY (NombreReto) REFERENCES Reto(NombreReto),
    FOREIGN KEY (NombreComercial) REFERENCES Patrocinador(NombreComercial)
);

--PatrocinadoresPorCarrera
CREATE TABLE PatrocinadoresPorCarrera(
    NombreCarrera VARCHAR(20),
    NombreComercial VARCHAR(20),
    PRIMARY KEY (NombreCarrera, NombreComercial),
    FOREIGN KEY (NombreCarrera) REFERENCES Carrera(NombreCarrera),
    FOREIGN KEY (NombreComercial) REFERENCES Patrocinador(NombreComercial)
);

--CuentasBancarias 
CREATE TABLE CuentasBancarias (
    NombreCarrera VARCHAR(20) NOT NULL, 
    NombreBanco VARCHAR(50),
    NumeroCuenta VARCHAR(20),
    PRIMARY KEY (NombreCarrera, NumeroCuenta),
    FOREIGN KEY (NombreCarrera) REFERENCES Carrera(NombreCarrera)
);



