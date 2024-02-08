using System;
using UnityEngine;

public class EmitRegularDecal : MinEventActionRemoveBuff
{
    // <triggered_effect trigger="onSelfBuffStart" action="EmitRegularDecal, DecalEffect" target="self"/>

    public override void Execute(MinEventParams _params)
    {
        try
        {
            EntityAlive localPlayer = _params.Self;

            var customPrefab = localPlayer.transform.FindInChilds("tempPrefab_Decal_effect2");

            if (customPrefab != null)
            {
                var emitter = customPrefab.GetComponent<ParticleSystem>();
                emitter.Emit(1);

                //Log.Out($"---Decal emitted { customPrefab.name }");
            }
        }
        catch (Exception ex)
        {
            Log.Out("DecalEffect::EmitRegularDecal failed: " + ex.Message);
        }
    }
}