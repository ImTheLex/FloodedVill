#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace CustomTools
{

    public class LevelDesignSaver
    {
        [MenuItem("Tools/Export Map As Prefab")]
        public static void Export()
        {
            GameObject map = GameObject.Find("GeneratedMap"); // ou autre nom

            if (map != null)
            {
                string localPath = "Assets/_/Database/Prefabs/GeneratedMap.prefab";
                localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);

                PrefabUtility.SaveAsPrefabAssetAndConnect(map, localPath, InteractionMode.UserAction);
                Debug.Log("Map exportée avec succès !");
            }
            else
            {
                Debug.LogError("Aucun objet nommé 'GeneratedMap' trouvé !");
            }
        }
    }
}
#endif
