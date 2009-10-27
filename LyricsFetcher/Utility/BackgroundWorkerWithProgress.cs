/*
 * This BackgroundWorker knows how to signal progress and termination events.
 * It is also able to be cancelled in an orderly manner.
 *
 * Author: Phillip Piper
 * Date: 22/01/2008 8:08 PM
 *
 * CHANGE LOG:
 * when who what
 * 2009-02-03  JPP  Changed IsRunning to use IsBusy
 * 2008-01-22  JPP  Initial Version
 */

using System;
using System.ComponentModel;

namespace LyricsFetcher
{
    /// <summary>
    /// Information that is broadcast during progress events
    /// </summary>
    public class ProgressEventArgs : EventArgs
    {
        public bool IsCancelled;
        public int Percentage;
        public string Message;
    }

    /// <summary>
    /// This is a background task that can signal progress and termination events.
    /// It can be cancelled in an orderly fashion, and can be waiting until it completes.
    /// </summary>
    /// <remarks>
    /// Subclasses should override DoWork() and periodically call ReportProgress().
    /// Within DoWork(), the code should regularly check CanContinueRunning to see if the
    /// method should terminate.
    /// </remarks>
    public class BackgroundWorkerWithProgress
    {
        public BackgroundWorkerWithProgress() {
        }

        #region Public properties

        /// <summary>
        /// Is the worker currently running?
        /// </summary>
        [Browsable(false)]
        public bool IsRunning {
            get { return this.bgw != null && this.bgw.IsBusy; }
        }

        /// <summary>
        /// Has this process been cancelled?
        /// </summary>
        [Browsable(false)]
        public bool IsCancelled {
            get { return this.isCancelled; }
        }
        private bool isCancelled = false;

        /// <summary>
        /// What was the result from the process?
        /// </summary>
        [Browsable(false)]
        public object Result {
            get { return this.result; }
        }
        protected object result;

        #endregion

        #region Commands

        /// <summary>
        /// If the command is currently executing, cancel it
        /// </summary>
        public virtual void Cancel() {
            this.isCancelled = true;
        }

        /// <summary>
        /// Execute the command and wait for the result
        /// </summary>
        public void Run() {
            this.RunAsync();
            this.Wait();
        }

        /// <summary>
        /// Execute the command and return immediately.
        /// </summary>
        public void RunAsync() {
            this.RunAsync(null);
        }

        /// <summary>
        /// Execute the command and return immediately.
        /// </summary>
        public void RunAsync(object argument) {
            this.isCancelled = false;

            this.bgw = new BackgroundWorker();
            this.bgw.WorkerReportsProgress = true;
            this.bgw.WorkerSupportsCancellation = true;
            this.bgw.DoWork += new DoWorkEventHandler(bgw_DoWork);
            this.bgw.ProgressChanged += new ProgressChangedEventHandler(bgw_ProgressChanged);
            this.bgw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgw_RunWorkerCompleted);
            this.bgw.RunWorkerAsync(argument);
        }
        protected BackgroundWorker bgw;


        /// <summary>
        /// Wait until the process has finished
        /// </summary>
        public void Wait() {
            while (this.IsRunning) {
                System.Windows.Forms.Application.DoEvents();
                System.Threading.Thread.Sleep(10);
            }
        }

        #endregion

        #region Implementation

        /// <summary>
        /// Subclasses should override this method to do whatever they want.
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        protected virtual object DoWork(DoWorkEventArgs e) {
            return null;
        }

        /// <summary>
        /// Call this periodically witin DoWork() to decide if the method should continue
        /// </summary>
        protected bool CanContinueRunning {
            get {
                return !this.IsCancelled && !this.bgw.CancellationPending;
            }
        }

        /// <summary>
        /// Call this within DoWork() to report progress
        /// </summary>
        /// <param name="percentage"></param>
        protected void ReportProgress(int percentage) {
            if (this.bgw != null)
                this.bgw.ReportProgress(percentage);
        }

        /// <summary>
        /// Call this within DoWork() to report progress
        /// </summary>
        /// <param name="percentage"></param>
        protected void ReportProgress(int percentage, string msg) {
            this.bgw.ReportProgress(percentage, msg);
        }

        #endregion

        #region Events

        public event EventHandler<ProgressEventArgs> ProgessEvent;
        public event EventHandler<ProgressEventArgs> DoneEvent;

        protected virtual void OnProgressEvent(ProgressEventArgs args) {
            if (this.ProgessEvent != null)
                this.ProgessEvent(this, args);
        }

        protected virtual void OnDoneEvent(ProgressEventArgs args) {
            if (this.DoneEvent != null)
                this.DoneEvent(this, args);
        }

        #endregion

        #region BackgroundWorker event handling

        void bgw_DoWork(object sender, DoWorkEventArgs e) {
            this.ReportProgress(0, "Starting...");

            e.Result = this.DoWork(e);
            e.Cancel = (this.isCancelled || this.bgw.CancellationPending);
        }

        void bgw_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            ProgressEventArgs args = new ProgressEventArgs();
            args.Percentage = e.ProgressPercentage;
            args.Message = e.UserState as String;
            this.OnProgressEvent(args);

            // Did the event handler cancel the process?
            if (args.IsCancelled)
                this.isCancelled = true;

            if (this.isCancelled)
                this.bgw.CancelAsync();
        }

        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if (e.Error != null) {
                System.Diagnostics.Debug.WriteLine(e.Error.Message);
                throw e.Error;
            }

            if (!e.Cancelled)
                this.result = e.Result;

            ProgressEventArgs args = new ProgressEventArgs();
            args.Percentage = 100;
            args.IsCancelled = e.Cancelled;
            this.OnDoneEvent(args);
        }

        #endregion
    }
}
