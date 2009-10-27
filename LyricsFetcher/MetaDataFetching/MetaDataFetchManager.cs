using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LyricsFetcher
{
    class MetaDataFetchManager : AbstractFetchManager
    {
        public MetaDataFetchManager() {
            this.MaxFetchingThreads = 2;
        }

        protected override void CancelInternal(Song song) {
            MetaDataLookup lookup = (MetaDataLookup)this.songStatusMap[song];
            if (lookup != null)
                lookup.Cancel();
        }

        protected override void QueueInternal(Song song) {
            MetaDataLookup lookup = new MetaDataLookup(song);
            this.songStatusMap[song] = lookup;
            lookup.StatusEvent += new EventHandler<MetaDataEventArgs>(lookupStatusEvent);
            lookup.ChangeState(MetaDataStatus.Waiting);
        }

        protected override void StartInternal(Song song) {
            MetaDataLookup lookup = (MetaDataLookup)this.songStatusMap[song];
            lookup.RunAsync(song);
        }

        #region Events

        public event EventHandler<MetaDataEventArgs> StatusEvent;

        protected virtual void OnStatusEvent(MetaDataEventArgs arg) {
            if (this.StatusEvent != null)
                this.StatusEvent(this, arg);
        }

        #endregion

        void lookupStatusEvent(object sender, MetaDataEventArgs e) {
            this.OnStatusEvent(e);
            if (e.MetaData.IsFinished) {
                MetaDataLookup lookup = (MetaDataLookup)sender;
                lookup.StatusEvent -= new EventHandler<MetaDataEventArgs>(lookupStatusEvent);
                this.CleanupOne(e.MetaData.Song);
            }
        }
    }
}
