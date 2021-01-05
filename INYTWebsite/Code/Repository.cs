﻿using INYTWebsite.CustomModels;
using INYTWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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

        internal InvoiceModel GetInvoiceById(int invoiceId)
        {
            try
            {
                var invoice = _db.Invoices.Find(invoiceId);
                if (invoice != null)
                {
                    var entity = _modelFactory.Create(invoice);
                    return entity;
                }
                else
                {
                    throw new Exception("The invoice was not present in the database");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal InvoiceModel GetInvoiceByInvoiceNumber(string invoiceNumber)
        {
            try
            {
                var invoice = _db.Invoices.Where(a => a.InvoiceNumber == invoiceNumber).FirstOrDefault();
                if (invoice != null)
                {
                    var entity = _modelFactory.Create(invoice);
                    return entity;
                }
                else
                {
                    throw new Exception("The invoice was not present in the database");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal List<InvoiceModel> GetInvoicesByCustomer(int customerId)
        {
            try
            {
                var invoices = _db.Invoices.Where(a => a.CustomerId == customerId).ToList();

                List<InvoiceModel> invoiceModels = new List<InvoiceModel>();

                invoices.ForEach(invoice => invoiceModels.Add(_modelFactory.Create(invoice)));

                return invoiceModels;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal List<InvoiceModel> GetInvoicesByServiceProvider(int serviceProviderId)
        {
            try
            {
                var invoices = _db.Invoices.Where(a => a.ServiceProviderId == serviceProviderId).ToList();

                List<InvoiceModel> invoiceModels = new List<InvoiceModel>();

                invoices.ForEach(invoice => invoiceModels.Add(_modelFactory.Create(invoice)));

                return invoiceModels;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal List<InvoiceModel> GetAllInvoices()
        {
            try
            {
                var invoices = _db.Invoices.ToList();

                List<InvoiceModel> invoiceModels = new List<InvoiceModel>();

                invoices.ForEach(invoice => invoiceModels.Add(_modelFactory.Create(invoice)));

                return invoiceModels;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal List<RatingModel> GetAllRatings()
        {
            try
            {
                var ratings = _db.Ratings.ToList();

                List<RatingModel> ratingModels = new List<RatingModel>();

                ratings.ForEach(rating => ratingModels.Add(_modelFactory.Create(rating)));

                return ratingModels;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal ServiceProviderModel GetServiceProviderByEmail(string emailAddress)
        {
            try
            {
                var serviceprovider = _db.ServiceProvider.Where(s => s.EmailAddress == emailAddress).FirstOrDefault();
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

        internal BookingModel GetBookingById(int bookingId)
        {
            try
            {
                var booking = _db.Booking.Find(bookingId);
                return _modelFactory.Create(booking);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal List<BookingModel> GetAllBookingsByCustomer(int customerId)
        {
            try
            {
                var bookings = _db.Booking.Where(a => a.CustomerId == customerId).ToList();

                List<BookingModel> bookingmodels = new List<BookingModel>();

                bookings.ForEach(booking => bookingmodels.Add(_modelFactory.Create(booking)));

                bookingmodels.ForEach(booking => booking.serviceProvider = GetServiceProvider(booking.serviceProviderId));

                return bookingmodels;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal List<ServiceProviderModel> GetServiceProvidersByService(int serviceid)
        {
            try
            {
                var serviceproviders = _db.ServiceProvider.Where(a => a.TradeId == Convert.ToInt32(serviceid)).ToList();

                List<ServiceProviderModel> spmodels = new List<ServiceProviderModel>();

                serviceproviders.ForEach(serviceprovider => spmodels.Add(_modelFactory.Create(serviceprovider)));

                return spmodels;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal SlotsModel GetAvailabilitySlotById(int id)
        {
            try
            {
                var slots = _db.AvailabilitySlots.Where(a => a.Id == Convert.ToInt32(id)).FirstOrDefault();
                if (slots != null)
                {
                    var entity = _modelFactory.Create(slots);
                    return entity;
                }
                else
                {
                    throw new Exception("The slot was not present in the database");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal List<MembershipModel> GetMemberships()
        {
            try
            {
                var memberships = _db.Membership.ToList();

                List<MembershipModel> membershipmodels = new List<MembershipModel>();

                memberships.ForEach(membership => membershipmodels.Add(_modelFactory.Create(membership)));

                return membershipmodels;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal MembershipModel GetMembershipById(int id)
        {
            try
            {
                var membership = _db.Membership.Find(id);
                return _modelFactory.Create(membership);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal List<RatingModel> GetRatings(int serviceproviderid)
        {
            try
            {
                List<RatingModel> ratingmodels = new List<RatingModel>();

                var ratings = (from r in _db.Ratings
                        join bk in _db.Booking on r.BookingId equals bk.Id
                        join s in _db.ServiceProvider on bk.ServiceProviderId equals s.Id
                        where s.Id == serviceproviderid
                        select r).ToList();

                ratings.ForEach(rating => ratingmodels.Add(_modelFactory.Create(rating)));

                return ratingmodels;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        internal bool DeleteSlot(int id)
        {
            try
            {
                var slots = _db.AvailabilitySlots.Where(a => a.Id == Convert.ToInt32(id)).FirstOrDefault();
                if (slots != null)
                {
                    _db.AvailabilitySlots.Remove(slots);
                    return (_db.SaveChanges() > 0); 
                }
                else
                {
                    throw new Exception("The slot was not present in the database");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal bool UpdateQuestions(ServiceProviderAdditionalQuestionsModel originalQuestions, 
            ServiceProviderAdditionalQuestionsModel updatedQuestions)
        {
            try
            {
                var question = _db.ServiceProviderAdditionalQuestions.Find(updatedQuestions.id);
                question.AdditionalQuestion = updatedQuestions.additionalQuestion;
                question.AnswerOptions = updatedQuestions.answerOptions;
                question.AnswerOptionType = updatedQuestions.answerOptionType;
                question.ServiceProviderId = updatedQuestions.serviceProviderId;
                _db.SaveChanges(true);
                return true;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        internal bool UpdateBooking(BookingModel bookingModel)
        {
            try
            {
                Booking originalBooking = _db.Booking.Find(bookingModel.id);
                Booking updatedBooking = _modelFactory.Parse(bookingModel);

                _db.Entry(originalBooking).CurrentValues.SetValues(updatedBooking);

                return _db.SaveChanges() > 0;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal bool DeleteQuestion(int id)
        {
            try
            {
                var question = _db.ServiceProviderAdditionalQuestions.Where(a => a.Id == Convert.ToInt32(id)).FirstOrDefault();
                if (question != null)
                {
                    _db.ServiceProviderAdditionalQuestions.Remove(question);
                    return (_db.SaveChanges() > 0);
                }
                else
                {
                    throw new Exception("The question was not present in the database");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        internal bool UpdateService(ServiceModel originalService, ServiceModel updatedService)
        {
            try
            {
                var service = _db.Services.Find(updatedService.id);
                service.Service = updatedService.Service;
                service.ServiceDescription = updatedService.Description;
                _db.SaveChanges(true);
                return true;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal bool DeleteService(int id)
        {
            try
            {
                var service = _db.Services.Where(a => a.Id == Convert.ToInt32(id)).FirstOrDefault();
                if (service != null)
                {
                    _db.Services.Remove(service);
                    return (_db.SaveChanges() > 0);
                }
                else
                {
                    throw new Exception("The service was not present in the database");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal bool UpdateSlot(SlotsModel originalSlots, SlotsModel updatedSlots)
        {
            try
            {
                var slot = _db.AvailabilitySlots.Find(updatedSlots.id);
                slot.BreakTimeInMins = updatedSlots.breakTimeInMins;
                slot.DayOfWeek = updatedSlots.dayOfWeek;
                slot.EndTime = updatedSlots.endTime;
                slot.MinimumHours = updatedSlots.minimumHours;
                slot.MinimumRate = updatedSlots.minimumRate;
                slot.RateForAdditionalHour = updatedSlots.rateForAdditionalHour;
                slot.ServiceProviderId = updatedSlots.serviceproviderId;
                slot.StartTime = updatedSlots.startTime;
                _db.SaveChanges(true);
                return true;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal bool UpdateServiceProvider(ServiceProviderModel originalSP, ServiceProviderModel updatedSP)
        {
            try
            {
                var sp = _db.ServiceProvider.Find(updatedSP.id);
                sp.AddressLine1 = updatedSP.addressLine1;
                sp.AddressLine2 = updatedSP.addressLine2;
                sp.City = updatedSP.city;
                sp.CompanyName = updatedSP.companyName;
                sp.CompanyNumber = updatedSP.companyNumber;
                sp.CompanySize = updatedSP.companySize;
                sp.ContactNumber = updatedSP.contactNumber;
                sp.Country = updatedSP.country;
                sp.DeactivationReason = updatedSP.deactivationReason;
                sp.Description = updatedSP.description;
                sp.EmailAddress = updatedSP.emailAddress;
                sp.EmailConfirmed = updatedSP.emailConfirmed;
                sp.FirstName = updatedSP.firstName;
                sp.IsActive = updatedSP.isActive;
                sp.LastName = updatedSP.lastName;
                sp.MembershipId = updatedSP.membershipId;
                sp.Postcode = updatedSP.postcode;
                sp.Region = updatedSP.region;
                sp.Website = updatedSP.website;
                _db.SaveChanges(true);
                return true;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal bool UpdateCustomer(CustomerModel originalCust, CustomerModel updatedCust)
        {
            try
            {
                var customer = _db.CustomerRegistration.Find(updatedCust.id);
                customer.AddressLine1 = updatedCust.addressLine1;
                customer.AddressLine2 = updatedCust.addressLine2;
                customer.City = updatedCust.city;
                customer.ContactNumber = updatedCust.contactNumber;
                customer.Country = updatedCust.country;
                customer.EmailAddress = updatedCust.emailAddress;
                customer.EmailConfirmed = updatedCust.emailConfirmed;
                customer.FirstName = updatedCust.firstName;
                customer.IsActive = updatedCust.isActive;
                customer.LastName = updatedCust.lastName;
                customer.Postcode = updatedCust.postcode;
                customer.Region = updatedCust.region;
                customer.HasAgreedTc = updatedCust.hasAgreedTC;
                _db.SaveChanges(true);
                return true;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
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
            var cust = _modelFactory.Parse(customer);

            _db.CustomerRegistration.Add(cust);

            try
            {
                _db.SaveChanges();
                return _modelFactory.Create(cust);
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

        internal bool CreateService(ServiceModel model)
        {
            _db.Services.Add(_modelFactory.Parse(model));

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

        internal ServiceModel GetServiceById(int id)
        {
            try
            {
                var service = _db.Services.Find(id);
                return _modelFactory.Create(service);
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


        internal ServiceProviderModel CreateServiceProvider(ServiceProviderModel model)
        {
            var sp = _modelFactory.Parse(model);
            _db.ServiceProvider.Add(sp);

            try
            {
                _db.SaveChanges();
                return _modelFactory.Create(sp);
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal InvoiceModel CreateInvoice(InvoiceModel model)
        {
            Invoices invoices = new Invoices();
            invoices = _modelFactory.Parse(model);
            _db.Invoices.Add(invoices);

            try
            {
                _db.SaveChanges();
                model.id = invoices.Id;
                return model;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.Message);
            }

        }

        internal LoginModel CreateLogin(LoginModel model)
        {
            _db.Login.Add(_modelFactory.Parse(model));

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

        internal LoginModel GetLoginByUsername(string userName)
        {
            try
            {
                var login = _db.Login.Where(a => a.Username == userName).FirstOrDefault();
                if (login != null)
                {
                    return _modelFactory.Create(login);
                }
                else
                {
                    throw new Exception("The login details were not present in the database");
                }
            }
            catch (Exception ex)
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
                    .FromSql("Execute [GetAvailabilityDates] {0}, {1}, {2}, {3}", startDate, endDate, interval, serviceProviderId).ToList();

                List<AvailabilityDatesModel> availabilitymodels = new List<AvailabilityDatesModel>();

                dates.ForEach(date => availabilitymodels.Add(_modelFactory.Create(date)));

                return availabilitymodels;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal bool CreateSlot(SlotsModel model)
        {
            _db.AvailabilitySlots.Add(_modelFactory.Parse(model));

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

        internal bool CreateRating(RatingModel model)
        {
            _db.Ratings.Add(_modelFactory.Parse(model));

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

        internal List<ConfigurationModel> GetConfigurationsByProvider(string provider)
        {
            try
            {
                List<ConfigurationModel> confs = new List<ConfigurationModel>();
                var configurations = _db.Configuration.Where(a => a.Provider == provider).ToList();
                foreach(var conf in configurations)
                {
                    confs.Add(_modelFactory.Create(conf));
                }
                return confs;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal List<CustomerModel> GetCustomers()
        {
            try
            {
                List<CustomerModel> custs = new List<CustomerModel>();
                var customers = _db.CustomerRegistration.ToList();
                foreach (var cust in customers)
                {
                    custs.Add(_modelFactory.Create(cust));
                }
                return custs;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal List<ServiceProviderModel> GetServiceProviders()
        {
            try
            {
                List<ServiceProviderModel> sps = new List<ServiceProviderModel>();
                var serviceproviders = _db.ServiceProvider.ToList();
                foreach (var sp in serviceproviders)
                {
                    sps.Add(_modelFactory.Create(sp));
                }
                return sps;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal List<BookingModel> GetBookings()
        {
            try
            {
                List<BookingModel> bookings = new List<BookingModel>();
                var custbookings = _db.Booking.ToList();
                foreach (var booking in custbookings)
                {
                    bookings.Add(_modelFactory.Create(booking));
                }
                return bookings;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //private bool SaveAllWithAudit()
        //{
        //    try
        //    {
        //        foreach (var ent in _db.ChangeTracker.Entries().Where(p => p.State == EntityState.Added || p.State == EntityState.Deleted || p.State == EntityState.Modified))
        //        {
        //            foreach (AuditLog x in GetAuditRecordsForChange(ent))
        //            {
        //                _ctx.AuditLogs.Add(x);
        //            }
        //        }

        //        return _ctx.SaveChanges() > 0;
        //    }
        //    catch (System.Data.Entity.Validation.DbEntityValidationException dex)
        //    {
        //        String errormessages = string.Empty;
        //        foreach (var ValidationError in dex.EntityValidationErrors)
        //        {
        //            foreach (var error in ValidationError.ValidationErrors)
        //            {
        //                errormessages += error.ErrorMessage;
        //            }
        //        }
        //        throw new Exception(errormessages);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //private List<AuditLog> GetAuditRecordsForChange(DbEntityEntry dbEntry)
        //{
        //    try
        //    {
        //        List<AuditLog> result = new List<AuditLog>();

        //        DateTime changeTime = DateTime.UtcNow;

        //        // Get the Table() attribute, if one exists
        //        TableAttribute tableAttr = dbEntry.Entity.GetType().GetCustomAttributes(typeof(TableAttribute), true).SingleOrDefault() as TableAttribute;

        //        // Get table name (if it has a Table attribute, use that, otherwise get the pluralized name)
        //        string tableName = dbEntry.Entity.GetType().Name;

        //        // Get primary key value (If you have more than one key column, this will need to be adjusted)
        //        var keyNames = dbEntry.Entity.GetType().GetProperties().Where(p => p.GetCustomAttributes(typeof(KeyAttribute), false).Count() > 0).ToList();

        //        //string keyName = (!String.IsNullOrEmpty(keyNames[0].Name)) ? keyNames[0].Name : GetKeyName(dbEntry.Entity.GetType().Name.ToString()) ;
        //        //temporarily until we can resolve how to get the primary key name - the above code is not working
        //        string keyName = "Id";

        //        if (dbEntry.State == EntityState.Added)
        //        {
        //            // For Inserts, just add the whole record
        //            foreach (string propertyName in dbEntry.CurrentValues.PropertyNames)
        //            {
        //                if (!String.IsNullOrEmpty(propertyName))
        //                {
        //                    result.Add(new AuditLog()
        //                    {
        //                        AuditLogId = Guid.NewGuid(),
        //                        EventDate = changeTime,
        //                        EventType = "A",    // Added
        //                        EntityName = tableName,
        //                        RecordId = dbEntry.CurrentValues.GetValue<object>(keyName).ToString(),
        //                        ColumnName = propertyName,
        //                        NewValue = dbEntry.CurrentValues.GetValue<object>(propertyName) == null ? null : dbEntry.CurrentValues.GetValue<object>(propertyName).ToString()
        //                    });
        //                }
        //            }
        //        }
        //        else if (dbEntry.State == EntityState.Deleted)
        //        {
        //            // Same with deletes
        //            result.Add(new AuditLog()
        //            {
        //                AuditLogId = Guid.NewGuid(),
        //                EventDate = changeTime,
        //                EventType = "D", // Deleted
        //                EntityName = tableName,
        //                RecordId = dbEntry.OriginalValues.GetValue<object>(keyName).ToString(),
        //                ColumnName = "*ALL",
        //                NewValue = dbEntry.OriginalValues.ToObject().ToString()
        //            });
        //        }
        //        else if (dbEntry.State == EntityState.Modified)
        //        {
        //            foreach (string propertyName in dbEntry.OriginalValues.PropertyNames)
        //            {
        //                if (!String.IsNullOrEmpty(propertyName))
        //                {
        //                    // For updates, we only want to capture the columns that actually changed
        //                    if (!object.Equals(dbEntry.OriginalValues.GetValue<object>(propertyName), dbEntry.CurrentValues.GetValue<object>(propertyName)))
        //                    {
        //                        result.Add(new AuditLog()
        //                        {
        //                            AuditLogId = Guid.NewGuid(),
        //                            EventDate = changeTime,
        //                            EventType = "M",    // Modified
        //                            EntityName = tableName,
        //                            RecordId = dbEntry.OriginalValues.GetValue<object>(keyName).ToString(),
        //                            ColumnName = propertyName,
        //                            OriginalValue = dbEntry.OriginalValues.GetValue<object>(propertyName) == null ? null : dbEntry.OriginalValues.GetValue<object>(propertyName).ToString(),
        //                            NewValue = dbEntry.CurrentValues.GetValue<object>(propertyName) == null ? null : dbEntry.CurrentValues.GetValue<object>(propertyName).ToString()
        //                        });
        //                    }
        //                }
        //            }
        //        }
        //        // Otherwise, don't do anything, we don't care about Unchanged or Detached entities
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}
    }
}
