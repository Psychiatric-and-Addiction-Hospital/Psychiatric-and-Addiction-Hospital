using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum NotificationType
    {
        SessionReminder = 0,
        SessionCancelled = 1,
        NewMessage = 2,
        SessionCompleted = 3,
        SystemAnnouncement = 4,
        FeedbackRequest = 5,
        ResourceShared = 6,
        ApplicationStatus = 7,
        ProgressUpdate = 8
    }
}
