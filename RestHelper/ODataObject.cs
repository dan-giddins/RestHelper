using Newtonsoft.Json;

namespace RestHelper
{
	/// <summary>
	/// This represents the object which is returned from an OData API.
	/// This simply contains a metadata value, as well as a collection of the filtered object type.
	/// </summary>
	public class ODataObject<T>
	{
		/// <summary>
		/// Provides a link detailing the metadata of the objects contained in Value
		/// </summary>
		[JsonProperty("@odata.context")]
		public string? Context { get; set; }
		/// <summary>
		/// Collection of objects filtered by OData
		/// </summary>
		[JsonProperty("value")]
		public T? Value { get; set; }
	}
}
