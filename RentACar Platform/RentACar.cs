using System;
using System.Collections.Generic;
using System.Linq;

public class Program {
    private static bool isAdmin = false;
    private static List<Car> cars = new List<Car>();
    private static User currentUser = null;

    public static void Main() {
        InsertCars();
        StartMessagePrint();
        ChooseFunction();
    }

    private static void InsertCars() {
        var random = new Random();
        string[] models = { "Sedan", "SUV", "Hatchback", "Coupe", "Convertible" };
        string[] brands = { "Toyota", "Ford", "BMW", "Mercedes", "Audi" };
        string[] colors = { "Red", "Blue", "Black", "White", "Gray" };

        for (int i = 0; i < 10; i++) {
            cars.Add(new Car(
                brands[random.Next(brands.Length)],
                models[random.Next(models.Length)],
                colors[random.Next(colors.Length)],
                random.Next(2000, 2023),
                Math.Round(random.NextDouble() * 100 + 500, 2),
                true)
            );
        }
    }

    private static void StartMessagePrint() {
        Console.WriteLine("\n--- RentACar Platform ---");
        ChooseUserType();
    }

    private static void ChooseUserType() {
        PrintUserTypeMessage();

        int typeUserChoice;
        while ((typeUserChoice = int.Parse(Console.ReadLine())) < 0 || typeUserChoice > 2) {
            Console.WriteLine("Please enter a valid choice.");
            PrintUserTypeMessage();
        }

        if (typeUserChoice == 2) {
            isAdmin = true;
        } 
        else {
            RegisterUser();
        }
    }

    private static void RegisterUser() {
        Console.WriteLine("Please enter your name:");
        string name = Console.ReadLine();

        Console.WriteLine("Please enter your password:");
        string password = Console.ReadLine();

        Console.WriteLine("Enter starting amount of money (e.g., 1000):");
        double money = double.Parse(Console.ReadLine());
        
        currentUser = new User(cars, name, password, money);

        Console.WriteLine($"User {name} registered successfully with {money} money.");
    }

    private static void PrintUserTypeMessage() {
        Console.WriteLine("Choose the type of user you want to register as.");
        Console.WriteLine("   1. User");
        Console.WriteLine("   2. Admin");
        Console.WriteLine("Enter your choice: ");
    }

    private static void ChooseFunction() {
        while (true) {
            int choice = DisplayMenu();
            if (isAdmin) {
                AdminActions(choice);
            } 
            else {
                UserActions(choice);
            }
        }
    }

    private static void AdminActions(int choice) {
        Admin admin = new Admin(cars);
        switch (choice) {
            case 1: admin.AddCar(); break;
            case 2: admin.DeleteCar(); break;
            case 3: admin.DeleteAllCars(); break;
            case 4: admin.ViewAllCars(); break;
            case 5: admin.ViewAvailableCars(); break;
            case 6: admin.ChangeCarDetails(); break;
            case 99: 
                Console.WriteLine("Goodbye!");
                Environment.Exit(0);
                break;
        }
    }

    private static void UserActions(int choice) {
        if (currentUser == null) return; 
        switch (choice) {
            case 1: currentUser.RentACar(); break;
            case 2: currentUser.ReturnACar(); break;
            case 3: currentUser.ViewAvailableCars(); break;
            case 4: currentUser.CalculatePrice(); break;
            case 99:
                Console.WriteLine("Goodbye!");
                Environment.Exit(0);
                break; 
        }
    }

    private static int DisplayMenu() {
        PrintFunctionsMenu();

        int choice;
        while ((choice = int.Parse(Console.ReadLine())) != 99) {
            if (!((!isAdmin && (choice > 4 || choice < 1)) || (isAdmin && (choice > 6 || choice < 1)))) {
                break;
            }

            Console.WriteLine("Please enter a valid choice.");
        }

        return choice;
    }

    private static void PrintFunctionsMenu() {
        Console.WriteLine("Choose one of the following functions: ");
        if (isAdmin) {
            Console.WriteLine("   1. Add a new car.");
            Console.WriteLine("   2. Delete a car.");
            Console.WriteLine("   3. Delete all cars.");
            Console.WriteLine("   4. View all cars.");
            Console.WriteLine("   5. View the available cars.");
            Console.WriteLine("   6. Change car details.");
        } 
        else {
            Console.WriteLine("   1. Rent a car.");
            Console.WriteLine("   2. Return a car.");
            Console.WriteLine("   3. View available cars.");
            Console.WriteLine("   4. Calculate rent price.");
        }
        Console.WriteLine("   99. Exit.");
        Console.WriteLine("Enter your choice: ");
    }
}

class BaseUser {
    protected List<Car> cars;

    protected BaseUser(List<Car> cars) {
        this.cars = cars;
    }

    public void ViewAvailableCars() {
        foreach (var car in cars) {
            if (!car.Available) {
                continue;
            }
            Console.WriteLine($"Brand: {car.Brand}, Model: {car.Model}, Color: {car.Color}, Year: {car.Year}, Price: {car.Price:F2}");
        }
    }

    protected bool CheckIndexIsValid(int index) {
        return index >= 0 && index < cars.Count;
    }

    protected bool IsCarAlreadyInTheSystem(Car car) {
        return cars.Any(existingCar => existingCar.Model == car.Model);
    }

    protected int FindCarIndex(string brand, string model) {
        return cars.FindIndex(car => car.Brand == brand && car.Model == model);
    }

}

class User : BaseUser {
    public string Name { get; private set; }
    public string Password { get; private set; }
    public double Money { get; private set; }

