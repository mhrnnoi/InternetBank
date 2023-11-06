using System.Transactions;
using InternetBank.Domain.Repositories;
using InternetBank.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InternetBank.Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly DbSet<Domain.Transactions.Transaction> dbContext;
    public TransactionRepository(ApplicationDbContext appdbContext)
    {
        dbContext = appdbContext.Transactions;

    }

    public void Add(Domain.Transactions.Transaction transactions)
    {
        dbContext.Add(transactions);
    }

    public async Task<List<Domain.Transactions.Transaction>> GetByDateAndSuccess(DateOnly? from, DateOnly? to, bool? isSuccess)
    {
        if (from is not null && to is not null && isSuccess is not null)
        {

            return await dbContext.Where(x => DateOnly.FromDateTime(x.CreatedDateTime) >= from && DateOnly.FromDateTime(x.CreatedDateTime) <= to && x.IsSuccess == isSuccess)
                                .ToListAsync();
        }
        else
        {
            var transactions = await dbContext.ToListAsync();
            return transactions.OrderByDescending(x => x.CreatedDateTime).Take(5).ToList();

        }

    }

    public async Task<Domain.Transactions.Transaction?> GetByOTP(string otp, double amount)
    {
        return await dbContext.FirstOrDefaultAsync(x => x.OTP == otp && amount == x.Amount);
    }

    public string SendOTP()
    {
        var sender = "10008663";
        var receptor = "09142898654";
        string otp = "";
        var rand = new Random();
        for (int i = 0; i < 5; i++)
        {
            otp += rand.Next(0, 9);
        }
        var message = otp;
        var api = new Kavenegar.KavenegarApi("79677737392F5164527849523448454B45654F702F3476416F65665159796E4F342F3146754536453966773D");
        var sdf = api.Send(sender, receptor, message);
        return otp;
    }
}
