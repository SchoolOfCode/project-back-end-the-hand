using System;
using System.ComponentModel.DataAnnotations;

    public class Timeslot
    {
        [Required]
        public string TimeSlot { get; set; }

        [Required]
        public int CurrentSlotOccupancy { get; set; }

    }
