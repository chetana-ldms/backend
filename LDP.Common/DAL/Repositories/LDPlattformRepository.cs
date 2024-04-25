using LDP.Common.DAL.Entities;
using LDP.Common.Model;
using LDP.Common.Requests;
using LDP_APIs.BL.APIRequests;
using LDP_APIs.BL.Models;
using LDP_APIs.DAL.DataContext;
using LDP_APIs.DAL.Entities;
using LDP_APIs.Models;
using Microsoft.EntityFrameworkCore;

namespace LDP_APIs.DAL.Respository
{
    public class LDPlattformRepository : ILDPlattformRepository
    {
        private readonly LDPlattformDataContext? _context;
        public LDPlattformRepository(LDPlattformDataContext context)
        {
            _context = context;
        }

        #region LDPTool

        public async Task<List<LDPTool>> GetConfiguredLDPTools()
        {
           
                var res = await _context.vm_ldp_tools.Where(tl => tl.active == 1).AsNoTracking().ToListAsync();
                return res;
        }

        public async Task<List<LDPTool>> GetConfiguredLDPTools(GetLDPToolRequest request)
        {

            var res = await _context.vm_ldp_tools.Where(tl => tl.active == 1 && tl.Tool_Type_ID == request.ToolTypeId).AsNoTracking().ToListAsync();
            return res;
        }
       

        public async Task<string> NewLDPTool(LDPTool request)
        {
            _context.vm_ldp_tools.Add(request);
            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to add the tool";

        }
        public async Task<string> UpdateLDPTool(LDPTool request)
        {
            var tool = await _context.vm_ldp_tools.Where(tool => tool.Tool_ID == request.Tool_ID
                            ).AsNoTracking().FirstOrDefaultAsync();
            if (tool == null)
            {
                return "Tool details not found for update the tool data";
            }
            request.Created_date = tool.Created_date;
            request.Created_user = tool.Created_user;
            request.active= tool.active;
            request.deleted_date= tool.deleted_date;
            request.deleted_user = tool.deleted_user;

            _context.vm_ldp_tools.Update(request);
            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to update the tool data";
        }

        public async Task<string> DeleteLDPTool(DeleteLDPToolRequest request, string deletedUserName)
        {
            var tool = await _context.vm_ldp_tools.Where(tool => tool.Tool_ID == request.ToolId
                            ).AsNoTracking().FirstOrDefaultAsync();
            if (tool == null)
            {
                return "tool details not found for delete the tool data";
            }

            tool.active = 0;
            tool.deleted_date = request.DeletedDate;
            tool.deleted_user = deletedUserName;
            tool.Modified_date = request.DeletedDate;
            tool.Modified_user = deletedUserName;

            _context.vm_ldp_tools.Update(tool);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to delete the tool data";

        }

        public async Task<LDPTool> GetLDPToolByName(string name)
        {
            var res = await _context.vm_ldp_tools.Where(tl => tl.Tool_Name.ToLower() == name.ToLower()).AsNoTracking().FirstOrDefaultAsync(); ;
            return res;
        }

        public async Task<LDPTool> GetLDPToolByNameOnUpdate(string name, int id)
        {
            var res = await _context.vm_ldp_tools.Where(tl => tl.Tool_Name.ToLower() == name.ToLower()
            && tl.Tool_ID != id ).AsNoTracking().FirstOrDefaultAsync(); ;
            return res;
        }
        public async Task<LDPTool> GetLDPToolByID(int tooid)
        {
            var res = await _context.vm_ldp_tools.Where(tl => tl.Tool_ID == tooid).AsNoTracking().FirstOrDefaultAsync();
            return res;
        }

       

        #endregion

        #region Organization

        public async Task<Organization> GetOrganizationByName(string name)
        {
            var res = await _context.vm_organizations.Where(or => or.Org_Name.ToLower() == name.ToLower()).AsNoTracking().FirstOrDefaultAsync(); ;
            return res;
        }

        public async Task<Organization> GetOrganizationByNameOnUpdate(string name, int id)
        {
            var res = await _context.vm_organizations.Where(or => or.Org_Name.ToLower() == name.ToLower()
            && or.Org_ID != id).AsNoTracking().FirstOrDefaultAsync(); ;
            return res;
        }
        public async Task<List<Organization>> GetOrganizationList()
        {
            return await _context.vm_organizations.Where(or => or.active == 1 ).AsNoTracking().ToListAsync();
        }

