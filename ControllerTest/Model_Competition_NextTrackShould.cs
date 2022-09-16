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
		public Competition Competition { get => _competition;  set => _competition = value;}

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
		[Test]
		public void NextTrack_OneInQueue_ReturnTrack()
		{
			// probably not be 0
			Track first = new Track("name", new SectionTypes[0]);
			Competition.Tracks.Enqueue(first);
			Track result = Competition.NextTrack();
			Assert.AreEqual(first, result);
		}
		[Test]
		public void NextTrack_OneInQueue_RemoveTrackFromQueue()
		{
			Track first = new Track("name", new SectionTypes[0]);
			Track result = Competition.NextTrack();
			result = Competition.NextTrack();
			Assert.IsNull(result);
		}
		[Test]
		public void NextTrack_TwoInQueue_ReturnNextTrack()
		{
			Track first = new Track("First", new SectionTypes[0]);
			Track second = new Track("Second", new SectionTypes[0]);
			Competition.Tracks.Enqueue(first);
			Competition.Tracks.Enqueue(second);
			Track result = Competition.NextTrack();
			Assert.That(first.Name, Is.EqualTo(result.Name));
			result = Competition.NextTrack();
			Assert.That(second.Name, Is.EqualTo(result.Name));
		}
	}
	
}
