using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tweetinvi;
using Tweetinvi.Models;

namespace Bot2Luis.Services
{
    public static class TwitterService
    {
        
        static TwitterService()
        {
            //Sustituir por las claves autorizadas de la API de Twitter
            const string consumerKey = "consumerKey";
            const string consumerSecret = "consumerSecret";
            const string consumerToken = "consumerToken";
            const string consumerTokenSecret = "consumerTokenSecret";

            Auth.SetUserCredentials(consumerKey, consumerSecret, consumerToken, consumerTokenSecret);
        }

        public static IUser GetUser(string name)
        {
            return User.GetUserFromScreenName(name);
        }

        public static IEnumerable<ITweet> GetKeyword(string keyword)
        {
            return Search.SearchTweets(keyword).Take(3);
        }

    }
}