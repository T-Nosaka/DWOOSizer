using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using LanzNet;

namespace DWOOSizer
{
	/// <summary>
	/// メインフォーム
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		protected System.Windows.Forms.OpenFileDialog openFileDialog1;
		protected System.Windows.Forms.MainMenu mainMenu1;
		protected System.Windows.Forms.MenuItem menuItem1;
		protected System.Windows.Forms.MenuItem menuItem2;
		protected System.Windows.Forms.MenuItem menuItem3;
		protected System.Windows.Forms.MenuItem menuItem4;
		protected System.Windows.Forms.MenuItem menuItem5;
		protected System.Windows.Forms.SaveFileDialog saveFileDialog1;
		protected System.Windows.Forms.MenuItem menuItem6;
		protected System.Windows.Forms.MenuItem menuItem7;
		protected System.Windows.Forms.MenuItem menuItem8;
		protected System.Windows.Forms.MenuItem menuItem10;
		protected System.Windows.Forms.MenuItem menuItem9;
		protected System.Windows.Forms.MenuItem menuItem11;
		protected System.Windows.Forms.MenuItem menuItem12;
		protected System.Windows.Forms.MenuItem menuItem25;
		protected System.Windows.Forms.MenuItem menuItem26;
		protected System.Windows.Forms.MenuItem menuItem27;
		protected System.Windows.Forms.MenuItem menuItem28;
		protected System.Windows.Forms.MenuItem menuItem29;
		protected System.Windows.Forms.MenuItem menuItem30;

		private IContainer components;

        /// <summary>
        /// 表示元ビットマップ
        /// </summary>
        protected Bitmap m_bitmap;

		/// <summary>
		/// サイズ更新機能付き画像コントロール
		/// </summary>
		protected SelectPictControl resizePictControl;

		/// <summary>
		/// 画像情報表示ダイアログ
		/// </summary>
		protected InfoForm m_InfoForm;

		/// <summary>
		/// 表示倍率
		/// </summary>
		protected double m_Zoom = 1.0;

		/// <summary>
		/// 画面サイズ変更にて、自動スクロールオンの再描画中フラグ
		/// </summary>
		protected bool m_bAutoScrollFirstEvent = false;

		/// <summary>
		/// ファイルフィルタの最終索引番号
		/// </summary>
		protected int m_filtercnt = 1;

		/// <summary>
		/// 手打ちの変更のイベントセット
		/// </summary>
		protected EventHandler m_pictvaluechanged = null;

		/// <summary>
		/// 閉じられる直前のイベント設定
		/// </summary>
		protected CancelEventHandler m_infoformclosing = null;

        /// <summary>
        /// サイズ変更イベントの設定
        /// </summary>
        protected EventHandler m_resizeev = null;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public MainForm()
		{
			InitializeComponent();

			//画像コントロール作成
			resizePictControl = new SelectPictControl();
			resizePictControl.Bitmap = null;
			resizePictControl.Dock = System.Windows.Forms.DockStyle.Fill;
			resizePictControl.EffectType = 0;
			resizePictControl.Location = new System.Drawing.Point(0, 0);
			resizePictControl.Name = "resizePictControl";
			resizePictControl.Selected = false;
			resizePictControl.Size = new System.Drawing.Size(224, 65);
			resizePictControl.TabIndex = 0;

			//イベント追加
			resizePictControl.Picture.DoubleClick += new EventHandler( MainPicture_DoubleClick );

			Initialize();

			m_pictvaluechanged = new EventHandler( PictValueChanged );
			m_infoformclosing = new CancelEventHandler( InfoFormClosing );
			m_resizeev = new EventHandler(this.MainForm_Resize);
			this.Resize += m_resizeev;
		}

