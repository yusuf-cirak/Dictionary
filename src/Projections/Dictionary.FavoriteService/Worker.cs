using Dictionary.Common;
using Dictionary.Common.Events;
using Dictionary.Common.Infrastructure;
using Dictionary.FavoriteService.Services;

namespace Dictionary.FavoriteService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var connStr = _configuration.GetConnectionString("DictionarySqlServer");

            var favService = new Services.FavoriteService(connStr);

            QueueFactory.GetOrCreateBasicConsumer
            .EnsureExchange(DictionaryConstants.FavoriteExchangeName)
            .EnsureQueue(DictionaryConstants.CreateEntryFavoriteQueue, DictionaryConstants.FavoriteExchangeName)
            .Receive<CreateEntryFavoriteEvent>( fav =>
            {
                // db insert
                favService.CreateEntryFav(fav).GetAwaiter();
                // _logger.LogInformation($"Received EntryId {fav.EntryId}");
                Console.WriteLine($"Time: {DateTime.Now} | {nameof(CreateEntryFavoriteEvent)} has received with EntryId : {fav.EntryId}");
            })
            .StartConsuming(DictionaryConstants.CreateEntryFavoriteQueue);
            
            
            QueueFactory.GetOrCreateBasicConsumer
                .EnsureExchange(DictionaryConstants.FavoriteExchangeName)
                .EnsureQueue(DictionaryConstants.DeleteEntryFavoriteQueue, DictionaryConstants.FavoriteExchangeName)
                .Receive<DeleteEntryFavoriteEvent>( fav =>
                {
                    // db insert
                    favService.DeleteEntryFav(fav).GetAwaiter().GetResult();
                    // _logger.LogInformation($"Received EntryId {fav.EntryId}");
                    Console.WriteLine($"Time: {DateTime.Now} | Received EntryId with delete action {fav.EntryId}");
                })
                .StartConsuming(DictionaryConstants.DeleteEntryFavoriteQueue);
        }
    }
}