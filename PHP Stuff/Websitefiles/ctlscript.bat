@echo off
rem START or STOP Services
rem ----------------------------------
rem Check if argument is STOP or START

if not ""%1"" == ""START"" goto stop

if exist C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\hypersonic\scripts\ctl.bat (start /MIN /B C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\server\hsql-sample-database\scripts\ctl.bat START)
if exist C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\ingres\scripts\ctl.bat (start /MIN /B C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\ingres\scripts\ctl.bat START)
if exist C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\mysql\scripts\ctl.bat (start /MIN /B C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\mysql\scripts\ctl.bat START)
if exist C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\postgresql\scripts\ctl.bat (start /MIN /B C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\postgresql\scripts\ctl.bat START)
if exist C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\apache\scripts\ctl.bat (start /MIN /B C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\apache\scripts\ctl.bat START)
if exist C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\openoffice\scripts\ctl.bat (start /MIN /B C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\openoffice\scripts\ctl.bat START)
if exist C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\apache-tomcat\scripts\ctl.bat (start /MIN /B C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\apache-tomcat\scripts\ctl.bat START)
if exist C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\resin\scripts\ctl.bat (start /MIN /B C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\resin\scripts\ctl.bat START)
if exist C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\jetty\scripts\ctl.bat (start /MIN /B C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\jetty\scripts\ctl.bat START)
if exist C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\subversion\scripts\ctl.bat (start /MIN /B C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\subversion\scripts\ctl.bat START)
rem RUBY_APPLICATION_START
if exist C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\lucene\scripts\ctl.bat (start /MIN /B C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\lucene\scripts\ctl.bat START)
if exist C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\third_application\scripts\ctl.bat (start /MIN /B C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\third_application\scripts\ctl.bat START)
goto end

:stop
echo "Stopping services ..."
if exist C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\third_application\scripts\ctl.bat (start /MIN /B C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\third_application\scripts\ctl.bat STOP)
if exist C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\lucene\scripts\ctl.bat (start /MIN /B C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\lucene\scripts\ctl.bat STOP)
rem RUBY_APPLICATION_STOP
if exist C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\subversion\scripts\ctl.bat (start /MIN /B C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\subversion\scripts\ctl.bat STOP)
if exist C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\jetty\scripts\ctl.bat (start /MIN /B C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\jetty\scripts\ctl.bat STOP)
if exist C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\hypersonic\scripts\ctl.bat (start /MIN /B C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\server\hsql-sample-database\scripts\ctl.bat STOP)
if exist C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\resin\scripts\ctl.bat (start /MIN /B C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\resin\scripts\ctl.bat STOP)
if exist C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\apache-tomcat\scripts\ctl.bat (start /MIN /B /WAIT C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\apache-tomcat\scripts\ctl.bat STOP)
if exist C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\openoffice\scripts\ctl.bat (start /MIN /B C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\openoffice\scripts\ctl.bat STOP)
if exist C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\apache\scripts\ctl.bat (start /MIN /B C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\apache\scripts\ctl.bat STOP)
if exist C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\ingres\scripts\ctl.bat (start /MIN /B C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\ingres\scripts\ctl.bat STOP)
if exist C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\mysql\scripts\ctl.bat (start /MIN /B C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\mysql\scripts\ctl.bat STOP)
if exist C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\postgresql\scripts\ctl.bat (start /MIN /B C:\Users\devbeebee\Desktop\MLAPI_Tests\Unity3D-MLAPI\PHP Stuff\Websitefiles\postgresql\scripts\ctl.bat STOP)

:end

