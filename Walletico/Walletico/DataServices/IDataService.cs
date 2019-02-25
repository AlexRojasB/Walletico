using System.Collections.Generic;
using Walletico.Models;

namespace Walletico.DataServices
{
    public interface IDataService
    {
        IEnumerable<Period> GetAllMonths();
        IEnumerable<Transaction> GetAllPerMonthTransactions(int month);
    }
}
