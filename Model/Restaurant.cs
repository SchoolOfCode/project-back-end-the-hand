using System;
using System.ComponentModel.DataAnnotations;

    public class Restaurant
    {
        public int Id { get; set; }

        [Required]
        public string RestaurantName { get; set; }
        
        [Required]
        public string Description {get; set; }
        
        [Required]
        public string OpeningTimes { get; set; }

        [Required]
        public string ClosingTimes { get; set; }

        [Required]
        public string PhoneNumber { get; set; }        // !!!!!! Need to check what the number format should be and how validation would work

        [Required]
        public string AddressLine1 { get; set; }

        [Required]
        public string Area { get; set; }

        [Required]
        public string Postcode { get; set; }

        [Required]
        public string WebsiteURL { get; set; }

        [Required]
        public string PhotoURL { get; set; }

        [Required]
        public string AdditionalInfo { get; set; }

        [Required]
        public string Cuisine { get; set; }

        [Required]
        public int Capacity { get; set; }

        [Required]
        public string RestaurantToken { get; set; }

    }
