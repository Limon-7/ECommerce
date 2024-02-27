### Service Structure:
- Core: Domian Section(Independent)
- Infrastructure: Reference ==>Core, Application 
- Application: Reference ==> Core
- API: Reference ==> Application, Infrastructure

#### CMD:
- dotnet add reference ../Catalog.Core