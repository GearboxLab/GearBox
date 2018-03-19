@ECHO OFF

IF "%1" == "use" IF "%2" == "php56" GOTO php56
IF "%1" == "use" IF "%2" == "php71" GOTO php71
GOTO error

:error
ECHO Error PHP version! (Currently support php56, php71)
GOTO end

:php56
ECHO Chnage to use php56
COPY /Y "%~dp0php56.bat" "%~dp0php.bat"
GOTO end

:php71
ECHO Chnage to use php71
COPY /Y "%~dp0php71.bat" "%~dp0php.bat"
GOTO end

:end
