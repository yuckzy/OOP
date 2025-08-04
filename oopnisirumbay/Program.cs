using System;
using oopnisirumbay;

namespace oopnisirumbay
{
    class Program
    {
        static void Main(string[] args)
        {
            // kotse1 is a Vehicle object (base class)
            // Vehicle is now abstract, so you cannot instantiate it directly.
            // Vehicle kotse1 = new Vehicle(); // This will cause an error

            // kotse2 is a Car object (derived from Vehicle)
            Car kotse2 = new Car();
            // kotse3 is another Car object
            Car kotse3 = new Car();

            // Calls Car's overridden StartEngine (prints "Car engine started! Vroom vroom!")
            kotse2.StartEngine();
            // Calls Car's UmaandarSa (prints "Sa Lupa")
            kotse2.UmaandarSa();
            // Sets properties for kotse3 (color, price, wheels)
            kotse3.InputData("Blue", 10000000, 4);
            // Introduces kotse3 (prints its color, price, wheels)
            kotse3.Introduce();
            // Overloaded Passenger method: welcomes a passenger by name
            kotse3.Passenger("Dwyane");
            // Overloaded Passenger method: checks if number of passengers is less than 5
            kotse3.Passenger(5);
            kotse3.Passenger(4);
            // Calls Car's overridden Travel (prints "Traveling on the road with a car!")
            kotse3.Travel();
            // Calls Car's implementation of the abstract Horn method
            kotse3.Horn();
        }
    }

    // Abstract base class representing a generic vehicle
    // Cannot be instantiated directly
    // kotse1 is a Vehicle object (base class)
    public abstract class Vehicle
    {
        protected string color; // Color of the vehicle
        protected double price; // Price of the vehicle

        // Virtual method to start the engine (can be overridden)
        public virtual void StartEngine()
        {
            Console.WriteLine("Broom broom!");
        }

        // Virtual method to travel (can be overridden)
        public virtual void Travel()
        {
            Console.WriteLine("Traveling on the road!");
        }

        // Abstract method: must be implemented by derived classes
        public abstract void Horn();
    }

    // Derived class representing a car, inherits from Vehicle
    // Inheritance allows Car to use Vehicle's properties and methods
    // kotse2 is a Car object (derived from Vehicle)
    public class Car : Vehicle
    {
        private int wheels; // Number of wheels

        // Overrides StartEngine to provide car-specific behavior
        public override void StartEngine()
        {
            Console.WriteLine("Car engine started! Vroom vroom!");
        }

        // Implements the abstract Horn method from Vehicle
        public override void Horn()
        {
            Console.WriteLine("Beep beep! (Car horn)");
        }

        // Prints where the car runs
        public void UmaandarSa()
        {
            Console.WriteLine("Sa Lupa");
        }

        // Sets the car's color, price, and number of wheels
        public void InputData(string color, double price, int wheels)
        {
            this.color = color;
            this.price = price;
            this.wheels = wheels;
        }

        // Introduces the car with its properties
        // koste3.Introduce() will print the car's details
        public void Introduce()
        {
            Console.WriteLine($"This car is {color}, costs {price} and has {wheels} wheels.");
        }

        // Overloaded method: welcomes a passenger by name
        public void Passenger(string name)
        {
            Console.WriteLine($"Welcome, {name}!");
        }

        // Overloaded method: checks if the number of passengers is less than 5
        public void Passenger(int number)
        {
            if (number < 5)
            {
                Console.WriteLine($"Kasya kayong {number}");
            }
            else
            {
                Console.WriteLine("BABA TANGINANYO!");
            }
        }

        // Overrides Travel to provide car-specific behavior
        public override void Travel()
        {
            Console.WriteLine("Traveling on the road with a car!");
        }
    }
}