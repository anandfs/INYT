using INYTWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INYTWebsite.Code
{
    public class ModelFactory : IModelFactory
    {
        public ServiceProviderModel Create(Tradesperson tradesperson)
        {
            if (tradesperson == null)
                return null;

            return new ServiceProviderModel()
            {
                id = tradesperson.Id,
                addressLine1 = tradesperson.AddressLine1,
                addressLine2 = tradesperson.AddressLine2,
                city = tradesperson.City,
                companyName = tradesperson.CompanyName,
                companyNumber = tradesperson.CompanyNumber,
                companySize = tradesperson.CompanyNumber,
                contactNumber = tradesperson.ContactNumber,
                country = tradesperson.Country,
                deactivationReason = tradesperson.DeactivationReason,
                description = tradesperson.Description,
                emailAddress = tradesperson.EmailAddress,
                firstName = tradesperson.FirstName,
                isActive = Convert.ToBoolean(tradesperson.IsActive),
                lastName = tradesperson.LastName,
                postcode = tradesperson.Postcode,
                tradeId = Convert.ToInt32(tradesperson.TradeId),
                website = tradesperson.Website
            };
        }

        public BookingModel Create(Booking booking)
        {
            if (booking == null)
                return null;

            BookingModel bookingModel = new BookingModel()
            {
                id = booking.Id,
                bookingAmount = Convert.ToDouble(booking.BookingAmount),
                bookingDate = Convert.ToDateTime(booking.BookingDate),
                bookingFulfilled = Convert.ToBoolean(booking.BookingFulfilled),
                bookingPaymentType = booking.BookingPaymentType,
                bookingTime = Convert.ToDateTime(booking.BookingTime),
                tradeId = Convert.ToInt32(booking.TradeId),
                customerId = Convert.ToInt32(booking.CustomerId)
            };

            return bookingModel;
        }

        public CustomerModel Create(CustomerRegistration customer)
        {
            if (customer == null)
                return null;

            return new CustomerModel()
            {
                addressLine1 = customer.AddressLine1,
                addressLine2 = customer.AddressLine2,
                city = customer.City,
                contactNumber = customer.ContactNumber,
                country = customer.Country,
                emailAddress = customer.EmailAddress,
                firstName = customer.FirstName,
                id = customer.Id,
                lastName = customer.LastName,
                postcode = customer.Postcode,
                region = customer.Region
            };
        }

        public Tradesperson Parse(ServiceProviderModel serviceprovider)
        {
            if (serviceprovider == null)
                return null;

            return new Tradesperson()
            {
                AddressLine1 = serviceprovider.addressLine1,
                AddressLine2 = serviceprovider.addressLine2,
                City = serviceprovider.city,
                CompanyName = serviceprovider.companyName,
                CompanyNumber = serviceprovider.companyNumber,
                CompanySize = serviceprovider.companyNumber,
                ContactNumber = serviceprovider.contactNumber,
                Country = serviceprovider.country,
                DeactivationReason = serviceprovider.deactivationReason,
                Description = serviceprovider.description,
                EmailAddress = serviceprovider.emailAddress,
                FirstName = serviceprovider.firstName,
                IsActive = Convert.ToBoolean(serviceprovider.isActive),
                LastName = serviceprovider.lastName,
                Postcode = serviceprovider.postcode,
                TradeId = Convert.ToInt32(serviceprovider.tradeId),
                Website = serviceprovider.website
            };
        }

        public Booking Parse(BookingModel bookingModel)
        {
            if (bookingModel == null)
                return null;

            return new Booking()
            {
                BookingAmount = Convert.ToDecimal(bookingModel.bookingAmount),
                BookingDate = Convert.ToDateTime(bookingModel.bookingDate),
                BookingFulfilled = Convert.ToBoolean(bookingModel.bookingFulfilled),
                BookingPaymentType = bookingModel.bookingPaymentType,
                BookingTime = Convert.ToDateTime(bookingModel.bookingTime),
                TradeId = Convert.ToInt32(bookingModel.tradeId),
                CustomerId = Convert.ToInt32(bookingModel.customerId)
            };
        }

        public CustomerRegistration Parse(CustomerModel customerModel)
        {
            if (customerModel == null)
                return null;

            return new CustomerRegistration()
            {
                AddressLine1 = customerModel.addressLine1,
                AddressLine2 = customerModel.addressLine2,
                City = customerModel.city,
                ContactNumber = customerModel.contactNumber,
                Country = customerModel.country,
                EmailAddress = customerModel.emailAddress,
                FirstName = customerModel.firstName,
                LastName = customerModel.lastName,
                Postcode = customerModel.postcode,
                Region = customerModel.region
            };
        }
    }
}
