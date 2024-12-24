# E-Commerce Backend API
This is a RESTful API built in ASP.NET Core to manage an e-commerce platform. It includes features for user management, product catalog management, shopping cart functionality, and order processing.

## Project Members
- Kashyap Bathani

## Project Requirements
- Application must build and run.
- RESTful API endpoints for user, product, cart, and order functionalities.
- Data persistence using a SQL Server database.
- Unit Testing (70% code coverage for Services and Models layer).
- Entity Framework Core for ORM and database interactions.
- Backend hosted on Azure (optional).

## Tech Stack
- ASP.NET Core (Web API Framework)
- C# (Programming Language)
- SQL Server (Database)
- EF Core (ORM Technology)
- Postman/Swagger (API Testing and Documentation)
- XUnit (Unit Testing Framework)

## User Stories
- User should be able to register, login, and access protected resources.
- User should be able to browse the product catalog, filter by category, and view product details.
- User should be able to add, remove, or update items in their shopping cart.
- User should be able to place an order from the shopping cart and track order status.
- Admin users should be able to manage products and categories.

## Tables
![alt text](<Screenshot 2024-12-23 184641.png>)

## MVP Goals
- CRUD operations for products and categories.
- Cart functionality: add, update, and remove items with price calculations.
- Place orders and maintain order history and status.

## Stretch Goals
- Basic recommendation system based on user purchase history.
- Frontend integration with Swagger or a basic UI.