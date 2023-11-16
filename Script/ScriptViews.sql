-------------------- Views -----------------------------------
--- View: VistaActividadesUsuario
--- Función: Muestra la actividades realizadas por un usuario.

CREATE VIEW VistaActividadesUsuario AS
SELECT *
FROM Actividad
WHERE NombreUsuario = 'NombreUsuario';
GO


-- View: VistaCarrerasDisponibles
-- Función: Muestra las carreras disponibles
CREATE VIEW VistaCarrerasDisponibles AS
SELECT *
FROM Carrera;
GO


-- View: VistaAmigosUsuario
-- Función: Muestra los amigos de un Usuario
CREATE VIEW VistaAmigosUsuario AS
SELECT *
FROM Amigo
WHERE UsuarioOrigen = 'NombreUsuario';
Go