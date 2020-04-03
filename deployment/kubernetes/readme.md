1. Copy `Service-Accounts` and `Service-Accounts-Worker` to the https://github.com/swisschain/kubernetes-swisschain/tree/master/Kubernetes/03.Pods/Exchange. 
2. Add services namespace if necessary.
3. Copy `appsettings.json` to https://github.com/swisschain/kubernetes-swisschain/tree/master/Settings/exchange/accounts.json
4. Replace all the secrets in the `accounts.json` with placeholders like `${PlaceHolderName}`. 
Use global-scoped, product-scoped, and service-scoped placeholders. If you not sure which scope particular placeholder has, ask the team.
5. Put placeholders with values to the settings blob in Azure Storage (TODO: specify the blob here)