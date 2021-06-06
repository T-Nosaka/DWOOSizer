using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using LanzNet;

namespace DWOOSizer
{
	/// <summary>
	/// ���C���t�H�[��
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		protected System.Windows.Forms.OpenFileDialog openFileDialog1;
		protected System.Windows.Forms.MainMenu mainMenu1;
		protected System.Windows.Forms.MenuItem menuItem1;
		protected System.Windows.Forms.MenuItem menuItem2;
		protected System.Windows.Forms.MenuItem menuItem3;
		protected System.Windows.Forms.MenuItem menuItem4;
		protected System.Windows.Forms.MenuItem menuItem5;
		protected System.Windows.Forms.SaveFileDialog saveFileDialog1;
		protected System.Windows.Forms.MenuItem menuItem6;
		protected System.Windows.Forms.MenuItem menuItem7;
		protected System.Windows.Forms.MenuItem menuItem8;
		protected System.Windows.Forms.MenuItem menuItem10;
		protected System.Windows.Forms.MenuItem menuItem9;
		protected System.Windows.Forms.MenuItem menuItem11;
		protected System.Windows.Forms.MenuItem menuItem12;
		protected System.Windows.Forms.MenuItem menuItem25;
		protected System.Windows.Forms.MenuItem menuItem26;
		protected System.Windows.Forms.MenuItem menuItem27;
		protected System.Windows.Forms.MenuItem menuItem28;
		protected System.Windows.Forms.MenuItem menuItem29;
		protected System.Windows.Forms.MenuItem menuItem30;

		private IContainer components;

        /// <summary>
        /// �\�����r�b�g�}�b�v
        /// </summary>
        protected Bitmap m_bitmap;

		/// <summary>
		/// �T�C�Y�X�V�@�\�t���摜�R���g���[��
		/// </summary>
		protected SelectPictControl resizePictControl;

		/// <summary>
		/// �摜���\���_�C�A���O
		/// </summary>
		protected InfoForm m_InfoForm;

		/// <summary>
		/// �\���{��
		/// </summary>
		protected double m_Zoom = 1.0;

		/// <summary>
		/// ��ʃT�C�Y�ύX�ɂāA�����X�N���[���I���̍ĕ`�撆�t���O
		/// </summary>
		protected bool m_bAutoScrollFirstEvent = false;

		/// <summary>
		/// �t�@�C���t�B���^�̍ŏI�����ԍ�
		/// </summary>
		protected int m_filtercnt = 1;

		/// <summary>
		/// ��ł��̕ύX�̃C�x���g�Z�b�g
		/// </summary>
		protected EventHandler m_pictvaluechanged = null;

		/// <summary>
		/// �����钼�O�̃C�x���g�ݒ�
		/// </summary>
		protected CancelEventHandler m_infoformclosing = null;

        /// <summary>
        /// �T�C�Y�ύX�C�x���g�̐ݒ�
        /// </summary>
        protected EventHandler m_resizeev = null;

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		public MainForm()
		{
			InitializeComponent();

			//�摜�R���g���[���쐬
			resizePictControl = new SelectPictControl();
			resizePictControl.Bitmap = null;
			resizePictControl.Dock = System.Windows.Forms.DockStyle.Fill;
			resizePictControl.EffectType = 0;
			resizePictControl.Location = new System.Drawing.Point(0, 0);
			resizePictControl.Name = "resizePictControl";
			resizePictControl.Selected = false;
			resizePictControl.Size = new System.Drawing.Size(224, 65);
			resizePictControl.TabIndex = 0;

			//�C�x���g�ǉ�
			resizePictControl.Picture.DoubleClick += new EventHandler( MainPicture_DoubleClick );

			Initialize();

			m_pictvaluechanged = new EventHandler( PictValueChanged );
			m_infoformclosing = new CancelEventHandler( InfoFormClosing );
			m_resizeev = new EventHandler(this.MainForm_Resize);
			this.Resize += m_resizeev;
		}

