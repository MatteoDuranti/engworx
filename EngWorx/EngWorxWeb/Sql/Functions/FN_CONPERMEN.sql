CREATE OR REPLACE FUNCTION         FN_CONPERMEN(P_CODAZI IN NVARCHAR2, P_CODMAT IN NVARCHAR2, P_CODFUN IN NVARCHAR2 ) RETURN NUMBER IS
result NUMBER;
BEGIN
    select count(*)
    into result
    from (
        select distinct *
        from tfnc
        where codfncfat is not null
        start with codfnc IN (select distinct f.codfnc
                                        from tfnc f inner join trolfnc rf on F.codfnc=RF.codfnc
                                        where rf.codrol IN (select codrol from tusrrol where codcmpgrp = P_CODAZI and codusr =  P_CODMAT)
                                        and desctl IS NOT NULL
                                        and desactctl IS NOT NULL)
        connect by codfnc = prior codfncfat
    ) tab
    where tab.codfnc = P_CODFUN;
   RETURN result;
END FN_CONPERMEN;
/

SHOW ERRORS;