        public async Task<Organization> GetOrganizationByID(int id)
        {
            var res = await _context.vm_organizations.Where(tl => tl.Org_ID == id).AsNoTracking().FirstOrDefaultAsync();
            return res;
        }
        public async Task<string> AddOrganization(Organization request)
        {
            
            _context.vm_organizations.Add(request);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to add the organization";
        }

        public async Task<string> UpdateOrganization(Organization request)
        {
            var tool = await _context.vm_organizations.Where(or => or.Org_ID == request.Org_ID
                            ).AsNoTracking().FirstOrDefaultAsync();
            if (tool == null)
            {
                return "Organization details not found for update the organization data";
            }
            request.Created_Date = tool.Created_Date;
            request.Created_User = tool.Created_User;
            request.active = tool.active;
            request.deleted_date = tool.deleted_date;
            request.deleted_user = tool.deleted_user;

            _context.vm_organizations.Update(request);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "failed";
        }

        public async Task<string> DeleteOrganization(DeleteOrganizationRequest request, string deletedUserName)
        {
            var org = await _context.vm_organizations.Where(org => org.Org_ID == request.OrgID
                            ).AsNoTracking().FirstOrDefaultAsync();
            if (org == null)
            {
                return "Organization details not found for delete the organization data";
            }

            org.active = 0;
            org.deleted_date = request.DeletedDate;
            org.deleted_user = deletedUserName;
            org.Modified_Date = request.DeletedDate;
            org.Modified_User = deletedUserName;

            _context.vm_organizations.Update(org);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to delete the organization";

        }

        #endregion

        #region organizationTools

        public async Task<List<OrganizationToolModel>> GetOrganizationToolList()
        {

            var OrgToolsdata = await (from ot in _context.vm_organization_tools
                                      join o in _context.vm_organizations on ot.Org_ID equals o.Org_ID
                                      join t in _context.vm_ldp_tools on ot.Tool_ID equals t.Tool_ID
                                      where ot.active == 1
                                      select (new OrganizationToolModel
                                      {
                                            ApiUrl = ot.Api_Url,
                                            AuthKey = ot.Auth_Key,
                                            CreatedDate = ot.Created_Date,
                                            CreatedUser = ot.Created_User,
                                            DeletedDate = ot.deleted_date,
                                            DeletedUser = ot.deleted_user,
                                            LastReadPKID= ot.Last_Read_PKID,
                                            ModifiedDate = ot.Modified_Date,
                                            ModifiedUser = ot.Modified_User,
                                            OrgID = ot.Org_ID,
                                            OrgName = o.Org_Name,
                                            ToolID = ot.Tool_ID,
                                            OrgToolID= ot.Org_Tool_ID,
                                            ToolName = t.Tool_Name
                                            //ToolActions = GetOrganizationToolActions(ot.Org_Tool_ID).Result 
                                             
                                      }))
                                  .AsNoTracking()
                                  .ToListAsync();
            return OrgToolsdata;
        }
       