		/// <summary>
		/// ��������
		/// </summary>
		protected void Initialize()
		{
			//�ǂݍ��݉\�摜�t�H�[�}�b�g��
			ImageCodecInfo[] encoders = ImageCodecInfo.GetImageEncoders();

			string allfilter = string.Empty;
			string filter = string.Empty;
			foreach (ImageCodecInfo ici in encoders) 
			{
				if( filter.Length > 0 )
				{
					filter += "|";
					allfilter += ";";
				}

				filter += string.Format( "{0}({1})|{2}", ici.FormatDescription, ici.FilenameExtension.ToLower(), ici.FilenameExtension );
				allfilter += string.Format( "{0}",ici.FilenameExtension );

				m_filtercnt++;
			}
			openFileDialog1.Filter = string.Format("�S�Ă̌`��|{0}|{1}",allfilter,filter);
			openFileDialog1.FilterIndex = 1;

			m_filtercnt++;		//�S�Ă̌`�����J�E���g

			saveFileDialog1.Filter = filter;
			saveFileDialog1.FilterIndex = encoders.Length;
			saveFileDialog1.AddExtension = true;

			//�f�[�^�t�H�[���N��
			m_InfoForm = new InfoForm();
			m_InfoForm.ZoomEdt.ValueChanged += new System.EventHandler(ZoomEdtValueChanged);
		}

		/// <summary>
		/// �g�p����Ă��郊�\�[�X�Ɍ㏈�������s���܂��B
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		/// <summary>
		/// �`��R���g���[���v���p�e�B
		/// </summary>
		protected virtual ScrollableControl ClientForm
		{
			get
			{
				return this;
			}
		}

		/// <summary>
		/// �ǂݍ��ݏ��������������A�A�C�h���փ��[�h��J�ڂ���C�x���g
		/// </summary>
		protected virtual void OnViewStart()
		{
		}

		/// <summary>
		/// ���j���[�̗L����������
		/// </summary>
		/// <param name="flg"></param>
		protected virtual void MenuEnable( )
		{
			bool bFlg = (m_bitmap == null) ? false:true;

			//�ۑ����j���[�̕\��
			menuItem5.Enabled = bFlg;
			//�摜���̕\��
			menuItem7.Enabled = bFlg;
			//�S��ʂ̕\��
			menuItem8.Enabled = bFlg;
		}

