use master
go

--drop database SuBeefrri;
--go

create database SuBeefrri
go

use SuBeefrri;
go

create table Sucursal (
IdSucursal int identity(1,1) not null,
Nombre varchar(50) not null,
Direccion varchar(100),
constraint pk_Sucursal primary key(IdSucursal)
);

create table TipoUsuario (
IdTipo int identity(1,1) not null,
Rol varchar(50) not null,
constraint pk_TipoUsuario primary key(IdTipo)
);

create table Persona(
IdPersona int identity(1,1) not null,
Nombres varchar(100) not null,
Apellidos varchar(100),
Ci varchar(100) not null,
Direccion varchar(100),
DireccionFoto varchar(100),
constraint pk_Persona primary key(IdPersona)
);

create table Proveedor (
IdProveedor int identity(1,1) not null,
Nombre varchar(50) not null,
Nit varchar(50),
constraint pk_Proveedor primary key(IdProveedor)
);

create table Usuario (
IdUsuario int identity(1,1) not null,
ClaveUs varchar(100) not null,
PasswordUs varchar(100),
IdPersona int not null,
IdSucursal int not null,
IdTipo int not null,
constraint pk_Usuario primary key(IdUsuario),
constraint fk_Persona_Usuario foreign key(IdPersona) references Persona(IdPersona) on update cascade on delete cascade,
constraint fk_Sucursal_Usuario foreign key(IdSucursal) references Sucursal(IdSucursal) on update cascade on delete cascade,
constraint fk_Tipo_Usuario foreign key(IdTipo) references TipoUsuario(IdTipo) on update cascade on delete cascade
);

create table OrderPedido (
IdPedido int identity(1,1) not null,
NroPedido int not null,
Fecha datetime not null,
IdUsuario int not null,
Estado varchar(20) not null,--aprobado,rechazado,pendiente
constraint pk_OrderPedido primary key(IdPedido),
constraint fk_Usuario_OrderPedido foreign key(IdUsuario) references Usuario(IdUsuario) on update cascade on delete cascade,
);

create table Producto (
IdProducto int identity(1,1) not null,
Nombre varchar(50) not null,
FechaRegistro datetime not null,
Precio_Entrega decimal(7,2) not null,
Precio_Venta decimal(7,2) not null,
Stock int not null,
Peso decimal(7,2)not null,
IdProveedor int not null,
constraint pk_Producto primary key(IdProducto),
constraint fk_Proveedor_Producto foreign key(IdProveedor) references Proveedor(IdProveedor) on update cascade on delete cascade,
);

create table DetallePedido (
IdDetalle int identity(1,1) not null,
Cantidad int not null,
SubTotal int not null,
IdProducto int not null,
IdPedido int not null,
constraint pk_DetallePedido primary key(IdDetalle),
constraint fk_Pedido_DetallePedido foreign key(IdPedido) references OrderPedido(IdPedido) on update cascade on delete cascade,
constraint fk_Producto_DetallePedido foreign key(IdProducto) references Producto(IdProducto) on update cascade on delete cascade,
);

create table Entrega (
IdEntrega int identity(1,1) not null,
Fecha datetime not null,
Estado int not null,
IdDetalle int not null,
constraint pk_Entrega primary key(IdEntrega),
constraint fk_Detalle_Entrega foreign key(IdDetalle) references DetallePedido(IdDetalle) on update cascade on delete cascade,
);

create table Cobro(
IdCobro int identity(1,1) not null,
FechaCobro datetime not null,
IdUsuario int,
IdEntrega int not null,
constraint pk_Cobro primary key(IdCobro),
constraint fk_Entrega_Cobro foreign key(IdEntrega) references Entrega(IdEntrega) on update cascade on delete cascade,
constraint fk_Usuario_Cobro foreign key(IdUsuario) references Usuario(IdUsuario),
);

select * from Sucursal
insert into Sucursal values('Siucursal 1','Sin Direccion'),('Siucursal 2','Sin Direccion'),('Siucursal 3','Sin Direccion'),('Siucursal 4','Sin Direccion')

select * from Persona
insert into Persona values('Juan Perez','Rios','12334','Sin Direccion',null),('Pedro Juan','Ortega','765464','Sin Direccion',null)

select * from TipoUsuario
insert into TipoUsuario values('Adm'),('Cliente')

select * from Usuario
insert into Usuario values ('123','123',1,1,1)
insert into Usuario values ('321','321',2,2,2)

select * from Proveedor
insert into Proveedor values('Proveedor 1','Sin Nit'),('Proveedor 2','Sin Nit'),('Proveedor 3','Sin Nit'),('Proveedor 4','Sin Nit')

select * from Producto 
insert into Producto values ('Producto 1',GETDATE(),10,20,10,10,1)
insert into Producto values ('Producto 2',GETDATE(),10,20,10,10,1)
insert into Producto values ('Producto 3',GETDATE(),10,20,10,10,1)
insert into Producto values ('Producto 4',GETDATE(),10,20,10,10,1)
insert into Producto values ('Producto 5',GETDATE(),10,20,10,10,1)
insert into Producto values ('Producto 6',GETDATE(),10,20,10,10,1)

--exec sp_reporte1
alter procedure sp_Reporte1
as
begin
SELECT        Sucursal.IdSucursal, Sucursal.Nombre NombreSucursal, Sucursal.Direccion, Producto.Nombre AS NombreProducto, Producto.Precio_Entrega, Producto.Precio_Venta, Proveedor.Nombre AS NombreProveedor
into #temp1
FROM            Sucursal INNER JOIN
                         Usuario ON Sucursal.IdSucursal = Usuario.IdSucursal INNER JOIN
                         OrderPedido ON Usuario.IdUsuario = OrderPedido.IdUsuario INNER JOIN
                         DetallePedido ON OrderPedido.IdPedido = DetallePedido.IdPedido INNER JOIN
                         Producto ON DetallePedido.IdProducto = Producto.IdProducto INNER JOIN
                         Proveedor ON Producto.IdProveedor = Proveedor.IdProveedor
ORDER BY Sucursal.IdSucursal

select top 1 IdSucursal, COUNT(IdSucursal) cantidad
into #tempSucursal
from #temp1
group by IdSucursal
order by 2 desc

select * from #temp1 
where IdSucursal= (select IdSucursal from #tempSucursal)
drop table #temp1
drop table #tempSucursal
end
go

select * from Sucursal
