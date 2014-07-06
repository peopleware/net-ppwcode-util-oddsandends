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
    Set-ExecutionPolicy RemoteSigned
    $moduleItem = Get-Item  .\src\packages\psake.*\tools\psake.psm1 | 
                    Sort-Object -Property FullName -Descending | 
                    Select-Object -First 1
    Import-Module $moduleItem.FullName -Force
    if ($target -ne "")
    {
        Invoke-psake $target
    }
}
catch 
{
    Write-Error $_
    Write-Host "Error executing psake.ps1"
}
