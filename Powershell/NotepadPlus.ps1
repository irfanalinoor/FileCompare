Function NodePadPlus
{
    Param
    (
        [Parameter(Mandatory=$True)]
        $action
    )

    if($action -eq 'install') 
    {
        $AppInstalled=Get-ItemProperty HKLM:\Software\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\* | Select-Object DisplayName, DisplayVersion, Publisher, InstallDate | where {$_.DisplayName -like "NotePad++*"}
 
        $appurl="https://notepad-plus-plus.org/download"
        $baseappurl="https://notepad-plus-plus.org"
        
        $Apponweb=(Invoke-WebRequest -Uri $appurl).Links| where {$_.href -match "Installer.x64.exe"}
        $onlineversion=[version]($apponweb.href.split("/")[3])
        
        $appinstalledversion= [version]$AppInstalled.DisplayVersion
        $folder=“$env:userprofile\Downloads“ + "\"
        $source = $baseappurl + $apponweb.href
        if($onlineversion -gt $appinstalledversion)
        {
            write-host "We have a newer version online. Installing Latest Version"
            Start-BitsTransfer -Source $source  -Destination $folder
            $targetfile=$folder + ($apponweb.href.split("/")[4])
            if(test-path $targetfile)
            {
                write-host "File available $targetfile. Installing..."
                Start-Process $targetfile
            }
            else
            {
                write-host "Download may have failed, cannot locate file in $targetfile"
        
            }
        
        }
        else
        {
            Write-Host "Onine Notepad++ version found '$onlineversion'"
            Write-Host "Current Version on this system '$appinstalledversion'"
            Write-Host "You already have a Higher version"
        }
    }

    if($action -eq 'uninstall') 
    {
        Write-Host "'$action'"
    }
}

NodePadPlus -action  install 
NodePadPlus -action  uninstall