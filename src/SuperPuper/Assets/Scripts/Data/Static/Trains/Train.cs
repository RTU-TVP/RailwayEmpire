using System;

namespace Data.Static.Trains
{
	public class Train
	{
		public Train(RailwayCarriage[] railwayCarriages)
		{
			RailwayCarriages = railwayCarriages;
		}

		public Train(RailwayCarriagesDatabase railwayCarriagesDatabase, int countRailwayCarriages)
		{
			RailwayCarriages = GenerateRailwayCarriages(railwayCarriagesDatabase, countRailwayCarriages);
		}

		public RailwayCarriage[] RailwayCarriages { get; set; }

		private static RailwayCarriage[] GenerateRailwayCarriages(RailwayCarriagesDatabase railwayCarriagesDatabase,
			int countRailwayCarriages)
		{
			var railwayCarriages = new RailwayCarriage[countRailwayCarriages];
			for (var i = 0; i < countRailwayCarriages; i++)
			{
				railwayCarriages[i] = railwayCarriagesDatabase.GetRandomRailwayCarriage();
			}

			return railwayCarriages;
		}
	}
}