@echo off
cd /D %~dp0
cmd.exe /C start "" /MIN call "C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\killprocess.bat" "httpd.exe"
if not exist apache\logs\httpd.pid GOTO exit
del apache\logs\httpd.pid

:exit
