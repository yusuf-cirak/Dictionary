using Dictionary.Common;
using Dictionary.Common.Events;
using Dictionary.Common.Infrastructure;
using Dictionary.FavoriteService.Services;

namespace Dictionary.FavoriteService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly FavoriteService.Services.FavoriteService _favoriteService;

        public Worker(ILogger<Worker> logger, Services.FavoriteService favoriteService)
        {
            _logger = logger;
            _favoriteService = favoriteService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            QueueFactory.GetOrCreateBasicConsumer
            .EnsureExchange(DictionaryConstants.FavoriteExchangeName)
            .EnsureQueue(DictionaryConstants.CreateEntryFavoriteQueue, DictionaryConstants.FavoriteExchangeName)
            .Receive<CreateEntryFavoriteEvent>( fav =>
            {
                // db insert
                _favoriteService.CreateEntryFav(fav).GetAwaiter();
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
                    _favoriteService.DeleteEntryFav(fav).GetAwaiter().GetResult();
                    // _logger.LogInformation($"Received EntryId {fav.EntryId}");
                    Console.WriteLine($"Time: {DateTime.Now} | Received EntryId with delete action {fav.EntryId}");
                })
                .StartConsuming(DictionaryConstants.DeleteEntryFavoriteQueue);
        }
    }
}