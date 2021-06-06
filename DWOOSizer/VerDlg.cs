using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace DWOOSizer
{
	/// <summary>
	/// バージョンダイアログ
	/// </summary>
	public class VerDlg : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label VerLbl;
		private System.Windows.Forms.Label label4;
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public VerDlg()
		{
			//
			// Windows フォーム デザイナ サポートに必要です。
			//
			InitializeComponent();

			//バージョン取得
			VerLbl.Text = Application.ProductVersion;


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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(VerDlg));
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.VerLbl = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(128)));
			this.label1.Location = new System.Drawing.Point(40, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(120, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "DWOO サイザー";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(184, 23);
			this.label2.TabIndex = 1;
			this.label2.Text = "Copyright(C) 2005 by WOOSOFT";
			// 
			// label3
			// 
			this.label3.Image = ((System.Drawing.Image)(resources.GetObject("label3.Image")));
			this.label3.Location = new System.Drawing.Point(16, 8);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(16, 16);
			this.label3.TabIndex = 2;
			// 
			// VerLbl
			// 
			this.VerLbl.Location = new System.Drawing.Point(72, 40);
			this.VerLbl.Name = "VerLbl";
			this.VerLbl.Size = new System.Drawing.Size(80, 16);
			this.VerLbl.TabIndex = 3;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(32, 40);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(32, 16);
			this.label4.TabIndex = 4;
			this.label4.Text = "Ver.";
			// 
			// VerDlg
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(194, 91);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.VerLbl);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "VerDlg";
			this.Text = "バージョン情報";
			this.ResumeLayout(false);

		}
		#endregion
	}
}
