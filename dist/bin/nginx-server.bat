@ECHO OFF

SET NGINX_VERSION=nginx-1.13.12

SET CURRENT_DIRECTRY=%~dp0
FOR %%X IN ("%CURRENT_DIRECTRY%.") DO SET GEARBOX_ROOT=%%~dpX

SET NSSM=%GEARBOX_ROOT%bin\nssm.exe
SET NGINX_BIN=%GEARBOX_ROOT%opt\%NGINX_VERSION%\nginx.exe
SET NGINX_PREFIX=%GEARBOX_ROOT%opt\%NGINX_VERSION%

IF "%~1"=="start"   GOTO start
IF "%~1"=="stop"    GOTO stop
IF "%~1"=="restart" GOTO restart
ECHO.
GOTO usage

:start
start "nginx" "%NGINX_BIN%" -p "%NGINX_PREFIX%"

GOTO end

:stop
"%NGINX_BIN%" -s quit -p "%NGINX_PREFIX%"

GOTO end

:restart
"%NGINX_BIN%" -s quit -p "%NGINX_PREFIX%"
start "nginx" "%NGINX_BIN%" -p "%NGINX_PREFIX%"

GOTO end

:usage
ECHO Usage:
ECHO     php-cgi-service.bat start
ECHO     php-cgi-service.bat stop
ECHO     php-cgi-service.bat restart
ECHO.

:end
