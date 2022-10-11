using Dictionary.Common;
using Dictionary.Common.Events;
using Dictionary.Common.Infrastructure;

namespace Dictionary.VoteService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        private readonly Services.VoteService _voteService;

        public Worker(ILogger<Worker> logger, IConfiguration configuration, Services.VoteService voteService)
        {
            _logger = logger;
            _configuration = configuration;
            _voteService = voteService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            QueueFactory.GetOrCreateBasicConsumer
                .EnsureExchange(DictionaryConstants.VoteExchangeName)
                .EnsureQueue(DictionaryConstants.CreateEntryVoteQueue, DictionaryConstants.VoteExchangeName)
                .Receive<CreateEntryVoteEvent>( @event =>
            {
                // db insert
                _voteService.CreateEntryVoteAsync(@event).GetAwaiter().GetResult();
                _logger.LogInformation($"Create entry vote received with EntryId:{@event.EntryId} and VoteType{@event.VoteType}");
            })
                .StartConsuming(DictionaryConstants.CreateEntryVoteQueue);
        }
    }
}