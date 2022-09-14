using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Controller
{
	public static class Data
	{
		static Competition competition;
   
        public static void Initialize()
        {
            Competition competition =  new Competition();
        }
        public static void addParticipants()
        {
            competition.Participants.Add(new Driver("Joe", 2, new Car(10,10,10,false), TeamColors.Blue));
        }

    }

	
}