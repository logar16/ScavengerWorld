using ScavengerWorld.World;
using System.Text;

namespace ScavengerWorld.Printing
{
    public class GeographyPrinter
    {
        public static string PrintGeography(Geography geography)
        {
            var builder = new StringBuilder();
            builder.AppendLine();
            for (int row = 0; row < geography.Height; row++)
            {
                for (int col = 0; col < geography.Width; col++)
                {
                    if (col == 0)
                        builder.Append("| ");

                    var terrain = " ";
                    switch (geography.GetTerrainAt(row, col))
                    {
                        //case Geography.Terrain.NORMAL:
                        //    terrain = "~";
                        //    break;
                        case Geography.Terrain.ROUGH:
                            terrain = "#";
                            break;
                    }

                    builder.Append($"{terrain} | ");
                }
                builder.AppendLine();
            }
            return builder.ToString();
        }
    }
}
