using System;
using System.Windows.Forms;

namespace DWOOSizer.Preview
{
	/// <summary>
	/// �}�E�X�C�x���g�C���^�[�t�F�[�X
	/// </summary>
	public interface MouseEventIf
	{
		/// <summary>
		/// �}�E�X�_�E��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void OnMouseDown( MouseEventArgs e);

		/// <summary>
		/// �}�E�X�A�b�v
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void OnMouseUp( MouseEventArgs e);

		/// <summary>
		/// �}�E�X���[�u
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void OnMouseMove( MouseEventArgs e);

		/// <summary>
		/// �C�x���g�r��
		/// </summary>
		/// <param name="e"></param>
		/// <returns></returns>
		bool IsEventExclusive( MouseEventArgs e );

		/// <summary>
		/// �ĕ`��̕K�v
		/// </summary>
		/// <returns></returns>
		bool IsInvalidate();
	}
}
