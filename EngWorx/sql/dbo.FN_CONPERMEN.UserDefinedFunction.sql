USE [ENGWORXDB]
GO
/****** Object:  UserDefinedFunction [dbo].[FN_CONPERMEN]    Script Date: 19/12/2014 11:16:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[FN_CONPERMEN] 
(
	@CODUSR VARCHAR(6)
	,@CODFNC VARCHAR(5)
)
RETURNS int
AS
BEGIN
	DECLARE @IS_ADMIN int = 0;
	DECLARE @IS_GRANTED int = 0;

	SELECT @IS_ADMIN = COUNT(*)
	FROM tusrrol
	WHERE codusr = @CODUSR
	AND CODROL = 'ADM';

	IF(@IS_ADMIN = 1) BEGIN
      RETURN 1
	END;

	WITH permessi AS
	(
	SELECT F.*
		   ,F.codfnc
		   ,F.CODFNCFAT
	FROM tfnc AS F
	WHERE F.codfnc IN
		(
		SELECT DISTINCT F.codfnc
		FROM tfnc AS F
		INNER JOIN trolfnc RF ON F.codfnc = RF.codfnc
		WHERE RF.codrol IN (
			SELECT codrol
			FROM tusrrol
			WHERE codusr = @CODUSR
			)
		AND desctl IS NOT NULL
		AND desactctl IS NOT NULL
		)
	UNION ALL
	SELECT F.*
		   ,F.codfnc
		   ,F.CODFNCFAT
	FROM tfnc AS F
	INNER JOIN permessi AS A ON F.CODFNC = A.CODFNCFAT
	)
	SELECT @IS_GRANTED = COUNT (DISTINCT P.CODFNC)
	FROM permessi AS P
	WHERE P.CODFNC = @CODFNC;
	
	RETURN @IS_GRANTED;

END

GO