        public async Task<OrganizationToolModel> GetOrganizationToolByID(int id )
        {
            //var OrgTooldata = await (from ot in _context.vm_organization_tools
            //                          join o in _context.vm_organizations on ot.Org_ID equals o.Org_ID
            //                          join t in _context.vm_ldp_tools on ot.Tool_ID equals t.Tool_ID
            //                          where ot.active == 1
            //                          && ot.Org_Tool_ID == id 
            //                          select (new OrganizationToolModel
            //                          {
            //                              //ApiUrl = ot.Api_Url,
            //                              AuthKey = ot.Auth_Key,
            //                              CreatedDate = ot.Created_Date,
            //                              CreatedUser = ot.Created_User,
            //                              DeletedDate = ot.deleted_date,
            //                              DeletedUser = ot.deleted_user,
            //                              //LastReadPKID = ot.Last_Read_PKID,
            //                              ModifiedDate = ot.Modified_Date,
            //                              ModifiedUser = ot.Modified_User,
            //                              OrgID = ot.Org_ID,
            //                              OrgName = o.Org_Name,
            //                              ToolID = ot.Tool_ID,
            //                              OrgToolID = ot.Org_Tool_ID,
            //                              ToolName = t.Tool_Name,
            //                              //ApiVerson = ot.api_verson ,
            //                              ToolTypeId = t.Tool_Type_ID,
            //                              ToolTypeName = t.Tool_Type,
            //                              //GetDataBatchSize = ot.GetData_BatchSize,
            //                              //LastReadAlertDate = ot.Last_Read_AlertDate


            //                          }))
            //                      .AsNoTracking()
            //                      .FirstOrDefaultAsync();
            //return OrgTooldata;


            var OrgToolsdata = await (from ot in _context.vm_organization_tools
                                      join o in _context.vm_organizations on ot.Org_ID equals o.Org_ID
                                      join t in _context.vm_ldp_tools on ot.Tool_ID equals t.Tool_ID
                                      where ot.active == 1
                                       && ot.Org_Tool_ID == id
                                      select (new OrganizationToolModel
                                      {
                                          ApiUrl = ot.Api_Url,
                                          AuthKey = ot.Auth_Key,
                                          CreatedDate = ot.Created_Date,
                                          CreatedUser = ot.Created_User,
                                          DeletedDate = ot.deleted_date,
                                          DeletedUser = ot.deleted_user,
                                          LastReadPKID = ot.Last_Read_PKID,
                                          ModifiedDate = ot.Modified_Date,
                                          ModifiedUser = ot.Modified_User,
                                          OrgID = ot.Org_ID,
                                          OrgName = o.Org_Name,
                                          ToolID = ot.Tool_ID,
                                          OrgToolID = ot.Org_Tool_ID,
                                          ToolName = t.Tool_Name,
                                          ToolTypeId = t.Tool_Type_ID,
                                          ToolTypeName = t.Tool_Type
                                          //ToolActions = GetOrganizationToolActions(ot.Org_Tool_ID).Result 

                                      }))
                                  .AsNoTracking()
                                  .FirstOrDefaultAsync();
            return OrgToolsdata;

        }

        public async Task<List<OrganizationToolActionModel>> GetOrganizationToolActions(int orgtoolid)
        {

            var OrgTooldata = await (from ota  in _context.vm_organization_tool_actions
                                     join ta in _context.vm_ldp_tool_actions on ota.tool_action_id equals ta.Tool_Action_ID
                                     join tta in _context.vm_ldp_tool_type_actions on ta.Tool_Type_Action_ID equals tta.Tool_Type_Action_ID
                                     where ota.org_tool_id == orgtoolid
                                     && ota.active == 1 
                                     select (new OrganizationToolActionModel
                                     {
                                         ApiUrl = ota.api_url,
                                         AuthKey = ota.auth_key,
                                         //LastReadPKID = ota,
                                         // ModifiedDate = ot.Modified_Date,
                                         //ModifiedUser = ot.Modified_User,
                                         OrgId = ota.org_id,
                                         // OrgName = o.Org_Name,
                                         ToolId = ota.tool_id,
                                         OrgToolId = ota.org_tool_id,
                                         // ToolName = t.Tool_Name,
                                         ApiVerson = ota.api_verson,
                                         //ToolTypeId = t.Tool_Type_ID,
                                         //ToolTypeName = t.Tool_Type,
                                         GetDataBatchSize = ota.GetData_BatchSize,
                                         LastReadAlertDate = ota.Last_Read_AlertDate,
                                         ToolActionName = tta.Tool_Action,
                                           OrgToolActionId = ota.org_tool_action_id,
                                           ToolActionId = ota.tool_action_id,
                                            Active = ota.active
                                             //LastReadPKID = ota.las
                                       }))
                                     .AsNoTracking()
                                     .ToListAsync();
            return OrgTooldata;
        }

        public async Task<string> AddOrganizationToolAction(List<OrganizationToolAction> request)
        {
            _context.vm_organization_tool_actions.AddRangeAsync(request);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to add the organization tool action data";
        }

