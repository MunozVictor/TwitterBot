using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;

namespace Bot2Luis.Dialogs
{
    //Sustituir por ID de Luis.ia de aplicación y su subscription key
    [LuisModel("ID", "SUB_KEY")]
    [Serializable]
    public class RootLuisDialog : LuisDialog<object>
    {
        [LuisIntent("")]
        [LuisIntent("None")]
        public async Task StartAsync(IDialogContext context , LuisResult result)
        {
            var message = "Disculpa, no he entendido lo que pides, ¿Podrias repetir?";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }

        [LuisIntent("FindUser")]
        public async Task FindUser(IDialogContext context , LuisResult result)
        {
            var username = string.Empty;
            if (result.Entities.Any()) username = result.Entities[0].Entity?.Replace(" ", string.Empty);
            if (!string.IsNullOrEmpty(username))
            {
                try
                {
                    var user = Services.TwitterService.GetUser(username);
                    if (user != null)
                    {
                        await context.PostAsync($"Esto es lo que dice Twitter sobre {username}");
                        await context.PostAsync(user?.Description);
                    }else
                    {
                        await context.PostAsync($"Disculpe, no encontramos ningun usuario con el nombre {username}");

                    }



                }catch(Exception ex)
                {
                    await context.PostAsync($"Disculpe, error al encontrar usuario con el nombre {username}");

                }


            }
            else
            {
                await context.PostAsync($"Disculpa no entiendo a que usuario de Twitter te referias");
            }
            context.Wait(MessageReceived);

        }

        [LuisIntent("SearchKeyWord")]
        public async Task SearchKeyWord(IDialogContext context,LuisResult result)
        {
            var keyWord = string.Empty;
            if (result.Entities.Any()) keyWord = result.Entities[0].Entity;
            if (!string.IsNullOrEmpty(keyWord))
            {
                

                var tweets = Services.TwitterService.GetKeyword(keyWord);
                if (tweets.Any())
                {

                    await context.PostAsync($"esto es lo que dice twitter sobre {keyWord}");
                    foreach (var tweet in tweets)
                    {
                        await context.PostAsync(tweet.Text);
                    }


                }
                else
                {
                    await context.PostAsync($"Disculpa, twitter no dice nada hoy sobre {keyWord}");
                }

            }else
            {
                await context.PostAsync("Disculpa, no entendi el trermino que queria buscar");

            }

            context.Wait(MessageReceived);
            
        }
    }
}