/*
 * This WaitCursor is a mind-boggling simple class to stop me
 * having to use so many try...finally blocks in my code.
 *
 * Author: Phillip Piper
 * Date: 28/02/2009 11:00 am
 *
 * CHANGE LOG:
 * when who what
 * 2009-02-28  JPP  Initial Version
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace LyricsFetcher {
    /// <summary>
    /// Create one of these in a using statement to have a wait cursor
    /// while the block executes
    /// </summary>
    public class WaitCursor : IDisposable {
        /// <summary>
        /// Create a WaitCursor with the normal wait cursor
        /// </summary>
        public WaitCursor() : this(Cursors.WaitCursor) {
        }

        /// <summary>
        /// Create a WaitCursor with the given cursor
        /// </summary>
        /// <param name="cursor"></param>
        public WaitCursor(Cursor cursor) {
            Cursor.Current = cursor;
        }

        /// <summary>
        /// Revert to the normal cursor on disposal
        /// </summary>
        public void Dispose() {
            Cursor.Current = Cursors.Default;
        }
    }
}
