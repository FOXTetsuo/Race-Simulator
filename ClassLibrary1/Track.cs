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

		//Turns an array of sectiontypes into a linkedlist of sections.
		public LinkedList<Section> SectionTypeToLinkedList(SectionTypes[] sectionParameter)
		{
			LinkedList<Section> sectionlist = new LinkedList<Section>();
			foreach (SectionTypes sectionType in sectionParameter)
			{
				sectionlist.AddLast(new Section(sectionType));
			}
			return sectionlist;
		}
	}
}
