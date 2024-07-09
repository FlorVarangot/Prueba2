--------------------------------------------------------------------------------------------------------------------------------------------

use LIBRERIA_DB
go

SELECT * FROM COMPRAS
WHERE Id BETWEEN 1 AND 4;

-- PARA REVISAR Y AGREGAR:

--Listar de Ventas.ASPX
SELECT V.Id AS Venta, V.FechaVenta AS Fecha, V.IdCliente, C.Apellido AS Cliente, V.TotalVenta AS Total
FROM VENTAS V
INNER JOIN CLIENTES C ON V.IdCliente = C.Id;

--Listar de DetalleVenta
SELECT D.IdVenta AS Venta, A.Descripcion AS Articulo, S.PrecioActual AS Precio, D.Cantidad
FROM DETALLE_VENTAS D
INNER JOIN ARTICULOS A ON D.IdArticulo = A.Id
INNER JOIN STOCKARTICULOS S ON A.Id = S.IdArticulo;

SELECT * FROM STOCKARTICULOS

CREATE TABLE STOCKARTICULOS (
	IdArticulo BIGINT NOT NULL,
	Fecha DATE NOT NULL,
	StockActual INT NOT NULL,
	PrecioActual MONEY NOT NULL,
)

----------------------------------------------- PRUEBAS -----------------------------------------------

SELECT  DA.Fecha, A.Codigo, A.Descripcion, DA.Stock, DA.Precio FROM DATOS_ARTICULOS DA
INNER JOIN ARTICULOS A ON DA.IdArticulo = A.Id
ORDER BY DA.Fecha DESC

UPDATE DATOS_ARTICULOS
SET Fecha = '2024-06-01'
WHERE Id BETWEEN 19 AND 24

SELECT * FROM DATOS_ARTICULOS
WHERE Fecha >= GETDATE();

-- INSERT 10 COMPRAS con prov y articculos aleatorios
DECLARE @ProveedorId INT;
DECLARE @ArticuloId BIGINT;
DECLARE @FechaCompra DATE;
DECLARE @TotalCompra MONEY;

SET @ProveedorId = (SELECT TOP 1 Id FROM PROVEEDORES ORDER BY NEWID());
SET @ArticuloId = (SELECT TOP 1 Id FROM ARTICULOS ORDER BY NEWID());

WHILE (SELECT COUNT(*) FROM COMPRAS) < 10
BEGIN
    SET @FechaCompra = DATEADD(DAY, -ABS(CHECKSUM(NEWID())) % 365, GETDATE());
    SET @TotalCompra = ROUND(RAND() * 1000, 2);

    INSERT INTO COMPRAS (FechaCompra, IdProveedor, TotalCompra)
    VALUES (@FechaCompra, @ProveedorId, @TotalCompra);

    -- detalles
    INSERT INTO DETALLE_COMPRAS (IdCompra, IdArticulo, Precio, Cantidad)
    VALUES (SCOPE_IDENTITY(), @ArticuloId, @TotalCompra / 2, 5);
END


-- INSERT 10 VENTAS con cli y art
DECLARE @ClienteId BIGINT;
DECLARE @FechaVenta DATE;
DECLARE @TotalVenta MONEY;

SET @ClienteId = (SELECT TOP 1 Id FROM CLIENTES ORDER BY NEWID());

WHILE (SELECT COUNT(*) FROM VENTAS) < 10
BEGIN
    SET @FechaVenta = DATEADD(DAY, -ABS(CHECKSUM(NEWID())) % 365, GETDATE());
    SET @TotalVenta = ROUND(RAND() * 500, 2);

    INSERT INTO VENTAS (FechaVenta, IdCliente, TotalVenta)
    VALUES (@FechaVenta, @ClienteId, @TotalVenta);
	
	--det de vventas
    INSERT INTO DETALLE_VENTAS (IdVenta, IdArticulo, Cantidad)
    VALUES (SCOPE_IDENTITY(), @ArticuloId, 3);
END


-------------------- SP PARA AGREGAR MARCA + VALIDACION ------------------------------
--SELECT * FROM MARCAS

ALTER PROCEDURE SP_AgregarMarca
    @Descripcion VARCHAR(100),
    @IdProveedor INT,
    @ImagenUrl VARCHAR(1000),
	@Activo BIT
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM Marcas WHERE Descripcion = @Descripcion)
    BEGIN
        INSERT INTO Marcas (Descripcion, IdProveedor, ImagenUrl, Activo)
        VALUES (@Descripcion, @IdProveedor, @ImagenUrl, @Activo)
    END
    ELSE
    BEGIN
        Raiserror('Ya existe una marca con el mismo nombre.', 16, 1)
    END
END


EXEC SP_AgregarMarca
    @Descripcion = 'Marca de prueba',
    @IdProveedor = 1,
    @ImagenUrl = 'https://th.bing.com/th/id/R.5f528b2a54430a3ccdca3866fb51fea1?rik=Na27yWVyFLJ1Xw&riu=http%3a%2f%2flogos-download.com%2fwp-content%2fuploads%2f2016%2f05%2fMarca_logo_white_red_background.png&ehk=4x%2bd%2b8wxZX00y7zwL73uHQM1UowofNsvSsE%2fBv6at6I%3d&risl=&pid=ImgRaw&r=0',
	@Activo = 1;

	delete from COMPRAS 
	where id = 10

--------------------------------- PARA CALCULAR TOTAL DE VENTAS SEGUN DETALLES Y DATOS_ARTICULOS :

SELECT  DA.IdArticulo, DA.Fecha, Da.Precio, A.Ganancia_Porcentaje
FROM DATOS_ARTICULOS DA
INNER JOIN ARTICULOS A ON DA.IdArticulo = A.Id
WHERE DA.Fecha <= '2024-05-10' -- aca fecha de venta
AND IdArticulo IN (4,1,8) -- aca poner id de articulos para ese idventa
ORDER BY Fecha DESC
---Esto trae los datos, luego hacer los c�lculos (+ %ganancia y *cantidad)

-----------------------------------------SP NUEVO USUARIO

EXEC SP_NuevoUsuario 'ALGO','123'
SELECT * FROM USUARIOS
--OK

EXEC SP_NuevoUsuario 'Admin','admin'

use LIBRERIA_DB
SELECT * FROM USUARIOS

UPDATE USUARIOS 
SET Tipo = 1 
WHERE Id=3
