﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IBMSwissReHackathon.TwitterApiClasses
{

    internal class Hashtag
    {

        [JsonProperty("indices")]
        public int[] Indices { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }

}
