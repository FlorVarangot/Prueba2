USE LIBRERIA_DB
GO
SET DATEFORMAT 'YMD'
GO
INSERT INTO CATEGORIAS(Descripcion)
VALUES	('ABROCHADORAS'),
		('ABACOS'),
		('BOLIGRAFOS'),
		('FIBRONES'),
		('PAPELER�A'),
		('JUGUETER�A'),
		('OFICINA'),
		('ADHESIVOS'),
		('ESCRITURA'),
		('MANUALIDADES'),
		('CORRECTORES'),
		('ESCOLAR')
GO

INSERT INTO PROVEEDORES
VALUES	('FERNANDEZ INS','30-70822107-9', null,'1111111111','Villa Martelli',1),
		('PENCIL SA','30-69822108-0','pencilsa@pencilsa.com.ar','1122222222','Saavedra',1),
		('MMIRANDA','30-71289224-1','pedidos@mmiranda.com','+54 11 33333333','Parque Patricios',1),
		('PROVEEDOR A','30-12345678-9','proveedorA@email.com','4747-1234', 'Calle A 123',1),
		('PROVEEDOR B','30-98765432-1','proveedorB@email.com','4719-5678', 'Calle B 456',1),
		('PAPELERIA S.A.', '30-98765432-1', 'contacto@papeleria.com', '4815-5678', 'Calle del Papel 123',0);
GO

INSERT INTO MARCAS (Descripcion, IdProveedor, ImagenUrl,Activo)
VALUES	('BIC', 1, 'https://th.bing.com/th/id/OIP.CF_jE1QZzGJqoOXE1E_ZlQHaFP?rs=1&pid=ImgDetMain',1),
		('MURANO', 2, null,1),
		('CONSUL TRIO', 2, 'https://i.pinimg.com/736x/b2/07/1f/b2071f1213da8ab3fbec184038c3e447.jpg',1),
		('FILGO', 3, 'https://d2r9epyceweg5n.cloudfront.net/stores/002/254/237/products/7483febf-a0eb-444c-b545-3d400bbb867f1-f7539175982bd714d816620494259461-640-0.jpeg',1),
		('SABONIS',4,'https://sabonis.cl/wp-content/uploads/2022/04/1640629353913-sb-logo-azul-_1_.png',1),
		('CRAYOLA',5,'https://logodix.com/logo/1856464.jpg',1),
		('FABER CASTELL',6,'https://th.bing.com/th/id/OIP.u-AailQosyvwTFolvD0eIwHaCZ?rs=1&pid=ImgDetMain',0),
		('BOREAL',6,null,0),
		('MAPED',1,'https://dabdoob-cdn-primary.fra1.cdn.digitaloceanspaces.com/media/master/brand/5psEFe/c45518da-73f2-4e5b-9e8c-2ebcdc0e58da_GLkuor2sEU.png',1),
		('PAPER MATE',3,'https://th.bing.com/th/id/OIP.OmgeY2LgD74_xM8CiIYWBQHaCL?rs=1&pid=ImgDetMain',0),
		('PELIKAN',4,'https://www.liblogo.com/img-logo/pe4721pdf4-pelikan-logo-pelikan-logo-industry--com.png',0),
		('SIMBALL',4,'https://simball.com.ar/wp-content/uploads/2022/01/logo-web.png',1),
		('PARKER',5,'https://cdn.shopify.com/s/files/1/0782/2043/collections/parker_62de1b2a-c0b3-48af-ba6b-912d072dd4f9_1024x1024.png?v=1586433453',1),
		('VOLIGOMA',1,'https://voligoma.com.ar/wp-content/themes/voligoma/images/logo.png',0)
GO

