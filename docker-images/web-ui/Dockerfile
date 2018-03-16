FROM microsoft/aspnet:4.6.2
WORKDIR /inetpub/wwwroot
EXPOSE 80
RUN md c:\aspnet-startup
COPY . c:/aspnet-startup
ENTRYPOINT ["powershell.exe", "c:\\aspnet-startup\\Startup.ps1"]