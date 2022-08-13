# CosmosDB

## Deployment

azure cli / ARM template

`az deployment group create --resource-group rg-azure-samples --template-file deployment.json`

check deployment 

`az cosmosdb show -g rg-azure-samples -n sql-cosmos-00001`


## Resources

https://docs.microsoft.com/en-gb/azure/cosmos-db/throughput-serverless