#wait for the SQL Server to come up
sleep 10s

echo "running set up script"
#run the setup script to create the DB and the schema in the DB
/opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P MyPass@word -d master -No -i cubicfox-db-init.sql