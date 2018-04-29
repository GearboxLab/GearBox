@ECHO OFF

net stop GearBoxPhp71
C:\GearBox\bin\nssm remove GearBoxPhp71 confirm
C:\GearBox\bin\nssm install GearBoxPhp71 C:\GearBox\bin\php71-cgi.bat "-b 127.0.0.1:19971"
net stop GearBoxPhp71
net start GearBoxPhp71

net stop GearBoxPhp56
C:\GearBox\bin\nssm remove GearBoxPhp56 confirm
C:\GearBox\bin\nssm install GearBoxPhp56 C:\GearBox\bin\php71-cgi.bat "-b 127.0.0.1:19971"
net stop GearBoxPhp56
net start GearBoxPhp56

PAUSE
