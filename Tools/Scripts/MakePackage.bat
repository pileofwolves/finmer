@echo off

if "%1"=="" goto ERR_ARG
set "STAGEDIR=%CD%\Staging"
set "INCLUDEDIR=%CD%\PackInclude"
set "GAMEDIR=%STAGEDIR%\Contents"
set "PACKNAME=%1"

rem Prepare staging directory
rmdir /S /Q %STAGEDIR%
mkdir %STAGEDIR%
mkdir %GAMEDIR%
pushd ..\..

rem Make xcopy exclusion list
echo .exp >> %STAGEDIR%\IgnoreList
echo .iobj >> %STAGEDIR%\IgnoreList
echo .ipdb >> %STAGEDIR%\IgnoreList
echo .lib >> %STAGEDIR%\IgnoreList
echo .dmp >> %STAGEDIR%\IgnoreList
echo .log >> %STAGEDIR%\IgnoreList

rem Copy Game artifacts, plus Modules (non-recursive to skip Samples)
xcopy /S /Y /EXCLUDE:%STAGEDIR%\IgnoreList Finmer.Game\bin\Release %GAMEDIR%
xcopy /I /Y Modules\*.furball %GAMEDIR%\Modules

rem Copy 'pack include' files
IF EXIST %INCLUDEDIR% (
	xcopy /S /Y %INCLUDEDIR%\* %GAMEDIR%
)

rem Extract PDBs for archival
move /Y %GAMEDIR%\x64\Lua51.pdb %STAGEDIR%\Lua51_x64.pdb
move /Y %GAMEDIR%\x86\Lua51.pdb %STAGEDIR%\Lua51_x86.pdb
move /Y %GAMEDIR%\Finmer.pdb %STAGEDIR%
move /Y %GAMEDIR%\Finmer.Core.pdb %STAGEDIR%

rem Make Game archive
7z a -bb0 -mmt -tzip %STAGEDIR%\%PACKNAME%.zip %GAMEDIR%\*

rem Copy Editor artifacts
xcopy /S /Y /EXCLUDE:%STAGEDIR%\IgnoreList Finmer.Editor\bin\Release %GAMEDIR%
move /Y %GAMEDIR%\Finmer.Editor.pdb %STAGEDIR%
move /Y %GAMEDIR%\Finmer.Core.pdb %STAGEDIR%

rem Make Game+Editor archive
7z a -bb0 -mmt -tzip %STAGEDIR%\%PACKNAME%-WithEditor.zip %GAMEDIR%\*

rem Delete the Contents folder, since its contents have now been archived
del %STAGEDIR%\IgnoreList
rmdir /S /Q %GAMEDIR%

popd
exit /B 0

:ERR_ARG
echo Usage: MakePackage.bat outputname
exit /B 1