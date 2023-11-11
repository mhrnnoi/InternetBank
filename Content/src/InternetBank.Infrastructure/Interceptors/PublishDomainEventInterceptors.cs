using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using InternetBank.Domain.Abstracts.Primitives;
using InternetBank.Domain.Interfaces;
using InternetBank.Infrastructure.OutboxMessages;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;

namespace InternetBank.Infrastructure.Interceptors;

public class PublishDomainEventInterceptors : SaveChangesInterceptor
{
    private readonly IPublisher _publisher;

    public PublishDomainEventInterceptors(IPublisher publisher)
    {
        _publisher = publisher;
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
                                                                                InterceptionResult<int> result,
                                                                                CancellationToken cancellationToken = default)
    {
        var dbContext = eventData.Context;
        if (dbContext is null)
            return await base.SavingChangesAsync(eventData, result, cancellationToken);

        var entities = dbContext.ChangeTracker.Entries<IHasDomainEvents>()
                                                 .Where(x => x.Entity.DomainEvents.Any())
                                                 .Select(x => x.Entity).ToList();
        var domainEvents = entities.SelectMany(x => x.DomainEvents).ToList();

        entities.ForEach((x) => x.ClearDomainEvents()); 

        var outboxMessages =  domainEvents.Select(domainEvent => new OutboxMessage(domainEvent.GetType().Name,
                                                                                                        JsonConvert.SerializeObject(domainEvent,
                                                                                                        new JsonSerializerSettings()
                                                                                                        { TypeNameHandling = TypeNameHandling.All })));

        foreach (var domainEvent in domainEvents)
        {
            
        }
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

}