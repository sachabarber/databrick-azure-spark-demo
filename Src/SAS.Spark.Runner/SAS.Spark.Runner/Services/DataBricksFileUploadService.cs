using SAS.Spark.Runner.REST.DataBricks;
using SAS.Spark.Runner.REST.DataBricks.Requests;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SAS.Spark.Runner.Services
{
    public class DataBricksFileUploadService : IDataBricksFileUploadService
    {
        private IDatabricksWebApiClient _databricksWebApiClient;

        public DataBricksFileUploadService(IDatabricksWebApiClient databricksWebApiClient)
        {
            _databricksWebApiClient = databricksWebApiClient;
        }

        public async Task UploadFileAsync(FileInfo file, int rawBytesLength, 
            Action<string> statusCallback, string path = "")
        {
            var dbfsPath = $"/{file.Name}";

            //Step 1 : Create the file
            statusCallback("Creating DBFS file");
            var dbfsCreateResponse = await _databricksWebApiClient.DbfsCreateAsync(
                new DatabricksDbfsCreateRequest
                    {
                        overwrite = true,
                        path = dbfsPath
                    });

            //Step 2 : Add block in chunks
            FileStream fileStream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read);
            var oneMegaByte = 1 << 20;
            byte[] buffer = new byte[oneMegaByte];
            int bytesRead = 0;
            int totalBytesSoFar = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                totalBytesSoFar += bytesRead;
                statusCallback(
                    $"Uploaded {FormatAsNumeric(totalBytesSoFar)} " +
                    $"out of {FormatAsNumeric(rawBytesLength)} bytes to DBFS");
                var base64EncodedData = Convert.ToBase64String(buffer.Take(bytesRead).ToArray());

                await _databricksWebApiClient.DbfsAddBlockAsync(
                    new DatabricksDbfsAddBlockRequest
                        {
                            data = base64EncodedData,
                            handle = dbfsCreateResponse.Handle
                    });

            }
            fileStream.Close();

            //Step 3 : Close the file
            statusCallback($"Finalising write to DBFS file");
            await _databricksWebApiClient.DbfsCloseAsync(
                    new DatabricksDbfsCloseRequest
                    {
                        handle = dbfsCreateResponse.Handle
                    });

        }

        private string FormatAsNumeric(int byteLength)
        {
            return byteLength.ToString("###,###,###");
        }
    }
}
