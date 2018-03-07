using UnityEngine;

public class AssetBundleManager
{
	public readonly GameObject Car;
	private readonly AssetBundle _assetBundle;

	public AssetBundleManager(Main main)
	{

		var dsc = System.IO.Path.DirectorySeparatorChar;
		_assetBundle = AssetBundle.LoadFromFile(main.Path + dsc + "assetbundle" + dsc + "sidefriction");

		Car = _assetBundle.LoadAsset<GameObject>("SideFrictonCar");

	}
}


