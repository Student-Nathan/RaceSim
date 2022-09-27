using Controller;
using Model;
using RaceSim;
using System.ComponentModel;

internal class Program {
    private static void Main(string[] args) {
        Data.Initialize();
        Data.nextRace();
        Visual.drawTrack(Data.currentRace.Track,3);
        //Visual.drawTrack(new Track("test 4", new SectionTypes[] { SectionTypes.RightCorner, SectionTypes.StartGrid, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight }), 3);
        //Visual.drawTrack(new Track("test 1", new SectionTypes[] { SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight }),0);
        //Thread.Sleep(1000);
        //Visual.drawTrack(new Track("test 2", new SectionTypes[] { SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight }),0);
        //Thread.Sleep(1000);
        //Visual.drawTrack(new Track("test 3", new SectionTypes[] {SectionTypes.Empty,SectionTypes.Straight,SectionTypes.RightCorner }),0);
        //Thread.Sleep(1000);
        //Thread.Sleep(1000);
        //Visual.drawTrack(new Track("test 5", new SectionTypes[] { SectionTypes.RightCorner, SectionTypes.StartGrid, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight }), 2);
        for (; ; ) {
            Thread.Sleep(100);
        }
    }
}