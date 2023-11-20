-- Insertando datos de patrocinadores utilizando el procedimiento almacenado
EXEC CrudPatrocinador 'INSERT',
    @NombreLegal = 'The Coca-Cola Company',
    @Logo = 'coca_cola_logo.png',
    @Telefono = 18002662,
    @NombreComercial = 'Coca-Cola';

EXEC CrudPatrocinador 'INSERT',
    @NombreLegal = 'PepsiCo Inc.',
    @Logo = 'pepsi_logo.png',
    @Telefono = 18003574,
    @NombreComercial = 'Pepsi';

EXEC CrudPatrocinador 'INSERT',
    @NombreLegal = 'Gatorade Company',
    @Logo = 'gatorade_logo.png',
    @Telefono = 18008884,
    @NombreComercial = 'Gatorade';

EXEC CrudPatrocinador 'INSERT',
    @NombreLegal = 'Healthy Foods International',
    @Logo = 'healthy_foods_logo.png',
    @Telefono = 18885552,
    @NombreComercial = 'Healthy Foods';

EXEC CrudPatrocinador 'INSERT',
    @NombreLegal = 'Nike Inc.',
    @Logo = 'nike_logo.png',
    @Telefono = 18006453,
    @NombreComercial = 'Nike';

EXEC CrudPatrocinador 'INSERT',
    @NombreLegal = 'Under Armour, Inc.',
    @Logo = 'under_armour_logo.png',
    @Telefono = 18003388,
    @NombreComercial = 'Under Armour';

EXEC CrudPatrocinador 'INSERT',
    @NombreLegal = 'Adidas AG',
    @Logo = 'adidas_logo.png',
    @Telefono = 18005242,
    @NombreComercial = 'Adidas';

EXEC CrudPatrocinador 'INSERT',
    @NombreLegal = 'PowerBar Company',
    @Logo = 'powerbar_logo.png',
    @Telefono = 18007797,
    @NombreComercial = 'PowerBar';


-- Insertando datos en la tabla Categoria
INSERT INTO Categoria (NombreCategoria, DescripcionCategoria, NombreCarrera)
VALUES ('Elite', 'Cualquiera que quiera inscribirse', 'nacionCR');

INSERT INTO Categoria (NombreCategoria, DescripcionCategoria, NombreCarrera)
VALUES ('Junior', 'Menor de 15 años', 'nacionCR');

INSERT INTO Categoria (NombreCategoria, DescripcionCategoria, NombreCarrera)
VALUES ('Master A', 'De 30 a 40 años', 'nacionCR');

INSERT INTO Categoria (NombreCategoria, DescripcionCategoria, NombreCarrera)
VALUES ('Master B', 'De 41 a 50 años', 'nacionCR');

INSERT INTO Categoria (NombreCategoria, DescripcionCategoria, NombreCarrera)
VALUES ('Master C', 'Más de 51 años', 'nacionCR');

INSERT INTO Categoria (NombreCategoria, DescripcionCategoria, NombreCarrera)
VALUES ('Open', 'De 24 a 30 años', 'nacionCR');

INSERT INTO Categoria (NombreCategoria, DescripcionCategoria, NombreCarrera)
VALUES ('Sub-23', 'De 15 a 23 años', 'nacionCR');

INSERT INTO Categoria (NombreCategoria, DescripcionCategoria, NombreCarrera)
VALUES ('Senior', 'Para competidores experimentados', 'nacionCR');




