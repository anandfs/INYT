using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INYTDb
{
    public interface IDataRepository
    {
        //Tradesperson CRUD
        Tradesperson GetTradesperson(int Id);
        IQueryable<Tradesperson> GetTradespersons();
        bool Insert(Tradesperson tradesperson);
        bool Update(Tradesperson originalTradesperson, Tradesperson updatedTradesperson);
        
    }
}
