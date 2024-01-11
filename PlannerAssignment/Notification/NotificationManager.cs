using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerAssignment.Notification
{
    public class NotificationManager
    {
        public INotificationType NotificationType { get; set; } = new PushNotification();

        public async void SendNotification(string text, string description)
        {
            NotificationType.SendNotification(text, description);
        }

        public bool IsPermissionGranted()
        {
            return NotificationType.IsPermissionGranted();
        }
    }
}
