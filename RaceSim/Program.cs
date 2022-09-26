using Controller;
using Model;
using RaceSim;
using System.ComponentModel;

internal class Program {
    private static void Main(string[] args) {
        Data.Initialize();
        Data.nextRace();
        Visual.drawTrack(new Track("test 1", new SectionTypes[] { SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight }));
        Thread.Sleep(1000);
        Visual.drawTrack(new Track("test 2", new SectionTypes[] { SectionTypes.Straight, SectionTypes.RightCorner,SectionTypes.Straight,SectionTypes.Straight }));
        Thread.Sleep(1000);
        //Visual.drawTrack(new Track("test 3", new SectionTypes[] { }));
        //Visual.drawTrack(new Track("test 2", new SectionTypes[] { SectionTypes.LeftCorner }));
        //Thread.Sleep(10);
        for (; ; ) {
            Thread.Sleep(100);
        }
    }
}