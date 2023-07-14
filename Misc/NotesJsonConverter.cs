﻿using EFT;
using HarmonyLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SIT.Core.Misc
{
    public class NotesJsonConverter : JsonConverter
    {
        private static Type _targetType;

        public NotesJsonConverter()
        {
            _targetType = typeof(AbstractGame).Assembly.GetTypes().First(t =>
                t.GetProperty("TransactionInProcess", BindingFlags.Instance | BindingFlags.Public) != null);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var valueToSerialize = Traverse.Create(_targetType).Field<List<object>>("Notes").Value;
            serializer.Serialize(writer, $"{{\"Notes\":{JsonConvert.SerializeObject(valueToSerialize)}}}");
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == _targetType;
        }
    }
}
