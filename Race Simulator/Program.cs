using Controller;
using Race_Simulator;
using System;

Console.BackgroundColor = ConsoleColor.DarkGreen;
Data.Initialize();
Data.NextRace();
Visualize.Initialize();
Visualize.DrawTrack(Data.CurrentRace.Track);

for (; ; )
{
	Thread.Sleep(100);
}

