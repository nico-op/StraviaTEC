

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
   @NombreUsuario VARCHAR(15)
AS
BEGIN
   BEGIN TRY
       IF NOT EXISTS (SELECT 1 FROM Usuario WHERE NombreUsuario = @NombreUsuario)
       BEGIN
           PRINT 'Usuario no encontrado';
       END
       ELSE
       BEGIN
           SELECT * FROM Usuario WHERE NombreUsuario = @NombreUsuario;
       END
   END TRY
   BEGIN CATCH
       RETURN -1; -- Puedes ajustar este valor de retorno seg�n tus necesidades.
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
    @NombreUsuario VARCHAR(15),
    @Contrasena VARCHAR(15)
AS
BEGIN
    -- Validaci�n: Verificar si el usuario ya existe
    IF EXISTS (SELECT 1 FROM Usuario WHERE NombreUsuario = @NombreUsuario)
    BEGIN
        PRINT 'Error: El nombre de usuario ya existe. Elija otro.';
        RETURN;
    END

    -- Inserci�n si el usuario no existe
    INSERT INTO Usuario (Nombre, Apellido1, Apellido2, Fecha_nacimiento, Nacionalidad, Foto, NombreUsuario, Contrasena)
    VALUES (@Nombre, @Apellido1, @Apellido2, @FechaNacimiento, @Nacionalidad, @Foto, @NombreUsuario, @Contrasena);

    PRINT 'Usuario insertado exitosamente.';
END;

-- METODO PUT
CREATE PROCEDURE ActualizarUsuario
    @NombreUsuario VARCHAR(15),
    @Nombre VARCHAR(20),
    @Apellido1 VARCHAR(20),
    @Apellido2 VARCHAR(20),
    @FechaNacimiento DATE,
    @Nacionalidad VARCHAR(20),
    @Foto VARCHAR(250),
    @Contrasena VARCHAR(15)
AS
BEGIN
    -- Validaci�n: Verificar si el usuario existe antes de la actualizaci�n
    IF NOT EXISTS (SELECT 1 FROM Usuario WHERE NombreUsuario = @NombreUsuario)
    BEGIN
        PRINT 'Error: El usuario no existe. No se puede actualizar.';
        RETURN; -- Detener la ejecuci�n si el usuario no existe
    END

    -- Actualizaci�n del usuario si existe
    UPDATE Usuario
    SET Nombre = @Nombre,
        Apellido1 = @Apellido1,
        Apellido2 = @Apellido2,
        Fecha_nacimiento = @FechaNacimiento,
        Nacionalidad = @Nacionalidad,
        Foto = @Foto,
        Contrasena = @Contrasena
    WHERE NombreUsuario = @NombreUsuario;

    PRINT 'Usuario actualizado exitosamente.';
END;




-- METODO DELETE
CREATE PROCEDURE EliminarUsuario
    @NombreUsuario VARCHAR(15)
AS
BEGIN
    -- Validaci�n: Verificar si el usuario existe antes de la eliminaci�n
    IF NOT EXISTS (SELECT 1 FROM Usuario WHERE NombreUsuario = @NombreUsuario)
    BEGIN
        PRINT 'Error: El usuario no existe. No se puede eliminar.';
        RETURN; -- Detener la ejecuci�n si el usuario no existe
    END

    -- Eliminaci�n del usuario si existe
    DELETE FROM Usuario
    WHERE NombreUsuario = @NombreUsuario;

    PRINT 'Usuario eliminado exitosamente.';
END;


----------------------------------------------

-- Stored Procedure de ACTIVIDAD
-- METODO POST para Insertar Actividad
CREATE PROCEDURE InsertarActividad
    @TipoActividad VARCHAR(20),
    @Kilometraje INT,
    @Altitud INT,
    @Ruta VARCHAR(20),
    @FechaHora DATE,
    @Duracion INT,
    @NombreUsuario VARCHAR(15)
AS
BEGIN
    -- Validaci�n: Verificar si el usuario existe antes de insertar la actividad
    IF NOT EXISTS (SELECT 1 FROM Usuario WHERE NombreUsuario = @NombreUsuario)
    BEGIN
        PRINT 'Error: El nombre de usuario no existe. Inserte un usuario v�lido.';
        RETURN;
    END

    -- Inserci�n de la actividad si el usuario existe
    INSERT INTO Actividad (TipoActividad, Kilometraje, Altitud, Ruta, FechaHora, Duracion, NombreUsuario)
    VALUES (@TipoActividad, @Kilometraje, @Altitud, @Ruta, @FechaHora, @Duracion, @NombreUsuario);

    PRINT 'Actividad insertada exitosamente.';
END;



-- Procedimiento almacenado para consultar las actividades de un usuario
CREATE PROCEDURE ConsultarActividadesPorUsuario
    @NombreUsuario VARCHAR(15)
