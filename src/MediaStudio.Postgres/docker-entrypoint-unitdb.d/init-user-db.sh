#!/bin/bash

set -e

psql -v ONN_ERROR_STOP=1 --username "$POSTGRES_USER" <<-EOSQL
create role $POSTGRES_DB_USER with login password '$POSTGRES_DB_PASS' superuser;

CREATE DATABASE $POSTGRES_DB_NAME
  WITH OWNER = $POSTGRES_DB_USER
       ENCODING = 'UTF8'
	   LL_COLLATE = 'en_US.utf8'
	   LL_CTYPE = 'en_US.utf8'
	   connection limit = -1;
	   
alter database $POSTGRES_DB_NAME set time zone 'Europe/Moscow';

grant all on database $POSTGRES_DB_NAME to $POSTGRES_DB_NAME;
revoke all on database $POSTGRES_DB_NAME from public;
revoke create on shema public from public;

\с $POSTGRES_DB_USER;

CREATE EXTENSION btree_gist;
CREATE EXTENSION tablefunc;
CREATE EXTENSION IF NOT EXIST "uuid-ossp";

EOSQL

{ echo "host all all 0.0.0.0/0 trust";} >> "$POSTGES_DATA/pg_hba.conf"