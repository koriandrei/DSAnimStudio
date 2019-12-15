using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Unity.FbxSdk;

namespace DSAnimStudio
{
    class ModelExporter
    {
        private Model m;

        public ModelExporter(Model m)
        {
            this.m = m;
        }

        internal static void Export()
        {
            foreach (Model m in Scene.Models)
            {
                ModelExporter mex = new ModelExporter(m);
                mex.ExportModel();
            }
        }

        private static FbxNode Create(NewAnimSkeleton.FlverBoneInfo BoneInfo, FbxManager manager)
        {
            FbxSkeleton skel = FbxSkeleton.Create(manager, BoneInfo.Name);
            skel.Size.Set(BoneInfo.Length);
            skel.SetSkeletonType(FbxSkeleton.EType.eLimbNode);

            FbxNode fbxNode = FbxNode.Create(manager, BoneInfo.Name);

            fbxNode.SetNodeAttribute(skel);
            var nodePosition = BoneInfo.ReferenceMatrix.Translation;
            fbxNode.LclTranslation.Set(new FbxDouble3(nodePosition.X, nodePosition.Y, nodePosition.Z));

            return fbxNode;
        }

        private static FbxNode CreateSubMesh(FbxScene scene, FlverSubmeshRenderer submesh, out FbxMesh mesh)
        {
            mesh = FbxMesh.Create(scene, submesh.FullMaterialName + "_Mesh");

            FlverShaderVertInput[] vertices = new FlverShaderVertInput[submesh.VertexCount];
            submesh.VertBuffer.GetData(vertices);

            mesh.InitControlPoints(vertices.Length);

            for (int i = 0; i< vertices.Length; i++)
            {
                var input = vertices[i];
                mesh.SetControlPointAt(new FbxVector4(input.Position.X, input.Position.Y, input.Position.Z), i);
            }

            for (int j = 0; j < submesh.MeshFacesets.Count; j++)
            {
                var faceset = submesh.MeshFacesets[j];

                if (faceset.LOD != 0)
                {
                    continue;
                }

                int[] indices = new int[faceset.IndexCount];
                faceset.IndexBuffer.GetData(indices);

                for (int k = 0; k < faceset.IndexCount; k += 3)
                {
                    mesh.BeginPolygon();

                    for (int kk = 0; kk < 3; ++kk)
                    {
                        mesh.AddPolygon(indices[k + kk]);
                    }
                    mesh.EndPolygon();
                }
            }
            
            FbxNode nnn = FbxNode.Create(scene, mesh.GetName() + "_Node");
            nnn.SetNodeAttribute(mesh);
            nnn.SetShadingMode(FbxNode.EShadingMode.eFlatShading);

            return nnn;
        }

        private void ExportModel()
        {
            FbxManager manager = FbxManager.Create();

            FbxScene scene = FbxScene.Create(manager, m.Name + "_Scene");

            NewAnimSkeleton skel = m.Skeleton;

            var root = scene.GetRootNode();
            IDictionary<int, FbxNode> bones;
            root.AddChild(ExtractSkeleton(scene, out bones, skel));

            FbxNode mesh = FbxNode.Create(scene, "Mesh");

            FbxMesh myMesh = null;

            foreach (var submesh in m.MainMesh.Submeshes)
            {
                FbxMesh localMesh = null;
                var mmm1 = CreateSubMesh(scene, submesh, out localMesh);

                if (myMesh == null)
                {
                    myMesh = localMesh;
                }

                mesh.AddChild(mmm1);
            }

            root.AddChild(mesh);

            var skin = ExtractSkin(scene, bones, m);

            myMesh.AddDeformer(skin);

            FbxAnimStack stack = FbxAnimStack.Create(scene, "AnimationStack1");

            FbxAnimLayer layer = FbxAnimLayer.Create(scene, "AnimLayer1");
            stack.AddMember(layer);


            //var translationCurve = bones[1].LclTranslation.GetCurveNode(layer, true);
            bones[1].LclRotation.GetCurveNode(layer, true);
            var channel1Curve = bones[1].LclRotation.GetCurve(layer, Globals.FBXSDK_CURVENODE_COMPONENT_X, true);
            //var channel1Curve = rotationCurve.GetCurve()

            var animation = m.AnimContainer.CurrentAnimation;

            channel1Curve.KeyModifyBegin();
            for (int frameIndex = 0; frameIndex < animation.FrameCount; frameIndex++)
            {
                FbxTime time = FbxTime.FromFrame(frameIndex);

                animation.CurrentTime = frameIndex * animation.FrameDuration;

                animation.ApplyMotionToSkeleton();

                Microsoft.Xna.Framework.Vector3 translation;
                Microsoft.Xna.Framework.Quaternion rotation;
                Microsoft.Xna.Framework.Vector3 scale;

                if (!animation.Skeleton.HkxSkeleton[1].ReferenceMatrix.Decompose(out scale,out rotation,out translation))
                {
                    continue;
                }

                int index = channel1Curve.KeyAdd(time);

                channel1Curve.KeySet(index, time, rotation.W);
            }
            animation.WriteCurrentFrameToSkeleton();

            //root.AddChild(stack);

            channel1Curve.KeyModifyEnd();

            var ioSettings = FbxIOSettings.Create(manager, "IOSettings");

            manager.SetIOSettings(ioSettings);

            FbxExporter ex = FbxExporter.Create(manager, "ex");
            ex.Initialize("out.fbx");


            ex.Export(scene);

            manager.Destroy();
        }

