using INYTWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INYTWebsite.Code
{
    public interface IModelFactory
    {
        ServiceProviderModel Create(Tradesperson tradesperson);
        Tradesperson Parse(ServiceProviderModel serviceprovider);

        BookingModel Create(Booking booking);
        Booking Parse(BookingModel bookingModel);

        CustomerModel Create(CustomerRegistration customer);
        CustomerRegistration Parse(CustomerModel customerModel);
    }
}