		/// <summary>
		/// 初期処理
		/// </summary>
		protected void Initialize()
		{
			//読み込み可能画像フォーマット列挙
			ImageCodecInfo[] encoders = ImageCodecInfo.GetImageEncoders();

			string allfilter = string.Empty;
			string filter = string.Empty;
			foreach (ImageCodecInfo ici in encoders) 
			{
				if( filter.Length > 0 )
				{
					filter += "|";
					allfilter += ";";
				}

				filter += string.Format( "{0}({1})|{2}", ici.FormatDescription, ici.FilenameExtension.ToLower(), ici.FilenameExtension );
				allfilter += string.Format( "{0}",ici.FilenameExtension );

				m_filtercnt++;
			}
			openFileDialog1.Filter = string.Format("全ての形式|{0}|{1}",allfilter,filter);
			openFileDialog1.FilterIndex = 1;

			m_filtercnt++;		//全ての形式をカウント

			saveFileDialog1.Filter = filter;
			saveFileDialog1.FilterIndex = encoders.Length;
			saveFileDialog1.AddExtension = true;

			//データフォーム起動
			m_InfoForm = new InfoForm();
			m_InfoForm.ZoomEdt.ValueChanged += new System.EventHandler(ZoomEdtValueChanged);
		}

		/// <summary>
		/// 使用されているリソースに後処理を実行します。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		/// <summary>
		/// 描画コントロールプロパティ
		/// </summary>
		protected virtual ScrollableControl ClientForm
		{
			get
			{
				return this;
			}
		}

		/// <summary>
		/// 読み込み処理等が完了し、アイドルへモードを遷移するイベント
		/// </summary>
		protected virtual void OnViewStart()
		{
		}

		/// <summary>
		/// メニューの有無無効制御
		/// </summary>
		/// <param name="flg"></param>
		protected virtual void MenuEnable( )
		{
			bool bFlg = (m_bitmap == null) ? false:true;

			//保存メニューの表示
			menuItem5.Enabled = bFlg;
			//画像情報の表示
			menuItem7.Enabled = bFlg;
			//全画面の表示
			menuItem8.Enabled = bFlg;
		}

