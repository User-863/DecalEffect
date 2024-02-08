# ``Harmony`` 7 Days To Die - Decal Effect
**NexusMods**: https://www.nexusmods.com/7daystodie/mods/4279

## How to add С# script to your particle system

* Сreate a new Unity project with your particle system.
* Download the [MultiPlatformExportAssetBundles.cs](https://github.com/7D2D/Templates-and-Utilities) script and place it into your Unity project's assets folder.
* In the *Assets* folder create a *Scripts* folder and in it a subfolder for your script.
* In this subfolder create your C# script and attach it to the particle system in your scene.
* Then right click in this script subfolder and *Create -> Assembly Definition*.
* Select the created assembly definition and in the inspector, in the **Name** field, specify **the exact name** of your script, for example: *CollisionNormalDecalSpawner*
* If you did everything correctly then the dll with your script will be automatically compiled (if you select your script, in the inspector you will see **Filename *CollisionNormalDecalSpawner.dll***).
* Now go to the ``Your Unity Project\Library\ScriptAssemblies\`` folder and copy the compiled dll with your script to the root of your mod folder.
* After that, export a ``unity3d`` file with your particle prefabs using *MultiPlatformExportAssetBundles.cs* and place it in the ``Resources`` subfolder of your mod.

**More info**: https://community.7daystodie.com/topic/27941-using-custom-explosion-particles-with-working-scripts-in-a20/


## References
https://github.com/SphereII/SphereII.Mods/tree/master/SampleProject

https://7d2dmods.github.io/HarmonyDocs/index.htm?Scripts.html

https://youtu.be/JRa2g3vgzBo?list=PLX2vGYjWbI0QJJfR-jSqxonYuCHrUhAvN

https://connect-prd-cdn.unity.com/20201104/948a5d90-8944-4bcc-ac7b-95e846cc8646/ParticleScripting.zip