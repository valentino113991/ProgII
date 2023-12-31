USE [master]
GO
/****** Object:  Database [facturacion]    Script Date: 13/09/2023 20:58:46 ******/
CREATE DATABASE [facturacion]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'facturacion', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\facturacion.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'facturacion_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\facturacion_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [facturacion] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [facturacion].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [facturacion] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [facturacion] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [facturacion] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [facturacion] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [facturacion] SET ARITHABORT OFF 
GO
ALTER DATABASE [facturacion] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [facturacion] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [facturacion] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [facturacion] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [facturacion] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [facturacion] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [facturacion] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [facturacion] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [facturacion] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [facturacion] SET  ENABLE_BROKER 
GO
ALTER DATABASE [facturacion] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [facturacion] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [facturacion] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [facturacion] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [facturacion] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [facturacion] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [facturacion] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [facturacion] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [facturacion] SET  MULTI_USER 
GO
ALTER DATABASE [facturacion] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [facturacion] SET DB_CHAINING OFF 
GO
ALTER DATABASE [facturacion] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [facturacion] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [facturacion] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [facturacion] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [facturacion] SET QUERY_STORE = OFF
GO
USE [facturacion]
GO
/****** Object:  Table [dbo].[T_ARTICULOS]    Script Date: 13/09/2023 20:58:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_ARTICULOS](
	[nro_articulo] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NULL,
	[precio_unitario] [numeric](8, 2) NULL,
 CONSTRAINT [pk_articulo] PRIMARY KEY CLUSTERED 
(
	[nro_articulo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_DETALLES_FACTURA]    Script Date: 13/09/2023 20:58:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_DETALLES_FACTURA](
	[nro_detalle] [int] IDENTITY(1,1) NOT NULL,
	[cantidad] [int] NULL,
	[nro_factura] [int] NULL,
	[nro_articulo] [int] NULL,
 CONSTRAINT [pk_detalle] PRIMARY KEY CLUSTERED 
(
	[nro_detalle] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_FACTURAS]    Script Date: 13/09/2023 20:58:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_FACTURAS](
	[nro_factura] [int] IDENTITY(1,1) NOT NULL,
	[fecha] [date] NULL,
	[cliente] [varchar](100) NULL,
	[nro_forma_pago] [int] NULL,
 CONSTRAINT [pk_factura] PRIMARY KEY CLUSTERED 
(
	[nro_factura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_FORMAS_PAGO]    Script Date: 13/09/2023 20:58:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_FORMAS_PAGO](
	[nro_forma_pago] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NULL,
 CONSTRAINT [pk_forma_pago] PRIMARY KEY CLUSTERED 
(
	[nro_forma_pago] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[T_ARTICULOS] ON 

INSERT [dbo].[T_ARTICULOS] ([nro_articulo], [nombre], [precio_unitario]) VALUES (1, N'remera adidas', CAST(11000.00 AS Numeric(8, 2)))
INSERT [dbo].[T_ARTICULOS] ([nro_articulo], [nombre], [precio_unitario]) VALUES (2, N'pantalon adidas', CAST(15000.00 AS Numeric(8, 2)))
INSERT [dbo].[T_ARTICULOS] ([nro_articulo], [nombre], [precio_unitario]) VALUES (3, N'gorra adidas', CAST(8000.00 AS Numeric(8, 2)))
SET IDENTITY_INSERT [dbo].[T_ARTICULOS] OFF
GO
SET IDENTITY_INSERT [dbo].[T_DETALLES_FACTURA] ON 

INSERT [dbo].[T_DETALLES_FACTURA] ([nro_detalle], [cantidad], [nro_factura], [nro_articulo]) VALUES (1, 1, 2, 1)
INSERT [dbo].[T_DETALLES_FACTURA] ([nro_detalle], [cantidad], [nro_factura], [nro_articulo]) VALUES (2, 1, 1, 3)
INSERT [dbo].[T_DETALLES_FACTURA] ([nro_detalle], [cantidad], [nro_factura], [nro_articulo]) VALUES (3, 1, 2, 2)
INSERT [dbo].[T_DETALLES_FACTURA] ([nro_detalle], [cantidad], [nro_factura], [nro_articulo]) VALUES (4, 2, 3, 2)
INSERT [dbo].[T_DETALLES_FACTURA] ([nro_detalle], [cantidad], [nro_factura], [nro_articulo]) VALUES (6, 7, 7, 1)
INSERT [dbo].[T_DETALLES_FACTURA] ([nro_detalle], [cantidad], [nro_factura], [nro_articulo]) VALUES (7, 7, 7, 3)
INSERT [dbo].[T_DETALLES_FACTURA] ([nro_detalle], [cantidad], [nro_factura], [nro_articulo]) VALUES (8, 10, 8, 1)
SET IDENTITY_INSERT [dbo].[T_DETALLES_FACTURA] OFF
GO
SET IDENTITY_INSERT [dbo].[T_FACTURAS] ON 

INSERT [dbo].[T_FACTURAS] ([nro_factura], [fecha], [cliente], [nro_forma_pago]) VALUES (1, CAST(N'2023-08-27' AS Date), N'Valentino Bertello', 1)
INSERT [dbo].[T_FACTURAS] ([nro_factura], [fecha], [cliente], [nro_forma_pago]) VALUES (2, CAST(N'2023-08-27' AS Date), N'Carlos Pérez', 3)
INSERT [dbo].[T_FACTURAS] ([nro_factura], [fecha], [cliente], [nro_forma_pago]) VALUES (3, CAST(N'2023-05-27' AS Date), N'Martin López', 2)
INSERT [dbo].[T_FACTURAS] ([nro_factura], [fecha], [cliente], [nro_forma_pago]) VALUES (7, CAST(N'2023-09-13' AS Date), N'Cristiano Ronaldo', 1)
INSERT [dbo].[T_FACTURAS] ([nro_factura], [fecha], [cliente], [nro_forma_pago]) VALUES (8, CAST(N'2023-09-13' AS Date), N'Linonel messi', 2)
SET IDENTITY_INSERT [dbo].[T_FACTURAS] OFF
GO
SET IDENTITY_INSERT [dbo].[T_FORMAS_PAGO] ON 

INSERT [dbo].[T_FORMAS_PAGO] ([nro_forma_pago], [nombre]) VALUES (1, N'efectivo')
INSERT [dbo].[T_FORMAS_PAGO] ([nro_forma_pago], [nombre]) VALUES (2, N'crédito')
INSERT [dbo].[T_FORMAS_PAGO] ([nro_forma_pago], [nombre]) VALUES (3, N'débito')
SET IDENTITY_INSERT [dbo].[T_FORMAS_PAGO] OFF
GO
ALTER TABLE [dbo].[T_DETALLES_FACTURA]  WITH CHECK ADD  CONSTRAINT [fk_detalle_articulo] FOREIGN KEY([nro_articulo])
REFERENCES [dbo].[T_ARTICULOS] ([nro_articulo])
GO
ALTER TABLE [dbo].[T_DETALLES_FACTURA] CHECK CONSTRAINT [fk_detalle_articulo]
GO
ALTER TABLE [dbo].[T_DETALLES_FACTURA]  WITH CHECK ADD  CONSTRAINT [fk_detalle_factura] FOREIGN KEY([nro_factura])
REFERENCES [dbo].[T_FACTURAS] ([nro_factura])
GO
ALTER TABLE [dbo].[T_DETALLES_FACTURA] CHECK CONSTRAINT [fk_detalle_factura]
GO
ALTER TABLE [dbo].[T_FACTURAS]  WITH CHECK ADD  CONSTRAINT [fk_factura_forma_pago] FOREIGN KEY([nro_forma_pago])
REFERENCES [dbo].[T_FORMAS_PAGO] ([nro_forma_pago])
GO
ALTER TABLE [dbo].[T_FACTURAS] CHECK CONSTRAINT [fk_factura_forma_pago]
GO
/****** Object:  StoredProcedure [dbo].[SP_CONSULTAR_ART]    Script Date: 13/09/2023 20:58:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_CONSULTAR_ART]
AS
BEGIN
select * from T_ARTICULOS;
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CONSULTAR_DETALLES_FACTURA]    Script Date: 13/09/2023 20:58:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_CONSULTAR_DETALLES_FACTURA]
@nro_factura int
AS BEGIN
select d.*, a.nombre, f.cliente, f.fecha, fp.nombre
FROM T_FACTURAS f JOIN T_DETALLES_FACTURA d ON d.nro_factura = f.nro_factura 
JOIN T_ARTICULOS a ON a.nro_articulo = d.nro_articulo
JOIN T_FORMAS_PAGO fp ON fp.nro_forma_pago = f.nro_forma_pago
WHERE d.nro_factura = @nro_factura
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CONSULTAR_FACTURAS]    Script Date: 13/09/2023 20:58:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[SP_CONSULTAR_FACTURAS]
@fecha_desde DATETIME,
@fecha_hasta DATETIME,
@cliente varchar(255)
AS BEGIN
SELECT f.nro_factura, f.fecha, f.cliente, fp.nombre
FROM T_FACTURAS f JOIN T_FORMAS_PAGO fp ON fp.nro_forma_pago = f.nro_forma_pago
WHERE (@fecha_desde is null OR f.fecha >= @fecha_desde) AND
(@fecha_hasta is null OR f.fecha <= @fecha_hasta) AND
(@cliente is null OR f.cliente LIKE + '%' + @cliente + '%')
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CONSULTAR_FORMAS_PAGO]    Script Date: 13/09/2023 20:58:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_CONSULTAR_FORMAS_PAGO]
AS
BEGIN
select * from T_FORMAS_PAGO;
END
GO
/****** Object:  StoredProcedure [dbo].[SP_INSERTAR_DETALLE]    Script Date: 13/09/2023 20:58:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_INSERTAR_DETALLE]
@cantidad INT,
@nro_factura INT,
@nro_articulo INT
AS
BEGIN
INSERT INTO T_DETALLES_FACTURA(cantidad, nro_factura, nro_articulo) VALUES (@cantidad, @nro_factura, @nro_articulo);
END
GO
/****** Object:  StoredProcedure [dbo].[SP_INSERTAR_FORMA_PAGO]    Script Date: 13/09/2023 20:58:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_INSERTAR_FORMA_PAGO]
@nombre varchar(50),
@nro_forma_pago INT OUTPUT
AS
BEGIN
INSERT INTO T_FORMAS_PAGO(nombre) VALUES (@nombre);
SET @nro_forma_pago = SCOPE_IDENTITY();
END
GO
/****** Object:  StoredProcedure [dbo].[SP_INSERTAR_MAESTRO]    Script Date: 13/09/2023 20:58:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_INSERTAR_MAESTRO]
@fecha DATE,
@cliente varchar(50),
@nro_forma_pago INT,
@nro_factura INT OUTPUT
AS
BEGIN
INSERT INTO T_FACTURAS(fecha, cliente, nro_forma_pago) VALUES (@fecha, @cliente, @nro_forma_pago);
SET @nro_factura = SCOPE_IDENTITY();
 END
GO
/****** Object:  StoredProcedure [dbo].[SP_PROXIMO_ID]    Script Date: 13/09/2023 20:58:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_PROXIMO_ID]
@next INT OUTPUT
as
begin
SET @next = (SELECT MAX (nro_factura) +1 FROM T_FACTURAS);
end
GO
/****** Object:  StoredProcedure [dbo].[SP_REPORTE_ARTICULOS]    Script Date: 13/09/2023 20:58:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_REPORTE_ARTICULOS] 
@fecha_desde DATETIME,
@fecha_hasta DATETIME
AS BEGIN
SELECT a.nombre as articulo, sum(cantidad) as cantidad
FROM T_DETALLES_FACTURA d JOIN T_ARTICULOS a ON a.nro_articulo = d.nro_articulo
JOIN T_FACTURAS f ON f.nro_factura = d.nro_factura
WHERE f.fecha BETWEEN @fecha_desde AND @fecha_hasta
GROUP BY a.nombre
END
GO
USE [master]
GO
ALTER DATABASE [facturacion] SET  READ_WRITE 
GO
