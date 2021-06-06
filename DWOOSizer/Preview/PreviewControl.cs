using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace DWOOSizer.Preview
{
	/// <summary>
	/// �v���r���[�R���g���[��
	/// </summary>
	public class PreviewControl : System.Windows.Forms.UserControl
	{
		/// <summary>
		/// �`��A�C�e�����X�g
		/// </summary>
		protected ArrayList m_viewlist = new ArrayList();

		#region �K�{�R�[�h
		/// <summary>
		/// �K�v�ȃf�U�C�i�ϐ��ł��B
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		public PreviewControl()
		{
			InitializeComponent();
		}

		/// <summary>
		/// �g�p����Ă��郊�\�[�X�Ɍ㏈�������s���܂��B
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region �R���|�[�l���g �f�U�C�i�Ő������ꂽ�R�[�h 
		/// <summary>
		/// �f�U�C�i �T�|�[�g�ɕK�v�ȃ��\�b�h�ł��B���̃��\�b�h�̓��e��
		/// �R�[�h �G�f�B�^�ŕύX���Ȃ��ł��������B
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// PreviewControl
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Name = "PreviewControl";
			this.Size = new System.Drawing.Size(232, 256);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PreviewControl_MouseUp);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.PreviewControl_Paint);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PreviewControl_MouseMove);
			this.MouseLeave += new System.EventHandler(this.PreviewControl_MouseLeave);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PreviewControl_MouseDown);

		}
		#endregion

		#endregion

		#region �C�x���g
		/// <summary>
		/// �`��C�x���g
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PreviewControl_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			e.Graphics.PageUnit = GraphicsUnit.Pixel ;

			foreach( ViewBaseItem item in m_viewlist )
			{
				item.OnPaint( e.Graphics );
			}
		}

		/// <summary>
		/// �}�E�X�_�E��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PreviewControl_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			bool bInvalidate = false;

			foreach( ViewBaseItem item in m_viewlist )
			{
				if( item is MouseEventIf )
				{
					MouseEventIf mouseitem = item as MouseEventIf;

					mouseitem.OnMouseDown( e );

					if( mouseitem.IsInvalidate() == true )
					{
						bInvalidate = true;
					}

					if( mouseitem.IsEventExclusive( e ) == true )
						break;
				}
			}

			if( bInvalidate == true )
				Invalidate();
		}

		/// <summary>
		/// �}�E�X�A�b�v
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PreviewControl_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			bool bInvalidate = false;

			foreach( ViewBaseItem item in m_viewlist )
			{
				if( item is MouseEventIf )
				{
					MouseEventIf mouseitem = item as MouseEventIf;

					mouseitem.OnMouseUp( e );

					if( mouseitem.IsInvalidate() == true )
					{
						bInvalidate = true;
					}

					if( mouseitem.IsEventExclusive( e ) == true )
						break;
				}
			}

			if( bInvalidate == true )
				Invalidate();
		}

		/// <summary>
		/// �}�E�X���[�u
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PreviewControl_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			bool bInvalidate = false;

			foreach( ViewBaseItem item in m_viewlist )
			{
				if( item is MouseEventIf )
				{
					MouseEventIf mouseitem = item as MouseEventIf;

					mouseitem.OnMouseMove( e );

					if( mouseitem.IsInvalidate() == true )
					{
						bInvalidate = true;
					}

					if( mouseitem.IsEventExclusive( e ) == true )
						break;
				}
			}

			if( bInvalidate == true )
				Invalidate();
		}

		/// <summary>
		/// �}�E�X���[�u
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PreviewControl_MouseLeave(object sender, System.EventArgs e)
		{
		
		}

		#endregion

		/// <summary>
		/// �`��A�C�e���ǉ�
		/// </summary>
		/// <param name="item"></param>
		public void AddItem( ViewBaseItem target )
		{
			foreach( ViewBaseItem item in m_viewlist )
			{
				if( item == target )
					return ;
			}

			m_viewlist.Add( target );
		}

		/// <summary>
		/// �`��A�C�e���폜
		/// </summary>
		/// <param name="target"></param>
		public void RemoveItem( ViewBaseItem target )
		{
			m_viewlist.Remove( target );
		}
	}
}
