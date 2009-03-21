/*
 * This file holds the class that provides an animating icon within an ObjectListView.
 *
 * Author: Phillip Piper
 * Date: 2009-02-14 8:28 AM
 *
 * CHANGE LOG:
 * 2009-02-14 JPP  - Initial version
 */

using System;
using System.Collections.Generic;
using System.Drawing;

using BrightIdeasSoftware;

namespace LyricsFetcher
{
    /// <summary>
    /// These delegates tell an animation helper if a particular model object
    /// should be draw with an animating icon.
    /// </summary>
    /// <param name="model">The model being considered</param>
    /// <returns>Is this model animating?</returns>
    public delegate bool IsAnimatingDelegate(object model);

    /// <summary>
    /// An AnimationHelper causes the icon in a particular column of an 
    /// ObjectListView to animate.
    /// </summary>
    /// <remarks>
    /// The current implementation does not work when the ListView is showing groups.
    /// </remarks>
    public class AnimationHelper
    {
        /// <summary>
        /// The list of names of the images to be used as the animation.
        /// These names must exist int the SmallImageList of the ObjectListView
        /// to which this AnimationHelper is attached.
        /// The first image in the list is used when not showing the animation.
        /// The other images are cycled.
        /// </summary>
        public List<string> ImageNames {
            get { return this.imageNames; }
            set { this.imageNames = value; }
        }
        private List<string> imageNames = new List<string>();

        /// <summary>
        /// Which column is going to have an animated image?
        /// </summary>
        public OLVColumn Column {
            get { return this.column; }
            set {
                this.column = value;
                this.column.ImageGetter = delegate(object model) {
                    if (this.IsAnimating(model)) {
                        return this.ImageNames[this.GetAnimationIndex(model)];
                    } else {
                        return this.ImageNames[0];
                    }
                };
            }
        }
        private OLVColumn column;

        /// <summary>
        /// This delegate is called when the helper need to know if a given
        /// model is currently being animated
        /// </summary>
        public IsAnimatingDelegate IsAnimatingGetter {
            get { return this.isAnimatingGetter; }
            set { this.isAnimatingGetter = value; }
        }
        private IsAnimatingDelegate isAnimatingGetter;

        /// <summary>
        /// Which ObjectListView is the parent of this helper?
        /// </summary>
        public ObjectListView ListView {
            get {
                if (this.Column == null)
                    return null;
                else
                    return (ObjectListView)this.Column.ListView;
            }
        }

        /// <summary>
        /// How many milliseconds should elapse between frames of
        /// the animation?
        /// </summary>
        public int MillisecondsBetweenAnimations {
            get { return this.millisecondsBetweenAnimations; }
            set {
                this.millisecondsBetweenAnimations = value;
                if (this.tickler != null)
                    this.tickler.Interval = this.millisecondsBetweenAnimations;
            }
        }
        private int millisecondsBetweenAnimations = 75;

        /// <summary>
        /// This takes the given image, which is a composite of all animation
        /// images, and breaks them into individual images of the given size.
        /// </summary>
        /// <param name="composite">The combined image</param>
        /// <param name="imageSize">How big is each individual image</param>
        /// <param name="maximumImages">At most this many images will be extracted from
        /// the composite</param>
        /// <remarks>
        /// <para>
        /// The images are animated row-by-row, not column-by-column.
        /// </para>
        /// <para>
        /// This replaces any previous ImageNames setting.
        /// </para>
        /// </remarks>
        public void SetCompositeAnimationImage(Bitmap composite, Size imageSize, int maximumImages) {
            this.ImageNames.Clear();

            int numHorizontal = composite.Size.Width / imageSize.Width;
            int numVertical = composite.Size.Height / imageSize.Height;

            String name = String.Format("{0}-{1}", this.Column.Text, Environment.TickCount);
            Rectangle r = new Rectangle();
            r.Size = imageSize;
            int count = 0;
            for (int j = 0; j < numVertical; j++) {
                r.X = 0;
                for (int i = 0; i < numHorizontal; i++) {
                    string imageName = String.Format("{0}-{1}", name, count);
                    this.ImageNames.Add(imageName);
                    this.ListView.SmallImageList.Images.Add(imageName,
                        composite.Clone(r, composite.PixelFormat));
                    count++;
                    if (count >= maximumImages)
                        break;
                    r.X += imageSize.Width;
                }
                r.Y += imageSize.Height;
            }
        }

