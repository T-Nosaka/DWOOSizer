using System;
using System.Collections;
using System.Drawing;

using LanzNet;

namespace DWOOSizer.Block
{
	/// <summary>
	/// 画像リサイズブロック管理
	/// </summary>
	public class BlockResizeBmp : BlockBmp
	{
		/// <summary>
		/// 元画像ブロックリスト
		/// </summary>
		protected ArrayList m_srcblocklist = new ArrayList();

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public BlockResizeBmp()
		{
		}

		/// <summary>
		/// ブロックを分ける
		/// </summary>
		/// <returns></returns>
		public int DivideSquare( int iWidth, int iHeight, int iSrcWidth, int iSrcHeight )
		{
			decimal dWidth = iWidth;
			decimal dSrcWidth = iSrcWidth;
			decimal dHeight = iHeight;
			decimal dSrcHeight = iSrcHeight;

			decimal dExtraWidthPer = dSrcWidth / dWidth;
			decimal dExtraHeightPer = dSrcHeight / dHeight ;

			//補正余分領域を調整
			decimal dSrcExtraLineWidth = (30.0m * dWidth / dSrcWidth );
			decimal dSrcExtraLineHeight = (30.0m * dHeight / dSrcHeight );
			if( dSrcExtraLineWidth < 1 )
				dSrcExtraLineWidth = 1;
			if( dSrcExtraLineHeight < 1 )
				dSrcExtraLineHeight = 1;

			m_iBlockExtraWidth = (int)dSrcExtraLineWidth;
			m_iBlockExtraHeight = (int)dSrcExtraLineHeight;

			int iBlockCnt = base.DivideSquare( iWidth, iHeight );

			foreach( Rectangle rect in m_extrablocklist )
			{
				decimal dSrcBlockX = rect.X;
				decimal dSrcBlockWidth = rect.Width;

				decimal dSrcPointX = dSrcBlockX * dExtraWidthPer;
				decimal dSrcPointWidth = dSrcBlockWidth * dExtraWidthPer;

				decimal dSrcBlockY = rect.Y;
				decimal dSrcBlockHeight = rect.Height;

				decimal dSrcPointY = dSrcBlockY * dExtraHeightPer;
				decimal dSrcPointHeight = dSrcBlockHeight * dExtraHeightPer;

				//ブロックサイズを算出し、リストへ追加
				Rectangle srcrect = new Rectangle( (int)dSrcPointX, (int)dSrcPointY, (int)dSrcPointWidth, (int)dSrcPointHeight );
				m_srcblocklist.Add( srcrect );
			}

			return iBlockCnt;
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
			Rectangle srcrect = (Rectangle)m_srcblocklist[ no ];

			//元画像を登録
			BitmapUn srcunbitmap = new BitmapUn( srcbitmap );
			resizer.SetBitmapRgb24( srcunbitmap.BitmapData );
			srcunbitmap.Unlock();
			srcunbitmap = null;

			//切りだして、拡縮セット
			BitmapUn dstunbitmap = new BitmapUn( srcrect.Width, srcrect.Height );
			resizer.TrimBitmapRgb24( dstunbitmap.BitmapData, (uint)srcrect.X, (uint)srcrect.Width, (uint)srcrect.Y, (uint)srcrect.Height );
			resizer.SetBitmapRgb24( dstunbitmap.BitmapData );
			dstunbitmap.Unlock();
			dstunbitmap = null;

			//拡大縮小処理
			Rectangle resizerect = base.GetExtraSquare( no );
			resizer.Resize( (uint)resizerect.Width, (uint)resizerect.Height );
			//拡縮画像取得
			BitmapUn resizeunbitmap = new BitmapUn( resizerect.Width, resizerect.Height );
			resizer.GetBitmapRgb24( resizeunbitmap.BitmapData );
			Bitmap resizebitmap = resizeunbitmap.Unlock();

			//描画
			Rectangle cliprect = base.GetSquare( no );
			gr.SetClip(new Rectangle( offsetX + cliprect.X, offsetY + cliprect.Y, cliprect.Width+1, cliprect.Height+1 ) );

			BlockDrawBmp blockbmp = new BlockDrawBmp();
			int blockcnt = blockbmp.DivideSquare( resizebitmap.Width, resizebitmap.Height );

			if( blockcnt == 1 )
			{
				//描画
				gr.DrawImage( resizebitmap, offsetX + resizerect.X, offsetY + resizerect.Y, resizebitmap.Width, resizebitmap.Height );
			}
			else
			{
				//ブロック内で大きい場合、さらに分けて描画
				for( int idx=0; idx<blockcnt; idx++ )
				{
					blockbmp.DrawBitmap( resizer, gr, resizebitmap, idx, offsetX + resizerect.X, offsetY + resizerect.Y );
				}
			}
		}
	}
}
