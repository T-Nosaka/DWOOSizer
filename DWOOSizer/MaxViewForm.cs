using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using LanzNet;

namespace DWOOSizer
{
	/// <summary>
	/// �ő�\���t�H�[��
	/// </summary>
	public class MaxViewForm : System.Windows.Forms.Form
	{
		/// <summary>
		/// �K�v�ȃf�U�C�i�ϐ��ł��B
		/// </summary>
		private System.ComponentModel.Container components = null;
		private DWOOSizer.ResizePictControl resizePictControl;

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		public MaxViewForm()
		{
			InitializeComponent();

			//�N���b�N�C�x���g
			resizePictControl.Picture.Click += new EventHandler(resizePictControl_Click);
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

		/// <summary>
		/// ��ԃ^�C�v�ݒ�
		/// </summary>
		public int EffectType
		{
			set
			{
				resizePictControl.EffectType = value;
			}
		}

		/// <summary>
		/// �摜�h�^�e
		/// </summary>
		public Bitmap Bitmap
		{
			get
			{
				return resizePictControl.Bitmap;
			}
			set
			{
				resizePictControl.Bitmap = value;
			}
		}

		#region Windows �t�H�[�� �f�U�C�i�Ő������ꂽ�R�[�h 
		/// <summary>
		/// �f�U�C�i �T�|�[�g�ɕK�v�ȃ��\�b�h�ł��B���̃��\�b�h�̓��e��
		/// �R�[�h �G�f�B�^�ŕύX���Ȃ��ł��������B
		/// </summary>
		private void InitializeComponent()
		{
			this.resizePictControl = new DWOOSizer.ResizePictControl();
			this.SuspendLayout();
			// 
			// resizePictControl
			// 
			this.resizePictControl.Bitmap = null;
			this.resizePictControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.resizePictControl.Location = new System.Drawing.Point(0, 0);
			this.resizePictControl.Name = "resizePictControl";
			this.resizePictControl.Size = new System.Drawing.Size(240, 200);
			this.resizePictControl.TabIndex = 0;
			this.resizePictControl.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.resizePictControl_KeyPress);
			// 
			// MaxViewForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(240, 200);
			this.ControlBox = false;
			this.Controls.Add(this.resizePictControl);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "MaxViewForm";
			this.ShowInTaskbar = false;
			this.Text = "MaxViewForm";
			this.TopMost = true;
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Resize += new System.EventHandler(this.MaxViewForm_Resize);
			this.ResumeLayout(false);

		}
		#endregion


		/// <summary>
		/// �N���b�N�C�x���g
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void resizePictControl_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// �L�[�C�x���g
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void resizePictControl_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if( e.KeyChar == 0x1b )
				this.Close();
		}

		/// <summary>
		/// �摜�ő剻�C�x���g
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MaxViewForm_Resize(object sender, System.EventArgs e)
		{
			resizePictControl.ReSizeImage( (uint)Width, (uint)Height );
		}
	}
}
