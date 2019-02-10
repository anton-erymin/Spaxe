using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;



namespace SoundExtractor
{
    class Extractor
    {
        [DllImport("Extractor.dll")]
        public static extern int Process(IntPtr handle, int pause, uint[] data, int count, ulong total);
    }
}
