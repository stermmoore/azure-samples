param name string = 'sql-cosmos-00001'
param location string = 'ukwest'
param locationName string = 'UK West'
param defaultExperience string = 'Core (SQL)'
param isZoneRedundant string = 'false'

resource name_resource 'Microsoft.DocumentDb/databaseAccounts@2022-05-15-preview' = {
  kind: 'GlobalDocumentDB'
  name: name
  location: location
  properties: {
    databaseAccountOfferType: 'Standard'
    locations: [
      {
        id: '${name}-${location}'
        failoverPriority: 0
        locationName: locationName
      }
    ]
    backupPolicy: {
      type: 'Periodic'
      periodicModeProperties: {
        backupIntervalInMinutes: 240
        backupRetentionIntervalInHours: 8
        backupStorageRedundancy: 'Local'
      }
    }
    isVirtualNetworkFilterEnabled: false
    virtualNetworkRules: []
    ipRules: []
    dependsOn: []
    enableMultipleWriteLocations: false
    capabilities: []
    enableFreeTier: true
    capacity: {
      totalThroughputLimit: 1000
    }
  }
  tags: {
    defaultExperience: defaultExperience
    'hidden-cosmos-mmspecial': ''
  }
}