using Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Controller {
    public class Race {
        public Track Track { get; set; }
        public List<IParticipant> Participants { get; set; }
        public DateTime StartTime { get; set; }
        private Random _random;
        private Dictionary<Section, SectionData> _positions;
        private static Timer _timer;
        public event EventHandler<DriversChangedEventArgs> DriversChanged;
        private int[] afgelegdeAfstanden;
        private int competitors;
        private int Threshold = 100;

        public Race(Track track, List<IParticipant> participants) {
            Track = track;
            Participants = participants;
            _positions = new Dictionary<Section, SectionData>();
            _random = new Random(DateTime.Now.Millisecond);
            _timer = new Timer();
            _timer.Interval = 500;
            _timer.Elapsed += OnTimedEvent;
            assignStart();
            afgelegdeAfstanden = new int[competitors];
            for (int i = 0; i < competitors; i++) {
                afgelegdeAfstanden[i] = 0;
            }
            //start();
        }
        public SectionData getSectionData(Section section) {
            if (!_positions.ContainsKey(section)) {
                _positions[section] = new SectionData();
            }
            return _positions[section];
        }

        public void RandomizeEquipment() {
            foreach (IParticipant participant in Participants) {
                participant.Equipment.Quality = _random.Next(1, 5);
                participant.Equipment.Performance = _random.Next(5, 10);
                participant.Equipment.Speed = _random.Next(5, 10);
            }
        }

        public void assignStart() {
            List<Section> starts = new List<Section>();
            int startNR = 0;
            foreach (Section section in Track.Sections) {
                if (section.SectionType.Equals(SectionTypes.StartGrid)) {
                    starts.Add(section);
                }
            }
            for (int i = 0; i < Participants.Count(); i++) {
                if (i + 1 > starts.Count * 2) {
                    return;
                }
                if (i % 2 == 0) {
                    getSectionData(starts[startNR]).Left = Participants[i];
                } else {
                    getSectionData(starts[startNR]).Right = Participants[i];
                }
                competitors++;

                _positions[starts[startNR]] = getSectionData(starts[startNR]);
                if (i % 2 == 1) {
                    startNR++;
                }
            }
        }
        private void start() {
            _timer.Start();
            _timer.AutoReset = true;
        }

        #region moveParticipants
        //pva: 
        //1. krijg de huidige sectie
        //1.1 zoek door de posities naar de sectiedata met de deelnemer
        //2. verwijder de deelnemer uit de sectie
        //3. krijg de volgende sectie
        //3.1 zoek door de track naar de index van de huidige sectie en tel daar 1 bij op
        //4. stop de deelnemer in de goede sectiedata

        //bug: gooit exeption wanneer drivers na een lap weer bij elkaar komen
        private Section searchPositions(IParticipant participant) {
            foreach (Section section in Data.currentRace.Track.Sections) {
                SectionData sectionData = getSectionData(section);
                if (sectionData.Left is not null) {
                    if (sectionData.Left.Equals(participant)) {
                        return section;
                    }
                } else if (sectionData.Right is not null) {
                    if (sectionData.Right.Equals(participant)) {
                        return section;
                    }
                }
            }
            throw new Exception("Participant not found");
        }

        private Section findNextSection(Section section, int sections) {
            int newIndex = findIndex(section) + sections;
            if (newIndex >= Track.Sections.Count) {
                newIndex -= Track.Sections.Count;
            }
            Section next = Track.Sections.ElementAt(newIndex);
            int i = 1;
            while (getSectionData(next).Left is not null && getSectionData(next).Right is not null) {
                if (i > sections) {
                    return section;
                } else {
                    newIndex -= i;
                    if (newIndex < 0) {
                        newIndex = Track.Sections.Count + newIndex;
                    }
                    next = Track.Sections.ElementAt(newIndex);
                    i++;
                }
            }
            return next;
        }

        private int findIndex(Section section) {
            int i = 0;
            foreach(Section loopSection in Track.Sections) {
                if (loopSection.Equals(section)) {
                    break;
                }
                i++;
            }
            return i;
        }
        //functie die de driver plaatst in de volgende sectie. 
        //driver wordt altijd op links geplaatst tenzij er een ander in dezelfde sectie staat
        private void placeDriverData(Section section, IParticipant driver, int distance) {
            SectionData data = getSectionData(section);
            if (data.Left is null) {
                data.Left = driver;
                data.DistanceLeft = distance;
            } else if(data.Right is null){
                data.Right = driver;
                data.DistanceRight = distance;
            } else {
                throw new Exception("Geen plaats in Sectie");
            }
        }
        //functie die de driver weghaalt van de oude positie
        private void removeDriverData(SectionData data, Boolean left) {
            if (left) {
                data.Left = null;
                data.DistanceLeft = 0;
            } else {
                data.Right = null;
                data.DistanceRight = 0;
            }

        }

        //timer event
        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e) {
            Boolean driversChanged = false;
            Section nextSection;

            //doorloopt alle secties in de race
            foreach (Section section in Track.Sections) {
                SectionData sectionData = getSectionData(section);
                if (sectionData.Left is not null) { //wanneer er een driver op de linker positie staat
                    //berekening voor het vooruitgaan
                    sectionData.DistanceLeft += (sectionData.Left.Equipment.Performance * sectionData.Left.Equipment.Speed);
                    if (sectionData.DistanceLeft >= Threshold) {//wanneer de driver naar de volgende moet worden verplaatst
                        int sections = (int)sectionData.DistanceLeft / Threshold;//berekent de hoeveelheid secties die de driver naar voren moet
                        nextSection = findNextSection(section, sections);//zoekt de volgende sectie
                        int newDistance = (int)(sectionData.DistanceLeft - (Threshold * sections));//berekent de 
                        placeDriverData(nextSection, sectionData.Left, newDistance);
                        removeDriverData(sectionData, true);
                        driversChanged = true;
                    }
                }
                if (sectionData.Right is not null) {
                    sectionData.DistanceRight += (sectionData.Right.Equipment.Performance * sectionData.Right.Equipment.Speed);
                    if (sectionData.DistanceRight >= Threshold) {
                        int sections = (int)sectionData.DistanceRight / Threshold;
                        nextSection = findNextSection(section, sections);
                        int newDistance = (int)(sectionData.DistanceRight - (Threshold * sections));
                        placeDriverData(nextSection, sectionData.Right, newDistance);
                        removeDriverData(sectionData, false);
                        driversChanged = true;
                    }
                }
            }
            if (driversChanged) {
                DriversChanged.Invoke(this, new DriversChangedEventArgs(Data.currentRace.Track));//raises the driversChanged event
            }
        }
    }
}
        #endregion

    
