$build_outputs = "Build Outputs"
$deploy_dir = "Deployment"
mkdir $deploy_dir

### Build Environment ###

$build_env_dir = "$deploy_dir\Build Environment"
mkdir "$build_env_dir"

$build_server_dir = "$build_env_dir\Build Server"
mkdir "$build_server_dir"
copy -path "$build_outputs\Deploy\Deploy.ps1" -dest "$build_server_dir\Deploy.ps1"
copy -path "$build_outputs\Deploy\Deploy.cmd" -dest "$build_server_dir\Deploy.cmd"

mkdir "$build_server_dir\PokerLeagueManager.Commands.WCF"
copy -path "$build_outputs\_PublishedWebsites\PokerLeagueManager.Commands.WCF_Package\*" -dest "$build_server_dir\PokerLeagueManager.Commands.WCF\"

mkdir "$build_server_dir\PokerLeagueManager.Queries.WCF"
copy -path "$build_outputs\_PublishedWebsites\PokerLeagueManager.Queries.WCF_Package\*" -dest "$build_server_dir\PokerLeagueManager.Queries.WCF\"

mkdir "$build_server_dir\PokerLeagueManager.DB.EventStore"
copy -path "$build_outputs\PokerLeagueManager.DB.EventStore.dacpac" -dest "$build_server_dir\PokerLeagueManager.DB.EventStore\"

mkdir "$build_server_dir\PokerLeagueManager.DB.QueryStore"
copy -path "$build_outputs\PokerLeagueManager.DB.QueryStore.dacpac" -dest "$build_server_dir\PokerLeagueManager.DB.QueryStore\"