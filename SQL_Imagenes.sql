CREATE TABLE Imagenes(
	IdImagen INT PRIMARY KEY IDENTITY(1,1),
	Titulo NVARCHAR(150) NOT NULL,
	Foto IMAGE NULL
);

-- PROCEDIMIENTOS ALMACENADOS

go

create procedure sp_AgregarImagen
@titulo nvarchar(150), 
@foto image
as
insert into Imagenes values(@titulo, @foto)
return 0

go

create procedure sp_EditarImagen
@idImagen int,
@titulo nvarchar(150), 
@foto image
as
update Imagenes set Titulo = @titulo, 
                    Foto = @foto
                    where IdImagen = @idImagen
return 0

go

-- eliminar pendiente de modulo Equipos y NOtebooks
--create procedure sp_EliminarImagen
--@idImagen int
--as
--delete from Imagenes where IdImagen = @idImagen
--return 0

go

create procedure sp_ListarImagenes
as
select * from Imagenes order by Titulo asc
return 0
