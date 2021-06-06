using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using LanzNet;

namespace DWOOSizer.Effect
{
	/// <summary>
	/// 画像効果設定フォーム
	/// </summary>
	public class EffectForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.PictureBox SamplePic;
		public System.Windows.Forms.TrackBar SaturationBar;
		public System.Windows.Forms.TrackBar LightnessBar;
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// 画像効果クラス
		/// </summary>
		protected ColorHue m_colorhue = new ColorHue();
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;

		/// <summary>
		/// 元画像
		/// </summary>
		protected Bitmap m_srcbitmap = null;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public EffectForm()
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
			this.SamplePic = new System.Windows.Forms.PictureBox();
			this.SaturationBar = new System.Windows.Forms.TrackBar();
			this.LightnessBar = new System.Windows.Forms.TrackBar();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.SaturationBar)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.LightnessBar)).BeginInit();
			this.SuspendLayout();
			// 
			// SamplePic
			// 
			this.SamplePic.Location = new System.Drawing.Point(32, 16);
			this.SamplePic.Name = "SamplePic";
			this.SamplePic.Size = new System.Drawing.Size(176, 144);
			this.SamplePic.TabIndex = 0;
			this.SamplePic.TabStop = false;
			// 
			// SaturationBar
			// 
			this.SaturationBar.LargeChange = 1;
			this.SaturationBar.Location = new System.Drawing.Point(24, 184);
			this.SaturationBar.Maximum = 100;
			this.SaturationBar.Minimum = -100;
			this.SaturationBar.Name = "SaturationBar";
			this.SaturationBar.Size = new System.Drawing.Size(192, 45);
			this.SaturationBar.TabIndex = 1;
			this.SaturationBar.TickStyle = System.Windows.Forms.TickStyle.None;
			this.SaturationBar.ValueChanged += new System.EventHandler(this.SaturationBar_ValueChanged);
			// 
			// LightnessBar
			// 
			this.LightnessBar.LargeChange = 1;
			this.LightnessBar.Location = new System.Drawing.Point(24, 264);
			this.LightnessBar.Maximum = 80;
			this.LightnessBar.Minimum = -80;
			this.LightnessBar.Name = "LightnessBar";
			this.LightnessBar.Size = new System.Drawing.Size(192, 45);
			this.LightnessBar.TabIndex = 2;
			this.LightnessBar.TickStyle = System.Windows.Forms.TickStyle.None;
			this.LightnessBar.ValueChanged += new System.EventHandler(this.LightnessBar_ValueChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(24, 168);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(40, 16);
			this.label1.TabIndex = 3;
			this.label1.Text = "彩度";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(24, 240);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(40, 16);
			this.label2.TabIndex = 3;
			this.label2.Text = "輝度";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(120, 320);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(64, 24);
			this.button1.TabIndex = 4;
			this.button1.Text = "適用";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(40, 320);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(64, 24);
			this.button2.TabIndex = 5;
			this.button2.Text = "リセット";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// EffectForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(234, 360);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.LightnessBar);
			this.Controls.Add(this.SaturationBar);
			this.Controls.Add(this.SamplePic);
			this.Controls.Add(this.label2);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "EffectForm";
			this.Text = "画像効果";
			((System.ComponentModel.ISupportInitialize)(this.SaturationBar)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.LightnessBar)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// 彩度調整イベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SaturationBar_ValueChanged(object sender, System.EventArgs e)
		{
			OnEffect();
		}

		/// <summary>
		/// 輝度調整イベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LightnessBar_ValueChanged(object sender, System.EventArgs e)
		{
			OnEffect();
		}
	
		/// <summary>
		/// 画像処理イベント発生
		/// </summary>
		protected void OnEffect()
		{
			if( m_srcbitmap == null )
				return;

			//送り画像を作成
			using( BitmapUn unbitmapDataSrc = new BitmapUn( m_srcbitmap ) )
			{
				//受け取り画像を作成
				using( BitmapUn unbitmapDst = new BitmapUn( SamplePic.Width, SamplePic.Height ) )
				{
					m_colorhue.Saturation = SaturationBar.Value;
					m_colorhue.Lightness = LightnessBar.Value;

					//画像処理
					m_colorhue.CookedBitmapRgb24( unbitmapDataSrc.BitmapData, unbitmapDst.BitmapData );

					//画像に反映
					SamplePic.Image = unbitmapDst.Unlock();
				}
			}

		}

		/// <summary>
		/// 適用ボタン押下
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button1_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.OK;
		}

		/// <summary>
		/// リセットボタン押下
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button2_Click(object sender, System.EventArgs e)
		{
			SaturationBar.Value = 0;
			LightnessBar.Value = 0;
		}

		/// <summary>
		/// 対象ビットマップ
		/// </summary>
		public Bitmap Bitmap
		{
			set
			{
				//イメージをアンマネージへ登録する。
				using( BitmapUn bitmapsrc = new BitmapUn( value ) )
				{
					OLanczLngMemNet lanznet = new OLanczLngMemNet();
					lanznet.EffectType( 0 );
					lanznet.SetBitmapRgb24( bitmapsrc.BitmapData );

					//画面表示サイズへ調整する。
					lanznet.Resize( (uint)SamplePic.Width , (uint)SamplePic.Height );

					using( BitmapUn bitmapdst = new BitmapUn( SamplePic.Width, SamplePic.Height ) )
					{
						//リサイズ後、画像メモリコピー
						lanznet.GetBitmapRgb24( bitmapdst.BitmapData );

						//イメージをセット
						m_srcbitmap = bitmapdst.Unlock();
						SamplePic.Image = m_srcbitmap;

						//調整クラス破棄
						lanznet.DestroyMemory();
						lanznet = null;
					}
				}
			}
		}
	}
}
