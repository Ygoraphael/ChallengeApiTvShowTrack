docker-compose up -d 2>$null
$scriptpath = $MyInvocation.MyCommand.Path
$dir = Split-Path $scriptpath
cd "$dir\TvShowTracker"
dotnet tool install --global dotnet-ef 2>$null
dotnet ef database update