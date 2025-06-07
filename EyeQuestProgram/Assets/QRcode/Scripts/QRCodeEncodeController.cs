/// <summary>
/// write by 52cwalk,if you have some question ,please contract lycwalk@gmail.com
/// </summary>
/// 


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

using ZXing;
using ZXing.QrCode;
using ZXing.QrCode.Internal;
using ZXing.Common;
using System.Text.RegularExpressions;
using System;

using UnityEngine.UI;

public class QRCodeEncodeController : MonoBehaviour {

    [Serializable]
    public class UnityEventTexture2D : UnityEvent<Texture2D> { };


	public RawImage qrCodeImage;
	public RawImage BarCodeImage;
	public enum CodeMode
	{
		QR_CODE,
        CODE_39,
        CODE_128,
        EAN_8,
        EAN_13,
        //DATA_MATRIX,
        NONE
	}

	public Texture2D m_EncodedTex;
	public Texture2D m_EncodedTex_BarCode;
	public int e_QRCodeWidth = 512;
	public int e_QRCodeHeight = 512;

	BitMatrix byteMatrix;
	BitMatrix byteMatrix_BarCode;
	public CodeMode eCodeFormat = CodeMode.QR_CODE;
	public Texture2D e_QRLogoTex;
	Texture2D tempLogoTex = null;
	public float e_EmbedLogoRatio = 0.2f;

    public UnityEventTexture2D onQREncodeFinished;

    void Start ()
	{
		int targetWidth = Mathf.Min(e_QRCodeWidth,e_QRCodeHeight);
		targetWidth = Mathf.Clamp (targetWidth, 128, 1024);
		e_QRCodeWidth = e_QRCodeHeight = targetWidth;
	}

	void Update ()
	{

	}

	/// <summary>
	/// Encode the specified string .
	/// </summary>
	/// <param name="valueStr"> content string.</param>
	public int Encode(string valueStr, CodeMode _Type)
	{
		Debug.Log("QR Code :" + valueStr);
		//	var writer = new QRCodeWriter();
		var writer = new MultiFormatWriter();
		Dictionary<EncodeHintType, object> hints = new Dictionary<EncodeHintType, object>();
		//set the code type
		hints.Add(EncodeHintType.CHARACTER_SET, "UTF-8");
		hints.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.H);

		switch (CodeMode.QR_CODE)
		{
			case CodeMode.QR_CODE:
				byteMatrix = writer.encode(valueStr, BarcodeFormat.QR_CODE, e_QRCodeWidth, e_QRCodeHeight, hints);
				break;
			case CodeMode.EAN_13:
				if ((valueStr.Length == 12 || valueStr.Length == 13) && bAllDigit(valueStr))
				{
					if (valueStr.Length == 13)
					{
						valueStr = valueStr.Substring(0, 12);
					}
					byteMatrix = writer.encode(valueStr, BarcodeFormat.EAN_13, e_QRCodeWidth, e_QRCodeWidth / 2, hints);
				}
				else
				{

					return -13;
				}
				break;
			case CodeMode.EAN_8:
				if ((valueStr.Length == 7 || valueStr.Length == 8) && bAllDigit(valueStr))
				{
					if (valueStr.Length == 8)
					{
						valueStr = valueStr.Substring(0, 7);
					}
					byteMatrix = writer.encode(valueStr, BarcodeFormat.EAN_8, e_QRCodeWidth, e_QRCodeWidth / 2, hints);
				}
				else
				{
					return -8;
				}
				break;
			case CodeMode.CODE_128:
				if (IsNumAndEnCh(valueStr) && valueStr.Length <= 80)
				{
					byteMatrix = writer.encode(valueStr, BarcodeFormat.CODE_128, e_QRCodeWidth, e_QRCodeWidth / 2, hints);
				}
				else
				{
					return -128;
				}
				break;
			case CodeMode.CODE_39:
				if (bAllDigit(valueStr))
				{
					byteMatrix = writer.encode(valueStr, BarcodeFormat.CODE_39, e_QRCodeWidth, e_QRCodeHeight / 2, hints);
				}
				else
				{
					return -39;
				}

				break;

			case CodeMode.NONE:
				return -1;
		}
		

		switch (_Type)
		{
			case CodeMode.QR_CODE:

				Debug.Log("QR_CODE CHECK");

					if (m_EncodedTex != null)
					{
						Destroy(m_EncodedTex);
						m_EncodedTex = null;
					}
					m_EncodedTex = new Texture2D(byteMatrix.Width, byteMatrix.Height);

					for (int i = 0; i != m_EncodedTex.width; i++)
					{
						for (int j = 0; j != m_EncodedTex.height; j++)
						{
							if (byteMatrix[i, j])
							{
								m_EncodedTex.SetPixel(i, j, Color.black);
							}
							else
							{
								m_EncodedTex.SetPixel(i, j, Color.white);
							}
						}
					}

					///rotation the image 
					Color32[] pixels = m_EncodedTex.GetPixels32();
					//pixels = RotateMatrixByClockwise(pixels, m_EncodedTex.width);
					m_EncodedTex.SetPixels32(pixels);

					m_EncodedTex.Apply();


					int width = m_EncodedTex.width;
					int height = m_EncodedTex.height;
					float aspect = width * 1.0f / height;
					qrCodeImage.GetComponent<RectTransform>().sizeDelta = new Vector2(170, 170.0f / aspect);
					qrCodeImage.texture = m_EncodedTex;
					return 0;
				
				break;
			case CodeMode.CODE_39:

				break;
		}