AS
BEGIN
    -- Validar si el usuario existe
    IF NOT EXISTS (SELECT 1 FROM Usuario WHERE NombreUsuario = @NombreUsuario)
    BEGIN
        -- Si el usuario no existe, imprimir un mensaje de error
        PRINT 'Error: El usuario no existe. Proporcione un nombre de usuario v�lido.';
        RETURN;
    END

    -- Consulta para seleccionar las actividades de un usuario
    SELECT TipoActividad, Kilometraje, Altitud, Ruta, FechaHora, Duracion, ActividadID
    FROM Actividad
    WHERE NombreUsuario = @NombreUsuario;
END

-- Delete Actividad por ActividadID
CREATE PROCEDURE EliminarActividadPorID
    @ActividadID INT
AS
BEGIN
    -- Verificar si la actividad existe
    IF EXISTS (SELECT 1 FROM Actividad WHERE ActividadID = @ActividadID)
    BEGIN
        -- Eliminar la actividad por ActividadID
        DELETE FROM Actividad WHERE ActividadID = @ActividadID;
        
        -- Devolver un mensaje de �xito
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
    @FechaHora DATETIME,
    @Duracion INT,
    @NombreUsuario VARCHAR(15)
AS
BEGIN
    -- Verificar si la actividad existe
    IF EXISTS (SELECT 1 FROM Actividad WHERE ActividadID = @ActividadID)
    BEGIN
        -- Verificar si el usuario existe
        IF NOT EXISTS (SELECT 1 FROM Usuario WHERE NombreUsuario = @NombreUsuario)
        BEGIN
            -- Si el usuario no existe, imprimir un mensaje de error
            PRINT 'Error: El usuario no existe. Proporcione un nombre de usuario v�lido.';
            RETURN;
        END

        -- Actualizar la actividad
        UPDATE Actividad
        SET
            TipoActividad = @TipoActividad,
            Kilometraje = @Kilometraje,
            Altitud = @Altitud,
            Ruta = @Ruta,
            FechaHora = @FechaHora,
            Duracion = @Duracion,
            NombreUsuario = @NombreUsuario
        WHERE ActividadID = @ActividadID;
        
        -- Devolver un mensaje de �xito
        PRINT 'La actividad ha sido actualizada correctamente.';
    END
    ELSE
    BEGIN
        -- La actividad no existe, devolver un mensaje de error
        PRINT 'La actividad que intentas actualizar no existe.';
    END
END;




----------------------------------------------
-- Stored Procedures de Carrera 

--obtener 
CREATE PROCEDURE ObtenerCarrera
    @nombreCarrera VARCHAR(20)
AS
BEGIN
    -- Verificar si la carrera existe
    IF EXISTS (SELECT 1 FROM Carrera WHERE NombreCarrera = @nombreCarrera)
    BEGIN
        -- La carrera existe, obtener la informaci�n de la carrera
        SELECT Costo, Modalidad, FechaCarrera, Recorrido, NombreCarrera
        FROM Carrera
        WHERE NombreCarrera = @nombreCarrera;
    END
    ELSE
    BEGIN
        -- La carrera no existe, devolver un mensaje indicando que no fue encontrada
        PRINT 'La carrera con el nombre especificado no fue encontrada.';
    END
END;

--Insertar 
CREATE PROCEDURE InsertarCarrera
    @costo INT,
    @modalidad VARCHAR(20),
    @fechaCarrera DATE,
    @recorrido VARCHAR(20),
    @nombreCarrera VARCHAR(20)
AS
BEGIN
    -- Verificar si la carrera ya existe
    IF NOT EXISTS (SELECT 1 FROM Carrera WHERE NombreCarrera = @nombreCarrera)
    BEGIN
        -- Insertar la nueva carrera
        INSERT INTO Carrera (Costo, Modalidad, FechaCarrera, Recorrido, NombreCarrera)
        VALUES (@costo, @modalidad, @fechaCarrera, @recorrido, @nombreCarrera);
        
        PRINT 'La carrera ha sido insertada correctamente.';
    END
    ELSE
    BEGIN
        -- La carrera ya existe, devolver un mensaje indicando que no se puede insertar
        PRINT 'La carrera con el nombre especificado ya existe. No se puede insertar.';
    END
END;

--eliminar
CREATE PROCEDURE EliminarCarrera
    @nombreCarrera VARCHAR(20)
AS
BEGIN
    -- Verificar si la carrera existe
    IF EXISTS (SELECT 1 FROM Carrera WHERE NombreCarrera = @nombreCarrera)
    BEGIN
        -- Eliminar la carrera
        DELETE FROM Carrera WHERE NombreCarrera = @nombreCarrera;
        PRINT 'La carrera ha sido eliminada correctamente.';
    END
    ELSE
    BEGIN
        -- La carrera no existe, devolver un mensaje indicando que no fue encontrada
        PRINT 'La carrera con el nombre especificado no fue encontrada. No se realiz� ninguna operaci�n de eliminaci�n.';
    END
END;
----------------------------------------------


