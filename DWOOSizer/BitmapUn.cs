using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;

namespace DWOOSizer
{
	/// <summary>
	/// アンマネージ用ビットマップ管理。
	/// </summary>
	public class BitmapUn: IDisposable
	{
		/// <summary>
		/// 管理ビットマップ
		/// </summary>
		protected Bitmap m_bitmap;

		/// <summary>
		/// ロックしたビットマップデータ
		/// </summary>
		protected BitmapData m_lockdata = null;

		/// <summary>
		/// 安全なビットマップデータプロパティ
		/// </summary>
		public BitmapData BitmapData
		{
			get
			{
				return m_lockdata;
			}
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="iWidht">幅</param>
		/// <param name="iHeight">高</param>
		public BitmapUn( int iWidht, int iHeight )
		{
			//ビットマップ作成
			m_bitmap = new Bitmap( iWidht, iHeight, PixelFormat.Format24bppRgb );
			Rectangle rectDst = new Rectangle( 0 , 0 , m_bitmap.Width, m_bitmap.Height ) ;
			m_lockdata = m_bitmap.LockBits( rectDst, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb ) ;
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="iWidht">幅</param>
		/// <param name="iHeight">高</param>
		public BitmapUn( Bitmap bitmap )
		{
			//ビットマップセット
			m_bitmap = bitmap;
			Rectangle rectDst = new Rectangle( 0 , 0 , m_bitmap.Width, m_bitmap.Height ) ;
			m_lockdata = m_bitmap.LockBits( rectDst, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb ) ;
		}

		/// <summary>
		/// ビットマップデータの解放
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
		/// デストラクタ
		/// </summary>
		public void Dispose()
		{
			lock( this )
			{
				Unlock();
			}
		}

		/// <summary>
		/// デストラクタ
		/// </summary>
		~BitmapUn()
		{
			Dispose();
		}
	}
}
