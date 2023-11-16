-------------------- Views -----------------------------------
--- View: VistaActividadesUsuario
--- Función: Muestra la actividades realizadas por un usuario.

CREATE VIEW VistaActividadesAmigo AS
SELECT A.*
FROM Actividad A
INNER JOIN Amigo AM ON A.NombreUsuario = AM.UsuarioDestino;
GO


-- View: VistaAmigosUsuario
-- Función: Muestra los amigos de un Usuario
CREATE VIEW VistaAmigosUsuario AS
SELECT U.NombreUsuario, U.Foto
FROM Amigo A
INNER JOIN Usuario U ON A.UsuarioDestino = U.NombreUsuario;
GO