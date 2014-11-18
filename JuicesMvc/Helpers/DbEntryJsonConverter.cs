using System;
using System.Data.Entity.Infrastructure;
using AutoMapper.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace JuicesMvc.Helpers {
	public class DbEntryJsonConverter : JsonConverter {
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
			writer.WriteNull();
			return;

			if (value == null) {
				writer.WriteNull();
				return;
			}

			var entry = (DbEntityEntry) value;
			writer.WriteStartObject();
			var res = new {
				entry.Entity, entry.State
			};

			/*serializer.
			serializer.Serialize(writer, JObject.FromObject(res));*/
			
			writer.WriteEndObject();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
			throw new NotSupportedException("DbEntryJsonConverter doesn't support reading");
		}

		public override bool CanConvert(Type objectType) {
			return typeof(DbEntityEntry).IsAssignableFrom(objectType);
		}
	}
}