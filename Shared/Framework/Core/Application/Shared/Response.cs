namespace Framework.Core.Application.Shared;

public readonly record struct Response<TPayload>
{
    public readonly TPayload? Payload { get; } = default!;
    public readonly List<Error> Errors { get; } = [];
    public readonly bool HasError => Errors.Count > 0;

    private Response(TPayload? payload)
    {
        Payload = payload;
        Errors = [];
    }
    private Response(Error error)
    {
        Errors = [error];
        Payload = default!;
    }
    private Response(List<Error> errors)
    {
        Errors = [.. errors];
        Payload = default!;
    }
    public Response() { }

    public static Response<TPayload> New()
        => new();

    public static implicit operator Response<TPayload>(TPayload? payload)
        => new(payload);

    public static implicit operator Response<TPayload>(Error error)
     => new(error);

    public static implicit operator Response<TPayload>(List<Error> errors)
      => new(errors);
}

public readonly record struct Error
{
    public ErrorType Type { get; }
    public string Message { get; }
    public string Description { get; }
    public readonly int Status { get; }

    private Error(int code, string message, string description, ErrorType type)
    {
        Status = code;
        Message = message;
        Description = description;
        Type = type;
    }

    public static Error Server(string message, string description = "")
        => new(500, message, description, ErrorType.Server);

    public static Error BadRequest(string message, string description = "")
        => new(400, message, description, ErrorType.BadRequest);

    public static Error Conflict(string message, string description = "")
        => new(409, message, description, ErrorType.Conflict);

    public static Error NotFound(string message, string description = "")
        => new(404, message, description, ErrorType.NotFound);

    public static Error Unauthorized(string message, string description = "")
        => new(401, message, description, ErrorType.Unauthorized);

    public static Error Forbidden(string message, string description = "")
        => new(403, message, description, ErrorType.Forbidden);

    public static Error Unknown(string message, string description = "")
        => new(520, message, description, ErrorType.Unknown);
}

public enum ErrorType : byte
{
    Conflict = 1,
    NotFound = 2,
    Unauthorized = 3,
    Forbidden = 4,
    Server = 5,
    BadRequest = 6,
    Unknown = 7
}
