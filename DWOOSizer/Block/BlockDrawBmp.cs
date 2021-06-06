using System;
using System.Collections;
using System.Drawing;

using LanzNet;

namespace DWOOSizer.Block
{
	/// <summary>
	/// 画像描画ブロック管理
	/// </summary>
	public class BlockDrawBmp : BlockBmp
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public BlockDrawBmp()
		{
//			m_iBlockWidth = 200;
//			m_iBlockHeight = 300;
		}

		/// <summary>
		/// GDIに展開
		/// </summary>
		/// <param name="resizer"></param>
		/// <param name="gr"></param>
		/// <param name="srcbitmap"></param>
		/// <param name="no"></param>
		/// <param name="offsetX"></param>
		/// <param name="offsetY"></param>
		public void DrawBitmap( OLanczLngMemNet resizer, Graphics gr, Bitmap srcbitmap, int no, int offsetX, int offsetY )
		{
			//元画像を切り出す
			Rectangle srcrect = GetSquare( no );

			//元画像を登録
			BitmapUn srcunbitmap = new BitmapUn( srcbitmap );
			resizer.SetBitmapRgb24( srcunbitmap.BitmapData );
			srcunbitmap.Unlock();
			srcunbitmap = null;

			//切りだす
			BitmapUn dstunbitmap = new BitmapUn( srcrect.Width, srcrect.Height );
			resizer.TrimBitmapRgb24( dstunbitmap.BitmapData, (uint)srcrect.X, (uint)srcrect.Width, (uint)srcrect.Y, (uint)srcrect.Height );
			resizer.SetBitmapRgb24( dstunbitmap.BitmapData );
			Bitmap dstbitmap = dstunbitmap.Unlock();
			dstunbitmap = null;

			//描画
			gr.DrawImage( dstbitmap, offsetX + srcrect.X, offsetY + srcrect.Y, dstbitmap.Width, dstbitmap.Height );
		}
	}
}
