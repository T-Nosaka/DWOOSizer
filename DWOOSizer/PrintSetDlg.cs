using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Printing;

using DWOOSizer.Preview;

namespace DWOOSizer
{
	/// <summary>
	/// プリンタ設定ダイアログ
	/// </summary>
	public class PrintSetDlg : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;


		private DWOOSizer.Preview.PrintPreviewControl PreviewControl;

		/// <summary>
		/// 設定ダイアログ
		/// </summary>
		private System.Windows.Forms.PrintDialog printDialog1;

		/// <summary>
		/// 紙設定
		/// </summary>
		protected PageSettings m_pagesetting;

		/// <summary>
		/// 親フォーム
		/// </summary>
		protected TabMainForm m_parentform;

		/// <summary>
		/// 印刷位置 X
		/// </summary>
		public System.Windows.Forms.NumericUpDown PositionX;

		/// <summary>
		/// 印刷位置 Y
		/// </summary>
		public System.Windows.Forms.NumericUpDown PositionY;

		/// <summary>
		/// 印刷拡大率
		/// </summary>
		public System.Windows.Forms.NumericUpDown ZoomFactor;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public PrintSetDlg( )
		{
			InitializeComponent();
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public PrintSetDlg( System.Windows.Forms.PrintDialog dialog, TabMainForm form, Bitmap bitmap )
		{
			InitializeComponent();

			printDialog1 = dialog;
			m_parentform = form;

			//印刷画像
			PreviewControl.Bitmap = bitmap;

			//紙設定
			m_pagesetting = dialog.Document.PrinterSettings.DefaultPageSettings;

			//紙サイズ設定
			PreviewControl.SetPaperSize( m_pagesetting, m_pagesetting.Landscape );

			//印刷画像ドロップイベント設定
			PreviewControl.ImageDropEvent += new EventHandler( OnDropImage );
		}


		/// <summary>
		/// 使用されているリソースに後処理を実行します。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}

				PreviewControl.Bitmap = null;
			}
			base.Dispose( disposing );
		}

		#region Windows フォーム デザイナで生成されたコード 
		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.ZoomFactor = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.PositionX = new System.Windows.Forms.NumericUpDown();
			this.PositionY = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.PreviewControl = new DWOOSizer.Preview.PrintPreviewControl();
			((System.ComponentModel.ISupportInitialize)(this.ZoomFactor)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PositionX)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PositionY)).BeginInit();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(160, 320);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(88, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "プリンタの設定";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(40, 320);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(88, 23);
			this.button2.TabIndex = 1;
			this.button2.Text = "閉じる";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// ZoomFactor
			// 
			this.ZoomFactor.Location = new System.Drawing.Point(16, 288);
			this.ZoomFactor.Maximum = new System.Decimal(new int[] {
																	   999,
																	   0,
																	   0,
																	   0});
			this.ZoomFactor.Minimum = new System.Decimal(new int[] {
																	   1,
																	   0,
																	   0,
																	   0});
			this.ZoomFactor.Name = "ZoomFactor";
			this.ZoomFactor.Size = new System.Drawing.Size(56, 19);
			this.ZoomFactor.TabIndex = 3;
			this.ZoomFactor.Value = new System.Decimal(new int[] {
																	 100,
																	 0,
																	 0,
																	 0});
			this.ZoomFactor.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 264);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 16);
			this.label1.TabIndex = 4;
			this.label1.Text = "拡大率";
			// 
			// PositionX
			// 
			this.PositionX.Location = new System.Drawing.Point(176, 264);
			this.PositionX.Maximum = new System.Decimal(new int[] {
																	  100000,
																	  0,
																	  0,
																	  0});
			this.PositionX.Minimum = new System.Decimal(new int[] {
																	  100000,
																	  0,
																	  0,
																	  -2147483648});
			this.PositionX.Name = "PositionX";
			this.PositionX.Size = new System.Drawing.Size(72, 19);
			this.PositionX.TabIndex = 5;
			this.PositionX.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
			// 
			// PositionY
			// 
			this.PositionY.Location = new System.Drawing.Point(176, 288);
			this.PositionY.Maximum = new System.Decimal(new int[] {
																	  100000,
																	  0,
																	  0,
																	  0});
			this.PositionY.Minimum = new System.Decimal(new int[] {
																	  100000,
																	  0,
																	  0,
																	  -2147483648});
			this.PositionY.Name = "PositionY";
			this.PositionY.Size = new System.Drawing.Size(72, 19);
			this.PositionY.TabIndex = 6;
			this.PositionY.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(112, 264);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 16);
			this.label2.TabIndex = 7;
			this.label2.Text = "位置   X";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(148, 288);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(12, 16);
			this.label3.TabIndex = 8;
			this.label3.Text = "Y";
			// 
			// PreviewControl
			// 
			this.PreviewControl.BackColor = System.Drawing.Color.White;
			this.PreviewControl.Bitmap = null;
			this.PreviewControl.ImageZoom = 1;
			this.PreviewControl.Location = new System.Drawing.Point(8, 8);
			this.PreviewControl.Name = "PreviewControl";
			this.PreviewControl.Size = new System.Drawing.Size(264, 240);
			this.PreviewControl.TabIndex = 9;
			// 
			// PrintSetDlg
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(282, 367);
			this.ControlBox = false;
			this.Controls.Add(this.PreviewControl);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.PositionY);
			this.Controls.Add(this.PositionX);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.ZoomFactor);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "PrintSetDlg";
			this.Text = "プリンタ設定";
			this.Load += new System.EventHandler(this.PrintSetDlg_Load);
			((System.ComponentModel.ISupportInitialize)(this.ZoomFactor)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PositionX)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PositionY)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// プリンタの設定ボタン押下
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button1_Click(object sender, System.EventArgs e)
		{
			//印刷設定
			printDialog1.AllowSomePages = true;
			if( printDialog1.ShowDialog( this ) == DialogResult.OK )
			{
				//紙サイズ設定
				PreviewControl.SetPaperSize( m_pagesetting, m_pagesetting.Landscape );

				OnSizeModify();

				return;
			}
		}

		/// <summary>
		/// 閉じるボタン押下
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button2_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// 紙サイズよりプレビューを調整する。
		/// </summary>
		protected void OnSizeModify()
		{
			//画像拡大率設定
			PreviewControl.ImageZoom = (double)ZoomFactor.Value/100.0;

			//画像位置設定
			PreviewControl.SetImagePoint( new Point((int)PositionX.Value, (int)PositionY.Value) );
		}

		/// <summary>
		/// 画像サイズ変更イベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void numericUpDown1_ValueChanged(object sender, System.EventArgs e)
		{
			m_parentform.PrintZoom = ZoomFactor.Value;

			m_parentform.PrintOffset = new Point((int)PositionX.Value,(int)PositionY.Value);

			OnSizeModify();
		}

		/// <summary>
		/// 表示イベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PrintSetDlg_Load(object sender, System.EventArgs e)
		{
			OnSizeModify();
		}

		/// <summary>
		/// 印刷画像のドロップイベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="arg"></param>
		protected void OnDropImage( object sender, EventArgs arg )
		{
			Point pos = PreviewControl.GetImagePoint();

			PositionX.Value = pos.X;
			PositionY.Value = pos.Y;
		}
	}
}