        public async Task<string> UpdateOrganizationToolActions(List<OrganizationToolAction> request)
        {
            List<OrganizationToolAction> addedlist = new List<OrganizationToolAction>();
            List<OrganizationToolAction> updatedlist = new List<OrganizationToolAction>();
            //List<OrganizationToolAction> deletedlist = new List<OrganizationToolAction>();

            //var toolactions = await _context.vm_organization_tool_actions.Where(or => or.org_tool_id == request[0].org_tool_id
            //             && or.active ==1 ).AsNoTracking().ToListAsync();
            //if (toolactions.Count == 0)
            //{
            //    // return "Organization tool details not found for update the organization tool data";
            //    addedlist.AddRange(request);
            //}
            //else
            //{

                var newlistfromRequest = request.Where(ota => ota.org_tool_action_id == 0);

                if (newlistfromRequest.Count() > 0 )
                {
                    addedlist.AddRange(newlistfromRequest);
                }

                var existingListfromRequest = request.Where(ota => ota.org_tool_action_id > 0);

                if (existingListfromRequest.Count() > 0)
                {
                    updatedlist.AddRange(existingListfromRequest);
                }

                var existingIds = existingListfromRequest.Select(ota => ota.org_tool_action_id).ToList();

                var dbtoolactions = await _context.vm_organization_tool_actions.Where(or => or.org_tool_id == request[0].org_tool_id
                         && or.active == 1).AsNoTracking().ToListAsync();
                if (dbtoolactions.Count > 0)
                {
                    var deletedItems = dbtoolactions.Except(dbtoolactions.Where(ota => existingIds.Contains(ota.org_tool_action_id))).ToList();

                    deletedItems.ForEach(ota => ota.active = 0);
                    updatedlist.AddRange(deletedItems);
                }

                if (addedlist.Count >0)
                {
                    _context.vm_organization_tool_actions.AddRange(addedlist);
                }

                if (updatedlist.Count > 0)
                {
                    _context.vm_organization_tool_actions.UpdateRange(updatedlist);
                }


            //}

            //
            // request.Created_Date = tool.Created_Date;
            //request.Created_User = tool.Created_User;

            //request.ForEach(ta =>
            //ta.active = getactiveStatus(ta.org_tool_action_id, toolactions)
            //) ;
            ////request.active = toolactions.active;
            ////request.deleted_date = tool.deleted_date;
            ////request.deleted_user = tool.deleted_user;

            //_context.vm_organization_tool_actions.UpdateRange(request);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to update the organization tool ";

        }
        private int getactiveStatus(int orgtoolactionid, List<OrganizationToolAction> toolactions)
        {
            var toolaction = toolactions.Where(ta => ta.org_tool_action_id == orgtoolactionid).FirstOrDefault();
            if (toolaction == null)
            {
                return 1;
            }
            else
                return toolaction.active;
        }
        public async Task<List<OrganizationToolModel>> GetOrganizationToolsByToolType(string toolType)
        {
            var OrgTooldata = await (from ot in _context.vm_organization_tools
                                     join o in _context.vm_organizations on ot.Org_ID equals o.Org_ID
                                     join t in _context.vm_ldp_tools on ot.Tool_ID equals t.Tool_ID
                                     where ot.active == 1
                                     && t.Tool_Type == toolType
                                     select (new OrganizationToolModel
                                     {
                                         //ApiUrl = ot.Api_Url,
                                         AuthKey = ot.Auth_Key,
                                         CreatedDate = ot.Created_Date,
                                         CreatedUser = ot.Created_User,
                                         DeletedDate = ot.deleted_date,
                                         DeletedUser = ot.deleted_user,
                                         //LastReadPKID = ot.Last_Read_PKID,
                                         ModifiedDate = ot.Modified_Date,
                                         ModifiedUser = ot.Modified_User,
                                         OrgID = ot.Org_ID,
                                         OrgName = o.Org_Name,
                                         ToolID = ot.Tool_ID,
                                         OrgToolID = ot.Org_Tool_ID,
                                         ToolName = t.Tool_Name,
                                         //ApiVerson = ot.api_verson,
                                         ToolTypeId = t.Tool_Type_ID,
                                         ToolTypeName = t.Tool_Type,
                                         //GetDataBatchSize = ot.GetData_BatchSize,
                                         //LastReadAlertDate = ot.Last_Read_AlertDate

                                     }))
                                  .AsNoTracking()
                                  .ToListAsync();
            return OrgTooldata;

        }

