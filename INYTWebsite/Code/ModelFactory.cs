using INYTWebsite.CustomModels;
using INYTWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INYTWebsite.Code
{
    public class ModelFactory
    {
        #region CREATE METHODS

        public ServiceProviderModel Create(ServiceProvider serviceprovider)
        {
            if (serviceprovider == null)
                return null;

            return new ServiceProviderModel()
            {
                id = serviceprovider.Id,
                addressLine1 = serviceprovider.AddressLine1,
                addressLine2 = serviceprovider.AddressLine2,
                city = serviceprovider.City,
                companyName = serviceprovider.CompanyName,
                companyNumber = serviceprovider.CompanyNumber,
                companySize = serviceprovider.CompanyNumber,
                contactNumber = serviceprovider.ContactNumber,
                country = serviceprovider.Country,
                deactivationReason = serviceprovider.DeactivationReason,
                description = serviceprovider.Description,
                emailAddress = serviceprovider.EmailAddress,
                firstName = serviceprovider.FirstName,
                isActive = Convert.ToBoolean(serviceprovider.IsActive),
                lastName = serviceprovider.LastName,
                postcode = serviceprovider.Postcode,
                tradeId = Convert.ToInt32(serviceprovider.TradeId),
                website = serviceprovider.Website
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
                bookingAccepted = Convert.ToBoolean(booking.BookingAccepted),
                bookingTime = Convert.ToDateTime(booking.BookingTime),
                serviceProviderId = Convert.ToInt32(booking.ServiceId),
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

        public SlotsModel Create(AvailabilitySlots slot)
        {
            if (slot == null)
                return null;

            return new SlotsModel()
            {
                dayOfWeek = slot.DayOfWeek,
                endTime = Convert.ToDateTime(slot.EndTime),
                id = slot.Id,
                maxBookingsPerDay = Convert.ToInt32(slot.MaxBookingsPerDay),
                startTime = Convert.ToDateTime(slot.StartTime),
                serviceproviderId = Convert.ToInt32(slot.ServiceProviderId)
            };
        }

        public ServiceProviderAdditionalQuestionsModel Create(ServiceProviderAdditionalQuestions question)
        {
            if (question == null)
                return null;

            return new ServiceProviderAdditionalQuestionsModel()
            {
                additionalQuestion = question.AdditionalQuestion,
                answerOptions = question.AnswerOptions,
                answerOptionType = question.AnswerOptionType,
                serviceProviderId = Convert.ToInt32(question.ServiceProviderId),
                id = question.Id
            };
        }

        public ServiceModel Create(Services service)
        {
            if (service == null)
                return null;

            return new ServiceModel()
            {
                id = service.Id,
                Service = service.Service,
                Description = service.ServiceDescription
            };
        }

        public AdditionalQuestionsModel Create(AdditionalQuestionAnswers answers)
        {
            if (answers == null)
                return null;

            return new AdditionalQuestionsModel()
            {
                additionalQuestion = answers.AdditionalQuestion,
                answer = answers.Answer,
                bookingId = Convert.ToInt32(answers.BookingId),
                customerId = Convert.ToInt32(answers.CustomerId),
                id = answers.Id,
                serviceId = Convert.ToInt32(answers.ServiceId)
            };
        }
        
        #endregion

        #region PARSE METHODS

        public ServiceProvider Parse(ServiceProviderModel serviceprovider)
        {
            if (serviceprovider == null)
                return null;

            return new ServiceProvider()
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
                BookingAccepted = bookingModel.bookingAccepted,
                ServiceId = Convert.ToInt32(bookingModel.serviceProviderId),
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

        public AvailabilitySlots Parse(SlotsModel slotModel)
        {
            if (slotModel == null)
                return null;

            return new AvailabilitySlots()
            {
                DayOfWeek = slotModel.dayOfWeek,
                EndTime = Convert.ToDateTime(slotModel.endTime),
                Id = slotModel.id,
                MaxBookingsPerDay = Convert.ToInt32(slotModel.maxBookingsPerDay),
                StartTime = Convert.ToDateTime(slotModel.startTime),
                ServiceProviderId = Convert.ToInt32(slotModel.serviceproviderId)
            };
        }

        internal AvailabilityDatesModel Create(AvailabilityDates date)
        {
            if (date == null)
                return null;

            return new AvailabilityDatesModel()
            {
                availability = date.Availability,
                availabilityDate = date.Dates,
                serviceProviderId = date.ServiceProviderId,
                weekName = date.WeekName
            };
        }

        public ServiceProviderAdditionalQuestions Parse(ServiceProviderAdditionalQuestionsModel questionModel)
        {
            if (questionModel == null)
                return null;

            return new ServiceProviderAdditionalQuestions()
            {
                AdditionalQuestion = questionModel.additionalQuestion,
                AnswerOptions = questionModel.answerOptions,
                AnswerOptionType = questionModel.answerOptionType,
                ServiceProviderId = Convert.ToInt32(questionModel.serviceProviderId),
                Id = Convert.ToInt32(questionModel.id)
            };
        }

        public Services Parse(ServiceModel serviceModel)
        {
            if (serviceModel == null)
                return null;

            return new Services()
            {
                Id = serviceModel.id,
                Service = serviceModel.Service,
                ServiceDescription = serviceModel.Description
            };
        }

        public AdditionalQuestionAnswers Parse(AdditionalQuestionsModel answers)
        {
            if (answers == null)
                return null;

            return new AdditionalQuestionAnswers()
            {
                AdditionalQuestion = answers.additionalQuestion,
                Answer = answers.answer,
                BookingId = answers.bookingId,
                CustomerId = answers.customerId,
                Id = answers.id,
                ServiceId = answers.serviceId
            };
        }

        internal AvailabilityDates Parse(AvailabilityDatesModel date)
        {
            if (date == null)
                return null;

            return new AvailabilityDates()
            {
                Availability = date.availability,
                Dates = date.availabilityDate,
                ServiceProviderId = date.serviceProviderId,
                WeekName = date.weekName
            };
        }

        #endregion
    }
}
