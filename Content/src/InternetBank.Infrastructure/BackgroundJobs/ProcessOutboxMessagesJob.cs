using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Azure.Core.Serialization;
using InternetBank.Domain.Abstracts.Primitives;
using InternetBank.Domain.Accounts.Entities;
using InternetBank.Infrastructure.Data;
using InternetBank.Infrastructure.OutboxMessages;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Quartz;

namespace InternetBank.Infrastructure.BackgroundJobs;

public class ProcessOutboxMessagesJob : IJob
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IPublisher _publisher;

    public ProcessOutboxMessagesJob(ApplicationDbContext dbContext, IPublisher publisher)
    {
        _dbContext = dbContext;
        _publisher = publisher;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var messages = await _dbContext.Set<OutboxMessage>()
                                       .Where(x => x.IsProcced == false)
                                       .Take(20)
                                       .ToListAsync(context.CancellationToken);
        foreach (var item in messages)
        {
            var domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(item.Content,
             new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
            if (domainEvent is null)
            {
                throw new Exception();
            }
            await _publisher.Publish(domainEvent);
            //may publishing got error we should catch it and handle it 
            item.IsProcced = true;
            item.ProccesedOnUTC = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();

        }


    }
}