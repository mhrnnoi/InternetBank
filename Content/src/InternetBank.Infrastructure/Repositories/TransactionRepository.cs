using InternetBank.Domain.Repositories;
using InternetBank.Domain.Transactions.Entities;
using InternetBank.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InternetBank.Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly DbSet<Transaction> dbContext;
    public TransactionRepository(ApplicationDbContext appdbContext)
    {
        dbContext = appdbContext.Transactions;
    }

    public void Add(Transaction transactions)
    {
        dbContext.Add(transactions);
    }

    public async Task<List<Transaction>> GetByDateAndSuccess(DateOnly? from,
                                                             DateOnly? to,
                                                             bool? isSuccess)
    {
        if (from is not null && to is not null && isSuccess is not null)
            return await dbContext.Where(x => DateOnly.FromDateTime(x.CreatedDateTime) >= from && DateOnly.FromDateTime(x.CreatedDateTime) <= to && x.IsSuccess == isSuccess)
                                .ToListAsync();

        else if (from is null && to is not null && isSuccess is not null)
            return await dbContext.Where(x => DateOnly.FromDateTime(x.CreatedDateTime) <= to && x.IsSuccess == isSuccess)
                                .ToListAsync();

        else if (from is not null && to is null && isSuccess is not null)
            return await dbContext.Where(x => DateOnly.FromDateTime(x.CreatedDateTime) >= from && x.IsSuccess == isSuccess)
                    .ToListAsync();

        else if (from is not null && to is not null && isSuccess is null)
            return await dbContext.Where(x => DateOnly.FromDateTime(x.CreatedDateTime) >= from && DateOnly.FromDateTime(x.CreatedDateTime) <= to)
                                .ToListAsync();

        else if (from is null && to is null && isSuccess is not null)
            return await dbContext.Where(x => x.IsSuccess == isSuccess)
                                .ToListAsync();

        else if (from is null && to is not null && isSuccess is null)
            return await dbContext.Where(x => DateOnly.FromDateTime(x.CreatedDateTime) <= to)
                                .ToListAsync();

        else if (from is not null && to is null && isSuccess is null)
            return await dbContext.Where(x => DateOnly.FromDateTime(x.CreatedDateTime) >= from)
                                .ToListAsync();
        else
        {
            var transactions = await dbContext.ToListAsync();
            return transactions.OrderByDescending(x => x.CreatedDateTime).Take(5).ToList();
        }

    }

    public async Task<Transaction?> GetByOTP(string otp,
                                                                 double amount,
                                                                 string userId)
    {
        return await dbContext.FirstOrDefaultAsync(x => x.Otp == otp
                                                        && amount == x.Amount
                                                        && x.UserId == userId);
    }

    public string SendOTP(string receptor, double amount)
    {
        string otp = "";
        var rand = new Random();
        for (int i = 0; i < 5; i++)
        {
            otp += rand.Next(0, 9);
        }
        var time = TimeOnly.FromDateTime(DateTime.UtcNow);
        var strTime = time.Hour.ToString() + ":" + time.Minute.ToString() + ":" + time.Second.ToString();
        var api = new Kavenegar.KavenegarApi("79677737392F5164527849523448454B45654F702F3476416F65665159796E4F342F3146754536453966773D");
        api.VerifyLookup(receptor,
                         otp,
                         amount.ToString(),
                         strTime,
                         "OtpSimulatorTest");
        return otp;
    }
}
