using System;
using UnityEngine;

namespace CityPresident.Core
{
    [Serializable]
    public sealed class EconomySimulation : MonoBehaviour
    {
        [SerializeField] private double nationalBudget = 100000000000d;
        [SerializeField] private double publicDebt = 85000000000d;
        [SerializeField] private double grossDomesticProduct = 420000000000d;
        [SerializeField] private float unemploymentRate = 7.4f;
        [SerializeField] private float inflationRate = 2.1f;
        [SerializeField] private double dailyTaxRevenue = 185000000d;
        [SerializeField] private double dailyPublicSpending = 172000000d;

        public double NationalBudget => nationalBudget;
        public double PublicDebt => publicDebt;
        public double GrossDomesticProduct => grossDomesticProduct;
        public float UnemploymentRate => unemploymentRate;
        public float InflationRate => inflationRate;

        public void AdvanceDay()
        {
            nationalBudget += dailyTaxRevenue - dailyPublicSpending;
            publicDebt = Math.Max(0d, publicDebt + dailyPublicSpending - dailyTaxRevenue);
            grossDomesticProduct *= 1.00008d;
            unemploymentRate = Mathf.Clamp(unemploymentRate - 0.002f, 2f, 30f);
            inflationRate = Mathf.Clamp(inflationRate + UnityEngine.Random.Range(-0.015f, 0.02f), -1f, 15f);
        }

        public void SetTaxAndSpending(double taxRevenue, double spending)
        {
            dailyTaxRevenue = Math.Max(0d, taxRevenue);
            dailyPublicSpending = Math.Max(0d, spending);
        }
    }
}
