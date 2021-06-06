/*++

	画像処理基底

--*/

#pragma once

#include <vector>

#include "OPlaneLeaf.h"

class DLLCLASSIMPEXP OImageProcess
{
public:
	typedef std::vector< OPlaneLeaf* > PLANELIST;

protected:
	DWORD m_TopX;	//トップX
	DWORD m_TopY;	//トップY

	DWORD m_ulHorzRes;
	DWORD m_ulVertRes;	//レゾリューション 回転処理に必要

	PLANELIST m_planelist;	//画像プレーンリスト

public:

	OImageProcess();
	~OImageProcess();

	OImageProcess( const OImageProcess& );

	void operator=(const OImageProcess& ) ;

	//画像を重ねる
	OImageProcess* operator+(const OImageProcess& );

	//ライン取得
	OImageProcess* GetLine( DWORD, DWORD );

	//垂直に領域取得
	OImageProcess* GetPerpendicularArea( DWORD , DWORD );

	//座標取得
	DWORD& GetTopX() { return m_TopX; }
	DWORD& GetTopY() { return m_TopY; }

	//格納領域取得
	DWORD GetWidth();
	DWORD GetHeight();

	//ダンプ出力
	virtual void FileOut( LPCTSTR ) {}

	//同じ水平画素のデータを作成
	OImageProcess* MakeAreaBoxH( DWORD );

	//同じ垂直画素のデータを作成
	OImageProcess* MakeAreaBoxV( DWORD );

	///
	// プレーン取得 (公開)
	///
	OPlaneLeaf* GetPlaneEx( std::string name )
	{
		return GetPlane( name );
	}

protected:
	virtual OImageProcess* Instance()
	{
		OImageProcess* pInstance = new OImageProcess();

		return pInstance;
	}

	virtual OPlaneLeaf* GetPlane( std::string );

	virtual void RemovePlane( std::string, bool bDestroy = true);

	OImageProcess* Append( const OImageProcess& );

	void ListDelete();

	//数値の丸め
	virtual inline double DefRound( double dSrcValue )
	{
		return dSrcValue + 0.5;
	}

private:
	void Init();


	void ListCopy( PLANELIST& destList );

};


