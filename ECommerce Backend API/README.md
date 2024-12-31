# E-Commerce Backend API
A C#/.NET-based E-Commerce backend API that allows users to browse products, manage a shopping cart, and place orders. This application is built using ASP.NET Core and Entity Framework Core for data persistence with SQL Server.

# Project Members
- Kashyap Bathani

# Project Requirements
- README that describes the application and its functionalities

- ERD of your DB

- The application should be ASP.NET Core application

- The application should build and run

- The application should have unit tests and at least 20% coverage (at least 5 unit tests that tests 5 different methods/functionality of your code)

- The application should communicate via HTTP(s) (Must have POST, GET, DELETE)

- The application should be RESTful API

- The application should persist data to a SQL Server DB

- The application should communicate to DB via EF Core (Entity Framework Core)


# Tech Stack
- C# with ASP.NET Core for the backend

- Entity Framework Core as the ORM

- SQL Server for data storage

- xUnit or MSTest/NUnit for unit testing

- Swagger (optional) for API documentation


# Project Overview
- User management: create, update, delete users.

- Product management: create, update, delete products (admin-only).

- Shopping cart: add/remove products to a user’s cart.

- Order processing: convert cart items into an order and persist order history.

# Key Endpoints
- Users: Create, read, update, delete user accounts.

- Products: List, retrieve, create, update, delete (create/update/delete restricted to admins).

- Carts: Add products, remove products, view cart.

- Orders: Checkout cart → order, retrieve order details.


# ERD
![alt text](mermaid-diagram-2024-12-31-002112.png)





# Features & User Stories
### User Management

- As a user, I can register a new account.

- As a user, I can update my profile details.

- As a user, I can delete my account.

### Product Management

- As an admin, I can add new products.

- As an admin, I can modify existing product details.

- As an admin, I can delete products.

- As any user, I can view or search products.

### Cart & Checkout

- As a user, I can add or remove products from my cart.

- As a user, I can checkout and create an order from my cart.
