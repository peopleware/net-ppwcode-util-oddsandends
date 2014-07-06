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

Task Default -depends ?

Task ? {
    WriteDocumentation
}

# Clean packages folder
Task PackageClean {
    Push-Location

    try
    {
        Write-Host "Clean package dependencies." -ForegroundColor Green

        # clean up packages folder
        Remove-Item -Path 'src\packages\*' -Exclude 'repositories.config' -Recurse -Force -ErrorAction Ignore
    }
    finally
    {
        Pop-Location
    }
}

# Restore packages
Task PackageRestore -depends PackageClean {
    Push-Location

    try
    {
        Write-Host "Restoring package dependencies." -ForegroundColor Green

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
Task Build -depends Clean {
    Push-Location

    try
    {
        Set-Location 'src'
        Write-Host "Compiling." -ForegroundColor Green
        $solution = Get-Item '*.sln' | Select-Object -First 1
        Exec { msbuild "$($solution.Name)" /t:Build /m /p:Configuration=$configuration /v:quiet /nologo }
    }
    finally
    {
        Pop-Location
    }
}

# Clean build artifacts and temporary files
Task Clean {
    Push-Location

    try
    {
        Write-Host "Cleaning." -ForegroundColor Green

        # msbuild clean
        Write-Host "Cleaning solution" -ForegroundColor Green
        Set-Location 'src'
        $solution = Get-Item '*.sln' | Select-Object -First 1
        Exec { msbuild "$($solution.Name)" /t:Clean /m /p:Configuration=$configuration /v:quiet /nologo }
        Set-Location '..'

        # clean up scratch folder
        Write-Host "Cleaning scratch" -ForegroundColor Green
        Remove-Item -Path 'scratch' -Recurse -Force -ErrorAction Ignore

        # clean up bin/obj folders
        Write-Host "Cleaning bin/obj" -ForegroundColor Green
        Set-Location 'src'
        Remove-Item -Path '*\bin','*\obj' -Recurse -Force -ErrorAction Ignore

        # clean up packages folder
        Write-Host "Cleaning packages" -ForegroundColor Green
        Get-ChildItem -Directory | Where-Object { $_.Name -ne 'packages'  } |  Get-ChildItem -Filter '*.nupkg' -recurse | Remove-Item -Force -ErrorAction Ignore
    }
    finally
    {
        Pop-Location
    }
}

# Full clean
Task RealClean -depends Clean,PackageClean

# Full build
Task FullBuild -depends PackageRestore,Build

# Create Nuget package
Task Package -depends FullBuild {
    Push-Location

    try
    {
        Write-Host "Packaging." -ForegroundColor Green

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
