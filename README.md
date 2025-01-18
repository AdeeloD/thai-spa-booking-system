# Momo & Meme - Thai Massage Booking System

## Project Overview

Momo & Meme is a web application that provides a simple, intuitive platform for booking Thai massage services. The system allows users to explore different massage types, choose from available masseurs, and book their preferred time slots. The application integrates with Google for external authentication and uses PostgreSQL for data storage.

---

## Features

- **Google Authentication:** Users can sign in using their Google account.
- **Booking System:** Customers can choose their preferred massage type, masseur, and set the desired time for their appointment.
- **Admin Panel:** Admins can manage massage types, view user data, and handle appointments.
- **Responsive Design:** The website is fully responsive and optimized for all screen sizes.

---

## Technologies Used

- **ASP.NET Core:** Backend framework for building the application.
- **PostgreSQL:** Database system for storing user and booking data.
- **Identity:** ASP.NET Core Identity for user authentication and management.
- **Google Authentication:** External authentication via Google.
- **Bootstrap 5:** Frontend framework for a responsive, mobile-friendly design.
- **Entity Framework Core:** ORM for interacting with the PostgreSQL database.

---

## Getting Started

### Prerequisites

To run this project locally, you will need the following:

- [.NET 6.0 or later](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/download/)
- [Visual Studio or Visual Studio Code](https://code.visualstudio.com/)

### Installation

1. Clone this repository:

   ```bash
   git clone https://github.com/yourusername/MomoAndMeme.git
   cd MomoAndMeme
   ```

2. Create a new PostgreSQL database and update the connection string in `appsettings.json`:

   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Port=5432;Username=yourusername;Password=yourpassword;Database=yourdatabase;"
   }
   ```

3. Install the required NuGet packages:

   ```bash
   dotnet add package Microsoft.EntityFrameworkCore.Design
   dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
   dotnet add package Microsoft.AspNetCore.Authentication.Google
   ```

   - **`Microsoft.EntityFrameworkCore.Design`:** Enables migrations and design-time features for Entity Framework Core.
   - **`Npgsql.EntityFrameworkCore.PostgreSQL`:** Adds PostgreSQL support for Entity Framework Core.
   - **`Microsoft.AspNetCore.Authentication.Google`:** Enables Google authentication for ASP.NET Core.

4. Run the database migrations:

   ```bash
   dotnet ef database update
   ```

5. Build and run the application:

   ```bash
   dotnet run
   ```

6. Open your browser and navigate to `https://localhost:7234` to access the application.

---

## Usage

- **User Login:** You can log in using your Google account via the "Login with Google" button on the homepage.
- **Booking an Appointment:** Navigate to the "Bookings" section, choose a massage type, select a masseur, and pick a time for your appointment.
- **Admin Access:** Admin users can manage massage types and bookings.

---

## Contributing

1. Fork this repository.
2. Create a new branch:

   ```bash
   git checkout -b feature/your-feature-name
   ```

3. Commit your changes:

   ```bash
   git commit -am 'Add new feature'
   ```

4. Push to the branch:

   ```bash
   git push origin feature/your-feature-name
   ```

5. Open a Pull Request.

---

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

