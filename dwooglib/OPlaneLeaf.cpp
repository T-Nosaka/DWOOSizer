/*++

	‰æ‘œƒƒ‚ƒŠŠî’ê

--*/

#include <windows.h>

#include "OPlaneLeaf.h"

OPlaneLeaf::OPlaneLeaf()
{
	Init();
}

OPlaneLeaf::OPlaneLeaf( DWORD width, DWORD height , DWORD dwDotSize, std::string leafName )
{
	Init();

	m_width = width;
	m_height = height;
	m_leafName = leafName;
	m_DotSize = dwDotSize;

	m_dwMemSize = m_width*m_height*m_DotSize;

}


void OPlaneLeaf::Init()
{
	m_dwMemSize = 0;
	m_leafName = "";
	m_width = 0;
	m_height = 0;

}


OPlaneLeaf::OPlaneLeaf( const OPlaneLeaf& instance )
{
	*this = instance;
}

void OPlaneLeaf::operator= ( const OPlaneLeaf& instance )
{
	m_leafName = instance.m_leafName;
	m_dwMemSize = instance.m_dwMemSize;
	m_width = instance.m_width;
	m_height = instance.m_height;
}


bool OPlaneLeaf::CopyDot( DISTANCE dest, DISTANCESRC src, DWORD dotCnt )
{
	if ( DotSize() == src.first->DotSize() )
	{
		MEMCPY( ((char*)FisrtMemPointer())+dest, 
			((char*)((src.first)->FisrtMemPointer())) + src.second,
			dotCnt * DotSize() );

		return true;
	}

	return false;
}

bool OPlaneLeaf::CopyDot( DISTANCESRC dest, DISTANCESRC src, DWORD dotCnt )
{
	if ( DotSize() == src.first->DotSize() &&
		dest.first->DotSize() == DotSize() )
	{
		MEMCPY( ((char*)((dest.first)->FisrtMemPointer())) + dest.second,
			((char*)((src.first)->FisrtMemPointer())) + src.second,
			dotCnt * DotSize() );

		return true;
	}

	return false;
}

void OPlaneLeaf::FillDot( DISTANCE dest, BYTE value, DWORD dotCnt )
{
	MEMSET( ((char*)FisrtMemPointer())+dest, value, dotCnt * DotSize() );
}

