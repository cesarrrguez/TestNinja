using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class BookingHelperTests
    {
        private Booking _booking;
        private Mock<IBookingRepository> _bookingRepositoryMock;

        [SetUp]
        public void SetUp()
        {
            _booking = new Booking
            {
                Id = 2,
                ArrivalDate = ArriveOn(2017, 1, 15),
                DepartureDate = DepartOn(2017, 1, 20),
                Reference = "a"
            };

            _bookingRepositoryMock = new Mock<IBookingRepository>();
            _bookingRepositoryMock.Setup(x => x.GetActiveBookings(1)).Returns(new List<Booking>
            {
                _booking

            }.AsQueryable());
        }

        [Test]
        public void OverlappingBookingsExist_WhenBookingStartsAndFinishesBeforeAnExistingBooking_ThenReturnEmptyString()
        {
            // Arrange + Act
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_booking.ArrivalDate, days: 2),
                DepartureDate = Before(_booking.ArrivalDate)
            }, _bookingRepositoryMock.Object);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void OverlappingBookingsExist_WhenBookingStartsBeforeAndFinishesInTheMiddleOfAnExistingBooking_ThenReturnExistingBookingsReference()
        {
            // Arrange + Act
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_booking.ArrivalDate),
                DepartureDate = After(_booking.ArrivalDate)
            }, _bookingRepositoryMock.Object);

            // Assert
            Assert.That(result, Is.EqualTo(_booking.Reference));
        }

        [Test]
        public void OverlappingBookingsExist_WhenBookingStartsBeforeAndFinishesAfterAnExistingBooking_ThenReturnExistingBookingsReference()
        {
            // Arrange + Act
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_booking.ArrivalDate),
                DepartureDate = After(_booking.DepartureDate)
            }, _bookingRepositoryMock.Object);

            // Assert
            Assert.That(result, Is.EqualTo(_booking.Reference));
        }

        [Test]
        public void OverlappingBookingsExist_WhenBookingStartsAndFinishesInTheMiddleOfAnExistingBooking_ThenReturnExistingBookingsReference()
        {
            // Arrange + Act
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_booking.ArrivalDate),
                DepartureDate = Before(_booking.DepartureDate)
            }, _bookingRepositoryMock.Object);

            // Assert
            Assert.That(result, Is.EqualTo(_booking.Reference));
        }

        [Test]
        public void OverlappingBookingsExist_WhenBookingStartsInTheMiddleOfAnExistingBookingButFinishesAfter_ThenReturnExistingBookingsReference()
        {
            // Arrange + Act
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_booking.ArrivalDate),
                DepartureDate = After(_booking.DepartureDate)
            }, _bookingRepositoryMock.Object);

            // Assert
            Assert.That(result, Is.EqualTo(_booking.Reference));
        }

        [Test]
        public void OverlappingBookingsExist_WhenBookingStartsAndFinishesAfterAnExistingBooking_ThenReturnEmptyString()
        {
            // Arrange + Act
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_booking.DepartureDate),
                DepartureDate = After(_booking.DepartureDate, days: 2)
            }, _bookingRepositoryMock.Object);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void OverlappingBookingsExist_WhenBookingsOverlapButNewBookingIsCancelled_ThenReturnEmptyString()
        {
            // Arrange + Act
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_booking.ArrivalDate),
                DepartureDate = After(_booking.DepartureDate),
                Status = "Cancelled"
            }, _bookingRepositoryMock.Object);

            // Assert
            Assert.That(result, Is.Empty);
        }

        private DateTime Before(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(-days);
        }

        private DateTime After(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(days);
        }

        private DateTime ArriveOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 14, 0, 0);
        }

        private DateTime DepartOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 10, 0, 0);
        }
    }
}