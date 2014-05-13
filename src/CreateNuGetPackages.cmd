@echo off

if [%1]==[] goto usage

setlocal
	set package_project=
	if [%1]==[Debug] set package_project=1
	if [%1]==[Release] set package_project=1
	if defined package_project (
		echo Packiging PPWCode.Util.OddsAndEnds.II ...
		nuget pack .\II\PPWCode.Util.OddsAndEnds.II.csproj -Build -Properties "Configuration=%1"
	)
endlocal
goto eob

:usage
echo.
echo REQ p1=Specifiy configuration build (Debug, Release, ...)
echo.
echo Example:
echo   call CreateNuGetPackage Release
echo.
:eob