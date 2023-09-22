using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Static.Trains
{
	[Serializable]
	public class Train
	{
		public Train(RailwayCarriage[] railwayCarriages)
		{
			RailwayCarriages = railwayCarriages;
			Lifetime = GetLifetime(RailwayCarriages);
		}

		public Train(RailwayCarriagesDatabase railwayCarriagesDatabase, int countRailwayCarriages)
		{
			RailwayCarriages = GenerateRailwayCarriages(railwayCarriagesDatabase, countRailwayCarriages);
			Lifetime = GetLifetime(RailwayCarriages);
		}

		public float Lifetime { get; private set; } = 0;
		public RailwayCarriage[] RailwayCarriages { get; private set; }

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

		private float GetLifetime(IEnumerable<RailwayCarriage> railwayCarriages)
		{
			return railwayCarriages.Sum(railwayCarriage => railwayCarriage.Lifetime);
		}
	}
}