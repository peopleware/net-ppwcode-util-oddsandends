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

param([string]$target = "")

try 
{
    # execution policy for scripts
    Set-ExecutionPolicy RemoteSigned

    # find module
    $modules = Get-Item  .\src\packages\psake.*\tools\psake.psm1
    if ($modules -eq $null)
    {
        Push-Location
        Set-Location 'src'
        & nuget restore
        Pop-Location

        $modules = Get-Item  .\src\packages\psake.*\tools\psake.psm1
        if ($modules -eq $null)
        {
            throw "Cannot find psake module."
        }
    }

    # take most recent module, if multiple found
    $module = $modules | Sort-Object -Property FullName -Descending | Select-Object -First 1

    # import module, force a reload if already loaded
    Import-Module $module.FullName -Force
    if ($target -ne "")
    {
        Invoke-psake $target
    }
}
catch 
{
    Write-Host "Error executing psake.ps1"
    Write-Host "Error:"
    Write-Host $_
    Write-Host
}
