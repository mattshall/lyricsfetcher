namespace LyricsFetcher
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemChooseLibrary = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemReloadLibrary = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDiscardCache = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutLyricsFetcherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.buttonStop = new System.Windows.Forms.Button();
            this.imageList24 = new System.Windows.Forms.ImageList(this.components);
            this.labelFetchStatus = new System.Windows.Forms.Label();
            this.buttonSelectNone = new System.Windows.Forms.Button();
            this.imageList22 = new System.Windows.Forms.ImageList(this.components);
            this.buttonSelectAll = new System.Windows.Forms.Button();
            this.buttonSelectMissing = new System.Windows.Forms.Button();
            this.buttonSelectUntried = new System.Windows.Forms.Button();
            this.buttonFetch = new System.Windows.Forms.Button();
            this.imageList32 = new System.Windows.Forms.ImageList(this.components);
            this.olvSongs = new BrightIdeasSoftware.FastObjectListView();
            this.olvColumnTitle = new BrightIdeasSoftware.OLVColumn();
            this.olvColumnArtist = new BrightIdeasSoftware.OLVColumn();
            this.olvColumnAlbum = new BrightIdeasSoftware.OLVColumn();
            this.olvColumnGenre = new BrightIdeasSoftware.OLVColumn();
            this.olvColumnLyricsStatus = new BrightIdeasSoftware.OLVColumn();
            this.imageList16 = new System.Windows.Forms.ImageList(this.components);
            this.buttonSearch = new System.Windows.Forms.Button();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxArtist = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.textBoxLyrics = new System.Windows.Forms.TextBox();
            this.textBoxAlbum = new System.Windows.Forms.TextBox();
            this.textBoxGenre = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvSongs)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.statusStrip1);
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(780, 562);
            this.panel1.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 540);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(780, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(640, 17);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.ForeColor = System.Drawing.Color.Red;
            this.toolStripStatusLabel2.Image = global::LyricsFetcher.Properties.Resources.burn16;
            this.toolStripStatusLabel2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(125, 17);
            this.toolStripStatusLabel2.Text = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolStripStatusLabel2.ToolTipText = "When the network is not available, lyrics cannot be fetched";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(780, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemChooseLibrary,
            this.toolStripMenuItemReloadLibrary,
            this.toolStripMenuItemDiscardCache,
            this.toolStripSeparator1,
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // toolStripMenuItemChooseLibrary
            // 
            this.toolStripMenuItemChooseLibrary.Name = "toolStripMenuItemChooseLibrary";
            this.toolStripMenuItemChooseLibrary.Size = new System.Drawing.Size(195, 22);
            this.toolStripMenuItemChooseLibrary.Text = "Choose Library...";
            this.toolStripMenuItemChooseLibrary.Click += new System.EventHandler(this.chooseLibraryToolStripMenuItem_Click);
            // 
            // toolStripMenuItemReloadLibrary
            // 
            this.toolStripMenuItemReloadLibrary.Name = "toolStripMenuItemReloadLibrary";
            this.toolStripMenuItemReloadLibrary.Size = new System.Drawing.Size(195, 22);
            this.toolStripMenuItemReloadLibrary.Text = "Reload Library";
            this.toolStripMenuItemReloadLibrary.Click += new System.EventHandler(this.toolStripMenuItemReloadLibrary_Click);
            // 
            // toolStripMenuItemDiscardCache
            // 
            this.toolStripMenuItemDiscardCache.Name = "toolStripMenuItemDiscardCache";
            this.toolStripMenuItemDiscardCache.Size = new System.Drawing.Size(195, 22);
            this.toolStripMenuItemDiscardCache.Text = "Discard Lyrics Cache...";
            this.toolStripMenuItemDiscardCache.Click += new System.EventHandler(this.toolStripMenuItemDiscardCache_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(192, 6);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutLyricsFetcherToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutLyricsFetcherToolStripMenuItem
            // 
            this.aboutLyricsFetcherToolStripMenuItem.Name = "aboutLyricsFetcherToolStripMenuItem";
            this.aboutLyricsFetcherToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.aboutLyricsFetcherToolStripMenuItem.Text = "About Lyrics Fetcher...";
            this.aboutLyricsFetcherToolStripMenuItem.Click += new System.EventHandler(this.aboutLyricsFetcherToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.buttonStop);
            this.splitContainer1.Panel1.Controls.Add(this.labelFetchStatus);
            this.splitContainer1.Panel1.Controls.Add(this.buttonSelectNone);
            this.splitContainer1.Panel1.Controls.Add(this.buttonSelectAll);
            this.splitContainer1.Panel1.Controls.Add(this.buttonSelectMissing);
            this.splitContainer1.Panel1.Controls.Add(this.buttonSelectUntried);
            this.splitContainer1.Panel1.Controls.Add(this.buttonFetch);
            this.splitContainer1.Panel1.Controls.Add(this.olvSongs);
            this.splitContainer1.Panel1.Margin = new System.Windows.Forms.Padding(3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.DarkGray;
            this.splitContainer1.Panel2.Controls.Add(this.buttonSearch);
            this.splitContainer1.Panel2.Controls.Add(this.buttonPlay);
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer1.Panel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.splitContainer1.Size = new System.Drawing.Size(779, 515);
            this.splitContainer1.SplitterDistance = 369;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.TabStop = false;
            // 
            // buttonStop
            // 
            this.buttonStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStop.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStop.ImageKey = "x";
            this.buttonStop.ImageList = this.imageList24;
            this.buttonStop.Location = new System.Drawing.Point(662, 87);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(112, 36);
            this.buttonStop.TabIndex = 7;
            this.buttonStop.Text = "Stop";
            this.buttonStop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.buttonStop, "Reload the library");
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Visible = false;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // imageList24
            // 
            this.imageList24.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList24.ImageStream")));
            this.imageList24.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList24.Images.SetKeyName(0, "close24.png");
            this.imageList24.Images.SetKeyName(1, "x");
            this.imageList24.Images.SetKeyName(2, "search");
            this.imageList24.Images.SetKeyName(3, "group");
            this.imageList24.Images.SetKeyName(4, "play");
            this.imageList24.Images.SetKeyName(5, "play2");
            this.imageList24.Images.SetKeyName(6, "stop");
            // 
            // labelFetchStatus
            // 
            this.labelFetchStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelFetchStatus.Font = new System.Drawing.Font("Lucida Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFetchStatus.Location = new System.Drawing.Point(663, 50);
            this.labelFetchStatus.Name = "labelFetchStatus";
            this.labelFetchStatus.Size = new System.Drawing.Size(113, 34);
            this.labelFetchStatus.TabIndex = 9;
            this.labelFetchStatus.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // buttonSelectNone
            // 
            this.buttonSelectNone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSelectNone.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSelectNone.ImageList = this.imageList22;
            this.buttonSelectNone.Location = new System.Drawing.Point(662, 242);
            this.buttonSelectNone.Name = "buttonSelectNone";
            this.buttonSelectNone.Size = new System.Drawing.Size(112, 29);
            this.buttonSelectNone.TabIndex = 5;
            this.buttonSelectNone.Text = "Select &None";
            this.buttonSelectNone.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.buttonSelectNone, "Deselect all songs");
            this.buttonSelectNone.UseVisualStyleBackColor = true;
            this.buttonSelectNone.Click += new System.EventHandler(this.buttonSelectNone_Click);
            // 
            // imageList22
            // 
            this.imageList22.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList22.ImageStream")));
            this.imageList22.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList22.Images.SetKeyName(0, "network-offline");
            this.imageList22.Images.SetKeyName(1, "stop");
            this.imageList22.Images.SetKeyName(2, "refresh");
            this.imageList22.Images.SetKeyName(3, "search");
            this.imageList22.Images.SetKeyName(4, "play");
            this.imageList22.Images.SetKeyName(5, "group");
            this.imageList22.Images.SetKeyName(6, "music");
            // 
            // buttonSelectAll
            // 
            this.buttonSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSelectAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSelectAll.ImageList = this.imageList22;
            this.buttonSelectAll.Location = new System.Drawing.Point(662, 207);
            this.buttonSelectAll.Name = "buttonSelectAll";
            this.buttonSelectAll.Size = new System.Drawing.Size(112, 29);
            this.buttonSelectAll.TabIndex = 4;
            this.buttonSelectAll.Text = "Select &All";
            this.buttonSelectAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.buttonSelectAll, "Select all songs");
            this.buttonSelectAll.UseVisualStyleBackColor = true;
            this.buttonSelectAll.Click += new System.EventHandler(this.buttonSelectAll_Click);
            // 
            // buttonSelectMissing
            // 
            this.buttonSelectMissing.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSelectMissing.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSelectMissing.ImageList = this.imageList22;
            this.buttonSelectMissing.Location = new System.Drawing.Point(662, 172);
            this.buttonSelectMissing.Name = "buttonSelectMissing";
            this.buttonSelectMissing.Size = new System.Drawing.Size(112, 29);
            this.buttonSelectMissing.TabIndex = 3;
            this.buttonSelectMissing.Text = "Select &Missing";
            this.buttonSelectMissing.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.buttonSelectMissing, "Select songs that do not have  lyrics");
            this.buttonSelectMissing.UseVisualStyleBackColor = true;
            this.buttonSelectMissing.Click += new System.EventHandler(this.buttonSelectMissing_Click);
            // 
            // buttonSelectUntried
            // 
            this.buttonSelectUntried.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSelectUntried.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSelectUntried.ImageList = this.imageList22;
            this.buttonSelectUntried.Location = new System.Drawing.Point(662, 137);
            this.buttonSelectUntried.Name = "buttonSelectUntried";
            this.buttonSelectUntried.Size = new System.Drawing.Size(112, 29);
            this.buttonSelectUntried.TabIndex = 2;
            this.buttonSelectUntried.Text = "Select &Untried";
            this.buttonSelectUntried.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.buttonSelectUntried, "Select songs for which no attempt has been made to find their lyrics");
            this.buttonSelectUntried.UseVisualStyleBackColor = true;
            this.buttonSelectUntried.Click += new System.EventHandler(this.buttonSelectUntried_Click);
            // 
            // buttonFetch
            // 
            this.buttonFetch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonFetch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonFetch.ImageKey = "fetch";
            this.buttonFetch.ImageList = this.imageList32;
            this.buttonFetch.Location = new System.Drawing.Point(662, 3);
            this.buttonFetch.Name = "buttonFetch";
            this.buttonFetch.Size = new System.Drawing.Size(112, 45);
            this.buttonFetch.TabIndex = 1;
            this.buttonFetch.Text = "&Fetch";
            this.buttonFetch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.buttonFetch, "Fetch the lyrics for the selected songs");
            this.buttonFetch.UseVisualStyleBackColor = true;
            this.buttonFetch.Click += new System.EventHandler(this.buttonFetch_Click);
            // 
            // imageList32
            // 
            this.imageList32.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList32.ImageStream")));
            this.imageList32.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList32.Images.SetKeyName(0, "trebleclef");
            this.imageList32.Images.SetKeyName(1, "fetch");
            this.imageList32.Images.SetKeyName(2, "stop");
            // 
            // olvSongs
            // 
            this.olvSongs.AllColumns.Add(this.olvColumnTitle);
            this.olvSongs.AllColumns.Add(this.olvColumnArtist);
            this.olvSongs.AllColumns.Add(this.olvColumnAlbum);
            this.olvSongs.AllColumns.Add(this.olvColumnGenre);
            this.olvSongs.AllColumns.Add(this.olvColumnLyricsStatus);
            this.olvSongs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.olvSongs.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.F2Only;
            this.olvSongs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumnTitle,
            this.olvColumnArtist,
            this.olvColumnAlbum,
            this.olvColumnGenre,
            this.olvColumnLyricsStatus});
            this.olvSongs.EmptyListMsg = "The library is empty";
            this.olvSongs.EmptyListMsgFont = new System.Drawing.Font("Lucida Sans", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.olvSongs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.olvSongs.FullRowSelect = true;
            this.olvSongs.GroupWithItemCountFormat = "{0} ({1} songs)";
            this.olvSongs.GroupWithItemCountSingularFormat = "{0} (only {1} song)";
            this.olvSongs.HideSelection = false;
            this.olvSongs.ItemRenderer = null;
            this.olvSongs.Location = new System.Drawing.Point(3, 3);
            this.olvSongs.Name = "olvSongs";
            this.olvSongs.ShowGroups = false;
            this.olvSongs.ShowImagesOnSubItems = true;
            this.olvSongs.ShowItemCountOnGroups = true;
            this.olvSongs.ShowItemToolTips = true;
            this.olvSongs.Size = new System.Drawing.Size(651, 362);
            this.olvSongs.SmallImageList = this.imageList16;
            this.olvSongs.TabIndex = 0;
            this.olvSongs.UseAlternatingBackColors = true;
            this.olvSongs.UseCompatibleStateImageBehavior = false;
            this.olvSongs.View = System.Windows.Forms.View.Details;
            this.olvSongs.VirtualMode = true;
            this.olvSongs.SelectionChanged += new System.EventHandler(this.olvSongs_SelectionChanged);
            // 
            // olvColumnTitle
            // 
            this.olvColumnTitle.AspectName = "Title";
            this.olvColumnTitle.Text = "Title";
            this.olvColumnTitle.UseInitialLetterForGroup = true;
            this.olvColumnTitle.Width = 143;
            // 
            // olvColumnArtist
            // 
            this.olvColumnArtist.AspectName = "Artist";
            this.olvColumnArtist.Text = "Artist";
            this.olvColumnArtist.Width = 132;
            // 
            // olvColumnAlbum
            // 
            this.olvColumnAlbum.AspectName = "Album";
            this.olvColumnAlbum.Text = "Album";
            this.olvColumnAlbum.Width = 128;
            // 
            // olvColumnGenre
            // 
            this.olvColumnGenre.AspectName = "Genre";
            this.olvColumnGenre.Text = "Genre";
            this.olvColumnGenre.Width = 110;
            // 
            // olvColumnLyricsStatus
            // 
            this.olvColumnLyricsStatus.AspectName = "LyricsStatusString";
            this.olvColumnLyricsStatus.FillsFreeSpace = true;
            this.olvColumnLyricsStatus.IsEditable = false;
            this.olvColumnLyricsStatus.Text = "Lyrics Status";
            // 
            // imageList16
            // 
            this.imageList16.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList16.ImageStream")));
            this.imageList16.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList16.Images.SetKeyName(0, "music");
            this.imageList16.Images.SetKeyName(1, "group");
            // 
            // buttonSearch
            // 
            this.buttonSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSearch.ImageKey = "search";
            this.buttonSearch.ImageList = this.imageList24;
            this.buttonSearch.Location = new System.Drawing.Point(662, 47);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(112, 36);
            this.buttonSearch.TabIndex = 2;
            this.buttonSearch.Text = "&Search...";
            this.buttonSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.buttonSearch, "Search the Web for lyrics for this song");
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // buttonPlay
            // 
            this.buttonPlay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPlay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPlay.ImageKey = "play";
            this.buttonPlay.ImageList = this.imageList24;
            this.buttonPlay.Location = new System.Drawing.Point(662, 6);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(112, 36);
            this.buttonPlay.TabIndex = 1;
            this.buttonPlay.Text = "&Play";
            this.buttonPlay.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.buttonPlay, "Play this song");
            this.buttonPlay.UseVisualStyleBackColor = true;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.textBoxArtist, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.textBoxTitle, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBoxLyrics, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBoxAlbum, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBoxGenre, 1, 3);
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 3);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(654, 136);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // textBoxArtist
            // 
            this.textBoxArtist.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxArtist.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxArtist.Location = new System.Drawing.Point(48, 29);
            this.textBoxArtist.Name = "textBoxArtist";
            this.textBoxArtist.Size = new System.Drawing.Size(271, 20);
            this.textBoxArtist.TabIndex = 3;
            this.textBoxArtist.Tag = "Artist";
            this.textBoxArtist.Validated += new System.EventHandler(this.textBox_Validated);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(337, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(15, 0, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 26);
            this.label2.TabIndex = 8;
            this.label2.Text = "&Lyrics:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "&Title:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 26);
            this.label3.TabIndex = 2;
            this.label3.Text = "A&rtist:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 26);
            this.label4.TabIndex = 4;
            this.label4.Text = "Al&bum:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 84);
            this.label5.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 20);
            this.label5.TabIndex = 6;
            this.label5.Text = "&Genre:";
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxTitle.Location = new System.Drawing.Point(48, 3);
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.Size = new System.Drawing.Size(271, 20);
            this.textBoxTitle.TabIndex = 1;
            this.textBoxTitle.Tag = "Title";
            this.textBoxTitle.Validated += new System.EventHandler(this.textBox_Validated);
            // 
            // textBoxLyrics
            // 
            this.textBoxLyrics.AcceptsReturn = true;
            this.textBoxLyrics.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLyrics.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLyrics.Location = new System.Drawing.Point(380, 3);
            this.textBoxLyrics.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.textBoxLyrics.Multiline = true;
            this.textBoxLyrics.Name = "textBoxLyrics";
            this.tableLayoutPanel1.SetRowSpan(this.textBoxLyrics, 5);
            this.textBoxLyrics.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLyrics.Size = new System.Drawing.Size(274, 130);
            this.textBoxLyrics.TabIndex = 9;
            this.textBoxLyrics.Tag = "Lyrics";
            this.textBoxLyrics.Validated += new System.EventHandler(this.textBox_Validated);
            // 
            // textBoxAlbum
            // 
            this.textBoxAlbum.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAlbum.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxAlbum.Location = new System.Drawing.Point(48, 55);
            this.textBoxAlbum.Name = "textBoxAlbum";
            this.textBoxAlbum.Size = new System.Drawing.Size(271, 20);
            this.textBoxAlbum.TabIndex = 5;
            this.textBoxAlbum.Tag = "Album";
            this.textBoxAlbum.Validated += new System.EventHandler(this.textBox_Validated);
            // 
            // textBoxGenre
            // 
            this.textBoxGenre.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxGenre.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxGenre.Location = new System.Drawing.Point(48, 81);
            this.textBoxGenre.Name = "textBoxGenre";
            this.textBoxGenre.Size = new System.Drawing.Size(271, 20);
            this.textBoxGenre.TabIndex = 7;
            this.textBoxGenre.Tag = "Genre";
            this.textBoxGenre.Validated += new System.EventHandler(this.textBox_Validated);
            // 
            // Form1
            // 
            this.AcceptButton = this.buttonFetch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.CancelButton = this.buttonStop;
            this.ClientSize = new System.Drawing.Size(780, 562);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Lyrics Fetcher";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.Form1_Layout);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.olvSongs)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button buttonSelectUntried;
        private System.Windows.Forms.Button buttonFetch;
        private BrightIdeasSoftware.FastObjectListView olvSongs;
        private System.Windows.Forms.Button buttonSelectNone;
        private System.Windows.Forms.Button buttonSelectAll;
        private System.Windows.Forms.Button buttonSelectMissing;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.TextBox textBoxArtist;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxTitle;
        private System.Windows.Forms.TextBox textBoxLyrics;
        private System.Windows.Forms.TextBox textBoxAlbum;
        private System.Windows.Forms.TextBox textBoxGenre;
        private BrightIdeasSoftware.OLVColumn olvColumnTitle;
        private BrightIdeasSoftware.OLVColumn olvColumnArtist;
        private BrightIdeasSoftware.OLVColumn olvColumnAlbum;
        private BrightIdeasSoftware.OLVColumn olvColumnLyricsStatus;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemChooseLibrary;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutLyricsFetcherToolStripMenuItem;
        private System.Windows.Forms.Label labelFetchStatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ImageList imageList24;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDiscardCache;
        private System.Windows.Forms.ImageList imageList22;
        private System.Windows.Forms.Button buttonStop;
        private BrightIdeasSoftware.OLVColumn olvColumnGenre;
        private System.Windows.Forms.ImageList imageList32;
        private System.Windows.Forms.ImageList imageList16;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemReloadLibrary;
    }
}

