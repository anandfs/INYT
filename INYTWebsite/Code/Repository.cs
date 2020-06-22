using INYTWebsite.CustomModels;
using INYTWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace INYTWebsite.Code
{
    public class Repository
    {
        public INYTContext _db;
        private ModelFactory _modelFactory;

        public Repository(INYTContext db, ModelFactory modelFactory)
        {
            _db = db;
            _modelFactory = modelFactory;
        }


        internal ServiceProviderModel GetServiceProvider(int serviceproviderid)
        {
            try
            {
                var serviceprovider = _db.ServiceProvider.Find(serviceproviderid);
                if (serviceprovider != null)
                {
                    var entity = _modelFactory.Create(serviceprovider);
                    return entity;
                }
                else
                {
                    throw new Exception("The service provider was not present in the database");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        internal List<ServiceProviderAdditionalQuestionsModel> GetAdditionalQuestions(int serviceProviderId)
        {
            try
            {
                var questions = _db.ServiceProviderAdditionalQuestions.Where(a => a.ServiceProviderId == serviceProviderId).ToList();

                List<ServiceProviderAdditionalQuestionsModel> questionModels = new List<ServiceProviderAdditionalQuestionsModel>();

                questions.ForEach(question => questionModels.Add(_modelFactory.Create(question)));

                return questionModels;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal List<BookingModel> GetAllBookings(int serviceproviderid)
        {
            try
            {
                var bookings = _db.Booking.Where(a => a.ServiceProviderId == serviceproviderid).ToList();

                List<BookingModel> bookingmodels = new List<BookingModel>();

                bookings.ForEach(booking => bookingmodels.Add(_modelFactory.Create(booking)));

                bookingmodels.ForEach(booking => booking.customer = GetCustomer(booking.customerId));

                return bookingmodels;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal List<ServiceProvider> GetServiceProvidersByService(int serviceid)
        {
            try
            {
                var serviceproviders = _db.ServiceProvider.Where(a => a.TradeId == Convert.ToInt32(serviceid)).ToList();

                return serviceproviders;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal List<SlotsModel> GetAvailabilitySlots(int serviceproviderid)
        {
            try
            {
                var slots = _db.AvailabilitySlots.Where(a => a.ServiceProviderId == serviceproviderid).ToList();

                List<SlotsModel> slotmodels = new List<SlotsModel>();

                slots.ForEach(booking => slotmodels.Add(_modelFactory.Create(booking)));

                return slotmodels;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal ServiceProviderAdditionalQuestionsModel GetServiceProviderQuestion(int id)
        {
            try
            {
                var serviceprovider_question = _db.ServiceProviderAdditionalQuestions.Find(id);
                if (serviceprovider_question != null)
                {
                    var entity = _modelFactory.Create(serviceprovider_question);
                    return entity;
                }
                else
                {
                    throw new Exception("The service provider was not present in the database");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal CustomerModel GetCustomerByEmail(string emailAddress)
        {
            try
            {
                var customer = _db.CustomerRegistration.Where(a => a.EmailAddress == emailAddress).FirstOrDefault();
                if (customer != null)
                {
                    return _modelFactory.Create(customer);
                }
                else
                {
                    throw new Exception("The customer was not present in the database");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal CustomerModel CreateCustomer(CustomerModel customer)
        {
            _db.CustomerRegistration.Add(_modelFactory.Parse(customer));

            try
            {
                _db.SaveChanges();
                return customer;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal bool CreateAdditionalQuestions(ServiceProviderAdditionalQuestionsModel answer)
        {
            _db.ServiceProviderAdditionalQuestions.Add(_modelFactory.Parse(answer));

            try
            {
                _db.SaveChanges();
                return true;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal CustomerModel GetCustomer(int id)
        {
            try
            {
                CustomerRegistration cust = new CustomerRegistration();
                cust = _db.CustomerRegistration.Find(id);
                return _modelFactory.Create(cust);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal List<ServiceModel> GetServices()
        {
            try
            {
                var services = _db.Services.ToList();

                List<ServiceModel> servicemodels = new List<ServiceModel>();

                services.ForEach(service => servicemodels.Add(_modelFactory.Create(service)));

                return servicemodels;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal BookingModel CreateBooking(BookingModel model)
        {
            _db.Booking.Add(_modelFactory.Parse(model));

            try
            {
                _db.SaveChanges();
                return model;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal bool CreateAdditionalQuestions(AdditionalQuestionsModel answer)
        {
            _db.AdditionalQuestionAnswers.Add(_modelFactory.Parse(answer));

            try
            {
                _db.SaveChanges();
                return true;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal List<AvailabilityDatesModel> GetAvailabilityDates(int serviceProviderId, DateTime startDate, DateTime endDate, int interval)
        {
            try
            {
                var dates = _db
                    .AvailabilityDates
                    .FromSql("Execute [GetAvailabilityDates] {0}, {1}, {2}", startDate, endDate, interval).ToList();

                List<AvailabilityDatesModel> availabilitymodels = new List<AvailabilityDatesModel>();

                dates.ForEach(date => availabilitymodels.Add(_modelFactory.Create(date)));

                return availabilitymodels;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }
    }
}
