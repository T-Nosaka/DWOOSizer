using System;
using System.Collections;
using System.Drawing;

namespace DWOOSizer.Block
{
	/// <summary>
	/// 画像ブロック管理
	/// </summary>
	public class BlockBmp
	{
		/// <summary>
		/// 基本ブロックサイズ幅
		/// </summary>
		protected int m_iBlockWidth = 800;

		/// <summary>
		/// 基本ブロックサイズ高
		/// </summary>
		protected int m_iBlockHeight = 800;

		/// <summary>
		/// 基本ブロックサイズ余分幅
		/// </summary>
		protected int m_iBlockExtraWidth = 100;

		/// <summary>
		/// 基本ブロックサイズ余分高
		/// </summary>
		protected int m_iBlockExtraHeight = 80;

		/// <summary>
		/// ブロックリスト
		/// </summary>
		protected ArrayList m_blocklist = new ArrayList();

		/// <summary>
		/// 余分ブロックリスト
		/// </summary>
		protected ArrayList m_extrablocklist = new ArrayList();

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public BlockBmp()
		{
		}

		/// <summary>
		/// 大きいブロックを分ける
		/// </summary>
		/// <returns></returns>
		public int DivideSquare( int iWidth, int iHeight )
		{
			m_blocklist = new ArrayList();

			//基本ブロックサイズ
			int iBlockWidth = m_iBlockWidth ;
			int iBlockHeight = m_iBlockHeight ;

			//水平の配列数
			int iWidthCnt = iWidth / iBlockWidth;
			//垂直の配列数
			int iHeightCnt = iHeight / iBlockHeight;

			if( iWidthCnt == 0 )
				iWidthCnt++;
			if( iHeightCnt == 0 )
				iHeightCnt++;

			//全体のブロック数
			int iBlockCnt = iWidthCnt * iHeightCnt ;

			for( int iHeightPos = 0; iHeightPos < iHeightCnt; iHeightPos++ )
			{
				for( int iWidthPos = 0; iWidthPos < iWidthCnt; iWidthPos++ )
				{
					//最終ブロックサイズ調整
					int iWidthSize = iBlockWidth;
					if( iWidthPos == iWidthCnt-1 )
						iWidthSize += ( iWidth%iBlockWidth );

					int iHeightSize = iBlockHeight;
					if( iHeightPos == iHeightCnt-1 )
						iHeightSize += ( iHeight%iBlockHeight );

					Rectangle rect = new Rectangle( iWidthPos * iBlockWidth, iHeightPos * iBlockHeight, iWidthSize, iHeightSize );
					m_blocklist.Add( rect );

					//ブロックサイズを算出し、リストへ追加
					int iLastLeft = iWidthPos * iBlockWidth - m_iBlockExtraWidth ;
					int iLastTop = iHeightPos * iBlockHeight - m_iBlockExtraHeight ;
					int iLastWidth = iWidthSize + m_iBlockExtraWidth*2;
					int iLastHeight = iHeightSize + m_iBlockExtraHeight*2;

					//端処理
					iLastLeft = ( iLastLeft < 0 ) ? 0:iLastLeft;
					iLastTop = ( iLastTop < 0 ) ? 0:iLastTop;
					if( iLastLeft == 0 )
						iLastWidth -= m_iBlockExtraWidth;
					if( iLastTop == 0 )
						iLastHeight -= m_iBlockExtraHeight;
					iLastWidth = ( iLastLeft+iLastWidth > iWidth ) ? ( iWidth - iLastLeft )+1:iLastWidth;
					iLastHeight = ( iLastTop+iLastHeight > iHeight ) ? ( iHeight - iLastTop )+1:iLastHeight;

					Rectangle exrect = new Rectangle( iLastLeft, iLastTop, iLastWidth, iLastHeight );
					m_extrablocklist.Add( exrect );
				}
			}

			return iBlockCnt;
		}

		/// <summary>
		/// 分割ブロック取得
		/// </summary>
		/// <param name="no"></param>
		/// <returns></returns>
		public Rectangle GetSquare( int no )
		{
			return (Rectangle)m_blocklist[ no ];
		}

		/// <summary>
		/// 余分分割ブロック取得
		/// </summary>
		/// <param name="no"></param>
		/// <returns></returns>
		public Rectangle GetExtraSquare( int no )
		{
			return (Rectangle)m_extrablocklist[ no ];
		}

	}
}
