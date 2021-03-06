﻿CREATE USER ENGWORX
  IDENTIFIED BY VALUES '2DA31B2AD7882166'
  DEFAULT TABLESPACE USERS
  TEMPORARY TABLESPACE TEMP
  PROFILE DEFAULT
  ACCOUNT UNLOCK;
  -- 2 Roles for ENGWORX 
  GRANT CONNECT TO ENGWORX;
  GRANT RESOURCE TO ENGWORX;
  ALTER USER ENGWORX DEFAULT ROLE ALL;
  -- 4 System Privileges for ENGWORX 
  GRANT UNLIMITED TABLESPACE TO ENGWORX;
  GRANT CREATE VIEW TO ENGWORX;
  GRANT CREATE ANY DIRECTORY TO ENGWORX;
  GRANT DROP ANY DIRECTORY TO ENGWORX;


