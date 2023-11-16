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

-- Trigger: EvitarUsuariosReplicados
-- Función: Evita que al momento de hacer un insert de Usuario, tenga un NombreUsuario replicado

CREATE TRIGGER EvitarUsuariosReplicados
ON Usuario
AFTER INSERT
AS
BEGIN
    IF EXISTS (
        SELECT 1
        FROM inserted I
        INNER JOIN Usuario U ON U.NombreUsuario = I.NombreUsuario
    )
    BEGIN
        RAISERROR ('El usuario ya existe en la tabla.', 16, 1)
        ROLLBACK TRANSACTION
        RETURN
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

    IF DATEDIFF(YEAR, @FechaNacimiento, @FechaActual) < 10
    BEGIN
        RAISERROR ('La diferencia entre la fecha de nacimiento y la fecha actual debe ser de al menos 10 años.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
END;

