using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INYTDb
{
    public class DataRepository : IDataRepository
    {
        INYTEntities _ctx;

        public DataRepository(INYTEntities ctx)
        {
            _ctx = ctx;
        }


        public Tradesperson GetTradesperson(int Id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Tradesperson> GetTradespersons()
        {
            try
            {
                return _ctx.Tradespersons.AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Insert(Tradesperson tradesperson)
        {
            throw new NotImplementedException();
        }

        public bool Update(Tradesperson originalTradesperson, Tradesperson updatedTradesperson)
        {
            throw new NotImplementedException();
        }
    }
}
