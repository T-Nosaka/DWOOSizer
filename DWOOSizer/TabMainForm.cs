using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;
using System.Reflection;

using LanzNet;
using DWOOSizer.Block;
using DWOOSizer.Effect;

namespace DWOOSizer
{
	/// <summary>
	/// タブ付きメインフォーム
	/// </summary>
	public class TabMainForm : MainForm
	{
		protected System.Windows.Forms.ContextMenu m_tabmenu;
		protected System.Windows.Forms.TabControl m_BmpTab;
		protected System.Windows.Forms.MenuItem menuItem32;
		protected System.Windows.Forms.MenuItem menuItem33;
		protected System.Windows.Forms.MenuItem menuItemContClose;

		//コンテキストメニュー
		protected System.Windows.Forms.ContextMenu contextMenu;
		protected System.Windows.Forms.MenuItem menuItem13;
		protected System.Windows.Forms.MenuItem menuItem14;
		protected System.Windows.Forms.MenuItem menuItem15;
		protected System.Windows.Forms.MenuItem menuItem16;
		protected System.Windows.Forms.MenuItem menuItem17;
		protected System.Windows.Forms.MenuItem menuItem18;
		protected System.Windows.Forms.MenuItem menuItem22;
		protected System.Windows.Forms.MenuItem menuItem23;
		protected System.Windows.Forms.MenuItem menuItem24;
		protected System.Windows.Forms.MenuItem menuItem50;
		protected System.Windows.Forms.MenuItem menuItemSp1;
		protected System.Windows.Forms.MenuItem menuItemSp2;
		protected System.Windows.Forms.MenuItem menuItemSp3;
		protected System.Windows.Forms.MenuItem menuItem60;
		protected System.Windows.Forms.MenuItem menuItem61;
		protected System.Windows.Forms.MenuItem menuItem62;
		protected System.Windows.Forms.MenuItem menuItem70;
		protected System.Windows.Forms.PrintDialog printDialog1;
		protected System.Windows.Forms.MenuItem menuItem71;
		protected System.Windows.Forms.MenuItem menuItemSp10;
		protected System.Windows.Forms.MenuItem menuItemSp11;
		protected System.Windows.Forms.MenuItem menuItem72;
		protected System.Windows.Forms.MenuItem menuItem73;
		protected System.Windows.Forms.MenuItem menuItem100;
		protected System.Windows.Forms.MenuItem menuItem110;
		protected System.Windows.Forms.MenuItem menuItem111;
		protected System.Windows.Forms.MenuItem menuItem112;
		protected System.Windows.Forms.MenuItem menuItem113;
		protected System.Windows.Forms.MenuItem menuItem114;
		protected System.Windows.Forms.MenuItem menuItemSp4;
		protected System.Windows.Forms.MenuItem menuItemRA1;
		protected System.Windows.Forms.MenuItem menuItemRA2;
		protected System.Windows.Forms.MenuItem menuItemRA3;
		protected System.Windows.Forms.MenuItem menuItemC1;
		protected System.Windows.Forms.PrintPreviewDialog debugpreview;
		protected System.Windows.Forms.MenuItem menuItem74;

		/// <summary>
		/// 名無し用カウント
		/// </summary>
		protected long m_clipcount = 0;

