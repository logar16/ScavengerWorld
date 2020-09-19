using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ScavengerWorld.Sensory;
using ScavengerWorld.World;
using ScavengerWorld.World.Markers;
using System;
using Xunit;

namespace ScavengerWorldTest.Markers
{
    public class MarkerParsing
    {
        [Fact]
        public void ParseMarkerDefinition_UsesInputData()
        {
            var display = new SensoryDisplay();
            display.Auditory.ResetTo(3, 7);
            var jsonDisplay = JObject.Parse(JsonConvert.SerializeObject(display));

            var data = new JObject
            {
                { "display", jsonDisplay },
                { "duration", 11 },
                { "decayRate", 0.95 }
            };

            //Act
            var definition = MarkerDefinition.ParseDefinition(data);

            //Assert
            Assert.NotNull(definition);
            Assert.Equal(7, definition.Display.Auditory.Strength);
            Assert.Equal(11, definition.Duration);
            Assert.Equal(0.95, definition.DecayRate);
        }

        [Theory]
        [InlineData("", typeof(LinearDecayMarker))]
        [InlineData("linear", typeof(LinearDecayMarker))]
        [InlineData("exponential", typeof(ExponentialDecayMarker))]
        public void ParseMarker_CreatesProperMarker(string inputType, Type expectedType)
        {
            var guid = Guid.NewGuid();

            var display = new SensoryDisplay();
            display.Smell.ResetTo(4, 8);
            var jsonDisplay = JObject.Parse(JsonConvert.SerializeObject(display));

            var data = new JObject
            {
                { "display", jsonDisplay },
                { "duration", 11 },
                { "decayRate", 0.95 },
                { "type", inputType }
            };

            //Act
            var marker = MarkerDefinition.ParseMarker(data, guid);

            //Assert
            Assert.NotNull(marker);
            Assert.Equal(guid, (marker as ITransferable).Owner);
            Assert.Equal(expectedType, marker.GetType());
            Assert.Equal(8, marker.Display.Smell.Strength);
        }
    }
}
