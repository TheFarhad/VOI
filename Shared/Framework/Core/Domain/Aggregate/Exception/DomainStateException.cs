namespace Framework.Core.Domain.Aggregate.Exception;

using System;
using Tool.Shared;
using Framework.Shared.Exceptions;

public abstract class DomainStateException : Exception, IException
{
    public string[] Parameters { get; protected set; }

    public DomainStateException(string message, params string[] parameters) :
        base(message.PlaceHolder(parameters)) =>
        Parameters = [.. parameters];

    public override string ToString() => Message.PlaceHolder(Parameters);
}

public class InvalidPropException : DomainStateException
{
    public InvalidPropException(string message, params string[] parameters) : base(message, parameters) { }
}

public class InvalidReferenceException : DomainStateException
{
    public InvalidReferenceException(string message, params string[] parameters) : base(message, parameters) { }
}