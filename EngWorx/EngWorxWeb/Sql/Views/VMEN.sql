CREATE OR REPLACE VIEW VMEN
AS 
SELECT u.codcmpgrp,
          u.codusr,
          test.codlvl,
          f.codfnc,
          test.codfncfat,
          test.desfnc,
          test.desctl,
          test.desactctl
     FROM (           SELECT LEVEL codlvl, tab.*
                        FROM (SELECT tusr.codcmpgrp || tusr.codusr codute,
                                     tfnc.codfnc,
                                     tfnc.codfncfat,
                                     tfnc.desfnc,
                                     tfnc.desctl,
                                     tfnc.desactctl,
                                     tfnc.cododr,
                                     FN_CONPERMEN (tusr.codcmpgrp,
                                                           tusr.codusr,
                                                           tfnc.codfnc)
                                        permessomenu
                                FROM tusr, tfnc) tab
                       WHERE permessomenu = 1
                  START WITH tab.codfncfat = '000'
                  CONNECT BY PRIOR tab.codfnc = tab.codfncfat
                             AND PRIOR tab.codute = tab.codute
           ORDER SIBLINGS BY tab.codute, tab.cododr) test
          INNER JOIN TUSR u
             ON test.codute = u.codcmpgrp || u.codusr
          INNER JOIN TFNC f
             ON test.codfnc = f.codfnc;


