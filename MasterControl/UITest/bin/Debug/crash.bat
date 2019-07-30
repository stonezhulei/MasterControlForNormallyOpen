@echo off
if not exist "log" (
    mkdir log
)

echo 数据收集中...
UITest.exe >> log\1.TXT
% UITest.exe > 1.TXT %
% start UITest.exe >> 1.TXT %