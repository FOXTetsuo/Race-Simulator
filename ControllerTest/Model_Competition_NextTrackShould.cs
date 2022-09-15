using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace ControllerTest
{
	[TestFixture]
	public class Model_Competition_NextTrackShould
	{
		private Competition _competition;
		public Competition Competition { get { return _competition; } set { _competition = value; } }
		[SetUp]
		public void SetUp()
		{
			_competition = new Competition();
		}
		[Test]
		public void NextTrack_EmptyQueue_ReturnNull()
		{
			Track result = Competition.NextTrack();
			Assert.IsNull(result);
		}
	}
	
}
