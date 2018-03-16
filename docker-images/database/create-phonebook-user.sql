USE [master]
GO

IF EXISTS(SELECT [name] FROM master.sys.server_principals WHERE name = 'phonebook-user')
	BEGIN
		PRINT 'user already exists...deleting';
		DROP LOGIN [phonebook-user]
	END
GO

CREATE LOGIN [phonebook-user]
WITH
	PASSWORD=N'T0uchpap3r',
	DEFAULT_DATABASE=[master],
	DEFAULT_LANGUAGE=[us_english],
	CHECK_EXPIRATION=OFF,
	CHECK_POLICY=OFF
GO

ALTER LOGIN [phonebook-user] ENABLE
GO

ALTER SERVER ROLE [dbcreator] ADD MEMBER [phonebook-user]
GO