using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using PlagueButtonAPI;
using UnityEngine;

namespace UnityExplorerHelper_VRC
{
    internal class Helper : BaseModule
    {
        public override void OnQuickMenuInit()
        {
            if (MelonHandler.Mods.All(o => o.Info.Name != "UnityExplorer"))
            {
                return; // No UE
            }

            MelonCoroutines.Start(RunMe());

            IEnumerator RunMe()
            {
                while (GameObject.Find("UniverseLibCanvas")?.transform?.Find("com.sinai.unityexplorer_Root") == null) // Wait for it to exist
                {
                    yield return new WaitForSeconds(0.5f);
                }

                var UE_UI_Handler = GameObject.Find("UniverseLibCanvas").transform.Find("com.sinai.unityexplorer_Root").gameObject.AddComponent<ObjectHandler>();

                UE_UI_Handler.OnUpdateEachSecond += (_, _) =>
                {
                    VRCInputManager.Method_Public_Static_Void_Boolean_0(true);
                    ButtonAPI.GetQuickMenuInstance().field_Private_Boolean_5 = true;
                };

                UE_UI_Handler.OnDisabled += _ =>
                {
                    VRCInputManager.Method_Public_Static_Void_Boolean_0(false);
                    ButtonAPI.GetQuickMenuInstance().field_Private_Boolean_5 = false;
                };
            }
        }
    }
}
