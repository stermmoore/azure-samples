# CosmosDB

## Deployment

azure cli / ARM template

`az deployment group create --resource-group rg-azure-samples --template-file deployment.json`

check deployment 

`az cosmosdb show -g rg-azure-samples -n sql-cosmos-00001`

## Consistency Levels

https://docs.microsoft.com/en-us/azure/cosmos-db/consistency-levels

- Strong - Reader is guaranteed to read the most recent committed version
- Bounded Staleness - Guarantees consistency outside of a "staleness window", 
configured as either a number of write operations or a number of seconds. Consistency 
within the same region will be strong
- Session (default) - Strong consistency within a session
- Consistent Prefix - Guarantees that writes are never read out of order
- Eventual - All writes will eventually be available but order is not guaranteed

## Resources

https://docs.microsoft.com/en-gb/azure/cosmos-db/throughput-serverless



