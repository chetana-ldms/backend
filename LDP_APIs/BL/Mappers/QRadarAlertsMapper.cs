using AutoMapper;
using LDP.Common;
using LDP_APIs.DAL.Entities;
using LDP_APIs.Models;
using System.Text.Json;

namespace LDP_APIs.BL.Mappers
{
    public class QRadarAlertsMapper:Profile
    {
        public QRadarAlertsMapper()
        {
            CreateMap<QRadaroffense, Alerts>()
                .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.description))
                .ForMember(dest => dest.org_tool_severity, opt => opt.MapFrom(src => src.severity))
                .ForMember(dest => dest.alert_Device_PKID, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.detected_time, opt => opt.MapFrom(src => ConvertFromUnixTimeMillisecondsToDat(src.start_time)))
                .ForMember(dest => dest.source, opt => opt.MapFrom(src => Constants.Source_QRadar))
                .ForMember(dest => dest.status, opt => opt.MapFrom(src => Constants.AlertNewStatus))
                .ForMember(dest => dest.alert_data, opt => opt.MapFrom(src => convertToJson(src)))
                .ForMember(dest => dest.Created_Date, opt => opt.MapFrom(src => DateTime.Now.ToUniversalTime()))
                .ForMember(dest => dest.Created_User, opt => opt.MapFrom(src => Constants.User_Background_User)).ReverseMap();
           
        }

        string convertToJson(QRadaroffense obj)
        {
            return JsonSerializer.Serialize(obj).ToString();
        }

        private DateTime ConvertFromUnixTimeMillisecondsToDat(double startTime)
        {
            DateTimeOffset dateTimeOffset2 = DateTimeOffset.FromUnixTimeMilliseconds((long)startTime);
            return dateTimeOffset2.UtcDateTime;
  
        }
    }
}
