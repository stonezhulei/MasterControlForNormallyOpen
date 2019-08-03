@echo off
call "%VS100COMNTOOLS%vsvars32.bat"

if exist "deploy" (
	rmdir deploy /q/s
)

cd MasterControl

:MSBuild /t:Clean;Rebuild /p:Configuration=Release /m
msbuild -t:clean -p:configuration="release" -m -t:rebuild

cd ..
mkdir deploy
if exist "deploy" (
	xcopy MasterControl\Config\*.ini deploy\*.ini
	xcopy MasterControl\UITest\bin\Release\*.exe deploy\*.exe
	xcopy MasterControl\UITest\bin\Release\*.dll deploy\*.dll
	xcopy MasterControl\UITest\bin\Release\*.pdb deploy\*.pdb
)

@echo on

pause