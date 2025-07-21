using System;

class Program
{
    static void Main()
    {
        string[] beat = { "Boom", "Clap", "Boom", "Clap", "Boom Boom", "Clap" };
        foreach (var sound in beat)
        {
            Console.WriteLine(sound);
            System.Threading.Thread.Sleep(500); // 500ms pause for rhythm
        }
    }
} 