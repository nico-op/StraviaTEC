

-- Stored Procedures
----------------------- USUARIO ---------------------------------------------------------
-- Stored Procedure CRUD para la tabla Usuario
CREATE PROCEDURE CrudUsuario
    @Operacion VARCHAR(10),
    @Nombre VARCHAR(20) = NULL,
    @Apellido1 VARCHAR(20) = NULL,
    @Apellido2 VARCHAR(20) = NULL,
    @Fecha_nacimiento DATE = NULL,
    @Fecha_actual DATETIME = NULL,
    @Nacionalidad VARCHAR(20) = NULL,
    @Foto VARCHAR(250) = NULL,
    @NombreUsuario VARCHAR(15) = NULL,
    @Contrasena VARCHAR(15) = NULL
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        IF @Operacion = 'SELECT'
        BEGIN
            SELECT 
                Nombre, 
                Apellido1, 
                Apellido2, 
                Fecha_nacimiento, 
                Fecha_actual, 
                Nacionalidad, 
                Foto, 
                NombreUsuario, 
                Contrasena, 
                Edad 
            FROM Usuario;
        END
        ELSE IF @Operacion = 'SELECT ONE'
        BEGIN
            SELECT 
                Nombre, 
                Apellido1, 
                Apellido2, 
                Fecha_nacimiento, 
                Fecha_actual, 
                Nacionalidad, 
                Foto, 
                NombreUsuario, 
                Contrasena, 
                Edad 
            FROM Usuario
            WHERE NombreUsuario = @NombreUsuario;
        END
        ELSE IF @Operacion = 'INSERT'
        BEGIN
            INSERT INTO Usuario (
                Nombre, 
                Apellido1, 
                Apellido2, 
                Fecha_nacimiento, 
                Fecha_actual, 
                Nacionalidad, 
                Foto, 
                NombreUsuario, 
                Contrasena
            ) VALUES (
                @Nombre, 
                @Apellido1, 
                @Apellido2, 
                @Fecha_nacimiento, 
                @Fecha_actual, 
                @Nacionalidad, 
                @Foto, 
                @NombreUsuario, 
                @Contrasena
            );
            PRINT 'Usuario registrado exitosamente.';
        END
        ELSE IF @Operacion = 'UPDATE'
        BEGIN
            UPDATE Usuario
            SET 
                Nombre = @Nombre, 
                Apellido1 = @Apellido1, 
                Apellido2 = @Apellido2, 
                Fecha_nacimiento = @Fecha_nacimiento, 
                Fecha_actual = @Fecha_actual, 
                Nacionalidad = @Nacionalidad, 
                Foto = @Foto,
                Contrasena = @Contrasena
            WHERE NombreUsuario = @NombreUsuario;
            PRINT 'Usuario actualizado exitosamente.';
        END
        ELSE IF @Operacion = 'DELETE'
        BEGIN
            DELETE FROM Usuario
            WHERE NombreUsuario = @NombreUsuario;
            PRINT 'Usuario eliminado exitosamente.';
        END
        ELSE
        BEGIN
            -- Operación no válida
            ROLLBACK;
            PRINT 'Error: Operación no válida.';
            RETURN;
        END

        COMMIT; -- Confirmar la transacción
    END TRY
    BEGIN CATCH
        ROLLBACK;
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();
    END CATCH;
END;
GO




------------------------------- ACTIVIDAD -----------------------------------------
-- CRUD para la tabla Actividad
CREATE PROCEDURE CrudActividad
    @Operacion VARCHAR(10),
    @ActividadID INT = NULL,
    @TipoActividad VARCHAR(20) = NULL,
    @Kilometraje INT = NULL,
    @Altitud INT = NULL,
    @Ruta VARCHAR(20) = NULL,
    @FechaHora DATETIME = NULL,
    @Duracion INT = NULL,
    @NombreUsuario VARCHAR(15) = NULL
