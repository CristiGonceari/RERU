# CVU.ERP.Module Layer

This library contains all the common logic of what makes a microservice to be a module, it includes 
- Dependency injection of the services
- Authentication/Authorization Logic with IdentityServer4

Adding Common controllers
```
            services.AddControllers ()
                .AddERPModuleControllers ();
```

Addinig common module services
```
            services.AddERPModuleServices ();
```