		#region Windows フォーム デザイナで生成されたコード 
		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem10 = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.menuItem11 = new System.Windows.Forms.MenuItem();
            this.menuItem26 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.menuItem25 = new System.Windows.Forms.MenuItem();
            this.menuItem27 = new System.Windows.Forms.MenuItem();
            this.menuItem30 = new System.Windows.Forms.MenuItem();
            this.menuItem28 = new System.Windows.Forms.MenuItem();
            this.menuItem29 = new System.Windows.Forms.MenuItem();
            this.menuItem12 = new System.Windows.Forms.MenuItem();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Multiselect = true;
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem10,
            this.menuItem6,
            this.menuItem25,
            this.menuItem12});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem2,
            this.menuItem5,
            this.menuItem4,
            this.menuItem3});
            this.menuItem1.Text = "ファイル";
            this.menuItem1.Popup += new System.EventHandler(this.menuItem1_Popup);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 0;
            this.menuItem2.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
            this.menuItem2.Text = "開く";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 1;
            this.menuItem5.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
            this.menuItem5.Text = "保存";
            this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 2;
            this.menuItem4.Text = "-";
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 3;
            this.menuItem3.Shortcut = System.Windows.Forms.Shortcut.CtrlX;
            this.menuItem3.Text = "終了";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // menuItem10
            // 
            this.menuItem10.Index = 1;
            this.menuItem10.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem9,
            this.menuItem11,
            this.menuItem26});
            this.menuItem10.Text = "編集";
            this.menuItem10.Popup += new System.EventHandler(this.menuItem10_Popup);
            // 
            // menuItem9
            // 
            this.menuItem9.Index = 0;
            this.menuItem9.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
            this.menuItem9.Text = "コピー";
            this.menuItem9.Click += new System.EventHandler(this.menuItem9_Click_1);
            // 
            // menuItem11
            // 
            this.menuItem11.Index = 1;
            this.menuItem11.Shortcut = System.Windows.Forms.Shortcut.CtrlV;
            this.menuItem11.Text = "ペースト";
            this.menuItem11.Click += new System.EventHandler(this.menuItem11_Click);
            // 
            // menuItem26
            // 
            this.menuItem26.Index = 2;
            this.menuItem26.Text = "選択解除";
            this.menuItem26.Click += new System.EventHandler(this.menuItem26_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 2;
            this.menuItem6.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem7,
            this.menuItem8});
            this.menuItem6.Text = "表示";
            this.menuItem6.Popup += new System.EventHandler(this.menuItem6_Popup);
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 0;
            this.menuItem7.Shortcut = System.Windows.Forms.Shortcut.CtrlI;
            this.menuItem7.Text = "画像情報";
            this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 1;
            this.menuItem8.Text = "全画面";
            this.menuItem8.Click += new System.EventHandler(this.menuItem8_Click);
            // 
            // menuItem25
            // 
            this.menuItem25.Index = 3;
            this.menuItem25.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem27,
            this.menuItem30,
            this.menuItem28,
            this.menuItem29});
            this.menuItem25.Text = "補間";
            // 
            // menuItem27
            // 
            this.menuItem27.Checked = true;
            this.menuItem27.Index = 0;
            this.menuItem27.Text = "ランチョス";
            this.menuItem27.Click += new System.EventHandler(this.menuItem27_Click);
            // 
            // menuItem30
            // 
            this.menuItem30.Index = 1;
            this.menuItem30.Text = "ニアレストネイバー";
            this.menuItem30.Click += new System.EventHandler(this.menuItem30_Click);
            // 
            // menuItem28
            // 
            this.menuItem28.Index = 2;
            this.menuItem28.Text = "バイリニア";
            this.menuItem28.Click += new System.EventHandler(this.menuItem28_Click);
            // 
            // menuItem29
            // 
            this.menuItem29.Index = 3;
            this.menuItem29.Text = "バイキュービック";
            this.menuItem29.Click += new System.EventHandler(this.menuItem29_Click);
            // 
            // menuItem12
            // 
            this.menuItem12.Index = 4;
            this.menuItem12.Text = "バージョン情報";
            this.menuItem12.Click += new System.EventHandler(this.menuItem12_Click);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
            this.ClientSize = new System.Drawing.Size(287, 0);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.mainMenu1;
            this.Name = "MainForm";
            this.Text = "DWOOSizer";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.ResumeLayout(false);

		}
		#endregion

		#region メニューイベント
		/// <summary>
		/// 読み込み処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			LoadFile();
		}

		/// <summary>
		/// 読み込み処理
		/// </summary>
		protected void LoadFile()
		{
			//ダイアログオープン
			openFileDialog1.FileName = string.Empty;
			if (openFileDialog1.ShowDialog(this) == DialogResult.Cancel)
				return;

			//開いた数
			int iCount = 0;

			foreach (string pictfilename in openFileDialog1.FileNames)
			{
				if (openFileDialog1.FilterIndex == 1)
				{
					OpenLoadFile(pictfilename);
				}
				else
				{
					LoadFile(pictfilename);
				}

				iCount++;
			}

			//単数のみ選択切り替え
			if (iCount == 1)
				OnViewStart();
		}

		/// <summary>
		/// 保存
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem5_Click(object sender, System.EventArgs e)
		{
			SaveImage( string.Empty );
		}

		/// <summary>
		/// 終了ボタン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void menuItem3_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// 表示メニュー
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem7_Click(object sender, System.EventArgs e)
		{
			InfoView();
		}

		/// <summary>
		/// 枠無表示
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void menuItem50_Click(object sender, System.EventArgs e)
		{
			NoFrameView();
		}

		/// <summary>
		/// 全画面表示
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void menuItem8_Click(object sender, System.EventArgs e)
		{
			MaxView();
		}

		/// <summary>
		/// クリップボードよりペースト
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem11_Click(object sender, System.EventArgs e)
		{
			ClipPaste();
		}

		/// <summary>
		/// クリップボードへコピー
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void menuItem9_Click_1(object sender, System.EventArgs e)
		{
			ClipCopy();
		}

		/// <summary>
		/// バージョン情報
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void menuItem12_Click(object sender, System.EventArgs e)
		{
			VersionDialog();
		}

		/// <summary>
		/// 編集メニューポップアップ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void menuItem10_Popup(object sender, System.EventArgs e)
		{
			//コピーメニューの表示
			menuItem9.Enabled = (m_bitmap == null) ? false:true;
			//選択解除メニューの表示
			menuItem26.Enabled = resizePictControl.Selected;
		}

		/// <summary>
		/// 選択解除メニュー
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void menuItem26_Click(object sender, System.EventArgs e)
		{
			resizePictControl.Selected = false;
		}

		/// <summary>
		/// ファイルメニューポップアップ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void menuItem1_Popup(object sender, System.EventArgs e)
		{
			MenuEnable();
		}

		/// <summary>
		/// 表示メニューポップアップ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void menuItem6_Popup(object sender, System.EventArgs e)
		{
			MenuEnable();
		}

		/// <summary>
		/// ランチョス
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem27_Click(object sender, System.EventArgs e)
		{
			SelectEffect(0);
		}

		/// <summary>
		/// ニアレストネイバー
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem30_Click(object sender, EventArgs e)
		{
			SelectEffect(1);
		}

		/// <summary>
		/// バイリニア
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem28_Click(object sender, System.EventArgs e)
		{
			SelectEffect(2);
		}

		/// <summary>
		/// バイキュービック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem29_Click(object sender, System.EventArgs e)
		{
			SelectEffect(3);
		}

		/// <summary>
		/// 補間タイプ選択
		/// </summary>
		/// <param name="effecttype"></param>
		protected void SelectEffect(int effecttype)
		{
			switch( effecttype )
            {
				case 0:
					//メインメニュー
					menuItem27.Checked = true;
					menuItem30.Checked = false;
					menuItem28.Checked = false;
					menuItem29.Checked = false;
					break;
				case 1:
					//メインメニュー
					menuItem27.Checked = false;
					menuItem30.Checked = true;
					menuItem28.Checked = false;
					menuItem29.Checked = false;
					break;
				case 2:
					//メインメニュー
					menuItem27.Checked = false;
					menuItem30.Checked = false;
					menuItem28.Checked = true;
					menuItem29.Checked = false;
					break;
				case 3:
					//メインメニュー
					menuItem27.Checked = false;
					menuItem30.Checked = false;
					menuItem28.Checked = false;
					menuItem29.Checked = true;
					break;
			}
			resizePictControl.EffectType = effecttype;
		}

		#endregion

		#region ドラッグ&ドロップ
		/// <summary>
		/// ファイルドロップイベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
		{
			string[] formats = e.Data.GetFormats();

			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				//ファイルドロップ
				foreach (string fileName in (string[])e.Data.GetData(DataFormats.FileDrop))
				{
					try
					{
						OpenLoadFile( fileName );
					}
					catch( Exception )
					{
					}
				}

				OnViewStart();
			}
			else
				if (e.Data.GetDataPresent(DataFormats.Dib))
			{
				//DIBフォーマット
				MemoryStream srcstream = e.Data.GetData(DataFormats.Dib) as MemoryStream;

				//このタイプは、ファイルヘッダが存在しないので、追加
				MemoryStream outstream = DIBReader.AddFileHead( srcstream );

				if( outstream == null )
					return;

				Bitmap dropbmp = new Bitmap( outstream );
				if( dropbmp == null )
					return;

				AttachBmp( dropbmp );

				OnViewStart();

				return;
			}
		}

		/// <summary>
		/// ファイルドロップ開始
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
		{
			e.Effect = DragDropEffects.All;
		}

		#endregion

		#region イベント

		/// <summary>
		/// リサイズ中
		/// </summary>
		private const int WM_SIZING = 0x214;

		/// <summary>
		/// リサイズ方向
		/// </summary>
		private const int WMSZ_LEFT        =   1;
		private const int WMSZ_RIGHT       =   2;
		private const int WMSZ_TOP         =   3;
		private const int WMSZ_TOPLEFT     =   4;
		private const int WMSZ_TOPRIGHT    =   5;
		private const int WMSZ_BOTTOM      =   6;
		private const int WMSZ_BOTTOMLEFT  =   7;
		private const int WMSZ_BOTTOMRIGHT =   8;

		/// <summary>
		/// リサイズ方向
		/// </summary>
		protected int m_resizekind = 0;

		/// <summary>
		/// ウィンドウプロシージャ
		/// </summary>
		/// <param name="m"></param>
		protected override void WndProc(ref Message m) 
		{
			switch( m.Msg )
			{
				case WM_SIZING:
					m_resizekind = m.WParam.ToInt32();
					break;
			}

			base.WndProc(ref m);
		}

		/// <summary>
		/// サイズ変更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_Resize(object sender, System.EventArgs e)
		{
			//シフトキーが押されていた場合
			if( Control.ModifierKeys == Keys.Shift && m_bitmap != null )
			{
				try
				{
					this.Resize -= m_resizeev;

					//アスペクト比を維持する。
					double dPicHeight = ClientForm.ClientSize.Height;
					double dPicWidth = ClientForm.ClientSize.Width;

					//元画像
					double dBmpHeight = m_bitmap.Height;
					double dBmpWidth = m_bitmap.Width;
					double dBmpAspect = (dBmpHeight/dBmpWidth);

					int iWidth = Width;
					int iHeight = Height;

#region 固定アスペクトリサイズ中心処理
					if( m_resizekind == WMSZ_TOPLEFT )
					{
						if( (dPicHeight/dPicWidth) < dBmpAspect )
						{
							//画面が横長なので、縦に合わせる。
							int iPicWidth = (int)(dPicHeight/dBmpAspect);
							iWidth = iPicWidth + (iWidth- ClientForm.ClientSize.Width);

							Location = new Point( Location.X - (iWidth-(int)dPicWidth), Location.Y );
						}
						else
						{
							//画面が縦長なので、横に合わせる。
							int iPicHeight = (int)(dPicWidth*dBmpAspect);
							iHeight = iPicHeight + (iHeight- ClientForm.ClientSize.Height);

							Location = new Point( Location.X , Location.Y - (iHeight-(int)dPicHeight) );
						}
					}
					else
						if( m_resizekind == WMSZ_BOTTOMRIGHT )
					{
						if( (dPicHeight/dPicWidth) < dBmpAspect )
						{
							//画面が横長なので、縦に合わせる。
							int iPicWidth = (int)(dPicHeight/dBmpAspect);
							iWidth = iPicWidth + (iWidth- ClientForm.ClientSize.Width);
						}
						else
						{
							//画面が縦長なので、横に合わせる。
							int iPicHeight = (int)(dPicWidth*dBmpAspect);
							iHeight = iPicHeight + (iHeight- ClientForm.ClientSize.Height);
						}
					}
					else
					if( m_resizekind == WMSZ_LEFT || m_resizekind == WMSZ_RIGHT || m_resizekind == WMSZ_BOTTOMLEFT )
					{
						//画面が縦長なので、横に合わせる。
						int iPicHeight = (int)(dPicWidth*dBmpAspect);
						iHeight = iPicHeight + (iHeight- ClientForm.ClientSize.Height);
					}
					else
					if( m_resizekind == WMSZ_TOP || m_resizekind == WMSZ_BOTTOM || m_resizekind == WMSZ_TOPRIGHT )
					{
						//画面が横長なので、縦に合わせる。
						int iPicWidth = (int)(dPicHeight/dBmpAspect);
						iWidth = iPicWidth + (iWidth- ClientForm.ClientSize.Width);
					}
#endregion

					if( Width != iWidth )
						Width = iWidth;
					if( Height != iHeight )
						Height = iHeight;
				}
				catch( Exception )
				{
				}
				finally
				{
					this.Resize += m_resizeev;
				}
			}

			ReSizeImage( false );

			if( this.WindowState == System.Windows.Forms.FormWindowState.Minimized )
			{
				//最小化された場合、情報ダイアログを非表示
				m_InfoForm.Visible = false;
			}
			else
				if( m_InfoForm.Visible == false )
			{
				if( WindowState != System.Windows.Forms.FormWindowState.Minimized &&
					menuItem7.Checked == true )

					//最小化から戻されかつ、情報ダイアログ表示の場合
					m_InfoForm.Visible = true;
			}
		}

		/// <summary>
		/// 手打ちのサイズ変更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void PictValueChanged(object sender, System.EventArgs e)
		{
			this.Resize -= m_resizeev;

			//画像情報を更新
			int tmpwidth = Width - ClientForm.ClientSize.Width;
			int tmpheight =  Height - ClientForm.ClientSize.Height;

			Width = tmpwidth + (int)((double)m_InfoForm.WidthEdt.Value / m_Zoom) ;
			Height = tmpheight + (int)((double)m_InfoForm.HeightEdt.Value / m_Zoom);

			this.Resize += m_resizeev;

			ReSizeImage( true );

			//一回目の補正で、上手くいっていない場合は、もう一度補正
			if( ClientForm.ClientSize.Width != (int)((double)m_InfoForm.WidthEdt.Value / m_Zoom) ||
				ClientForm.ClientSize.Height != (int)((double)m_InfoForm.HeightEdt.Value / m_Zoom) )
			{
				tmpwidth = Width - ClientForm.ClientSize.Width;
				tmpheight =  Height - ClientForm.ClientSize.Height;

				Height = tmpheight + (int)((double)m_InfoForm.HeightEdt.Value / m_Zoom);
				Width = tmpwidth + (int)((double)m_InfoForm.WidthEdt.Value / m_Zoom) ;

				ReSizeImage( true );
			}
		}

		/// <summary>
		/// 表示倍率変更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void ZoomEdtValueChanged(object sender, System.EventArgs e)
		{
			m_Zoom = (double)m_InfoForm.ZoomEdt.Value / 100.0;
			ReSizeImage( true );
		}

		/// <summary>
		/// 情報ウィンドウが閉じられる直前のイベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void InfoFormClosing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel = true;

			InfoView();
		}

		/// <summary>
		/// ダブルクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainPicture_DoubleClick(object sender, System.EventArgs e)
		{
			MaxView();
		}

		#endregion

		#region ファイル
		/// <summary>
		/// イメージの内容により、サポート内容からロード
		/// </summary>
		public virtual void OpenLoadFile( string filename )
		{
			try
			{
				//一旦、メモリに展開する。
				MemoryStream deststream = new MemoryStream();

				using(FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read ))
				{
					BinaryReader infile = new BinaryReader( fs );
					byte[] tmpbin = infile.ReadBytes( (int)fs.Length );

					deststream.Write( tmpbin, 0, tmpbin.Length );
				}
				//イメージをアタッチする。
				AttachBmp( new Bitmap(deststream), filename );
			}
			catch( Exception )
			{
			}
		}

		/// <summary>
		/// イメージをロード
		/// </summary>
		public virtual void LoadFile( string filename )
		{
			try
			{
				//一旦、メモリに展開する。
				MemoryStream deststream = new MemoryStream();

				using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
				{
					BinaryReader infile = new BinaryReader(fs);
					byte[] tmpbin = infile.ReadBytes((int)fs.Length);

					deststream.Write(tmpbin, 0, tmpbin.Length);
				}
				//イメージをアタッチする。
				AttachBmp(new Bitmap(deststream), filename);
			}
			catch ( Exception )
			{
			}
		}

		/// <summary>
		/// イメージをセーブ
		/// </summary>
		public virtual void SaveImage( string filename )
		{
			if( m_bitmap == null || resizePictControl.Width <= 0 ||  resizePictControl.Height <= 0 )
				return ;

			//ダイアログオープン
			string filename1 = filename;
			if( filename == string.Empty )
			{
				//拡張子無し名
				filename1 = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
			}

			saveFileDialog1.FileName = filename1;
			saveFileDialog1.InitialDirectory = openFileDialog1.InitialDirectory;
			if( saveFileDialog1.ShowDialog( this ) == DialogResult.Cancel )
				return;

			try
			{
				//保存
				//ファイル形式を取得
				int iSelect = saveFileDialog1.FilterIndex - 1;
				ImageCodecInfo[] encoders = ImageCodecInfo.GetImageEncoders();

				//色深度パラメータ
				EncoderParameters encoderParams = new EncoderParameters();
				encoderParams.Param[0] = new EncoderParameter(Encoder.ColorDepth, 24L);

				resizePictControl.Picture.Image.Save( saveFileDialog1.FileName, encoders[ iSelect ], encoderParams );
			}
			catch( Exception )
			{
			}
		}
		#endregion

		#region イメージ

		/// <summary>
		/// イメージをアタッチ
		/// </summary>
		public virtual void AttachBmp( Bitmap bitmap, string filename )
		{
			AttachBmp( bitmap );
		}

		/// <summary>
		/// イメージをアタッチ
		/// </summary>
		public virtual void AttachBmp( Bitmap bitmap )
		{
			if( m_bitmap != null )
			{
				m_bitmap.Dispose();
				m_bitmap = null;
			}

			//元ビットマップをセットする。
			m_bitmap = bitmap;
			resizePictControl.Bitmap = bitmap.Clone() as Bitmap;

			//コントロールのＤｏｃｋを手動に変更
			resizePictControl.Dock = DockStyle.None;

			ReSizeImage( false );

			//手打ちの変更のイベントセット
			this.m_InfoForm.SizeValueChanged -= m_pictvaluechanged;
			this.m_InfoForm.SizeValueChanged += m_pictvaluechanged;

			//閉じられる直前のイベント設定
			this.m_InfoForm.Closing -= m_infoformclosing;
			this.m_InfoForm.Closing += m_infoformclosing;

			//画像情報を表示
			m_InfoForm.pictinfo.Text = string.Format( "{0}x{1}", m_bitmap.Width, m_bitmap.Height );

			//選択領域を初期化
			resizePictControl.Selected = false;
		}

		/// <summary>
		/// イメージのサイズを変更
		/// </summary>
		protected void ReSizeImage( bool InfoFormedt )
		{
			if( m_bitmap == null || ClientForm.ClientSize.Width <= 0 ||  ClientForm.ClientSize.Height <= 0 )
				return ;

			//画面表示サイズへ調整する。
			if( m_Zoom <= 1.0 )
			{
				try
				{
					ClientForm.SuspendLayout();

					resizePictControl.Width = (int)((double)ClientForm.ClientSize.Width * m_Zoom);
					resizePictControl.Height = (int)((double)ClientForm.ClientSize.Height * m_Zoom);

					ClientForm.AutoScroll = false;
					//センター位置へ移動
					resizePictControl.Location = new Point((ClientForm.ClientSize.Width - resizePictControl.Width)/2,
						(ClientForm.ClientSize.Height - resizePictControl.Height)/2);

					resizePictControl.ReSizeImage( (uint)resizePictControl.Width , (uint)resizePictControl.Height );
				}
				finally
				{
					ClientForm.ResumeLayout(true);
				}
			}
			else
			{
				try
				{
					if( ClientForm.AutoScroll == false )
					{
						//自動スクロールオンにて、コントロールサイズを調整する。

						//左上に移動
						resizePictControl.Location = new Point(0,0);
						resizePictControl.Width = (int)((double)ClientForm.ClientSize.Width * m_Zoom);
						resizePictControl.Height = (int)((double)ClientForm.ClientSize.Height * m_Zoom);

						m_bAutoScrollFirstEvent = true; //ここから、スクロールバー調整の為に、下のイベントが発生するので、調整させる。
						ClientForm.AutoScroll = true;
						m_bAutoScrollFirstEvent = false;
					}
					else
					{
						if( m_bAutoScrollFirstEvent == false )
							ClientForm.SuspendLayout();

						//一旦、現在のスクロール位置を取得
						Point currentPos = ClientForm.AutoScrollPosition;

						//左上に移動
						resizePictControl.Location = new Point(currentPos.X,currentPos.Y);

						resizePictControl.Width = (int)((double)ClientForm.ClientSize.Width * m_Zoom);
						resizePictControl.Height = (int)((double)ClientForm.ClientSize.Height * m_Zoom);
					}

					resizePictControl.ReSizeImage( (uint)resizePictControl.Width , (uint)resizePictControl.Height );
				}
				finally
				{
					ClientForm.ResumeLayout(true);
				}
			}

			if( InfoFormedt == false )
			{
				m_InfoForm.SizeValueChangedVisible = false;

				//画像情報を更新
				m_InfoForm.WidthEdt.Value = resizePictControl.Width;
				m_InfoForm.HeightEdt.Value = resizePictControl.Height;

				m_InfoForm.SizeValueChangedVisible = true;
			}
		}
		#endregion

		#region クリップボード
		/// <summary>
		/// クリップボードへコピー
		/// </summary>
		protected void ClipCopy()
		{
			if( m_bitmap == null || resizePictControl.Width <= 0 ||  resizePictControl.Height <= 0 )
				return ;

			//選択状態の場合、クリップ
			if( resizePictControl.Selected == true )
			{
				Bitmap trimbitmap = resizePictControl.SelectBitmap;
				if( trimbitmap != null )
					Clipboard.SetDataObject(trimbitmap, true);
			}
			else
			{
				Clipboard.SetDataObject(resizePictControl.Picture.Image, true);
			}
		}

		/// <summary>
		/// クリップボードよりペースト
		/// </summary>
		protected void ClipPaste()
		{
			try
			{
				//クリップボードにあるデータの取得
				IDataObject d = Clipboard.GetDataObject();
				//ビットマップデータ形式に関連付けられているデータを取得
				Bitmap img = d.GetData(DataFormats.Bitmap) as Bitmap;
				if (img != null)
				{
					AttachBmp( img );
				}

				//選択領域を初期化
				resizePictControl.Selected = false;

				OnViewStart();
			}
			catch( Exception )
			{
			}
		}
		#endregion

		/// <summary>
		/// 画像情報表示
		/// </summary>
		protected void InfoView()
		{
			if( m_bitmap == null )
				return ;

			menuItem7.Checked = !menuItem7.Checked;

			if( menuItem7.Checked == true )
				this.m_InfoForm.Show();
			else
				this.m_InfoForm.Hide();
		}

		/// <summary>
		/// 全画面表示
		/// </summary>
		protected void MaxView()
		{
			if( m_bitmap == null )
				return ;

			MaxViewForm maxform = new MaxViewForm();
			maxform.EffectType = resizePictControl.EffectType;
			maxform.Bitmap = m_bitmap;
			maxform.Show();
		}

		/// <summary>
		/// バージョン情報
		/// </summary>
		protected void VersionDialog()
		{
			VerDlg verdlg = new VerDlg();
			verdlg.ShowDialog( this );
		}

		/// <summary>
		/// 枠無表示
		/// </summary>
		protected virtual void NoFrameView()
		{
			if( m_bitmap == null )
				return ;

			this.Visible = false;

			MaxViewForm maxform = new MaxViewForm();

			//でかさを合わせる
			maxform.Size = resizePictControl.Size;

			Point pos = this.Location;
			pos.X += this.ClientForm.Location.X;
			pos.Y += this.ClientForm.Location.Y;

			maxform.Location = pos;
			maxform.WindowState = System.Windows.Forms.FormWindowState.Normal;

			maxform.EffectType = resizePictControl.EffectType;
			maxform.Bitmap = m_bitmap;
			maxform.ShowDialog();

			this.Visible = true;
		}
    }
}
