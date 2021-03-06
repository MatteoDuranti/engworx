﻿CREATE TABLE TGRPARE
(
  CODGRPARE  VARCHAR2(3 BYTE)                   NOT NULL,
  DESGRPARE  VARCHAR2(100 BYTE),
  CODUSRMOD  VARCHAR2(8 BYTE),
  FLGMODTYP  CHAR(1 BYTE),
  TMSLSTMOD  TIMESTAMP(6)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

COMMENT ON COLUMN TGRPARE.CODGRPARE IS 'Codice univoco dell''area di raggruppamento';

COMMENT ON COLUMN TGRPARE.DESGRPARE IS 'Descrizione dell''area di raggruppamento';

COMMENT ON COLUMN TGRPARE.CODUSRMOD IS 'Codice utente modificatore';

COMMENT ON COLUMN TGRPARE.FLGMODTYP IS 'Tipo di modifica';

COMMENT ON COLUMN TGRPARE.TMSLSTMOD IS 'Data di modifica';


