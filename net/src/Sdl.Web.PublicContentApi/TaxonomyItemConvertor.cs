﻿using System;
using Newtonsoft.Json;
using Sdl.Web.PublicContentApi.ContentModel;

namespace Sdl.Web.PublicContentApi
{
    public class TaxonomyItemConvertor : JsonConverter
    {
        public override bool CanConvert(Type objectType) 
            => typeof (ISitemapItem).IsAssignableFrom(objectType);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var result = new TaxonomySitemapItem();
            try
            {
                var target = serializer.Deserialize<Newtonsoft.Json.Linq.JObject>(reader);
                serializer.Populate(target.CreateReader(), result);
                switch (result.Type)
                {
                    case "Page":
                        PageSitemapItem pageItem = new PageSitemapItem();
                        serializer.Populate(target.CreateReader(), pageItem);
                        return pageItem;
                }
            }
            catch
            {
            }
            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }   
}
