USE [ENGWORXDB]
GO
/****** Object:  User [engworxuser]    Script Date: 19/12/2014 11:16:48 ******/
CREATE USER [engworxuser] FOR LOGIN [engworxuser] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [engworxuser]
GO
