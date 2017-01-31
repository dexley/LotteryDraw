using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using SIS.Lottery.Api.Models;

namespace SIS.Lottery.Api.Application
{
    public interface ILotteryService
    {
        Task<CommandResult> CreateDraw(LotteryDraw lotteryDraw);

        Task<CommandResult> UpdateDraw(LotteryDraw lotteryDraw);

        Task<IEnumerable<LotteryDraw>> SearchDraws(DateTime drawDate);
    }
}