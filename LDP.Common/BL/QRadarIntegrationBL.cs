using AutoMapper;
using LDP.Common;
using LDP.Common.BL.Interfaces;
using LDP.Common.Model;
using LDP_APIs.BL.Interfaces;
using LDP_APIs.BL.Models;
using LDP_APIs.DAL.Entities;
using LDP_APIs.DAL.Interfaces;
using LDP_APIs.Interfaces;
using LDP_APIs.Models;

namespace LDP_APIs.BL
{
    public class QRadarIntegrationBL : IQRadarIntegrationBL
    {
        IQRadarIntegrationservice _service;
        IAlertsRepository _repo;
        public readonly IMapper _mapper;
        ILDPlattformBL _plattformBL;
        IAlertHistoryBL _alertHistoryBL;
        ILdpMasterDataBL _masterDataBL;
        IAlertExtnFieldBL _alertExtnFieldBL;
        public QRadarIntegrationBL(IQRadarIntegrationservice service, IAlertsRepository repo
            , IMapper mapper
             , ILDPlattformBL plattformBL
            , IAlertHistoryBL alertHistoryBL, ILdpMasterDataBL masterDataBL, IAlertExtnFieldBL alertExtnFieldBL)
        {
            _service = service;
            _repo = repo;
            _mapper = mapper;
            _plattformBL = plattformBL;
            _alertHistoryBL = alertHistoryBL;
            _masterDataBL = masterDataBL;
            _alertExtnFieldBL = alertExtnFieldBL;
        }
        public getOffenseResponse Getoffenses(GetOffenseRequest request)
        {
            getOffenseResponse methodResponse = new getOffenseResponse();
            GetOffenseDTO dto = new GetOffenseDTO();
            dto.clientRequest = request;
            dto.ToolID = request.ToolID;
            dto.clientRequest.OrgID = request.OrgID;
            var tooltypeID = _plattformBL.GetMasterDataByDataValue(Constants.Tool_Type, Constants.Tool_Type_SIEM);
            //
            var conndtl = _plattformBL.GetToolConnectionDetails(dto.clientRequest.OrgID, tooltypeID);
            if (conndtl==null)
            {
                methodResponse.IsSuccess = false;
                methodResponse.Message = "Tool connection details not found , please check the request or configuration";
                return methodResponse;
            }
            dto.APIUrl = conndtl.ApiUrl;
            dto.AuthKey = conndtl.AuthKey;
            //dto.alert_MaxPKID = conndtl.LastReadPKID;
            //dto.APIVersion = conndtl.ApiVerson;
            //dto.GetDataBatchSize = conndtl.GetDataBatchSize;
            dto.ToolID = conndtl.ToolID;
            var res = _service.Getoffenses(dto).Result;
            if (!res.IsSuccess)
            {
                return res;
            }
            var _mappedResponse = _mapper.Map<List<QRadaroffense>, List<Alerts>>(res.offensesList);
            MapPlatformSeverity(request, conndtl, _mappedResponse);
            var OrderedAlerts = _mappedResponse.OrderBy(a => a.alert_Device_PKID);
            //
            _repo.Addalerts(OrderedAlerts.ToList());

            var offenseIds = _mappedResponse.Select(m => m.alert_Device_PKID).ToList();
            //
            var alerts = _repo.GetAlertsByDevicePKIds(offenseIds, request.OrgID);

            // Adding Alerrt Rule data into AlertExtnFields Table 
            AddAlertRulesData(res, alerts);
            ////Adding History data 
            AddHistoryData(_mappedResponse);

            dto.alert_MaxPKID = _mappedResponse.Max(obj => obj.alert_Device_PKID);
            _plattformBL.UpdateLastReadPKID(dto);
            res.offensesList = null;
            //res.Result.offensesList = _mappedResponse;
            return res;

        }
        void AddAlertRulesData(getOffenseResponse res, Task<List<Alerts>> alerts)
        {
            List<AlertExtnFieldModel> alertExtnFields = new List<AlertExtnFieldModel>();
            AlertExtnFieldModel alertExtnField = null;
            int alertId = 0;
            foreach (var offense in res.offensesList)
            {
                if (alerts.Result != null)
                {
                    alertId = alerts.Result.Where(alert => alert.alert_Device_PKID == offense.id).FirstOrDefault().alert_id;
                }
                else
                {
                    alertId = 0;
                }
                foreach (var alertrule in offense.rules)
                {
                    alertExtnField = new AlertExtnFieldModel();
                    alertExtnField.AlertId = alertId;
                    alertExtnField.DataType = Constants.AlertAlertRuleType;
                    alertExtnField.DataFieldValue = alertrule.type;
                    alertExtnField.DataFieldValueType = Constants.DataType_String;
                    alertExtnField.DataId = alertrule.id;
                    alertExtnField.CreatedDate = DateTime.UtcNow;
                    alertExtnField.CreatedUser = Constants.User_Background_User;

                    alertExtnField.Active = 1;
                    alertExtnFields.Add(alertExtnField);
                }
            }

            _alertExtnFieldBL.AddRangeAlertExtnFields(alertExtnFields);
        }

