
set ARCHIVE=%CD%

:: Отделяем имя папки от пути.
for /d %%A in ("%ARCHIVE%") do set ARCHIVE=%%~nA

:: Удаляем папки obj и bin из всех проектов решения, а также файл *.suo и папки .vs, TestResults и packages, который содержит персональные настройки IDE.
for /d /r %%I in (*) do if /i "%%~nI"=="obj" rd /s /q "%%~I"
for /d /r %%I in (*)  do if /i "%%~nI"=="bin" rd /s /q "%%~I"
rd /s /q "%CD%\packages"
rd /s /q "%CD%\.vs"
rd /s /q "%CD%\TestResults"
del /q /ah "%CD%\%ARCHIVE%.suo"

:: Создаём архив.
chdir /d ..
"c:\program files\winrar\winrar.exe" a -r "%ARCHIVE%.zip" "%ARCHIVE%"

:: Убираем из архива батник(и).
"c:\program files\winrar\winrar.exe" d "%ARCHIVE%" *.bat

set ARCHIVE=