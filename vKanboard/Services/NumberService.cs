
using Microsoft.AspNetCore.SignalR;
using vKanboard.Hubs;

namespace vKanboard.Services
{
    public class NumberService : BackgroundService
    {
        private readonly IHubContext<NumberHub> _hubContext;

        public NumberService(IHubContext<NumberHub> hubContext)
        {
            _hubContext = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Random rnd = new Random();
                int number = rnd.Next(0, 100);

                Console.Write($"Number : {number}\r");
                await _hubContext.Clients.All.SendAsync("ReceiveRandomNumber", number);

                await Task.Delay(500);
            }
        }
    }
}
