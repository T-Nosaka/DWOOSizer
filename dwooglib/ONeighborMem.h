/*

	�j�A���X�g�l�C�o�Ǘ�

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
	// ��f���ϊ�
	virtual bool Resize( OImageProcess& imgmem, DWORD dwWidthPrm, DWORD dwHeightPrm );
} ;


