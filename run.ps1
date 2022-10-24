$scriptpath = $MyInvocation.MyCommand.Path
$dir = Split-Path $scriptpath
cd "$dir\TvShowTracker"

docker build -t tvshowapp .

cd ..

docker-compose up --build