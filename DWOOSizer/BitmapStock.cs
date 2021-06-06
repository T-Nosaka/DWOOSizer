using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;

namespace DWOOSizer
{
	/// <summary>
	/// Bitmapのメモリ節約クラス
	/// </summary>
	public class BitmapStock : IDisposable
	{
		/// <summary>
		/// 排他
		/// </summary>
		protected ManualResetEvent m_ready = new ManualResetEvent(true);

		/// <summary>
		/// テンポラリファイル
		/// </summary>
		protected FileStream m_bitmapfile = null;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public BitmapStock()
		{
		}

		/// <summary>
		/// デストラクタ
		/// </summary>
		public void Dispose()
		{
			lock (this)
			{
				if (m_bitmapfile != null)
				{
					m_ready.WaitOne();
					m_bitmapfile.Close();
					File.Delete(m_bitmapfile.Name);
					m_bitmapfile = null;
				}
			}
		}

		/// <summary>
		/// ビットマッププロパティ
		/// </summary>
		public Bitmap Bitmap
		{
			get
			{
				return Pop();
			}
			set
			{
				if( value == null )
				{
					//NULL設定の場合、削除
					Dispose();
				}
				else
				{
					//上書きの場合
					Push( value );
				}
			}
		}

		/// <summary>
		/// ビットマップをテンポラリファイルへ保存
		/// </summary>
		protected void Push( Bitmap bitmap )
		{
			try
			{
				lock (this)
				{
					m_ready.Reset();

					string temp = string.Empty;

					//既存ファイルを削除
					if (m_bitmapfile != null)
					{
						temp = m_bitmapfile.Name;
						m_bitmapfile.Close();
						File.Delete(m_bitmapfile.Name);
					}
					else
					{
						temp = Path.GetTempFileName();
					}

					//排他作成
					m_bitmapfile = new FileStream(temp, FileMode.Create, FileAccess.ReadWrite, FileShare.Write);
				}
				Task.Run(() =>
				{
					bitmap.Save(m_bitmapfile, ImageFormat.Bmp);
					m_ready.Set();
				});
			}
			catch
			{
				Dispose();
			}
		}

		/// <summary>
		/// ビットマップをテンポラリから復帰
		/// </summary>
		protected Bitmap Pop()
		{
			lock(this)
			{
				m_ready.WaitOne();
				m_bitmapfile.Seek(0, SeekOrigin.Begin);
				return new Bitmap(m_bitmapfile);
			}
		}
	}
}
