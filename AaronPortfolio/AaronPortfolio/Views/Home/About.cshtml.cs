using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AaronPortfolio.Views.Home.AboutModel
{
    public class AboutModel : PageModel
    {
        public void OnGet()
        {
        }

        public void OnPost(string newMessage)
        {
            var messageList = HttpContext.Session.GetString("messages");
            List<string> messages = string.IsNullOrEmpty(messageList) ? new List<string>() : JsonConvert.DeserializeObject<List<string>>(messageList);

            if (!string.IsNullOrEmpty(newMessage))
            {
                messages.Add(newMessage);
            }

            HttpContext.Session.SetString("messages", JsonConvert.SerializeObject(messages));
        }
    }
}
