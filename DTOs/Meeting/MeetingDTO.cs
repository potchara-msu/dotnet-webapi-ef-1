using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_webapi_ef.DTOs.Meeting
{
    public class MeetingDTO
    {
    public int Idx { get; set; }
    public string Detail { get; set; } = null!;
    public string Meetingdatetime { get; set; } = null!;
    public double Latitude { get; set; }
    public double Longitude { get; set; } 
    }
}