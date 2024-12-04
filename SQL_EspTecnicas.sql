CREATE TABLE EspTecnicas(
	IdEspTecnica INT PRIMARY KEY IDENTITY(1,1),
	Programa NVARCHAR(100) NOT NULL,
	Titulo NVARCHAR(100) NOT NULL,
	Descripcion NVARCHAR(255) NOT NULL
);

-- PROCEDIMIENTOS ALMACENADOS
go

create procedure sp_AgregarEspTecnica
@programa nvarchar(100), 
@titulo nvarchar(100), 
@descripcion nvarchar(255)
as
insert into EspTecnicas values(@programa, @titulo, @descripcion)
return 0

go

create procedure sp_EditarEspTecnica
@idEspTecnica int,
@programa nvarchar(100), 
@titulo nvarchar(100), 
@descripcion nvarchar(255)
as
update EspTecnicas set Programa = @programa, 
                    Titulo = @titulo, 
                    Descripcion = @descripcion
                    where IdEspTecnica = @idEspTecnica
return 0

go

create procedure sp_EliminarEspTecnica
@idEspTecnica int
as
delete from EspTecnicas where IdEspTecnica = @idEspTecnica
return 0

go

create procedure sp_ListarEspTecnicas
as
select * from EspTecnicas order by Programa asc
return 0

go

create procedure sp_ListarEspTecnicasXPrograma
@programa nvarchar(100)
as
select * from EspTecnicas where Programa = @programa
return 0

