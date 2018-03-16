FROM microsoft/mssql-server-windows-developer
ADD create-phonebook-user.sql /initial-scripts/create-phonebook-user.sql
ADD run-scripts.bat /initial-scripts/run-scripts.bat
RUN /initial-scripts/run-scripts.bat
