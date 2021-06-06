using System;
using System.Collections;
using System.Drawing;

using LanzNet;

namespace DWOOSizer.Block
{
	/// <summary>
	/// �摜���T�C�Y�u���b�N�Ǘ�
	/// </summary>
	public class BlockResizeBmp : BlockBmp
	{
		/// <summary>
		/// ���摜�u���b�N���X�g
		/// </summary>
		protected ArrayList m_srcblocklist = new ArrayList();

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		public BlockResizeBmp()
		{
		}

		/// <summary>
		/// �u���b�N�𕪂���
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

			//�␳�]���̈�𒲐�
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

				//�u���b�N�T�C�Y���Z�o���A���X�g�֒ǉ�
				Rectangle srcrect = new Rectangle( (int)dSrcPointX, (int)dSrcPointY, (int)dSrcPointWidth, (int)dSrcPointHeight );
				m_srcblocklist.Add( srcrect );
			}

			return iBlockCnt;
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
			Rectangle srcrect = (Rectangle)m_srcblocklist[ no ];

			//���摜��o�^
			BitmapUn srcunbitmap = new BitmapUn( srcbitmap );
			resizer.SetBitmapRgb24( srcunbitmap.BitmapData );
			srcunbitmap.Unlock();
			srcunbitmap = null;

			//�؂肾���āA�g�k�Z�b�g
			BitmapUn dstunbitmap = new BitmapUn( srcrect.Width, srcrect.Height );
			resizer.TrimBitmapRgb24( dstunbitmap.BitmapData, (uint)srcrect.X, (uint)srcrect.Width, (uint)srcrect.Y, (uint)srcrect.Height );
			resizer.SetBitmapRgb24( dstunbitmap.BitmapData );
			dstunbitmap.Unlock();
			dstunbitmap = null;

			//�g��k������
			Rectangle resizerect = base.GetExtraSquare( no );
			resizer.Resize( (uint)resizerect.Width, (uint)resizerect.Height );
			//�g�k�摜�擾
			BitmapUn resizeunbitmap = new BitmapUn( resizerect.Width, resizerect.Height );
			resizer.GetBitmapRgb24( resizeunbitmap.BitmapData );
			Bitmap resizebitmap = resizeunbitmap.Unlock();

			//�`��
			Rectangle cliprect = base.GetSquare( no );
			gr.SetClip(new Rectangle( offsetX + cliprect.X, offsetY + cliprect.Y, cliprect.Width+1, cliprect.Height+1 ) );

			BlockDrawBmp blockbmp = new BlockDrawBmp();
			int blockcnt = blockbmp.DivideSquare( resizebitmap.Width, resizebitmap.Height );

			if( blockcnt == 1 )
			{
				//�`��
				gr.DrawImage( resizebitmap, offsetX + resizerect.X, offsetY + resizerect.Y, resizebitmap.Width, resizebitmap.Height );
			}
			else
			{
				//�u���b�N���ő傫���ꍇ�A����ɕ����ĕ`��
				for( int idx=0; idx<blockcnt; idx++ )
				{
					blockbmp.DrawBitmap( resizer, gr, resizebitmap, idx, offsetX + resizerect.X, offsetY + resizerect.Y );
				}
			}
		}
	}
}