        private void MapPlatformSeverity(GetOffenseRequest request, OrganizationToolModel conndtl, List<Alerts> _mappedResponse)
        {
            var clientSeverityMappingdata = _masterDataBL.GetOrgMasterData(Constants.AlertSevirityType, request.OrgID);
            var sevirityPlatformData = _plattformBL
                .GetMasterDataByDatType(new APIRequests.LDPMasterDataRequest()
                { MaserDataType = Constants.AlertSevirityType }).MasterData;
            //

            int _newStatusId = _plattformBL.GetMasterDataByDataValue(Constants.AlertStatusType, Constants.AlertNewStatus);

            _mappedResponse.ForEach(obj =>
            {
                obj.tool_id = conndtl.ToolID;
                obj.org_id = request.OrgID;
                obj.status_ID = _newStatusId;
                obj.severity_id = clientSeverityMappingdata.Data.Where
                (dt => dt.OrgDataValue == obj.org_tool_severity).FirstOrDefault().DataId;
                obj.severity_name = sevirityPlatformData.Where(sev => sev.DataID == obj.severity_id).FirstOrDefault().DataValue;
            });
        }

        private void AddHistoryData(List<Alerts> _mappedResponse)
        {
            AlertHistoryModel alertHistoryrequst = null;
            List<AlertHistoryModel> alertHistoryList = new List<AlertHistoryModel>();
            foreach (var alert in _mappedResponse)
            {
                alertHistoryrequst = new AlertHistoryModel()
                {
                    AlertId = alert.alert_id,
                    HistoryDate = alert.Created_Date,
                    CreatedUser = alert.Created_User,
                    //HistoryDescription = "Alert created :" + alert.alert_id
                    //                       + " Created by  : Background job",
                    HistoryDescription = $"Alert # :{alert.alert_id} created ",
                    OrgId = alert.org_id,
                    CreatedUserId = 0
                };
                alertHistoryList.Add(alertHistoryrequst);
            }
            _alertHistoryBL.AddRangealertHistory(alertHistoryList);
        }

        public Task<getOffenseResponse> GetoffensesWithData(GetOffenseRequest request)
        {
            GetOffenseDTO dto = new GetOffenseDTO();
            dto.clientRequest = request;
            
            dto.clientRequest.OrgID = request.OrgID;
            var tooltypeID = _plattformBL.GetMasterDataByDataValue(Constants.Tool_Type, Constants.Tool_Type_SIEM);
            var conndtl = _plattformBL.GetToolConnectionDetails(dto.clientRequest.OrgID, tooltypeID);
            dto.APIUrl = conndtl.ApiUrl;
            dto.AuthKey = conndtl.AuthKey;
            dto.alert_MaxPKID = conndtl.LastReadPKID;
            dto.ToolID = conndtl.ToolID;
            var res = _service.Getoffenses(dto);
            var _mappedResponse = _mapper.Map<IEnumerable<QRadaroffense>, List<Alerts>>(res.Result.offensesList);
            _mappedResponse.ForEach(obj => { obj.tool_id = conndtl.ToolID; obj.org_id = request.ToolID; });

            _repo.Addalerts(_mappedResponse);

            //Adding History data 
            AddHistoryData(_mappedResponse);

            dto.alert_MaxPKID = _mappedResponse.Max(obj => obj.alert_Device_PKID);
            _plattformBL.UpdateLastReadPKID(dto);
            //res.Result.offensesList = null;
            return res;
        }
    }
}
