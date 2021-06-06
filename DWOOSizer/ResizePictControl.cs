using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using LanzNet;


namespace DWOOSizer
{
	/// <summary>
	/// �摜��ԃR���g���[��
	/// </summary>
	public partial class ResizePictControl : System.Windows.Forms.UserControl
	{
		/// <summary>
		/// �T�C�Y�����N���X
		/// </summary>
		protected OLanczLngMemNet m_lanznet = null;

		/// <summary>
		/// ��ԃ^�C�v
		/// </summary>
		protected int m_effecttype = 0;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public ResizePictControl()
        {
            // ���̌Ăяo���́AWindows.Forms �t�H�[�� �f�U�C�i�ŕK�v�ł��B
            InitializeComponent();

			this.Disposed += (owner,args) => 
			{
				if (m_lanznet != null)
				{
					m_lanznet.DestroyMemory();
					m_lanznet = null;
				}
			};
        }

		/// <summary>
		/// ��ԃ^�C�v�ݒ�
		/// </summary>
		public int EffectType
		{
			get
			{
				return m_effecttype;
			}
			set
			{
				m_effecttype = value;
				if( m_lanznet != null )
				{
					m_lanznet.EffectType( value );

					ReSizeImage( (uint)this.Width, (uint)this.Height );
				}
			}
		}

		/// <summary>
		/// �摜�h�^�e
		/// </summary>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual Bitmap Bitmap
		{
			get
			{
				return MainPicture.Image as Bitmap;
			}
			set
			{
				if( value == null )
				{
					//�N���A�̏ꍇ�A�m�ۍς݃��������j��
					if( m_lanznet != null )
					{
						m_lanznet.DestroyMemory();
						m_lanznet = null;
					}

					return ;
				}

				//�r�b�g�}�b�v�̃A�^�b�`
				MainPicture.Image = value;

				//�C���[�W���A���}�l�[�W�֓o�^����B
				using ( BitmapUn srcbitmap = new BitmapUn( value ) )
				{
					if( m_lanznet != null )
					{
						m_lanznet.DestroyMemory();
						m_lanznet = null;
					}

					m_lanznet = new OLanczLngMemNet();
					m_lanznet.EffectType( m_effecttype );
					m_lanznet.SetBitmapRgb24( srcbitmap.BitmapData );
				}
			}
		}

		/// <summary>
		/// �s�N�`���R���g���[���v���p�e�B
		/// </summary>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public PictureBox Picture
		{
			get
			{
				return MainPicture;
			}
		}

		/// <summary>
		/// �C���[�W�̃T�C�Y��ύX
		/// </summary>
		public virtual void ReSizeImage( uint dwWidth, uint dwHeight )
		{
			if( m_lanznet == null || dwWidth <= 0 ||  dwHeight <= 0 )
				return ;

			lock( this )
			{
				//��ʕ\���T�C�Y�֒�������B
				m_lanznet.Resize( dwWidth , dwHeight );

				//�󂯎��摜���쐬
				BitmapUn dstbitmap = new BitmapUn( (int)dwWidth, (int)dwHeight );
				//���T�C�Y��A�摜�������R�s�[
				m_lanznet.GetBitmapRgb24( dstbitmap.BitmapData );
				//�s�N�`���[�֐ݒ�
				MainPicture.Image = dstbitmap.Unlock();
			}
		}

	}
}
