using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Model {
    public class Track {
        public String Name { get; set; }
        public LinkedList<Section> Sections { get; set; }
        public int RotationINT { get; set; }

        public Track(String name, SectionTypes[] sections, int rotationINT) {
            Name = name;
            Sections = ConvertSections(sections);
            this.RotationINT = rotationINT;
                     
        }
        private LinkedList<Section> ConvertSections(SectionTypes[] sections) {
            LinkedList<Section> result = new LinkedList<Section>();
            foreach (SectionTypes type in sections) {
                result.AddLast(new Section(type));
            }
            return result;
        }
    }
}
