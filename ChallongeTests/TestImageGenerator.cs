using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ChallongeTests
{
    internal static class TestImageGenerator
    {
        internal static byte[] GenerateTestPngBytes()
        {
            string tempFileName = Path.ChangeExtension(Path.GetRandomFileName(), "png");

            using Bitmap bitmap = new(150, 150, PixelFormat.Format24bppRgb);
            bitmap.Save(tempFileName, ImageFormat.Png);

            using FileStream fs = new(tempFileName, FileMode.Open);
            byte[] buffer = new byte[fs.Length];
            fs.Read(buffer, 0, (int)fs.Length);
            fs.Close();
            File.Delete(tempFileName);
            
            return buffer;
        }
    }
}
