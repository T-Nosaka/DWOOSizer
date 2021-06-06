using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace DWOOSizer
{
	/// <summary>
	/// �ꊇ�ۑ��_�C�A���O
	/// </summary>
	public class BlanketSaveDlg : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.RadioButton radioButton1;
		private System.Windows.Forms.RadioButton radioButton2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button button3;
		/// <summary>
		/// �K�v�ȃf�U�C�i�ϐ��ł��B
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// �摜��ԃR���g���[��
		/// </summary>
		protected ResizePictControl m_resize;

		/// <summary>
		/// �y�[�W���X�g
		/// </summary>
		protected TabBmpPage[] m_bmplist;

		/// <summary>
		/// �摜��ԃR���g���[���v���p�e�B
		/// </summary>
		public ResizePictControl ResizeControl
		{
			set
			{
				m_resize = value;
			}
		}

		/// <summary>
		/// �y�[�W���X�g�v���p�e�B
		/// </summary>
		public TabBmpPage[] BmpList
		{
			set
			{
				m_bmplist = value;
			}
		}

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		public BlanketSaveDlg()
		{
			InitializeComponent();

			viewInit();
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

		#region Windows �t�H�[�� �f�U�C�i�Ő������ꂽ�R�[�h 
		/// <summary>
		/// �f�U�C�i �T�|�[�g�ɕK�v�ȃ��\�b�h�ł��B���̃��\�b�h�̓��e��
		/// �R�[�h �G�f�B�^�ŕύX���Ȃ��ł��������B
		/// </summary>
		private void InitializeComponent()
		{
			this.button2 = new System.Windows.Forms.Button();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.radioButton1 = new System.Windows.Forms.RadioButton();
			this.radioButton2 = new System.Windows.Forms.RadioButton();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.button3 = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(264, 144);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(88, 23);
			this.button2.TabIndex = 2;
			this.button2.Text = "����";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(24, 32);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(296, 19);
			this.textBox1.TabIndex = 3;
			this.textBox1.Text = "";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(336, 32);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(24, 23);
			this.button1.TabIndex = 4;
			this.button1.Text = "....";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// comboBox1
			// 
			this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox1.Location = new System.Drawing.Point(24, 88);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(88, 20);
			this.comboBox1.TabIndex = 5;
			// 
			// radioButton1
			// 
			this.radioButton1.Checked = true;
			this.radioButton1.Location = new System.Drawing.Point(16, 24);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new System.Drawing.Size(72, 24);
			this.radioButton1.TabIndex = 6;
			this.radioButton1.TabStop = true;
			this.radioButton1.Text = "�^�u��";
			// 
			// radioButton2
			// 
			this.radioButton2.Location = new System.Drawing.Point(120, 24);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new System.Drawing.Size(64, 24);
			this.radioButton2.TabIndex = 7;
			this.radioButton2.Text = "�A��";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radioButton2);
			this.groupBox1.Controls.Add(this.radioButton1);
			this.groupBox1.Location = new System.Drawing.Point(136, 72);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(200, 56);
			this.groupBox1.TabIndex = 8;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "�t�@�C����";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(24, 64);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 16);
			this.label1.TabIndex = 9;
			this.label1.Text = "�ۑ��`��";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(24, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(152, 16);
			this.label2.TabIndex = 9;
			this.label2.Text = "�ۑ��ꏊ";
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(24, 144);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(88, 23);
			this.button3.TabIndex = 10;
			this.button3.Text = "�ۑ�";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// BlanketSaveDlg
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(370, 184);
			this.ControlBox = false;
			this.Controls.Add(this.button3);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.label2);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "BlanketSaveDlg";
			this.Text = "�ꊇ�ۑ�";
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// �����\��
		/// </summary>
		protected void viewInit()
		{
			//�����f�B���N�g���ʒu
			textBox1.Text = Application.StartupPath;

			//�ۑ��`��
			//�ǂݍ��݉\�摜�t�H�[�}�b�g��
			ImageCodecInfo[] encoders = ImageCodecInfo.GetImageEncoders();

			foreach (ImageCodecInfo ici in encoders) 
			{
				string ext = ici.FilenameExtension;
				string[] strlist = ext.Split( new char[]{';'} );

				comboBox1.Items.Add( strlist[0] );
			}

			comboBox1.SelectedIndex = 0;
		}

		/// <summary>
		/// ����{�^������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button2_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		/// <summary>
		/// �t�H���_�I���{�^������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button1_Click(object sender, System.EventArgs e)
		{
			if( folderBrowserDialog1.ShowDialog( this ) == DialogResult.OK )
			{
				textBox1.Text = folderBrowserDialog1.SelectedPath;
			}
		}

		/// <summary>
		/// �ۑ��{�^������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button3_Click(object sender, System.EventArgs e)
		{
			//�ۑ��ꏊ�`�F�b�N
			if( Directory.Exists( textBox1.Text ) == false )
			{
				MessageBox.Show( "�f�B���N�g�������݂��܂���B" );
				return;
			}

			if( File.Exists( textBox1.Text ) == true )
			{
				MessageBox.Show( "�Ώۂ��t�@�C���Ƃ��đ��݂��܂��B" );
				return;
			}

			//�ۑ��`��
			string ext = Path.GetExtension( comboBox1.Text );
			ext = ext.Substring( 1, ext.Length-1 );

			//�ۑ��t�@�C����
			int iType = ( radioButton1.Checked == true ) ? 0:1;

			int iCount = 1;	//�A�ԗp�J�E���^
			//�ۑ�
			foreach( TabBmpPage tabpage in m_bmplist )
			{
				switch( iType )
				{
						//�^�u��
					case 0:
					{
						string filenamenoext = Path.GetFileNameWithoutExtension( tabpage.Filename );	//�g���q�����t�@�C����
						string filename = string.Format("{0}.{1}", filenamenoext, ext );				//�g���q�t���t�@�C����

						string filepath = Path.Combine( textBox1.Text, filename );		//�t���p�X
						//�t�@�C�����̃_�u���`�F�b�N
						int iCnt = 0;
						while( File.Exists( filepath ) == true )
						{
							filename = string.Format("{0}_{2}.{1}", filenamenoext, ext, iCnt );				//�g���q�t���t�@�C����
							filepath = Path.Combine( textBox1.Text, filename );		//�t���p�X
						}

						//�ۑ�
						//�t�@�C���`�����擾
						ImageCodecInfo[] encoders = ImageCodecInfo.GetImageEncoders();

						//�F�[�x�p�����[�^
						EncoderParameters encoderParams = new EncoderParameters();
						encoderParams.Param[0] = new EncoderParameter(Encoder.ColorDepth, 24L);

						m_resize.Bitmap = tabpage.SrcBitmap;
						m_resize.ReSizeImage( (uint)m_resize.Width, (uint)m_resize.Height );
						m_resize.Bitmap.Save( filepath, encoders[ comboBox1.SelectedIndex ], encoderParams );
					}
						break;

						//�A��
					case 1:
					{
						string filename = string.Format("{0}.{1}", iCount, ext );		//�g���q�t���t�@�C����
						string filepath = Path.Combine( textBox1.Text, filename );		//�t���p�X

						//�ۑ�
						//�t�@�C���`�����擾
						ImageCodecInfo[] encoders = ImageCodecInfo.GetImageEncoders();

						//�F�[�x�p�����[�^
						EncoderParameters encoderParams = new EncoderParameters();
						encoderParams.Param[0] = new EncoderParameter(Encoder.ColorDepth, 24L);

						File.Delete( filepath );

						m_resize.Bitmap = tabpage.SrcBitmap;
						m_resize.ReSizeImage( (uint)m_resize.Width, (uint)m_resize.Height );
						m_resize.Bitmap.Save( filepath, encoders[ comboBox1.SelectedIndex ], encoderParams );

						iCount++;
					}
						break;
				}
			}

			Close();
		}
	}
}
