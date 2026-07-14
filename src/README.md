## About this solution

This is the web solution for the [ABP CMS Kit Demo](https://github.com/abpframework/cms-kit-demo). It is a minimalist, non-layered ABP application that demonstrates CMS Kit features such as pages, blogs, comments, reactions, ratings, tags, and menus.

Related documentation:

* [CMS Kit module](https://abp.io/docs/latest/modules/cms-kit/index)
* [ABP Studio Solution Runner and tasks](https://abp.io/docs/latest/studio/running-applications)
* [Single-layer web application guide](https://abp.io/docs/latest/get-started/single-layer-web-application)

## Requirements

* .NET 10.0+
* ABP CLI 10.4.1+ (for `abp install-libs`)
* Node.js with Yarn v1 support for MVC UI package restore

## How to run

### Run with ABP Studio

The ABP Studio profile in `../etc/abp-studio/run-profiles/Default.abprun.json` defines an `Initialize Solution` task. Run this task from ABP Studio's Solution Runner **Tasks** tab after cloning or opening the solution for the first time.

The initial task runs `../etc/abp-studio/scripts/initialize-solution.ps1`, which installs MVC UI libraries and applies/seeds the SQLite database.

After the task succeeds, start the `CmsKitDemo` application from ABP Studio's Solution Runner.

### Run manually

From the `CmsKitDemo` directory, install MVC UI libraries:

```bash
abp install-libs
```

Then apply migrations and seed the SQLite database:

```bash
dotnet run --migrate-database
```

Then run the application:

```bash
dotnet run
```

The application uses SQLite, so no external database server is required. The default credentials are `admin` / `1q2w3E*`.

Happy coding..!



