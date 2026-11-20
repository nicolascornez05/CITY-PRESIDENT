using System;
using System.Collections.Generic;
using UnityEngine;

namespace CityPresident.World
{
    [Serializable]
    public class CityState
    {
        public string id;
        public string cityName;
        public string regionName;
        public bool isCapital;
        [Min(0)] public int population;
        [Min(0)] public long municipalBudget;
        [Range(0f, 100f)] public float satisfaction = 60f;
        [Range(0f, 100f)] public float unemploymentRate = 7f;
    }

    /// <summary>Serializable state for one offline country save.</summary>
    [Serializable]
    public class CountryState
    {
        public string countryName = "Novaria";
        public string currencyCode = "EUR";
        public int population;
        public long nationalBudget;
        public long publicDebt;
        public List<CityState> cities = new List<CityState>();

        public void RecalculatePopulation()
        {
            long total = 0;
            foreach (CityState city in cities)
            {
                if (city != null) total += Math.Max(0, city.population);
            }

            population = (int)Math.Min(int.MaxValue, total);
        }
    }
}