INSERT INTO ARTICULOS(Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Ganancia_Porcentaje, Imagen, Stock_Minimo, Activo)
VALUES	('AAA001','HP-210','ABROCHADORA PINZA 24/6',1,1,55,'https://th.bing.com/th/id/OIP.fTc2Wxi4JhGH_QQfPpQX9QHaHa?rs=1&pid=ImgDetMain',5,1),
		('AAA002','HP-45 ','ABROCHADORA PINZA 26/6',2,1,55,'https://www.ramospapeleria.com.ar/img/p/23355/1.jpeg?width=600&mode=max&upscale=false&quality=90',5,1),
		('AAA003','HP 10 ','ABROCHADORA PINZA P/BROCHE N�10',3,1,55,'https://th.bing.com/th/id/R.cb3f6cb6cd4cd392f9e0dea50b568580?rik=jVwT8Z%2bUcb%2b%2feA&pid=ImgRaw&r=0',5,1),
		('AAA004','HP 12 ','ABROCHADORA PINZA P/BROCHE N�12',4,1,55,'https://th.bing.com/th/id/OIP.TVHr_-8Zbq0vgQ1PtKZxfAHaHa?rs=1&pid=ImgDetMain',5,1),
		('AAA005','ABACO PL�STICO N� 3','ABACO PLASTICO 3 COLUMNAS',7,2,38,'https://th.bing.com/th/id/OIP.78D11ioXuGSLsz-dEB0GDwHaHa?w=1000&h=1000&rs=1&pid=ImgDetMain',10,1),
		('AAA006','ABACO PL�STICO N� 4','ABACO PLASTICO 4 COLUMNAS',6,2,38,'https://padihey.com.br/wp-content/uploads/2022/03/7d50596fd15159ed9262419b4e552c88.png',10,1),
		('AAA007','ABACO PL�STICO N� 5','ABACO PLASTICO 5 COLUMNAS',2,2,40,'https://th.bing.com/th/id/OIP.hf3sjOaKOk3_kx9o4ZpxSgHaHa?w=500&h=500&rs=1&pid=ImgDetMain',10,1),
		('AAA008','HS-10 A','ABROCHADORA KANGARO',3,1,40,null,10,1),
		('AAA009','POCKET 10','ABROCHADORA KANGARO POCKET 10',4,1,40,null,10,1),
		('AAA010','STAPLER 10','ABROCHADORA KANGARO STAPLER 10',5,1,40,'https://http2.mlstatic.com/D_NQ_NP_682857-MLA42884052846_072020-O.webp',10,1),
		('AAA011','BOLTRIO X12','BOLIG.CONSUL TRIO ROJO RETRACTIL',6,4,35,'https://th.bing.com/th/id/OIP.y6DixmrxZHDJLjXKQMeN_gHaHa?pid=ImgDet&w=474&h=474&rs=1',10,1),
		('AAA012','BOL. NEGRO 1MM X36','BOLIGRAFO FILGO FASTRACK NEGRO 1MM RETRACTIL',7,4,35,'https://http2.mlstatic.com/D_NQ_NP_2X_634662-MLA31351168191_072019-F.jpg',10,1),
		('AAA013','BOL. ROJO 1MM X36','BOLIGRAFO FILGO FASTRACK ROJO 1MM RETRACTIL',1,4,35,'https://th.bing.com/th/id/OIP.wDyxHYaByxp7QhV-XV6LrQHaGH?rs=1&pid=ImgDetMain',10,0),
		('AAA014','BOL. AZUL 1MM X36 F','BOLIGRAFO FILGO GINZA AZUL 1MM RETRACTIL',2,4,35,null,10,0),
		('LIB001', 'CUADERNO N3', 'CUADERNO DE TAPA DURA CON HOJAS RAYADAS',4,6,30,'https://example.com/cuaderno_rayado.jpg', 30,1),
		('LIB002', 'L�PIZ HB', 'L�PIZ DE GRAFITO PARA ESCRIBIR Y DIBUJAR',7,7,15, 'https://example.com/lapiz_hb.jpg', 40,1),
		('LIB003', 'MCD-223', 'SET DE 12 MARCADORES DE COLORES VARIADOS',5,5,25, 'https://example.com/marcadores_colores.jpg', 40,0),
		('LIB004', 'ASEM 36-2', 'AGENDA CON VISTA SEMANAL Y ESPACIO PARA NOTAS',1,6,30.5, 'https://example.com/agenda_semanal.jpg', 10,1),
		('AAA015','BOREAL A4-70','RESMA PAPEL A4 70GR 21x29,7',8,6,40,'https://resmasboreal.com.ar/assets/uploads/product/8_1449676166.jpg',10,1),
		('AAA016','SKU# 015540105','REGLA FLEXIBLE DE 20CM',11,3,40,null,10,1),
		('AAA017','CO-C 56','CORRECTOR CINTA 5MM 6MTS',10,11,40,null,10,1),
		('AAA018','GNFT-116114','BLISTER GOMA DE BORRAR NIGHTFALL TEENS S/ FTALATOS 21,8 X 12 X 61MM',9,3,40,'https://ar.maped.com/wp-content/uploads/sites/34/2022/11/116114_r01.png',10,1),
		('AAA019','SKU-0211','MARCADORES JOURNAL COLOR PASTEL PUNTA C�NICA - LETTERING',12,4,40,'https://simball.com.ar/wp-content/uploads/2022/05/Pastel-copy.png',10,1),
		('AAA020','IM-GT','LAPICERA PLUMA IM NUEVA LINEA LACA NEGRO GT',13,7,40,'https://casalalapicera.com/wp-content/uploads/2023/03/lapicera-de-pluma-parker-im-laca-negra-gt.png',10,1),
		('AAA020','VB-40','VOLIBARRA BARRA ADHESIVA 40GR',13,8,40,'https://voligoma.com.ar/wp-content/themes/voligoma/images/our-products-volibarra.png',10,1)
