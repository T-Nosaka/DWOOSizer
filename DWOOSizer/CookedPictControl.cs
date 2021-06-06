using System;
using System.Drawing;
using System.Drawing.Drawing2D;

using LanzNet;

namespace DWOOSizer
{
	/// <summary>
	/// 画像処理管理コントロール
	/// </summary>
	public class CookedPictControl : ResizePictControl
	{
		#region コンポーネント デザイナで生成されたコード 
		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// MainPicture
			// 
			this.MainPicture.Name = "MainPicture";
			this.MainPicture.Size = new System.Drawing.Size(152, 152);
			// 
			// CookedPictControl
			// 
			this.Name = "CookedPictControl";
			this.Size = new System.Drawing.Size(152, 152);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// 画像調整クラス
		/// </summary>
		protected ColorHue m_colorhue = new ColorHue();

		/// <summary>
		/// 彩度プロパティ
		/// </summary>
		public int Saturation
		{
			get
			{
				return m_colorhue.Saturation;
			}
			set
			{
				m_colorhue.Saturation = value;
			}
		}

		/// <summary>
		/// 輝度プロパティ
		/// </summary>
		public int Lightness
		{
			get
			{
				return m_colorhue.Lightness;
			}
			set
			{
				m_colorhue.Lightness = value;
			}
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CookedPictControl()
		{
		}

		/// <summary>
		/// 画像処理
		/// </summary>
		public Bitmap Cooked( Bitmap srcbitmap )
		{
			//送り画像を作成
			using( BitmapUn unbitmapDataSrc = new BitmapUn( srcbitmap ) )
			{
				//受け取り画像を作成
				using( BitmapUn unbitmapDst = new BitmapUn( srcbitmap.Width, srcbitmap.Height ) )
				{
					//画像処理
					m_colorhue.CookedBitmapRgb24( unbitmapDataSrc.BitmapData, unbitmapDst.BitmapData );

					//元画像に反映
					return unbitmapDst.Unlock();
				}
			}
		}
	}
}
