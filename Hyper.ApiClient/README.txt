1) Install AutoRest
https://github.com/Azure/autorest

2) Run this command
AutoRest -CodeGenerator CSharp -Modeler Swagger -Input http://localhost:21637/swagger/v1/swagger.json -Namespace Hyper.ApiClient -OutputDirectory C:\cesarc\git-hub\Hyper\Hyper.ApiClient -AddCrendentials true