GO

INSERT INTO Compras (FechaCompra, IdProveedor, TotalCompra)
VALUES 
    ('2024-06-01', 1, 100000),
    ('2024-06-01', 1, 150000),
    ('2024-06-01', 1, 115900),
    ('2024-06-01', 1, 110680),
    ('2024-06-02', 3, 3500),
    ('2024-06-01', 1, 15020),
    ('2024-06-02', 2, 20200),
    ('2024-06-03', 3, 18900),
    ('2024-06-04', 1, 21600),
    ('2024-06-05', 4, 25000);
GO

INSERT INTO Detalle_Compras (IdCompra, IdArticulo, Precio, Cantidad)
VALUES 
    (1, 3, 10000, 10),
    (2, 5, 3750, 10),
    (2, 2, 3750, 10),
    (3, 7, 5795, 20),
    (4, 2, 6917.50, 16),
    (5, 6, 700, 5),
    (6, 11, 751, 10),
    (6, 12, 751, 10),
    (7, 9, 2020, 10),
    (8, 12, 3780, 5),
    (9, 14, 4320, 5),
    (10, 4, 5000, 5);
GO


INSERT INTO CLIENTES (Nombre, Apellido, DNI, Telefono, Email, Direccion)
VALUES ('Juan', 'P�rez', '12345678', '011-555-1234', 'juan@email.com', 'Av. Rivadavia 123'),
       ('Marta', 'Gonz�lez', '23456789', '011-555-5678', 'maria@email.com', 'Calle Corrientes 456'),
       ('Carlos', 'Rodr�guez', '34567890', '011-555-9876', 'carlos@email.com', 'Av. Santa Fe 789'),
       ('Laura', 'Fern�ndez', '45678901', '011-555-4321', 'laura@email.com', 'Calle Florida 987'),
       ('Pedro', 'L�pez', '56789012', '011-555-2468', 'pedro@email.com', 'Av. 9 de Julio 654'),
	   ('Ana', 'Garc�a', '12345678', '555-1111', 'ana@example.com', 'Calle de los Libros 456'),
	   ('Patricio', 'Malman', '98765432', '555-2222', 'carlos@example.com', 'Avenida de las Letras 789'),
	   ('Fernanda', 'Menotti', '54321098', '555-3333', 'laura@example.com', 'Plaza de la Cultura 101'),
	   ('Pablo', 'Bielsa', '87654321', '555-4444', 'pedro@example.com', 'Rinc�n de los Escritores 23'),
	   ('Susana', 'Donamara', '13579246', '555-5555', 'maria@example.com', 'Esquina de los Libros 7');
