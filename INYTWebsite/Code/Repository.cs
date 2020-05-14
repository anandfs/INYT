using INYTWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INYTWebsite.Code
{
    public class Repository
    {
        public INYTContext _db;
        private IModelFactory _modelFactory;

        public Repository(INYTContext db, IModelFactory modelFactory)
        {
            _db = db;
            _modelFactory = modelFactory;
        }


        public List<BookingModel> GetAllBookings(int tradepersonId)
        {
            var bookings = _db.Booking.Where(a => a.TradespersonId == tradepersonId).ToList();

            List<BookingModel> bookingmodels = new List<BookingModel>();

            bookings.ForEach(booking => bookingmodels.Add(_modelFactory.Create(booking)));

            bookingmodels.ForEach(booking => booking.customer = GetCustomer(booking.customerId));

            return bookingmodels;
        }

        public CustomerModel GetCustomer(int id)
        {
            CustomerRegistration cust = new CustomerRegistration();
            cust = _db.CustomerRegistration.Find(id);
            return _modelFactory.Create(cust);
        }
    }
}
