using Dictionary.Common;
using Dictionary.Common.Events;
using Dictionary.Common.Infrastructure;

namespace Dictionary.VoteService
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
            var voteService = new Services.VoteService(_configuration
                .GetConnectionString("DictionarySqlServer"));

            QueueFactory.GetOrCreateBasicConsumer
                .EnsureExchange(DictionaryConstants.VoteExchangeName)
                .EnsureQueue(DictionaryConstants.CreateEntryVoteQueue, DictionaryConstants.VoteExchangeName)
                .Receive<CreateEntryVoteEvent>( @event =>
            {
                // db insert
                voteService.CreateEntryVoteAsync(@event).GetAwaiter().GetResult();
                _logger.LogInformation($"Create entry vote received with EntryId:{@event.EntryId} and VoteType{@event.VoteType}");
            })
                .StartConsuming(DictionaryConstants.CreateEntryVoteQueue);
        }
    }
}