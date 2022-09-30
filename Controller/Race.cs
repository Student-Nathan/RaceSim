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

        public Race(Track track, List<IParticipant> participants){
            Track = track;
            Participants = participants;
            _positions = new Dictionary<Section, SectionData>();
            _random = new Random(DateTime.Now.Millisecond);
            _timer = new Timer();
            _timer.Interval = 500;
            _timer.Elapsed += OnTimedEvent;
            assignStart();
            afgelegdeAfstanden = new int[competitors];
            for(int i = 0; i < competitors; i++) {
                afgelegdeAfstanden[i] = 0;
            }
            start();
        }
        public SectionData getSectionData(Section section) {
            if (!_positions.ContainsKey(section)) {
                _positions[section] = new SectionData();
            }
            return _positions[section];
        }

        public void RandomizeEquipment() {
            foreach (IParticipant participant in Participants) {
                participant.Equipment.Quality = _random.Next(1,20);
                participant.Equipment.Performance = _random.Next(1,20);
                participant.Equipment.Speed = _random.Next(1,20);
                //participant.Equipment.Quality = 10;
                //participant.Equipment.Performance = 10;
                //participant.Equipment.Speed = 10;
            }
        }

        public void assignStart() {
            List<Section> starts = new List<Section>();
            int startNR=0;
            foreach (Section section in Track.Sections) {
                if (section.SectionType.Equals(SectionTypes.StartGrid)) {
                    starts.Add(section);
                }
            }
            for(int i = 0; i<Participants.Count(); i++) {
                if (i+1 > starts.Count*2) {
                    return;
                }
                if (i%2 == 0) {
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
                if (getSectionData(section).Left is not null) {
                    if (getSectionData(section).Left.Equals(participant)) {
                        return section;
                    }
                } else if (getSectionData(section).Right is not null) {
                    if (getSectionData(section).Right.Equals(participant)) {
                        return section;
                    }
                }
            }
            throw new Exception("Participant not found");

        }

        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e) {
            Boolean driversChanged = false;
            for (int i = 0; i < competitors; i++) {//itterates for every driver that is actually participating
                //code to add the meters a driver makes in 1 "turn"
                afgelegdeAfstanden[i] += (Data.Competition.Participants[i].Equipment.Performance * Data.Competition.Participants[i].Equipment.Speed);
                //moves the driver to the next section when threshold is reached (100 m by default)
                if (afgelegdeAfstanden[i] > 100) {
                    int sections = afgelegdeAfstanden[i] / 100; //berekent hoeveel sections een driver mag verplaatsen
                    afgelegdeAfstanden[i] -= 100 * sections; //haalt het aantal sections van de gereden afstand af
                    Section currentSection = searchPositions(Data.Competition.Participants[i]);
                    Section nextSection = new Section(SectionTypes.Straight); //variable with placeholder value
                    
                    //code to find the next section the driver is on
                    for (int j = 0; j < sections; j++) {
                        LinkedListNode<Section> temp = Track.Sections.Find(currentSection).Next;

                        try {
                            nextSection = temp.Value;
                        } catch (NullReferenceException) {//if the driver has reached the end of the list of sections, go to the first in the list
                            temp = Track.Sections.First;
                            nextSection = temp.Value;
                        }
                    }

                    //part that removes the participant from the section their are currently on
                    if (getSectionData(currentSection).Left is not null) {
                        if (getSectionData(currentSection).Left.Equals(Data.Competition.Participants[i])) {
                            getSectionData(currentSection).Left = null;
                        }
                    } else if (getSectionData(currentSection).Right is not null) {
                        if (getSectionData(currentSection).Right.Equals(Data.Competition.Participants[i])) {
                            getSectionData(currentSection).Right = null;
                        }
                    }

                    //part that places the participant on the next available section
                    Boolean changed = false;
                    while (!changed) {//if the next section is occupied the participant will be placed in the next available spot (searches in previous sections)
                        if (getSectionData(nextSection).Left is null) {
                            getSectionData(nextSection).Left = Data.Competition.Participants[i];
                            changed = true;
                        } else if (getSectionData(nextSection).Right is null) {
                            getSectionData(nextSection).Right = Data.Competition.Participants[i];
                            changed = true;
                        } else {//part that finds the previous section in case the section is full
                            LinkedListNode<Section> temp = Track.Sections.Find(nextSection).Previous;
                            if (temp is null) {
                                temp = Track.Sections.Last;
                                nextSection = temp.Value;
                            } else {
                                nextSection = temp.Value;
                            }
                        }
                    }
                    driversChanged = true;
                }
            }
            if (driversChanged) {
                DriversChanged.Invoke(this, new DriversChangedEventArgs(Data.currentRace.Track));//raises the driversChanged event
            }
        }
        #endregion
    }
}