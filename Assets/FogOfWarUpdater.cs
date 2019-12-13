using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWarUpdater : MonoBehaviour {

	// Use this for initialization
	[SerializeField]
	public int _textureSize;

	[SerializeField]
	public int _radius;

	[SerializeField]
	public Transform car;
	Texture2D _texture;
	Texture2D _mapTexture;
	Texture2D _noMapTexture;
	Color[] _pixels;
	Vector2 _centerPixel;

	int _pixelsPerUnit;
	Material fogOfWarMat = null;

	public void SetRendererActive()
	{
		GetComponent<Renderer>().enabled = true;
	}

	private void Awake()
	{

		if (!SystemInfo.supportsComputeShaders)
		{
			Debug.LogWarning("No Compute Shader support!");
		}
		SetRendererActive();
		var renderer = GetComponent<Renderer>();
		fogOfWarMat = null;
		if (renderer != null)
		{
			fogOfWarMat = renderer.material;
		}

		if (fogOfWarMat == null)
		{
			Debug.LogError("Material for Fog Of War not found!");
			return;
		}

		_mapTexture = new Texture2D(_textureSize, _textureSize, TextureFormat.RGBA32, false);
		_mapTexture.wrapMode = TextureWrapMode.Clamp;

		_noMapTexture = new Texture2D(_textureSize, _textureSize, TextureFormat.RGBA32, false);
		_noMapTexture.wrapMode = TextureWrapMode.Clamp;

		_texture = _mapTexture;

		_pixels = _texture.GetPixels();
		ClearPixels();
		_mapTexture.SetPixels(_pixels);
		_mapTexture.Apply(false);
		_noMapTexture.SetPixels(_pixels);
		_noMapTexture.Apply(false);
		fogOfWarMat.mainTexture = _texture;

		// _revealers = new List<Revealer>();
		
		_pixelsPerUnit = Mathf.RoundToInt(_textureSize / transform.lossyScale.x);

		_centerPixel = new Vector2(_textureSize * 0.5f, _textureSize * 0.5f);
		TeamManager.teamChanged += SwitchMap;
	}

	void SwitchMap()
	{
		bool cart = TeamManager.IsInTeam("Cartographer");
		if(cart)
		{
			_texture = _mapTexture;
			_radius = 5;
		}
		else
		{
			_radius = 10;
			_texture = _noMapTexture;
		}
		fogOfWarMat.mainTexture = _texture;
		_pixels = _texture.GetPixels();

	}

	private void CreateCircle(int originX, int originY, int radius)
	{
		bool hasCartographer = TeamManager.IsInTeam("Cartographer");
		for (var y = -radius * _pixelsPerUnit; y <= radius * _pixelsPerUnit; ++y)
		{
			for (var x = -radius * _pixelsPerUnit; x <= radius * _pixelsPerUnit; ++x)
			{
				if (x * x + y * y <= (radius * _pixelsPerUnit) * (radius * _pixelsPerUnit))
				{
					Color c = new Color(0, 0, 0, Vector2.Distance(new Vector2(originX+x, originY+y), new Vector2(originX,originY))/(float)radius);
					if(hasCartographer)
					{
						c.a = 0f;
					}
					_pixels[(originY + y) * _textureSize + originX + x] = c;
				}
			}
		}
	}
	private void Update()
	{

		var translatedPos = car.position - transform.position;

		var pixelPosX = Mathf.RoundToInt(translatedPos.x * _pixelsPerUnit + _centerPixel.x);
		var pixelPosY = Mathf.RoundToInt(translatedPos.y * _pixelsPerUnit + _centerPixel.y);
		
		CreateCircle(pixelPosX, pixelPosY, _radius);
		
		_texture.SetPixels(_pixels);
		_texture.Apply(false);
	}
	private void ClearPixels()
	{
		for (var i = 0; i < _pixels.Length; i++)
		{
			_pixels[i] = Color.black;
		}
	}
}
