using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Walletico.Models;
using Walletico.Shared;

namespace Walletico.DataServices
{
    public class DataService : IDataService
    {
        public IEnumerable<Period> GetAllMonths() => 
            DateTimeFormatInfo.CurrentInfo.AbbreviatedMonthNames.Where(c => !string.IsNullOrEmpty(c)).Select(c => new Period { Month = c.Replace(".", "").ToUpperInvariant() }).ToList();

        public IEnumerable<Transaction> GetAllPerMonthTransactions(int month) => new List<Transaction>
            {
                new Transaction
                {
                    Amount = 1200.63M,
                    Description = "Gasolina",
                    EntryDate = DateTime.Now.AddMinutes(15),
                    TransType = 1
                },
                new Transaction
                {
                    Amount = 8500.74M,
                    Description = "Salario",
                    EntryDate = DateTime.Now.AddMonths(2),
                    TransType = 0
                },
            };

        public async Task<string[]> PlacesNearby(string latitud, string longitud)
        {
            var pato = await HttpClientSingleton.HttpClient.GetAsync($"https://api.mapbox.com/geocoding/v5/mapbox.places/{longitud},{latitud}.json?access_token={Secrets.PlaceAPIKey}");
            return null;
        }
    }
}
