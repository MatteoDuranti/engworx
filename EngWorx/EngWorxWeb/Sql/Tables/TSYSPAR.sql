CREATE TABLE TSYSPAR
(
  CODSYSPAR     VARCHAR2(30 BYTE)               NOT NULL,
  NUMSYSPARIDX  NUMBER(2)                       NOT NULL,
  DESPARVAL     VARCHAR2(50 BYTE),
  DESPARDES     VARCHAR2(100 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

COMMENT ON COLUMN TSYSPAR.CODSYSPAR IS 'Codice del parametro';

COMMENT ON COLUMN TSYSPAR.NUMSYSPARIDX IS 'Numero di indice del parametro. Vale sempre zero per i parametri semplici (interi, stringhe, ecc...). Si utilizzano numeri maggiori di zero in caso di array.';

COMMENT ON COLUMN TSYSPAR.DESPARVAL IS 'Valore del parametro in forma di stringa';

COMMENT ON COLUMN TSYSPAR.DESPARDES IS 'Descrizione del parametro';


