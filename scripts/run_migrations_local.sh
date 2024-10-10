go install github.com/pressly/goose/v3/cmd/goose@latest

export GOOSE_DRIVER=postgres
export GOOSE_DBSTRING="host=localhost port=6432 user=algorithm password=academyofalgorithm2021 dbname=algorithm_db sslmode=disable"

cd ../migrations
goose up