    public User(List<Car> cars, string name, string password, double money) : base(cars) {
        Name = name;
        Password = password;
        Money = money;
    }

    public void CalculatePrice() {
        Console.WriteLine("Enter the brand and model of the car for which you want to calculate the rent (In this format: Model Brand): ");
        string[] data = Console.ReadLine().Split(' ').ToArray();
        
        int index = FindCarIndex(data[0], data[1]);
        
        if (!CheckIndexIsValid(index)) {
            Console.WriteLine("Please enter a valid car.");
            return;
        }
        
        Console.WriteLine("Enter how many months you want to rent the car for: ");
        int time = int.Parse(Console.ReadLine());
        
        Console.WriteLine($"The price for {cars[index].Brand} {cars[index].Model} for {time} months is ${cars[index].Price * time:F2}.");
    }

    public void RentACar() {
        Console.WriteLine("Enter the brand and model of the car that you want to rent (In this format: Model Brand): ");
        string[] data = Console.ReadLine().Split(' ').ToArray();
        
        int index = FindCarIndex(data[0], data[1]);
                
        if (!CheckIndexIsValid(index)) {
            Console.WriteLine("Please enter a valid car.");
            return;
        }
        
        Console.WriteLine("Enter how many months you want to rent the car for: ");
        int time = int.Parse(Console.ReadLine());

        if (time < 1) {
            Console.WriteLine("Invalid time.");
            return;
        }

        if (!cars[index].Available) {
            Console.WriteLine("This car is already rented.");
            return;
        }

        if (cars[index].Price * time > Money) {
            Console.WriteLine("You do not have enough money to rent this car.");
            return;
        }

        Money -= cars[index].Price * time;
        cars[index].Available = false;
        Console.WriteLine($"You have rented the {cars[index].Model}. Remaining money: {Money:F2}");
    }

    public void ReturnACar() {
        Console.WriteLine("Enter the brand and model of the car that you want to return (In this format: Model Brand): ");
        string[] data = Console.ReadLine().Split(' ').ToArray();
        
        int index = FindCarIndex(data[0], data[1]);

        if (!CheckIndexIsValid(index)) {
            Console.WriteLine("Please enter a valid car.");
            return;
        }

        if (cars[index].Available) {
            Console.WriteLine("This car was not rented.");
            return;
        }

        cars[index].Available = true;
        Console.WriteLine($"You have returned the {cars[index].Model}.");
    }
}

class Admin : BaseUser {
    public Admin(List<Car> cars) : base(cars) { }

    public void AddCar() {
        Console.WriteLine("Enter car details to add (In this format: Brand Model Color Year Price Available): ");
        string[] data = Console.ReadLine().Split().ToArray();

        Car car = new Car(data[0], data[1] , data[2], int.Parse(data[3]), double.Parse(data[4]), bool.Parse(data[5]));
        if (IsCarAlreadyInTheSystem(car)) {
            Console.WriteLine("This car is already in the system.");
            return;
        }

        cars.Add(car);
        Console.WriteLine($"Car {data[1]} added to the system.");
    }

    public void DeleteCar() {
        Console.WriteLine("Enter the brand and model of the car that you want to rent (In this format: Model Brand): ");
        string[] data = Console.ReadLine().Split(' ').ToArray();
        
        int index = FindCarIndex(data[0], data[1]);

        if (!CheckIndexIsValid(index)) {
            Console.WriteLine("Please enter a valid car.");
            return;
        }

        cars.RemoveAt(index);
        Console.WriteLine("Car deleted.");
    }

    public void DeleteAllCars() {
        Console.WriteLine("Are you sure you want to delete all cars?[Y/N]");
        char input = Console.ReadLine().ToUpper()[0];

        if (input == 'N') {
            return;
        }
        
        cars.Clear();
    }

    public void ViewAllCars() {
        Console.WriteLine("All cars:");
        foreach (var car in cars) {
            Console.WriteLine($"Brand: {car.Brand}, Model: {car.Model}, Color: {car.Color}, Year: {car.Year}, Price: {car.Price:F2}, Available: {(car.Available ? "Yes" : "No")}");
        }
    }

    public void ChangeCarDetails() {
        Console.WriteLine("Enter the brand and model of the car that you want to rent (In this format: Model Brand): ");
        string[] data = Console.ReadLine().Split(' ').ToArray();
        
        int index = FindCarIndex(data[0], data[1]);

        if (!CheckIndexIsValid(index)) {
            Console.WriteLine("Please enter a valid car.");
            return;
        }

        Console.WriteLine("Enter the property to change (brand, model, color, year, price, available): ");
        string property = Console.ReadLine().ToLower();

        Console.WriteLine("Enter the new value: ");
        string newValue = Console.ReadLine();

        switch (property) {
            case "model":
                cars[index].Model = newValue;
                break;
            case "color":
                cars[index].Color = newValue;
                break;
            case "brand":
                cars[index].Brand = newValue;
                break;
            case "year":
                cars[index].Year = int.Parse(newValue);
                break;
            case "price":
                cars[index].Price = double.Parse(newValue);
                break;
            case "available":
                cars[index].Available = bool.Parse(newValue);
                break;
            default:
                Console.WriteLine("Invalid property.");
                break;
        }

        Console.WriteLine("Car details updated.");
    }
}

class Car {
    public string Model { get; set; }
    public string Color { get; set; }
    public string Brand { get; set; }
    public int Year { get; set; }
    public double Price { get; set; }
    public bool Available { get; set; }

    public Car(string brand, string model, string color, int year, double price, bool available) {
        Brand = brand;
        Model = model;
        Color = color;
        Year = year;
        Price = price;
        Available = available;
    }
}