		#region Windows フォーム デザイナで生成されたコード 
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TabMainForm));
            this.contextMenu = new System.Windows.Forms.ContextMenu();
            this.menuItem17 = new System.Windows.Forms.MenuItem();
            this.menuItemContClose = new System.Windows.Forms.MenuItem();
            this.menuItem18 = new System.Windows.Forms.MenuItem();
            this.menuItemSp1 = new System.Windows.Forms.MenuItem();
            this.menuItem14 = new System.Windows.Forms.MenuItem();
            this.menuItemSp2 = new System.Windows.Forms.MenuItem();
            this.menuItem15 = new System.Windows.Forms.MenuItem();
            this.menuItem50 = new System.Windows.Forms.MenuItem();
            this.menuItem16 = new System.Windows.Forms.MenuItem();
            this.menuItemSp3 = new System.Windows.Forms.MenuItem();
            this.menuItem22 = new System.Windows.Forms.MenuItem();
            this.menuItem23 = new System.Windows.Forms.MenuItem();
            this.menuItem24 = new System.Windows.Forms.MenuItem();
            this.menuItemSp4 = new System.Windows.Forms.MenuItem();
            this.menuItemRA1 = new System.Windows.Forms.MenuItem();
            this.menuItemRA2 = new System.Windows.Forms.MenuItem();
            this.menuItemRA3 = new System.Windows.Forms.MenuItem();
            this.menuItem13 = new System.Windows.Forms.MenuItem();
            this.m_BmpTab = new System.Windows.Forms.TabControl();
            this.m_tabmenu = new System.Windows.Forms.ContextMenu();
            this.menuItem33 = new System.Windows.Forms.MenuItem();
            this.menuItem32 = new System.Windows.Forms.MenuItem();
            this.menuItem60 = new System.Windows.Forms.MenuItem();
            this.menuItem61 = new System.Windows.Forms.MenuItem();
            this.menuItem62 = new System.Windows.Forms.MenuItem();
            this.menuItem70 = new System.Windows.Forms.MenuItem();
            this.menuItem72 = new System.Windows.Forms.MenuItem();
            this.menuItem73 = new System.Windows.Forms.MenuItem();
            this.menuItem74 = new System.Windows.Forms.MenuItem();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.menuItem71 = new System.Windows.Forms.MenuItem();
            this.menuItemSp10 = new System.Windows.Forms.MenuItem();
            this.menuItemSp11 = new System.Windows.Forms.MenuItem();
            this.menuItem100 = new System.Windows.Forms.MenuItem();
            this.menuItem110 = new System.Windows.Forms.MenuItem();
            this.menuItem111 = new System.Windows.Forms.MenuItem();
            this.menuItem112 = new System.Windows.Forms.MenuItem();
            this.menuItem113 = new System.Windows.Forms.MenuItem();
            this.menuItem114 = new System.Windows.Forms.MenuItem();
            this.menuItemC1 = new System.Windows.Forms.MenuItem();
            this.debugpreview = new System.Windows.Forms.PrintPreviewDialog();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem110});
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem32,
            this.menuItem100,
            this.menuItemSp10,
            this.menuItem60,
            this.menuItemSp11,
            this.menuItem71,
            this.menuItem70});
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 10;
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 9;
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 2;
            // 
            // menuItem12
            // 
            this.menuItem12.Index = 5;
            // 
            // menuItem27
            // 
            this.menuItem27.Enabled = false;
            this.menuItem27.Index = 1;
            // 
            // menuItem28
            // 
            this.menuItem28.Enabled = false;
            // 
            // menuItem29
            // 
            this.menuItem29.Enabled = false;
            // 
            // menuItem30
            // 
            this.menuItem30.Enabled = false;
            this.menuItem30.Index = 0;
            // 
            // m_InfoForm
            // 
            this.m_InfoForm.Location = new System.Drawing.Point(500, 600);
            // 
            // contextMenu
            // 
            this.contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem17,
            this.menuItemContClose,
            this.menuItem18,
            this.menuItemSp1,
            this.menuItem14,
            this.menuItemSp2,
            this.menuItem15,
            this.menuItem50,
            this.menuItem16,
            this.menuItemSp3,
            this.menuItem22,
            this.menuItem23,
            this.menuItem24,
            this.menuItemSp4,
            this.menuItemRA1,
            this.menuItemRA2,
            this.menuItemRA3});
            this.contextMenu.Popup += new System.EventHandler(this.contextMenu_Popup);
            // 
            // menuItem17
            // 
            this.menuItem17.Index = 0;
            this.menuItem17.Text = "開く";
            this.menuItem17.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // menuItemContClose
            // 
            this.menuItemContClose.Enabled = false;
            this.menuItemContClose.Index = 1;
            this.menuItemContClose.Text = "閉じる";
            this.menuItemContClose.Click += new System.EventHandler(this.menuItem25_Click);
            // 
            // menuItem18
            // 
            this.menuItem18.Index = 2;
            this.menuItem18.Text = "保存";
            this.menuItem18.Click += new System.EventHandler(this.menuItem5_Click);
            // 
            // menuItemSp1
            // 
            this.menuItemSp1.Index = 3;
            this.menuItemSp1.Text = "-";
            // 
            // menuItem14
            // 
            this.menuItem14.Index = 4;
            this.menuItem14.Text = "ペースト";
            this.menuItem14.Click += new System.EventHandler(this.menuItem11_Click);
            // 
            // menuItemSp2
            // 
            this.menuItemSp2.Index = 5;
            this.menuItemSp2.Text = "-";
            // 
            // menuItem15
            // 
            this.menuItem15.Index = 6;
            this.menuItem15.Text = "画像情報";
            this.menuItem15.Click += new System.EventHandler(this.menuItem7_Click);
            // 
            // menuItem50
            // 
            this.menuItem50.Index = 7;
            this.menuItem50.Text = "枠無表示";
            // 
            // menuItem16
            // 
            this.menuItem16.Index = 8;
            this.menuItem16.Text = "全画面";
            // 
            // menuItemSp3
            // 
            this.menuItemSp3.Index = 9;
            this.menuItemSp3.Text = "-";
            // 
            // menuItem22
            // 
            this.menuItem22.Checked = true;
            this.menuItem22.Index = 10;
            this.menuItem22.Text = "ランチョス";
            this.menuItem22.Click += new System.EventHandler(this.menuItem27_Click);
            // 
            // menuItem23
            // 
            this.menuItem23.Index = 11;
            this.menuItem23.Text = "バイリニア";
            this.menuItem23.Click += new System.EventHandler(this.menuItem28_Click);
            // 
            // menuItem24
            // 
            this.menuItem24.Index = 12;
            this.menuItem24.Text = "バイキュービック";
            this.menuItem24.Click += new System.EventHandler(this.menuItem29_Click);
            // 
            // menuItemSp4
            // 
            this.menuItemSp4.Index = 13;
            this.menuItemSp4.Text = "-";
            // 
            // menuItemRA1
            // 
            this.menuItemRA1.Index = 14;
            this.menuItemRA1.Text = "右９０";
            this.menuItemRA1.Click += new System.EventHandler(this.menuItem112_Click);
            // 
            // menuItemRA2
            // 
            this.menuItemRA2.Index = 15;
            this.menuItemRA2.Text = "右１８０";
            this.menuItemRA2.Click += new System.EventHandler(this.menuItem113_Click);
            // 
            // menuItemRA3
            // 
            this.menuItemRA3.Index = 16;
            this.menuItemRA3.Text = "左９０";
            this.menuItemRA3.Click += new System.EventHandler(this.menuItem114_Click);
            // 
            // menuItem13
            // 
            this.menuItem13.Index = -1;
            this.menuItem13.Text = "";
            // 
            // m_BmpTab
            // 
            this.m_BmpTab.ContextMenu = this.m_tabmenu;
            this.m_BmpTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_BmpTab.HotTrack = true;
            this.m_BmpTab.Location = new System.Drawing.Point(0, 0);
            this.m_BmpTab.Multiline = true;
            this.m_BmpTab.Name = "m_BmpTab";
            this.m_BmpTab.SelectedIndex = 0;
            this.m_BmpTab.Size = new System.Drawing.Size(349, 0);
            this.m_BmpTab.TabIndex = 1;
            this.m_BmpTab.SelectedIndexChanged += new System.EventHandler(this.m_BmpTab_SelectedIndexChanged);
            // 
            // m_tabmenu
            // 
            this.m_tabmenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem33});
            // 
            // menuItem33
            // 
            this.menuItem33.Index = 0;
            this.menuItem33.Text = "全部閉じる";
            this.menuItem33.Click += new System.EventHandler(this.menuItem33_Click);
            // 
            // menuItem32
            // 
            this.menuItem32.Enabled = false;
            this.menuItem32.Index = 1;
            this.menuItem32.Shortcut = System.Windows.Forms.Shortcut.CtrlDel;
            this.menuItem32.Text = "閉じる";
            this.menuItem32.Click += new System.EventHandler(this.menuItem25_Click);
            // 
            // menuItem60
            // 
            this.menuItem60.Index = 5;
            this.menuItem60.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem61,
            this.menuItem62});
            this.menuItem60.Text = "TWAIN";
            // 
            // menuItem61
            // 
            this.menuItem61.Index = 0;
            this.menuItem61.Text = "選択";
            this.menuItem61.Click += new System.EventHandler(this.menuItem61_Click);
            // 
            // menuItem62
            // 
            this.menuItem62.Index = 1;
            this.menuItem62.Text = "取込";
            this.menuItem62.Click += new System.EventHandler(this.menuItem62_Click);
            // 
            // menuItem70
            // 
            this.menuItem70.Index = 8;
            this.menuItem70.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem72,
            this.menuItem73,
            this.menuItem74});
            this.menuItem70.Text = "印刷";
            // 
            // menuItem72
            // 
            this.menuItem72.Index = 0;
            this.menuItem72.Text = "単一印刷";
            this.menuItem72.Click += new System.EventHandler(this.menuItem72_Click);
            // 
            // menuItem73
            // 
            this.menuItem73.Index = 1;
            this.menuItem73.Text = "複数印刷";
            this.menuItem73.Click += new System.EventHandler(this.menuItem73_Click);
            // 
            // menuItem74
            // 
            this.menuItem74.Index = 2;
            this.menuItem74.Text = "プレビュー";
            this.menuItem74.Visible = false;
            this.menuItem74.Click += new System.EventHandler(this.menuItem74_Click);
            // 
            // menuItem71
            // 
            this.menuItem71.Index = 7;
            this.menuItem71.Shortcut = System.Windows.Forms.Shortcut.CtrlP;
            this.menuItem71.Text = "印刷設定";
            this.menuItem71.Click += new System.EventHandler(this.menuItem71_Click);
            // 
            // menuItemSp10
            // 
            this.menuItemSp10.Index = 4;
            this.menuItemSp10.Text = "-";
            // 
            // menuItemSp11
            // 
            this.menuItemSp11.Index = 6;
            this.menuItemSp11.Text = "-";
            // 
            // menuItem100
            // 
            this.menuItem100.Index = 3;
            this.menuItem100.Text = "一括保存";
            this.menuItem100.Click += new System.EventHandler(this.menuItem100_Click);
            // 
            // menuItem110
            // 
            this.menuItem110.Index = 4;
            this.menuItem110.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem111,
            this.menuItemC1});
            this.menuItem110.Text = "補正";
            // 
            // menuItem111
            // 
            this.menuItem111.Enabled = false;
            this.menuItem111.Index = 0;
            this.menuItem111.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem112,
            this.menuItem113,
            this.menuItem114});
            this.menuItem111.Text = "回転";
            // 
            // menuItem112
            // 
            this.menuItem112.Index = 0;
            this.menuItem112.Shortcut = System.Windows.Forms.Shortcut.Ctrl6;
            this.menuItem112.Text = "右９０";
            this.menuItem112.Click += new System.EventHandler(this.menuItem112_Click);
            // 
            // menuItem113
            // 
            this.menuItem113.Index = 1;
            this.menuItem113.Shortcut = System.Windows.Forms.Shortcut.Ctrl5;
            this.menuItem113.Text = "右１８０";
            this.menuItem113.Click += new System.EventHandler(this.menuItem113_Click);
            // 
            // menuItem114
            // 
            this.menuItem114.Index = 2;
            this.menuItem114.Shortcut = System.Windows.Forms.Shortcut.Ctrl4;
            this.menuItem114.Text = "左９０";
            this.menuItem114.Click += new System.EventHandler(this.menuItem114_Click);
            // 
            // menuItemC1
            // 
            this.menuItemC1.Enabled = false;
            this.menuItemC1.Index = 1;
            this.menuItemC1.Text = "彩度・輝度";
            this.menuItemC1.Click += new System.EventHandler(this.menuItemC1_Click);
            // 
            // debugpreview
            // 
            this.debugpreview.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.debugpreview.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.debugpreview.ClientSize = new System.Drawing.Size(400, 300);
            this.debugpreview.Enabled = true;
            this.debugpreview.Icon = ((System.Drawing.Icon)(resources.GetObject("debugpreview.Icon")));
            this.debugpreview.Name = "debugpreview";
            this.debugpreview.Visible = false;
            // 
            // TabMainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
            this.ClientSize = new System.Drawing.Size(349, 0);
            this.ContextMenu = this.contextMenu;
            this.Controls.Add(this.m_BmpTab);
            this.Name = "TabMainForm";
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public TabMainForm()
		{
			InitializeComponent();

			//コンテキストをアタッチ
			resizePictControl.ContextMenu = this.contextMenu;
		}

		/// <summary>
		/// 描画コントロールプロパティ
		/// </summary>
		protected override ScrollableControl ClientForm
		{
			get
			{
				if( m_BmpTab.SelectedTab != null )
					return m_BmpTab.SelectedTab;

				return this;
			}
		}

		/// <summary>
		/// イメージをセーブ
		/// </summary>
		public override void SaveImage( string filename )
		{
			if( m_BmpTab.SelectedTab != null )
			{
				TabBmpPage tabpage = m_BmpTab.SelectedTab as TabBmpPage;
				base.SaveImage( Path.GetFileNameWithoutExtension(tabpage.Filename) );
			}
		}

		/// <summary>
		/// タブを再描画
		/// </summary>
		protected void RefreshTab()
		{
			//選択されたページを取得
			TabBmpPage tabpage = m_BmpTab.SelectedTab as TabBmpPage;
			if( tabpage != null )
			{
				//コントロールを作成しなおす
				tabpage.Controls.Clear();
				//ページにピクチャーを貼り付ける
				tabpage.Controls.Add( resizePictControl );
			}
		}

		/// <summary>
		/// 読み込み処理等が完了し、アイドルへモードを遷移するイベント
		/// </summary>
		protected override void OnViewStart()
		{
			m_BmpTab.SelectedIndex = m_BmpTab.Controls.Count - 1;
		}

		/// <summary>
		/// メニューの有無無効制御
		/// </summary>
		/// <param name="flg"></param>
		protected override void MenuEnable( )
		{
			bool bFlg = (m_bitmap == null) ? false:true;

			//補間メニュー
			menuItem22.Enabled = bFlg;	//ランチョス コンテキスト
			menuItem27.Enabled = bFlg;	//ランチョス
			menuItem23.Enabled = bFlg;	//バイリニア コンテキスト
			menuItem28.Enabled = bFlg;	//バイリニア
			menuItem24.Enabled = bFlg;	//バイキュービック コンテキスト
			menuItem29.Enabled = bFlg;  //バイキュービック
			menuItem30.Enabled = bFlg;  //ニアレストネイバー

			//一括保存
			menuItem100.Enabled = bFlg;
			//保存メニューの表示
			menuItem18.Enabled = bFlg;
			//コピーの表示
			menuItem13.Enabled = bFlg;
			//画像情報
			menuItem15.Enabled = bFlg;
			//全画面
			menuItem16.Enabled = bFlg;

			//閉じるメニューの表示
			menuItemContClose.Enabled = bFlg;
		
			//枠無しの表示
			menuItem50.Enabled = bFlg;

			//閉じるメニューの表示
			menuItem32.Enabled = bFlg;

			//印刷設定
			menuItem70.Enabled = bFlg;
			menuItem71.Enabled = bFlg;
			menuItem72.Enabled = bFlg;
			menuItem73.Enabled = bFlg;

			//補正
			//回転
			menuItem111.Enabled = bFlg;
			menuItem112.Enabled = bFlg;	//右９０
			menuItemRA1.Enabled = bFlg;	//右９０ コンテキスト
			menuItem113.Enabled = bFlg;	//右１８０
			menuItemRA2.Enabled = bFlg;	//右１８０ コンテキスト
			menuItem114.Enabled = bFlg;	//左９０
			menuItemRA3.Enabled = bFlg;	//左９０ コンテキスト

			//彩度・輝度
			menuItemC1.Enabled = bFlg;

			base.MenuEnable();
		}

		#region タブ制御

		/// <summary>
		/// ファイルからタブ追加
		/// </summary>
		/// <param name="bitmap"></param>
		protected void AddTabCreate( Bitmap bitmap, string filename )
		{
			//タブページ作成
			TabBmpPage tabpage = new TabBmpPage();

			//ページ数をタイトルにしておく
			if( filename == string.Empty )
			{
				DateTime now = DateTime.Now;
				//ファイル名を記録する。
				tabpage.Filename = string.Format("{0:yyyyMMddHHmmss}{1}.bmp",now,m_clipcount);

				tabpage.Text = string.Format("CLIP{0}", m_clipcount );

				m_clipcount++;
			}
			else
			{
				tabpage.Text = Path.GetFileName( filename );
				//ファイル名を記録する。
				tabpage.Filename = Path.GetFileName( filename );
			}

			//タブページに画像を保存する。
			tabpage.SrcBitmap = bitmap;

			//ページ追加
			m_BmpTab.Controls.Add( tabpage );

			//メニューを有効
			MenuEnable( );

			//メモリの解放
			GC.Collect(2);
		}

		/// <summary>
		/// タブを削除
		/// </summary>
		/// <param name="index"></param>
		protected void RemoveTabDelete( int index )
		{
			TabBmpPage tabpage = m_BmpTab.GetControl( index ) as TabBmpPage;
			if( tabpage != null )
			{
				tabpage.SrcBitmap = null;

				m_BmpTab.Controls.RemoveAt( index );

				if( m_BmpTab.Controls.Count < 1 )
				{
					//初期状態にする。
					if ( m_bitmap != null )
					{
						m_bitmap.Dispose();
						m_bitmap = null;

						if( resizePictControl.Bitmap != null )
						{
							resizePictControl.Bitmap = null;
						}

						m_clipcount = 0;
					}

					//メニューを無効
					MenuEnable( );
				}

				//メモリの解放
				GC.Collect(2);
			}
		}

		/// <summary>
		/// タブを全削除
		/// </summary>
		/// <param name="index"></param>
		protected void RemoveTabDelete()
		{
			this.SuspendLayout();

			this.m_BmpTab.SelectedIndexChanged -= new System.EventHandler(this.m_BmpTab_SelectedIndexChanged);

			foreach( TabBmpPage tabpage in m_BmpTab.Controls )
			{
				tabpage.SrcBitmap = null;
			}

			m_BmpTab.Controls.Clear();

			//初期状態にする。
			if ( m_bitmap != null )
			{
				m_bitmap.Dispose();
				m_bitmap = null;

				if( resizePictControl.Bitmap != null )
				{
					resizePictControl.Bitmap = null;
				}

				m_clipcount = 0;
			}

			//メニューを無効
			MenuEnable( );

			this.m_BmpTab.SelectedIndexChanged += new System.EventHandler(this.m_BmpTab_SelectedIndexChanged);

			this.ResumeLayout(false);

			//メモリの解放
			GC.Collect();
		}
		#endregion

		#region イメージ
		/// <summary>
		/// イメージをアタッチ
		/// </summary>
		public override void AttachBmp( Bitmap bitmap, string filename )
		{
			this.SuspendLayout();
			if( m_BmpTab.Controls.Count == 0 )
			{
				//色々とありまして
				base.AttachBmp( bitmap );
			}

			//タブを作成する。
			AddTabCreate( bitmap, filename );

			//タブをリフレッシュ
			RefreshTab();

			this.ResumeLayout(false);
		}

		/// <summary>
		/// イメージをアタッチ
		/// </summary>
		public override void AttachBmp( Bitmap bitmap )
		{
			AttachBmp( bitmap, string.Empty );
		}

		/// <summary>
		/// 右９０度回転
		/// </summary>
		protected void Rotate( int pos )
		{
			TabBmpPage page = m_BmpTab.SelectedTab as TabBmpPage;

			if( page != null )
			{
				page.Rotate( pos );

				base.AttachBmp( page.SrcBitmap );

				//タブをリフレッシュ
				RefreshTab();
			}
		}

		#endregion

		#region イベント

		/// <summary>
		/// タブが選択されたイベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void m_BmpTab_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//タブをリフレッシュ
			RefreshTab();

			//選択されたページを取得
			TabBmpPage tabpage = m_BmpTab.SelectedTab as TabBmpPage;
			if( tabpage != null )
			{
				base.AttachBmp( tabpage.SrcBitmap );

				GC.Collect();
			}
		}
		#endregion

		#region メニュー

		/// 読み込み処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			LoadFile();
		}

		/// <summary>
		/// 保存
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem5_Click(object sender, System.EventArgs e)
		{
			SaveImage(string.Empty);
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
		/// クリップボードよりペースト
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem11_Click(object sender, System.EventArgs e)
		{
			ClipPaste();
		}

		/// <summary>
		/// ファイルメニューポップアップ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected override void menuItem1_Popup(object sender, System.EventArgs e)
		{
			base.menuItem1_Popup( sender, e);
		}

		/// <summary>
		/// コンテキストメニューポップアップ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void contextMenu_Popup(object sender, System.EventArgs e)
		{
			MenuEnable( );
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
		/// タブを閉じる選択
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem25_Click(object sender, System.EventArgs e)
		{
			RemoveTabDelete( m_BmpTab.SelectedIndex );
		}

		/// <summary>
		/// 全部閉じる
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem33_Click(object sender, System.EventArgs e)
		{
			RemoveTabDelete();
		}

		/// <summary>
		/// TWAIN 選択
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem61_Click(object sender, System.EventArgs e)
		{
		}

		/// <summary>
		/// TWAIN 取得
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem62_Click(object sender, System.EventArgs e)
		{
		}

		/// <summary>
		/// 一括保存
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem100_Click(object sender, System.EventArgs e)
		{
			if( m_BmpTab.SelectedTab != null )
			{
				//ページリスト作成
				TabBmpPage[] bmplist = new TabBmpPage[m_BmpTab.Controls.Count];
				int iCnt = 0;
				foreach( TabBmpPage tabpage in m_BmpTab.Controls )
				{
					bmplist[ iCnt ] = tabpage;
					iCnt++;
				}

				BlanketSaveDlg dlg = new BlanketSaveDlg( );
				dlg.BmpList = bmplist;
				dlg.ResizeControl = resizePictControl;

				dlg.ShowDialog( this );

				dlg.BmpList = null;

				//強制カレント描画
				m_BmpTab_SelectedIndexChanged( this, null );
			}
		}

		/// <summary>
		/// 右９０
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem112_Click(object sender, System.EventArgs e)
		{
			Rotate( 0 );
		}

		/// <summary>
		/// 右１８０
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem113_Click(object sender, System.EventArgs e)
		{
			Rotate( 1 );
		}

		/// <summary>
		/// 左９０
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem114_Click(object sender, System.EventArgs e)
		{
			Rotate( 2 );
		}

		/// <summary>
		/// 彩度・輝度ボタン押下
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItemC1_Click(object sender, System.EventArgs e)
		{
			if( m_bitmap == null )
				return;

			EffectForm efform = new EffectForm();
			efform.Bitmap = m_bitmap;
			if( efform.ShowDialog( this ) == DialogResult.OK )
			{
				resizePictControl.Saturation = efform.SaturationBar.Value;
				resizePictControl.Lightness = efform.LightnessBar.Value;
				Bitmap destbitmap = resizePictControl.Cooked( m_bitmap );
				AttachBmp( destbitmap );
				OnViewStart();
			}
		}

		#endregion
	
		/// <summary>
		/// 枠無表示
		/// </summary>
		protected override void NoFrameView()
		{
			if( m_bitmap == null )
				return ;

			//選択されたページを取得
			TabBmpPage tabpage = m_BmpTab.SelectedTab as TabBmpPage;
			if( tabpage == null )
				return;

			this.Visible = false;

			MaxViewForm maxform = new MaxViewForm();

			//でかさを合わせる
			maxform.Size = ClientForm.Size;

			Point pos = this.Location;
			pos.X += ( ClientForm.Location.X + tabpage.Location.X );
			pos.Y += ( ClientForm.Location.Y + tabpage.Location.Y + m_BmpTab.ItemSize.Height);

			maxform.Location = pos;
			maxform.WindowState = System.Windows.Forms.FormWindowState.Normal;

			maxform.EffectType = resizePictControl.EffectType;
			maxform.Bitmap = m_bitmap;
			maxform.ShowDialog();

			this.Visible = true;
		}

		#region 印刷

		/// <summary>
		/// 印刷ドキュメント
		/// </summary>
		PrintDocument m_pd = new PrintDocument();

		/// <summary>
		/// 印刷中ページ
		/// </summary>
		protected int m_page = 0;

		/// <summary>
		/// 印刷画像リスト
		/// </summary>
		protected ArrayList m_printlist = new ArrayList();

		/// <summary>
		/// 印刷拡大率
		/// </summary>
		protected decimal m_printzoom = (decimal)100;

		/// <summary>
		/// 印刷拡大率プロパティ
		/// </summary>
		public decimal PrintZoom
		{
			get
			{
				return m_printzoom;
			}
			set
			{
				m_printzoom = value;
			}
		}

		/// <summary>
		/// 印刷位置
		/// </summary>
		protected Point m_printoffset = new Point( 0, 0 );

		/// <summary>
		/// 印刷位置プロパティ
		/// </summary>
		public Point PrintOffset
		{
			get
			{
				return m_printoffset;
			}
			set
			{
				m_printoffset = value;
			}
		}

		/// <summary>
		/// 印刷設定
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem71_Click(object sender, System.EventArgs e)
		{
			if( m_BmpTab.Controls.Count <= 0 )
				return;

			TabBmpPage tabpage = m_BmpTab.SelectedTab as TabBmpPage;
			if( tabpage == null )
				return;

			m_printlist = new ArrayList();

			//印刷画像追加
			m_printlist.Add( tabpage );

			m_page = 0;

			int ix = PrintOffset.X;
			int iy = PrintOffset.Y;

			//印刷ドキュメント作成
			printDialog1.Document = m_pd;

			try
			{
				PrintSetDlg pageset = new PrintSetDlg( printDialog1, this, resizePictControl.Bitmap );
				pageset.ZoomFactor.Value = m_printzoom;
				pageset.PositionX.Value = ix;
				pageset.PositionY.Value = iy;
				pageset.ShowDialog( this );

				m_printlist = null;
				
				pageset.Dispose();
			}
			catch( Exception )
			{
				MessageBox.Show( "プリンタが存在しません。" );
			}

			return;
		}

		/// <summary>
		/// 印刷プレビュー
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem74_Click(object sender, System.EventArgs e)
		{
			if( m_BmpTab.Controls.Count <= 0 )
				return;

			m_printlist = new ArrayList();

			TabBmpPage tabpage = m_BmpTab.SelectedTab as TabBmpPage;
			if( tabpage == null )
				return;

			//印刷画像追加
			m_printlist.Add( tabpage );

			m_page = 0;

			try
			{
				//印刷ドキュメント作成
				m_pd.DocumentName = tabpage.Text;
				m_pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);

				debugpreview.Document = m_pd;
				debugpreview.ShowDialog( this );
			}
			catch( Exception )
			{
			}

			m_printlist = null;

			GC.Collect();

			return;
		
		}

		/// <summary>
		/// 単一印刷
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem72_Click(object sender, System.EventArgs e)
		{
			if( m_BmpTab.Controls.Count <= 0 )
				return;

			m_printlist = new ArrayList();

			TabBmpPage tabpage = m_BmpTab.SelectedTab as TabBmpPage;
			if( tabpage == null )
				return;

			//印刷画像追加
			m_printlist.Add( tabpage );

			m_page = 0;

			try
			{
				//印刷ドキュメント作成
				m_pd.DocumentName = tabpage.Text;
				m_pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);

				//印刷
				m_pd.Print();
			}
			catch( Exception )
			{
			}

			m_printlist = null;

			GC.Collect();

			return;
		}

		/// <summary>
		/// 複数印刷
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem73_Click(object sender, System.EventArgs e)
		{
			if( m_BmpTab.Controls.Count <= 0 )
				return;

			foreach( TabBmpPage tabpage in m_BmpTab.Controls )
			{
				m_printlist = new ArrayList();
				m_printlist.Add( tabpage );

				m_page = 0;

				try
				{
					//印刷ドキュメント作成
					m_pd.DocumentName = tabpage.Text;
					m_pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);

					//印刷
					m_pd.Print();
				}
				catch( Exception )
				{
				}

				GC.Collect();
			}

			m_printlist = null;

			return;
		}

		/// <summary>
		/// 印刷イベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="ev"></param>
		private void pd_PrintPage(object sender, PrintPageEventArgs ev) 
		{
			IntPtr hdc = new IntPtr();
			int iHorzres = 1;
			int iVertres = 1;
			int iPhysicalWidth = 1;
			int iPhysicalHeight = 1;
			int iMarginLeft = 1;
			int iMarginTop = 1;

			try
			{
				//物理印刷領域取得
				hdc = ev.Graphics.GetHdc();

				//印字可能領域
				iHorzres = DeviceCaps.GetDeviceCaps( hdc, (int)DeviceCaps.GetDeviceCapsFunction.HORZRES );
				iVertres = DeviceCaps.GetDeviceCaps( hdc, (int)DeviceCaps.GetDeviceCapsFunction.VERTRES );

				//実領域ドット数
				iPhysicalWidth = DeviceCaps.GetDeviceCaps( hdc, (int)DeviceCaps.GetDeviceCapsFunction.PHYSICALWIDTH );
				iPhysicalHeight = DeviceCaps.GetDeviceCaps( hdc, (int)DeviceCaps.GetDeviceCapsFunction.PHYSICALHEIGHT );

				//余白位置
				//左上マージントップ
				iMarginLeft = DeviceCaps.GetDeviceCaps( hdc, (int)DeviceCaps.GetDeviceCapsFunction.PHYSICALOFFSETX );
				iMarginTop = DeviceCaps.GetDeviceCaps( hdc, (int)DeviceCaps.GetDeviceCapsFunction.PHYSICALOFFSETY );
			}
			finally
			{
				ev.Graphics.ReleaseHdc(hdc);
			}

			//位置調整
			iMarginLeft += PrintOffset.X;
			iMarginTop += PrintOffset.Y;

			//画像取り出し
			BitmapStock bitmapstock = new BitmapStock();
			TabBmpPage tabpage = m_printlist[m_page] as TabBmpPage;

			//拡大率
			decimal iZoomWidth = m_printzoom/100 * (decimal)resizePictControl.Width;
			decimal iZoomHeight = m_printzoom/100 * (decimal)resizePictControl.Height;

			ev.Graphics.PageUnit = GraphicsUnit.Pixel ;
			Bitmap srcbmp = tabpage.SrcBitmap;

			var resizer = new OLanczLngMemNet();
			resizer.EffectType( resizePictControl.EffectType );

			BlockResizeBmp blockbmp = new BlockResizeBmp();
			int blockcnt = blockbmp.DivideSquare( (int)iZoomWidth, (int)iZoomHeight, srcbmp.Width, srcbmp.Height );
			for( int no=0; no<blockcnt; no++ )
			{
				blockbmp.DrawBitmap( resizer, ev.Graphics, srcbmp, no, PrintOffset.X, PrintOffset.Y );

			}

			GC.Collect();

			m_page++;

			if( m_printlist.Count > m_page )
				ev.HasMorePages = true;
			else
			{
				m_page = 0;
				ev.HasMorePages = false;
			}
		}
		#endregion
	}
}
