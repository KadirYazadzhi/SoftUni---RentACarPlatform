# 🚗 RentACar Platform - Code Overview and Functionalities 

Welcome to the **RentACar Platform** repository! 🚗💻 This system simulates a car rental service, showcasing key programming principles through a simple and user-friendly interface. 

The **RentACar Platform** is a console-based project designed to illustrate key programming concepts such as **Object-Oriented Programming** (OOP) principles, including **encapsulation**, **inheritance**, and **polymorphism**. 🛠️📚 The system leverages classes to manage users and vehicles, implements data validation mechanisms, and utilizes collections for efficient data handling. It serves as a practical example for learning how to structure code, enforce data integrity, and create dynamic user interactions in a clean and modular way. ✨


## 🛠️ **Core Components**

### 📁 Namespaces Used
- `using System;`
- `using System.Collections.Generic;`
- `using System.Linq;`

These namespaces provide essential functionalities like console input/output, collections, and LINQ operations.


## 🚀 **Main Functionality**

### `Main()` Method
- **Purpose**: Entry point of the application.
- **Actions**:
  1. Calls `InsertCars()` to populate the car list.
  2. Prints a welcome message via `StartMessagePrint()`.
  3. Directs the user to select their role and functionalities with `ChooseFunction()`.


## 🏎️ **Car Initialization**

### `InsertCars()` Method
- **Purpose**: Creates a list of 10 randomly generated cars with properties:
  - Brand, Model, Color, Year, Price, Availability.
- **Details**:
  - Uses `Random` to select random values from predefined arrays.
  - Each car is added to the `cars` list.


## 👥 **User Management**

### `ChooseUserType()`
- **Purpose**: Differentiates between Admin and Regular User roles.
- **Options**:
  1. Register as a **User**.
  2. Register as an **Admin** (`isAdmin = true`).

### `RegisterUser()`
- **Purpose**: Collects user details and initializes a new user profile.
- **Details**:
  - Prompts for **name**, **password**, and **money**.
  - Stores user information in the `currentUser` variable.


## 📜 **Menu and Functionalities**

### `DisplayMenu()` and `PrintFunctionsMenu()`
- **Purpose**: Displays menu options for users or admins.
- **Options**:
  - **Admin**:
    1. Add/Delete cars.
    2. View or modify car details.
    3. Exit.
  - **User**:
    1. Rent/Return cars.
    2. View available cars.
    3. Calculate rent prices.


## 🔑 **Admin Functionalities**

### Admin Actions
- **Add a Car**: Adds a new car with user-provided details.
- **Delete a Car**: Removes a car by its brand and model.
- **View All Cars**: Lists all cars in the system.
- **Modify Car Details**: Changes attributes like brand, model, price, etc.


## 🤝 **User Functionalities**

### User Actions
- **Rent a Car**: Deducts the rental cost from user balance and marks the car as unavailable.
- **Return a Car**: Returns a car and marks it as available.
- **Calculate Rent Price**: Displays the cost for renting a car for a specified duration.
- **View Available Cars**: Lists all cars currently available for rent.


## 📦 **Core Classes**

### `BaseUser`
- **Purpose**: Common functionality for `User` and `Admin`.
- **Key Methods**:
  - `ViewAvailableCars()`: Displays all available cars.
  - `CheckIndexIsValid()`: Ensures valid indices for cars.
  - `IsCarAlreadyInTheSystem()`: Prevents duplicate car entries.
  - `FindCarIndex()`: Finds a car by brand and model.

### `User` (Inherits `BaseUser`)
- **Attributes**:
  - Name, Password, Money.
- **Key Methods**:
  - `RentACar()`: Handles car rental logic.
  - `ReturnACar()`: Manages car return functionality.
  - `CalculatePrice()`: Estimates the total rent cost.

### `Admin` (Inherits `BaseUser`)
- **Key Methods**:
  - `AddCar()`: Adds a car to the system.
  - `DeleteCar()`: Removes a specific car.
  - `ChangeCarDetails()`: Updates car attributes.

### `Car`
- **Attributes**:
  - Brand, Model, Color, Year, Price, Availability.
- **Purpose**: Represents a car entity in the system.


## 🏁 **Exit Handling**

### `Environment.Exit(0)`
- Graceful exit from the application when the user chooses option `99`.


## 📌 **Highlights**
- **Role-Based Access**: Separate menus and functionalities for admins and users.
- **Robust Input Handling**: Ensures valid user input with retries.
- **Data Management**: Maintains a dynamic list of cars with search and validation features.


## ✨ **Future Improvements**
- 🔒 Enhance security with encrypted passwords.
- 📈 Add a reporting system for rented cars.
- 🌐 Implement a GUI for a better user experience.


## 🛠 **Technologies Used**

- **C# .NET**: For backend logic and OOP implementation 🖥️
- **Visual Studio**: IDE used for development 👨‍💻
- **Lists**: For storing user accounts, transactions, and loans 📋

## 🚀 **How to Use**

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/KadirYazadzhi/SoftUni---RentACarPlatform.git
    ```
2. Open the Project:
   - Open the ```.sln``` file in Visual Studio.

3. Run the Application:
   - The program will prompt you to create a new account or log in.
   - Choose an action such as depositing money, viewing balance, or applying for a loan.

4. Exit the Program:
   - Type "exit" to close the application after you're done.
