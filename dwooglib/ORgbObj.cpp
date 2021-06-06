/*++

	�摜�t�@�C������

--*/


#include <windows.h>

#include "ORgbObj.h"


ORgbObj::ORgbObj()
{
	Init();
}

ORgbObj::~ORgbObj()
{
	DropImage();
}

void ORgbObj::DropImage()
{
	if ( m_pImage != NULL )
	{
		delete [] m_pImage;
	}

	Init();
}

void ORgbObj::Init()
{
	m_pImage = NULL;

	m_dwWidth = 0;
	m_dwHeight = 0;

	m_rowBytes = 0;

}


void ORgbObj::operator= ( const ORgbObj& instance )
{
	DropImage();

	//�������T�C�Y
	DWORD dwMemSize = ((ORgbObj&)instance).GetMemorySize();

	m_pImage = new BYTE[ dwMemSize + 1 ];
	memset( m_pImage, (char)NULL, dwMemSize + 1 );
	memcpy( m_pImage, instance.m_pImage, dwMemSize );

	m_dwWidth = instance.m_dwWidth;
	m_dwHeight = instance.m_dwHeight;

	m_rowBytes = instance.m_rowBytes;

}

void ORgbObj::SetImage( DWORD dwWidth, DWORD dwHeight, DWORD dwRowByte, LPBYTE pImage )
{
	DWORD dwMemSize = 0;

	//�����T�C�Y�̃��������؂��Ă����ꍇ�A�Ď擾���Ȃ�
	if( m_dwWidth == dwWidth &&
		m_dwHeight == dwHeight &&
		m_rowBytes == dwRowByte &&
		m_pImage != NULL )
	{
		dwMemSize = GetMemorySize();
	}
	else
	{
		m_dwWidth = dwWidth;
		m_dwHeight = dwHeight;
		m_rowBytes = dwRowByte;

		if ( m_pImage != NULL )
		{
			delete [] m_pImage;
		}

		dwMemSize = GetMemorySize();

		m_pImage = new BYTE[ dwMemSize + 1 ];
	}

	memcpy( m_pImage, pImage, dwMemSize );
}

void ORgbObj::ImageCopy( void* dest )
{
	if( m_pImage != NULL )
	{
		//�r�b�g�}�b�v���u���b�N�R�s�[
		memcpy( dest, m_pImage, GetMemorySize() );
	}
}
