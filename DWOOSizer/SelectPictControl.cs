using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DWOOSizer
{
	/// <summary>
	/// 選択付き画像補間コントロール
	/// </summary>
	public class SelectPictControl : CookedPictControl
	{
		/// <summary>
		/// 選択矩形
		/// </summary>
		protected Rectangle m_marque = new Rectangle( 0,0,0,0);

		/// <summary>
		/// 元画像に対しての選択矩形
		/// </summary>
		protected Rectangle m_selectarea = new Rectangle( 0,0,0,0);

		/// <summary>
		/// ドラッグ中フラグ
		/// </summary>
		protected bool m_drag = false;

		/// <summary>
		/// 選択状態
		/// </summary>
		protected bool m_selected = false;
		private System.Windows.Forms.Timer SelectTimer;
		private System.ComponentModel.IContainer components;

		/// <summary>
		/// 元の画像
		/// </summary>
		protected Bitmap m_srcbitmap = null;

		/// <summary>
		/// ラインスタイルスイッチ
		/// </summary>
		protected bool m_linestyle = false;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public SelectPictControl()
		{
			InitializeComponent();
		}

		#region コンポーネント デザイナで生成されたコード 
		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
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
		/// 元画像に対しての選択矩形プロパティ
		/// </summary>
		public Bitmap SelectBitmap
		{
			get
			{
				if( m_selectarea.Width <= 0 || m_selectarea.Height <= 0 )
					return null;

				//元ビットマップからトリムする。
				//受け取り画像を作成
				using( BitmapUn bitmapDst = new BitmapUn( m_selectarea.Width, m_selectarea.Height ) )
				{
					//トリム処理
					m_lanznet.TrimBitmapRgb24( bitmapDst.BitmapData, (uint)m_selectarea.X, (uint)m_selectarea.Width, (uint)m_selectarea.Y, (uint)m_selectarea.Height );

					return bitmapDst.Unlock();
				}
			}
		}

		/// <summary>
		/// 選択状態プロパティ
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

				//選択領域描画タイマー設定
				SelectTimer.Enabled = value;

			}
		}

		/// <summary>
		/// 画像Ｉ／Ｆ
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
		/// マウスダウン
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
		/// マウス移動
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
		/// マウスアップ
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

			//座標系を修正
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

			//画像サイズのオーバーフロー修正
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

			//選択範囲を元画像の座標で計算
			double srcwidth = (double)m_srcbitmap.Width ;
			double destwidth = (double)Width;
			double marque_x = (double)m_marque.X;
			double marque_width = (double)m_marque.Width;

			double srcheight = (double)m_srcbitmap.Height ;
			double destheight = (double)Height;
			double marque_y = (double)m_marque.Y;
			double marque_height = (double)m_marque.Height;

			//幅を拡大率で計算
			double zoomper = srcwidth / destwidth ;
			m_selectarea.X = (int)(marque_x * zoomper);
			m_selectarea.Width = (int)(marque_width * zoomper);

			zoomper = srcheight / destheight ;
			m_selectarea.Y = (int)(marque_y * zoomper);
			m_selectarea.Height = (int)(marque_height * zoomper);
		}

		/// <summary>
		/// マーキー描画
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

					//座標系を修正
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

					//ペンを作成
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
		/// 描画イベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainPicture_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			DrawMarque( e.Graphics, m_linestyle );
		}

		/// <summary>
		/// リサイズイベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SelectPictControl_Resize(object sender, System.EventArgs e)
		{
			if( m_lanznet == null || m_srcbitmap == null )
				return ;

			//選択範囲を元画像の座標で計算
			double srcwidth = (double)m_srcbitmap.Width ;
			double destwidth = (double)Width;
			double select_x = (double)m_selectarea.X;
			double select_width = (double)m_selectarea.Width;

			double srcheight = (double)m_srcbitmap.Height ;
			double destheight = (double)Height;
			double select_y = (double)m_selectarea.Y;
			double select_height = (double)m_selectarea.Height;

			//幅を拡大率で計算
			double zoomper = destwidth / srcwidth;
			m_marque.X = (int)(select_x * zoomper);
			m_marque.Width = (int)(select_width * zoomper);

			zoomper = destheight / srcheight;
			m_marque.Y = (int)(select_y * zoomper);
			m_marque.Height = (int)(select_height * zoomper);
		}

		/// <summary>
		/// 描画タイマー起動
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
