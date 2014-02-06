$build_outputs = "BuildOutputs"
$deploy_dir = "Deployment"
mkdir $deploy_dir

### Local Environment ###

$local_env_dir = "$deploy_dir\Local Environment"
mkdir "$local_env_dir"

$local_server_dir = "$local_env_dir\localhost"
mkdir "$local_server_dir"
copy -path "$build_outputs\Deploy\Deploy.ps1" -dest "$local_server_dir\Deploy.ps1"
copy -path "$build_outputs\Deploy\Deploy.cmd" -dest "$local_server_dir\Deploy.cmd"

mkdir "$local_server_dir\PokerLeagueManager.Commands.WCF"
copy -path "$build_outputs\_PublishedWebsites\PokerLeagueManager.Commands.WCF_Package\*" -dest "$local_server_dir\PokerLeagueManager.Commands.WCF\"

mkdir "$local_server_dir\PokerLeagueManager.Queries.WCF"
copy -path "$build_outputs\_PublishedWebsites\PokerLeagueManager.Queries.WCF_Package\*" -dest "$local_server_dir\PokerLeagueManager.Queries.WCF\"

mkdir "$local_server_dir\PokerLeagueManager.DB.EventStore"
copy -path "$build_outputs\PokerLeagueManager.DB.EventStore.dacpac" -dest "$local_server_dir\PokerLeagueManager.DB.EventStore\"
copy -path "$build_outputs\PokerLeagueManager.DB.EventStore.localhost.publish.xml" -dest "$local_server_dir\PokerLeagueManager.DB.EventStore\PokerLeagueManager.DB.EventStore.publish.xml"

mkdir "$local_server_dir\PokerLeagueManager.DB.QueryStore"
copy -path "$build_outputs\PokerLeagueManager.DB.QueryStore.dacpac" -dest "$local_server_dir\PokerLeagueManager.DB.QueryStore\"
copy -path "$build_outputs\PokerLeagueManager.DB.QueryStore.localhost.publish.xml" -dest "$local_server_dir\PokerLeagueManager.DB.QueryStore\PokerLeagueManager.DB.QueryStore.publish.xml"

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
copy -path "$build_outputs\PokerLeagueManager.DB.EventStore.BUILD.publish.xml" -dest "$build_server_dir\PokerLeagueManager.DB.EventStore\PokerLeagueManager.DB.EventStore.publish.xml"

mkdir "$build_server_dir\PokerLeagueManager.DB.QueryStore"
copy -path "$build_outputs\PokerLeagueManager.DB.QueryStore.dacpac" -dest "$build_server_dir\PokerLeagueManager.DB.QueryStore\"
copy -path "$build_outputs\PokerLeagueManager.DB.QueryStore.BUILD.publish.xml" -dest "$build_server_dir\PokerLeagueManager.DB.QueryStore\PokerLeagueManager.DB.QueryStore.publish.xml"

############################

copy -path "$build_outputs\deploy\config\*" -dest "$deploy_dir\" -Force -Recurse