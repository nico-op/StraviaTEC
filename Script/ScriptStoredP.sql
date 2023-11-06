

-- Stored Procedures
-- Usuario 
-- METODO GET ALL
CREATE PROCEDURE ObtenerTodosLosUsuarios
AS
BEGIN
    SELECT *
    FROM Usuario;
END;

--METODO GET

CREATE PROCEDURE ObtenerUsuario
    @Usuario VARCHAR(15)
AS
BEGIN
    SELECT *
    FROM Usuario
    WHERE Usuario = @Usuario;
END;

-- METODO POST
CREATE PROCEDURE InsertarUsuario
    @Nombre VARCHAR(20),
    @Apellido1 VARCHAR(20),
    @Apellido2 VARCHAR(20),
    @FechaNacimiento DATE,
    @Nacionalidad VARCHAR(20),
    @Foto VARCHAR(250),
    @Usuario VARCHAR(15),
    @Contraseņa VARCHAR(15)
AS
BEGIN
    INSERT INTO Usuario (Nombre, Apellido1, Apellido2, Fecha_nacimiento, Nacionalidad, Foto, Usuario, Contraseņa)
    VALUES (@Nombre, @Apellido1, @Apellido2, @FechaNacimiento, @Nacionalidad, @Foto, @Usuario, @Contraseņa);
END;

--METODO PUT
CREATE PROCEDURE ActualizarUsuario
    @Usuario VARCHAR(15),
    @Nombre VARCHAR(20),
    @Apellido1 VARCHAR(20),
    @Apellido2 VARCHAR(20),
    @FechaNacimiento DATE,
    @Nacionalidad VARCHAR(20),
    @Foto VARCHAR(250),
    @Contraseņa VARCHAR(15)
AS
BEGIN
    UPDATE Usuario
    SET Nombre = @Nombre,
        Apellido1 = @Apellido1,
        Apellido2 = @Apellido2,
        Fecha_nacimiento = @FechaNacimiento,
        Nacionalidad = @Nacionalidad,
        Foto = @Foto,
        Contraseņa = @Contraseņa
    WHERE Usuario = @Usuario;
END;

--METODO DELETE

CREATE PROCEDURE EliminarUsuario
    @Usuario VARCHAR(15)
AS
BEGIN
    DELETE FROM Usuario
    WHERE Usuario = @Usuario;
END;