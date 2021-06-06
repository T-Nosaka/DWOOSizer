using System;
using System.Collections;
using System.Drawing;

using LanzNet;

namespace DWOOSizer.Block
{
	/// <summary>
	/// �摜�`��u���b�N�Ǘ�
	/// </summary>
	public class BlockDrawBmp : BlockBmp
	{
		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		public BlockDrawBmp()
		{
//			m_iBlockWidth = 200;
//			m_iBlockHeight = 300;
		}

		/// <summary>
		/// GDI�ɓW�J
		/// </summary>
		/// <param name="resizer"></param>
		/// <param name="gr"></param>
		/// <param name="srcbitmap"></param>
		/// <param name="no"></param>
		/// <param name="offsetX"></param>
		/// <param name="offsetY"></param>
		public void DrawBitmap( OLanczLngMemNet resizer, Graphics gr, Bitmap srcbitmap, int no, int offsetX, int offsetY )
		{
			//���摜��؂�o��
			Rectangle srcrect = GetSquare( no );

			//���摜��o�^
			BitmapUn srcunbitmap = new BitmapUn( srcbitmap );
			resizer.SetBitmapRgb24( srcunbitmap.BitmapData );
			srcunbitmap.Unlock();
			srcunbitmap = null;

			//�؂肾��
			BitmapUn dstunbitmap = new BitmapUn( srcrect.Width, srcrect.Height );
			resizer.TrimBitmapRgb24( dstunbitmap.BitmapData, (uint)srcrect.X, (uint)srcrect.Width, (uint)srcrect.Y, (uint)srcrect.Height );
			resizer.SetBitmapRgb24( dstunbitmap.BitmapData );
			Bitmap dstbitmap = dstunbitmap.Unlock();
			dstunbitmap = null;

			//�`��
			gr.DrawImage( dstbitmap, offsetX + srcrect.X, offsetY + srcrect.Y, dstbitmap.Width, dstbitmap.Height );
		}
	}
}
