using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class MeshTools {

    public class MeshPart
    {
        /// <summary>
        /// 部件模型
        /// </summary>
        public SkinnedMeshRenderer skin;
        /// <summary>
        /// 骨骼名字列表
        /// </summary>
        public List<string> liBonesName;

        /// <summary>
        /// 初始化部件
        /// </summary>
        /// <param name="skin">模型</param>
        /// <param name="bones">骨骼列表，如果为空，则从模型上面获取骨骼</param>
        public MeshPart(SkinnedMeshRenderer skin, string[] bones = null)
        {
            this.skin = skin;
            liBonesName = new List<string>();
            if (bones != null) {
                for (int i = 0; i < bones.Length; ++i) {
                    liBonesName.Add(bones[i]);
                }
            } else {
                for (int i = 0; i < skin.bones.Length; ++i) {
                    var tName = skin.bones[i].name;
                    liBonesName.Add(tName);
                }
            }
        }
    }

    /// <summary>
    /// 合并各部件，生成一个Mesh
    /// </summary>
    public static SkinnedMeshRenderer CombineSkinnedMeshes(this Animator self, Dictionary<string, MeshPart> dictParts)
    {
        float startTime = Time.realtimeSinceStartup;

        List<CombineInstance> combineInstances = new List<CombineInstance>();
        List<Material> materials = new List<Material>();
        List<Transform> bones = new List<Transform>();

        // 取骨架
        Transform[] hips = self.GetComponentsInChildren<Transform>();

        // 重组
        foreach (var part in dictParts.Values) {
            var smr = part.skin;
            materials.AddRange(smr.materials);
            for (int sub = 0; sub < smr.sharedMesh.subMeshCount; sub++) {
                CombineInstance ci = new CombineInstance();
                ci.mesh = smr.sharedMesh;
                ci.subMeshIndex = sub;
                combineInstances.Add(ci);
            }

            // 添加骨骼，注意顺序
            for (int i = 0; i < part.liBonesName.Count; ++i) {
                var bone = part.liBonesName[i];
                for (int j = 0; j < hips.Length; ++j) {
                    var t = hips[j];
                    if (bone == t.name) {                        
                        bones.Add(t);
                        break;
                    }
                }
            }
        }

        // 配置，SkinnedMeshRenderer得挂载在Animator上面
        var skin = self.gameObject.NeedComponent<SkinnedMeshRenderer>();
        if (skin.sharedMesh == null) skin.sharedMesh = new Mesh();
        skin.sharedMesh.name = string.Format("{0} (Combined)", skin.sharedMesh.GetInstanceID());
        skin.sharedMesh.CombineMeshes(combineInstances.ToArray(), false, false);
        skin.bones = bones.ToArray();
        skin.materials = materials.ToArray();

        LogMgr.I("合并角色模型完成，消耗: {0} ms", (Time.realtimeSinceStartup - startTime) * 1000);

        return skin;
    }
}
