﻿using Microsoft.AspNetCore.Components.Server;
using System.Text.Json.Serialization;

namespace ITKT_PROJEKTAS.Entities
{
    public class Route
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int Length { get; set; }
        public Difficulity Difficulity { get; set; }
        public string Description { get; set; }
        public int PricePerPerson { get; set; }
        public int MaxPeople { get; set; }
        public Reservation? Reservation { get; set; }
    }
}