        public async Task<string> AddOrganizationTool(OganizationTool request)
        {
            _context.vm_organization_tools.Add(request);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to add the organization tool data";
        }
        
        public async Task<string> UpdateOrganizationTool(OganizationTool request)
        {
            var tool = await _context.vm_organization_tools.Where(or => or.Org_Tool_ID == request.Org_Tool_ID
                            ).AsNoTracking().FirstOrDefaultAsync();
            if (tool == null)
            {
                return "Organization tool details not found for update the organization tool data";
            }
            request.Created_Date = tool.Created_Date;
            request.Created_User = tool.Created_User;
            request.active = tool.active;
            request.deleted_date = tool.deleted_date;
            request.deleted_user = tool.deleted_user;
            _context.vm_organization_tools.Update(request);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to update the organization tool ";

        }

        public async Task<OganizationTool> GetToolConnectionDetails(int OrgID , int tooltypeID)
        {
         

            var Query = await (from ot in _context.vm_organization_tools
                               join t in _context.vm_ldp_tools on  ot.Tool_ID equals t.Tool_ID
                        where t.Tool_Type_ID == tooltypeID
                        && t.active == 1
                        && ot.active == 1
                        && ot.Org_ID == OrgID
                        select ot).AsNoTracking()
                        .FirstOrDefaultAsync();

            return Query;
        }

        public async Task<string> UpdateLastReadPKID(GetOffenseDTO request)
        {
           
            var alertObj =  await _context.vm_organization_tools
                .Where( tool => tool.Org_ID == request.clientRequest.OrgID  && tool.Tool_ID == request.ToolID)
                .FirstOrDefaultAsync();
            if (alertObj != null)
            {
                alertObj.Last_Read_PKID = request.alert_MaxPKID;

                _context.vm_organization_tools.Update(alertObj);
                await _context.SaveChangesAsync();
            }
            return "";

        }

        public async Task<string> UpdateLastReadAlertDate(OrganizationToolModel request)
        {
            var toolactionobj = await _context.vm_organization_tool_actions
                .Where(tool => tool.tool_action_id == request.ToolActions[0].OrgToolActionId )
                .FirstOrDefaultAsync();
            if (toolactionobj != null)
            {
                toolactionobj.Last_Read_AlertDate = request.ToolActions[0].LastReadAlertDate;

                _context.vm_organization_tool_actions.Update(toolactionobj);
                await _context.SaveChangesAsync();
            }
            return "";

        }

        public async Task<GetOffenseDTO> GetLastReadPKID(GetOffenseDTO request)
        {

            var alertObj = await _context.vm_organization_tools
                .Where(tool => tool.Org_ID == request.clientRequest.OrgID && tool.Tool_ID == request.ToolID)
                .FirstOrDefaultAsync();
            if (alertObj != null)
            {
                request.alert_MaxPKID = alertObj.Last_Read_PKID;
     
            }
            return request;

        }
        public async Task<string> DeleteOrganizationTool(DeleteOrganizationToolsRequest request, string deletedUserName)
        {
            var orgtool = await _context.vm_organization_tools.Where(orgtl => orgtl.Org_Tool_ID == request.OrgToolID
                         ).AsNoTracking().FirstOrDefaultAsync();
            if (orgtool == null)
            {
                return "Organization Tool details not found for delete the tool data";
            }

            orgtool.active = 0;
            orgtool.deleted_date = request.DeletedDate;
            orgtool.deleted_user = deletedUserName;
            orgtool.Modified_Date = request.DeletedDate;
            orgtool.Modified_User = deletedUserName;

            _context.vm_organization_tools.Update(orgtool);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to delete the organization tools data";

        }

        #endregion

        #region MasterData
        public async Task<List<LDPMasterData>> GetMasterData(string MasterDataType)
        {
      
                return await _context.vm_ldp_masterdata.Where(mdata => mdata.data_type == MasterDataType)
                     .AsNoTracking().ToListAsync();
                 
        }

        public async Task<List<LDPMasterData>> GetMasterDataByMultipleTypes(List<string> MasterDataTypes)
        {
            
                return await _context.vm_ldp_masterdata.Where(mdata => MasterDataTypes.Contains(mdata.data_type))
                     .AsNoTracking().ToListAsync();
        }

