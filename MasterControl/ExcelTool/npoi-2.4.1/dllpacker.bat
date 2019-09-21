@echo off

%当前目录%
echo currDir=%CD%

%工作目录%
echo workDir=%~dp0


cd /d %~dp0

if not exist "Excel.dll" (
%托管dll合并%
ilmerge.exe /ndebug /target:dll /targetplatform:v4 /out:Excel.dll NPOI.dll /log ICSharpCode.SharpZipLib.dll NPOI.OOXML.dll NPOI.OpenXml4Net.dll NPOI.OpenXmlFormats.dll
)

@echo on

pause