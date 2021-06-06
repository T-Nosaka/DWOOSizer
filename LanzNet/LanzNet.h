// LanzNet.h

#pragma once

using namespace System;
using namespace System::Drawing::Imaging;
using namespace System::Runtime::InteropServices;

namespace LanzNet
{
	public ref class OLanczLngMemNet
	{
	public:
		OLanczLngMemNet()
		{
			//�������� �����`���X
			m_Impl = new OLanczLngMem();

			m_reserve = new ORgbIfMem();
		}

		~OLanczLngMemNet()
		{
			DestroyMemory();
		}

		///
		// ���
		///
		void DestroyMemory()
		{
			if( m_Impl != NULL )
			{
				delete m_Impl;
				m_Impl = NULL;
			}
			if( m_reserve != NULL )
			{
				delete m_reserve;
				m_reserve =NULL;
			}
		}

		void EffectType( int type )
		{
			OResizeMem* effectobj = NULL;

			switch( type )
			{
			case 0:
				effectobj = new OLanczLngMem();
				break;
			case 1:
				effectobj = new ONeighborMem();
				break;
			case 2:
				effectobj = new OBiLinearMem();
				break;
			case 3:
				effectobj = new OBiCubicMem();
				break;
			}

			//�摜������
			*effectobj = *m_Impl;
			delete m_Impl;
			m_Impl = effectobj;
		}

		///
		// �����r�b�g�}�b�v�C���[�W�ݒ�
		///
		bool SetBitmapRgb24( BitmapData^ bitmap )
		{
			ORgbBmpObj oRgbData ;
			oRgbData.AttachImage( bitmap->Width, bitmap->Height, bitmap->Stride, (LPBYTE)(HANDLE)(bitmap->Scan0.ToPointer()) ) ;

			//�ǂݍ��񂾉摜��ێ�
			m_reserve->SetBitmapRgb24( oRgbData );

			//�}�l�[�W���̃������ׁ̈A������Ȃ��B
			oRgbData.DettachImage();

			return true;
		}

		///
		// �r�b�g�}�b�v�C���[�W�擾
		// ���T�C�Y��
		///
		bool GetBitmapRgb24( BitmapData^ bitmap )
		{
			ORgbBmpObj oDstRgbData ;

			m_Impl->GetBitmapRgb24( oDstRgbData, (LPBYTE)bitmap->Scan0.ToPointer() );

			bitmap->Width = oDstRgbData.Width();
			bitmap->Height = oDstRgbData.Height();
			bitmap->Stride = oDstRgbData.RowByte();

			//�}�l�[�W���̃������ׁ̈A������Ȃ��B
			oDstRgbData.DettachImage();

			return true;
		}

		///
		// �r�b�g�}�b�v���g��������B
		// ���U�[�u�����Ă錳�f�[�^����f�[�^���쐬����B
		///
		void TrimBitmapRgb24( BitmapData^ bitmap, DWORD left, DWORD width, DWORD top, DWORD height )
		{
			ORgbBmpObj oDstRgbData ;

			ORgbIfMem* trimh = (ORgbIfMem* )m_reserve->GetPerpendicularArea( left, left+width );
			ORgbIfMem* trimw = (ORgbIfMem* )trimh->GetLine( top, top+height );

			trimw->GetBitmapRgb24( oDstRgbData );

			bitmap->Width = oDstRgbData.Width();
			bitmap->Height = oDstRgbData.Height();
			bitmap->Stride = oDstRgbData.RowByte();
			//�r�b�g�}�b�v���u���b�N�R�s�[
			oDstRgbData.ImageCopy( bitmap->Scan0.ToPointer() );

			delete trimh;
			delete trimw;
		}

		///
		// �摜�T�C�Y�ϊ�
		///
		bool Resize( unsigned long dwWidthPrm, unsigned long dwHeightPrm  )
		{
			try
			{
				//���摜�������N
				m_Impl->GetSrcImage( *m_reserve );

				m_Impl->Resize( *m_reserve, dwWidthPrm, dwHeightPrm );

				return true;
			}
			catch( Exception^ )
			{
				return false;
			}
		}

	private:
		//�����`���X�I�u�W�F�N�g �A���}�l�[�W
		OResizeMem* m_Impl;

		//���摜
		ORgbIfMem* m_reserve;
	};
}
