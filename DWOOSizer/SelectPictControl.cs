using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DWOOSizer
{
	/// <summary>
	/// �I��t���摜��ԃR���g���[��
	/// </summary>
	public class SelectPictControl : CookedPictControl
	{
		/// <summary>
		/// �I����`
		/// </summary>
		protected Rectangle m_marque = new Rectangle( 0,0,0,0);

		/// <summary>
		/// ���摜�ɑ΂��Ă̑I����`
		/// </summary>
		protected Rectangle m_selectarea = new Rectangle( 0,0,0,0);

		/// <summary>
		/// �h���b�O���t���O
		/// </summary>
		protected bool m_drag = false;

		/// <summary>
		/// �I�����
		/// </summary>
		protected bool m_selected = false;
		private System.Windows.Forms.Timer SelectTimer;
		private System.ComponentModel.IContainer components;

		/// <summary>
		/// ���̉摜
		/// </summary>
		protected Bitmap m_srcbitmap = null;

		/// <summary>
		/// ���C���X�^�C���X�C�b�`
		/// </summary>
		protected bool m_linestyle = false;

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		public SelectPictControl()
		{
			InitializeComponent();
		}

		#region �R���|�[�l���g �f�U�C�i�Ő������ꂽ�R�[�h 
		/// <summary>
		/// �f�U�C�i �T�|�[�g�ɕK�v�ȃ��\�b�h�ł��B���̃��\�b�h�̓��e��
		/// �R�[�h �G�f�B�^�ŕύX���Ȃ��ł��������B
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.SelectTimer = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// MainPicture
			// 
			this.MainPicture.Name = "MainPicture";
			this.MainPicture.Paint += new System.Windows.Forms.PaintEventHandler(this.MainPicture_Paint);
			this.MainPicture.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainPicture_MouseUp);
			this.MainPicture.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainPicture_MouseMove);
			this.MainPicture.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainPicture_MouseDown);
			// 
			// SelectTimer
			// 
			this.SelectTimer.Interval = 1000;
			this.SelectTimer.Tick += new System.EventHandler(this.SelectTimer_Tick);
			// 
			// SelectPictControl
			// 
			this.Name = "SelectPictControl";
			this.Resize += new System.EventHandler(this.SelectPictControl_Resize);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// ���摜�ɑ΂��Ă̑I����`�v���p�e�B
		/// </summary>
		public Bitmap SelectBitmap
		{
			get
			{
				if( m_selectarea.Width <= 0 || m_selectarea.Height <= 0 )
					return null;

				//���r�b�g�}�b�v����g��������B
				//�󂯎��摜���쐬
				using( BitmapUn bitmapDst = new BitmapUn( m_selectarea.Width, m_selectarea.Height ) )
				{
					//�g��������
					m_lanznet.TrimBitmapRgb24( bitmapDst.BitmapData, (uint)m_selectarea.X, (uint)m_selectarea.Width, (uint)m_selectarea.Y, (uint)m_selectarea.Height );

					return bitmapDst.Unlock();
				}
			}
		}

		/// <summary>
		/// �I����ԃv���p�e�B
		/// </summary>
		public bool Selected
		{
			get
			{
				return m_selected;
			}
			set
			{
				m_selected = value;
				MainPicture.Invalidate( );

				//�I��̈�`��^�C�}�[�ݒ�
				SelectTimer.Enabled = value;

			}
		}

		/// <summary>
		/// �摜�h�^�e
		/// </summary>
		public override Bitmap Bitmap
		{
			set
			{
//				if( value == null )
//					return ;

				m_srcbitmap = value;

				base.Bitmap = value;
			}
		}

		/// <summary>
		/// �}�E�X�_�E��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainPicture_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if( m_lanznet == null || m_srcbitmap == null )
				return ;

			m_marque.X = e.X;
			m_marque.Width = 0;
			m_marque.Y = e.Y;
			m_marque.Height = 0;

			m_drag = true;
			this.Selected = false;
		}

		/// <summary>
		/// �}�E�X�ړ�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainPicture_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if( m_lanznet == null || m_srcbitmap == null )
				return ;

			if( m_drag == true )
			{
				m_marque.Width = e.X - m_marque.X;
				m_marque.Height = e.Y - m_marque.Y;

				this.Selected = true;
			}
		}

		/// <summary>
		/// �}�E�X�A�b�v
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainPicture_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if( m_lanznet == null || m_srcbitmap == null || Width <= 0 || Height <= 0 )
			{
				this.Selected = false;
				return ;
			}

			m_drag = false;

			//���W�n���C��
			if( m_marque.Width < 0 )
			{
				m_marque.X = m_marque.X + m_marque.Width;
				m_marque.Width = -m_marque.Width;
			}
			if( m_marque.Height < 0 )
			{
				m_marque.Y = m_marque.Y + m_marque.Height;
				m_marque.Height = -m_marque.Height;
			}

			//�摜�T�C�Y�̃I�[�o�[�t���[�C��
			if( m_marque.X < 0 )
			{
				m_marque.Width += m_marque.X;
				m_marque.X = 0;
			}
			if( (m_marque.X+m_marque.Width) > Width )
			{
				m_marque.Width = Width - m_marque.X;
			}
			if( m_marque.Y < 0 )
			{
				m_marque.Height += m_marque.Y;
				m_marque.Y = 0;
			}
			if( (m_marque.Y+m_marque.Height) > Height )
			{
				m_marque.Height = Height - m_marque.Y;
			}

			//�I��͈͂����摜�̍��W�Ōv�Z
			double srcwidth = (double)m_srcbitmap.Width ;
			double destwidth = (double)Width;
			double marque_x = (double)m_marque.X;
			double marque_width = (double)m_marque.Width;

			double srcheight = (double)m_srcbitmap.Height ;
			double destheight = (double)Height;
			double marque_y = (double)m_marque.Y;
			double marque_height = (double)m_marque.Height;

			//�����g�嗦�Ōv�Z
			double zoomper = srcwidth / destwidth ;
			m_selectarea.X = (int)(marque_x * zoomper);
			m_selectarea.Width = (int)(marque_width * zoomper);

			zoomper = srcheight / destheight ;
			m_selectarea.Y = (int)(marque_y * zoomper);
			m_selectarea.Height = (int)(marque_height * zoomper);
		}

		/// <summary>
		/// �}�[�L�[�`��
		/// </summary>
		/// <param name="g"></param>
		protected void DrawMarque( Graphics g, bool sw )
		{
			if( m_lanznet == null || m_srcbitmap == null )
				return ;

			lock( this )
			{
				if( Selected == true )
				{
					Rectangle marque = new Rectangle( m_marque.X, m_marque.Y, m_marque.Width, m_marque.Height );

					//���W�n���C��
					if( marque.Width < 0 )
					{
						marque.X = marque.X + marque.Width;
						marque.Width = -marque.Width;
					}
					if( marque.Height < 0 )
					{
						marque.Y = marque.Y + marque.Height;
						marque.Height = -marque.Height;
					}

					//�y�����쐬
					Pen pen = new Pen(Color.Black, 2 );
					pen.Brush = new LinearGradientBrush( new Point(0,0), new Point(2,2) , Color.White, Color.Black);
					if( sw == true )
						pen.DashStyle = DashStyle.DashDotDot;
					else
						pen.DashStyle = DashStyle.DashDot;
					pen.Width = 2.0f;

					g.DrawRectangle( pen, marque );
				}
			}
		}

		/// <summary>
		/// �`��C�x���g
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainPicture_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			DrawMarque( e.Graphics, m_linestyle );
		}

		/// <summary>
		/// ���T�C�Y�C�x���g
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SelectPictControl_Resize(object sender, System.EventArgs e)
		{
			if( m_lanznet == null || m_srcbitmap == null )
				return ;

			//�I��͈͂����摜�̍��W�Ōv�Z
			double srcwidth = (double)m_srcbitmap.Width ;
			double destwidth = (double)Width;
			double select_x = (double)m_selectarea.X;
			double select_width = (double)m_selectarea.Width;

			double srcheight = (double)m_srcbitmap.Height ;
			double destheight = (double)Height;
			double select_y = (double)m_selectarea.Y;
			double select_height = (double)m_selectarea.Height;

			//�����g�嗦�Ōv�Z
			double zoomper = destwidth / srcwidth;
			m_marque.X = (int)(select_x * zoomper);
			m_marque.Width = (int)(select_width * zoomper);

			zoomper = destheight / srcheight;
			m_marque.Y = (int)(select_y * zoomper);
			m_marque.Height = (int)(select_height * zoomper);
		}

		/// <summary>
		/// �`��^�C�}�[�N��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SelectTimer_Tick(object sender, System.EventArgs e)
		{
			m_linestyle = !m_linestyle;

			MainPicture.Invalidate();
		}
	}
}
