using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Drawing2D;

namespace DWOOSizer.Preview
{
	/// <summary>
	/// ���T�C�Y�A�C�e��
	/// </summary>
	public class PaperSizeItem : RectItem
	{
		/// <summary>
		/// ���ݒ�
		/// </summary>
		protected PageSettings m_pagesetting;
 
		/// <summary>
		/// �c��
		/// </summary>
		protected bool m_landscape;

		/// <summary>
		/// �n�_(�`����W)
		/// </summary>
		protected Point m_startpoint;

		/// <summary>
		/// �n�_�v���p�e�B
		/// </summary>
		public Point StartPoint
		{
			get
			{
				return m_startpoint;
			}
			set
			{
				m_startpoint = value;
			}
		}

		/// <summary>
		/// ���T�C�Y�v���p�e�B
		/// </summary>
		public PageSettings PageSettings
		{
			get
			{
				return m_pagesetting;
			}
			set
			{
				m_pagesetting = value;
			}
		}

		/// <summary>
		/// �c���v���p�e�B
		/// </summary>
		public bool LandScape
		{
			get
			{
				return m_landscape;
			}
			set
			{
				m_landscape = value;
			}
		}

		/// <summary>
		/// �}�[�W���T�C�Y
		/// </summary>
		protected Rectangle m_marginrect ;

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		public PaperSizeItem()
		{
		}

		/// <summary>
		/// �y���쐬
		/// </summary>
		/// <returns></returns>
		protected override Pen CreatePen()
		{
			Pen pen = new Pen(Color.Black, 2 );
			pen.Brush = new SolidBrush( Color.Black );
			pen.Width = 1.0f;

			return pen;
		}

		/// <summary>
		/// �`��C�x���g
		/// </summary>
		/// <param name="gr"></param>
		public override void OnPaint(Graphics gr)
		{
			double height = ((double)m_marginrect.Height)*m_zoom;
			double width = ((double)m_marginrect.Width)*m_zoom;

			double top = ((double)m_marginrect.Y)*m_zoom;
			double left = ((double)m_marginrect.X)*m_zoom;

			Rectangle rect = new Rectangle( (int)((double)m_rect.X + left), (int)((double)m_rect.Y + top),(int)width,(int)height );

			//�y�����쐬
			Pen pen = new Pen(Color.Gray, 2 );
			pen.Brush = new LinearGradientBrush( new Point(0,0), new Point(2,2) , Color.White, Color.Black);
			pen.Width = 1.0f;

			gr.DrawRectangle( pen, rect );

			base.OnPaint (gr);
		}

		/// <summary>
		/// ���g�T�C�Y����
		/// </summary>
		public double SizeAdjust( Graphics gr, Rectangle areasize )
		{
			//������h�b�g�擾
			int iHorzres = 0;
			int iVertres = 0;
			int iMarginLeft = 1;
			int iMarginTop = 1;
			int iCapHorzres = 1;
			int iCapVertres = 1;

			IntPtr hdc = IntPtr.Zero;
			try
			{
				//��������̈�擾
				hdc = gr.GetHdc();

				if( hdc != IntPtr.Zero )
				{
					//���̈�h�b�g��
					iHorzres = DeviceCaps.GetDeviceCaps( hdc, (int)DeviceCaps.GetDeviceCapsFunction.PHYSICALWIDTH );
					iVertres = DeviceCaps.GetDeviceCaps( hdc, (int)DeviceCaps.GetDeviceCapsFunction.PHYSICALHEIGHT );

					//�]���ʒu
					//����}�[�W���g�b�v
					iMarginLeft = DeviceCaps.GetDeviceCaps( hdc, (int)DeviceCaps.GetDeviceCapsFunction.PHYSICALOFFSETX );
					iMarginTop = DeviceCaps.GetDeviceCaps( hdc, (int)DeviceCaps.GetDeviceCapsFunction.PHYSICALOFFSETY );

					//�󎚉\�̈�
					iCapHorzres = DeviceCaps.GetDeviceCaps( hdc, (int)DeviceCaps.GetDeviceCapsFunction.HORZRES );
					iCapVertres = DeviceCaps.GetDeviceCaps( hdc, (int)DeviceCaps.GetDeviceCapsFunction.VERTRES );
				}
			}
			finally
			{
				gr.ReleaseHdc(hdc);
			}

			//�}�[�W���ݒ�
			m_marginrect = new Rectangle( iMarginLeft, iMarginTop, iCapHorzres, iCapVertres);

			double paper_h = (double)iVertres;
			double paper_w = (double)iHorzres;
			double area_h = (double)areasize.Height;
			double area_w = (double)areasize.Width;

			double insize_h = area_h - area_h/10;
			double insize_w = area_w - area_w/10;

			//���̃C���[�W�T�C�Y
			int rectWidth = (int)(insize_h/paper_h*paper_w);
			int rectHeight = (int)(insize_h);

			if( rectWidth > insize_w )
			{
				rectWidth = (int)(insize_w);
				rectHeight = (int)(insize_w/paper_w*paper_h);
			}

			//�Z���^�����O
			m_rect.X = (int)(area_w - rectWidth) / 2;
			m_rect.Y = (int)(area_h - rectHeight) / 2;

			//���ݒ�
			m_rect.Width = (int)paper_w;
			m_rect.Height = (int)paper_h;

			//�g�嗦
			m_zoom = ((double)rectWidth)/paper_w;

			//�n�_�`����W�ݒ�
			m_startpoint = new Point( m_rect.X, m_rect.Y );

			return m_zoom;
		}
	}
}
