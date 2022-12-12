using System.IO;
using System.Threading.Tasks;

namespace EnergyCtrlrAlg
{
    public class StreamWriterClass
    {
        public static async Task WriteAsync(string[] output)
        {
            await File.WriteAllLinesAsync("SimCtrlrOutput.txt", output);
        }
    }
}