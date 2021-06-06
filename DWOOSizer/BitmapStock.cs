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
	/// Bitmap�̃������ߖ�N���X
	/// </summary>
	public class BitmapStock : IDisposable
	{
		/// <summary>
		/// �r��
		/// </summary>
		protected ManualResetEvent m_ready = new ManualResetEvent(true);

		/// <summary>
		/// �e���|�����t�@�C��
		/// </summary>
		protected FileStream m_bitmapfile = null;

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		public BitmapStock()
		{
		}

		/// <summary>
		/// �f�X�g���N�^
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
		/// �r�b�g�}�b�v�v���p�e�B
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
					//NULL�ݒ�̏ꍇ�A�폜
					Dispose();
				}
				else
				{
					//�㏑���̏ꍇ
					Push( value );
				}
			}
		}

		/// <summary>
		/// �r�b�g�}�b�v���e���|�����t�@�C���֕ۑ�
		/// </summary>
		protected void Push( Bitmap bitmap )
		{
			try
			{
				lock (this)
				{
					m_ready.Reset();

					string temp = string.Empty;

					//�����t�@�C�����폜
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

					//�r���쐬
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
		/// �r�b�g�}�b�v���e���|�������畜�A
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
