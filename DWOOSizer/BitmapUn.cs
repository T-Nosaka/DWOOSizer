using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;

namespace DWOOSizer
{
	/// <summary>
	/// �A���}�l�[�W�p�r�b�g�}�b�v�Ǘ��B
	/// </summary>
	public class BitmapUn: IDisposable
	{
		/// <summary>
		/// �Ǘ��r�b�g�}�b�v
		/// </summary>
		protected Bitmap m_bitmap;

		/// <summary>
		/// ���b�N�����r�b�g�}�b�v�f�[�^
		/// </summary>
		protected BitmapData m_lockdata = null;

		/// <summary>
		/// ���S�ȃr�b�g�}�b�v�f�[�^�v���p�e�B
		/// </summary>
		public BitmapData BitmapData
		{
			get
			{
				return m_lockdata;
			}
		}

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		/// <param name="iWidht">��</param>
		/// <param name="iHeight">��</param>
		public BitmapUn( int iWidht, int iHeight )
		{
			//�r�b�g�}�b�v�쐬
			m_bitmap = new Bitmap( iWidht, iHeight, PixelFormat.Format24bppRgb );
			Rectangle rectDst = new Rectangle( 0 , 0 , m_bitmap.Width, m_bitmap.Height ) ;
			m_lockdata = m_bitmap.LockBits( rectDst, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb ) ;
		}

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		/// <param name="iWidht">��</param>
		/// <param name="iHeight">��</param>
		public BitmapUn( Bitmap bitmap )
		{
			//�r�b�g�}�b�v�Z�b�g
			m_bitmap = bitmap;
			Rectangle rectDst = new Rectangle( 0 , 0 , m_bitmap.Width, m_bitmap.Height ) ;
			m_lockdata = m_bitmap.LockBits( rectDst, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb ) ;
		}

		/// <summary>
		/// �r�b�g�}�b�v�f�[�^�̉��
		/// </summary>
		public Bitmap Unlock()
		{
			if( m_lockdata != null )
			{
				m_bitmap.UnlockBits( m_lockdata );
				m_lockdata = null;
			}

			return m_bitmap;
		}

		/// <summary>
		/// �f�X�g���N�^
		/// </summary>
		public void Dispose()
		{
			lock( this )
			{
				Unlock();
			}
		}

		/// <summary>
		/// �f�X�g���N�^
		/// </summary>
		~BitmapUn()
		{
			Dispose();
		}
	}
}
