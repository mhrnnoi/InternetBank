using InternetBank.Domain.Accounts.ValueObjects;
using InternetBank.Domain.Repositories;
using InternetBank.Domain.Transactions.Entities;
using InternetBank.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InternetBank.Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{


        // public async Task<List<Transaction>> GetByDateAndSuccess(DateOnly from,
        //                                                          DateOnly to,
        //                                                          bool isSuccess)
        // {

        //     return await dbContext.Where(x => DateOnly.FromDateTime(x.CreatedDateTime) >= from
        //                                       && DateOnly.FromDateTime(x.CreatedDateTime) <= to
        //                                       && x.IsSuccess == isSuccess)
        //                                                                   .ToListAsync();


        // }

        // public async Task<Transaction?> GetByOTP(string otp,
        //                                          double amount)
        // {
        //     return await dbContext.FirstOrDefaultAsync(x => x.Otp == otp
        //                                                     && amount == x.Amount);
        // }

        //     public async Task<List<Transaction>> GetLastFive()
        //     {
        //         var transactions = await dbContext.ToListAsync();
        //         return transactions.OrderByDescending(x => x.CreatedDateTime).Take(5).ToList();
        //     }

        public string SendOTP(string receptor, double amount, Otp otp)
        {

                var time = TimeOnly.FromDateTime(DateTime.UtcNow);
                var strTime = time.Hour.ToString() + ":" + time.Minute.ToString() + ":" + time.Second.ToString();
                var api = new Kavenegar.KavenegarApi("79677737392F5164527849523448454B45654F702F3476416F65665159796E4F342F3146754536453966773D");
                var res = api.VerifyLookup(receptor,
                                           otp.Value.ToString(),
                                           amount.ToString(),
                                           strTime,
                                           "OtpSimulatorTest");
                return res.StatusText;
        }
}
