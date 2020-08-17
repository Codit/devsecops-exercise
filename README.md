# DevSecOps Exercise

Exercise to provide DevSecOps for a REST API.

![Codit logo](./media/logo.png)

## What is it?

A REST API to manage a warehouse which is using an in-memory data store.

## Challenge

Now it's up to you! Read more about our [challenge](Challenge.md) and get started!

## Getting Started

### Running it locally

You can easily run the sample locally:

1. Change `src/docker-compose.override.yml` with your own configuration
2. Navigate to `src/` folder
3. Start up with Docker Compose
```shell
$ docker-compose up
```
4. Browse to http://localhost:777/api/docs/index.html

### Running the tests

We provide two types of tests:

- `Integration` - Sends HTTP requests to a running API to verify all operations
- `Smoke` - Sends HTTP requests to a running API to verify it's up and running, without changing any data

Here is how you can run the tests locally:

```shell
$ dotnet test .\src\Codit.Exercises.DevOps.Tests\Codit.Exercises.DevOps.Tests.csproj --filter Category=Smoke
Test run for D:\Code\GitHub\devsecops-exercise\src\Codit.Exercises.DevOps.Tests\bin\Debug\netcoreapp3.1\Codit.Exercises.DevOps.Tests.dll(.NETCoreApp,Version=v3.1)
Microsoft (R) Test Execution Command Line Tool Version 16.5.0
Copyright (c) Microsoft Corporation.  All rights reserved.

Starting test execution, please wait...

A total of 1 test files matched the specified pattern.

Test Run Successful.
Total tests: 2
     Passed: 2
 Total time: 1,6618 Seconds
```