        private static FbxNode ExtractSkeleton(FbxScene scene, out IDictionary<int, FbxNode> skeletonNodesRef, NewAnimSkeleton skeleton)
        {
            SortedDictionary<int, FbxNode> skeletonHierarchy = new SortedDictionary<int, FbxNode>();

            skeletonNodesRef = skeletonHierarchy;

            FbxNode root = null;
            foreach (var skelNode in skeleton.FlverSkeleton)
            {
                FbxSkeleton bone = FbxSkeleton.Create(scene, skelNode.Name + "_Bone");

                bone.SetSkeletonType(FbxSkeleton.EType.eLimb);

                FbxNode node = FbxNode.Create(scene, bone.GetName() + "_Node");

                if (skelNode.Name.ToLower().Contains("root"))
                {
                    if (root == null)
                    {
                        root = node;
                    }
                    else
                    {
                        System.Diagnostics.Debugger.Break();
                    }
                }

                if (skelNode.HkxBoneIndex >= 0)
                {
                    var hkxNode = skeleton.HkxSkeleton[skelNode.HkxBoneIndex];

                    if (hkxNode.ChildIndices.Count == 0)
                    {
                        bone.SetSkeletonType(FbxSkeleton.EType.eEffector);
                    }

                    if (skelNode.HkxBoneIndex == 0)
                    {
                        bone.SetSkeletonType(FbxSkeleton.EType.eRoot);
                    }
                }

                node.SetNodeAttribute(bone);
                if (!skeletonHierarchy.ContainsKey(skelNode.HkxBoneIndex))
                {
                    skeletonHierarchy.Add(skelNode.HkxBoneIndex, node);
                }
            }

            root = skeletonHierarchy[0];

            for (int havokIndex = 0; havokIndex < skeleton.HkxSkeleton.Count; havokIndex++)
            {
                FbxNode parentFbxNode = skeletonHierarchy[skeleton.HkxSkeleton[havokIndex].ParentIndex];
                parentFbxNode.AddChild(skeletonHierarchy[havokIndex]);


            }

            return root;
        }
        class BoneWeightInfo
        {
            public int BoneIndex;
            public float Weight;
        }
        private static FbxSkin ExtractSkin(FbxScene scene, IDictionary<int, FbxNode> bones, Model model)
        {
            FbxSkin skin = FbxSkin.Create(scene, "skin123123");
            for (int HavokBoneIndex = 0; HavokBoneIndex < model.Skeleton.HkxSkeleton.Count; HavokBoneIndex++)
            {
                var HavokBone = model.Skeleton.HkxSkeleton[HavokBoneIndex];

                var mesh = model.MainMesh.Submeshes[0];
                FlverShaderVertInput[] vertices = new FlverShaderVertInput[mesh.VertexCount];
                mesh.VertBuffer.GetData(vertices);

                Dictionary<int, float> weights = new Dictionary<int, float>();

                for (int vertexIndex = 0; vertexIndex < vertices.Length; vertexIndex++)
                {
                    var vertex = vertices[vertexIndex];

                    BoneWeightInfo[] boneWeight = new BoneWeightInfo[4];

                    boneWeight[0] = new BoneWeightInfo();
                    boneWeight[0].BoneIndex = (int)Math.Round(vertex.BoneIndices.X);
                    boneWeight[0].Weight = (int)Math.Round(vertex.BoneWeights.X);

                    boneWeight[1] = new BoneWeightInfo();
                    boneWeight[1].BoneIndex = (int)Math.Round(vertex.BoneIndices.Y);
                    boneWeight[1].Weight = (int)Math.Round(vertex.BoneWeights.Y);

                    boneWeight[2] = new BoneWeightInfo();
                    boneWeight[2].BoneIndex = (int)Math.Round(vertex.BoneIndices.Z);
                    boneWeight[2].Weight = (int)Math.Round(vertex.BoneWeights.Z);

                    boneWeight[3] = new BoneWeightInfo();
                    boneWeight[3].BoneIndex = (int)Math.Round(vertex.BoneIndices.W);
                    boneWeight[3].Weight = (int)Math.Round(vertex.BoneWeights.W);

                    BoneWeightInfo foundBone = boneWeight.SingleOrDefault(x => x.BoneIndex == HavokBoneIndex && x.BoneIndex != 0);

                    if (foundBone == null)
                    {
                        continue;
                    }
                    weights.Add(vertexIndex, foundBone.Weight);
                }

                FbxCluster cluster = FbxCluster.Create(scene, HavokBone.Name + "_Skin_Cluster");
                cluster.SetLink(bones[HavokBoneIndex]);
                cluster.SetControlPointIWCount(weights.Count);
                foreach (var pair in weights)
                {
                    cluster.AddControlPointIndex(pair.Key, pair.Value);
                }
                skin.AddCluster(cluster);
            }


            return skin;
            
            // node.SetNodeAttribute(skin);
        }
    }
}
