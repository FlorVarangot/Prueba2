USE LIBRERIA_DB
GO

-------------------------------------------STORED PROCEDURES-------------------------------------------

-----CATEGORIAS-----
CREATE PROCEDURE sp_listarCategorias
AS
BEGIN
    SELECT Id, Descripcion, Activo
    FROM CATEGORIAS
END
go

CREATE PROCEDURE sp_agregarCategoria
    @Descripcion VARCHAR(100),
    @Activo BIT
AS
BEGIN
    INSERT INTO CATEGORIAS
        (Descripcion, Activo)
    VALUES
        (@Descripcion, @Activo)
END
go

CREATE PROCEDURE sp_modificarCategoria
    @Descripcion VARCHAR(100),
    @Id INT
AS
BEGIN
    UPDATE CATEGORIAS
    SET Descripcion = @Descripcion
    WHERE Id = @Id
END
GO

CREATE PROCEDURE sp_obtenerCategoriaPorId
    @Id INT
AS
BEGIN
    SELECT ID, Descripcion, Activo
    FROM CATEGORIAS
    WHERE ID = @Id
END
GO

CREATE PROCEDURE sp_verificarCategoria
    @Descripcion VARCHAR(100)
AS
BEGIN
    SELECT COUNT(*)
    FROM CATEGORIAS
    WHERE Descripcion = @Descripcion
END
GO

CREATE PROCEDURE sp_verificarCategoriaConID
    @Descripcion VARCHAR(100),
    @IdCategoria INT
AS
BEGIN
    SELECT COUNT(*)
    FROM CATEGORIAS
    WHERE Descripcion = @Descripcion AND Id != @IdCategoria
END
GO

CREATE PROCEDURE sp_eliminarLogico
    @Id INT,
    @Activo BIT
AS
BEGIN
    UPDATE CATEGORIAS
    SET Activo = @Activo
    WHERE Id = @Id
END
GO

CREATE PROCEDURE sp_reactivarModificar
    @Descripcion VARCHAR(100),
    @Activo BIT,
    @Id INT
AS
BEGIN
    UPDATE CATEGORIAS
    SET Descripcion = @Descripcion, Activo = @Activo
    WHERE Id = @Id
END
GO

-----MARCAS-----
CREATE PROCEDURE sp_ListarMarcas
AS
BEGIN
    SELECT Id, Descripcion, IdProveedor, ImagenUrl, Activo
    FROM MARCAS
END
GO

CREATE PROCEDURE sp_AgregarMarca
    @Descripcion VARCHAR(100),
    @IdProveedor INT,
    @ImagenUrl VARCHAR(1000),
    @Activo BIT
AS
BEGIN
    INSERT INTO MARCAS
        (Descripcion, IdProveedor, ImagenUrl, Activo)
    VALUES
        (@Descripcion, @IdProveedor, @ImagenUrl, @Activo)
END
GO

CREATE PROCEDURE sp_modificarMarca
    @Id INT,
    @Descripcion VARCHAR(100),
    @IdProveedor INT,
    @ImagenUrl VARCHAR(1000)
AS
BEGIN
    UPDATE MARCAS
    SET Descripcion = @Descripcion, IdProveedor = @IdProveedor, ImagenUrl = @ImagenUrl
    WHERE Id = @Id
END
GO

CREATE PROCEDURE sp_eliminarLogicoMarca
    @Id INT,
    @Activo BIT
AS
BEGIN
    UPDATE MARCAS
    SET Activo = @Activo
    WHERE Id = @Id
END
GO

CREATE PROCEDURE sp_verificarMarca
    @Descripcion VARCHAR(100)
AS
BEGIN
    SELECT COUNT(*)
    FROM MARCAS
    WHERE Descripcion = @Descripcion
END
GO

CREATE PROCEDURE sp_verificarMarcaConId
    @Descripcion VARCHAR(100),
    @IdMarca INT
AS
BEGIN
    SELECT COUNT(*)
    FROM MARCAS
    WHERE Descripcion = @Descripcion
        AND ID != @IdMarca
END
GO

CREATE PROCEDURE sp_reactivarModificarMarca
    @Id INT,
    @Descripcion VARCHAR(100),
    @IdProveedor INT,
    @ImagenUrl VARCHAR(1000),
    @Activo BIT
AS
BEGIN
    UPDATE MARCAS
    SET Descripcion = @Descripcion,
        IdProveedor = @IdProveedor,
        ImagenUrl = @ImagenUrl,
        Activo = @Activo
    WHERE Id = @Id
