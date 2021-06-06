/*++

	Bitmap I/F �摜�������Ǘ�

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

	//Bitmap�Ƃ� I/F 
	bool SetBitmapRgb24( ORgbBmpObj& );

	//�������ɃA�h���X���Z�b�g���邱�ƂŁA�㏑������B
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

