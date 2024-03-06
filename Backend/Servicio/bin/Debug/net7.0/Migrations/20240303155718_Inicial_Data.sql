SET IDENTITY_INSERT [dbo].[Ciudad] ON 
INSERT [dbo].[Ciudad] ([Id], [Descripcion], [Latitud], [Longitud], [Activo]) VALUES (1, N'La Plata', -34.92, -57.95, 1)
INSERT [dbo].[Ciudad] ([Id], [Descripcion], [Latitud], [Longitud], [Activo]) VALUES (2, N'Lima', -12.04, -77.02, 1)
INSERT [dbo].[Ciudad] ([Id], [Descripcion], [Latitud], [Longitud], [Activo]) VALUES (3, N'Alaska', 59.53, -151.23, 1)
INSERT [dbo].[Ciudad] ([Id], [Descripcion], [Latitud], [Longitud], [Activo]) VALUES (4, N'Asunción', -25.3, -57.79, 1)
SET IDENTITY_INSERT [dbo].[Ciudad] OFF

SET IDENTITY_INSERT [dbo].[TipoVehiculo] ON 
INSERT [dbo].[TipoVehiculo] ([Id], [Descripcion]) VALUES (1, N'Camión')
INSERT [dbo].[TipoVehiculo] ([Id], [Descripcion]) VALUES (2, N'Moto')
INSERT [dbo].[TipoVehiculo] ([Id], [Descripcion]) VALUES (3, N'Auto')
INSERT [dbo].[TipoVehiculo] ([Id], [Descripcion]) VALUES (4, N'Camioneta')
SET IDENTITY_INSERT [dbo].[TipoVehiculo] OFF

SET IDENTITY_INSERT [dbo].[Vehiculo] ON 
INSERT [dbo].[Vehiculo] ([Id], [Patente], [Marca], [Anio], [Tara], [IdTipoVehiculo], [Activo]) VALUES (1, N'ABC123', N'VOLVO', 2024, 1200, 1, 1)
INSERT [dbo].[Vehiculo] ([Id], [Patente], [Marca], [Anio], [Tara], [IdTipoVehiculo], [Activo]) VALUES (2, N'321CBA', N'YAMAHA', 2014, 6, 2, 1)
INSERT [dbo].[Vehiculo] ([Id], [Patente], [Marca], [Anio], [Tara], [IdTipoVehiculo], [Activo]) VALUES (3, N'AG458ZK', N'FIAT', 2024, 50, 3, 1)
SET IDENTITY_INSERT [dbo].[Vehiculo] OFF

SET IDENTITY_INSERT [dbo].[Viaje] ON 
INSERT [dbo].[Viaje] ([Id], [Fecha], [FechaAnterior], [Activo], [IdVehiculo], [IdCiudad]) VALUES (1, '2024-03-10', NULL, 1, 1, 3)
INSERT [dbo].[Viaje] ([Id], [Fecha], [FechaAnterior], [Activo], [IdVehiculo], [IdCiudad]) VALUES (2, '2024-03-12', NULL, 1, 3, 4)
INSERT [dbo].[Viaje] ([Id], [Fecha], [FechaAnterior], [Activo], [IdVehiculo], [IdCiudad]) VALUES (3, '2024-03-15', NULL, 1, 2, 2)
INSERT [dbo].[Viaje] ([Id], [Fecha], [FechaAnterior], [Activo], [IdVehiculo], [IdCiudad]) VALUES (4, '2024-03-11', NULL, 1, 2, 1)
SET IDENTITY_INSERT [dbo].[Viaje] OFF