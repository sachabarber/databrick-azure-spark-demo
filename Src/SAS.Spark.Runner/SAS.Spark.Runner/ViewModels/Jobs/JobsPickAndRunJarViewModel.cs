using SAS.Spark.Runner.REST.DataBricks;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using SAS.Spark.Runner.Services;
using System.Linq;
using SAS.Spark.Runner.REST.DataBricks.Requests;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Diagnostics;

namespace SAS.Spark.Runner.ViewModels.Jobs
{
    public class JobsPickAndRunJarViewModel : INPCBase
    {
        private IMessageBoxService _messageBoxService;
        private IDatabricksWebApiClient _databricksWebApiClient;
        private IOpenFileService _openFileService;
        private IDataBricksFileUploadService _dataBricksFileUploadService;
        private string _jarFilePath;
        private string _status;
        private FileInfo _jarFile;
        private bool _isBusy;
        private bool _isPolling = false;
        private string _jobsJson;
        private Stopwatch _watch = new Stopwatch();

        public JobsPickAndRunJarViewModel(
            IMessageBoxService messageBoxService,
            IDatabricksWebApiClient databricksWebApiClient,
            IOpenFileService openFileService,
            IDataBricksFileUploadService dataBricksFileUploadService)
        {
            _messageBoxService = messageBoxService;
            _databricksWebApiClient = databricksWebApiClient;
            _openFileService = openFileService;
            _dataBricksFileUploadService = dataBricksFileUploadService;
            PickInputJarFileCommand = new SimpleAsyncCommand<object, object>(x => !IsBusy && !_isPolling, ExecutePickInputJarFileCommandAsync);
        }

        public string JobsJson
        {
            get
            {
                return this._jobsJson;
            }
            set
            {
                RaiseAndSetIfChanged(ref this._jobsJson, value, () => JobsJson);
            }
        }

        public string JarFilePath
        {
            get
            {
                return this._jarFilePath;
            }
            set
            {
                RaiseAndSetIfChanged(ref this._jarFilePath, value, () => JarFilePath);
            }
        }

        public string Status
        {
            get
            {
                return this._status;
            }
            set
            {
                RaiseAndSetIfChanged(ref this._status, value, () => Status);
            }
        }

        public bool IsBusy
        {
            get
            {
                return this._isBusy;
            }
            set
            {
                RaiseAndSetIfChanged(ref this._isBusy, value, () => IsBusy);
            }
        }

        public ICommand PickInputJarFileCommand { get; private set; }
 
        