END
GO

CREATE PROCEDURE sp_obtenerMarcaPorId
    @Id INT
AS
BEGIN
    SELECT Id, Descripcion, IdProveedor, ImagenUrl, Activo
    FROM MARCAS
    WHERE Id = @Id
END
GO

-----CLIENTES-----
CREATE PROCEDURE sp_listarClientes
AS
BEGIN
    SELECT Id, Nombre, Apellido, DNI, Telefono, Email, Direccion, Activo
    FROM CLIENTES
END
GO

CREATE PROCEDURE sp_verificarClientePorDNI
    @DNI VARCHAR(10)
AS
BEGIN
    SELECT COUNT(*) AS ExisteCliente
    FROM CLIENTES
    WHERE DNI = @DNI
END
GO

CREATE PROCEDURE sp_verificarClientePorDNIyID
    @DNI VARCHAR(10),
    @IdCliente BIGINT
AS
BEGIN
    SELECT COUNT(*) AS ExisteCliente
    FROM CLIENTES
    WHERE DNI = @DNI AND ID != @IdCliente
END
GO

CREATE PROCEDURE sp_agregarCliente
    @Nombre VARCHAR(30),
    @Apellido VARCHAR(30),
    @DNI VARCHAR(10),
    @Telefono VARCHAR(15),
    @Email VARCHAR(50),
    @Direccion VARCHAR(50),
    @Activo BIT
AS
BEGIN
    INSERT INTO CLIENTES
        (Nombre, Apellido, DNI, Telefono, Email, Direccion, Activo)
    VALUES
        (@Nombre, @Apellido, @DNI, @Telefono, @Email, @Direccion, @Activo)
END
GO

CREATE PROCEDURE sp_eliminarLogicoCliente
    @Id BIGINT,
    @Activo BIT
AS
BEGIN
    UPDATE CLIENTES
    SET Activo = @Activo
    WHERE Id = @Id
END
GO

CREATE PROCEDURE sp_modificarCliente
    @Id BIGINT,
    @Nombre VARCHAR(30),
    @Apellido VARCHAR(30),
    @DNI VARCHAR(10),
    @Telefono VARCHAR(15),
    @Email VARCHAR(50),
    @Direccion VARCHAR(50)
AS
BEGIN
    UPDATE CLIENTES
    SET Nombre = @Nombre,
        Apellido = @Apellido,
        DNI = @DNI,
        Telefono = @Telefono,
        Email = @Email,
        Direccion = @Direccion
    WHERE Id = @Id
END
GO

CREATE PROCEDURE sp_reactivarModificarCliente
    @Id BIGINT,
    @Nombre VARCHAR(30),
    @Apellido VARCHAR(30),
    @DNI VARCHAR(10),
    @Telefono VARCHAR(15),
    @Email VARCHAR(50),
    @Direccion VARCHAR(50),
    @Activo BIT
AS
BEGIN
    UPDATE CLIENTES
    SET Nombre = @Nombre,
        Apellido = @Apellido,
        DNI = @DNI,
        Telefono = @Telefono,
        Email = @Email,
        Direccion = @Direccion,
        Activo = @Activo
    WHERE Id = @Id
END
GO

CREATE PROCEDURE sp_obtenerClientePorId
    @Id BIGINT
AS
BEGIN
    SELECT Id, Nombre, Apellido, DNI, Telefono, Email, Direccion, Activo
    FROM CLIENTES
    WHERE Id = @Id
END
GO

-----USUARIOS-----
CREATE PROCEDURE SP_NuevoUsuario
@User VARCHAR(50),
@Pass VARCHAR(50),
@Email NVARCHAR(50)
AS
BEGIN
	INSERT INTO USUARIOS (Usuario, Contraseña, Email, Tipo)
	OUTPUT inserted.Id
	VALUES (@User, @Pass, @Email, 0)
END
GO

CREATE PROCEDURE SP_ModificarUsuario
   @Nombre VARCHAR(50),
   @Apellido VARCHAR(50),
   @Email NVARCHAR(50),
   @Imagen VARCHAR(30),
   @Id INT
AS
BEGIN
    UPDATE USUARIOS
    SET Nombre = @Nombre, Apellido = @Apellido, Email = @Email, Imagen = @Imagen
    WHERE Id = @Id
END
GO


-----ARTICULOS-----

-----PROVEEDORES-----

-----COMPRAS-----

-----VENTAS-----


