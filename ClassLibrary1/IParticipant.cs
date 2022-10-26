using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Model
{
    public interface IParticipant : INotifyPropertyChanged
    {
		public string Name { get; set; }
        public int Points { get; set; }
        public IEquipment Equipment  { get; set; }
        public TeamColors TeamColor { get; set; }
		public Section CurrentSection { get; set; }
		public string ImageSource { get; set; }
		public string ImageSourceWinner { get; set; }
		public string BrokeImageSource { get; set; }
		public int DistanceCovered { get; set; }
		public int LoopsPassed { get; set; }
		public string FunFact { get; set; }
    }

    public enum TeamColors
    {
        Red,
        Green,
        Purple,
        Orange
    }
}
