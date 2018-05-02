@ECHO OFF

SET CURRENT_DIRECTRY=%~dp0
FOR %%X IN ("%CURRENT_DIRECTRY%.") DO SET GEARBOX_ROOT=%%~dpX
SET GEARBOX_ROOT=%GEARBOX_ROOT:~0,-1%

SET NSSM=%GEARBOX_ROOT%\bin\nssm.exe

IF NOT -%1-==-- IF NOT -%2-==-- GOTO command
GOTO usage

:command
SET COMMAND_NAME=%~1
SET SERVICE_NAME=%~2

IF "%COMMAND_NAME%"=="install"   GOTO install
IF "%COMMAND_NAME%"=="uninstall" GOTO uninstall
IF "%COMMAND_NAME%"=="start"     GOTO start
IF "%COMMAND_NAME%"=="stop"      GOTO stop
IF "%COMMAND_NAME%"=="restart"   GOTO restart
ECHO.
GOTO usage

:install
IF -%3-==-- GOTO usage
IF -%4-==-- GOTO usage

net stop %SERVICE_NAME%
"%NSSM%" remove "%SERVICE_NAME%" confirm
"%NSSM%" install "%SERVICE_NAME%" "%GEARBOX_ROOT%\bin\%~3-cgi.bat" "-b %~4"

ECHO.
GOTO end

:uninstall
net stop "%SERVICE_NAME%"
"%NSSM%" remove "%SERVICE_NAME%" confirm

ECHO.
GOTO end

:start
net start "%SERVICE_NAME%"

GOTO end

:stop
net stop "%SERVICE_NAME%"

GOTO end

:restart
net stop "%SERVICE_NAME%"
net start "%SERVICE_NAME%"

GOTO end

:usage
ECHO.
ECHO Usage:
ECHO     php-cgi-service.bat install ^<service-name^> ^<php-version^> ^<address:port^>^|^<port^>
ECHO     php-cgi-service.bat uninstall ^<service-name^>
ECHO     php-cgi-service.bat start ^<service-name^>
ECHO     php-cgi-service.bat stop ^<service-name^>
ECHO     php-cgi-service.bat restart ^<service-name^>
ECHO.
ECHO Arguments:
ECHO     ^<service-name^>: Service name (ex: GearBoxPhp71)
ECHO     ^<php-version^>: PHP version (php56 or php71)
ECHO     ^<address:port^>: PHP cgi ip address and port number (ex: 127.0.0.1:19971)
ECHO.

:end
