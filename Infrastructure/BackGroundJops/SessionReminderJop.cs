using Application.Common.Interfaces;
using Domain.Enums;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class SessionReminderJob : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public SessionReminderJob(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AddIdentityDbContext>();
                var notifyService = scope.ServiceProvider.GetRequiredService<INotificationService>();

                // جلب الجلسات التي ستبدأ خلال الـ 24 ساعة القادمة ولم يتم إرسال تذكير لها
                var tomorrow = DateTime.UtcNow.AddDays(1);
                var sessions = await context.Sessions
                    .Where(s => s.ScheduledDate.Date == tomorrow.Date && s.Status == SessionStatus.Scheduled)
                    .ToListAsync();

                foreach (var session in sessions)
                {
                    await notifyService.SendNotificationAsync(
                        session.PatientId,
                        "تذكير بموعد الجلسة",
                        $"نذكرك بموعد جلستك غداً في تمام الساعة {session.ScheduledDate:HH:mm}.",
                        NotificationType.SessionReminder,
                        session.Id
                    );
                }
            }

            await Task.Delay(TimeSpan.FromHours(12), stoppingToken); // يفحص مرتين في اليوم
        }
    }
}