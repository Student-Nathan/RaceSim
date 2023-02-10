using Model;
using Section = Model.Section;
using Timer = System.Timers.Timer;

namespace Controller {
    public delegate void nextRace(object sender, EventArgs e);
    public class Race {
        public Track Track { get; set; }
        public List<IParticipant> Participants { get; set; }
        public DateTime StartTime { get; set; }
        private Random _random;
        private Dictionary<Section, SectionData> _positions;
        private static Timer? _timer;
        public event EventHandler<DriversChangedEventArgs>? DriversChanged;
        public event EventHandler<UpdateRaceStatsArgs>? ParticipantFinished;
        public int CurrentCompetitorNumber { get; set; } //aanpassen logica 
        private readonly int Threshold = 100; //hoeveel meter is een sectie
        private int laps = 3; //hoeveel rondjes moet een auto hebben gereden
        private int finishedParticipants = 0;


        public Race(Track track, List<IParticipant> participants) {
            Track = track;
            Participants = participants;
            _positions = new Dictionary<Section, SectionData>();
            StartTime = DateTime.Now;
            _random = new Random(DateTime.Now.Millisecond);
            _timer = new Timer();
            _timer.Interval = 500;
            _timer.Elapsed += OnTimedEvent;
            AssignStart();

            for (int i = 0; i < participants.Count; i++) {
                participants[i].LapTime = 0;
                participants[i].PreviousTime = StartTime;
                participants[i].Laps = 0;
            }
            Start();
        }
        public Race(Track track, List<IParticipant> participants, Boolean test) {//constructor for unit tests
            Track = track;
            Participants = participants;
            _positions = new Dictionary<Section, SectionData>();
            StartTime = DateTime.Now;
            _random = new Random(DateTime.Now.Millisecond);
            _timer = new Timer();
            _timer.Interval = 500;
            _timer.Elapsed += OnTimedEvent;
            AssignStart();

            for (int i = 0; i < participants.Count; i++) {
                participants[i].LapTime = 0;
                participants[i].PreviousTime = StartTime;
                participants[i].Laps = 0;
            }
        }

        public SectionData GetSectionData(Section section) {
            if (!_positions.ContainsKey(section)) {
                _positions[section] = new SectionData();
            }
            return _positions[section];
        }

        public void RandomizeEquipment() {
            foreach (IParticipant participant in Participants) {
                participant.Equipment.Quality = _random.Next(1, 10);
                participant.Equipment.Performance = _random.Next(5, 20);
                participant.Equipment.Speed = _random.Next(4, 10);
            }
        }

        private void AssignStart() {
            List<Section> starts = new List<Section>();
            int startNR = 0;
            foreach (Section section in Track.Sections) {
                if (section.SectionType.Equals(SectionTypes.StartGrid)) {
                    starts.Add(section);
                }
            }
            for (int i = 0; i < Participants.Count; i++) {
                if (i + 1 > starts.Count * 2) {
                    return;
                }
                if (i % 2 == 0) {
                    GetSectionData(starts[startNR]).Left = Participants[i];
                } else {
                    GetSectionData(starts[startNR]).Right = Participants[i];
                }
                CurrentCompetitorNumber++;

                _positions[starts[startNR]] = GetSectionData(starts[startNR]);
                if (i % 2 == 1) {
                    startNR++;
                }
            }
        }
        private void Start() {
            if (_timer is not null) {
                _timer.Start();
                _timer.AutoReset = true;
            }
        }

        #region moveParticipants

        public Section FindNextSection(Section section, int sections) {
            Section next = section;
            if (Track.Sections is null) {
                throw new NullReferenceException("Error: Track.Sections is null");
            }
            if (Track.Sections.First is null || Track.Sections.Last is null) {
                throw new NullReferenceException("Error Track.Sections is null");
            }
            for (int i = 0; i < sections; i++) {
                if (next.Equals(Track.Sections.Last?.Value)) {
                    next = Track.Sections.First.Value;
                } else {
                    next = Track.Sections.Find(next).Next.Value;
                }
            }
            SectionData nextData = GetSectionData(next);
            if (nextData.Left is not null && nextData.Right is not null) {
                return section;
            }

            return next;
        }

        //functie die de driver plaatst in de volgende sectie. 
        //driver wordt altijd op links geplaatst tenzij er een ander in dezelfde sectie staat
        public void PlaceDriverData(Section oldSection, Section newSection, IParticipant driver, int newDistance,int distance) {
            SectionData data = GetSectionData(newSection);
            


            //sectie om de laps bij te houden

            for (int i = 0; i < distance; i++) {
                if (oldSection.SectionType.Equals(SectionTypes.Finish)) {
                    driver.Laps += 1;

                    driver.LapTime = (DateTime.Now - driver.PreviousTime).TotalSeconds;
                    driver.PreviousTime = DateTime.Now;
                    ParticipantFinished?.Invoke(this, new UpdateRaceStatsArgs(this));
                    break;
                } else {
                    oldSection = FindNextSection(oldSection, 1);
                }
            }

            //als de driver is gefinished, plaats hem niet meer
            if (driver.Laps >= laps) {
                if (finishedParticipants < 3) {
                    driver.Points += (3 - finishedParticipants);
                    finishedParticipants++;
                }
                return;
            }

            //zoniet plaats hem
            if (data.Left is null) {
                data.Left = driver;
                data.DistanceLeft = newDistance;
            } else if(data.Right is null){
                data.Right = driver;
                data.DistanceRight = newDistance;
            } else {
                throw new Exception("Geen plaats in Sectie");
            }
        }