        private async Task<object> ExecutePickInputJarFileCommandAsync(object param)
        {
            IsBusy = true;

            try
            {
                _openFileService.Filter = "Jar Files (*.jar)|*.jar";
                _openFileService.InitialDirectory = @"c:\";
                _openFileService.FileName = "";
                var dialogResult = _openFileService.ShowDialog(null);
                if(dialogResult.Value)
                {
                    if(!_openFileService.FileName.ToLower().EndsWith(".jar"))
                    {
                        _messageBoxService.ShowError($"{_openFileService.FileName} is not a JAR file");
                        return Task.FromResult<object>(null);
                    }
                    _jarFile = new FileInfo(_openFileService.FileName);
                    JarFilePath = _jarFile.Name;
                    var rawBytesLength = File.ReadAllBytes(_jarFile.FullName).Length;
                    await _dataBricksFileUploadService.UploadFileAsync(_jarFile, rawBytesLength,
                        (newStatus) => this.Status = newStatus);

                    bool uploadedOk = await IsDbfsFileUploadedAndAvailableAsync(_jarFile, rawBytesLength);
                    if (uploadedOk)
                    {
                        //TODO : 2.0/jobs/runs/submit
                        //TODO : ooll for success using jobs/runs/get, store that in the JSON

                        var runId = await SubmitJarJobAsync(_jarFile);
                        if(!runId.HasValue)
                        {
                            IsBusy = false;
                            _messageBoxService.ShowError(this.Status = $"Looks like there was a problem with calling Spark API '2.0/jobs/runs/submit'");
                        }
                        else
                        {
                            await PollForRunIdAsync(runId.Value);
                        }

                    }
                    else
                    {
                        IsBusy = false;
                        _messageBoxService.ShowError("Looks like the Jar file did not upload ok....Boo");
                    }
                }
            }
            catch (Exception ex)
            {
                _messageBoxService.ShowError(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
            return Task.FromResult<object>(null);
        }

        private async Task<bool> IsDbfsFileUploadedAndAvailableAsync(FileInfo dbfsFile, int rawBytesLength)
        {
            bool fileUploadOk = false;
            int maxNumberOfAttemptsAllowed = 10;
            int numberOfAttempts = 0;

            while (!fileUploadOk || (numberOfAttempts == maxNumberOfAttemptsAllowed))
            {
                //check for the file in Dbfs
                var response = await _databricksWebApiClient.DbfsListAsync();
                fileUploadOk = response.files.Any(x =>

                    x.file_size == rawBytesLength &&
                    x.is_dir == false &&
                    x.path == $@"/{dbfsFile.Name}"
                );
                numberOfAttempts++;
                this.Status = $"Checking that Jar has been uploaded ok.\r\nAttempt {numberOfAttempts} out of {maxNumberOfAttemptsAllowed}";
                await Task.Delay(500);
            }
            return fileUploadOk;
        }

        private async Task<int?> SubmitJarJobAsync(FileInfo dbfsFile)
        {
            this.Status = $"Creating the Spark job using '2.0/jobs/runs/submit'";

            // =====================================================================
            // EXAMPLE REQUEST
            // =====================================================================
            //{
            //  "run_name": "my spark task",
            //  "new_cluster": 
            //  {
            //    "spark_version": "3.4.x-scala2.11",
            //    "node_type_id": "Standard_D3_v2",
            //    "num_workers": 10
            //  },
            //  "libraries": [
            //    {
            //      "jar": "dbfs:/my-jar.jar"
            //    }
            //  ],
            //  "timeout_seconds": 3600,
            //  "spark_jar_task": {
            //    "main_class_name": "com.databricks.ComputeModels",
            //    "parameters" : ["10"]
            //  }
            //}

            var datePart = DateTime.Now.ToShortDateString().Replace("/", "");
            var timePart = DateTime.Now.ToShortTimeString().Replace(":", "");
            var request = new RunsSubmitJarTaskRequest()
            {
                run_name = $"JobsPickAndRunJarViewModel_{datePart}_{timePart}",
                new_cluster = new NewCluster
                {
                    // see api/2.0/clusters/spark-versions
                    spark_version = "4.0.x-scala2.11",
                    // see api/2.0/clusters/list-node-types
                    node_type_id = "Standard_F4s",
                    num_workers = 2
                },
                libraries = new List<Library>
                {
                    new Library { jar = $"dbfs:/{dbfsFile.Name}"}
                },
                timeout_seconds = 3600,
                spark_jar_task = new SparkJarTask
                {
                    main_class_name = "SparkApp",
                    parameters = new List<string>() { "10" }
                }
            };

            var response = await _databricksWebApiClient.JobsRunsSubmitJarTaskAsync(request);
            return response.RunId;
        }

        private async Task PollForRunIdAsync(int runId)
        {
            _watch.Reset();
            _watch.Start();
            while (_isPolling)
            {
                var response = await _databricksWebApiClient.JobsRunsGetAsync(runId);
                JobsJson = JsonConvert.SerializeObject(response, Formatting.Indented);
                var state = response.state;
                this.Status = "Job not complete polling for completion.\r\n" +
                    $"Job has been running for {_watch.Elapsed.Seconds} seconds";

                try
                {
                    if (!string.IsNullOrEmpty(state.result_state))
                    {
                        _isPolling = false;
                        IsBusy = false;
                        _messageBoxService.ShowInformation(
                            $"Job finnished with Status : {state.result_state}");
                    }
                    else
                    {
                        switch (state.life_cycle_state)
                        {
                            case "TERMINATING":
                            case "RUNNING":
                            case "PENDING":
                                break;
                            case "SKIPPED":
                            case "TERMINATED":
                            case "INTERNAL_ERROR":
                                _isPolling = false;
                                IsBusy = false;
                                break;
                        }
                    }
                }
                finally
                {
                    if (_isPolling)
                    {
                        await Task.Delay(5000);
                    }
                }
            }
        }
    }
}
