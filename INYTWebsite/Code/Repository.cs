using INYTWebsite.CustomModels;
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

        internal bool UpdateSlot(SlotsModel originalSlots, SlotsModel updatedSlots)
        {
            try
            {
                var slot = _db.AvailabilitySlots.First();
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
            _db.ServiceProvider.Add(_modelFactory.Parse(model));

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
