SET PGPASSWORD=a

cd /D C:\Program Files

cd PostgreSQL

cd 13

cd bin

pg_restore.exe --host localhost --port 5432 --username postgres --dbname "AeroSalesRestore" --verbose "C:\Qt\BACK.backup"
