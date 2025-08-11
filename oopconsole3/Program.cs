using System; // Import System namespace for basic functionality

namespace oopconsole3
{
    // Main program class
    class Program
    {
        static void Main(string[] args)
        {
            // Create a Square object and set its data
            Shape iskwer = new Square();
            iskwer.setData("Yellow", 10); // Set color and side length
            iskwer.displayColor(); // Show color
            Console.WriteLine($"Area of the square: {iskwer.getArea()}"); // Show area
            iskwer.displayInfo(); // Show detailed info

            // Create a Circle object and set its data
            Shape bilog = new Circle();
            bilog.setData("Green", 5); // Set color and radius
            bilog.displayColor(); // Show color
            Console.WriteLine($"Area of the circle: {bilog.getArea()}"); // Show area
            bilog.displayInfo(); // Show detailed info

            // Dispose objects explicitly to release resources and print "Shape Destroyed"
            iskwer.Dispose();
            bilog.Dispose();

            Console.ReadLine(); // Keeps the console open to see all output
        }
    }

    // Abstract base class for all shapes, implements IDisposable for resource management
    public abstract class Shape : IDisposable
    {
        public string color; // Stores the color of the shape
        public double sideLength; // Stores the side length (or radius for circle)
        private bool disposed = false; // Tracks if Dispose has been called

        public Shape()
        {
            Console.WriteLine("Shape Created"); // Print when a shape is created
        }

        ~Shape()
        {
            Dispose(false); // Destructor calls Dispose for cleanup
        }

        // Public method to dispose the object and suppress finalization
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); // Prevents the destructor from running again
        }

        // Protected method for actual resource cleanup
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                Console.WriteLine("Shape Destroyed"); // Print when a shape is destroyed
                disposed = true;
            }
        }

        // Abstract method to get the area, must be implemented by derived classes
        public abstract double getArea();

        // Abstract method to display info, must be implemented by derived classes
        public abstract void displayInfo();

        // Abstract method to display color, must be implemented by derived classes
        public abstract void displayColor();

        // Virtual method to set data, can be overridden by derived classes
        public virtual void setData(string color, double sideLength)
        {
            this.color = color;
            this.sideLength = sideLength;
        }
    }

    // Square class inherits from Shape and implements required methods
    public class Square : Shape
    {
        // Calculates area of the square
        public override double getArea()
        {
            return sideLength * sideLength;
        }
        // Displays detailed info about the square
        public override void displayInfo()
        {
            Console.WriteLine($"Square Color: {color}, Area: {getArea()}");
        }
        // Displays the color of the square
        public override void displayColor()
        {
            Console.WriteLine($"Square Color: {color}");
        }
    }

    // Circle class inherits from Shape and implements required methods
    public class Circle : Shape
    {
        // Overrides setData to use radius instead of side length
        public override void setData(string color, double radius)
        {
            this.color = color;
            this.sideLength = radius; // Store radius in sideLength for compatibility
        }
        // Calculates area of the circle
        public override double getArea()
        {
            return Math.PI * sideLength * sideLength;
        }
        // Displays detailed info about the circle
        public override void displayInfo()
        {
            Console.WriteLine($"Circle Color: {color}, Area: {getArea()}");
        }
        // Displays the color of the circle
        public override void displayColor()
        {
            Console.WriteLine($"Circle Color: {color}");
        }
    }
}