GO

INSERT INTO VENTAS (FechaVenta, IdCliente, TotalVenta)
VALUES ('2023-10-03', 1, 18740.18),
       ('2023-09-04', 2, 5100.90),
       ('2023-08-05', 3, 1704.85),
       ('2023-09-06', 1, 2492.70),
       ('2023-06-07', 2, 16405.98),
	   ('2024-01-06', 4, 7776),
	   ('2024-02-07', 6, 36514.48),
	   ('2024-03-08', 8, 4985.40),
	   ('2024-04-09', 10, 4704),
	   ('2024-05-10', 3, 70062.68)
GO


INSERT INTO DETALLE_VENTAS (IdVenta, IdArticulo, Cantidad)
VALUES	(1,12,1),
		(1,2,1),
		(1,6,1),
		(2,6,1),
		(2,8,1),
		(3,1,1),
		(3,12,1),
		(4,8,2),
		(5,2,1),
		(5,1,1),
		(5,11,2),
		(6,11,3),
		(7,5,1),
		(7,14,1),
		(7,10,2),
		(8,8,2),
		(9,10,2),
		(10,4,3),
		(10,1,1),
		(10,8,1)
GO

INSERT INTO DATOS_ARTICULOS (IdArticulo, Fecha, Stock, Precio)
VALUES	(1, '2022-01-15', 10, 899.99),
		(1, '2023-01-15', 10, 1099.99),
		(2, '2022-02-20', 8, 1050.00),
		(2, '2023-02-20', 8, 8300.00),
		(3, '2022-03-10', 15, 1380.00),
		(3, '2023-03-10', 15, 12580.00),
		(4, '2022-04-05', 7, 1100.00),
		(4, '2023-04-05', 7, 14200.00),
		(5, '2022-05-12', 20, 1500.00),
		(5, '2023-05-02', 20, 21299.99),
		(6, '2022-06-18', 5, 1000.00),
		(6, '2023-01-18', 5, 1890.00),
		(7, '2022-07-15', 12, 1250.00),
		(7, '2023-03-05', 12, 11300.00),
		(8, '2022-08-30', 6, 999.99),
		(8, '2023-03-30', 6, 1780.50),
		(9, '2022-09-14', 18, 1400.00),
		(9, '2023-02-14', 18, 3450.90),
		(10, '2022-01-22', 9, 749.90),
		(10, '2023-10-22', 9, 1680.00),
		(11, '2022-2-05', 25, 680.00),
		(11, '2023-11-05', 25, 1920.00),
		(12, '2022-12-10', 5, 1000.00),
		(12, '2023-1-10', 5, 2419.99),
		(13, '2022-01-18', 14, 950.50),
		(13, '2023-01-10', 14, 2890.00),
		(14, '2022-02-12', 6, 200.00),
		(14, '2023-02-10', 6, 1790.00),
		(15, '2022-03-25', 22, 14650.00),
		(15, '2023-03-25', 22, 28350.00),
		(16, '2022-04-6', 10, 1000.00),
		(16, '2023-04-8', 10, 36000.00),
		(17, '2022-05-15', 16, 1375.00),
		(17, '2023-05-15', 16, 4350.50),
		(18, '2022-06-1', 12, 1400.00),
		(18, '2023-06-1', 12, 7800.50)
GO

INSERT INTO USUARIOS(Usuario, Contraseña, Tipo)
VALUES	('Admin','admin',1),
		('Vendedor','vendedor',2)
GO
