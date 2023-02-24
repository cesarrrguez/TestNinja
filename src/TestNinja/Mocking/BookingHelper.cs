using System;
using System.Collections.Generic;
using System.Linq;

namespace TestNinja.Mocking
{
    public static class BookingHelper
    {
        public static string OverlappingBookingsExist(Booking booking, IBookingRepository repository)
        {
            if (booking.Status is "Cancelled")
            {
                return string.Empty;
            }

            var bookings = repository.GetActiveBookings(booking.Id);
            var overlappingBooking =
                bookings.FirstOrDefault(
                    x =>
                        booking.ArrivalDate < x.DepartureDate &&
                        x.ArrivalDate < booking.DepartureDate);

            return overlappingBooking is null
                ? string.Empty
                : overlappingBooking.Reference;
        }
    }

    public interface IUnitOfWork
    {
        IQueryable<T> Query<T>();
    }

    public class UnitOfWork : IUnitOfWork
    {
        public IQueryable<T> Query<T>()
        {
            return new List<T>().AsQueryable();
        }
    }

    public class Booking
    {
        public string Status { get; set; }
        public int Id { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public string Reference { get; set; }
    }
}