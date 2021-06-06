/*++

	�摜BMP����

--*/

#pragma once

#include "ORgbObj.h"

class DLLCLASSIMPEXP ORgbBmpObj : public ORgbObj 
{
protected:

public:
	ORgbBmpObj() : ORgbObj() {}

	ORgbBmpObj( const ORgbBmpObj& instance )
	{
		(ORgbObj&)*this = (ORgbObj&)instance;
	}

	///
	// �����C���[�W�A�^�b�`
	///
	void AttachImage( DWORD dwWidth, DWORD dwHeight, DWORD dwRowByte, LPBYTE image )
	{
		m_dwWidth = dwWidth;
		m_dwHeight = dwHeight;
		m_rowBytes = dwRowByte;

		if( m_pImage != NULL )
		{
			delete [] m_pImage;
		}

		m_pImage = image;
	}

	///
	// �����f�^�b�`
	///
	void DettachImage()
	{
		m_pImage = NULL;
	}
};


