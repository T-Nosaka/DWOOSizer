/*++

	Windows
	汎用

	Bitmap I/F 画像メモリ管理

--*/


#include <windows.h>

#include "ORgbIfMem.h"
#include "OPlaneLeafRgb24.h"

bool ORgbIfMem::SetBitmapRgb24( ORgbBmpObj& oRgb )
{
	m_TopX = 0;
	m_TopY = 0;

	DWORD dwWidth = oRgb.Width();
	DWORD dwHeight = oRgb.Height();

	m_ulHorzRes = dwWidth;
	m_ulVertRes = dwHeight;

	//既にメモリが存在する場合、破棄
	ListDelete();

	//BMPは、画がリバースされてくることを忘れるな
	//RGB24Bit
	//一行バイト数より1ドット
	DWORD dwDotBytes = labs( oRgb.RowByte() )/dwWidth;

	//ＲＧＢプレーンを作成
	OPlaneLeafRgb24* pRed = new OPlaneLeafRgb24( dwWidth, dwHeight, OPlaneLeafRgb24::REDNAME );
	OPlaneLeafRgb24* pGreen = new OPlaneLeafRgb24( dwWidth, dwHeight, OPlaneLeafRgb24::GREENNAME );
	OPlaneLeafRgb24* pBlue = new OPlaneLeafRgb24( dwWidth, dwHeight, OPlaneLeafRgb24::BLUENAME );

	//各色の反復子取得
	OPlaneLeafRgb24::iterator itRed,itGreen,itBlue;

	itRed = pRed->begin();
	itGreen = pGreen->begin();
	itBlue = pBlue->begin();

	//画像を上下リバースし、処理しやすくする。 BMPは、BGRである。
	DWORD dwPtSize = sizeof(OPlaneLeafRgb24::RGB24) * 3;
	DWORD dwLine;
	DWORD dwCursor;
	for( dwLine=0; dwLine<dwHeight; dwLine++ )
	{
		register LPBYTE pStart2 = (LPBYTE)( oRgb.Image() + oRgb.RowByte() * dwLine  );

		for( dwCursor=0; dwCursor<dwWidth; dwCursor++ )
		{
			//B
			*itBlue = (BYTE)*(pStart2);
			//G
			*itGreen = (BYTE)*(pStart2+1);
			//R
			*itRed = (BYTE)*(pStart2+2);

			pStart2 += dwPtSize;

			itBlue++;
			itGreen++;
			itRed++;
		}
	}

	//プレーンを追加
	m_planelist.push_back( pRed );
	m_planelist.push_back( pGreen );
	m_planelist.push_back( pBlue );

	return true;
}


bool ORgbIfMem::GetBitmapRgb24( ORgbBmpObj& oRgb, LPBYTE pMemory )
{
	//プレーンを取得
	OPlaneLeafRgb24* pRed = (OPlaneLeafRgb24*)GetPlane( OPlaneLeafRgb24::REDNAME );
	OPlaneLeafRgb24* pGreen = (OPlaneLeafRgb24*)GetPlane( OPlaneLeafRgb24::GREENNAME );
	OPlaneLeafRgb24* pBlue = (OPlaneLeafRgb24*)GetPlane( OPlaneLeafRgb24::BLUENAME );

	if ( pRed == NULL ||
		 pGreen == NULL ||
		 pBlue == NULL )
		return false;

	//画像サイズ取得
	DWORD dwWidth = GetWidth();
	DWORD dwHeight = GetHeight();
	if ( dwHeight == 0 )
		return false;

	DWORD dwRowBytes = sizeof(OPlaneLeafRgb24::RGB24) * ( ( (dwWidth*3)%4 == 0 ) ? dwWidth*3:((dwWidth*3/4)+1)*4 );

	//各色の反復子取得
	OPlaneLeafRgb24::iterator itRed,itGreen,itBlue;

	itRed = pRed->begin();
	itGreen = pGreen->begin();
	itBlue = pBlue->begin();

	//メモリ取得
	if( pMemory == NULL )
		pMemory = new BYTE[ dwRowBytes * dwHeight + 4];

	DWORD dwPtSize = sizeof(OPlaneLeafRgb24::RGB24) * 3;
	DWORD dwLine;
	DWORD dwCursor;
	for( dwLine=0; dwLine<dwHeight; dwLine++ )
	{
		register LPBYTE pStart2 = (LPBYTE)(pMemory + dwRowBytes * dwLine);

		for( dwCursor=0; dwCursor<dwWidth; dwCursor++ )
		{
			* ( pStart2 ) = *itBlue ;
			* ( pStart2 + 1 ) = *itGreen ;
			* ( pStart2 + 2 ) = *itRed ;

			pStart2 += dwPtSize;

			itBlue++;
			itGreen++;
			itRed++;
		}
	}

	//メモリを取り直さずにポインタ渡し
	oRgb.AttachImage( dwWidth, dwHeight, dwRowBytes, pMemory );

	return true;
}

