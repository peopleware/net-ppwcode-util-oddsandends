###############################################################################
###  PSAKE build script                                                     ###
###############################################################################
###  Copyright 2015 by PeopleWare n.v.                                      ###
###############################################################################
###  Authors: Ruben Vandeginste                                             ###
###############################################################################
###                                                                         ###
###  A psake build file that configures                                     ###
###     - build and clean the solution                                      ###
###     - restore nuget packages                                            ###
###     - create and publish nuget packages                                 ###
###     - run unit tests                                                    ###
###     - generate documentation                                            ###
###                                                                         ###
###############################################################################
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


#region CONFIGURATION

###############################################################################
### CONFIGURATION                                                           ### 
###############################################################################

###############################################################################
# Use build tools from .NET framework 4.0
#
Framework '4.0'

###############################################################################
# Properties that are used by this psake
# script; these can be overridden when
# invoking this script
#
Properties {
    # buildconfig: the configuration used for building 
    $buildconfig = 'Debug'
    # repos: ordered array of nuget repositories to use for fetching packages
    $repos = @("local", "nuget")
    # publishrepo: nuget repository to use for publishing a new package
    $publishrepo = 'local'
    # chatter: number to indicate verbosity during the execution of tasks
    #   0 - no output
    #   1 - minimal output, tasks executed
    #   2 - extra output, steps within tasks
    #   3 - even more output
    $chatter = 1
    # chattercolor: foreground color to use for task output
    $chattercolor = 'Green'
    # nugetcache: by default, disable the nuget cache
    $usenugetcache = $false
}

#endregion

#region PRIVATE HELPERS

###############################################################################
### PRIVATE HELPERS                                                         ###
###############################################################################

###############################################################################
# Helper for printing out documentation
#
function PropertyDocumentation() {
    Write-Host 'Properties'
    Write-Host '----------'
    Write-Host "`$buildconfig     Build configuration."
    Write-Host "`$repos           Ordered array of repositories to use as nuget source repositories."
    Write-Host "`$publishrepo     The repository to use for publishing generated nuget packages."
    Write-Host "`$chatter         The level of verbosity during task execution."
    Write-Host  '             0 - No output'
    Write-Host  '             1 - Minimal output, tasks executed'
    Write-Host  '             2 - Extra output, steps within tasks'
    Write-Host  '             3 - Even more output, debug execution'
    Write-Host "`$chattercolor    Foreground color to use for task output."
    Write-Host "`$usenugetcache   Boolean to indicate whether the NuGet built-in cache should be used (default=$false)."
}

###############################################################################
# Helper for chatter
#
function Chatter {
    param
    (
        [string]
        $msg = '.',

        [int]
        $level = 3
    )

    if ($level -le $chatter) {
        Write-Host $msg -ForegroundColor $chattercolor
    }
}

#endregion


#region TASKS

###############################################################################
### TASKS                                                                   ###
###############################################################################

###############################################################################
# Default task, executed when no task is
# given explicitly
#
Task Default -depends ?

###############################################################################
# Help
#
Task ? -description 'Show help.' {
    Write-Host
    PropertyDocumentation 
    WriteDocumentation
}

###############################################################################
# Clean ReSharper cache folder
#
Task ReSharperClean -description 'Clean ReSharper cache folder in the solution.' {

    Chatter 'Clean ReSharper cache.' 1

    Push-Location
    try
    {
        # clean up cache folder
        Remove-Item -Path 'src\_ReSharper*' -Recurse -Force
    }
    finally
    {
        Pop-Location
    }
}

###############################################################################
# Clean packages folder
#
Task PackageClean -description 'Clean packages folder in the solution.' -depends ReSharperClean {

    Chatter 'Clean package dependencies.' 1

    Push-Location
    try
    {
        # clean up packages folder
        Remove-Item -Path 'src\packages\*' -Exclude 'repositories.config' -Recurse -Force
    }
    finally
    {
        Pop-Location
    }
}

###############################################################################
# Restore packages
#
Task PackageRestore -description 'Restore nuget package dependencies.' -depends PackageClean {

    Chatter 'Restoring package dependencies.' 1

    Push-Location
    try
    {
        Set-Location 'src'
        $solution = Get-Item '*.sln' | Select-Object -First 1
        $reposources = ''
        $repos | ForEach-Object { $reposources="$reposources -source $_" } 

        $nocache = ''
        if (-not $usenugetcache) {
            $nocache = '-NoCache'
        }

        Chatter "nuget restore $solution $reposources" 3
        Exec { Invoke-Expression "nuget restore $solution $reposources $nocache -NonInteractive -Verbosity quiet" }
    }
    finally
    {
        Pop-Location
    }
}

###############################################################################
# Build the solution
#
Task Build -description 'Build the solution.' -depends Clean {

    Chatter 'Building the solution.' 1
    
    Push-Location
    try
    {
        Set-Location 'src'
        $solution = Get-Item '*.sln' | Select-Object -First 1
        Chatter "MSBuild: $($solution.Name) /t:Build /m /nr:false /p:Configuration=$buildconfig /v:quiet /nologo." 3
        Exec { msbuild "$($solution.Name)" /t:Build /m /nr:false /p:Configuration=$buildconfig /v:quiet /nologo }
    }
    finally
    {
        Pop-Location
    }
}

