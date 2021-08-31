using Newtonsoft.Json;

namespace GCSEmulator.Dtos
{
    public interface IResourceResponse
    {
        [JsonProperty]
        string Kind { get; }
    }
}