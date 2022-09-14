// See https://aka.ms/new-console-template for more information
using Controller;
using Model;
internal class Program {
    private static void Main(string[] args) {
        Data.Initialize();
        Data.nextRace();
        Console.WriteLine("test print");
        Console.WriteLine(Data.currentRace.Track.Name);
        Data.nextRace();
        Console.WriteLine(Data.currentRace.Track.Name);
        for (; ; ) {
            Thread.Sleep(100);
        }
    }
}