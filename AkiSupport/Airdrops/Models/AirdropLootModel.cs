﻿using Newtonsoft.Json;

namespace SIT.Core.AkiSupport.Airdrops.Models
{
    public class AirdropLootModel
    {
        [JsonProperty("tpl")]
        public string Tpl { get; set; }

        [JsonProperty("isPreset")]
        public bool IsPreset { get; set; }

        [JsonProperty("stackCount")]
        public int StackCount { get; set; }

        [JsonProperty("id")]
        public string ID { get; set; }
    }
}