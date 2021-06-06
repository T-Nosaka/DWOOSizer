using System;
using System.Collections;
using System.Drawing;

namespace DWOOSizer.Block
{
	/// <summary>
	/// �摜�u���b�N�Ǘ�
	/// </summary>
	public class BlockBmp
	{
		/// <summary>
		/// ��{�u���b�N�T�C�Y��
		/// </summary>
		protected int m_iBlockWidth = 800;

		/// <summary>
		/// ��{�u���b�N�T�C�Y��
		/// </summary>
		protected int m_iBlockHeight = 800;

		/// <summary>
		/// ��{�u���b�N�T�C�Y�]����
		/// </summary>
		protected int m_iBlockExtraWidth = 100;

		/// <summary>
		/// ��{�u���b�N�T�C�Y�]����
		/// </summary>
		protected int m_iBlockExtraHeight = 80;

		/// <summary>
		/// �u���b�N���X�g
		/// </summary>
		protected ArrayList m_blocklist = new ArrayList();

		/// <summary>
		/// �]���u���b�N���X�g
		/// </summary>
		protected ArrayList m_extrablocklist = new ArrayList();

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		public BlockBmp()
		{
		}

		/// <summary>
		/// �傫���u���b�N�𕪂���
		/// </summary>
		/// <returns></returns>
		public int DivideSquare( int iWidth, int iHeight )
		{
			m_blocklist = new ArrayList();

			//��{�u���b�N�T�C�Y
			int iBlockWidth = m_iBlockWidth ;
			int iBlockHeight = m_iBlockHeight ;

			//�����̔z��
			int iWidthCnt = iWidth / iBlockWidth;
			//�����̔z��
			int iHeightCnt = iHeight / iBlockHeight;

			if( iWidthCnt == 0 )
				iWidthCnt++;
			if( iHeightCnt == 0 )
				iHeightCnt++;

			//�S�̂̃u���b�N��
			int iBlockCnt = iWidthCnt * iHeightCnt ;

			for( int iHeightPos = 0; iHeightPos < iHeightCnt; iHeightPos++ )
			{
				for( int iWidthPos = 0; iWidthPos < iWidthCnt; iWidthPos++ )
				{
					//�ŏI�u���b�N�T�C�Y����
					int iWidthSize = iBlockWidth;
					if( iWidthPos == iWidthCnt-1 )
						iWidthSize += ( iWidth%iBlockWidth );

					int iHeightSize = iBlockHeight;
					if( iHeightPos == iHeightCnt-1 )
						iHeightSize += ( iHeight%iBlockHeight );

					Rectangle rect = new Rectangle( iWidthPos * iBlockWidth, iHeightPos * iBlockHeight, iWidthSize, iHeightSize );
					m_blocklist.Add( rect );

					//�u���b�N�T�C�Y���Z�o���A���X�g�֒ǉ�
					int iLastLeft = iWidthPos * iBlockWidth - m_iBlockExtraWidth ;
					int iLastTop = iHeightPos * iBlockHeight - m_iBlockExtraHeight ;
					int iLastWidth = iWidthSize + m_iBlockExtraWidth*2;
					int iLastHeight = iHeightSize + m_iBlockExtraHeight*2;

					//�[����
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
		/// �����u���b�N�擾
		/// </summary>
		/// <param name="no"></param>
		/// <returns></returns>
		public Rectangle GetSquare( int no )
		{
			return (Rectangle)m_blocklist[ no ];
		}

		/// <summary>
		/// �]�������u���b�N�擾
		/// </summary>
		/// <param name="no"></param>
		/// <returns></returns>
		public Rectangle GetExtraSquare( int no )
		{
			return (Rectangle)m_extrablocklist[ no ];
		}

	}
}
