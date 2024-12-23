# Library Management System

This is a Full Stack Web API built with ASP.NET Core to help manage library operations, including books, members, and lending records.

## Project Members
- Kashyap Bathani

## Project Requirements
- Application must build and run.
- Unit Testing (20% code coverage with at least 5 test cases for Services and Controllers).
- Utilize a SQL Server database for persistence.
- Backend must communicate via RESTful API (GET, POST, DELETE).

## Tech Stack

- C# (Back End Programming Language)
- SQL Server (Database)
- EF Core (ORM Technology)
- ASP.NET Core (Web API Framework)
- Swagger/OpenAPI (API Documentation)
- xUnit, Moq (Unit Testing)

## User Stories
- Admin should be able to add, update, and delete books in the library.
- Admin should be able to register new members.
- Admin should be able to track lending records (book issued, return status, due dates, etc.).
- Users should be able to view a list of available books.
- Users should be able to check the status of borrowed books.

## Tables
![alt text](<Database ER diagram (crow's foot).png>)

## MVP Goals
- Admin(s) can:
  - Add, view, and delete books.
  - Add, view, and delete members.
  - Add and view lending records.
- Users can:
  - View all available books.
  - Check the status of their borrowed books.
- Ensure data persistence with SQL Server.
- RESTful API must have functional GET, POST, and DELETE methods.

## Stretch Goals
- Track overdue books and send reminders.
- Generate reports for borrowed books, member activities, and overdue penalties.
- Frontend integration with a web application or C# console app.
