using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraktalZeichner
{
    /// <summary>
    /// Class for efficient/parallel pixelwise processing of image data
    /// </summary>
    public class ImageArray : IDisposable
    {
        private Bitmap btm;
        private BitmapData bmpData;
        private IntPtr ptr;
        private int[] data;
        private int width;
        private int height;

        public ImageArray(Bitmap btm)
        {
            width = btm.Width;
            height = btm.Height;
            bmpData = btm.LockBits(new Rectangle(0, 0, btm.Width, btm.Height), ImageLockMode.ReadWrite, btm.PixelFormat);
            ptr = bmpData.Scan0;
            data = new int[btm.Width * btm.Height];
            System.Runtime.InteropServices.Marshal.Copy(ptr, data, 0, data.Length);
            this.btm = btm;
        }

        public void DoPixelWise(Func<int, int, int, int, int> op)
        {
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = op(i, width, height, data[i]);
            }
        }

        public void DoPixelWise(Func<int, int, int, int, int, int> op)
        {
            for (int i = 0; i < data.Length; i++)
            {
                int x = i % width;
                int y = i / width;
                data[i] = op(x, y, width, height, data[i]);
            }
        }

        public void DoPixelWiseParallel(Func<int, int, int, int, int, int> op)
        {
            Parallel.For(0, data.Length, i =>
            {
                int x = i%width;
                int y = i/width;
                data[i] = op(x, y, width, height, data[i]);
            });
        }

        public void EndProcessing()
        {
            System.Runtime.InteropServices.Marshal.Copy(data, 0, ptr, width * height);
            btm.UnlockBits(bmpData);
        }

        public void Dispose()
        {
            btm.Dispose();
        }
    }
}
