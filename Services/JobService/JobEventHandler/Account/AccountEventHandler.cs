﻿using Hangfire;
using IApplicationService.AppEvent;
using Oxygen.Client.ServerProxyFactory.Interface;
using Oxygen.Client.ServerSymbol.Events;
using System;
using System.Threading.Tasks;

namespace JobService.JobEventHandler.Account
{
    public class AccountEventHandler : IEventHandler
    {
        private readonly IStateManager stateManager;
        public AccountEventHandler(IStateManager stateManager)
        {
            this.stateManager = stateManager;
        }
        [EventHandlerFunc(EventTopicDictionary.Account.LoginSucc)]
        public async Task<DefaultEventHandlerResponse> LoginCacheExpireJob(EventHandleRequest<LoginSuccessDto> input)
        {
            //作业执行延时后失效登录Token
            var jobid = BackgroundJob.Schedule<IEventBus>(x => x.SendEvent(EventTopicDictionary.Account.LoginExpire, input.data), TimeSpan.FromDays(7));
            return await Task.FromResult(DefaultEventHandlerResponse.Default());
        }
    }
}