$curDir = Get-Location
$servicesDir = Join-Path -Path $curDir -ChildPath backend/services
$modulesDir = Join-Path -Path $curDir -ChildPath backend/modules
$authServicePubDir = Join-Path -Path $servicesDir -ChildPath Auth.Service/published
$authEventProcessorPubDir = Join-Path -Path $modulesDir -ChildPath auth/Auth.EventProcessor/published
$cryptServicePubDir = Join-Path -Path $servicesDir -ChildPath Crypt.Service/published
$cryptEventProcessorPubDir = Join-Path -Path $modulesDir -ChildPath crypt/Crypt.EventProcessor/published

if (Test-Path $authServicePubDir){
	Remove-Item $authServicePubDir -Recurse -Force
	Write-Host "auth servis published klsörü silindi"
}

if (Test-Path $authEventProcessorPubDir){
	Remove-Item $authEventProcessorPubDir -Recurse -Force
	Write-Host "auth event processor published klsörü silindi"
}

if (Test-Path $cryptServicePubDir){
	Remove-Item $cryptServicePubDir -Recurse -Force
	Write-Host "crypt servis published klsörü silindi"
}

if (Test-Path $cryptEventProcessorPubDir){
	Remove-Item $cryptEventProcessorPubDir -Recurse -Force
	Write-Host "crypt event processor published klsörü silindi"
}

#dotnet publish ./backend/services/Auth.Service/Auth.Service.csproj -c Release -o backend/services/Auth.Service/published
#dotnet publish ./backend/modules/auth/Auth.EventProcessor/Auth.EventProcessor.csproj -c Release -o backend/modules/auth/Auth.EventProcessor/published
#dotnet publish ./backend/services/Crypt.Service/Crypt.Service.csproj -c Release -o backend/services/Crypt.Service/published
#dotnet publish ./backend/modules/crypt/Crypt.EventProcessor/Crypt.EventProcessor.csproj -c Release -o backend/modules/crypt/Crypt.EventProcessor/published
#docker-compose -f docker-compose.yml -f docker-compose.override.yml up --build --force-recreate -d
#
#if (Test-Path $authServicePubDir){
#	Remove-Item $authServicePubDir -Recurse -Force
#	Write-Host "auth servis published klsörü silindi"
#}
#
#if (Test-Path $authEventProcessorPubDir){
#	Remove-Item $authEventProcessorPubDir -Recurse -Force
#	Write-Host "auth event processor published klsörü silindi"
#}
#
#if (Test-Path $cryptServicePubDir){
#	Remove-Item $cryptServicePubDir -Recurse -Force
#	Write-Host "crypt servis published klsörü silindi"
#}
#
#if (Test-Path $cryptEventProcessorPubDir){
#	Remove-Item $cryptEventProcessorPubDir -Recurse -Force
#	Write-Host "crypt event processor published klsörü silindi"
#}
