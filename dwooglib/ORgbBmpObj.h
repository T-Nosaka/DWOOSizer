/*++

	画像BMP実装

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
	// 強制イメージアタッチ
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
	// 強制デタッチ
	///
	void DettachImage()
	{
		m_pImage = NULL;
	}
};