        public async Task<LDPMasterData> GetMasterDataByDataValue(string MasterDataType, string MasterDataValue)
        {
           
                return await _context.vm_ldp_masterdata.Where(mdata => mdata.data_type == MasterDataType
                 && mdata.data_value == MasterDataValue)
                    .AsNoTracking().FirstOrDefaultAsync();
          

        }

        public async Task<LDPMasterData> GetMasterDataValueByDataId(string MasterDataType, int MasterDataId)
        {

            var res = await _context.vm_ldp_masterdata.Where(mdata => mdata.data_type == MasterDataType
             && mdata.data_id == MasterDataId )
                .AsNoTracking().FirstOrDefaultAsync();
            return res;
        }

        public async Task<LDPMasterData> GetLDPMasterDataByID(int id)
        {
            var res = await _context.vm_ldp_masterdata.Where(tl => tl.data_id == id).AsNoTracking().FirstOrDefaultAsync();
            return res;
        }
        #endregion
        #region ToolTypeActions

        public async Task<ToolTypeAction> GetToolTypeActionByName(string name)
        {
            var res = await _context.vm_ldp_tool_type_actions.Where(tta => tta.Tool_Action.ToLower() == name.ToLower()).AsNoTracking().FirstOrDefaultAsync(); ;
         
            return res;
        }

        public async Task<ToolTypeAction> GetToolTypeActionByNameOnUpdate(string name, int id)
        {
            var res = await _context.vm_ldp_tool_type_actions.Where(tta => tta.Tool_Action.ToLower() == name.ToLower()
            && tta.Tool_Type_Action_ID != id ).AsNoTracking().FirstOrDefaultAsync(); ;
            return res;
        }
        public async Task<string> AddToolTypeAction(ToolTypeAction request)
        {
            _context.vm_ldp_tool_type_actions.Add(request);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to add the tool type action";
        }


