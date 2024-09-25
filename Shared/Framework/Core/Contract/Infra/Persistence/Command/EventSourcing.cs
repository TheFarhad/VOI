﻿namespace Framework.Core.Contract.Infra.Persistence.Command;

using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain.Aggregate.Event;

public sealed record OutboxEvent
{
    // می توان برای این جدول و برای ستون های زیر نانکلاستر ایندکس ترکیبی در نظر گرفت
    // OwnerAggregate, IsProccessd

    public long Id { get; init; }
    public DomainEventState State { get; init; }
    public required string OwnerAggregate { get; init; }
    public required string OwnerAggregateType { get; init; }
    public required string EventName { get; init; }
    public required string EventTypeName { get; init; }
    public required string Payload { get; init; }
    public required string UserId { get; init; }
    public DateTime OccurredOnUtc { get; init; }
    public string? TraceId { get; init; }
    public string? SpanId { get; init; }
    public bool IsProccessd { get; set; }
}

public interface IEventStore
{
    void Save<TDomainEvent>(string aggregateName, string aggregateId, IEnumerable<TDomainEvent> events)
        where TDomainEvent : IEvent;

    Task SaveAsync<TDomainEvent>(string aggregateName, string aggregateId, IEnumerable<TDomainEvent> events)
        where TDomainEvent : IEvent;
}

public interface IEventSorcingRepository
{
    //Task<List<OutboxEvent>> Get(int maxCount = 100);
    //Task MarkAsRead(List<OutboxEvent> outBoxEventItems);
}