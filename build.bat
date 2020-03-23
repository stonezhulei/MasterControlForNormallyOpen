@echo off
call "%VS100COMNTOOLS%vsvars32.bat"

if exist deploy  rmdir deploy /q /s


cd MasterControl

::MSBuild /t:Clean;Rebuild /p:Configuration=Release /m
msbuild -t:clean -p:configuration="release" -m -t:rebuild
@if not "%errorlevel%"=="0" goto error

cd ..
mkdir deploy

xcopy MasterControl\Config\*.ini              deploy\*.ini  /C /I /R /K /Y
xcopy MasterControl\UITest\bin\Release\*.exe  deploy\*.exe  /C /I /R /K /Y
xcopy MasterControl\UITest\bin\Release\*.dll  deploy\*.dll  /C /I /R /K /Y
xcopy MasterControl\UITest\bin\Release\*.pdb  deploy\*.pdb  /C /I /R /K /Y
@if "%errorlevel%"=="0" goto end

:error
@echo === build fail ===

:end
pause