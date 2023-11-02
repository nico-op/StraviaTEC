 --Tabla Usuario 
CREATE TABLE Usuario (
    Nombre VARCHAR(20),
    Apellido1 VARCHAR(20),
    Apellido2 VARCHAR(20),
    Fecha_nacimiento DATE,
    Fecha_actual AS GETDATE(),
    Nacionalidad VARCHAR(20),
    Foto VARCHAR(250),
    Usuario VARCHAR(15),
	Contraseña VARCHAR(15),
	PRIMARY KEY(Usuario),
    UNIQUE (Contraseña),
    Edad AS DATEDIFF(YEAR, Fecha_nacimiento, GETDATE()) 
);


	--Actividad 
CREATE TABLE Actividad(
	TipoActividad VARCHAR(20), 
	Kilometraje INT, 
	Altitud INT, 
	Ruta VARCHAR(20), 
	Fecha DATE,
	Hora TIME, 
	Duracion TIME, 
	ActividadID INT,
	Usuario VARCHAR(15),
	PRIMARY KEY(ActividadID),
	FOREIGN KEY (Usuario) REFERENCES Usuario(Usuario), 
	);

	--Reto 
CREATE TABLE Reto (
	Privacidad VARCHAR(20),
	Periodo int, 
	TipoActividad VARCHAR(20), 
	Altitud VARCHAR(2),
	Fondo VARCHAR(2),
	NombreReto VARCHAR(20),
	PRIMARY KEY(NombreReto),
);


	--Carrera
CREATE TABLE Carrera(
	Costo int, 
	Modalidad VARCHAR(20),
	FechaCarrera DATE,
	Recorrido VARCHAR(20),
	NombreCarrera VARCHAR(20),
	PRIMARY KEY (NombreCarrera),
);


	--Patrocinador 
CREATE TABLE Patrocinador(
	NombreLegal VARCHAR(20), 
	Logo VARCHAR(250),
	Telefono INT,
	NombreComercial VARCHAR(20),
	PRIMARY KEY(NombreComercial),

);

	--Grupo
CREATE TABLE Grupo( 
	NombreGrupo VARCHAR(20),
	Descripcion VARCHAR(50),
	Administrador VARCHAR(20),
	Creacion DATE,
	GrupoID VARCHAR(20), 
	PRIMARY KEY (GrupoID),

);

	--Comentario 
CREATE TABLE Comentario(
	Contenido VARCHAR(100),
	FechaPublicacion DATE,
	Usuario VARCHAR(15),
	ActividadID INT,
	ComentarioID INT PRIMARY KEY,
	FOREIGN KEY (Usuario) REFERENCES Usuario(Usuario),
	FOREIGN KEY (ActividadID) REFERENCES Actividad(ActividadID),
);


	--Categoria 
CREATE TABLE Categoria (
    NombreCategoria VARCHAR(20),
    DescripcionCategoria VARCHAR(100),
    NombreCarrera VARCHAR(20),
    PRIMARY KEY (NombreCategoria, NombreCarrera),
    FOREIGN KEY (NombreCarrera) REFERENCES Carrera(NombreCarrera)
);


--Usuarios por reto 
CREATE TABLE UsuariosPorReto (
    Usuario VARCHAR(15),
    NombreReto VARCHAR(20),
    PRIMARY KEY (Usuario, NombreReto),
    FOREIGN KEY (Usuario) REFERENCES Usuario(Usuario),
    FOREIGN KEY (NombreReto) REFERENCES Reto(NombreReto)
);

	--UsuariosPorCarrera
CREATE TABLE UsuariosPorCarrera (
    Usuario VARCHAR(15),
    NombreCarrera VARCHAR(20),
    PRIMARY KEY (Usuario, NombreCarrera),
    FOREIGN KEY (Usuario) REFERENCES Usuario(Usuario),
    FOREIGN KEY (NombreCarrera) REFERENCES Carrera(NombreCarrera)
);

	--UsuariosPorGrupo
CREATE TABLE UsuariosPorGrupo(
    Usuario VARCHAR(15),
    GrupoID VARCHAR(20),
    PRIMARY KEY (Usuario, GrupoID),
    FOREIGN KEY (Usuario) REFERENCES Usuario(Usuario),
    FOREIGN KEY (GrupoID) REFERENCES Grupo(GrupoID),
);

	--PatrocinadoresPorReto
CREATE TABLE PatrocinadoresPorReto(
    NombreReto VARCHAR(20),
    NombreComercial VARCHAR(20),
    PRIMARY KEY (NombreReto, NombreComercial),
    FOREIGN KEY (NombreReto) REFERENCES Reto(NombreReto),
    FOREIGN KEY (NombreComercial) REFERENCES Patrocinador(NombreComercial),
);

	--PatrocinadoresPorReto
CREATE TABLE PatrocinadoresPorCarrera(
    NombreCarrera VARCHAR(20),
    NombreComercial VARCHAR(20),
    PRIMARY KEY (NombreCarrera, NombreComercial),
    FOREIGN KEY (NombreCarrera) REFERENCES Carrera(NombreCarrera),
    FOREIGN KEY (NombreComercial) REFERENCES Patrocinador(NombreComercial),
);

	--CuentasBancarias 
CREATE TABLE CuentasBancarias (
    NombreCarrera VARCHAR(20), 
    NombreBanco VARCHAR(50),
    NumeroCuenta VARCHAR(20),
    PRIMARY KEY (NombreCarrera, NumeroCuenta),
    FOREIGN KEY (NombreCarrera) REFERENCES Carrera(NombreCarrera)
);

