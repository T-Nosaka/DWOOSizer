using System;
using System.Drawing;

using LanzNet;

namespace DWOOSizer.Preview
{
	/// <summary>
	/// ���z�摜�A�C�e��
	/// </summary>
	public class VirtualPictItem : RectItem
	{
		/// <summary>
		/// �T�C�Y�����N���X
		/// </summary>
		protected OLanczLngMemNet m_lanznet ;

		/// <summary>
		/// ����摜
		/// </summary>
		protected Bitmap m_bitmap = null;

		/// <summary>
		/// ����摜�v���p�e�B
		/// </summary>
		public Bitmap Bitmap
		{
			get
			{
				return m_bitmap;
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
				m_bitmap = value;

				//�C���[�W���A���}�l�[�W�֓o�^����B
				using ( BitmapUn srcbitmap = new BitmapUn( value ) )
				{
					if( m_lanznet != null )
					{
						m_lanznet.DestroyMemory();
						m_lanznet = null;
					}

					m_lanznet = new OLanczLngMemNet();
					m_lanznet.EffectType( 0 );
					m_lanznet.SetBitmapRgb24( srcbitmap.BitmapData );
				}
			}
		}
		
		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		public VirtualPictItem()
		{
		}

		/// <summary>
		/// �y���쐬
		/// </summary>
		/// <returns></returns>
		protected override Pen CreatePen()
		{
			Pen pen = new Pen(Color.FromArgb(128, Color.Black), 1 );
			pen.Brush = new SolidBrush( Color.Black );
			pen.Width = 1.0f;

			return pen;
		}

		/// <summary>
		/// �`��C�x���g
		/// </summary>
		/// <param name="gr"></param>
		public override void OnPaint(System.Drawing.Graphics gr)
		{
			double height = ((double)m_rect.Height)*m_zoom;
			double width = ((double)m_rect.Width)*m_zoom;

			if( m_bitmap != null )
			{
				//��ʕ\���T�C�Y�֒�������B
				m_lanznet.Resize( (uint)width , (uint)height );

				//�󂯎��摜���쐬
				BitmapUn unbitmapDst = new BitmapUn( (int)width, (int)height );

				//���T�C�Y��A�摜�������R�s�[
				m_lanznet.GetBitmapRgb24( unbitmapDst.BitmapData );

				//�摜�\��t��
				gr.DrawImage( unbitmapDst.Unlock(), m_rect.X,m_rect.Y,(int)width,(int)height );
			}

			//��ɘg������
			base.OnPaint (gr);
		}
	}
}
