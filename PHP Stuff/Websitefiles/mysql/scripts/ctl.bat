@echo off
rem START or STOP Services
rem ----------------------------------
rem Check if argument is STOP or START

if not ""%1"" == ""START"" goto stop


"C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\mysql\bin\mysqld" --defaults-file="C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\mysql\bin\my.ini" --standalone
if errorlevel 1 goto error
goto finish

:stop
cmd.exe /C start "" /MIN call "C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\killprocess.bat" "mysqld.exe"

if not exist "C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\mysql\data\%computername%.pid" goto finish
echo Delete %computername%.pid ...
del "C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\mysql\data\%computername%.pid"
goto finish


:error
echo MySQL could not be started

:finish
exit
