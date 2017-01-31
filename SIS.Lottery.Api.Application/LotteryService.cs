using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIS.Lottery.Api.Models;


namespace SIS.Lottery.Api.Application
{

    public enum CommandResult
    {
        Ok,
        AlreadyExists,
        Invalid,
        NotFound
    }


    public class LotteryService : ILotteryService
    {
        private readonly IRepository<LotteryDraw> _lotteryDrawRepository;

        public LotteryService(IRepository<LotteryDraw> lotteryDrawRepository)
        {
            _lotteryDrawRepository = lotteryDrawRepository;
        }

        public async Task<CommandResult> CreateDraw(LotteryDraw lotteryDraw)
        {
            var result = await _lotteryDrawRepository.Find(lotteryDraw.Name);

            if (result != null)
            {
                return CommandResult.AlreadyExists;
            }

            await _lotteryDrawRepository.Create(lotteryDraw);

            return CommandResult.Ok;
        }

        public async Task<CommandResult> UpdateDraw(LotteryDraw lotteryDraw)
        {
            var result = await _lotteryDrawRepository.Find(lotteryDraw.Name);

            if (result == null)
            {
                return CommandResult.NotFound;
            }

            if (
                lotteryDraw.WinningPrimaryNumbers.Any(
                    x => x < result.RangePrimaryNumbers.Item1 || x > result.RangePrimaryNumbers.Item2) ||
                lotteryDraw.WinningSecondaryNumbers.Any(
                    x => x < result.RangeSecondaryNumbers.Item1 || x > result.RangeSecondaryNumbers.Item2))
            {
                return CommandResult.Invalid;
             }


            await _lotteryDrawRepository.Update(lotteryDraw);

            return CommandResult.Ok;
        }

        public async Task<IEnumerable<LotteryDraw>> SearchDraws(DateTime drawDate)
        {
            var all = await _lotteryDrawRepository.GetAll();
            return all.ToList().Where(x => x.DrawDate == drawDate);
        }
    }
}
