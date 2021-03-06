# Example ConfigServer

## Overview
This set of code and documentation is used for demonstration purposes only. Its a simple customer configuration api and supporting tools. Each customer can have a whole tree of configuration information stored against their account. The api allows a consumer to manage and query the config.

## Code Structure
The source code is made up of a number of projects. 
- ConfigService.Api, this is a .net core API with swagger documentation
- ConfigService.Database, the sql database definition
- ConfigService.Model, the poco that form the OO model
- ConfigService.Repository.Sql, the sql repository
- ConfigService.Repository.InMemory, an in memory testing repository and used by the unit tests
- ConfigService.Api.Tests, the Api unit tests
- ConfigService.WinFormsApp, a windows forms application for managing the SettingTypes

## Libs used
- Swashbuckle.AspNetCore
- Moq
- xunit
- Newtonsoft.Json
- Microsoft.EntityFrameworkCore.SqlServer

## How and Why
This example demonstrates the used of the .NET Core and the MVC Web API features.

- **Focus** -- The primary focus of this repo is to have code for discussion with colleagues.
- **Platforms** -- This code is designed to run .NET Core so hopefully run anyware.
- **Open Source** -- The MIT license is applied to the code.

## Build and Release

Behind the scenes each time a change is checked into the master branch a build is started on a Visual Studio Team Services.

