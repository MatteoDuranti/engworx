USE [ENGWORXDB]
GO
/****** Object:  View [dbo].[VMEN]    Script Date: 19/12/2014 11:16:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*ORDER BY CODUSR, SORPAT*/
CREATE VIEW [dbo].[VMEN]
AS
WITH menu AS (SELECT        U.CODUSR, F.CODFNC, F.CODFNCFAT, F.DESFNC, F.DESACTCTL, F.DESCTL, F.CODODR, 1 AS CODLVL, CAST(RIGHT({ fn CONCAT(REPLICATE('0', 2), CAST(F.CODODR AS VARCHAR(2))) }, 2) 
                                                            AS VARCHAR(20)) AS SORPAT, dbo.FN_CONPERMEN(U.CODUSR, F.CODFNC) AS PERMEN
                                   FROM            dbo.TFNC AS F CROSS JOIN
                                                            dbo.TUSR AS U
                                   WHERE        (F.CODFNCFAT IS NULL)
                                   UNION ALL
                                   SELECT        U.CODUSR, F.CODFNC, F.CODFNCFAT, F.DESFNC, F.DESACTCTL, F.DESCTL, F.CODODR, M.CODLVL + 1 AS Expr1, CAST({ fn CONCAT(M.SORPAT, RIGHT({ fn CONCAT(REPLICATE('0', 2), 
                                                            CAST(F.CODODR AS VARCHAR(2))) }, 2)) } AS VARCHAR(20)) AS SORPAT, dbo.FN_CONPERMEN(U.CODUSR, F.CODFNC) AS PERMEN
                                   FROM            dbo.TFNC AS F CROSS JOIN
                                                            dbo.TUSR AS U INNER JOIN
                                                            menu AS M ON F.CODFNCFAT = M.CODFNC AND U.CODUSR = M.CODUSR
                                   WHERE        (F.CODFNCFAT IS NOT NULL))
    SELECT        ISNULL(CODUSR, '') AS CODUSR, ISNULL(CODFNC, '') AS CODFNC, CODFNCFAT, DESFNC, DESCTL, DESACTCTL, CODLVL, CODODR, SORPAT
     FROM            menu AS M
     WHERE        (PERMEN = 1)

GO
