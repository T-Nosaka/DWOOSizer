using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace DWOOSizer
{
	/// <summary>
	/// 画像情報ダイアログ
	/// </summary>
	public class InfoForm : System.Windows.Forms.Form
	{
		public System.Windows.Forms.NumericUpDown WidthEdt;
		public System.Windows.Forms.NumericUpDown HeightEdt;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		public System.Windows.Forms.Label pictinfo;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button VGABtn;
		private System.Windows.Forms.Button SVGABtn;
		private System.Windows.Forms.Button XGABtn;
		public System.Windows.Forms.NumericUpDown ZoomEdt;
		private System.Windows.Forms.Label label3;
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public InfoForm()
		{
			InitializeComponent();
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
			this.WidthEdt = new System.Windows.Forms.NumericUpDown();
			this.HeightEdt = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.pictinfo = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.VGABtn = new System.Windows.Forms.Button();
			this.SVGABtn = new System.Windows.Forms.Button();
			this.XGABtn = new System.Windows.Forms.Button();
			this.ZoomEdt = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.WidthEdt)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.HeightEdt)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ZoomEdt)).BeginInit();
			this.SuspendLayout();
			// 
			// WidthEdt
			// 
			this.WidthEdt.Location = new System.Drawing.Point(72, 56);
			this.WidthEdt.Maximum = new System.Decimal(new int[] {
																	 9999,
																	 0,
																	 0,
																	 0});
			this.WidthEdt.Name = "WidthEdt";
			this.WidthEdt.Size = new System.Drawing.Size(72, 19);
			this.WidthEdt.TabIndex = 0;
			this.WidthEdt.ValueChanged += new System.EventHandler(this.WidthEdt_ValueChanged);
			// 
			// HeightEdt
			// 
			this.HeightEdt.Location = new System.Drawing.Point(72, 80);
			this.HeightEdt.Maximum = new System.Decimal(new int[] {
																	  9999,
																	  0,
																	  0,
																	  0});
			this.HeightEdt.Name = "HeightEdt";
			this.HeightEdt.Size = new System.Drawing.Size(72, 19);
			this.HeightEdt.TabIndex = 1;
			this.HeightEdt.ValueChanged += new System.EventHandler(this.HeightEdt_ValueChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 56);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(24, 16);
			this.label1.TabIndex = 2;
			this.label1.Text = "幅";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 80);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(24, 16);
			this.label2.TabIndex = 2;
			this.label2.Text = "高";
			// 
			// pictinfo
			// 
			this.pictinfo.Location = new System.Drawing.Point(16, 32);
			this.pictinfo.Name = "pictinfo";
			this.pictinfo.Size = new System.Drawing.Size(120, 16);
			this.pictinfo.TabIndex = 2;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(16, 8);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(120, 16);
			this.label4.TabIndex = 2;
			this.label4.Text = "元画像サイズ";
			// 
			// VGABtn
			// 
			this.VGABtn.Location = new System.Drawing.Point(16, 104);
			this.VGABtn.Name = "VGABtn";
			this.VGABtn.Size = new System.Drawing.Size(40, 23);
			this.VGABtn.TabIndex = 3;
			this.VGABtn.Text = "VGA";
			this.VGABtn.Click += new System.EventHandler(this.VGABtn_Click);
			// 
			// SVGABtn
			// 
			this.SVGABtn.Location = new System.Drawing.Point(56, 104);
			this.SVGABtn.Name = "SVGABtn";
			this.SVGABtn.Size = new System.Drawing.Size(48, 23);
			this.SVGABtn.TabIndex = 3;
			this.SVGABtn.Text = "SVGA";
			this.SVGABtn.Click += new System.EventHandler(this.SVGABtn_Click);
			// 
			// XGABtn
			// 
			this.XGABtn.Location = new System.Drawing.Point(104, 104);
			this.XGABtn.Name = "XGABtn";
			this.XGABtn.Size = new System.Drawing.Size(40, 23);
			this.XGABtn.TabIndex = 4;
			this.XGABtn.Text = "XGA";
			this.XGABtn.Click += new System.EventHandler(this.XGABtn_Click);
			// 
			// ZoomEdt
			// 
			this.ZoomEdt.Location = new System.Drawing.Point(72, 136);
			this.ZoomEdt.Maximum = new System.Decimal(new int[] {
																	500,
																	0,
																	0,
																	0});
			this.ZoomEdt.Minimum = new System.Decimal(new int[] {
																	10,
																	0,
																	0,
																	0});
			this.ZoomEdt.Name = "ZoomEdt";
			this.ZoomEdt.Size = new System.Drawing.Size(48, 19);
			this.ZoomEdt.TabIndex = 5;
			this.ZoomEdt.Value = new System.Decimal(new int[] {
																  100,
																  0,
																  0,
																  0});
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 136);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 16);
			this.label3.TabIndex = 6;
			this.label3.Text = "画像倍率";
			// 
			// InfoForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(154, 163);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.ZoomEdt);
			this.Controls.Add(this.XGABtn);
			this.Controls.Add(this.VGABtn);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.HeightEdt);
			this.Controls.Add(this.WidthEdt);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.pictinfo);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.SVGABtn);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "InfoForm";
			this.ShowInTaskbar = false;
			this.Text = "画像情報";
			this.TopMost = true;
			((System.ComponentModel.ISupportInitialize)(this.WidthEdt)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.HeightEdt)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ZoomEdt)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// ダイアログの表示フラグ
		/// </summary>
		public bool SizeValueChangedVisible = true;

		/// <summary>
		/// サイズ変更イベント
		/// </summary>
		public event System.EventHandler SizeValueChanged;

		private void WidthEdt_ValueChanged(object sender, System.EventArgs e)
		{
			if( SizeValueChanged != null && SizeValueChangedVisible == true )
				SizeValueChanged( this, e );
		}

		private void HeightEdt_ValueChanged(object sender, System.EventArgs e)
		{
			if( SizeValueChanged != null && SizeValueChangedVisible == true )
				SizeValueChanged( this, e );
		}

		/// <summary>
		/// VGAボタン押下
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void VGABtn_Click(object sender, System.EventArgs e)
		{
			WidthEdt.Value = 640;
			HeightEdt.Value = 480;
		}

		/// <summary>
		/// SVGAボタン押下
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SVGABtn_Click(object sender, System.EventArgs e)
		{
			WidthEdt.Value = 800;
			HeightEdt.Value = 600;
		}

		/// <summary>
		/// XGAボタン押下
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void XGABtn_Click(object sender, System.EventArgs e)
		{
			WidthEdt.Value = 1024;
			HeightEdt.Value = 768;
		}
	}
}
