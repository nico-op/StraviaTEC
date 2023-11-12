

-- Stored Procedures
-- Los stored procedures deben englobar varias operaciones


-- USUARIO --

-- ACTIVIDAD --

-- CARRERA --

-- RETO --
ALTER PROCEDURE CrudReto
    @Operacion VARCHAR(10) = '',
    @NombreReto VARCHAR(20) , -- Hacer que @NombreReto sea opcional
    @Privacidad VARCHAR(20) , -- Hacer que @Privacidad sea opcional
    @Periodo INT , -- Hacer que @Periodo sea opcional
    @TipoActividad VARCHAR(20) , -- Hacer que @TipoActividad sea opcional
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



-- GRUPO --















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

