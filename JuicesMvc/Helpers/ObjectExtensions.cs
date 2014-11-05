using Newtonsoft.Json;

namespace JuicesMvc.Helpers {
	public static class ObjectExtensions {
	    public static string ToJson(this object obj, JsonSerializerSettings settings = null,  Formatting? formatting = null)
	    {
	        /*var settings = new JsonSerializerSettings
	        {
	            PreserveReferencesHandling = PreserveReferencesHandling.Objects
	        };*/

	        var ser = JsonSerializer.Create(settings);
            
            
	        return formatting != null ? 
                JsonConvert.SerializeObject(obj, formatting.Value) : 
                JsonConvert.SerializeObject(obj);
	    }
	}
}