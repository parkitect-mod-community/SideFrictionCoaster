using UnityEngine;

public class AssetBundleManager
{
	public readonly GameObject Car;
	private readonly AssetBundle _assetBundle;

	public AssetBundleManager(Main main)
	{

		var dsc = System.IO.Path.DirectorySeparatorChar;
		_assetBundle = AssetBundle.LoadFromFile(main.Path + dsc + "assetbundle" + dsc + "assetpack");

		Car = _assetBundle.LoadAsset<GameObject>("8f034a245ff2b4d1e80ad27667bdd1df");
		_assetBundle.Unload(false);
	}
	
}


