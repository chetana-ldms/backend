using FluentValidation;
using FluentValidation.Results;
using LDP.Common.Requests;
using LDP.Common.Responses;
using LDP_APIs.BL.APIRequests;
using LDP_APIs.BL.APIResponse;
using LDP_APIs.BL.Interfaces;
using LDP_APIs.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static LDP.Common.Requests.DeleteToolActionRequest;

namespace LDP_APIs.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ControllerName("LDP Configuration")]
    public class LDPlattformController : ControllerBase
    {
        ILDPlattformBL _bl;

        private IValidator<LDPToolRequest> _lDPToolValidator;
        private IValidator<UpdateLDPToolRequest> _updateLDPToolValidator;
        private IValidator<DeleteLDPToolRequest> _deleteLDPToolValidator;

        private IValidator<AddOrganizationRequest> _addOrganizationValidator;
        private IValidator<UpdateOrganizationRequest> _updateOrganizationValidator;
        private IValidator<DeleteOrganizationRequest> _deleteOrganizationValidator;

        private IValidator<AddOrganizationToolsRequest> _addOrganizationToolValidator;
        private IValidator<UpdateOrganizationToolsRequest> _updateOrganizationToolValidator;
        private IValidator<DeleteOrganizationToolsRequest> _deleteOrganizationToolValidator;

        private IValidator<AddToolTypeActionRequest> _addToolTypeActionnValidator;
        private IValidator<UpdateToolTypeActionRequest> _updateToolTypeActionnValidator;
        private IValidator<DeleteToolTypeActionRequest> _deleteToolTypeActionnValidator;

        private IValidator<AddToolActionRequest> _addToolActionValidator;
        private IValidator<UpdateToolActionRequest> _updateToolActionValidator;
        private IValidator<DeleteToolActionRequest> _deleteToolActionValidator;
        public LDPlattformController(ILDPlattformBL bl
            , IValidator<LDPToolRequest> lDPToolValidator
            ,IValidator<UpdateLDPToolRequest> updateLDPToolValidator
            , IValidator<DeleteLDPToolRequest> deleteLDPToolValidator

            , IValidator<AddOrganizationRequest> addOrganizationValidator
            ,IValidator<UpdateOrganizationRequest> updateOrganizationValidator
            , IValidator<DeleteOrganizationRequest> deleteOrganizationValidator

            , IValidator<AddOrganizationToolsRequest> addOrganizationToolValidator
            ,IValidator<UpdateOrganizationToolsRequest> updateOrganizationToolValidator
            , IValidator<DeleteOrganizationToolsRequest> deleteOrganizationToolValidator

            , IValidator<AddToolTypeActionRequest> addToolTypeActionnValidator
            ,IValidator<UpdateToolTypeActionRequest> updateToolTypeActionnValidator
            , IValidator<DeleteToolTypeActionRequest> deleteToolTypeActionnValidator

            , IValidator<AddToolActionRequest> addToolActionValidator
            ,IValidator<UpdateToolActionRequest> updateToolActionValidator
            , IValidator<DeleteToolActionRequest> deleteToolActionValidator
            )
        {
            _bl = bl;

            _lDPToolValidator = lDPToolValidator;
            _updateLDPToolValidator = updateLDPToolValidator ;
            _deleteLDPToolValidator = deleteLDPToolValidator;

            _addOrganizationValidator = addOrganizationValidator;
            _updateOrganizationValidator = updateOrganizationValidator;
            _deleteOrganizationValidator = deleteOrganizationValidator;

            _addOrganizationToolValidator = addOrganizationToolValidator;
            _updateOrganizationToolValidator = updateOrganizationToolValidator;
            _deleteOrganizationToolValidator = deleteOrganizationToolValidator;

            _addToolTypeActionnValidator = addToolTypeActionnValidator;
            _updateToolTypeActionnValidator = updateToolTypeActionnValidator;
            _deleteToolTypeActionnValidator = deleteToolTypeActionnValidator;

            _addToolActionValidator = addToolActionValidator;
            _updateToolActionValidator = updateToolActionValidator;
            _deleteToolActionValidator = deleteToolActionValidator;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("LDPTools")]
        public GetConfiguredLDPToolsResponse GetConfiguredSIEMTools()
        {
            return _bl.GetConfiguredLDPTools();
        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("LDPToolsByToolType")]
        public GetConfiguredLDPToolsResponse GetConfiguredSIEMTools(GetLDPToolRequest request)
        {
            return _bl.GetConfiguredLDPTools(request);
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("LDPToolDetails")]
        public GetConfiguredLDPToolResponse GetLDPToolDetails(int id)
        {
            return _bl.GetLDPToolByID(id);
        }


        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("LDPTools/Add")]
        public LDPToolResponse AddLDPTool(LDPToolRequest request)
        {
            baseResponse response = new LDPToolResponse();

            var result = _lDPToolValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as LDPToolResponse;

            }
            return _bl.NewLDPTool(request);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("LDPTools/Update")]
        public LDPToolResponse UpdateLDPTool(UpdateLDPToolRequest request)
        {
            baseResponse response = new LDPToolResponse();

            var result = _updateLDPToolValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as LDPToolResponse;

            }
            return _bl.UpdateLDPTool(request);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("LDPTools/Delete")]
        public DeleteLDPToolResponse DeleteLDPTool(DeleteLDPToolRequest request)
        {
            baseResponse response = new DeleteLDPToolResponse();

            var result = _deleteLDPToolValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as DeleteLDPToolResponse;

            }
            return _bl.DeleteLDPTool(request);
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("Organizations")]
        public GetOrganizationsResponse GetOrganizationList()
        {

            return _bl.GetOrganizationList();
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("OrganizationDetails")]
        public GetOrganizationResponse GetOrganizationDetails(int id)
        {

            return _bl.GetOrganizationByID(id);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Organization/Add")]
        public OrganizationResponse AddOrganization(AddOrganizationRequest request)
        {

            baseResponse response = new OrganizationResponse();

            var result = _addOrganizationValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as OrganizationResponse;

            }

            return _bl.AddOrganization(request);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Organization/Update")]
        public OrganizationResponse UpdateOrganization(UpdateOrganizationRequest request)
        {
            baseResponse response = new OrganizationResponse();

            var result = _updateOrganizationValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as OrganizationResponse;

            }
            return _bl.UpdateOrganization(request);
        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Organization/Delete")]
        public DeleteOrganizationsResponse DeleteOrganization(DeleteOrganizationRequest request)
        {
            baseResponse response = new DeleteOrganizationsResponse();

            var result = _deleteOrganizationValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as DeleteOrganizationsResponse;

            }
            return _bl.DeleteOrganization(request);
        }
        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("OrganizationTools")]
        public GetOrganizationToolsResponse GetOrganizationToolsList()
        {

            return _bl.GetOrganizationToolsList();

        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("OrganizationToolDetails")]
        public GetOrganizationToolResponse GetOrganizationToolDetails(int id)
        {
            return _bl.GetOrganizationToolByID(id);

        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("OrganizationTools/Add")]
        public OrganizationToolsResponse AddOrganizationTool(AddOrganizationToolsRequest request)
        {
            baseResponse response = new OrganizationToolsResponse();

            var result = _addOrganizationToolValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as OrganizationToolsResponse;

            }
            return _bl.AddOrganizationTool(request);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("OrganizationTools/Update")]
        public OrganizationToolsResponse UpdateOrganizationTool(UpdateOrganizationToolsRequest request)
        {
            baseResponse response = new OrganizationToolsResponse();

            var result = _updateOrganizationToolValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as OrganizationToolsResponse;

            }
            var res = _bl.UpdateOrganizationTool(request);

            return res;
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("OrganizationTools/Delete")]
        public OrganizationToolsResponse DeleteOrganizationTool(DeleteOrganizationToolsRequest request)
        {
            baseResponse response = new OrganizationToolsResponse();

            var result = _deleteOrganizationToolValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as OrganizationToolsResponse;

            }
            var res = _bl.DeleteOrganizationTool(request);

            return res;
        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("MasterData")]
        public LDPMasterDataResponse GetMasterData(LDPMasterDataRequest request)
        {
            return _bl.GetMasterDataByDatType(request);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("MasterDataByMultipleTypes")]
        public LDPMasterDataResponse GetMasterDataByMultipleTypes(LDPMasterDataByMultipleTypesRequest request)
        {
            return _bl.GetMasterDataByMultipleTypes(request);
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("ToolTypeActions")]
        public GetToolTypeActionResponse GetToolTypeActions()
        {
            return _bl.GetToolTypeActions();
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("GetToolTypeActionsByToolType")]
        public GetToolTypeActionResponse GetToolTypeActionsByToolType(GetToolTypeActinByToolTypeRequest request)
        {
            return _bl.GetToolTypeActionsByToolType(request);
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("ToolTypeActionDetails")]
        public GetToolTypeActionSingleResponse GetToolTypeActionDetails(int id)
        {
            return _bl.GetTooltypeActionByToolActionID(id);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("ToolTypeAction/Add")]
        public ToolTypeActionResponse AddToolTypeAction(AddToolTypeActionRequest request)
        {
            baseResponse response = new ToolTypeActionResponse();

            var result = _addToolTypeActionnValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as ToolTypeActionResponse;

            }
            return _bl.AddToolTypeAction(request);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("ToolTypeAction/Update")]
        public ToolTypeActionResponse UpdateToolTypeAction(UpdateToolTypeActionRequest request)
        {
            baseResponse response = new ToolTypeActionResponse();

            var result = _updateToolTypeActionnValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as ToolTypeActionResponse;

            }
            return _bl.UpdateToolTypeAction(request);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("ToolTypeAction/Delete")]
        public DeleteToolTypeActionResponse DeleteToolTypeAction(DeleteToolTypeActionRequest request)
        {
            baseResponse response = new DeleteToolTypeActionResponse();

            var result = _deleteToolTypeActionnValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as DeleteToolTypeActionResponse;

            }
            return _bl.DeleteToolTypeAction(request);
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("ToolActions")]
        public GetToolActionResponse GetToolActions()
        {
            return _bl.GetToolActionList();
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("GetToolActionsByTool")]
        public GetToolActionResponse GetToolActionsByTool(GetActionRequest request)
        {
            return _bl.GetToolActionsByTool(request);
        }
        

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("ToolActionDetails")]
        public GetToolActionSingleResponse GetToolActionDetails(int id)
        {
            return _bl.GetToolActionByID(id);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("ToolAction/Add")]
        public ToolActionResponse AddToolAction(AddToolActionRequest request)
        {
            baseResponse response = new ToolActionResponse();

            var result = _addToolActionValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as ToolActionResponse;

            }
            return _bl.AddToolAction(request);

        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("ToolAction/Update")]
        public ToolActionResponse UpdateToolAction(UpdateToolActionRequest request)
        {
            baseResponse response = new ToolActionResponse();

            var result = _updateToolActionValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as ToolActionResponse;

            }
            return _bl.UpdateToolAction(request);
        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("ToolAction/Delete")]
        public DeleteToolActionResponse DeleteToolAction(DeleteToolActionRequest request)
        {
            baseResponse response = new DeleteToolActionResponse();

            var result = _deleteToolActionValidator.Validate(request);

            if (!result.IsValid)
            {
                BuildValiationMessage(result, ref response);
                return response as DeleteToolActionResponse;

            }
            return _bl.DeleteToolAction(request);
        }
        private void BuildValiationMessage(ValidationResult result, ref baseResponse validationresponse)
        {
            validationresponse.IsSuccess = false;
            validationresponse.Message = "Validation Error";
            validationresponse.HttpStatusCode = HttpStatusCode.BadRequest;
            validationresponse.errors = result.Errors.Select(e => e.ErrorMessage.ToString()).ToList();

        }
    }
}
