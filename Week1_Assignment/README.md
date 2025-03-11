# Device Web API Project - Week 1 Assignment 1

## Description
This .NET Web API provides CRUD operations for managing working place devices while adhering to RESTful standards. It ensures structured error handling, proper HTTP status codes, and supports model validation.

## Table of Contents
- [Features](#features)
- [Video of the Running Program](#video-of-the-running-program)
- [Prerequisites](#prerequisites)
- [API Endpoints](#api-endpoints)
- [Technologies Used](#technologies-used)
- [Contact](#contact)

## Features
✅ RESTful API Standards – Follows best practices for resource management  
✅ HTTP Methods – Implements GET, POST, PUT, DELETE, and PATCH  
✅ Proper HTTP Status Codes – Uses 200, 201, 400, 404, 500 with a standardized error response format  
✅ Model Validation – Ensures required fields are validated before processing  
✅ Routing – Well-defined endpoints with meaningful URLs  
✅ Model Binding – Supports data binding from both body and query parameters  
✅ Advanced Listing & Sorting – Includes a /api/devices/list?name=abc endpoint for filtering  

## Video of the Running Program
<div class="display: flex"><b>Click The Image for Video</b></div>

[![Pa&Pa Bootcamp Week1](https://img.youtube.com/vi/KdmQh9Zo-jg/0.jpg)](https://youtu.be/KdmQh9Zo-jg)

### Prerequisites
- [.NET SDK](https://dotnet.microsoft.com/download)  
- Optional: Postman for testing API endpoints

## API Endpoints

| HTTP Method | Endpoint               | Description                                              |
|------------|-----------------------|----------------------------------------------------------|
| **GET**    | `/api/device`         | Get all devices                                          |
| **GET**    | `/api/device/{id}`    | Get a specific device by ID                              |
| **GET**    | `/api/device/list-by-location?location=xyz` | Get devices filtered by location (query parameter)  |
| **POST**   | `/api/device`         | Create a new device (ID is auto-generated)              |
| **PUT**    | `/api/device/{id}`    | Update a device completely by ID                        |
| **PATCH**  | `/api/device/{id}`    | Partially update a device by ID                         |
| **DELETE** | `/api/device/{id}`    | Delete a device by ID                                   |

## Technologies Used

- .NET 8
- ASP.NET Core Web API
- Swagger

## Contact

* Aysel Bilmez
* Email: aybilmez@gmail.com
* GitHub: ayselbilmez
