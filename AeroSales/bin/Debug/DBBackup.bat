SET PGPASSWORD=a

cd /D C:\Program Files

cd PostgreSQL

cd 13

cd bin

pg_dump.exe --host localhost --port 5432 --username postgres --format custom --blobs --verbose --file "C:\Qt\BACK.backup" "AeroSales"


