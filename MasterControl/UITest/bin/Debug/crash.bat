@echo off
if not exist "log" (
    mkdir log
)

echo �����ռ���...
MasterControl >> log\1.TXT
% MasterControl > 1.TXT %
% start MasterControl >> 1.TXT %