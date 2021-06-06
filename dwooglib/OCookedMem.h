/*++

	HLS�ϊ� I/F �F���ϊ��Ǘ�

--*/

#pragma once

#include "ORgbIfMem.h"

class DLLCLASSIMPEXP OCookedMem : public ORgbIfMem
{
public:

	//�R���X�g���N�^
	OCookedMem() : ORgbIfMem()
	{
		Init();
	}

	//�R�s�[�R���X�g���N�^
	OCookedMem( const OCookedMem& instance )
	{
		((ORgbIfMem&)*this) = instance;

		m_SetSaturation = ((OCookedMem&)instance).m_SetSaturation;
		m_SetLightness = ((OCookedMem&)instance).m_SetLightness;
	}

	//���R�s�[�R���X�g���N�^
	OCookedMem( const OImageProcess& instance )
	{
		Init();

		((OImageProcess&)*this) = instance;
	}

	//���R�s�[�R���X�g���N�^
	OCookedMem( const ORgbIfMem& instance )
	{
		Init();

		((ORgbIfMem&)*this) = instance;
	}

	//�f�X�g���N�^
	virtual ~OCookedMem( ) {}

	//�R�s�[���Z�q
	void operator= ( const OCookedMem& instance )
	{
		((ORgbIfMem&)*this) = (ORgbIfMem&)instance;
	}

protected:

	//�����l�ݒ�
	void Init()
	{
		m_SetSaturation = 0;
		m_SetLightness = 0;
	}

	//�C���X�^���X�쐬  �K�{
	virtual OImageProcess* Instance()
	{
		OImageProcess* pInstance = new OCookedMem();

		return pInstance;
	}

	long m_SetSaturation;		//�ʓx
	long m_SetLightness;		//���x

public:

	long& SetSaturation( ) {	return m_SetSaturation;}
	long& SetLightness( ) {		return m_SetLightness;}

	//HLS�ϊ�
	bool Function();
};


