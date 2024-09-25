namespace Tool.Map;

using Microsoft.Extensions.Logging;
using AutoMapper;

public interface IMap
{
    TOutput Map<TSource, TOutput>(TSource source);
}

public class Mapper : IMap
{
    private readonly IMapper _mapper;
    private readonly ILogger<Mapper> _logger;

    public Mapper(IMapper mapper, ILogger<Mapper> logger)
    {
        _logger = logger;
        _mapper = mapper;
        _logger.LogInformation("AutoMapper Start working");
    }

    public TOutput Map<TSource, TOutput>(TSource source)
    {
        _logger
            .LogTrace("AutoMapper  Map {source} To {destination} with data {sourcedata}",
                      typeof(TSource),
                      typeof(TOutput),
                      source);

        return _mapper.Map<TSource, TOutput>(source);
    }
}

public class AutoMapperConfig
{
    public string AssmblyNamesForLoadProfiles { get; set; } = string.Empty;
}

