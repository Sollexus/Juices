using Newtonsoft.Json;

namespace JuicesMvc.Helpers {
	public static class ObjectExtensions {
		public static string ToJson(this object obj, JsonSerializerSettings settings = null) {
			return settings != null ?
				JsonConvert.SerializeObject(obj, settings) :
				JsonConvert.SerializeObject(obj);
		}

		public static string ToJson(this object obj, JsonSerializerSettings settings, Formatting formatting) {
			return JsonConvert.SerializeObject(obj, formatting, settings);
		}
	}
}