AS
BEGIN
    BEGIN TRANSACTION;

    IF @Operacion = 'INSERT'
    BEGIN
        -- Insertar nueva actividad
        INSERT INTO Actividad (TipoActividad, Kilometraje, Altitud, Ruta, FechaHora, Duracion, NombreUsuario)
        VALUES (@TipoActividad, @Kilometraje, @Altitud, @Ruta, @FechaHora, @Duracion, @NombreUsuario);
        
        PRINT 'Actividad insertada exitosamente.';
    END
    ELSE IF @Operacion = 'SELECT ONE'
    BEGIN
        -- Seleccionar una actividad por ID
        SELECT * FROM Actividad WHERE ActividadID = @ActividadID;
    END
    ELSE IF @Operacion = 'SELECT'
    BEGIN
        -- Seleccionar todas las actividades
        SELECT * FROM Actividad;
    END
    ELSE IF @Operacion = 'UPDATE'
    BEGIN
        -- Verificar si la actividad existe antes de actualizar
        IF NOT EXISTS (SELECT 1 FROM Actividad WHERE ActividadID = @ActividadID)
        BEGIN
            ROLLBACK;
            PRINT 'Error: La actividad que intentas actualizar no existe.';
            RETURN;
        END

        -- Actualizar la actividad
        UPDATE Actividad
        SET
            TipoActividad = COALESCE(@TipoActividad, TipoActividad),
            Kilometraje = COALESCE(@Kilometraje, Kilometraje),
            Altitud = COALESCE(@Altitud, Altitud),
            Ruta = COALESCE(@Ruta, Ruta),
            FechaHora = COALESCE(@FechaHora, FechaHora),
            Duracion = COALESCE(@Duracion, Duracion),
            NombreUsuario = COALESCE(@NombreUsuario, NombreUsuario)
        WHERE ActividadID = @ActividadID;
        
        PRINT 'Actividad actualizada exitosamente.';
    END
    ELSE IF @Operacion = 'DELETE'
    BEGIN
        -- Eliminar la actividad
        DELETE FROM Actividad WHERE ActividadID = @ActividadID;
        
        PRINT 'Actividad eliminada exitosamente.';
    END
    ELSE
    BEGIN
        -- Operación no válida
        ROLLBACK;
        PRINT 'Error: Operación no válida.';
        RETURN;
    END

    COMMIT; -- Confirmar la transacción
END;
GO


---------------------------- CARRERA -------------------------
CREATE PROCEDURE CrudCarrera
    @Operacion VARCHAR(10),
    @Costo INT = NULL,
    @Modalidad VARCHAR(20) = NULL,
    @FechaCarrera DATE = NULL,
    @Recorrido VARCHAR(20) = NULL,
    @NombreCarrera VARCHAR(20) = NULL
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        IF @Operacion = 'SELECT'
        BEGIN
            SELECT 
                Costo, 
                Modalidad, 
                FechaCarrera, 
                Recorrido, 
                NombreCarrera
            FROM Carrera;
        END
        ELSE IF @Operacion = 'SELECT ONE'
        BEGIN
            SELECT 
                Costo, 
                Modalidad, 
                FechaCarrera, 
                Recorrido, 
                NombreCarrera
            FROM Carrera
            WHERE NombreCarrera = @NombreCarrera;
        END
        ELSE IF @Operacion = 'INSERT'
        BEGIN
            INSERT INTO Carrera (
                Costo, 
                Modalidad, 
                FechaCarrera, 
                Recorrido, 
                NombreCarrera
            ) VALUES (
                @Costo, 
                @Modalidad, 
                @FechaCarrera, 
                @Recorrido, 
                @NombreCarrera
            );
            PRINT 'Carrera registrada exitosamente.';
        END
        ELSE IF @Operacion = 'UPDATE'
        BEGIN
            UPDATE Carrera
            SET 
                Costo = @Costo, 
                Modalidad = @Modalidad, 
                FechaCarrera = @FechaCarrera, 
                Recorrido = @Recorrido
            WHERE NombreCarrera = @NombreCarrera;
            PRINT 'Carrera actualizada exitosamente.';
        END
        ELSE IF @Operacion = 'DELETE'
        BEGIN
            DELETE FROM Carrera
            WHERE NombreCarrera = @NombreCarrera;
            PRINT 'Carrera eliminada exitosamente.';
        END
        ELSE
        BEGIN
            -- Operación no válida
            ROLLBACK;
            PRINT 'Error: Operación no válida.';
            RETURN;
        END

        COMMIT; -- Confirmar la transacción
    END TRY
    BEGIN CATCH
        ROLLBACK;
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();
    END CATCH;
END;
GO



--------------------------- RETO --------------------------
CREATE PROCEDURE CrudReto
    @Operacion VARCHAR(10),
    @NombreReto VARCHAR(20) = NULL , -- Hacer que @NombreReto sea opcional
    @Privacidad VARCHAR(20) = NULL, -- Hacer que @Privacidad sea opcional
    @Periodo INT = NULL, -- Hacer que @Periodo sea opcional
    @TipoActividad VARCHAR(20) = NULL, -- Hacer que @TipoActividad sea opcional
    @Altitud VARCHAR(2) = NULL, -- Hacer que @Altitud sea opcional
    @Fondo VARCHAR(2) = NULL -- Hacer que @Fondo sea opcional
