/*++

	画像ファイル抽象

--*/

#pragma once

#include "OPlaneDllDef.h"

class DLLCLASSIMPEXP ORgbObj
{
protected:
	//格納メモリ
	LPBYTE m_pImage;	//イメージデータ
	DWORD  m_dwWidth;
	DWORD  m_dwHeight;
	long   m_rowBytes;	//1行内のバイト数

public:
	ORgbObj();
	~ORgbObj();

	ORgbObj( const ORgbObj& instance ) { *this = instance; }

	//コピー
	void operator= ( const ORgbObj& );

	DWORD Width() { return m_dwWidth; }
	DWORD Height() { return m_dwHeight; }

	long RowByte() { return m_rowBytes; }

	LPBYTE Image() { return m_pImage; }

	void SetImage( DWORD dwWidth, DWORD dwHeight, DWORD dwRowByte, LPBYTE pImage ) ;

	//出力
	long GetMemorySize() { return m_dwHeight * m_rowBytes; }

	//イメージメモリコピー
	void ImageCopy( void* dest );

protected:
	void Init();
	virtual void DropImage() ;

};

