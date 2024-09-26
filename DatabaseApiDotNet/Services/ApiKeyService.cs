using System;

public class ApiKeyService
{
    public string TheMovieDbBearerApiKey => Environment.GetEnvironmentVariable("THE_MOVIE_DB_BEARER_API_KEY");
    public string RawgApiKey => Environment.GetEnvironmentVariable("RAWG_API_KEY");
    public string DiscogsConsumerKey => Environment.GetEnvironmentVariable("DISCOGS_CONSUMER_KEY");
    public string DiscogsConsumerSecret => Environment.GetEnvironmentVariable("DISCOGS_CONSUMER_SECRET");

    public ApiKeyService()
    {
        //Console.WriteLine($"TMDB Key: {TheMovieDbBearerApiKey}");
        //Console.WriteLine($"RAWG Key: {RawgApiKey}");
        //Console.WriteLine($"Discogs Consumer Key: {DiscogsConsumerKey}");
        //Console.WriteLine($"Discogs Consumer Secret: {DiscogsConsumerSecret}");
    }
}