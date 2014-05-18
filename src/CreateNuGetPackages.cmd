@echo off

if [%1]==[] goto usage

setlocal
    set project=PPWCode.Util.OddsAndEnds.II
	set package_project=
	if [%1]==[Debug] set package_project=1
	if [%1]==[Release] set package_project=1
	if defined package_project (
		echo Packiging %project% ...
		nuget restore -NoCache
		nuget pack .\II\%project%.csproj -Build -Properties "Configuration=%1"
	)
	
	set publish_package=
	if not [%2]==[] set publish_package=1
	if defined publish_package (
		if not exist %project%.%3.nupkg echo nuget package %project%.%3.nupkg not found
		if exist %project%.%3.nupkg (
			echo Publish %project%.%3.nupkg to source %2
			nuget push %project%.%3.nupkg -source %2
		)
	)
endlocal
goto eob

:usage
echo.
echo REQ p1=Specifiy configuration build (Debug, Release, ...)
echo OPT p2=Publish to nuget source (local, ...)
echo OPT p3=Version of package (1.0.0
echo.
echo Example:
echo   call CreateNuGetPackage Release local 1.0.0
echo.
:eob