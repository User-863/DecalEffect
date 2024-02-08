using System;
using HarmonyLib;
using UnityEngine;

namespace DecalEffect.Harmony
{
    [HarmonyPatch(typeof(ItemActionAttack))]
    [HarmonyPatch("Hit")]
    public class DecalItemActionAttackHit
    {
        public static void Prefix(WorldRayHitInfo hitInfo, string _attackingDeviceMadeOf)
        {
            try
            {
                if (hitInfo == null)
                    return;
    
                var localPlayer = GameManager.Instance.World.GetPrimaryPlayer();
                var camera = localPlayer.transform.FindInChilds("Camera");
                var holdingItem = localPlayer.inventory.holdingItem;

                if (holdingItem.HasAnyTags(FastTags.Parse("ranged")) && !holdingItem.HasAnyTags(FastTags.Parse("bowSkill")) && !holdingItem.HasAnyTags(FastTags.Parse("motorTool")))
                {
                    if (!holdingItem.HasAnyTags(FastTags.Parse("shotgun")))
                    {
                        // Get attached prefab
                        var customPrefab = localPlayer.transform.FindInChilds("tempPrefab_Decal_effect2");

                        if (customPrefab != null)
                        {
                            // Get particle system
                            var emitter = customPrefab.GetComponent<ParticleSystem>();

                            // Rotate emitter in the direction of view
                            customPrefab.rotation = camera.rotation;

                            // Do not touch emitter/prefab world position use translated local position instead
                            //Vector3 thePosition = localPlayer.transform.InverseTransformPoint(hitInfo.hit.pos); // Doesn't work because entity.transform.position returns incorrect results
                            Vector3 thePosition = Quaternion.Inverse(localPlayer.transform.rotation) * (hitInfo.hit.pos - localPlayer.position); // Manual InverseTransformPoint

                            // Apply translated position
                            emitter.transform.localPosition = thePosition;

                            // Emit
                            emitter.Emit(1);

                            //Log.Out($"---Decal emitted { customPrefab.name }");
                        }
                    }
                    else
                    {
                        var customPrefab = localPlayer.transform.FindInChilds("tempPrefab_Decal_effect2_S");
    
                        if (customPrefab != null)
                        {
                            var emitter = customPrefab.GetComponent<ParticleSystem>();

                            customPrefab.rotation = camera.rotation;

                            Vector3 thePosition = Quaternion.Inverse(localPlayer.transform.rotation) * (hitInfo.hit.pos - localPlayer.position);

                            // Change shot spread with distance
                            var emitterShape = emitter.shape;
                            Vector3 gunPosition = localPlayer.position; gunPosition.y += 1.8f;
                            //float distance = hitInfo.hit.distanceSq;
                            float distance = Vector3.Distance(gunPosition, hitInfo.hit.pos);
                            float emitterRadius = (((distance - 0f) * (0.6f - 0.01f)) / (10f - 0f)) + 0.01f;
                            emitterShape.radius = emitterRadius;

                            emitter.transform.localPosition = thePosition;

                            emitter.Emit(1);

                            //Log.Out($"---Decal emitted { customPrefab.name }, distance { distance }, shot spread { emitterRadius }");
                        }
                    }
                }
                else
                {
                    // Melee hit duplicate remove
                    if (_attackingDeviceMadeOf.StartsWith("M"))
                        return;

                    var customPrefab = localPlayer.transform.FindInChilds("tempPrefab_Decal_effect2_M");
    
                    if (customPrefab != null)
                    {
                        var emitter = customPrefab.GetComponent<ParticleSystem>();

                        customPrefab.rotation = camera.rotation;

                        Vector3 thePosition = Quaternion.Inverse(localPlayer.transform.rotation) * (hitInfo.hit.pos - localPlayer.position);

                        emitter.transform.localPosition = thePosition;

                        emitter.Emit(1);
    
                        //Log.Out($"---Decal emitted { customPrefab.name }");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Out("DecalEffect::EmitDecal failed: " + ex.Message);
            }
        }
    }

    /*
    [HarmonyPatch(typeof(WeatherManager))]
    [HarmonyPatch("HandleBiomeChanging")]
    public class BiomeDecalMaterial
    {
        public static void Prefix(BiomeDefinition _newBiome)
        {
            try
            {
                if (_newBiome == null)
                    return;

                var localPlayer = GameManager.Instance.World.GetPrimaryPlayer();

                var splatterMat = localPlayer.transform.FindInChilds("tempPrefab_Decal_effect2").transform.FindInChilds("Splatter").GetComponent<Renderer>().sharedMaterial;
                var splatterMatM = localPlayer.transform.FindInChilds("tempPrefab_Decal_effect2_M").transform.FindInChilds("Splatter").GetComponent<Renderer>().sharedMaterial;

                // < biomemap id = "01" name = "snow" />
                // < biomemap id = "03" name = "pine_forest" />
                // < biomemap id = "05" name = "desert" />
                // < biomemap id = "06" name = "water" />
                // < biomemap id = "07" name = "radiated" />
                // < biomemap id = "08" name = "wasteland" />
                // < biomemap id = "09" name = "burnt_forest" />
                // < biomemap id = "13" name = "caveFloor" />
                // < biomemap id = "14" name = "caveCeiling" />
                // < biomemap id = "18" name = "onlyWater" />
                // < biomemap id = "19" name = "underwater" />

                if (_newBiome.m_Id == 8 || _newBiome.m_Id == 9)
                {
                    if (splatterMat != null)
                    {
                        splatterMat.SetFloat("_Intensity", 1.5f);
                    }

                    if (splatterMatM != null)
                    {
                        splatterMatM.SetFloat("_Intensity", 1.5f);
                    }
                }
                else
                {
                    if (splatterMat != null)
                    {
                        splatterMat.SetFloat("_Intensity", 2f);
                    }

                    if (splatterMatM != null)
                    {
                        splatterMat.SetFloat("_Intensity", 2f);
                    }
                }

                //Log.Out($"---New biome ID { _newBiome.m_Id }");
            }
            catch (Exception ex)
            {
                Log.Out("DecalEffect::BiomeDecalMaterial failed: " + ex.Message);
            }
        }
    }
    */
}
