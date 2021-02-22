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

        public ConfigurationModel Create(Configuration configuration)
        {
            if (configuration == null)
                return null;

            return new ConfigurationModel()
            {
                id = configuration.Id,
                keyName = configuration.KeyName,
                keyValue = configuration.KeyValue,
                provider = configuration.Provider
            };
        }

        public InvoiceModel Create(Invoices invoice)
        {
            if (invoice == null)
                return null;

            return new InvoiceModel()
            {
                amount = Convert.ToDouble(invoice.Amount),
                paypalBookingReference = invoice.PaypalBookingReference,
                customerId = Convert.ToInt32(invoice.CustomerId),
                id = Convert.ToInt32(invoice.Id),
                invoiceNumber = invoice.InvoiceNumber,
                paidDate = Convert.ToDateTime(invoice.PaidDate),
                serviceProviderId = Convert.ToInt32(invoice.ServiceProviderId)
            };
        }

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
                isVATRegistered = serviceprovider.IsVatRegistered,
                VATNumber = serviceprovider.Vatnumber,
                contactNumber = serviceprovider.ContactNumber,
                country = serviceprovider.Country,
                deactivationReason = serviceprovider.DeactivationReason,
                description = serviceprovider.Description,
                emailAddress = serviceprovider.EmailAddress,
                firstName = serviceprovider.FirstName,
                isActive = Convert.ToBoolean(serviceprovider.IsActive),
                lastName = serviceprovider.LastName,
                postcode = serviceprovider.Postcode,
                serviceId = Convert.ToInt32(serviceprovider.TradeId),
                website = serviceprovider.Website,
                membershipId = Convert.ToInt32(serviceprovider.MembershipId),
                emailConfirmed = Convert.ToBoolean(serviceprovider.EmailConfirmed),
                VerifyCode = serviceprovider.VerifyCode
            };
        }

        public LoginModel Create(Login login)
        {
            if (login == null)
                return null;

            LoginModel loginModel = new LoginModel()
            {
                id = login.Id,
                createdDate = Convert.ToDateTime(login.CreatedDate),
                userid = Convert.ToInt32(login.UserId),
                password = login.Password,
                userName = login.Username
            };

            return loginModel;
        }

        public RatingModel Create(Ratings rating)
        {
            if (rating == null)
                return null;

            RatingModel ratingModel = new RatingModel()
            {
                bookingId = Convert.ToInt32(rating.BookingId),
                createdBy = Convert.ToInt32(rating.CreatedBy),
                createdDate = Convert.ToDateTime(rating.CreatedDate),
                customerId = Convert.ToInt32(rating.CustomerId),
                display = Convert.ToBoolean(rating.Display),
                id = rating.Id,
                ratingComments = rating.RatingComments,
                ratings = Convert.ToInt32(rating.Ratings1)
            };

            return ratingModel;
        }

        public MembershipModel Create(Membership membership)
        {
            if (membership == null)
                return null;

            MembershipModel membershipModel = new MembershipModel()
            {
                id = membership.Id,
                basicSubscriptionFee = Convert.ToDouble(membership.BasicSubscriptionFee),
                commission = Convert.ToDouble(membership.Commission),
                description = membership.Description,
                name = membership.Name
            };

            return membershipModel;
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
                bookingHours = Convert.ToInt32(booking.BookingHours),
                bookingReference = booking.PaypalBookingReference,
                serviceProviderId = Convert.ToInt32(booking.ServiceProviderId),
                serviceId = Convert.ToInt32(booking.ServiceId),
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
                region = customer.Region,
                isActive = Convert.ToBoolean(customer.IsActive),
                hasAgreedTC = Convert.ToBoolean(customer.HasAgreedTc),
                emailConfirmed = Convert.ToBoolean(customer.EmailConfirmed),
                VerifyCode = customer.VerifyCode
            };
        }

        public SlotsModel Create(AvailabilitySlots slot)
        {
            if (slot == null)
                return null;

            return new SlotsModel()
            {
                dayOfWeek = slot.DayOfWeek,
                minimumHours = Convert.ToInt32(slot.MinimumHours),
                minimumRate = Convert.ToInt32(slot.MinimumRate),
                rateForAdditionalHour = slot.RateForAdditionalHour,                
                endTime = Convert.ToDateTime(slot.EndTime),
                breakTimeInMins = Convert.ToInt32(slot.BreakTimeInMins),
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
                IsVatRegistered = serviceprovider.isVATRegistered,
                Vatnumber = serviceprovider.VATNumber,
                ContactNumber = serviceprovider.contactNumber,
                Country = serviceprovider.country,
                DeactivationReason = serviceprovider.deactivationReason,
                Description = serviceprovider.description,
                EmailAddress = serviceprovider.emailAddress,
                FirstName = serviceprovider.firstName,
                IsActive = Convert.ToBoolean(serviceprovider.isActive),
                LastName = serviceprovider.lastName,
                Postcode = serviceprovider.postcode,
                TradeId = Convert.ToInt32(serviceprovider.serviceId),
                Website = serviceprovider.website,
                MembershipId = Convert.ToInt32(serviceprovider.membershipId),
                EmailConfirmed = Convert.ToBoolean(serviceprovider.emailConfirmed),
                VerifyCode = serviceprovider.VerifyCode
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
                BookingHours = Convert.ToInt32(bookingModel.bookingHours),
                BookingAccepted = bookingModel.bookingAccepted,
                PaypalBookingReference = bookingModel.bookingReference,
                ServiceId = Convert.ToInt32(bookingModel.serviceId),
                ServiceProviderId = Convert.ToInt32(bookingModel.serviceProviderId),
                CustomerId = Convert.ToInt32(bookingModel.customerId),
                Id = bookingModel.id
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
                Region = customerModel.region,
                HasAgreedTc = customerModel.hasAgreedTC,
                IsActive = customerModel.isActive,
                EmailConfirmed = customerModel.emailConfirmed,
                VerifyCode = customerModel.VerifyCode
            };
        }

        public AvailabilitySlots Parse(SlotsModel slotModel)
        {
            if (slotModel == null)
                return null;

            return new AvailabilitySlots()
            {
                DayOfWeek = slotModel.dayOfWeek,
                MinimumHours = slotModel.minimumHours,
                MinimumRate = slotModel.minimumRate,
                RateForAdditionalHour = slotModel.rateForAdditionalHour,
                BreakTimeInMins = slotModel.breakTimeInMins,
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
                weekName = date.WeekName,
                additionalRate = date.additionalRate,
                id = date.ID,
                minHours = date.minHours,
                minRate = date.minRate,
                startTime = date.startTime,
                endTime = date.endTime
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

        public Ratings Parse(RatingModel ratingModel)
        {
            if (ratingModel == null)
                return null;

            Ratings rating = new Ratings()
            {
                BookingId = Convert.ToInt32(ratingModel.bookingId),
                CreatedBy = Convert.ToInt32(ratingModel.createdBy),
                CreatedDate = Convert.ToDateTime(ratingModel.createdDate),
                CustomerId = Convert.ToInt32(ratingModel.customerId),
                Display = Convert.ToBoolean(ratingModel.display),
                Id = ratingModel.id,
                RatingComments = ratingModel.ratingComments,
                Ratings1 = Convert.ToInt32(ratingModel.ratings)
            };

            return rating;
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

        internal Login Parse(LoginModel login)
        {
            if (login == null)
                return null;

            Login loginModel = new Login()
            {
                Id = login.id,
                CreatedDate = Convert.ToDateTime(login.createdDate),
                UserId = login.userid,
                Password = login.password,
                Username = login.userName                
            };

            return loginModel;
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

        internal Membership Parse(MembershipModel membershipModel)
        {
            if (membershipModel == null)
                return null;

            Membership membership = new Membership()
            {
                Id = membershipModel.id,
                BasicSubscriptionFee = Convert.ToDecimal(membershipModel.basicSubscriptionFee),
                Commission = Convert.ToDouble(membershipModel.commission),
                Description = membershipModel.description,
                Name = membershipModel.name
            };

            return membership;
        }

        internal Invoices Parse(InvoiceModel invoice)
        {
            if (invoice == null)
                return null;

            return new Invoices()
            {
                Amount = Convert.ToDecimal(invoice.amount),
                PaypalBookingReference = invoice.paypalBookingReference,
                CustomerId = Convert.ToInt32(invoice.customerId),
                Id = Convert.ToInt32(invoice.id),
                InvoiceNumber = invoice.invoiceNumber,
                PaidDate = Convert.ToDateTime(invoice.paidDate),
                ServiceProviderId = Convert.ToInt32(invoice.serviceProviderId)
            };
        }

        #endregion
    }
}
