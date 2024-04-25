using LDP.Common.DAL.DataContexts;
using LDP.Common.DAL.Entities.Common;
using LDP.Common.DAL.Interfaces;
using LDP.Common.Requests;
using Microsoft.EntityFrameworkCore;

namespace LDP.Common.DAL.Repositories
{
    public class NotificationDetailsRepository : INotificationDetailsRepository
    {
        private readonly CommonDataContext? _context;
        public NotificationDetailsRepository(CommonDataContext? context)
        {
            _context = context;
        }
        public async Task<int> AddNotificationDetails(NotificaionDetail request)
        {
            _context.vm_notification_Details.Add(request);

            return  await _context.SaveChangesAsync();
           
        }

        public async Task<List<NotificaionDetail>> GetNotificationDetails(GetNoficationDetailRequest request)
        {
           return await  _context.vm_notification_Details.Where(notif => notif.message_id == request.MessageId)
                .AsNoTracking().ToListAsync();
        }

        public async Task<int> UpdateNotificationDetails(NotificaionDetail request)
        {
            _context.vm_notification_Details.Add(request);

            return await _context.SaveChangesAsync();
        }
    }
}
