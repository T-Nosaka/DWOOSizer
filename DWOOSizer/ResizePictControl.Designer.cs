namespace DWOOSizer
{
	partial class ResizePictControl
	{
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.Container components = null;

		protected System.Windows.Forms.PictureBox MainPicture;


		/// <summary>
		/// 使用されているリソースに後処理を実行します。
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region コンポーネント デザイナで生成されたコード 
		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
            this.MainPicture = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.MainPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // MainPicture
            // 
            this.MainPicture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPicture.Location = new System.Drawing.Point(0, 0);
            this.MainPicture.Name = "MainPicture";
            this.MainPicture.Size = new System.Drawing.Size(186, 180);
            this.MainPicture.TabIndex = 0;
            this.MainPicture.TabStop = false;
            // 
            // ResizePictControl
            // 
            this.Controls.Add(this.MainPicture);
            this.Name = "ResizePictControl";
            this.Size = new System.Drawing.Size(186, 180);
            ((System.ComponentModel.ISupportInitialize)(this.MainPicture)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion
	}
}
