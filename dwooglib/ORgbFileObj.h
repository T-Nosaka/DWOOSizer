/*++

	画像ファイル実装

--*/

#pragma once

#include "ORgbObj.h"

class ORgbFileObj : public ORgbObj 
{
protected:

	class FileExecp
	{
	};

public:
	ORgbFileObj() : ORgbObj() {}

	ORgbFileObj( const ORgbFileObj& );


	bool FileRead( LPCTSTR pFileName );

	//いつか、サポート
	bool FileWrite( LPCTSTR pFileName );

};


