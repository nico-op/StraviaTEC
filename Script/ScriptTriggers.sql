-------------------- Triggers -------------------------
------------ Usuario --------------
-- Trigger: ActualizarFechaActual_Usuario
-- Función: Realiza actualizacion de la fecha actual para actualizar la edad

CREATE TRIGGER ActualizarFechaActual_Usuario
ON Usuario
AFTER INSERT, UPDATE
AS
BEGIN
    UPDATE Usuario
    SET Fecha_actual = GETDATE()
    FROM Usuario U
    INNER JOIN inserted I ON U.NombreUsuario = I.NombreUsuario;
END;
GO

-- Trigger: ValidarFechaCarrera
-- Función: Evita que la fecha sea antes de la fecha actual, es decir, no se puede crear una carrera en el pasado.

CREATE OR ALTER TRIGGER ValidarFechaCarrera
ON Carrera
AFTER INSERT, UPDATE
AS
BEGIN
    IF EXISTS (
        SELECT * 
        FROM inserted 
        WHERE FechaCarrera < GETDATE()
    )
    BEGIN
        RAISERROR('La fecha de la carrera no puede ser anterior a la fecha actual.', 16, 1)
        ROLLBACK TRANSACTION; -- Deshacer la transacción para evitar la inserción o actualización
    END
END;
GO


-- Trigger: ValidarEdadUsuario
-- Función:Evita que se coloque la fecha de nacimiento igual que la fecha actual

CREATE TRIGGER ValidarEdadUsuario
ON Usuario
AFTER INSERT, UPDATE
AS
BEGIN
    DECLARE @FechaNacimiento DATE, @FechaActual DATETIME;
    SELECT @FechaNacimiento = Fecha_nacimiento, @FechaActual = Fecha_actual FROM inserted;

    IF DATEDIFF(YEAR, @FechaNacimiento, @FechaActual) < 5
    BEGIN
        RAISERROR ('La diferencia entre la fecha de nacimiento y la fecha actual debe ser de al menos 10 años.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
END;
GO
