/*++

	Windows
	汎用

	画像ファイル実装

--*/

#include <windows.h>

#include "ORgbFileObj.h"

#pragma warning(disable:4101)

ORgbFileObj::ORgbFileObj( const ORgbFileObj& instance )
{
	(ORgbObj&)*this = (ORgbObj&)instance;
}

bool ORgbFileObj::FileRead( LPCTSTR pFileName )
{
	LPBYTE pMemory = NULL;
	//ビットマップ構造
	BITMAPFILEHEADER bitmapfilehead ;
	BITMAPINFOHEADER bitmapinfhead ;

	memset( &bitmapfilehead, (char)NULL, sizeof(bitmapfilehead) );
	memset( &bitmapinfhead, (char)NULL, sizeof(bitmapinfhead) );

	HANDLE hFile = CreateFile(	pFileName, 
								GENERIC_READ, 
								FILE_SHARE_READ, 
								NULL, 
								OPEN_EXISTING, 
								FILE_ATTRIBUTE_NORMAL, 
								NULL );
	if ( hFile == INVALID_HANDLE_VALUE )
	{
		return false;
	}

	try
	{
		DWORD dwRead = 0;
		BOOL bResult = FALSE;
		DWORD dwSetFilePointerResult = 0;
		LONG dwSetFilePointerHigh = 0;

		//ヘッダ情報読み込み
		bResult = ReadFile(
							hFile, 
							&bitmapfilehead, 
							sizeof(bitmapfilehead), 
							&dwRead, 
							NULL );
		if ( bResult == FALSE )
			throw FileExecp();

		//INF情報読み込み
		bResult = ReadFile(
							hFile, 
							&bitmapinfhead, 
							sizeof(bitmapinfhead), 
							&dwRead, 
							NULL );
		if ( bResult == FALSE )
			throw FileExecp();

		//種別確認 後々拡張したいな
		if((bitmapfilehead.bfType != MAKEWORD('B', 'M'))
			||(bitmapinfhead.biPlanes != 1)
			||(bitmapinfhead.biBitCount != 24)
			||(bitmapinfhead.biCompression != 0))
		{
			throw FileExecp();
		}

		//画像へポイント移動
		dwSetFilePointerResult = SetFilePointer(
									hFile, 
									bitmapfilehead.bfOffBits, 
									&dwSetFilePointerHigh, 
									FILE_BEGIN );
		if ( dwSetFilePointerResult == 0xFFFFFFFF )
			throw FileExecp();

		//メモリ確保
		pMemory = new BYTE[ bitmapfilehead.bfSize ];

		//画像情報読み込み
		bResult = ReadFile(
							hFile, 
							pMemory, 
							bitmapfilehead.bfSize - bitmapfilehead.bfOffBits, 
							&dwRead, 
							NULL );
		if ( bResult == FALSE )
			throw FileExecp();


	}
	catch( FileExecp excp )
	{
		CloseHandle( hFile );

		if ( pMemory != NULL )
			delete [] pMemory;

		return false;
	}

	CloseHandle( hFile );

	//読み込めた画像を設定
	m_pImage = pMemory;

	m_dwWidth = bitmapinfhead.biWidth;
	m_dwHeight = bitmapinfhead.biHeight;
	m_rowBytes = ( bitmapinfhead.biWidth * 3 + 3 ) / 4 * 4;	//4バイト境界を考慮

	return true;
}



bool ORgbFileObj::FileWrite( LPCTSTR pFileName )
{
	if ( m_pImage == NULL )
		return false;

	LPBYTE pMemory = NULL;
	//ビットマップ構造
	BITMAPFILEHEADER bitmapfilehead ;
	BITMAPINFOHEADER bitmapinfhead ;

	memset( &bitmapfilehead, (char)NULL, sizeof(bitmapfilehead) );
	memset( &bitmapinfhead, (char)NULL, sizeof(bitmapinfhead) );

	HANDLE hFile = CreateFile(	pFileName, 
								GENERIC_WRITE, 
								0, 
								NULL, 
								CREATE_ALWAYS, 
								FILE_ATTRIBUTE_NORMAL, 
								NULL );
	if ( hFile == INVALID_HANDLE_VALUE )
	{
		return false;
	}

	//ヘッダ作成 24Bit専用
	

	DWORD dwRowBytes = ( m_dwWidth * 3 + 3 ) / 4 * 4;	//4バイト境界を考慮
	DWORD dwImageSize = m_dwHeight * dwRowBytes;
	DWORD dwFileSize = ( dwImageSize + sizeof( bitmapfilehead ) + sizeof( bitmapinfhead ) );

	memset( &bitmapfilehead, (char)NULL, sizeof( bitmapfilehead ) );
	bitmapfilehead.bfType = MAKEWORD('B', 'M');
	bitmapfilehead.bfSize = dwFileSize;
	bitmapfilehead.bfOffBits = sizeof( bitmapfilehead ) + sizeof( bitmapinfhead );

	bitmapinfhead.biSize = sizeof(bitmapinfhead);
	bitmapinfhead.biWidth = m_dwWidth;
	bitmapinfhead.biHeight = m_dwHeight;
	bitmapinfhead.biPlanes = 1;
	bitmapinfhead.biBitCount = 24;
	bitmapinfhead.biCompression = 0;
	bitmapinfhead.biSizeImage = 0;
	bitmapinfhead.biXPelsPerMeter = 2834;
	bitmapinfhead.biYPelsPerMeter = 2834;
	bitmapinfhead.biClrUsed = 0;
	bitmapinfhead.biClrImportant = 0;

	try
	{
		DWORD lpNumberOfBytesToWrite ;
		BOOL bResult = FALSE;
		DWORD dwSetFilePointerResult = 0;
		LONG dwSetFilePointerHigh = 0;

		//ヘッダ情報書き込み
		bResult = WriteFile(
							hFile, 
							&bitmapfilehead, 
							sizeof(bitmapfilehead), 
							&lpNumberOfBytesToWrite, 
							NULL );
		if ( bResult == FALSE )
			throw FileExecp();

		//INF情報書き込み
		bResult = WriteFile(
							hFile, 
							&bitmapinfhead, 
							sizeof(bitmapinfhead), 
							&lpNumberOfBytesToWrite, 
							NULL );
		if ( bResult == FALSE )
			throw FileExecp();

		//画像情報読み込み
		bResult = WriteFile(
							hFile, 
							m_pImage, 
							dwImageSize, 
							&lpNumberOfBytesToWrite, 
							NULL );
		if ( bResult == FALSE )
			throw FileExecp();
	}
	catch( FileExecp excp )
	{
		CloseHandle( hFile );

		if ( pMemory != NULL )
			delete [] pMemory;

		return false;
	}

	CloseHandle( hFile );

	return true;
}
