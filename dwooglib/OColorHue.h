/*++

	�F���ϊ��Ǘ�

--*/

#pragma once

class OColorHue
{
protected:
	//�ݒ���
	long m_SetRed;				//�F��
	long m_SetYellow;
	long m_SetGreen;
	long m_SetCyan;
	long m_SetBlue;
	long m_SetMazenta;

	bool m_SetBlackEnable;		//���̕�ԃX�C�b�`
	long m_SetBlackHue;			//���̐F��
	long m_SetBlackLevel;		//���̋P�x (%)

	long m_SetSaturation;		//�ʓx
	long m_SetLightness;		//���x

	//���̓f�[�^
	BYTE m_inR;
	BYTE m_inG;
	BYTE m_inB;

	//�ȉ��A�o�̓f�[�^
	//�F���e�[�u��
	long m_HueWheel[360];

	double m_Lightness;		//���x
	double m_Saturation;	//�ʓx
	double m_Hue;			//�F��

	//�o�̓f�[�^
	BYTE m_outR;
	BYTE m_outG;
	BYTE m_outB;

	//�J���[�z�C�[���擾�p�萔
	static double PAI;
	static double R_OFFSSET;
	static double G_OFFSSET;
	static double B_OFFSSET;

	//�萔��`
	static double HLSMAX;
	static double RGBMAX;
	static double UNDEFINED;

	//�ݒ�͈�
	static double SetBlackHueRANGE;
	static double SetBlackLevelRANGE;
	static double SetSaturationRANGE;
	static double SetLightnessRANGE;


public:
	OColorHue( );

	//�ϊ��p�����[�^I/F
	long& SetRed( ) {			return m_SetRed;}		
	long& SetYellow( ) {		return m_SetYellow;}
	long& SetGreen( ) {			return m_SetGreen;}
	long& SetCyan( ) {			return m_SetCyan;}
	long& SetBlue( ) {			return m_SetBlue;}
	long& SetMazenta( ) {		return m_SetMazenta;}

	bool& SetBlackEnable( ) {	return m_SetBlackEnable;}	
	long& SetBlackHue( ) {		return m_SetBlackHue;}	
	long& SetBlackLevel( ) {	return m_SetBlackLevel;}

	long& SetSaturation( ) {	return m_SetSaturation;}
	long& SetLightness( ) {		return m_SetLightness;}

	//�\�[�X�f�[�^
	BYTE& inR()	{ return m_inR;}
	BYTE& inG()	{ return m_inG;}
	BYTE& inB()	{ return m_inB;}

	//���ʃf�[�^
	BYTE& outR()	{ return m_outR;}
	BYTE& outG()	{ return m_outG;}
	BYTE& outB()	{ return m_outB;}


	//�F���e�[�u���쐬
	bool HueWheel();

	//RGB=>HLS�ϊ�
	void RGBtoHLS();

	//�f�[�^�ϊ�
	bool HLStoRGB();

	//�J���[�z�C�[�������쐬
	bool ColorWheel( double dRad );


protected:
	void Init();

	BYTE* MaximumValue()
	{
		if ( m_inR >= m_inG && m_inR >= m_inB )
			return &m_inR;
		if ( m_inG >= m_inR && m_inG >= m_inB )
			return &m_inG;
		if ( m_inB >= m_inR && m_inB >= m_inG )
			return &m_inB;

		//�S������
		return NULL;
	}

	BYTE* MinimumValue()
	{
		if ( m_inR <= m_inG && m_inR <= m_inB )
			return &m_inR;
		if ( m_inG <= m_inR && m_inG <= m_inB )
			return &m_inG;
		if ( m_inB <= m_inR && m_inB <= m_inG )
			return &m_inB;

		//�S������
		return NULL;
	}

	double Function( double dHue, double cmin, double cmax );


};


