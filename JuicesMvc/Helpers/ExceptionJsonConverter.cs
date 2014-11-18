using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace JuicesMvc.Helpers {
	public class ExceptionJsonConverter : JsonConverter {
		public Boolean IsDebug { get; set; }

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
			if (value == null) {
				writer.WriteNull();
				return;
			}
			var exception = (Exception)value;
			var type = exception.GetType();
			var contract = serializer.ContractResolver.ResolveContract(type);
			writer.WriteStartObject();
			var objectContract = contract as JsonObjectContract;
			if (objectContract != null) {
				foreach (var property in objectContract.Properties) {
					var propName = property.UnderlyingName;
					if (property.Ignored) continue;
					if (property.DeclaringType == typeof(Exception) && (propName == "TargetSite" || propName == "HResult")
					 ) {
						// Exception type's properties: Message, Data, InnerException, TargetSite, StackTrace, HelpLink, Source, HResult
						continue;
					}
					if ((propName == "InnerException" || propName == "StackTrace") && !IsDebug)
						continue;

					var pi = type.GetProperty(property.UnderlyingName);
					if (pi.CanRead) {

						var memberValue = pi.GetValue(exception, null);
						if (memberValue != null || serializer.NullValueHandling == NullValueHandling.Include) {
							writer.WritePropertyName(property.PropertyName);
							serializer.Serialize(writer, memberValue);
						}
					}
				}
			}
			writer.WriteEndObject();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
			throw new NotSupportedException("ExceptionJsonConverter doesn't support reading");
		}

		public override bool CanConvert(Type objectType) {
			return typeof(Exception).IsAssignableFrom(objectType);
		}
	}
}