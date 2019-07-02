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

using System.Linq;
using TrackedRiderUtility;
using UnityEngine;

public class Main : IMod
{
    private TrackRiderBinder binder;
    private GameObject hider;
    
    private GameObject ProxyObject(GameObject gameObject)
    {
        gameObject.transform.SetParent(hider.transform);
        return gameObject;
    }
    
    
    public string Path
    {
        get { return ModManager.Instance.getModEntries().First(x => x.mod == this).path; }
    }

    public void onEnabled()
    {
        hider = new GameObject();
        hider.SetActive(false);
        
       AssetBundleManager assetBundleManager = new AssetBundleManager(this);

        binder = new TrackRiderBinder("750af72f6eff659e238b2a5c8826e3c8");

        var trackedRide =
            binder.RegisterTrackedRide<TrackedRide>("Wooden Coaster", "SideFrictionCoaster", "Side Friction Coaster");
        trackedRide.price = 3600;
        trackedRide.dropsImportanceExcitement = .7f;
        trackedRide.inversionsImportanceExcitement = .67f;
        trackedRide.averageLatGImportanceExcitement = .7f;
        trackedRide.maxBankingAngle = 20;
        trackedRide.accelerationVelocity = .09f;
        trackedRide.carTypes = new CoasterCarInstantiator[] { };

        var meshGenerator =
            binder.RegisterMeshGenerator<SideFrictionTrackGenerator>(trackedRide);
        TrackRideHelper.PassMeshGeneratorProperties(TrackRideHelper.GetTrackedRide("Wooden Coaster").meshGenerator,
            trackedRide.meshGenerator);
        trackedRide.meshGenerator.customColors = new[]
        {
            new Color(68f / 255f, 47f / 255f, 37f / 255f, 1),
            new Color(74f / 255f, 32f / 255f, 32f / 255f, 1),
            new Color(66f / 255f, 66f / 255f, 66f / 255f, 1)
        };

        var coasterCarInstantiator =
            binder.RegisterCoasterCarInstaniator<CoasterCarInstantiator>(trackedRide, "SideFrictionInstantiator",
                "Side Friction Car", 5, 7, 2);

        var car = binder.RegisterCar<BaseCar>(ProxyObject(Object.Instantiate(assetBundleManager.Car)), "SideFrictionCar", .25f, .02f, true,
            new[]
            {
                new Color(0f / 255, 4f / 255, 190f / 255),
                new Color(138f / 255, 15f / 255, 15f / 255),
                new Color(101f / 255, 21f / 255, 27f / 255),
                new Color(172f / 255, 41f / 255, 42f / 255)
            });

        car.gameObject.AddComponent<RestraintRotationController>().closedAngles = new Vector3(0, 0, 120);
        coasterCarInstantiator.vehicleGO = car;

        binder.Apply();
        //deprecatedMappings
        var oldHash = "a9sfj-[a9w34ainw;kjasinda";

        GameObjectHelper.RegisterDeprecatedMapping("side_friction_GO", trackedRide.name);
        GameObjectHelper.RegisterDeprecatedMapping("Side Friction@CoasterCarInstantiator" + oldHash,
            coasterCarInstantiator.name);
        GameObjectHelper.RegisterDeprecatedMapping("SideFriction_Car" + oldHash, car.name);
    }

    public void onDisabled()
    {
        binder.Unload();
    }

    public string Name => "Side Friction Coaster";

    public string Description =>
        "An early wooden coaster design that used a trough/side rail design that isn't use anymore in most modern day coaster.";

    string IMod.Identifier => "SideFrictionCoaster";
}