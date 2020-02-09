# CodingCat.Extensions.Configuration

Provides extensions to configure dotnet configurations easier.

#### Provide the configurations folder path

The original dotnet `IConfigurationBuilder` provided a function to set the base path, however in some cases  we are too lazy and hoping just use the relative path.

With this library, it provides an extension function `SetRelativePath` which uses the `AppDomain.CurrentDomain.BaseDirectory` internally.

```csharp
var builder = new ConfigurationBuilder()
  .SetRelativePath("App_Data/Configurations");
```

#### Register your configurations

In a larger scale system, we would always love to group the same purpose of configurations by classes. This library provides some extension functions to do so in an easier way:

```csharp
var systemConfigurationSource = new ConfigurationSource<SystemConfiguration>(
  Environment.Default,
  FileType.Json,
  isOptional: false
);

var configurations = new ConfigurationBuilder()
  .Register(systemConfigurationSource)
  .Register(systemConfigurationSource
    .With(Environment.Develop)
    .With(isOptional: true)
  )
  .Build();
  
var systemConfiguration = configurations.Bind<SystemConfiguration>();
```

#### Auto registration

If the amount of configuration classes are too large to register the sources one by one, this library also provides the auto registration feature by setting the default environment file is required, but the others are optional:

```csharp
var configurations = new ConfigurationBuilder()
  .Auto<SystemConfiguration>(FileType.Json) // -- Auto register SystemConfiguration.json, SystemConfiguration.Develop.json, SystemConfiguration.Staging.json, SystemConfiguration.Production.json
  .Auto<CacheConfiguration>(FileType.Json)
  .Auto<PricingConfiguration>(FileType.Json)
  .Auto<CheckoutConfiguration>(FileType.Json)
  .Build();
```

#### What if using DI?

Dotnet nowadays handles the operations by DI heavily and this library would also love to save you from some typing:

```csharp
var services = new ServiceCollection()
  .AddOptions()
  .AutoConfigure<SystemConfiguration>(configurations)
  .AutoConfigure<CacheConfiguration>(configurations)
  .AutoConfigure<PricingConfiguration>(configurations)
  .AutoConfigure<CheckoutConfiguration>(configurations)
  .BuildServiceProvider();
var systemConfiguration = services.GetRequiredService<IOptionsSnapshot<SystemConfiguration>>();
```

#### Target Frameworks

- .Net 4.6.1
- .Net Standard 2.0