		return 0;
	}

    public void _EncodeBarCode(string valueStr, CodeMode _Type)
    {
        Debug.Log("EncodeBarCode :" + valueStr);
        //	var writer = new QRCodeWriter();
        var writer = new MultiFormatWriter();
        Dictionary<EncodeHintType, object> hints = new Dictionary<EncodeHintType, object>();
        //set the code type
        hints.Add(EncodeHintType.CHARACTER_SET, "UTF-8");
        hints.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.H);

        switch (_Type)
        {
            case CodeMode.QR_CODE:
                byteMatrix_BarCode = writer.encode(valueStr, BarcodeFormat.QR_CODE, e_QRCodeWidth, e_QRCodeHeight, hints);
                break;
            case CodeMode.EAN_13:
                if ((valueStr.Length == 12 || valueStr.Length == 13) && bAllDigit(valueStr))
                {
                    if (valueStr.Length == 13)
                    {
                        valueStr = valueStr.Substring(0, 12);
                    }
                    byteMatrix_BarCode = writer.encode(valueStr, BarcodeFormat.EAN_13, e_QRCodeWidth, e_QRCodeWidth / 2, hints);
                }
  
                break;
            case CodeMode.EAN_8:
                if ((valueStr.Length == 7 || valueStr.Length == 8) && bAllDigit(valueStr))
                {
                    if (valueStr.Length == 8)
                    {
                        valueStr = valueStr.Substring(0, 7);
                    }
                    byteMatrix_BarCode = writer.encode(valueStr, BarcodeFormat.EAN_8, e_QRCodeWidth, e_QRCodeWidth / 2, hints);
                }
   
                break;
            case CodeMode.CODE_128:
                if (IsNumAndEnCh(valueStr) && valueStr.Length <= 80)
                {
                    byteMatrix_BarCode = writer.encode(valueStr, BarcodeFormat.CODE_128, e_QRCodeWidth, e_QRCodeWidth / 2, hints);
                }
 
                break;
            case CodeMode.CODE_39:
                if (bAllDigit(valueStr))
                {
                    byteMatrix_BarCode = writer.encode(valueStr, BarcodeFormat.CODE_39, e_QRCodeWidth, e_QRCodeHeight / 2, hints);
                }

                break;
        }


        Debug.Log("CODE_39 CHECK");

        if (m_EncodedTex_BarCode != null)
        {
            Destroy(m_EncodedTex_BarCode);
            m_EncodedTex_BarCode = null;
        }

        m_EncodedTex_BarCode = new Texture2D(byteMatrix_BarCode.Width, byteMatrix_BarCode.Height);

        for (int i = 0; i != m_EncodedTex_BarCode.width; i++)
        {
            for (int j = 0; j != m_EncodedTex_BarCode.height; j++)
            {
                if (byteMatrix_BarCode[i, j])
                {
                    m_EncodedTex_BarCode.SetPixel(i, j, Color.black);
                }
                else
                {
                    m_EncodedTex_BarCode.SetPixel(i, j, Color.white);
                }
            }
        }

        ///rotation the image 
        Color32[] pixels = m_EncodedTex_BarCode.GetPixels32();
        //pixels = RotateMatrixByClockwise(pixels, m_EncodedTex.width);
        m_EncodedTex_BarCode.SetPixels32(pixels);

        m_EncodedTex_BarCode.Apply();

        int width = m_EncodedTex_BarCode.width;
        int height = m_EncodedTex_BarCode.height;
        float aspect = width * 1.0f / height;
        BarCodeImage.GetComponent<RectTransform>().sizeDelta = new Vector2(170, 170.0f / aspect);
        BarCodeImage.texture = m_EncodedTex_BarCode;

		Debug.Log("DONE");

    }

    static Color32[] RotateMatrixByClockwise(Color32[] matrix, int n)
		{
			Color32[] ret = new Color32[n * n];
			for (int i = 0; i < n; ++i)
			{
				for (int j = 0; j < n; ++j)
				{
					ret[i * n + j] = matrix[(n - i - 1) * n + j];
				}
			}
			return ret;
		}

		/// <summary>
		/// anticlockwise
		/// </summary>
		/// <returns>The matrix.</returns>
		/// <param name="matrix">Matrix.</param>
		/// <param name="n">N.</param>
		static Color32[] RotateMatrixByAnticlockwise(Color32[] matrix, int n)
		{
			Color32[] ret = new Color32[n * n];

			for (int i = 0; i < n; ++i)
			{
				for (int j = 0; j < n; ++j)
				{
					ret[i * n + j] = matrix[(n - j - 1) * n + i];
				}
			}
			return ret;
		}


		bool isContainDigit(string str)
		{
			for (int i = 0; i != str.Length; i++)
			{
				if (str[i] >= '0' && str[i] <= '9')
				{
					return true;
				}
			}
			return false;
		}

		bool IsNumAndEnCh(string input)
		{
			string pattern = @"^[A-Za-z0-9-_!@# |+/*]+$";
			Regex regex = new Regex(pattern);
			return regex.IsMatch(input);
		}


		bool isContainChar(string str)
		{
			for (int i = 0; i != str.Length; i++)
			{
				if (str[i] >= 'a' && str[i] <= 'z')
				{
					return true;
				}
			}
			return false;
		}

		bool bAllDigit(string str)
		{
			for (int i = 0; i != str.Length; i++)
			{
				if (str[i] >= '0' && str[i] <= '9')
				{
				}
				else
				{
					return false;
				}
			}
			return true;
		}

}
