USE LIBRERIA_DB
GO

--SP Categorias    
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
        (@Descripcion, @Activo);
END
go

CREATE PROCEDURE sp_modificarCategoria
    @Descripcion VARCHAR(100),
    @Id INT
AS
BEGIN
    UPDATE CATEGORIAS
    SET Descripcion = @Descripcion
    WHERE Id = @Id;
END
GO

CREATE PROCEDURE sp_obtenerCategoriaPorId
    @Id INT
AS
BEGIN
    SELECT ID, Descripcion, Activo
    FROM CATEGORIAS
    WHERE ID = @Id;
END
GO

CREATE PROCEDURE sp_verificarCategoria
    @Descripcion VARCHAR(100)
AS
BEGIN
    SELECT COUNT(*)
    FROM CATEGORIAS
    WHERE Descripcion = @Descripcion;
END
GO

CREATE PROCEDURE sp_verificarCategoriaConID
    @Descripcion VARCHAR(100),
    @IdCategoria INT
AS
BEGIN
    SELECT COUNT(*)
    FROM CATEGORIAS
    WHERE Descripcion = @Descripcion AND Id != @IdCategoria;
END
GO

CREATE PROCEDURE sp_eliminarLogico
    @Id INT,
    @Activo BIT
AS
BEGIN
    UPDATE CATEGORIAS
    SET Activo = @Activo
    WHERE Id = @Id;
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
    WHERE Id = @Id;
END
GO