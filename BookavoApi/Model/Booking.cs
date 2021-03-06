using System;
using System.ComponentModel.DataAnnotations;

    public class Booking
    {
        public int BookingId { get; set; }

        [Required]
        public int RestaurantId { get; set; }

        [Required]
        public string CustomerName { get; set; }
        
        [Required]
        public string BookingDate {get; set; }
        
        [Required]
        public string BookingTime { get; set; }

        [Required]
        public int NumberOfPeople { get; set; }

        [Required]
        public string CustomerMobile { get; set; }        
        
        [Required]
        public string CustomerEmail { get; set; }

    }