        public async Task<string> UpdateToolTypeAction(ToolTypeAction request)
        {
            var tool = await _context.vm_ldp_tool_type_actions.Where(ta => ta.Tool_Type_Action_ID == request.Tool_Type_Action_ID
                            ).AsNoTracking().FirstOrDefaultAsync();
            if (tool == null)
            {
                return "Tool type action  details not found for update the tool type action data";
            }
            request.Created_date = tool.Created_date;
            request.Created_user = tool.Created_user;
            request.active = tool.active;
            request.deleted_date = tool.deleted_date;
            request.deleted_user = tool.deleted_user;

            _context.vm_ldp_tool_type_actions.Update(request);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to update the tool type action data";
        }
        public async Task<List<GetToolTypeActionModel>> GetToolTypeActions()
        {

            var TTactiondata = await (from tta in _context.vm_ldp_tool_type_actions
                                      join tt in _context.vm_ldp_masterdata on tta.Tool_Type_ID equals tt.data_id
                                      where tta.active == 1
                                      select (new GetToolTypeActionModel
                                      {
                                          CreatedDate = tta.Created_date,
                                          CreatedUser = tta.Created_user,
                                          DeletedDate = tta.deleted_date,
                                          DeletedUser = tta.deleted_user,
                                          ModifiedDate = tta.Modified_date,
                                          ModifiedUser = tta.Modified_user,
                                          active = tta.active,
                                          Processed = tta.Processed,
                                          ToolTypeActionID = tta.Tool_Type_Action_ID,
                                          ToolTypeName = tt.data_value,
                                          ToolAction = tta.Tool_Action,
                                          ToolTypeID = tta.Tool_Type_ID

                                      }))
                              .AsNoTracking()
                              .ToListAsync();
            return TTactiondata;
        }
        public async Task<List<GetToolTypeActionModel>> GetToolTypeActionsByToolType(GetToolTypeActinByToolTypeRequest request)
        {

            var TTactiondata = await (from tta in _context.vm_ldp_tool_type_actions
                                      join tt in _context.vm_ldp_masterdata on tta.Tool_Type_ID equals tt.data_id
                                      where tta.active == 1
                                      && tta.Tool_Type_ID == request.ToolTypeId
                                      select (new GetToolTypeActionModel
                                      {
                                          CreatedDate = tta.Created_date,
                                          CreatedUser = tta.Created_user,
                                          DeletedDate = tta.deleted_date,
                                          DeletedUser = tta.deleted_user,
                                          ModifiedDate = tta.Modified_date,
                                          ModifiedUser = tta.Modified_user,
                                          active = tta.active,
                                          Processed = tta.Processed,
                                          ToolTypeActionID = tta.Tool_Type_Action_ID,
                                          ToolTypeName = tt.data_value,
                                          ToolAction = tta.Tool_Action,
                                          ToolTypeID = tta.Tool_Type_ID

                                      }))
                              .AsNoTracking()
                              .ToListAsync();
            return TTactiondata;
        }
        public async Task<string> DeleteToolTypeAction(DeleteToolTypeActionRequest request,string deletedUserName)
        {
            var ttaction = await _context.vm_ldp_tool_type_actions.Where(orgtltype => orgtltype.Tool_Type_Action_ID == request.ToolTypeActionID
                          ).AsNoTracking().FirstOrDefaultAsync();
            if (ttaction == null)
            {
                return " Tool type action details not found for delete the tool action data";
            }

            ttaction.active = 0;
            ttaction.deleted_date = request.DeletedDate;
            ttaction.deleted_user = deletedUserName;
            ttaction.Modified_date = request.DeletedDate;
            ttaction.Modified_user = deletedUserName;

            _context.vm_ldp_tool_type_actions.Update(ttaction);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to delete the tool type action data";

        }
        public async Task<GetToolTypeActionModel> GetTooltypeActionByToolActionID(int id)
        {
            var TTactiondata = await (from tta in _context.vm_ldp_tool_type_actions
                                      join tt in _context.vm_ldp_masterdata on tta.Tool_Type_ID equals tt.data_id
                                      where tta.active == 1
                                      && tta.Tool_Type_Action_ID == id
                                      select (new GetToolTypeActionModel
                                      {
                                          CreatedDate = tta.Created_date,
                                          CreatedUser = tta.Created_user,
                                          DeletedDate = tta.deleted_date,
                                          DeletedUser = tta.deleted_user,
                                          ModifiedDate = tta.Modified_date,
                                          ModifiedUser = tta.Modified_user,
                                          active = tta.active,
                                          Processed = tta.Processed,
                                          ToolTypeActionID = tta.Tool_Type_Action_ID,
                                          ToolTypeName = tt.data_value,
                                          ToolAction = tta.Tool_Action,
                                          ToolTypeID = tta.Tool_Type_ID

                                      }))
                              .AsNoTracking()
                              .FirstOrDefaultAsync();
            return TTactiondata;
        }
        public async Task<ToolTypeAction> GetToolTypeActionByID(int id)
        {
            var res = await _context.vm_ldp_tool_type_actions.Where(tl => tl.Tool_Type_Action_ID == id).AsNoTracking().FirstOrDefaultAsync();
            return res;
        }
        #endregion

        #region Tool actions

        public async Task<List<GetToolActionModel>> GetToolActions()
        {
            var toolactions = await (from ta in _context.vm_ldp_tool_actions
                                     join t in _context.vm_ldp_tools on ta.Tool_ID equals t.Tool_ID
                                     join tta in _context.vm_ldp_tool_type_actions on ta.Tool_Type_Action_ID equals tta.Tool_Type_Action_ID
                                     where ta.active == 1
                                     select (new GetToolActionModel
                                     {
                                        CreatedDate = ta.Created_date,
                                        CreatedUser = ta.Created_user,
                                        DeletedDate = ta.deleted_date,
                                        DeletedUser = ta.deleted_user,
                                        ModifiedDate = ta.Modified_date,
                                        ModifiedUser = ta.Modified_user,
                                        active = ta.active,
                                        Processed = ta.Processed,
                                        ToolTypeActionID = ta.Tool_Type_Action_ID,
                                        ToolActionID = ta.Tool_Action_ID,
                                        ToolID = ta.Tool_ID,
                                        ToolName = t.Tool_Name,
                                        ToolTypeActionName = tta.Tool_Action
                                          
                                     }))
                              .AsNoTracking()
                              .ToListAsync();

            return toolactions;
        }

