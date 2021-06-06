/*

	リサイズ画像メモリ管理

*/


#include <windows.h>

#include "OResizeMem.h"
#include "OPlaneLeafRgb24.h"


void OResizeMem::DettachPlane( std::string name )
{
	//画像プレーン解放
	PLANELIST::iterator it ;

	for( it = m_planelist.begin(); it!=m_planelist.end() && m_planelist.size()>0; it++ )
	{
		if ( *it != NULL )
		{
			if ( (*it)->LeafName() == name )
			{
				m_planelist.erase( it );
				it = m_planelist.begin();
			}
		}
	}
}

void OResizeMem::GetSrcImage( OImageProcess& imgmem )
{
	//ターゲットを24BitRGBとする
	OPlaneLeafRgb24* pRed = (OPlaneLeafRgb24*)imgmem.GetPlaneEx( OPlaneLeafRgb24::REDNAME );
	OPlaneLeafRgb24* pGreen = (OPlaneLeafRgb24*)imgmem.GetPlaneEx( OPlaneLeafRgb24::GREENNAME );
	OPlaneLeafRgb24* pBlue = (OPlaneLeafRgb24*)imgmem.GetPlaneEx( OPlaneLeafRgb24::BLUENAME );

	if ( pRed == NULL ||
		 pGreen == NULL ||
		 pBlue == NULL )
		return ;

	ListDelete();

	//元画像として追加
	//プレーンを追加
	m_planelist.push_back( pRed );
	m_planelist.push_back( pGreen );
	m_planelist.push_back( pBlue );
}

