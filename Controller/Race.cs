using Model;

namespace Controller {
    public class Race {
        public Track Track { get; set; }
        public List<IParticipant> Participants { get; set; }
        public DateTime StartTime { get; set; }
        private Random _random;
        private Dictionary<Section, SectionData> _positions;

        public Race(Track track, List<IParticipant> participants){
            Track = track;
            Participants = participants;
            _positions = new Dictionary<Section, SectionData>();
            _random = new Random(DateTime.Now.Millisecond);
            assignStart();
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
            }
        }

        public void assignStart() {
            List<Section> starts = new List<Section>();
            int startNR=0;
            foreach (Section section in Track.Sections) {
                if (section.SectionType.Equals(SectionTypes.StartGrid)) {
                    starts.Add(section);
                    Console.WriteLine("added");
                }
            }
            for(int i = 0; i<Participants.Count(); i++) {
                if (i > starts.Count*2) {
                    return;
                }
                if (i%2 == 0) {
                    getSectionData(starts[startNR]).Left = Participants[i];
                } else {
                    Console.WriteLine(i);
                    getSectionData(starts[startNR]).Right = Participants[i];
                }
                
                _positions[starts[startNR]] = getSectionData(starts[startNR]);
                if (i % 2 == 1) {
                    startNR++;
                }
            }
        }
    }
}