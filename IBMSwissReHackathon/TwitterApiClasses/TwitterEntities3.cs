﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IBMSwissReHackathon.TwitterApiClasses
{

    internal class TwitterEntities3
    {

        [JsonProperty("urls")]
        public object[] Urls { get; set; }

        [JsonProperty("hashtags")]
        public Hashtag3[] Hashtags { get; set; }

        [JsonProperty("user_mentions")]
        public UserMention2[] UserMentions { get; set; }

        [JsonProperty("symbols")]
        public object[] Symbols { get; set; }
    }

}
