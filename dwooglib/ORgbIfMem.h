/*++

	Bitmap I/F 画像メモリ管理

--*/

#pragma once

#include "OImageProcess.h"

#include "ORgbBmpObj.h"

class DLLCLASSIMPEXP ORgbIfMem : public OImageProcess
{
protected:

public:

	ORgbIfMem() : OImageProcess()
	{
	}

	ORgbIfMem( const OImageProcess& instance )
	{
		(OImageProcess&)*this = instance;
	}

	ORgbIfMem( const ORgbIfMem& instance )
	{
		*this = instance;
	}

	void operator=(const ORgbIfMem& instance )
	{
		(OImageProcess&)*this = (OImageProcess&)instance;
	}

	//Bitmapとの I/F 
	bool SetBitmapRgb24( ORgbBmpObj& );

	//メモリにアドレスをセットすることで、上書きする。
	bool GetBitmapRgb24( ORgbBmpObj&, LPBYTE pMemory = NULL );

protected:
	virtual OImageProcess* Instance()
	{
		OImageProcess* pInstance = new ORgbIfMem();

		return pInstance;
	}

	virtual inline double DefRound( double dSrcValue )
	{
		return OImageProcess::DefRound( dSrcValue );
	}

};

