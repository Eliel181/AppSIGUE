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

    RETURN 0; -- Opcional
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

    RETURN 0; -- Opcional
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

    RETURN 0; -- Opcional
END;

go
--SP -ListarUsuarios
CREATE PROCEDURE sp_ListarUsuarios
AS
BEGIN
    -- Seleccionar todos los usuarios
    SELECT IdUsuario, Nombre, Apellido, LoginName, Password, Rol, Estado, Foto
    FROM Usuarios;

    RETURN 0; -- Opcional
END;
