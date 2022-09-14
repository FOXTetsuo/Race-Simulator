using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Competition
    {
        public Queue<Track> Tracks { get; set; }
        public List<IParticipant>? Participants { get; set; }

        public Track NextTrack()
        {
            return null;
        }



    }
}
