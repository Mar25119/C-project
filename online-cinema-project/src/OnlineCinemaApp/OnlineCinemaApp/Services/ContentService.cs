using System.Collections.Generic;

public class ContentService
{
    private readonly ContentRepository _contentRepository;

    public ContentService(ContentRepository contentRepository)
    {
        _contentRepository = contentRepository;
    }

    public List<Movie> GetAllMovies()
    {
        return _contentRepository.GetAllMovies();
    }

    public List<TvSeries> GetAllSeries()
    {
        return _contentRepository.GetAllSeries();
    }

    public List<Genre> GetAllGenres()
    {
        return _contentRepository.GetAllGenres();
    }
    public List<Content> Search(string query)
    {
        return _contentRepository.Search(query);
    }

}