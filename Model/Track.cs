using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Model {
    public class Track {
        public String Name;
        public LinkedList<Section> Sections;

        public Track(String name, SectionTypes[] sections) {
            Name = name;
            Sections = convertSections(sections);
                     
        }
        private LinkedList<Section> convertSections(SectionTypes[] sections) {
            LinkedList<Section> result = new LinkedList<Section>();
            foreach (SectionTypes type in sections) {
                result.AddLast(new Section(type));
            }
            return result;
        }
    }
}