        #region Implementation

        /// <summary>
        /// Where in the animatation cycle is the given model?
        /// If the given model has not started animating, it begins at position 1
        /// (not position 0, which is used for non-animating models)
        /// </summary>
        /// <param name="model"></param>
        /// <returns>The models current position in the animation cycle</returns>
        private int GetAnimationIndex(object model) {
            if (!this.animationIndexMap.ContainsKey(model))
                this.animationIndexMap[model] = 1;

            return this.animationIndexMap[model];
        }
        private Dictionary<object, int> animationIndexMap = new Dictionary<object, int>();

        /// <summary>
        /// Return true if the given model is being animated
        /// </summary>
        /// <param name="model">The model to consider</param>
        /// <returns>Is this model showing an animated image</returns>
        private bool IsAnimating(object model) {
            if (this.IsAnimatingGetter == null)
                return false;
            else
                return this.IsAnimatingGetter(model);
        }

        #endregion

        #region Tickler implementation

        /// <summary>
        /// Start the animations rolling
        /// </summary>
        public void Start() {
            // Create a timer that will fire every MillisecondsBetweenAnimations.
            // By setting SynchronizingObject, the timer will invoke the elapsed event
            // on the UI thread.
            if (this.tickler == null) {
                this.tickler = new System.Timers.Timer(this.MillisecondsBetweenAnimations);
                this.tickler.SynchronizingObject = this.ListView;
                this.tickler.Elapsed += new System.Timers.ElapsedEventHandler(tickler_Elapsed);
            }

            this.tickler.Start();
        }
        private System.Timers.Timer tickler;

        /// <summary>
        /// Stop the animations
        /// </summary>
        public void Stop() {
            if (this.tickler != null) {
                this.tickler.Stop();
            }
        }

        void tickler_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
            // Handle death/destruction/hiding
            if (this.Column == null ||
                this.ListView == null ||
                this.ListView.IsDisposed ||
                !this.Column.IsVisible ||
                this.ListView.View != System.Windows.Forms.View.Details)
                return;

            Rectangle updateRect = new Rectangle();
            List<object> objectsToRemove = new List<object>();

            // Run through each visible row, checking to see if it should be animated
            int topIndex = this.ListView.TopItemIndex;
            int bottomIndex = topIndex + this.ListView.RowsPerPage;
            for (int i = topIndex; i <= bottomIndex; i++) {
                OLVListItem olvi = this.ListView.GetItem(i);
                if (olvi == null)
                    continue;

                // Has this row been animating?
                object animatingModel = olvi.RowObject;
                if (!this.animationIndexMap.ContainsKey(olvi.RowObject))
                    continue;

                // Collect the area that must be redraw
                OLVListSubItem subItem = (OLVListSubItem)olvi.SubItems[this.Column.Index];
                if (updateRect.IsEmpty)
                    updateRect = subItem.Bounds;
                else
                    updateRect = Rectangle.Union(updateRect, subItem.Bounds);

                // If it is still animating, advance to the next image, wrapping
                // at the end. If it is no longer animating, remove it.
                if (this.IsAnimating(animatingModel)) {
                    int newIndex = this.GetAnimationIndex(animatingModel) + 1;
                    if (newIndex >= this.ImageNames.Count - 1)
                        newIndex = 1;
                    this.animationIndexMap[animatingModel] = newIndex;

                    // For virtual lists, it is enough to redraw the cell, 
                    // but for non-owner drawn, we have to be more forceful
                    if (!this.ListView.VirtualMode) {
                        subItem.ImageSelector = column.GetImage(animatingModel);
                        this.ListView.SetSubItemImage(olvi.Index, this.Column.Index, subItem, false);
                    }
                } else
                    objectsToRemove.Add(animatingModel);
            }

            // Get rid of models that are no longer animating
            foreach (object key in objectsToRemove)
                this.animationIndexMap.Remove(key);

            // Force the listview to redraw the animated images
            if (!updateRect.IsEmpty)
                this.ListView.Invalidate(updateRect);
        }

        #endregion
    }
}
