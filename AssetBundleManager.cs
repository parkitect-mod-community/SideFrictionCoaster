/**
* Copyright 2019 Michael Pollind
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*     http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/

using UnityEngine;

public class AssetBundleManager
{
    private readonly AssetBundle _assetBundle;
    public readonly GameObject Car;

    public AssetBundleManager(Main main)
    {
        var dsc = System.IO.Path.DirectorySeparatorChar;
        _assetBundle = AssetBundle.LoadFromFile(main.Path + dsc + "assetbundle" + dsc + "assetpack");

        Car = _assetBundle.LoadAsset<GameObject>("8f034a245ff2b4d1e80ad27667bdd1df");
        _assetBundle.Unload(false);
    }
}