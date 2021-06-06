using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace DWOOSizer
{
	/// <summary>
	/// タブページ画像
	/// </summary>
	public class TabBmpPage : TabPage, IDisposable
	{
		/// <summary>
		/// 表示元ビットマップ
		/// </summary>
		protected BitmapStock m_bitmap = new BitmapStock();

		/// <summary>
		/// ファイル名　もしくは、その代り
		/// </summary>
		protected string m_filename = string.Empty;

		/// <summary>
		/// ファイル名プロパティ
		/// </summary>
		public string Filename
		{
			get
			{
				return m_filename;
			}
			set
			{
				m_filename = value;
			}
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public TabBmpPage()
		{
		}

		/// <summary>
		/// デストラクタ
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			m_bitmap.Dispose();

			base.Dispose (disposing);
		}

		/// <summary>
		/// 表示元ビットマッププロパティ
		/// </summary>
		public Bitmap SrcBitmap
		{
			get
			{
				return m_bitmap.Bitmap;
			}
			set
			{
				m_bitmap.Bitmap = value;
			}
		}

		/// <summary>
		/// 90度回転
		/// </summary>
		public void Rotate( int pos )
		{
			Bitmap bitmap = SrcBitmap;

			RotateFlipType fliptype = RotateFlipType.Rotate90FlipNone;
			switch( pos )
			{
				case 0:
					fliptype = RotateFlipType.Rotate90FlipNone;
					break;
				case 1:
					fliptype = RotateFlipType.Rotate180FlipNone;
					break;
				case 2:
					fliptype = RotateFlipType.Rotate270FlipNone;
					break;
			}

			bitmap.RotateFlip( fliptype );

			SrcBitmap = bitmap;
		}
	}
}