        public async Task<List<GetToolActionModel>> GetToolActionsByTool(GetActionRequest request)
        {
            var toolactions = await (from ta in _context.vm_ldp_tool_actions
                                     join t in _context.vm_ldp_tools on ta.Tool_ID equals t.Tool_ID
                                     join tta in _context.vm_ldp_tool_type_actions on ta.Tool_Type_Action_ID equals tta.Tool_Type_Action_ID
                                     where ta.active == 1
                                     && ta.Tool_ID == request.ToolId
                                     select (new GetToolActionModel
                                     {
                                         CreatedDate = ta.Created_date,
                                         CreatedUser = ta.Created_user,
                                         DeletedDate = ta.deleted_date,
                                         DeletedUser = ta.deleted_user,
                                         ModifiedDate = ta.Modified_date,
                                         ModifiedUser = ta.Modified_user,
                                         active = ta.active,
                                         Processed = ta.Processed,
                                         ToolTypeActionID = ta.Tool_Type_Action_ID,
                                         ToolActionID = ta.Tool_Action_ID,
                                         ToolID = ta.Tool_ID,
                                         ToolName = t.Tool_Name,
                                         ToolTypeActionName = tta.Tool_Action

                                     }))
                              .AsNoTracking()
                              .ToListAsync();

            return toolactions;
        }

        public async Task<GetToolActionModel> GetToolActionByID(int id)
        {
            var toolaction = await (from ta in _context.vm_ldp_tool_actions
                                    join t in _context.vm_ldp_tools on ta.Tool_ID equals t.Tool_ID
                                    join tta in _context.vm_ldp_tool_type_actions on ta.Tool_Type_Action_ID equals tta.Tool_Type_Action_ID
                                    where ta.active == 1
                                    && ta.Tool_Action_ID == id
                                    select (new GetToolActionModel
                                    {
                                        CreatedDate = ta.Created_date,
                                        CreatedUser = ta.Created_user,
                                        DeletedDate = ta.deleted_date,
                                        DeletedUser = ta.deleted_user,
                                        ModifiedDate = ta.Modified_date,
                                        ModifiedUser = ta.Modified_user,
                                        active = ta.active,
                                        Processed = ta.Processed,
                                        ToolTypeActionID = ta.Tool_Type_Action_ID,
                                        ToolActionID = ta.Tool_Action_ID,
                                        ToolID = ta.Tool_ID,
                                        ToolName = t.Tool_Name,
                                        ToolTypeActionName = tta.Tool_Action,
                                        ToolTypeId = t.Tool_Type_ID,
                                        ToolTypeName = t.Tool_Type

                                    }))
                            .AsNoTracking()
                            .FirstOrDefaultAsync();

                return toolaction;

            }

        public async Task<string> AddTooolAction(LDPToolActions request)
        {
            _context.vm_ldp_tool_actions.Add(request);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "failed";
        }

        public async Task<string> UpdateToolAction(LDPToolActions request)
        {
            var tool = await _context.vm_ldp_tool_actions.Where(ta => ta.Tool_Action_ID == request.Tool_Action_ID
                         ).AsNoTracking().FirstOrDefaultAsync();
            if (tool == null)
            {
                return "Tool  action  details not found for update the tool  action data";
            }
            request.Created_date = tool.Created_date;
            request.Created_user = tool.Created_user;
            request.active = tool.active;
            request.deleted_date = tool.deleted_date;
            request.deleted_user = tool.deleted_user;

            _context.vm_ldp_tool_actions.Update(request);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to update the tool  action data";
        }

        public async Task<string> DeleteToolAction(DeleteToolActionRequest request,string deletedUserName)
        {
            var ttaction = await _context.vm_ldp_tool_actions.Where(toolaction => toolaction.Tool_Action_ID == request.ToolActionId
                          ).AsNoTracking().FirstOrDefaultAsync();
            if (ttaction == null)
            {
                return " Tool action details not found for delete the tool action data";
            }

            ttaction.active = 0;
            ttaction.deleted_date = request.DeletedDate;
            ttaction.deleted_user = deletedUserName;
            ttaction.Modified_date = request.DeletedDate;
            ttaction.Modified_user = deletedUserName;

            _context.vm_ldp_tool_actions.Update(ttaction);

            var status = await _context.SaveChangesAsync();

            if (status > 0)
                return "";
            else
                return "Failed to delete the tool  action data";

        }







        
        

       



        #endregion
    }
}
