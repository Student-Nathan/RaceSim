﻿using Model;

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
            _random = new Random(DateTime.Now.Millisecond);
            
        }
        public SectionData getSectionData(Section section) {
            if (!_positions.ContainsKey(section)) {
                _positions[section] = new SectionData();
            }
            return _positions[section];
        }//voor in college: vraag naar constructors

        public void RandomizeEquipment() {
            foreach (var participant in Participants) {
                participant.Equipment.Quality = _random.Next(DateTime.Now.Millisecond);
                participant.Equipment.Performance = _random.Next(DateTime.Now.Millisecond);
            }
        }
    }
}