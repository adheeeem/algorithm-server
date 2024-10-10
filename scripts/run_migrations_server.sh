dbHost=$DB_HOST
dbPassword=$DB_PASSWORD

export GOOSE_DRIVER=postgres
export GOOSE_DBSTRING="host=${dbHost} port=5432 user=virtualmenu password=${dbPassword} dbname=virtualmenu"

cd migrations
goose up
