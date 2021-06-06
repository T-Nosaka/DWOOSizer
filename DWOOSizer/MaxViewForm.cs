using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using LanzNet;

namespace DWOOSizer
{
	/// <summary>
	/// 最大表示フォーム
	/// </summary>
	public class MaxViewForm : System.Windows.Forms.Form
	{
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.Container components = null;
		private DWOOSizer.ResizePictControl resizePictControl;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public MaxViewForm()
		{
			InitializeComponent();

			//クリックイベント
			resizePictControl.Picture.Click += new EventHandler(resizePictControl_Click);
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

		/// <summary>
		/// 補間タイプ設定
		/// </summary>
		public int EffectType
		{
			set
			{
				resizePictControl.EffectType = value;
			}
		}

		/// <summary>
		/// 画像Ｉ／Ｆ
		/// </summary>
		public Bitmap Bitmap
		{
			get
			{
				return resizePictControl.Bitmap;
			}
			set
			{
				resizePictControl.Bitmap = value;
			}
		}

		#region Windows フォーム デザイナで生成されたコード 
		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.resizePictControl = new DWOOSizer.ResizePictControl();
			this.SuspendLayout();
			// 
			// resizePictControl
			// 
			this.resizePictControl.Bitmap = null;
			this.resizePictControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.resizePictControl.Location = new System.Drawing.Point(0, 0);
			this.resizePictControl.Name = "resizePictControl";
			this.resizePictControl.Size = new System.Drawing.Size(240, 200);
			this.resizePictControl.TabIndex = 0;
			this.resizePictControl.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.resizePictControl_KeyPress);
			// 
			// MaxViewForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(240, 200);
			this.ControlBox = false;
			this.Controls.Add(this.resizePictControl);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "MaxViewForm";
			this.ShowInTaskbar = false;
			this.Text = "MaxViewForm";
			this.TopMost = true;
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Resize += new System.EventHandler(this.MaxViewForm_Resize);
			this.ResumeLayout(false);

		}
		#endregion


		/// <summary>
		/// クリックイベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void resizePictControl_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// キーイベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void resizePictControl_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if( e.KeyChar == 0x1b )
				this.Close();
		}

		/// <summary>
		/// 画像最大化イベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MaxViewForm_Resize(object sender, System.EventArgs e)
		{
			resizePictControl.ReSizeImage( (uint)Width, (uint)Height );
		}
	}
}
