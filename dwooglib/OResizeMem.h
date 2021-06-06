/*++

	画像リサイズ抽象クラス

--*/

#pragma once

#include "ORgbIfMem.h"

class DLLCLASSIMPEXP OResizeMem : public ORgbIfMem
{
public:

	///
	// 倍率変換
	virtual bool TransL( double dMagnification = 0.7 )
	{
		return false;
	}

	///
	// 画素数変換
	virtual bool Resize( OImageProcess& , DWORD dwWidthPrm, DWORD dwHeightPrm )
	{
		return false;
	}

	///
	// 元画像取得
	// ※但し、ポイントを移動するだけなので、破棄禁止
	///
	void GetSrcImage( OImageProcess& imgmem );

protected:

	///
	// プレーン解放
	///
	void DettachPlane( std::string );

};
