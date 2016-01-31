﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IBMSwissReHackathon.TwitterApiClasses
{

    internal class Actor3
    {

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("statusesCount")]
        public int StatusesCount { get; set; }

        [JsonProperty("utcOffset")]
        public string UtcOffset { get; set; }

        [JsonProperty("languages")]
        public string[] Languages { get; set; }

        [JsonProperty("preferredUsername")]
        public string PreferredUsername { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("postedTime")]
        public string PostedTime { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("verified")]
        public bool Verified { get; set; }

        [JsonProperty("friendsCount")]
        public int FriendsCount { get; set; }

        [JsonProperty("twitterTimeZone")]
        public string TwitterTimeZone { get; set; }

        [JsonProperty("favoritesCount")]
        public int FavoritesCount { get; set; }

        [JsonProperty("listedCount")]
        public int ListedCount { get; set; }

        [JsonProperty("objectType")]
        public string ObjectType { get; set; }

        [JsonProperty("links")]
        public Link3[] Links { get; set; }

        [JsonProperty("location")]
        public Location4 Location { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("followersCount")]
        public int FollowersCount { get; set; }
    }

}
