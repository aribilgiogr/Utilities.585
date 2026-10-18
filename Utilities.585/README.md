# Utilities.585

Utilities.585 is a small helper library for .NET projects that provides common utilities used across sample and learning projects: basic repository/unit-of-work abstractions, extension methods, simple email sending helpers, and file import adapters.

Key points
- Target framework: .NET 10
- Purpose: reusable utilities for demonstrations and small apps

Projects and contents
- Data (IRepository, Repository, IUnitOfWork, UnitOfWork)
- Extensions (ServiceCollectionExtensions, StringExtensions)
- Helpers (EmailSender, FileImportAdapter)
- Models (Reply, SmtpOptions)

Getting started

1. Clone the repository:

   git clone https://github.com/aribilgiogr/Utilities.585.git

2. Open the solution in Visual Studio 2022+ or Visual Studio 2026:

   Utilities.585\Utilities.585.slnx

3. Build using Visual Studio or the dotnet CLI:

   dotnet build Utilities.585\Utilities.585.csproj

Contents and accurate API surface

The library uses the namespace prefix Utilities._585 and contains the following main areas:

- Data
  - IRepository<TEntity> — common CRUD/query signatures for entities
  - Repository<TEntity> — EF Core backed implementation
  - IUnitOfWork / UnitOfWork<TContext> — repository factory and CommitAsync()

- Extensions
  - ServiceCollectionExtensions
	- AddUnitOfWork<TContext>(IServiceCollection) — registers IUnitOfWork and IRepository<> (requires a DbContext type)
	- AddSmtpEmailSender(IConfiguration) — binds SmtpOptions from configuration and registers IEmailSender
  - StringExtensions
	- Base64UrlEncode(this string)
	- Base64UrlDecode(this string)

- Helpers
  - EmailSender (implements Microsoft.AspNetCore.Identity.UI.Services.IEmailSender)
	- Task SendEmailAsync(string email, string subject, string htmlMessage)
  - FileImportAdapter
	- FromJsonCollectionAsync<T>(Stream)
	- FromJsonAsync<T>(Stream)
	- FromCSVAsync<T>(Stream)
	- FromExcelAsync<T>(Stream)
	All return Reply<T> or Reply<IEnumerable<T>> wrappers defined in Models.

- Models
  - Reply and Reply<T> — lightweight operation/result wrapper (IsSuccess, Errors, Data)
  - SmtpOptions — configuration POCO (SectionName = "SmtpSetting")

Usage examples

Register services in Startup / Program:

```csharp
// register unit-of-work for your DbContext
services.AddUnitOfWork<MyAppDbContext>();

// register SMTP email sender configured from IConfiguration
services.AddSmtpEmailSender(Configuration);
```

Email sending (DI):

```csharp
public class MyService
{
	private readonly IEmailSender _emailSender;

	public MyService(IEmailSender emailSender) => _emailSender = emailSender;

	public Task SendAsync() => _emailSender.SendEmailAsync("to@example.com", "hi", "<p>hello</p>");
}
```

Reading files with FileImportAdapter:

```csharp
using var stream = File.OpenRead("data.csv");
var result = await FileImportAdapter.FromCSVAsync<MyDto>(stream);
if (result.IsSuccess)
{
	var items = result.Data; // IEnumerable<MyDto>
}
else
{
	// inspect result.Errors
}
```

Repository / UnitOfWork usage:

```csharp
var repo = unitOfWork.GetRepository<MyEntity>();
await repo.CreateAsync(entity);
var reply = await unitOfWork.CommitAsync();
if (!reply.IsSuccess) // handle errors
	;
```

Dependencies

The project uses the following notable packages (check the project file for exact versions):
- Microsoft.EntityFrameworkCore
- Microsoft.Extensions.Configuration
- Microsoft.AspNetCore.Identity.UI (for IEmailSender)
- CsvHelper
- MiniExcelLibs

Notes

- The library is intended as a lightweight collection of helpers for demos and small applications.
- SmtpOptions binds from configuration section named "SmtpSetting".

Contributing

Contributions, issues and feature requests are welcome. Fork the repository, make changes in a feature branch, and open a pull request against the origin repository.

License

No license file is included in the repository. Check the GitHub repo or contact the owner for licensing details.

