using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace Bot_Application1.Dialogs
{
    [Serializable]
    public class SimpleDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            //result 是從simulator傳過來的訊息
            var activity = await result as Activity;
            if (activity.Text.StartsWith("cool"))
            {
                await context.PostAsync($"666");
            }
            else if (activity.Text.StartsWith("wow"))
                { await context.PostAsync($"999"); } else




            //context為連接simulator和這個bot的管道 PostAsync則是傳回去的函式
            await context.PostAsync($"我好餓");
            //將管道接到Step2函式下一次則變促發Step2而不是MessageReceivedAsync
            context.Wait(Step2);
        }
        private async Task Step2(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            await context.PostAsync(activity.Text + $" 我想吃晚餐");

            context.Wait(Step3);
        }
        private async Task Step3(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            await context.PostAsync("今天吃粥");

            context.Wait(MessageReceivedAsync);
        }
    }
}