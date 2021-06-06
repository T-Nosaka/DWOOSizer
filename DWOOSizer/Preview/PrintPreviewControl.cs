using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace DWOOSizer.Preview
{
	/// <summary>
	/// ����v���r���[�R���g���[��
	/// </summary>
	public class PrintPreviewControl : PreviewControl
	{
		/// <summary>
		/// ���T�C�Y
		/// </summary>
		protected PaperSizeItem m_paperitem = new PaperSizeItem();

		/// <summary>
		/// �摜�C���[�W
		/// </summary>
		protected PreviewItem m_imgitem = new PreviewItem();

		/// <summary>
		/// ���g�嗦
		/// </summary>
		protected double m_zoom = 0.0;

		/// <summary>
		/// �摜�g�嗦
		/// </summary>
		protected double m_imgzoom = 1.0;

		/// <summary>
		/// ����摜
		/// </summary>
		protected Bitmap m_bitmap = new Bitmap(1,1);

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
				m_imgitem.Bitmap = value;

				m_bitmap = value;
			}
		}

		/// <summary>
		/// �摜�g�嗦�v���p�e�B
		/// </summary>
		public double ImageZoom
		{
			get
			{
				return m_imgzoom;
			}
			set
			{
				m_imgzoom = value;
			}
		}

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		public PrintPreviewControl()
		{
			//������ԍ쐬

			//����Ώ�
			AddItem( m_imgitem );

			//���T�C�Y�o�^
			AddItem( m_paperitem );

			//�C�x���g�ݒ�
			m_imgitem.DropEvent += new EventHandler( OnDropImage );

			//�_�u���o�b�t�@�ݒ�
			SetStyle(ControlStyles.DoubleBuffer, true);
			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
		}

		/// <summary>
		/// �g�p����Ă��郊�\�[�X�Ɍ㏈�������s���܂��B
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				Bitmap = null;
			}
			base.Dispose( disposing );
		}

		/// <summary>
		/// ���T�C�Y�Z�b�g
		/// </summary>
		public void SetPaperSize( PageSettings pagesetting, bool bLandscape )
		{
			m_paperitem.PageSettings = pagesetting;
			m_paperitem.LandScape = bLandscape;

			Rectangle areasize = new Rectangle( 0,0, Width, Height );

			//�T�C�Y�������s���g�嗦���擾
			m_zoom = m_paperitem.SizeAdjust( pagesetting.PrinterSettings.CreateMeasurementGraphics(), areasize );

			//�摜�g�嗦���Z�b�g
			m_imgitem.Zoom = m_zoom * m_imgzoom;
		}

		/// <summary>
		/// �摜�ʒu�ݒ�
		/// </summary>
		/// <param name="pos"></param>
		public void SetImagePoint( Point pos )
		{
			int pos_x = (int)(((double)pos.X) * m_zoom);
			int pos_y = (int)(((double)pos.Y) * m_zoom);

			//�摜�g�嗦���Z�b�g
			m_imgitem.Zoom = m_zoom * m_imgzoom;

			m_imgitem.Rect = new Rectangle( m_paperitem.StartPoint.X + pos_x, m_paperitem.StartPoint.Y + pos_y, Bitmap.Width, Bitmap.Height );

			this.Invalidate();
		}

		/// <summary>
		/// �摜�ʒu�擾
		/// </summary>
		/// <returns></returns>
		public Point GetImagePoint()
		{
			double dY = (double)(m_imgitem.Rect.Y - m_paperitem.StartPoint.Y);
			double dX = (double)(m_imgitem.Rect.X - m_paperitem.StartPoint.X);

			dY = dY / m_zoom;
			dX = dX / m_zoom;

			return new Point( (int)dX, (int) dY );
		}

		/// <summary>
		/// �h���b�v�C�x���g
		/// </summary>
		public event EventHandler ImageDropEvent;

		/// <summary>
		/// ����摜�̃h���b�v�C�x���g
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="arg"></param>
		protected void OnDropImage( object sender, EventArgs arg )
		{
			if( ImageDropEvent != null )
			{
				ImageDropEvent( this, arg );
			}
		}

	}
}
