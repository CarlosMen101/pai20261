-- 1. PROCEDIMIENTO PARA AGREGAR PRODUCTO (CON BÚSQUEDA/CREACIÓN DE CATEGORÍA)
CREATE PROCEDURE SP_AgregarProducto 
    @Nombre NVARCHAR(40), 
    @Precio MONEY,
    @NombreCategoria NVARCHAR(15) 
AS
BEGIN
    DECLARE @CatID INT;

    -- Buscar si la categoría ya existe y guardar su ID
    SELECT @CatID = CategoryID 
    FROM Categories 
    WHERE CategoryName = @NombreCategoria;

    -- Si no existe (@CatID es NULL), crearla
    IF @CatID IS NULL
    BEGIN
        INSERT INTO Categories(CategoryName) VALUES(@NombreCategoria);
        -- Obtener el ID que SQL le acaba de asignar a esta nueva categoría
        SET @CatID = SCOPE_IDENTITY();
    END

    -- Insertar el producto usando el ID de categoría encontrado o creado
    INSERT INTO Products(ProductName, UnitPrice, CategoryID) 
    VALUES(@Nombre, @Precio, @CatID);
END;
GO

-- 2. PROCEDIMIENTO PARA LISTAR PRODUCTOS EN EL DATAGRID
CREATE PROCEDURE SP_ListarProductosConCategoria
AS
BEGIN
    SELECT 
        p.ProductID, 
        p.ProductName, 
        p.UnitPrice, 
        c.CategoryName
    FROM Products p
    INNER JOIN Categories c ON p.CategoryID = c.CategoryID;
END;
GO