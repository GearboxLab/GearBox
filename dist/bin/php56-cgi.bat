@ECHO OFF

SET PHP_VERSION=php-5.6.34-Win32-VC11-x86

SET CURRENT_DIRECTRY=%~dp0
FOR %%X IN ("%CURRENT_DIRECTRY%.") DO SET GEARBOX_ROOT=%%~dpX

SET PHP_ROOT=%GEARBOX_ROOT%opt\%PHP_VERSION%
SET PHPRC=%GEARBOX_ROOT%etc\%PHP_VERSION%
SET PHP_INI_SCAN_DIR=%GEARBOX_ROOT%etc\%PHP_VERSION%\php.d

"%PHP_ROOT%\php-cgi.exe" %*