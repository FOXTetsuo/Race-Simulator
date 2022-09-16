using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Track
    {
        public string Name { get; set; }
        public LinkedList<Section> Sections { get; set; }

		public Track(string name, SectionTypes[] sections)
        {
            Name = name;
			Sections = SectionTypeToLinkedList(sections);
        }
		public LinkedList<Section> SectionTypeToLinkedList (SectionTypes[] sectionParemeter)
		{
			LinkedList<Section> sectionlist = new LinkedList<Section>();
			foreach (SectionTypes sectionType in sectionParemeter)
			{
				Section section = new Section(sectionType);
				sectionlist.AddLast(section);
			}
			return sectionlist;
		}
    }
}
