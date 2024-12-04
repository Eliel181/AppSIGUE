CREATE TABLE Usuarios (
    IdUsuario INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(50) NOT NULL,
    Apellido NVARCHAR(50) NOT NULL,
    LoginName NVARCHAR(50) NOT NULL,
    Password NVARCHAR(100) NOT NULL,
    Rol NVARCHAR(50) NOT NULL,
    Estado BIT,
    Foto IMAGE 
);

--TABLA LOCACION
CREATE TABLE Locaciones (
    IdLocacion INT PRIMARY KEY IDENTITY(1,1),
    Descripcion VARCHAR(150) NOT NULL
);

-- TABLA PERSONAS
CREATE TABLE Personas (
    IdPersona INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL,
    Apellido VARCHAR(100) NOT NULL,
    Cuit INT NOT NULL,
    Domicilio VARCHAR(100) NOT NULL,
    Telefono INT NOT NULL,
    Foto IMAGE,
    Tipo VARCHAR(60) NOT NULL,
    Locacion INT FOREIGN KEY REFERENCES Locaciones(IdLocacion)
);

go

-- SP-AGREGAR USUARIOS
CREATE PROCEDURE sp_AgregarUsuario
    @nombre NVARCHAR(100),
    @apellido NVARCHAR(100),
    @loginName NVARCHAR(100),
    @password NVARCHAR(100),
    @rol NVARCHAR(100), 
    @estado BIT,
    @foto IMAGE
AS
BEGIN
    -- Insertar datos en la tabla Usuarios
    INSERT INTO Usuarios (Nombre, Apellido, LoginName, Password, Rol, Estado, Foto)
    VALUES (@nombre, @apellido, @loginName, @password, @rol, @estado, @foto);

    RETURN 0; 
END;

GO
-- SP-EDITAR USUARIOS
CREATE PROCEDURE sp_EditarUsuario
    @idUsuario INT,
    @nombre NVARCHAR(100),
    @apellido NVARCHAR(100),
    @loginName NVARCHAR(100),
    @password NVARCHAR(100),
    @rol NVARCHAR(100), 
    @estado BIT,
    @foto IMAGE
AS
BEGIN
    -- Actualizar los datos del usuario
    UPDATE Usuarios
    SET Nombre = @nombre,
        Apellido = @apellido,
        LoginName = @loginName,
        Password = @password,
        Rol = @rol,
        Estado = @estado,
        Foto = @foto
    WHERE IdUsuario = @idUsuario;

    RETURN 0;
END;

GO
--SP -ELIMINAR_USUARIOS
CREATE PROCEDURE sp_EliminarUsuario
    @idUsuario INT
AS
BEGIN
    -- Eliminar el usuario por IdUsuario
    DELETE FROM Usuarios
    WHERE IdUsuario = @idUsuario;

    RETURN 0;
END;

go
--SP -ListarUsuarios
CREATE PROCEDURE sp_ListarUsuarios
AS
BEGIN
    -- Seleccionar todos los usuarios
    SELECT IdUsuario, Nombre, Apellido, LoginName, Password, Rol, Estado, Foto
    FROM Usuarios;

    RETURN 0; 
END;

-------SP PARA LOCACIONES--------------------------------------------
go
-- SP-AGREGAR LOCACIONES
CREATE PROCEDURE sp_AgregarLocacion
    @descripcion NVARCHAR(100)
AS
BEGIN
    -- Insertar datos en la tabla Usuarios
    INSERT INTO Locaciones (Descripcion)
    VALUES (@descripcion);

    RETURN 0; 
END;

GO
-- SP-EDITAR LOCACION
CREATE PROCEDURE sp_EditaLocacion
    @idLocacion INT,
    @descripcion NVARCHAR(150)
AS
BEGIN
    -- Actualizar los datos del usuario
    UPDATE Locaciones
    SET Descripcion = @descripcion
        
    WHERE IdLocacion = @idLocacion;

    RETURN 0; 
END;

GO
--SP -ELIMINAR_LOCACIONES
CREATE PROCEDURE sp_EliminarLocacion
    @idLocacion INT
AS
BEGIN
    -- Eliminar el usuario por IdUsuario
    DELETE FROM Locaciones
    WHERE IdLocacion = @idLocacion;

    RETURN 0; 
END;

go
--SP -Listar_Locaciones
CREATE PROCEDURE sp_ListarLocaciones
AS
BEGIN
    -- Seleccionar todos los usuarios
    SELECT IdLocacion, Descripcion
    FROM Locaciones;

    RETURN 0;
END;



-------SP PARA PERSONAS
go
-- SP-AGREGAR Personas
CREATE PROCEDURE sp_AgregarPersona
    @Nombre VARCHAR(100),
    @Apellido VARCHAR(100),
    @Cuit INT,
    @Domicilio VARCHAR(100),
    @Telefono INT,
    @Foto IMAGE,
    @Tipo VARCHAR(60),
    @Locacion INT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Personas (Nombre, Apellido, Cuit, Domicilio, Telefono, Foto, Tipo, Locacion)
    VALUES (@Nombre, @Apellido, @Cuit, @Domicilio, @Telefono, @Foto, @Tipo, @Locacion)
END

GO
-- SP-EDITAR PERSONAS
CREATE PROCEDURE sp_EditarPersona
    @IdPersona INT,
    @Nombre VARCHAR(100),
    @Apellido VARCHAR(100),
    @Cuit INT,
    @Domicilio VARCHAR(100),
    @Telefono INT,
    @Foto IMAGE,
    @Tipo VARCHAR(60),
    @Locacion INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Personas
    SET Nombre = @Nombre,
        Apellido = @Apellido,
        Cuit = @Cuit,
        Domicilio = @Domicilio,
        Telefono = @Telefono,
        Foto = @Foto,
        Tipo = @Tipo,
        Locacion = @Locacion
    WHERE IdPersona = @IdPersona
END

GO
--SP ELIMINAR PERSONA
CREATE PROCEDURE sp_EliminarPersona
    @IdPersona INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Personas
    WHERE IdPersona = @IdPersona
END

go
--SP -LISTAR PERSONAS
CREATE PROCEDURE sp_ListarPersonas
AS
BEGIN
    SET NOCOUNT ON;

    SELECT IdPersona, Nombre, Apellido, Cuit, Domicilio, Telefono, Foto, Tipo, Locacion
    FROM Personas
END
