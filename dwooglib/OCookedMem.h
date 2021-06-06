/*++

	HLS変換 I/F 色相変換管理

--*/

#pragma once

#include "ORgbIfMem.h"

class DLLCLASSIMPEXP OCookedMem : public ORgbIfMem
{
public:

	//コンストラクタ
	OCookedMem() : ORgbIfMem()
	{
		Init();
	}

	//コピーコンストラクタ
	OCookedMem( const OCookedMem& instance )
	{
		((ORgbIfMem&)*this) = instance;

		m_SetSaturation = ((OCookedMem&)instance).m_SetSaturation;
		m_SetLightness = ((OCookedMem&)instance).m_SetLightness;
	}

	//基底コピーコンストラクタ
	OCookedMem( const OImageProcess& instance )
	{
		Init();

		((OImageProcess&)*this) = instance;
	}

	//基底コピーコンストラクタ
	OCookedMem( const ORgbIfMem& instance )
	{
		Init();

		((ORgbIfMem&)*this) = instance;
	}

	//デストラクタ
	virtual ~OCookedMem( ) {}

	//コピー演算子
	void operator= ( const OCookedMem& instance )
	{
		((ORgbIfMem&)*this) = (ORgbIfMem&)instance;
	}

protected:

	//初期値設定
	void Init()
	{
		m_SetSaturation = 0;
		m_SetLightness = 0;
	}

	//インスタンス作成  必須
	virtual OImageProcess* Instance()
	{
		OImageProcess* pInstance = new OCookedMem();

		return pInstance;
	}

	long m_SetSaturation;		//彩度
	long m_SetLightness;		//明度

public:

	long& SetSaturation( ) {	return m_SetSaturation;}
	long& SetLightness( ) {		return m_SetLightness;}

	//HLS変換
	bool Function();
};


