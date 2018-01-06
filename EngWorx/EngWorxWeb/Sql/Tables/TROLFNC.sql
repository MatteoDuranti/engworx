﻿CREATE TABLE TROLFNC
(
  CODROL     VARCHAR2(3 BYTE)                   NOT NULL,
  CODFNC     VARCHAR2(5 BYTE)                   NOT NULL,
  DATASC     TIMESTAMP(6),
  CODUSRMOD  VARCHAR2(8 BYTE),
  FLGMODTYP  CHAR(1 BYTE),
  TMSLSTMOD  TIMESTAMP(6)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

COMMENT ON COLUMN TROLFNC.CODROL IS 'Codice univoco del ruolo';

COMMENT ON COLUMN TROLFNC.CODFNC IS 'Codice univoco della funzione';

COMMENT ON COLUMN TROLFNC.DATASC IS 'Data associazione';

COMMENT ON COLUMN TROLFNC.CODUSRMOD IS 'Codice utente modificatore';

COMMENT ON COLUMN TROLFNC.FLGMODTYP IS 'Tipo di modifica';

COMMENT ON COLUMN TROLFNC.TMSLSTMOD IS 'Data di modifica';


