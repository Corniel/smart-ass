using SmartAss.Topology;
using System;
using System.Drawing;
using System.Linq;

namespace SmartAss.Logging
{
    public static class ImageLogger
    {
        public static Bitmap Heatmap<T>(int[] heatmap, Raster<T> raster) where T : RasterTile<T>
        {
            var size = 16;
            var image = new Bitmap(raster.Cols * size, raster.Rows * size);

            var max = heatmap.Max();

            if (max == 0)
            {
                return image;
            }
            var ratio = 254d / max;

            for (var row = 0; row < raster.Rows; row++)
            {
                for (var col = 0; col < raster.Cols; col++)
                {
                    var index = raster[col, row].Index;
                    int value = (int)Math.Ceiling(heatmap[index] * ratio);

                    for (var x = size * col; x < size * (col + 1); x++)
                    {
                        for (var y = size * row; y < size * (row + 1); y++)
                        {
                            image.SetPixel(x, y, Color.FromArgb(value, 0, 0));
                        }
                    }
                }
            }
            return image;
        }
    }
}
