﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IBMSwissReHackathon.AlchemyDataNewsAPI
{

    internal class Url
    {

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("enrichedTitle")]
        public EnrichedTitle EnrichedTitle { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }

}