using AutoMapper;
using LDP.Common.DAL.Entities;
using LDP.Common.DAL.Interfaces;
using LDP.Common.Model;
using LDP.Common.Requests;
using LDP_APIs.DAL.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace LDP.Common.DAL.Repositories
{
    public class AlertPlayBookProcessRepository : IAlertPlayBookProcessRepository
    {
        private readonly AlertsDataContext? _context;
        IMapper _mapper;
        IAlertPlayBookProcessActionRepository _actionRepository;
        public AlertPlayBookProcessRepository(AlertsDataContext context, IMapper mapper
            , IAlertPlayBookProcessActionRepository actionRepository)
        {
            _context = context;
            _mapper = mapper;
            _actionRepository = actionRepository;
        }
        public async Task<string> AddAlertPlayBookProcess(AddAlertPlayBookProcessRequest request)
        {
            AlertPlayBookProcess alertPlayBookProcess = null;

            using (IDbContextTransaction? transaction = _context?.Database.BeginTransaction())
            {
                try
                {
                    foreach (var playbookprocess in request.AlertPlayBookProcessData)
                    {
                        var processdata = _mapper.Map<AddAlertPlayBookProcessModel, AlertPlayBookProcess>(playbookprocess);
  
                        _context.vm_alert_playbooks_process.Add(processdata);
                        await _context.SaveChangesAsync();
                        var processactiondata = _mapper.Map<List<AddAlertPlayBookProcessActionModel>, List<AlertPlayBookProcessAction>>(playbookprocess.AlertPlayBookProcessActions);
                        processactiondata.ForEach(actiondata =>
                        {   actiondata.alert_playbooks_process_id = processdata.alert_playbooks_process_id;
                            actiondata.Created_Date = processdata.Created_Date;
                            actiondata.Created_User = processdata.Created_User;
                            actiondata.tool_action_status = "pending";
                        });
                       
                        await _actionRepository.AddRangeAlertPlayBookProcessActions(processactiondata);
                    }

                    _context.SaveChangesAsync();
                    transaction.Commit();

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }

                return "success";
            }
        }

        public async Task<string> AddRangeAlertPlayBookProcess(List<AlertPlayBookProcess> request)
        {
            _context.vm_alert_playbooks_process.AddRange(request);
            await _context.SaveChangesAsync();
            return "success";
        }

        public async Task<List<AlertPlayBookProcess>> GetAlertPlayBookProcessByIDs(List<int> ProcessIDs)
        {
           var res = await _context.vm_alert_playbooks_process.Where(process =>
           ProcessIDs.Contains(process.alert_playbooks_process_id)).ToListAsync();
            return res;
        }

        public Task<List<AlertPlayBookProcess>> GetAlertPlayBookProcessByStatus(string status)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateStatus(string stsatus, string user, DateTime updateDate)
        {
            throw new NotImplementedException();
        }
    }
}
