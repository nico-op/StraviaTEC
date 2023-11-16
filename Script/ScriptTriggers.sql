-------------------- Triggers -------------------------

CREATE TRIGGER ActualizarFechaModificacion_Usuario
ON Usuario
AFTER UPDATE
AS
BEGIN
    UPDATE Usuario
    SET FechaModificacion = GETDATE()
    FROM Usuario U
    INNER JOIN inserted i ON U.NombreUsuario = i.NombreUsuario;
END;