# Recipes Web app

Recipes Web app is basic MVC app for storing recipes

## Description

The Recipes Web app is an MVC project that stores recipes and information about them. The recipes are stored in a database.
The application has Users, Admins and Contributors. The User can only view recipes, if he wants to add one, he needs to become a Contributor first. 
Admins are able to edit each recipe, while Contributors can edit only the ones they have added. Search and filter functionallities are also availible. 

## Getting Started

### Approaches

The database was created using the Code First approach provided by the framework. The models were created and then used to generate the database. The services store all the bussines logic.
Web is separated from the data by separating data from view models. 

### Dependencies

- .NET 5.0
- ASP.NET Core
- Entity Framework Core
- Identity.UI
- SQL Server
- Bootstrap

## Authors

Bilyana Karcheva
