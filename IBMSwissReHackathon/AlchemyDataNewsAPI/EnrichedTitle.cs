﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IBMSwissReHackathon.AlchemyDataNewsAPI
{

    internal class EnrichedTitle
    {

        [JsonProperty("concepts")]
        public Concept[] Concepts { get; set; }
    }

}