        //functie die de driver weghaalt van de oude positie
        public void RemoveDriverData(SectionData data, Boolean left) {
            if (left) {
                data.Left = null;
                data.DistanceLeft = 0;
            } else {
                data.Right = null;
                data.DistanceRight = 0;
            }

        }



        //timer event
        private void OnTimedEvent(Object? source, System.Timers.ElapsedEventArgs e) {
            Boolean driversChanged = false;
            Boolean everyoneFinished = true;
            Section nextSection;
            Section? itterationSection;
            CheckBroken();
            //doorloopt alle secties in de race
            if (Track.Sections.Last is not null) {
               itterationSection = Track.Sections.Last.Value;
            } else {
                throw new NullReferenceException("Error: No Sections in Track.Sections");
            }
            while (itterationSection is not null) {
                SectionData sectionData = GetSectionData(itterationSection);
                if (sectionData.Left is not null) { //wanneer er een driver op de linker positie staat
                    //berekening voor het vooruitgaan
                    if (!sectionData.Left.Equipment.IsBroken) {
                        sectionData.DistanceLeft += (sectionData.Left.Equipment.Performance * sectionData.Left.Equipment.Speed);
                        if (sectionData.DistanceLeft >= Threshold) {//wanneer de driver naar de volgende moet worden verplaatst
                            int sections = (int)sectionData.DistanceLeft / Threshold;//berekent de hoeveelheid secties die de driver naar voren moet
                            nextSection = FindNextSection(itterationSection, sections);//zoekt de volgende sectie
                            int newDistance = (int)(sectionData.DistanceLeft - (Threshold * sections));//berekent de nieuwe distance om in de nieuwe sectie te plaatsen
                            if (nextSection.Equals(itterationSection)) {
                                sectionData.DistanceLeft = Threshold;
                            } else {
                                PlaceDriverData(itterationSection, nextSection, sectionData.Left, newDistance, (int)sections);
                                RemoveDriverData(sectionData, true);
                                driversChanged = true;
                            }
                        }
                    } else {
                        driversChanged = true;
                    }
                    everyoneFinished = false;
                }
                if (sectionData.Right is not null) {//zelfde, maar dan voor rechts
                    if (!sectionData.Right.Equipment.IsBroken) {
                        sectionData.DistanceRight += (sectionData.Right.Equipment.Performance * sectionData.Right.Equipment.Speed);
                        if (sectionData.DistanceRight >= Threshold) {
                            int sections = (int)sectionData.DistanceRight / Threshold;//berekent de hoeveelheid secties die de driver naar voren moet
                            nextSection = FindNextSection(itterationSection, sections);//zoekt de volgende sectie
                            if (nextSection.Equals(itterationSection)) {
                                sectionData.DistanceRight = Threshold;
                            } else {
                                int newDistance = (int)(sectionData.DistanceRight - (Threshold * sections));//berekent de nieuwe distance om in de nieuwe sectie te plaatsen
                                PlaceDriverData(itterationSection, nextSection, sectionData.Right, newDistance, (int)sections);
                                RemoveDriverData(sectionData, false);
                            }
                            driversChanged = true;

                        }
                    } else {
                        driversChanged = true;
                    }
                    everyoneFinished = false;
                }
                itterationSection = Track.Sections.Find(itterationSection)?.Previous?.Value;
            }

            if (driversChanged) {
                if (DriversChanged is not null) {
                    if (Data.CurrentRace is not null) {
                        DriversChanged?.Invoke(this, new DriversChangedEventArgs(Data.CurrentRace.Track));//raises the driversChanged event
                    }
                }
            }
            if (everyoneFinished) {
                Data.NextRace();
            }
        }
        #endregion
        public void Cleanup() {
            if (_timer is not null) {
                _timer.Stop();
            }
            DriversChanged = null;
            ParticipantFinished = null;
            
        }

        private void CheckBroken() {
            for(int i = 0; i<CurrentCompetitorNumber; i++) {
                int random = _random.Next(0, Participants[i].Equipment.Quality*10);
                if (!Participants[i].Equipment.IsBroken) {
                    if (random == 1) {
                        Participants[i].Equipment.IsBroken = true;
                        if (Participants[i].Equipment.Performance > 3) {
                            Participants[i].Equipment.Performance -= 1;
                        } else {
                            Participants[i].Equipment.Quality += 1;
                        }
                    }
                } else {
                    if (random <= Participants[i].Equipment.Quality * 25) {
                        Participants[i].Equipment.IsBroken = false;
                    }
                }
            }
        }
    }
}
        

    
