﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IBMSwissReHackathon.TwitterApiClasses
{

    internal class Search
    {

        [JsonProperty("results")]
        public int Results { get; set; }

        [JsonProperty("current")]
        public int Current { get; set; }
    }

}
