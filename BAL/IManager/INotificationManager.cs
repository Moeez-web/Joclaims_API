using MODEL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.IManager
{
    public interface INotificationManager
    {
        string NotificationSent(int notificationID);
        CommonMeta ViewAllNotification(int UserID, int PageNo);
        string MarkAllRead(int UserID);
        string MarkNotificationRead(int NotificationID);
        List<Notification> GetSingleNotification();
        string MarkNotificationsSent(List<Notification> Notifications);
        CommonMeta ViewAllNotificationTN(int UserID, int PageNo);
    }

}
