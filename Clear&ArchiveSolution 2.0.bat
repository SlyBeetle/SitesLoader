
set ARCHIVE=%CD%

:: �������� ��� ����� �� ����.
for /d %%A in ("%ARCHIVE%") do set ARCHIVE=%%~nA

:: ������� ����� obj � bin �� ���� �������� �������, � ����� ���� *.suo � ����� .vs, TestResults � packages, ������� �������� ������������ ��������� IDE.
for /d /r %%I in (*) do if /i "%%~nI"=="obj" rd /s /q "%%~I"
for /d /r %%I in (*)  do if /i "%%~nI"=="bin" rd /s /q "%%~I"
rd /s /q "%CD%\packages"
rd /s /q "%CD%\.vs"
rd /s /q "%CD%\TestResults"
del /q /ah "%CD%\%ARCHIVE%.suo"

:: ������ �����.
chdir /d ..
"c:\program files\winrar\winrar.exe" a -r "%ARCHIVE%.zip" "%ARCHIVE%"

:: ������� �� ������ ������(�).
"c:\program files\winrar\winrar.exe" d "%ARCHIVE%" *.bat

set ARCHIVE=