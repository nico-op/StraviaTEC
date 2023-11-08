

-- Stored Procedures

----------------------------------------------
-- Stored Procedures de Usuario 
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
    BEGIN TRY
        SELECT *
        FROM Usuario
        WHERE Usuario = @Usuario;
    END TRY
    BEGIN CATCH
		RETURN -1;
    END CATCH;
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
    @Contraseña VARCHAR(15)
AS
BEGIN
    INSERT INTO Usuario (Nombre, Apellido1, Apellido2, Fecha_nacimiento, Nacionalidad, Foto, Usuario, Contraseña)
    VALUES (@Nombre, @Apellido1, @Apellido2, @FechaNacimiento, @Nacionalidad, @Foto, @Usuario, @Contraseña);
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
    @Contraseña VARCHAR(15)
AS
BEGIN
    UPDATE Usuario
    SET Nombre = @Nombre,
        Apellido1 = @Apellido1,
        Apellido2 = @Apellido2,
        Fecha_nacimiento = @FechaNacimiento,
        Nacionalidad = @Nacionalidad,
        Foto = @Foto,
        Contraseña = @Contraseña
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

----------------------------------------------
-- Stored Procedures de Actividad

-- Post Actividad
-- Insertar una actividad
CREATE PROCEDURE InsertarActividad
    @TipoActividad VARCHAR(20),
    @Kilometraje INT,
    @Altitud INT,
    @Ruta VARCHAR(20),
    @Fecha DATE,
    @Hora TIME,
    @Duracion TIME,
    @Usuario VARCHAR(15)
AS
BEGIN
    -- Insertar la nueva actividad
    INSERT INTO Actividad (TipoActividad, Kilometraje, Altitud, Ruta, Fecha, Hora, Duracion, Usuario)
    VALUES (@TipoActividad, @Kilometraje, @Altitud, @Ruta, @Fecha, @Hora, @Duracion, @Usuario);

    -- Devolver un mensaje de éxito
    PRINT 'Nueva actividad insertada correctamente.';
END

--- Get Actividad
-- Procedimiento almacenado para consultar las actividades de un usuario
CREATE PROCEDURE ConsultarActividadesPorUsuario
    @Usuario VARCHAR(15)
AS
BEGIN
    -- Consulta para seleccionar las actividades de un usuario
    SELECT TipoActividad, Kilometraje, Altitud, Ruta, Fecha, Hora, Duracion, ActividadID
    FROM Actividad
    WHERE Usuario = @Usuario;
END

-- Delete Actividad
-- Procedimiento almacenado para eliminar una actividad
CREATE PROCEDURE EliminarActividad
    @ActividadID INT
AS
BEGIN
    -- Verificar si la actividad existe
    IF EXISTS (SELECT 1 FROM Actividad WHERE ActividadID = @ActividadID)
    BEGIN
        -- Eliminar la actividad
        DELETE FROM Actividad WHERE ActividadID = @ActividadID;
        
        -- Devolver un mensaje de éxito
        PRINT 'La actividad ha sido eliminada correctamente.';
    END
    ELSE
    BEGIN
        -- La actividad no existe, devolver un mensaje de error
        PRINT 'La actividad que intentas eliminar no existe.';
    END
END


-- Put Actividad
-- Procedimiento almacenado para actualizar una actividad
CREATE PROCEDURE ActualizarActividad
    @ActividadID INT,
    @TipoActividad VARCHAR(20),
    @Kilometraje INT,
    @Altitud INT,
    @Ruta VARCHAR(20),
    @Fecha DATE,
    @Hora TIME,
    @Duracion TIME
AS
BEGIN
    -- Verificar si la actividad existe
    IF EXISTS (SELECT 1 FROM Actividad WHERE ActividadID = @ActividadID)
    BEGIN
        -- Actualizar la actividad
        UPDATE Actividad
        SET
            TipoActividad = @TipoActividad,
            Kilometraje = @Kilometraje,
            Altitud = @Altitud,
            Ruta = @Ruta,
            Fecha = @Fecha,
            Hora = @Hora,
            Duracion = @Duracion
        WHERE ActividadID = @ActividadID;
        
        -- Devolver un mensaje de éxito
        PRINT 'La actividad ha sido actualizada correctamente.';
    END
    ELSE
    BEGIN
        -- La actividad no existe, devolver un mensaje de error
        PRINT 'La actividad que intentas actualizar no existe.';
    END
END




----------------------------------------------




----------------------------------------------