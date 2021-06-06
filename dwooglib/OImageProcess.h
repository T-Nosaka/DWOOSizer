/*++

	�摜�������

--*/

#pragma once

#include <vector>

#include "OPlaneLeaf.h"

class DLLCLASSIMPEXP OImageProcess
{
public:
	typedef std::vector< OPlaneLeaf* > PLANELIST;

protected:
	DWORD m_TopX;	//�g�b�vX
	DWORD m_TopY;	//�g�b�vY

	DWORD m_ulHorzRes;
	DWORD m_ulVertRes;	//���]�����[�V���� ��]�����ɕK�v

	PLANELIST m_planelist;	//�摜�v���[�����X�g

public:

	OImageProcess();
	~OImageProcess();

	OImageProcess( const OImageProcess& );

	void operator=(const OImageProcess& ) ;

	//�摜���d�˂�
	OImageProcess* operator+(const OImageProcess& );

	//���C���擾
	OImageProcess* GetLine( DWORD, DWORD );

	//�����ɗ̈�擾
	OImageProcess* GetPerpendicularArea( DWORD , DWORD );

	//���W�擾
	DWORD& GetTopX() { return m_TopX; }
	DWORD& GetTopY() { return m_TopY; }

	//�i�[�̈�擾
	DWORD GetWidth();
	DWORD GetHeight();

	//�_���v�o��
	virtual void FileOut( LPCTSTR ) {}

	//����������f�̃f�[�^���쐬
	OImageProcess* MakeAreaBoxH( DWORD );

	//����������f�̃f�[�^���쐬
	OImageProcess* MakeAreaBoxV( DWORD );

	///
	// �v���[���擾 (���J)
	///
	OPlaneLeaf* GetPlaneEx( std::string name )
	{
		return GetPlane( name );
	}

protected:
	virtual OImageProcess* Instance()
	{
		OImageProcess* pInstance = new OImageProcess();

		return pInstance;
	}

	virtual OPlaneLeaf* GetPlane( std::string );

	virtual void RemovePlane( std::string, bool bDestroy = true);

	OImageProcess* Append( const OImageProcess& );

	void ListDelete();

	//���l�̊ۂ�
	virtual inline double DefRound( double dSrcValue )
	{
		return dSrcValue + 0.5;
	}

private:
	void Init();


	void ListCopy( PLANELIST& destList );

};


