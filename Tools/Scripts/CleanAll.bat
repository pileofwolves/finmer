pushd ..\..
rmdir /S /Q "Finmer.Core\bin"
rmdir /S /Q "Finmer.Core\obj"
rmdir /S /Q "Finmer.Editor\bin"
rmdir /S /Q "Finmer.Editor\obj"
rmdir /S /Q "Finmer.Game\bin"
rmdir /S /Q "Finmer.Game\obj"
rmdir /S /Q "External\lua\bin"
rmdir /S /Q "External\lua\obj"
rmdir /S /Q "Tools\Scripts\Staging"
rmdir /S /Q ".vs"
rmdir /S /Q "packages"
del /F /S *.user
del /F /S *.sav
popd