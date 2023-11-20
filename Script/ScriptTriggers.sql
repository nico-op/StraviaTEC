-------------------- Triggers -------------------------
------------ Usuario --------------
-- Trigger: ActualizarFechaActual_Usuario
-- Funci�n: Realiza actualizacion de la fecha actual para actualizar la edad

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
-- Funci�n: Evita que la fecha sea antes de la fecha actual, es decir, no se puede crear una carrera en el pasado.

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
        ROLLBACK TRANSACTION; -- Deshacer la transacci�n para evitar la inserci�n o actualizaci�n
    END
END;
GO


-- Trigger: ValidarEdadUsuario
-- Funci�n:Evita que se coloque la fecha de nacimiento igual que la fecha actual

CREATE TRIGGER ValidarEdadUsuario
ON Usuario
AFTER INSERT, UPDATE
AS
BEGIN
    DECLARE @FechaNacimiento DATE, @FechaActual DATETIME;
    SELECT @FechaNacimiento = Fecha_nacimiento, @FechaActual = Fecha_actual FROM inserted;

    IF DATEDIFF(YEAR, @FechaNacimiento, @FechaActual) < 5
    BEGIN
        RAISERROR ('La diferencia entre la fecha de nacimiento y la fecha actual debe ser de al menos 10 a�os.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
END;
GO