###############################################################################
# Clean build artifacts and temporary files
#
Task Clean -description 'Clean build output and generated packages.' {

    Chatter 'Cleaning.' 1

    Push-Location
    try
    {
        # msbuild clean
        Chatter 'Cleaning solution' 2
        Set-Location 'src'
        $solution = Get-Item '*.sln' | Select-Object -First 1
        Chatter "MSBuild: $($solution.Name) /t:Clean /m /nr:false /p:Configuration=$buildconfig /v:quiet /nologo." 3
        Exec { msbuild "$($solution.Name)" /t:Clean /m /nr:false /p:Configuration=$buildconfig /v:quiet /nologo }
        Set-Location '..'

        # clean up scratch folder
        Chatter 'Cleaning scratch' 2
        Remove-Item -Path 'scratch' -Recurse -Force

        # clean up bin/obj folders
        Chatter 'Cleaning bin/obj' 2
        Set-Location 'src'
        Remove-Item -Path '*\bin','*\obj' -Recurse -Force
        Set-Location '..'
        
        # clean up generated nuget packages
        Chatter 'Cleaning generated packages' 2
        Set-Location 'src'
        Get-ChildItem -Directory |
            Where-Object { $_.Name -ne 'packages'  } |
            Get-ChildItem -Filter '*.nupkg' -recurse |
            Foreach-Object { Remove-Item -LiteralPath $_.FullName }
        Set-Location '..'
    }
    finally
    {
        Pop-Location
    }
}

###############################################################################
# Full clean
#
Task FullClean -description 'Clean build output, generated packages and packages folder.' -depends Clean,PackageClean

###############################################################################
# Full build
#
Task FullBuild -description 'Do a full build starting from a clean solution.' -depends PackageRestore,Build

###############################################################################
# Create Nuget packages
#
Task Package -description 'Generate the packages from a clean build, and publish the packages.' -depends FullBuild {

    Chatter 'Packaging.' 1

    Push-Location
    try
    {
        Set-Location 'src'
        $solution = Get-Item '*.sln' | Select-Object -First 1

        # build
        $nuspecfiles = Get-ChildItem -Directory | Where-Object { $_.Name -ne 'packages'  } |  Get-ChildItem -Filter '*.nuspec' -recurse
        $nuspecfiles | ForEach-Object {
            Chatter "Packaging: $_.BaseName" 2
            Set-Location $_.DirectoryName
            Exec { NuGet.exe pack "$($_.BaseName).csproj" -Build -Properties "Configuration=$buildconfig" -Verbosity quiet }
            $nupkgfiles = Get-ChildItem -File -Filter '*.nupkg'
            Exec { NuGet.exe push "$($nupkgfiles[0].Name)" -source $publishrepo }
        }
    }
    finally
    {
        Pop-Location
    }
}

###############################################################################
# Run unit tests
#
Task Test -description 'Run the unit tests using NUnit console test runner.' -depends FullBuild {

    Chatter 'Testing.' 1

    Push-Location
    try
    {
        # find nunit-console runner
        Chatter 'Searching nunit-console runner.' 2
        $runner = Get-ChildItem -Path 'src\packages' -Filter 'nunit-console.exe' -Recurse | Select-Object -First 1
        
        # find unit test dlls
        Chatter 'Searching test projects.' 2
        $testprojects = Get-ChildItem -Directory -Path 'src' | Where-Object { $_.Name -ne 'packages' } | Get-ChildItem -Filter '*Test*.csproj' -Recurse
        $testdlls = $testprojects | ForEach-Object { Get-ChildItem -Path "scratch\bin\$($_.BaseName)-*" -Filter "$($_.BaseName).dll" -Recurse | Select-Object -First 1 }
        
        # execute runner, one run for each unit test
        foreach ($testdll in $testdlls) {
            # generate folder for test output
            $dummy = New-Item -ItemType Directory -Path (Join-Path 'scratch\nunit\' "$($testdll.BaseName)")
        
            # execute runner
            $testrunnerargs = @(
                "/work:scratch\nunit\$($testdll.BaseName)"
                '/framework:net-4.0'
                '/result:nunit-test-results.xml'
                '/out:nunit-stdout.txt'
                '/err:nunit-stderr.txt'
                "$($testdll.FullName)"
                )

            Chatter "Test runner exe: $($runner.FullName)" 3
            Chatter 'Test runner args:' 3
            foreach ($arg in $testrunnerargs) {
                Chatter "     $arg" 3
            }
            
            Exec { & "$($runner.FullName)" $testrunnerargs }
        }
    }
    finally
    {
        Pop-Location
    }
}

###############################################################################
# Generate documentation
#
Task Documentation -description 'Generate documentation using Sandcastle Help File Builder.' -depends FullBuild {

    Chatter 'Documenting.' 1

    Push-Location
    try
    {
        Set-Location 'src'
        $solution = Get-Item '*.sln' | Select-Object -First 1

        # build
        $shfbfiles = Get-ChildItem -Directory | Where-Object { $_.Name -ne 'packages'  } |  Get-ChildItem -Filter '*.shfbproj' -recurse
        $shfbfiles | ForEach-Object {
            Chatter "Documenting: $_.BaseName" 2
            Set-Location $_.DirectoryName
            Exec { msbuild "$($_.Name)" /t:Build /nr:false /p:Configuration=$buildconfig /v:quiet /nologo }
        }
    }
    finally
    {
        Pop-Location
    }
}

#endregion
