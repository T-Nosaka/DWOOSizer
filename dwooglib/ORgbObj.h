/*++

	�摜�t�@�C������

--*/

#pragma once

#include "OPlaneDllDef.h"

class DLLCLASSIMPEXP ORgbObj
{
protected:
	//�i�[������
	LPBYTE m_pImage;	//�C���[�W�f�[�^
	DWORD  m_dwWidth;
	DWORD  m_dwHeight;
	long   m_rowBytes;	//1�s���̃o�C�g��

public:
	ORgbObj();
	~ORgbObj();

	ORgbObj( const ORgbObj& instance ) { *this = instance; }

	//�R�s�[
	void operator= ( const ORgbObj& );

	DWORD Width() { return m_dwWidth; }
	DWORD Height() { return m_dwHeight; }

	long RowByte() { return m_rowBytes; }

	LPBYTE Image() { return m_pImage; }

	void SetImage( DWORD dwWidth, DWORD dwHeight, DWORD dwRowByte, LPBYTE pImage ) ;

	//�o��
	long GetMemorySize() { return m_dwHeight * m_rowBytes; }

	//�C���[�W�������R�s�[
	void ImageCopy( void* dest );

protected:
	void Init();
	virtual void DropImage() ;

};

