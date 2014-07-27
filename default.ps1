# Copyright 2014 by PeopleWare n.v..
# 
# Licensed under the Apache License, Version 2.0 (the "License");
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at
# 
# http://www.apache.org/licenses/LICENSE-2.0
# 
# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an "AS IS" BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License.

#
# psake build file
#

Framework "4.0"

Properties {
    $configuration = "Debug"
    $repos = @("local", "nuget")
    $publishrepo = "local"
}

# Properties description
function PropertyDocumentation() {
    Write-Host "Properties"
    Write-Host "----------"
    Write-Host "`$configuration   The build configuration."
    Write-Host "`$repos           Array of repositories to use as source repositories."
    Write-Host "`$publishrepo     The repository to use for publishing generated packages."
}

# Default is help
Task Default -depends ?

# Show help
Task ? -description "Show help." {
    Write-Host
    PropertyDocumentation 
    WriteDocumentation
}

# Clean ReSharper cache folder
Task ReSharperClean -description "Clean ReSharper cache folder in the solution." {
    Push-Location

    try
    {
        #Write-Host "Clean ReSharper cache." -ForegroundColor Green

        # clean up cache folder
        Remove-Item -Path 'src\_ReSharper*' -Recurse -Force -ErrorAction Ignore
    }
    finally
    {
        Pop-Location
    }
}

# Clean packages folder
Task PackageClean -description "Clean packages folder in the solution." -depends ReSharperClean {
    Push-Location

    try
    {
        #Write-Host "Clean package dependencies." -ForegroundColor Green

        # clean up packages folder
        Remove-Item -Path 'src\packages\*' -Exclude 'repositories.config' -Recurse -Force -ErrorAction Ignore
    }
    finally
    {
        Pop-Location
    }
}

# Restore packages
Task PackageRestore -description "Restore nuget package dependencies." -depends PackageClean {
    Push-Location

    try
    {
        #Write-Host "Restoring package dependencies." -ForegroundColor Green

        Set-Location 'src'
        $solution = Get-Item '*.sln' | Select-Object -First 1
        $reposources = ""
        $repos | ForEach-Object { $reposources="$reposources -source $_" } 
        #Exec { & nuget "restore $solution $reposources" }
        $args="restore $solution $reposources"
        Start-Process -Wait -FilePath nuget -ArgumentList $args -NoNewWindow
    }
    finally
    {
        Pop-Location
    }
}

# Build the solution
Task Build -description "Build the solution." -depends Clean {
    Push-Location

    try
    {
        #Write-Host "Compiling." -ForegroundColor Green

        Set-Location 'src'
        $solution = Get-Item '*.sln' | Select-Object -First 1
        Exec { msbuild "$($solution.Name)" /t:Build /m /p:Configuration=$configuration /v:quiet /nologo }
    }
    finally
    {
        Pop-Location
    }
}

# Clean build artifacts and temporary files
Task Clean -description "Clean build output and generated packages." {
    Push-Location

    try
    {
        #Write-Host "Cleaning." -ForegroundColor Green

        # msbuild clean
        Set-Location 'src'
        $solution = Get-Item '*.sln' | Select-Object -First 1
        Exec { msbuild "$($solution.Name)" /t:Clean /m /p:Configuration=$configuration /v:quiet /nologo }
        Set-Location '..'

        # clean up scratch folder
        Remove-Item -Path 'scratch' -Recurse -Force -ErrorAction Ignore

        # clean up bin/obj folders
        Set-Location 'src'
        Remove-Item -Path '*\bin','*\obj' -Recurse -Force -ErrorAction Ignore

        # clean up packages folder
        Get-ChildItem -Directory | Where-Object { $_.Name -ne 'packages'  } |  Get-ChildItem -Filter '*.nupkg' -recurse | Remove-Item -Force -ErrorAction Ignore
    }
    finally
    {
        Pop-Location
    }
}

# Full clean
Task RealClean -description "Clean build output, generated packages and packages folder." -depends Clean,PackageClean

# Full build
Task FullBuild -description "Do a full build starting from a clean solution." -depends PackageRestore,Build

# Create Nuget package
Task Package -description "Generate the packages from a clean build, and publish the packages." -depends FullBuild {
    Push-Location

    try
    {
        #Write-Host "Packaging." -ForegroundColor Green

        Set-Location 'src'
        $solution = Get-Item '*.sln' | Select-Object -First 1

        # build
        $nuspecfiles = Get-ChildItem -Directory | Where-Object { $_.Name -ne 'packages'  } |  Get-ChildItem -Filter '*.nuspec' -recurse
        $nuspecfiles | ForEach-Object {
            Set-Location $_.DirectoryName
            Exec { nuget pack "$($_.BaseName).csproj" -Build -Properties "Configuration=$configuration" -Verbosity quiet }
            $nupkgfiles = Get-ChildItem -File -Filter "*.nupkg"
            Exec { nuget push "$($nupkgfiles[0].Name)" -source $publishrepo }
        }
    }
    finally
    {
        Pop-Location
    }
}
