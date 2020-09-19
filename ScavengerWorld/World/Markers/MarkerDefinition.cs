using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ScavengerWorld.Sensory;
using System;

namespace ScavengerWorld.World.Markers
{
    public class MarkerDefinition
    {
        [JsonProperty("display")]
        public SensoryDisplay Display { get; set; }
        [JsonProperty("duration")]
        public double Duration { get; set; }
        [JsonProperty("decayRate")]
        public double DecayRate { get; set; }


        public static MarkerDefinition ParseDefinition(JObject data)
        {
            return JsonConvert.DeserializeObject<MarkerDefinition>(data.ToString());
        }

        public static Marker ParseMarker(JObject data, Guid owner)
        {
            var definition = ParseDefinition(data);
            string type = data.Value<string>("type");

            switch (type)
            {
                case "exponential":
                    return new ExponentialDecayMarker(definition, owner);
                default:    //linear
                    return new LinearDecayMarker(definition, owner);
            }
        }

    }
}
