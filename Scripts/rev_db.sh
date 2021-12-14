#!/bin/bash

dotnet ef dbcontext scaffold "Server=127.0.0.1;Database=db_api_test;User Id=SA;Password=p@55word;" Microsoft.EntityFrameworkCore.SqlServer