		#region Windows �t�H�[�� �f�U�C�i�Ő������ꂽ�R�[�h 
		/// <summary>
		/// �f�U�C�i �T�|�[�g�ɕK�v�ȃ��\�b�h�ł��B���̃��\�b�h�̓��e��
		/// �R�[�h �G�f�B�^�ŕύX���Ȃ��ł��������B
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem10 = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.menuItem11 = new System.Windows.Forms.MenuItem();
            this.menuItem26 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.menuItem25 = new System.Windows.Forms.MenuItem();
            this.menuItem27 = new System.Windows.Forms.MenuItem();
            this.menuItem30 = new System.Windows.Forms.MenuItem();
            this.menuItem28 = new System.Windows.Forms.MenuItem();
            this.menuItem29 = new System.Windows.Forms.MenuItem();
            this.menuItem12 = new System.Windows.Forms.MenuItem();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Multiselect = true;
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem10,
            this.menuItem6,
            this.menuItem25,
            this.menuItem12});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem2,
            this.menuItem5,
            this.menuItem4,
            this.menuItem3});
            this.menuItem1.Text = "�t�@�C��";
            this.menuItem1.Popup += new System.EventHandler(this.menuItem1_Popup);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 0;
            this.menuItem2.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
            this.menuItem2.Text = "�J��";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 1;
            this.menuItem5.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
            this.menuItem5.Text = "�ۑ�";
            this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 2;
            this.menuItem4.Text = "-";
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 3;
            this.menuItem3.Shortcut = System.Windows.Forms.Shortcut.CtrlX;
            this.menuItem3.Text = "�I��";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // menuItem10
            // 
            this.menuItem10.Index = 1;
            this.menuItem10.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem9,
            this.menuItem11,
            this.menuItem26});
            this.menuItem10.Text = "�ҏW";
            this.menuItem10.Popup += new System.EventHandler(this.menuItem10_Popup);
            // 
            // menuItem9
            // 
            this.menuItem9.Index = 0;
            this.menuItem9.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
            this.menuItem9.Text = "�R�s�[";
            this.menuItem9.Click += new System.EventHandler(this.menuItem9_Click_1);
            // 
            // menuItem11
            // 
            this.menuItem11.Index = 1;
            this.menuItem11.Shortcut = System.Windows.Forms.Shortcut.CtrlV;
            this.menuItem11.Text = "�y�[�X�g";
            this.menuItem11.Click += new System.EventHandler(this.menuItem11_Click);
            // 
            // menuItem26
            // 
            this.menuItem26.Index = 2;
            this.menuItem26.Text = "�I������";
            this.menuItem26.Click += new System.EventHandler(this.menuItem26_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 2;
            this.menuItem6.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem7,
            this.menuItem8});
            this.menuItem6.Text = "�\��";
            this.menuItem6.Popup += new System.EventHandler(this.menuItem6_Popup);
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 0;
            this.menuItem7.Shortcut = System.Windows.Forms.Shortcut.CtrlI;
            this.menuItem7.Text = "�摜���";
            this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 1;
            this.menuItem8.Text = "�S���";
            this.menuItem8.Click += new System.EventHandler(this.menuItem8_Click);
            // 
            // menuItem25
            // 
            this.menuItem25.Index = 3;
            this.menuItem25.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem27,
            this.menuItem30,
            this.menuItem28,
            this.menuItem29});
            this.menuItem25.Text = "���";
            // 
            // menuItem27
            // 
            this.menuItem27.Checked = true;
            this.menuItem27.Index = 0;
            this.menuItem27.Text = "�����`���X";
            this.menuItem27.Click += new System.EventHandler(this.menuItem27_Click);
            // 
            // menuItem30
            // 
            this.menuItem30.Index = 1;
            this.menuItem30.Text = "�j�A���X�g�l�C�o�[";
            this.menuItem30.Click += new System.EventHandler(this.menuItem30_Click);
            // 
            // menuItem28
            // 
            this.menuItem28.Index = 2;
            this.menuItem28.Text = "�o�C���j�A";
            this.menuItem28.Click += new System.EventHandler(this.menuItem28_Click);
            // 
            // menuItem29
            // 
            this.menuItem29.Index = 3;
            this.menuItem29.Text = "�o�C�L���[�r�b�N";
            this.menuItem29.Click += new System.EventHandler(this.menuItem29_Click);
            // 
            // menuItem12
            // 
            this.menuItem12.Index = 4;
            this.menuItem12.Text = "�o�[�W�������";
            this.menuItem12.Click += new System.EventHandler(this.menuItem12_Click);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
            this.ClientSize = new System.Drawing.Size(287, 0);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.mainMenu1;
            this.Name = "MainForm";
            this.Text = "DWOOSizer";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.ResumeLayout(false);

		}
		#endregion

		#region ���j���[�C�x���g
		/// <summary>
		/// �ǂݍ��ݏ���
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			LoadFile();
		}

		/// <summary>
		/// �ǂݍ��ݏ���
		/// </summary>
		protected void LoadFile()
		{
			//�_�C�A���O�I�[�v��
			openFileDialog1.FileName = string.Empty;
			if (openFileDialog1.ShowDialog(this) == DialogResult.Cancel)
				return;

			//�J������
			int iCount = 0;

			foreach (string pictfilename in openFileDialog1.FileNames)
			{
				if (openFileDialog1.FilterIndex == 1)
				{
					OpenLoadFile(pictfilename);
				}
				else
				{
					LoadFile(pictfilename);
				}

				iCount++;
			}

			//�P���̂ݑI��؂�ւ�
			if (iCount == 1)
				OnViewStart();
		}

		/// <summary>
		/// �ۑ�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem5_Click(object sender, System.EventArgs e)
		{
			SaveImage( string.Empty );
		}

		/// <summary>
		/// �I���{�^��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void menuItem3_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// �\�����j���[
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem7_Click(object sender, System.EventArgs e)
		{
			InfoView();
		}

		/// <summary>
		/// �g���\��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void menuItem50_Click(object sender, System.EventArgs e)
		{
			NoFrameView();
		}

		/// <summary>
		/// �S��ʕ\��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void menuItem8_Click(object sender, System.EventArgs e)
		{
			MaxView();
		}

		/// <summary>
		/// �N���b�v�{�[�h���y�[�X�g
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem11_Click(object sender, System.EventArgs e)
		{
			ClipPaste();
		}

		/// <summary>
		/// �N���b�v�{�[�h�փR�s�[
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void menuItem9_Click_1(object sender, System.EventArgs e)
		{
			ClipCopy();
		}

		/// <summary>
		/// �o�[�W�������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void menuItem12_Click(object sender, System.EventArgs e)
		{
			VersionDialog();
		}

		/// <summary>
		/// �ҏW���j���[�|�b�v�A�b�v
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void menuItem10_Popup(object sender, System.EventArgs e)
		{
			//�R�s�[���j���[�̕\��
			menuItem9.Enabled = (m_bitmap == null) ? false:true;
			//�I���������j���[�̕\��
			menuItem26.Enabled = resizePictControl.Selected;
		}

		/// <summary>
		/// �I���������j���[
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void menuItem26_Click(object sender, System.EventArgs e)
		{
			resizePictControl.Selected = false;
		}

		/// <summary>
		/// �t�@�C�����j���[�|�b�v�A�b�v
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void menuItem1_Popup(object sender, System.EventArgs e)
		{
			MenuEnable();
		}

		/// <summary>
		/// �\�����j���[�|�b�v�A�b�v
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void menuItem6_Popup(object sender, System.EventArgs e)
		{
			MenuEnable();
		}

		/// <summary>
		/// �����`���X
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem27_Click(object sender, System.EventArgs e)
		{
			SelectEffect(0);
		}

		/// <summary>
		/// �j�A���X�g�l�C�o�[
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem30_Click(object sender, EventArgs e)
		{
			SelectEffect(1);
		}

		/// <summary>
		/// �o�C���j�A
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem28_Click(object sender, System.EventArgs e)
		{
			SelectEffect(2);
		}

		/// <summary>
		/// �o�C�L���[�r�b�N
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem29_Click(object sender, System.EventArgs e)
		{
			SelectEffect(3);
		}

		/// <summary>
		/// ��ԃ^�C�v�I��
		/// </summary>
		/// <param name="effecttype"></param>
		protected void SelectEffect(int effecttype)
		{
			switch( effecttype )
            {
				case 0:
					//���C�����j���[
					menuItem27.Checked = true;
					menuItem30.Checked = false;
					menuItem28.Checked = false;
					menuItem29.Checked = false;
					break;
				case 1:
					//���C�����j���[
					menuItem27.Checked = false;
					menuItem30.Checked = true;
					menuItem28.Checked = false;
					menuItem29.Checked = false;
					break;
				case 2:
					//���C�����j���[
					menuItem27.Checked = false;
					menuItem30.Checked = false;
					menuItem28.Checked = true;
					menuItem29.Checked = false;
					break;
				case 3:
					//���C�����j���[
					menuItem27.Checked = false;
					menuItem30.Checked = false;
					menuItem28.Checked = false;
					menuItem29.Checked = true;
					break;
			}
			resizePictControl.EffectType = effecttype;
		}

		#endregion

		#region �h���b�O&�h���b�v
		/// <summary>
		/// �t�@�C���h���b�v�C�x���g
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
		{
			string[] formats = e.Data.GetFormats();

			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				//�t�@�C���h���b�v
				foreach (string fileName in (string[])e.Data.GetData(DataFormats.FileDrop))
				{
					try
					{
						OpenLoadFile( fileName );
					}
					catch( Exception )
					{
					}
				}

				OnViewStart();
			}
			else
				if (e.Data.GetDataPresent(DataFormats.Dib))
			{
				//DIB�t�H�[�}�b�g
				MemoryStream srcstream = e.Data.GetData(DataFormats.Dib) as MemoryStream;

				//���̃^�C�v�́A�t�@�C���w�b�_�����݂��Ȃ��̂ŁA�ǉ�
				MemoryStream outstream = DIBReader.AddFileHead( srcstream );

				if( outstream == null )
					return;

				Bitmap dropbmp = new Bitmap( outstream );
				if( dropbmp == null )
					return;

				AttachBmp( dropbmp );

				OnViewStart();

				return;
			}
		}

		/// <summary>
		/// �t�@�C���h���b�v�J�n
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
		{
			e.Effect = DragDropEffects.All;
		}

		#endregion

		#region �C�x���g

		/// <summary>
		/// ���T�C�Y��
		/// </summary>
		private const int WM_SIZING = 0x214;

		/// <summary>
		/// ���T�C�Y����
		/// </summary>
		private const int WMSZ_LEFT        =   1;
		private const int WMSZ_RIGHT       =   2;
		private const int WMSZ_TOP         =   3;
		private const int WMSZ_TOPLEFT     =   4;
		private const int WMSZ_TOPRIGHT    =   5;
		private const int WMSZ_BOTTOM      =   6;
		private const int WMSZ_BOTTOMLEFT  =   7;
		private const int WMSZ_BOTTOMRIGHT =   8;

		/// <summary>
		/// ���T�C�Y����
		/// </summary>
		protected int m_resizekind = 0;

		/// <summary>
		/// �E�B���h�E�v���V�[�W��
		/// </summary>
		/// <param name="m"></param>
		protected override void WndProc(ref Message m) 
		{
			switch( m.Msg )
			{
				case WM_SIZING:
					m_resizekind = m.WParam.ToInt32();
					break;
			}

			base.WndProc(ref m);
		}

		/// <summary>
		/// �T�C�Y�ύX
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_Resize(object sender, System.EventArgs e)
		{
			//�V�t�g�L�[��������Ă����ꍇ
			if( Control.ModifierKeys == Keys.Shift && m_bitmap != null )
			{
				try
				{
					this.Resize -= m_resizeev;

					//�A�X�y�N�g����ێ�����B
					double dPicHeight = ClientForm.ClientSize.Height;
					double dPicWidth = ClientForm.ClientSize.Width;

					//���摜
					double dBmpHeight = m_bitmap.Height;
					double dBmpWidth = m_bitmap.Width;
					double dBmpAspect = (dBmpHeight/dBmpWidth);

					int iWidth = Width;
					int iHeight = Height;

#region �Œ�A�X�y�N�g���T�C�Y���S����
					if( m_resizekind == WMSZ_TOPLEFT )
					{
						if( (dPicHeight/dPicWidth) < dBmpAspect )
						{
							//��ʂ������Ȃ̂ŁA�c�ɍ��킹��B
							int iPicWidth = (int)(dPicHeight/dBmpAspect);
							iWidth = iPicWidth + (iWidth- ClientForm.ClientSize.Width);

							Location = new Point( Location.X - (iWidth-(int)dPicWidth), Location.Y );
						}
						else
						{
							//��ʂ��c���Ȃ̂ŁA���ɍ��킹��B
							int iPicHeight = (int)(dPicWidth*dBmpAspect);
							iHeight = iPicHeight + (iHeight- ClientForm.ClientSize.Height);

							Location = new Point( Location.X , Location.Y - (iHeight-(int)dPicHeight) );
						}
					}
					else
						if( m_resizekind == WMSZ_BOTTOMRIGHT )
					{
						if( (dPicHeight/dPicWidth) < dBmpAspect )
						{
							//��ʂ������Ȃ̂ŁA�c�ɍ��킹��B
							int iPicWidth = (int)(dPicHeight/dBmpAspect);
							iWidth = iPicWidth + (iWidth- ClientForm.ClientSize.Width);
						}
						else
						{
							//��ʂ��c���Ȃ̂ŁA���ɍ��킹��B
							int iPicHeight = (int)(dPicWidth*dBmpAspect);
							iHeight = iPicHeight + (iHeight- ClientForm.ClientSize.Height);
						}
					}
					else
					if( m_resizekind == WMSZ_LEFT || m_resizekind == WMSZ_RIGHT || m_resizekind == WMSZ_BOTTOMLEFT )
					{
						//��ʂ��c���Ȃ̂ŁA���ɍ��킹��B
						int iPicHeight = (int)(dPicWidth*dBmpAspect);
						iHeight = iPicHeight + (iHeight- ClientForm.ClientSize.Height);
					}
					else
					if( m_resizekind == WMSZ_TOP || m_resizekind == WMSZ_BOTTOM || m_resizekind == WMSZ_TOPRIGHT )
					{
						//��ʂ������Ȃ̂ŁA�c�ɍ��킹��B
						int iPicWidth = (int)(dPicHeight/dBmpAspect);
						iWidth = iPicWidth + (iWidth- ClientForm.ClientSize.Width);
					}
#endregion

					if( Width != iWidth )
						Width = iWidth;
					if( Height != iHeight )
						Height = iHeight;
				}
				catch( Exception )
				{
				}
				finally
				{
					this.Resize += m_resizeev;
				}
			}

			ReSizeImage( false );

			if( this.WindowState == System.Windows.Forms.FormWindowState.Minimized )
			{
				//�ŏ������ꂽ�ꍇ�A���_�C�A���O���\��
				m_InfoForm.Visible = false;
			}
			else
				if( m_InfoForm.Visible == false )
			{
				if( WindowState != System.Windows.Forms.FormWindowState.Minimized &&
					menuItem7.Checked == true )

					//�ŏ�������߂��ꂩ�A���_�C�A���O�\���̏ꍇ
					m_InfoForm.Visible = true;
			}
		}

		/// <summary>
		/// ��ł��̃T�C�Y�ύX
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void PictValueChanged(object sender, System.EventArgs e)
		{
			this.Resize -= m_resizeev;

			//�摜�����X�V
			int tmpwidth = Width - ClientForm.ClientSize.Width;
			int tmpheight =  Height - ClientForm.ClientSize.Height;

			Width = tmpwidth + (int)((double)m_InfoForm.WidthEdt.Value / m_Zoom) ;
			Height = tmpheight + (int)((double)m_InfoForm.HeightEdt.Value / m_Zoom);

			this.Resize += m_resizeev;

			ReSizeImage( true );

			//���ڂ̕␳�ŁA��肭�����Ă��Ȃ��ꍇ�́A������x�␳
			if( ClientForm.ClientSize.Width != (int)((double)m_InfoForm.WidthEdt.Value / m_Zoom) ||
				ClientForm.ClientSize.Height != (int)((double)m_InfoForm.HeightEdt.Value / m_Zoom) )
			{
				tmpwidth = Width - ClientForm.ClientSize.Width;
				tmpheight =  Height - ClientForm.ClientSize.Height;

				Height = tmpheight + (int)((double)m_InfoForm.HeightEdt.Value / m_Zoom);
				Width = tmpwidth + (int)((double)m_InfoForm.WidthEdt.Value / m_Zoom) ;

				ReSizeImage( true );
			}
		}

		/// <summary>
		/// �\���{���ύX
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void ZoomEdtValueChanged(object sender, System.EventArgs e)
		{
			m_Zoom = (double)m_InfoForm.ZoomEdt.Value / 100.0;
			ReSizeImage( true );
		}

		/// <summary>
		/// ���E�B���h�E�������钼�O�̃C�x���g
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void InfoFormClosing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel = true;

			InfoView();
		}

		/// <summary>
		/// �_�u���N���b�N
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainPicture_DoubleClick(object sender, System.EventArgs e)
		{
			MaxView();
		}

		#endregion

		#region �t�@�C��
		/// <summary>
		/// �C���[�W�̓��e�ɂ��A�T�|�[�g���e���烍�[�h
		/// </summary>
		public virtual void OpenLoadFile( string filename )
		{
			try
			{
				//��U�A�������ɓW�J����B
				MemoryStream deststream = new MemoryStream();

				using(FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read ))
				{
					BinaryReader infile = new BinaryReader( fs );
					byte[] tmpbin = infile.ReadBytes( (int)fs.Length );

					deststream.Write( tmpbin, 0, tmpbin.Length );
				}
				//�C���[�W���A�^�b�`����B
				AttachBmp( new Bitmap(deststream), filename );
			}
			catch( Exception )
			{
			}
		}

		/// <summary>
		/// �C���[�W�����[�h
		/// </summary>
		public virtual void LoadFile( string filename )
		{
			try
			{
				//��U�A�������ɓW�J����B
				MemoryStream deststream = new MemoryStream();

				using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
				{
					BinaryReader infile = new BinaryReader(fs);
					byte[] tmpbin = infile.ReadBytes((int)fs.Length);

					deststream.Write(tmpbin, 0, tmpbin.Length);
				}
				//�C���[�W���A�^�b�`����B
				AttachBmp(new Bitmap(deststream), filename);
			}
			catch ( Exception )
			{
			}
		}

		/// <summary>
		/// �C���[�W���Z�[�u
		/// </summary>
		public virtual void SaveImage( string filename )
		{
			if( m_bitmap == null || resizePictControl.Width <= 0 ||  resizePictControl.Height <= 0 )
				return ;

			//�_�C�A���O�I�[�v��
			string filename1 = filename;
			if( filename == string.Empty )
			{
				//�g���q������
				filename1 = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
			}

			saveFileDialog1.FileName = filename1;
			saveFileDialog1.InitialDirectory = openFileDialog1.InitialDirectory;
			if( saveFileDialog1.ShowDialog( this ) == DialogResult.Cancel )
				return;

			try
			{
				//�ۑ�
				//�t�@�C���`�����擾
				int iSelect = saveFileDialog1.FilterIndex - 1;
				ImageCodecInfo[] encoders = ImageCodecInfo.GetImageEncoders();

				//�F�[�x�p�����[�^
				EncoderParameters encoderParams = new EncoderParameters();
				encoderParams.Param[0] = new EncoderParameter(Encoder.ColorDepth, 24L);

				resizePictControl.Picture.Image.Save( saveFileDialog1.FileName, encoders[ iSelect ], encoderParams );
			}
			catch( Exception )
			{
			}
		}
		#endregion

		#region �C���[�W

		/// <summary>
		/// �C���[�W���A�^�b�`
		/// </summary>
		public virtual void AttachBmp( Bitmap bitmap, string filename )
		{
			AttachBmp( bitmap );
		}

		/// <summary>
		/// �C���[�W���A�^�b�`
		/// </summary>
		public virtual void AttachBmp( Bitmap bitmap )
		{
			if( m_bitmap != null )
			{
				m_bitmap.Dispose();
				m_bitmap = null;
			}

			//���r�b�g�}�b�v���Z�b�g����B
			m_bitmap = bitmap;
			resizePictControl.Bitmap = bitmap.Clone() as Bitmap;

			//�R���g���[���̂c���������蓮�ɕύX
			resizePictControl.Dock = DockStyle.None;

			ReSizeImage( false );

			//��ł��̕ύX�̃C�x���g�Z�b�g
			this.m_InfoForm.SizeValueChanged -= m_pictvaluechanged;
			this.m_InfoForm.SizeValueChanged += m_pictvaluechanged;

			//�����钼�O�̃C�x���g�ݒ�
			this.m_InfoForm.Closing -= m_infoformclosing;
			this.m_InfoForm.Closing += m_infoformclosing;

			//�摜����\��
			m_InfoForm.pictinfo.Text = string.Format( "{0}x{1}", m_bitmap.Width, m_bitmap.Height );

			//�I��̈��������
			resizePictControl.Selected = false;
		}

		/// <summary>
		/// �C���[�W�̃T�C�Y��ύX
		/// </summary>
		protected void ReSizeImage( bool InfoFormedt )
		{
			if( m_bitmap == null || ClientForm.ClientSize.Width <= 0 ||  ClientForm.ClientSize.Height <= 0 )
				return ;

			//��ʕ\���T�C�Y�֒�������B
			if( m_Zoom <= 1.0 )
			{
				try
				{
					ClientForm.SuspendLayout();

					resizePictControl.Width = (int)((double)ClientForm.ClientSize.Width * m_Zoom);
					resizePictControl.Height = (int)((double)ClientForm.ClientSize.Height * m_Zoom);

					ClientForm.AutoScroll = false;
					//�Z���^�[�ʒu�ֈړ�
					resizePictControl.Location = new Point((ClientForm.ClientSize.Width - resizePictControl.Width)/2,
						(ClientForm.ClientSize.Height - resizePictControl.Height)/2);

					resizePictControl.ReSizeImage( (uint)resizePictControl.Width , (uint)resizePictControl.Height );
				}
				finally
				{
					ClientForm.ResumeLayout(true);
				}
			}
			else
			{
				try
				{
					if( ClientForm.AutoScroll == false )
					{
						//�����X�N���[���I���ɂāA�R���g���[���T�C�Y�𒲐�����B

						//����Ɉړ�
						resizePictControl.Location = new Point(0,0);
						resizePictControl.Width = (int)((double)ClientForm.ClientSize.Width * m_Zoom);
						resizePictControl.Height = (int)((double)ClientForm.ClientSize.Height * m_Zoom);

						m_bAutoScrollFirstEvent = true; //��������A�X�N���[���o�[�����ׂ̈ɁA���̃C�x���g����������̂ŁA����������B
						ClientForm.AutoScroll = true;
						m_bAutoScrollFirstEvent = false;
					}
					else
					{
						if( m_bAutoScrollFirstEvent == false )
							ClientForm.SuspendLayout();

						//��U�A���݂̃X�N���[���ʒu���擾
						Point currentPos = ClientForm.AutoScrollPosition;

						//����Ɉړ�
						resizePictControl.Location = new Point(currentPos.X,currentPos.Y);

						resizePictControl.Width = (int)((double)ClientForm.ClientSize.Width * m_Zoom);
						resizePictControl.Height = (int)((double)ClientForm.ClientSize.Height * m_Zoom);
					}

					resizePictControl.ReSizeImage( (uint)resizePictControl.Width , (uint)resizePictControl.Height );
				}
				finally
				{
					ClientForm.ResumeLayout(true);
				}
			}

			if( InfoFormedt == false )
			{
				m_InfoForm.SizeValueChangedVisible = false;

				//�摜�����X�V
				m_InfoForm.WidthEdt.Value = resizePictControl.Width;
				m_InfoForm.HeightEdt.Value = resizePictControl.Height;

				m_InfoForm.SizeValueChangedVisible = true;
			}
		}
		#endregion

		#region �N���b�v�{�[�h
		/// <summary>
		/// �N���b�v�{�[�h�փR�s�[
		/// </summary>
		protected void ClipCopy()
		{
			if( m_bitmap == null || resizePictControl.Width <= 0 ||  resizePictControl.Height <= 0 )
				return ;

			//�I����Ԃ̏ꍇ�A�N���b�v
			if( resizePictControl.Selected == true )
			{
				Bitmap trimbitmap = resizePictControl.SelectBitmap;
				if( trimbitmap != null )
					Clipboard.SetDataObject(trimbitmap, true);
			}
			else
			{
				Clipboard.SetDataObject(resizePictControl.Picture.Image, true);
			}
		}

		/// <summary>
		/// �N���b�v�{�[�h���y�[�X�g
		/// </summary>
		protected void ClipPaste()
		{
			try
			{
				//�N���b�v�{�[�h�ɂ���f�[�^�̎擾
				IDataObject d = Clipboard.GetDataObject();
				//�r�b�g�}�b�v�f�[�^�`���Ɋ֘A�t�����Ă���f�[�^���擾
				Bitmap img = d.GetData(DataFormats.Bitmap) as Bitmap;
				if (img != null)
				{
					AttachBmp( img );
				}

				//�I��̈��������
				resizePictControl.Selected = false;

				OnViewStart();
			}
			catch( Exception )
			{
			}
		}
		#endregion

		/// <summary>
		/// �摜���\��
		/// </summary>
		protected void InfoView()
		{
			if( m_bitmap == null )
				return ;

			menuItem7.Checked = !menuItem7.Checked;

			if( menuItem7.Checked == true )
				this.m_InfoForm.Show();
			else
				this.m_InfoForm.Hide();
		}

		/// <summary>
		/// �S��ʕ\��
		/// </summary>
		protected void MaxView()
		{
			if( m_bitmap == null )
				return ;

			MaxViewForm maxform = new MaxViewForm();
			maxform.EffectType = resizePictControl.EffectType;
			maxform.Bitmap = m_bitmap;
			maxform.Show();
		}

		/// <summary>
		/// �o�[�W�������
		/// </summary>
		protected void VersionDialog()
		{
			VerDlg verdlg = new VerDlg();
			verdlg.ShowDialog( this );
		}

		/// <summary>
		/// �g���\��
		/// </summary>
		protected virtual void NoFrameView()
		{
			if( m_bitmap == null )
				return ;

			this.Visible = false;

			MaxViewForm maxform = new MaxViewForm();

			//�ł��������킹��
			maxform.Size = resizePictControl.Size;

			Point pos = this.Location;
			pos.X += this.ClientForm.Location.X;
			pos.Y += this.ClientForm.Location.Y;

			maxform.Location = pos;
			maxform.WindowState = System.Windows.Forms.FormWindowState.Normal;

			maxform.EffectType = resizePictControl.EffectType;
			maxform.Bitmap = m_bitmap;
			maxform.ShowDialog();

			this.Visible = true;
		}
    }
}
