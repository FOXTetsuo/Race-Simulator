namespace Model
{
	public class Section
	{
		public SectionTypes SectionType { get; set; }

		public Section(SectionTypes type)
		{
			SectionType = type;
		}
	}

	public enum SectionTypes
	{
		Straight,
		StraightVertical,
		CornerSE,
		CornerSW,
		CornerNE,
		CornerNW,
		Finish
	}
}
