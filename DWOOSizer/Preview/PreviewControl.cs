using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace DWOOSizer.Preview
{
	/// <summary>
	/// プレビューコントロール
	/// </summary>
	public class PreviewControl : System.Windows.Forms.UserControl
	{
		/// <summary>
		/// 描画アイテムリスト
		/// </summary>
		protected ArrayList m_viewlist = new ArrayList();

		#region 必須コード
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public PreviewControl()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 使用されているリソースに後処理を実行します。
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

		#region コンポーネント デザイナで生成されたコード 
		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
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

		#region イベント
		/// <summary>
		/// 描画イベント
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
		/// マウスダウン
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
		/// マウスアップ
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
		/// マウスムーブ
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
		/// マウスリーブ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PreviewControl_MouseLeave(object sender, System.EventArgs e)
		{
		
		}

		#endregion

		/// <summary>
		/// 描画アイテム追加
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
		/// 描画アイテム削除
		/// </summary>
		/// <param name="target"></param>
		public void RemoveItem( ViewBaseItem target )
		{
			m_viewlist.Remove( target );
		}
	}
}
