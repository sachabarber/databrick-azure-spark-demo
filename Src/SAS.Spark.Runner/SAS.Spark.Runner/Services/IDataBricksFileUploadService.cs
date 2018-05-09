using System;
using System.IO;
using System.Threading.Tasks;

namespace SAS.Spark.Runner.Services
{
    public interface IDataBricksFileUploadService
    {
        Task UploadFileAsync(FileInfo file, int rawBytesLength, Action<string> statusCallback, string path ="");
    }
}
