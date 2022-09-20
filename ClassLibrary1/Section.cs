using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        LeftCorner270,
		LeftCorner0,
        RightCorner180,
		RightCorner90,
		StartGrid,
        Finish
    }
}
