/*

	ニアレストネイバ管理

*/

#pragma once

#include "OResizeMem.h"

class DLLCLASSIMPEXP ONeighborMem : public OResizeMem
{
protected:

public:
	ONeighborMem() {}

	ONeighborMem( const ONeighborMem& instance )
	{
		((OResizeMem&)*this) = instance;
	}

	ONeighborMem( const OImageProcess& instance )
	{
		((OImageProcess&)*this) = instance;
	}

	ONeighborMem( const OResizeMem& instance )
	{
		((OResizeMem&)*this) = instance;
	}

	virtual ~ONeighborMem( ) {}

	void operator= ( const ONeighborMem& instance )
	{
		((OResizeMem&)*this) = (OResizeMem&)instance;
	}

	///
	// 画素数変換
	virtual bool Resize( OImageProcess& imgmem, DWORD dwWidthPrm, DWORD dwHeightPrm );
} ;


