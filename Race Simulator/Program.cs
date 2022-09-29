﻿using Controller;
using Model;
using Race_Simulator;
using System;

Console.BackgroundColor = ConsoleColor.DarkGreen;
Data.Initialize();
//Data.NextRace();
Data.NextRace();
Visualize.Initialize(Data.CurrentRace);

Data.CurrentRace.PlaceContestants(Data.CurrentRace.Track, Data.CurrentRace.Participants);

Visualize.DrawTrack(Data.CurrentRace.Track);
Data.CurrentRace.Start();

for (; ; )
{
	Thread.Sleep(100);
}
