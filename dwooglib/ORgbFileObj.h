/*++

	�摜�t�@�C������

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

	//�����A�T�|�[�g
	bool FileWrite( LPCTSTR pFileName );

};