AS
BEGIN
    BEGIN TRANSACTION; -- Iniciar la transacción

    IF @Operacion = 'INSERT'
    BEGIN
        -- Validar si el nombre del reto ya existe
        IF EXISTS (SELECT 1 FROM Reto WHERE NombreReto = @NombreReto)
        BEGIN
            PRINT 'Error: Ya existe un reto con ese nombre.';
            ROLLBACK; -- Deshacer la transacción
            RETURN;
        END

        -- Insertar nuevo reto
        INSERT INTO Reto (Privacidad, Periodo, TipoActividad, Altitud, Fondo, NombreReto)
        VALUES (@Privacidad, @Periodo, @TipoActividad, @Altitud, @Fondo, @NombreReto);
        
        PRINT 'Reto insertado exitosamente.';
    END
	ELSE IF @Operacion = 'SELECT'
		BEGIN
			SELECT * FROM Reto
		END 
    ELSE IF @Operacion = 'SELECT ONE'
    BEGIN
        -- Seleccionar un reto por nombre
        SELECT * FROM Reto WHERE NombreReto = @NombreReto;
    END

    ELSE IF @Operacion = 'UPDATE'
    BEGIN
        -- Validar si el reto existe antes de actualizar
        IF NOT EXISTS (SELECT 1 FROM Reto WHERE NombreReto = @NombreReto)
        BEGIN
            PRINT 'Error: El reto que intentas actualizar no existe.';
            ROLLBACK; -- Deshacer la transacción
            RETURN;
        END

        -- Actualizar el reto
        UPDATE Reto
        SET Privacidad = @Privacidad,
            Periodo = @Periodo,
            TipoActividad = @TipoActividad,
            Altitud = @Altitud,
            Fondo = @Fondo
        WHERE NombreReto = @NombreReto;
        
        PRINT 'Reto actualizado exitosamente.';
    END
    ELSE IF @Operacion = 'DELETE'
    BEGIN
        -- Eliminar el reto
        DELETE FROM Reto WHERE NombreReto = @NombreReto;
        
        PRINT 'Reto eliminado exitosamente.';
    END
    ELSE
    BEGIN
        -- Operación no válida
        PRINT 'Error: Operación no válida.';
        ROLLBACK; -- Deshacer la transacción
        RETURN;
    END

    COMMIT; -- Confirmar la transacción
END;
GO


-------------------------------------- GRUPO -------------------------------------------
CREATE PROCEDURE CrudGrupo
    @Operacion VARCHAR(10),
    @NombreGrupo VARCHAR(20) = NULL,
    @Descripcion VARCHAR(50) = NULL,
    @Administrador VARCHAR(20) = NULL,
    @Creacion DATE = NULL,
    @GrupoID VARCHAR(20) = NULL
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        IF @Operacion = 'SELECT'
        BEGIN
            SELECT 
                NombreGrupo, 
                Descripcion, 
                Administrador, 
                Creacion, 
                GrupoID
            FROM Grupo;
        END
        ELSE IF @Operacion = 'SELECT ONE'
        BEGIN
            SELECT 
                NombreGrupo, 
                Descripcion, 
                Administrador, 
                Creacion, 
                GrupoID
            FROM Grupo
            WHERE GrupoID = @GrupoID;
        END
        ELSE IF @Operacion = 'INSERT'
        BEGIN
            INSERT INTO Grupo (
                NombreGrupo, 
                Descripcion, 
                Administrador, 
                Creacion, 
                GrupoID
            ) VALUES (
                @NombreGrupo, 
                @Descripcion, 
                @Administrador, 
                @Creacion, 
                @GrupoID
            );
            PRINT 'Grupo registrado exitosamente.';
        END
        ELSE IF @Operacion = 'UPDATE'
        BEGIN
            UPDATE Grupo
            SET 
                NombreGrupo = @NombreGrupo, 
                Descripcion = @Descripcion, 
                Administrador = @Administrador, 
                Creacion = @Creacion
            WHERE GrupoID = @GrupoID;
            PRINT 'Grupo actualizado exitosamente.';
        END
        ELSE IF @Operacion = 'DELETE'
        BEGIN
            DELETE FROM Grupo
            WHERE GrupoID = @GrupoID;
            PRINT 'Grupo eliminado exitosamente.';
        END
        ELSE
        BEGIN
            -- Operación no válida
            ROLLBACK;
            PRINT 'Error: Operación no válida.';
            RETURN;
        END

        COMMIT; -- Confirmar la transacción
    END TRY
    BEGIN CATCH
        ROLLBACK;
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        RAISEERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;
















--- ELIMINAR TODOS LOS PROCEDIMIENTOS ALMACENADOS
--DECLARE @ProcedureName NVARCHAR(128)
--DECLARE ProcedureCursor CURSOR FOR
--SELECT ROUTINE_NAME
--FROM INFORMATION_SCHEMA.ROUTINES
--WHERE ROUTINE_TYPE = 'PROCEDURE'

--OPEN ProcedureCursor

--FETCH NEXT FROM ProcedureCursor INTO @ProcedureName
--WHILE @@FETCH_STATUS = 0
--BEGIN
--    DECLARE @DropStatement NVARCHAR(MAX)
--    SET @DropStatement = 'DROP PROCEDURE ' + @ProcedureName
--    EXEC sp_executesql @DropStatement

--    FETCH NEXT FROM ProcedureCursor INTO @ProcedureName
--END

--CLOSE ProcedureCursor
--DEALLOCATE ProcedureCursor

