namespace Dictionary.Common;

public sealed class DictionaryConstants
{
    public const string RabbitMqHost = "localhost";

    //public const int RabbitMqPort = 1453;

    public const string DefaultExchangeType = "direct";



    // Exchanges

    public const string UserExchangeName = "UserExchange";

    public const string FavoriteExchangeName = "FavoriteExchange";

    public const string VoteExchangeName = "VoteExchange";


    // Queues
    public const string UserEmailChangedQueue = "UserEmailChangedQueue";

    public const string CreateEntryCommentFavoriteQueue = "CreateEntryCommentFavoriteQueue";

    public const string CreateEntryFavoriteQueue = "CreateEntryFavoriteQueue";

    public const string CreateEntryVoteQueue = "CreateEntryVoteQueue";

    public const string DeleteEntryFavoriteQueue = "DeleteEntryFavoriteQueue";

    public const string DeleteEntryVoteQueue = "DeleteEntryVoteQueue";

    public const string CreateEntryCommentVoteQueue = "CreateEntryCommentVoteQueue";

    public const string DeleteEntryCommentVoteQueue = "DeleteEntryCommentVoteQueue";

    public const string DeleteEntryCommentFavoriteQueue = "DeleteEntryCommentFavoriteQueue";
}