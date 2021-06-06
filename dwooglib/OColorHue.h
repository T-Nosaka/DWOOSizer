/*++

	色相変換管理

--*/

#pragma once

class OColorHue
{
protected:
	//設定情報
	long m_SetRed;				//色相
	long m_SetYellow;
	long m_SetGreen;
	long m_SetCyan;
	long m_SetBlue;
	long m_SetMazenta;

	bool m_SetBlackEnable;		//黒の補間スイッチ
	long m_SetBlackHue;			//黒の色相
	long m_SetBlackLevel;		//黒の輝度 (%)

	long m_SetSaturation;		//彩度
	long m_SetLightness;		//明度

	//入力データ
	BYTE m_inR;
	BYTE m_inG;
	BYTE m_inB;

	//以下、出力データ
	//色相テーブル
	long m_HueWheel[360];

	double m_Lightness;		//明度
	double m_Saturation;	//彩度
	double m_Hue;			//色相

	//出力データ
	BYTE m_outR;
	BYTE m_outG;
	BYTE m_outB;

	//カラーホイール取得用定数
	static double PAI;
	static double R_OFFSSET;
	static double G_OFFSSET;
	static double B_OFFSSET;

	//定数定義
	static double HLSMAX;
	static double RGBMAX;
	static double UNDEFINED;

	//設定範囲
	static double SetBlackHueRANGE;
	static double SetBlackLevelRANGE;
	static double SetSaturationRANGE;
	static double SetLightnessRANGE;


public:
	OColorHue( );

	//変換パラメータI/F
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

	//ソースデータ
	BYTE& inR()	{ return m_inR;}
	BYTE& inG()	{ return m_inG;}
	BYTE& inB()	{ return m_inB;}

	//結果データ
	BYTE& outR()	{ return m_outR;}
	BYTE& outG()	{ return m_outG;}
	BYTE& outB()	{ return m_outB;}


	//色相テーブル作成
	bool HueWheel();

	//RGB=>HLS変換
	void RGBtoHLS();

	//データ変換
	bool HLStoRGB();

	//カラーホイール成分作成
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

		//全部同一
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

		//全部同一
		return NULL;
	}

	double Function( double dHue, double cmin, double cmax );


};


