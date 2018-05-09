using System;
using System.Collections.Generic;
using System.Windows;
using Microsoft.Win32;



namespace SAS.Spark.Runner.Services
{
    /// <summary>
    /// This class implements the IOpenFileService for WPF purposes.
    /// </summary>
    public class WPFOpenFileService : IOpenFileService
    {

        /// <summary>
        /// Embedded OpenFileDialog to pass back correctly selected
        /// values to ViewModel
        /// </summary>
        private OpenFileDialog _ofd = new OpenFileDialog();

        /// <summary>
        /// This method should show a window that allows a file to be selected
        /// </summary>
        /// <param name="owner">The owner window of the dialog</param>
        /// <returns>A bool from the ShowDialog call</returns>
        public bool? ShowDialog(Window owner)
        {
            //Set embedded OpenFileDialog.Filter
            if (!String.IsNullOrEmpty(this.Filter))
                _ofd.Filter = this.Filter;

            //Set embedded OpenFileDialog.InitialDirectory
            if (!String.IsNullOrEmpty(this.InitialDirectory))
                _ofd.InitialDirectory = this.InitialDirectory;

            //return results
            return _ofd.ShowDialog(owner);
        }

        /// <summary>
        /// FileName : Simply use embedded OpenFileDialog.FileName
        /// Also allow users to set new FileName, which sets OpenFileDialog.FileName
        /// </summary>
        public string FileName
        {
            get { return _ofd.FileName; }
            set { _ofd.FileName = value; }
        }

        /// <summary>
        /// Filter : Simply use embedded OpenFileDialog.Filter
        /// </summary>
        public string Filter
        {
            get { return _ofd.Filter; }
            set { _ofd.Filter = value; }
        }

        /// <summary>
        /// Filter : Simply use embedded OpenFileDialog.InitialDirectory
        /// </summary>
        public string InitialDirectory
        {
            get { return _ofd.InitialDirectory; }
            set { _ofd.InitialDirectory = value; }
        }
    }
}
