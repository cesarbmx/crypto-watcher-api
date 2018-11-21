1) Install AutoRest
https://github.com/Azure/autorest

2) Run this command
AutoRest -CodeGenerator CSharp -Modeler Swagger -Input http://localhost:21637/swagger/v1/swagger.json -Namespace CryptoWatcher.ApiClient -OutputDirectory C:\cesarc\git-hub\CryptoWatcher\CryptoWatcher.ApiClient -AddCrendentials true
