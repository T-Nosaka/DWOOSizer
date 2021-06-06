/*++

	Windows
	汎用

	画像メモリ基底

--*/

#pragma once

#include "OPlaneDllDef.h"

//メモリマクロ
#define MEMSET(a,b,c)	memset(a,b,c)
#define MEMCPY(a,b,c)	memcpy(a,b,c)

//アロケート関連は、operator new delete をオーバーロードすること

#include <string>

#pragma warning(disable:4251)

class DLLCLASSIMPEXP OPlaneLeaf
{
public:

	typedef DWORD DISTANCE;				//距離
	typedef std::pair< OPlaneLeaf*, DISTANCE > DISTANCESRC;

protected:
	std::string m_leafName;		//このプレーンの名前
	DWORD	m_dwMemSize;		//メモリサイズ
	DWORD	m_width;			//幅
	DWORD	m_height;			//高
	DWORD	m_DotSize;			//ドットサイズ

	//Protected Default Constructor
	OPlaneLeaf() ;

public:
	OPlaneLeaf( DWORD width, DWORD height , DWORD dwDotSize, std::string );
	OPlaneLeaf( const OPlaneLeaf& );

	virtual ~OPlaneLeaf() {}

	virtual void operator= ( const OPlaneLeaf& );

	//コピーの生成 ( 名前が指定されない場合、メンバのプレーン名が指定される
	virtual OPlaneLeaf* Instance( DWORD width = 0, DWORD height = 0, std::string name = "" ) = 0;

	std::string& LeafName() { return m_leafName; }

	inline DWORD Width() { return m_width; }
	inline DWORD Height() { return m_height; }
	DWORD MemSize() { return m_dwMemSize; }

	//先頭メモリから距離取得
	virtual DISTANCE GetPoint( DWORD dwX, DWORD dwY )
	{
		if ( dwX > m_width || dwY > m_height )
			return 0;

//		return dwX*dwY*DotSize();
		return dwX*DotSize() + Width()*DotSize()* dwY;
	}

	//先頭メモリから距離と自身取得
	virtual DISTANCESRC GetPointObj( DWORD dwX, DWORD dwY )
	{
		DISTANCESRC rtn;

		rtn.first = this;
		rtn.second = GetPoint( dwX, dwY );

		return rtn;
	}

	virtual LPBYTE Point( DISTANCE dwDistance )
	{
		return ((LPBYTE)FisrtMemPointer())+dwDistance;
	}

	//１ドットサイズ取得
	virtual DWORD DotSize() =0 ;//{ return m_DotSize; }
	//メモリコピー
	virtual bool CopyDot( DISTANCE dest, DISTANCESRC src, DWORD dotCnt );
	virtual bool CopyDot( DISTANCESRC dest, DISTANCESRC src, DWORD dotCnt );
	//メモリセット
	virtual void FillDot( DISTANCE dest, BYTE value, DWORD dotCnt ) ;

	//領域を透明で埋める
	virtual void FillClearDot( DISTANCE dest, DWORD dotCnt ) = 0;
	//領域を最大値で埋める
	virtual void FillMaxDot( DISTANCE dest, DWORD dotCnt ) = 0;

public:
	virtual void* FisrtMemPointer() = 0;


private:
	void Init();				//コンストラクタ専用

};


