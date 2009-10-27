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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.buttonMetaData = new System.Windows.Forms.Button();
            this.imageList16 = new System.Windows.Forms.ImageList(this.components);
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
            this.panel3 = new System.Windows.Forms.Panel();
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
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.checkBoxFetchAfterAccept = new System.Windows.Forms.CheckBox();
            this.buttonReject = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonShowMissingDetails = new System.Windows.Forms.RadioButton();
            this.radioButtonShowWithoutLyrics = new System.Windows.Forms.RadioButton();
            this.radioButtonShowAll = new System.Windows.Forms.RadioButton();
            this.buttonMdLookup = new System.Windows.Forms.Button();
            this.olvMetaData = new BrightIdeasSoftware.FastObjectListView();
            this.olvMdTitle = new BrightIdeasSoftware.OLVColumn();
            this.olvMdArtist = new BrightIdeasSoftware.OLVColumn();
            this.olvMdAlbum = new BrightIdeasSoftware.OLVColumn();
            this.olvMdGenre = new BrightIdeasSoftware.OLVColumn();
            this.olvMdLyricsStatus = new BrightIdeasSoftware.OLVColumn();
            this.olvMdStatus = new BrightIdeasSoftware.OLVColumn();
            this.buttonAccept = new System.Windows.Forms.Button();
            this.labelStatus = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panelProposedDetails = new System.Windows.Forms.TableLayoutPanel();
            this.label20 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.labelArtist = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelTitleProposed = new System.Windows.Forms.Label();
            this.labelArtistProposed = new System.Windows.Forms.Label();
            this.labelGenreProposed = new System.Windows.Forms.Label();
            this.labelGenre = new System.Windows.Forms.Label();
            this.labelProposed = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonMdPlay = new System.Windows.Forms.Button();
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
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.olvColumnLyrics = new BrightIdeasSoftware.OLVColumn();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvSongs)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvMetaData)).BeginInit();
            this.panelProposedDetails.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Controls.Add(this.statusStrip1);
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(734, 511);
            this.panel1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.ImageList = this.imageList16;
            this.tabControl1.Location = new System.Drawing.Point(3, 27);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(728, 459);
            this.tabControl1.TabIndex = 3;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.ImageKey = "music";
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(720, 430);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Lyrics";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.Panel1.Controls.Add(this.buttonMetaData);
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
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.Panel2.Controls.Add(this.panel3);
            this.splitContainer1.Panel2.Controls.Add(this.buttonSearch);
            this.splitContainer1.Panel2.Controls.Add(this.buttonPlay);
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer1.Panel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.splitContainer1.Size = new System.Drawing.Size(720, 430);
            this.splitContainer1.SplitterDistance = 307;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.TabStop = false;
            // 
            // buttonMetaData
            // 
            this.buttonMetaData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMetaData.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMetaData.ImageKey = "edit";
            this.buttonMetaData.ImageList = this.imageList16;
            this.buttonMetaData.Location = new System.Drawing.Point(600, 271);
            this.buttonMetaData.Name = "buttonMetaData";
            this.buttonMetaData.Size = new System.Drawing.Size(112, 32);
            this.buttonMetaData.TabIndex = 10;
            this.buttonMetaData.Text = "MetaData...";
            this.buttonMetaData.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.buttonMetaData, "Try to find the title and artist of the selected songs\r\n(Hold SHIFT to not switch" +
                    " to the MetaData tab)");
            this.buttonMetaData.UseVisualStyleBackColor = true;
            this.buttonMetaData.Click += new System.EventHandler(this.buttonMetaData_Click);
            // 
            // imageList16
            // 
            this.imageList16.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList16.ImageStream")));
            this.imageList16.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList16.Images.SetKeyName(0, "music");
            this.imageList16.Images.SetKeyName(1, "group");
            this.imageList16.Images.SetKeyName(2, "edit");
            // 
            // buttonStop
            // 
            this.buttonStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStop.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonStop.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStop.ImageKey = "x";
            this.buttonStop.ImageList = this.imageList24;
            this.buttonStop.Location = new System.Drawing.Point(600, 87);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(112, 32);
            this.buttonStop.TabIndex = 7;
            this.buttonStop.Text = "Stop";
            this.buttonStop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.buttonStop, "Stop loading the library");
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
            this.imageList24.Images.SetKeyName(7, "accept");
            // 
            // labelFetchStatus
            // 
            this.labelFetchStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelFetchStatus.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFetchStatus.Location = new System.Drawing.Point(600, 50);
            this.labelFetchStatus.Name = "labelFetchStatus";
            this.labelFetchStatus.Size = new System.Drawing.Size(113, 34);
            this.labelFetchStatus.TabIndex = 9;
            this.labelFetchStatus.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // buttonSelectNone
            // 
            this.buttonSelectNone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSelectNone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSelectNone.ImageList = this.imageList22;
            this.buttonSelectNone.Location = new System.Drawing.Point(600, 215);
            this.buttonSelectNone.Name = "buttonSelectNone";
            this.buttonSelectNone.Size = new System.Drawing.Size(112, 24);
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
            this.buttonSelectAll.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSelectAll.ImageList = this.imageList22;
            this.buttonSelectAll.Location = new System.Drawing.Point(600, 185);
            this.buttonSelectAll.Name = "buttonSelectAll";
            this.buttonSelectAll.Size = new System.Drawing.Size(112, 24);
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
            this.buttonSelectMissing.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSelectMissing.ImageList = this.imageList22;
            this.buttonSelectMissing.Location = new System.Drawing.Point(600, 155);
            this.buttonSelectMissing.Name = "buttonSelectMissing";
            this.buttonSelectMissing.Size = new System.Drawing.Size(112, 24);
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
            this.buttonSelectUntried.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSelectUntried.ImageList = this.imageList22;
            this.buttonSelectUntried.Location = new System.Drawing.Point(600, 125);
            this.buttonSelectUntried.Name = "buttonSelectUntried";
            this.buttonSelectUntried.Size = new System.Drawing.Size(112, 24);
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
            this.buttonFetch.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonFetch.ImageKey = "fetch";
            this.buttonFetch.ImageList = this.imageList32;
            this.buttonFetch.Location = new System.Drawing.Point(600, 3);
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
            this.imageList32.Images.SetKeyName(3, "database");
            // 
            // olvSongs
            // 
            this.olvSongs.AllColumns.Add(this.olvColumnTitle);
            this.olvSongs.AllColumns.Add(this.olvColumnArtist);
            this.olvSongs.AllColumns.Add(this.olvColumnAlbum);
            this.olvSongs.AllColumns.Add(this.olvColumnGenre);
            this.olvSongs.AllColumns.Add(this.olvColumnLyrics);
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
            this.olvSongs.EmptyListMsgFont = new System.Drawing.Font("Tahoma", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.olvSongs.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.olvSongs.FullRowSelect = true;
            this.olvSongs.GroupWithItemCountFormat = "{0} ({1} songs)";
            this.olvSongs.GroupWithItemCountSingularFormat = "{0} (only {1} song)";
            this.olvSongs.HideSelection = false;
            this.olvSongs.ItemRenderer = null;
            this.olvSongs.Location = new System.Drawing.Point(0, 3);
            this.olvSongs.Name = "olvSongs";
            this.olvSongs.ShowGroups = false;
            this.olvSongs.ShowImagesOnSubItems = true;
            this.olvSongs.ShowItemCountOnGroups = true;
            this.olvSongs.ShowItemToolTips = true;
            this.olvSongs.Size = new System.Drawing.Size(592, 300);
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
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.MediumBlue;
            this.panel3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(731, 1);
            this.panel3.TabIndex = 7;
            // 
            // buttonSearch
            // 
            this.buttonSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSearch.ImageKey = "search";
            this.buttonSearch.ImageList = this.imageList24;
            this.buttonSearch.Location = new System.Drawing.Point(600, 44);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(112, 32);
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
            this.buttonPlay.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPlay.ImageKey = "play";
            this.buttonPlay.ImageList = this.imageList24;
            this.buttonPlay.Location = new System.Drawing.Point(600, 6);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(112, 32);
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
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 6);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(595, 113);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // textBoxArtist
            // 
            this.textBoxArtist.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxArtist.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxArtist.Location = new System.Drawing.Point(55, 30);
            this.textBoxArtist.Name = "textBoxArtist";
            this.textBoxArtist.Size = new System.Drawing.Size(235, 21);
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
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.MediumBlue;
            this.label2.Location = new System.Drawing.Point(308, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(15, 0, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 27);
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
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.MediumBlue;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 27);
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
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.MediumBlue;
            this.label3.Location = new System.Drawing.Point(3, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 27);
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
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.MediumBlue;
            this.label4.Location = new System.Drawing.Point(3, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 27);
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
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.MediumBlue;
            this.label5.Location = new System.Drawing.Point(3, 87);
            this.label5.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 21);
            this.label5.TabIndex = 6;
            this.label5.Text = "&Genre:";
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTitle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxTitle.Location = new System.Drawing.Point(55, 3);
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.Size = new System.Drawing.Size(235, 21);
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
            this.textBoxLyrics.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLyrics.Location = new System.Drawing.Point(357, 3);
            this.textBoxLyrics.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.textBoxLyrics.Multiline = true;
            this.textBoxLyrics.Name = "textBoxLyrics";
            this.tableLayoutPanel1.SetRowSpan(this.textBoxLyrics, 5);
            this.textBoxLyrics.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLyrics.Size = new System.Drawing.Size(238, 107);
            this.textBoxLyrics.TabIndex = 9;
            this.textBoxLyrics.Tag = "Lyrics";
            this.textBoxLyrics.Validated += new System.EventHandler(this.textBox_Validated);
            // 
            // textBoxAlbum
            // 
            this.textBoxAlbum.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAlbum.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxAlbum.Location = new System.Drawing.Point(55, 57);
            this.textBoxAlbum.Name = "textBoxAlbum";
            this.textBoxAlbum.Size = new System.Drawing.Size(235, 21);
            this.textBoxAlbum.TabIndex = 5;
            this.textBoxAlbum.Tag = "Album";
            this.textBoxAlbum.Validated += new System.EventHandler(this.textBox_Validated);
            // 
            // textBoxGenre
            // 
            this.textBoxGenre.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxGenre.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxGenre.Location = new System.Drawing.Point(55, 84);
            this.textBoxGenre.Name = "textBoxGenre";
            this.textBoxGenre.Size = new System.Drawing.Size(235, 21);
            this.textBoxGenre.TabIndex = 7;
            this.textBoxGenre.Tag = "Genre";
            this.textBoxGenre.Validated += new System.EventHandler(this.textBox_Validated);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer2);
            this.tabPage2.ImageKey = "edit";
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(720, 430);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "MetaData";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.checkBoxFetchAfterAccept);
            this.splitContainer2.Panel1.Controls.Add(this.buttonReject);
            this.splitContainer2.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer2.Panel1.Controls.Add(this.buttonMdLookup);
            this.splitContainer2.Panel1.Controls.Add(this.olvMetaData);
            this.splitContainer2.Panel1.Controls.Add(this.buttonAccept);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.labelStatus);
            this.splitContainer2.Panel2.Controls.Add(this.label6);
            this.splitContainer2.Panel2.Controls.Add(this.panelProposedDetails);
            this.splitContainer2.Panel2.Controls.Add(this.panel2);
            this.splitContainer2.Panel2.Controls.Add(this.buttonMdPlay);
            this.splitContainer2.Size = new System.Drawing.Size(717, 427);
            this.splitContainer2.SplitterDistance = 310;
            this.splitContainer2.TabIndex = 0;
            // 
            // checkBoxFetchAfterAccept
            // 
            this.checkBoxFetchAfterAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxFetchAfterAccept.Checked = true;
            this.checkBoxFetchAfterAccept.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxFetchAfterAccept.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxFetchAfterAccept.Location = new System.Drawing.Point(600, 272);
            this.checkBoxFetchAfterAccept.Margin = new System.Windows.Forms.Padding(0);
            this.checkBoxFetchAfterAccept.Name = "checkBoxFetchAfterAccept";
            this.checkBoxFetchAfterAccept.Size = new System.Drawing.Size(117, 34);
            this.checkBoxFetchAfterAccept.TabIndex = 13;
            this.checkBoxFetchAfterAccept.Text = "Fetch lyrics after accepting";
            this.toolTip1.SetToolTip(this.checkBoxFetchAfterAccept, "If this is true, a search will be made for lyrics for the song after a metadata c" +
                    "hange is accepted");
            this.checkBoxFetchAfterAccept.UseVisualStyleBackColor = true;
            // 
            // buttonReject
            // 
            this.buttonReject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonReject.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonReject.ImageKey = "x";
            this.buttonReject.ImageList = this.imageList24;
            this.buttonReject.Location = new System.Drawing.Point(600, 171);
            this.buttonReject.Name = "buttonReject";
            this.buttonReject.Size = new System.Drawing.Size(112, 32);
            this.buttonReject.TabIndex = 11;
            this.buttonReject.Text = "&Reject";
            this.buttonReject.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.buttonReject, "Reject the found metadata on the selected songs");
            this.buttonReject.UseVisualStyleBackColor = true;
            this.buttonReject.Click += new System.EventHandler(this.buttonReject_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.radioButtonShowMissingDetails);
            this.groupBox1.Controls.Add(this.radioButtonShowWithoutLyrics);
            this.groupBox1.Controls.Add(this.radioButtonShowAll);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(600, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(112, 76);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Show Songs";
            // 
            // radioButtonShowMissingDetails
            // 
            this.radioButtonShowMissingDetails.AutoSize = true;
            this.radioButtonShowMissingDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonShowMissingDetails.Location = new System.Drawing.Point(7, 55);
            this.radioButtonShowMissingDetails.Name = "radioButtonShowMissingDetails";
            this.radioButtonShowMissingDetails.Size = new System.Drawing.Size(100, 18);
            this.radioButtonShowMissingDetails.TabIndex = 2;
            this.radioButtonShowMissingDetails.TabStop = true;
            this.radioButtonShowMissingDetails.Text = "Missing details";
            this.radioButtonShowMissingDetails.UseVisualStyleBackColor = true;
            this.radioButtonShowMissingDetails.CheckedChanged += new System.EventHandler(this.radioButtonShowMissingDetails_CheckedChanged);
            // 
            // radioButtonShowWithoutLyrics
            // 
            this.radioButtonShowWithoutLyrics.AutoSize = true;
            this.radioButtonShowWithoutLyrics.Checked = true;
            this.radioButtonShowWithoutLyrics.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonShowWithoutLyrics.Location = new System.Drawing.Point(7, 36);
            this.radioButtonShowWithoutLyrics.Name = "radioButtonShowWithoutLyrics";
            this.radioButtonShowWithoutLyrics.Size = new System.Drawing.Size(99, 18);
            this.radioButtonShowWithoutLyrics.TabIndex = 1;
            this.radioButtonShowWithoutLyrics.TabStop = true;
            this.radioButtonShowWithoutLyrics.Text = "Without lyrics";
            this.radioButtonShowWithoutLyrics.UseVisualStyleBackColor = true;
            this.radioButtonShowWithoutLyrics.CheckedChanged += new System.EventHandler(this.radioButtonShowWithoutLyrics_CheckedChanged);
            // 
            // radioButtonShowAll
            // 
            this.radioButtonShowAll.AutoSize = true;
            this.radioButtonShowAll.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonShowAll.Location = new System.Drawing.Point(7, 17);
            this.radioButtonShowAll.Name = "radioButtonShowAll";
            this.radioButtonShowAll.Size = new System.Drawing.Size(37, 18);
            this.radioButtonShowAll.TabIndex = 0;
            this.radioButtonShowAll.TabStop = true;
            this.radioButtonShowAll.Text = "All";
            this.radioButtonShowAll.UseVisualStyleBackColor = true;
            this.radioButtonShowAll.CheckedChanged += new System.EventHandler(this.radioButtonShowAll_CheckedChanged);
            // 
            // buttonMdLookup
            // 
            this.buttonMdLookup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMdLookup.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMdLookup.ImageKey = "database";
            this.buttonMdLookup.ImageList = this.imageList32;
            this.buttonMdLookup.Location = new System.Drawing.Point(600, 82);
            this.buttonMdLookup.Name = "buttonMdLookup";
            this.buttonMdLookup.Size = new System.Drawing.Size(112, 45);
            this.buttonMdLookup.TabIndex = 8;
            this.buttonMdLookup.Text = "&Lookup";
            this.buttonMdLookup.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.buttonMdLookup, "Lookup the title and artist for the selected tracks");
            this.buttonMdLookup.UseVisualStyleBackColor = true;
            this.buttonMdLookup.Click += new System.EventHandler(this.buttonMdLookup_Click);
            // 
            // olvMetaData
            // 
            this.olvMetaData.AllColumns.Add(this.olvMdTitle);
            this.olvMetaData.AllColumns.Add(this.olvMdArtist);
            this.olvMetaData.AllColumns.Add(this.olvMdAlbum);
            this.olvMetaData.AllColumns.Add(this.olvMdGenre);
            this.olvMetaData.AllColumns.Add(this.olvMdLyricsStatus);
            this.olvMetaData.AllColumns.Add(this.olvMdStatus);
            this.olvMetaData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.olvMetaData.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.F2Only;
            this.olvMetaData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvMdTitle,
            this.olvMdArtist,
            this.olvMdLyricsStatus,
            this.olvMdStatus});
            this.olvMetaData.EmptyListMsg = "No songs to show";
            this.olvMetaData.EmptyListMsgFont = new System.Drawing.Font("Tahoma", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.olvMetaData.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.olvMetaData.FullRowSelect = true;
            this.olvMetaData.GroupWithItemCountFormat = "{0} ({1} songs)";
            this.olvMetaData.GroupWithItemCountSingularFormat = "{0} (only {1} song)";
            this.olvMetaData.HideSelection = false;
            this.olvMetaData.ItemRenderer = null;
            this.olvMetaData.Location = new System.Drawing.Point(0, 3);
            this.olvMetaData.Name = "olvMetaData";
            this.olvMetaData.ShowGroups = false;
            this.olvMetaData.ShowImagesOnSubItems = true;
            this.olvMetaData.ShowItemCountOnGroups = true;
            this.olvMetaData.ShowItemToolTips = true;
            this.olvMetaData.Size = new System.Drawing.Size(589, 303);
            this.olvMetaData.SmallImageList = this.imageList16;
            this.olvMetaData.TabIndex = 1;
            this.olvMetaData.UseAlternatingBackColors = true;
            this.olvMetaData.UseCompatibleStateImageBehavior = false;
            this.olvMetaData.View = System.Windows.Forms.View.Details;
            this.olvMetaData.VirtualMode = true;
            this.olvMetaData.SelectionChanged += new System.EventHandler(this.olvMetaData_SelectionChanged);
            // 
            // olvMdTitle
            // 
            this.olvMdTitle.AspectName = "Title";
            this.olvMdTitle.Text = "Title";
            this.olvMdTitle.UseInitialLetterForGroup = true;
            this.olvMdTitle.Width = 143;
            // 
            // olvMdArtist
            // 
            this.olvMdArtist.AspectName = "Artist";
            this.olvMdArtist.Text = "Artist";
            this.olvMdArtist.Width = 132;
            // 
            // olvMdAlbum
            // 
            this.olvMdAlbum.AspectName = "Album";
            this.olvMdAlbum.DisplayIndex = 2;
            this.olvMdAlbum.IsVisible = false;
            this.olvMdAlbum.Text = "Album";
            this.olvMdAlbum.Width = 128;
            // 
            // olvMdGenre
            // 
            this.olvMdGenre.AspectName = "Genre";
            this.olvMdGenre.DisplayIndex = 2;
            this.olvMdGenre.IsVisible = false;
            this.olvMdGenre.Text = "Genre";
            // 
            // olvMdLyricsStatus
            // 
            this.olvMdLyricsStatus.Text = "Lyrics Status";
            this.olvMdLyricsStatus.Width = 97;
            // 
            // olvMdStatus
            // 
            this.olvMdStatus.AspectName = "";
            this.olvMdStatus.FillsFreeSpace = true;
            this.olvMdStatus.IsEditable = false;
            this.olvMdStatus.Text = "MetaData Status";
            this.olvMdStatus.Width = 80;
            // 
            // buttonAccept
            // 
            this.buttonAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAccept.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAccept.ImageKey = "accept";
            this.buttonAccept.ImageList = this.imageList24;
            this.buttonAccept.Location = new System.Drawing.Point(600, 133);
            this.buttonAccept.Name = "buttonAccept";
            this.buttonAccept.Size = new System.Drawing.Size(112, 32);
            this.buttonAccept.TabIndex = 5;
            this.buttonAccept.Text = "&Accept";
            this.buttonAccept.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.buttonAccept, "Accept the found metadata on the selected songs");
            this.buttonAccept.UseVisualStyleBackColor = true;
            this.buttonAccept.Click += new System.EventHandler(this.buttonAccept_Click);
            // 
            // labelStatus
            // 
            this.labelStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelStatus.AutoEllipsis = true;
            this.labelStatus.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatus.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelStatus.Location = new System.Drawing.Point(54, 6);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(532, 30);
            this.labelStatus.TabIndex = 19;
            this.labelStatus.Text = "Failed. The media file for this song could be not found, or  it has been deleted " +
                "to make way for a hyperspace bypass";
            this.labelStatus.UseMnemonic = false;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.MediumBlue;
            this.label6.Location = new System.Drawing.Point(3, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Status:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelProposedDetails
            // 
            this.panelProposedDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelProposedDetails.ColumnCount = 4;
            this.panelProposedDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panelProposedDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.panelProposedDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panelProposedDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.panelProposedDetails.Controls.Add(this.label20, 0, 3);
            this.panelProposedDetails.Controls.Add(this.label17, 0, 2);
            this.panelProposedDetails.Controls.Add(this.labelArtist, 1, 2);
            this.panelProposedDetails.Controls.Add(this.label8, 0, 1);
            this.panelProposedDetails.Controls.Add(this.labelTitle, 1, 1);
            this.panelProposedDetails.Controls.Add(this.labelTitleProposed, 3, 1);
            this.panelProposedDetails.Controls.Add(this.labelArtistProposed, 3, 2);
            this.panelProposedDetails.Controls.Add(this.labelGenreProposed, 3, 3);
            this.panelProposedDetails.Controls.Add(this.labelGenre, 1, 3);
            this.panelProposedDetails.Controls.Add(this.labelProposed, 3, 0);
            this.panelProposedDetails.Location = new System.Drawing.Point(0, 35);
            this.panelProposedDetails.Name = "panelProposedDetails";
            this.panelProposedDetails.RowCount = 5;
            this.panelProposedDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.panelProposedDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.panelProposedDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.panelProposedDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.panelProposedDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.panelProposedDetails.Size = new System.Drawing.Size(589, 93);
            this.panelProposedDetails.TabIndex = 17;
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.MediumBlue;
            this.label20.Location = new System.Drawing.Point(3, 60);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(44, 20);
            this.label20.TabIndex = 27;
            this.label20.Text = "Genre:";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.MediumBlue;
            this.label17.Location = new System.Drawing.Point(3, 40);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(44, 20);
            this.label17.TabIndex = 21;
            this.label17.Text = "Artist:";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelArtist
            // 
            this.labelArtist.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelArtist.AutoEllipsis = true;
            this.labelArtist.AutoSize = true;
            this.labelArtist.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelArtist.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelArtist.Location = new System.Drawing.Point(53, 40);
            this.labelArtist.Name = "labelArtist";
            this.labelArtist.Size = new System.Drawing.Size(209, 20);
            this.labelArtist.TabIndex = 22;
            this.labelArtist.Text = "Switchfoot";
            this.labelArtist.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelArtist.UseMnemonic = false;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.MediumBlue;
            this.label8.Location = new System.Drawing.Point(3, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 20);
            this.label8.TabIndex = 11;
            this.label8.Text = "Title:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelTitle
            // 
            this.labelTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTitle.AutoEllipsis = true;
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelTitle.Location = new System.Drawing.Point(53, 20);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(209, 20);
            this.labelTitle.TabIndex = 18;
            this.labelTitle.Text = "Meant To Live";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelTitle.UseMnemonic = false;
            // 
            // labelTitleProposed
            // 
            this.labelTitleProposed.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTitleProposed.AutoEllipsis = true;
            this.labelTitleProposed.AutoSize = true;
            this.labelTitleProposed.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitleProposed.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelTitleProposed.Location = new System.Drawing.Point(268, 20);
            this.labelTitleProposed.Name = "labelTitleProposed";
            this.labelTitleProposed.Size = new System.Drawing.Size(318, 20);
            this.labelTitleProposed.TabIndex = 18;
            this.labelTitleProposed.Text = "Meant To Live";
            this.labelTitleProposed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelTitleProposed.UseMnemonic = false;
            // 
            // labelArtistProposed
            // 
            this.labelArtistProposed.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelArtistProposed.AutoEllipsis = true;
            this.labelArtistProposed.AutoSize = true;
            this.labelArtistProposed.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelArtistProposed.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelArtistProposed.Location = new System.Drawing.Point(268, 40);
            this.labelArtistProposed.Name = "labelArtistProposed";
            this.labelArtistProposed.Size = new System.Drawing.Size(318, 20);
            this.labelArtistProposed.TabIndex = 18;
            this.labelArtistProposed.Text = "Meant To Live";
            this.labelArtistProposed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelArtistProposed.UseMnemonic = false;
            // 
            // labelGenreProposed
            // 
            this.labelGenreProposed.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelGenreProposed.AutoEllipsis = true;
            this.labelGenreProposed.AutoSize = true;
            this.labelGenreProposed.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGenreProposed.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelGenreProposed.Location = new System.Drawing.Point(268, 60);
            this.labelGenreProposed.Name = "labelGenreProposed";
            this.labelGenreProposed.Size = new System.Drawing.Size(318, 20);
            this.labelGenreProposed.TabIndex = 18;
            this.labelGenreProposed.Text = "Meant To Live";
            this.labelGenreProposed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelGenreProposed.UseMnemonic = false;
            // 
            // labelGenre
            // 
            this.labelGenre.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelGenre.AutoEllipsis = true;
            this.labelGenre.AutoSize = true;
            this.labelGenre.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGenre.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelGenre.Location = new System.Drawing.Point(53, 60);
            this.labelGenre.Name = "labelGenre";
            this.labelGenre.Size = new System.Drawing.Size(209, 20);
            this.labelGenre.TabIndex = 28;
            this.labelGenre.Text = "Gospel & Religious";
            this.labelGenre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelGenre.UseMnemonic = false;
            // 
            // labelProposed
            // 
            this.labelProposed.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelProposed.AutoSize = true;
            this.labelProposed.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProposed.ForeColor = System.Drawing.Color.MediumBlue;
            this.labelProposed.Location = new System.Drawing.Point(268, 0);
            this.labelProposed.Name = "labelProposed";
            this.labelProposed.Size = new System.Drawing.Size(318, 20);
            this.labelProposed.TabIndex = 16;
            this.labelProposed.Text = "Proposed";
            this.labelProposed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.MediumBlue;
            this.panel2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(728, 1);
            this.panel2.TabIndex = 6;
            // 
            // buttonMdPlay
            // 
            this.buttonMdPlay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMdPlay.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMdPlay.ImageKey = "play";
            this.buttonMdPlay.ImageList = this.imageList24;
            this.buttonMdPlay.Location = new System.Drawing.Point(600, 6);
            this.buttonMdPlay.Name = "buttonMdPlay";
            this.buttonMdPlay.Size = new System.Drawing.Size(112, 32);
            this.buttonMdPlay.TabIndex = 3;
            this.buttonMdPlay.Text = "&Play";
            this.buttonMdPlay.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.buttonMdPlay, "Play this song");
            this.buttonMdPlay.UseVisualStyleBackColor = true;
            this.buttonMdPlay.Click += new System.EventHandler(this.buttonMdPlay_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 489);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(734, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(594, 17);
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
            this.menuStrip1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(734, 24);
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
            // olvColumnLyrics
            // 
            this.olvColumnLyrics.AspectName = "Lyrics";
            this.olvColumnLyrics.DisplayIndex = 4;
            this.olvColumnLyrics.IsVisible = false;
            this.olvColumnLyrics.Text = "Lyrics";
            // 
            // Form1
            // 
            this.AcceptButton = this.buttonFetch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.buttonStop;
            this.ClientSize = new System.Drawing.Size(734, 511);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.olvSongs)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvMetaData)).EndInit();
            this.panelProposedDetails.ResumeLayout(false);
            this.panelProposedDetails.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
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
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private BrightIdeasSoftware.FastObjectListView olvMetaData;
        private BrightIdeasSoftware.OLVColumn olvMdTitle;
        private BrightIdeasSoftware.OLVColumn olvMdArtist;
        private BrightIdeasSoftware.OLVColumn olvMdAlbum;
        private BrightIdeasSoftware.OLVColumn olvMdStatus;
        private System.Windows.Forms.Button buttonAccept;
        private System.Windows.Forms.Button buttonMdPlay;
        private System.Windows.Forms.Button buttonMdLookup;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TableLayoutPanel panelProposedDetails;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label labelGenre;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label labelArtist;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label labelTitleProposed;
        private System.Windows.Forms.Label labelProposed;
        private System.Windows.Forms.Label labelArtistProposed;
        private System.Windows.Forms.Label labelGenreProposed;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonShowMissingDetails;
        private System.Windows.Forms.RadioButton radioButtonShowWithoutLyrics;
        private System.Windows.Forms.RadioButton radioButtonShowAll;
        private System.Windows.Forms.Button buttonReject;
        private System.Windows.Forms.Button buttonMetaData;
        private System.Windows.Forms.CheckBox checkBoxFetchAfterAccept;
        private BrightIdeasSoftware.OLVColumn olvMdGenre;
        private BrightIdeasSoftware.OLVColumn olvMdLyricsStatus;
        private BrightIdeasSoftware.OLVColumn olvColumnLyrics;
    }
}

