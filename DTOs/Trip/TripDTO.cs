using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_webapi_ef.DTOs.Destination;


namespace dotnet_webapi_ef.DTOs.Trip
{
    // DTO => โครงสร้างข้อมูลที่จะใช้ตอนรับเข้ากับส่งออกไปจาก WebAPI
    public class TripDTO
    {
    public int Idx { get; set; }
    public string Name { get; set; } = null!;
    public string Country { get; set; } = null!;
    public int Destinationid { get; set; }
    public string Coverimage { get; set; } = null!;
    public string Detail { get; set; } = null!;
    public int Price { get; set; }
    public int Duration { get; set; }
    // public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    public virtual DestinationDTO Destination { get; set; } = null!;
    }
}