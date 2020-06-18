using I3dm.Tile;
using NUnit.Framework;
using SharpGLTF.Validation;
using System.IO;
using System.Linq;

namespace i3dm.tile.tests
{
    public class Tests
    {
        string expectedMagicHeader = "i3dm";
        int expectedVersionHeader = 1;


        [Test]
        public void InstancedWithBatchTableTest()
        {
            // arrange
            // source: http://vcities.umsl.edu/Cesium1.54/Apps/SampleData/Cesium3DTiles/Instanced/InstancedWithBatchTable/
            var i3dmfile = File.OpenRead(@"testfixtures/instancedWithBatchTable.i3dm");
            Assert.IsTrue(i3dmfile != null);
            // act
            var i3dm = I3dmReader.ReadI3dm(i3dmfile);
            Assert.IsTrue(expectedMagicHeader == i3dm.I3dmHeader.Magic);
            Assert.IsTrue(expectedVersionHeader == i3dm.I3dmHeader.Version);
            Assert.IsTrue(i3dm.I3dmHeader.GltfFormat == 1);
            Assert.IsTrue(i3dm.BatchTableJson.Length >= 0);
            Assert.IsTrue(i3dm.BatchTableJson == "{\"Height\":[20,20,20,20,20,20,20,20,20,20,20,20,20,20,20,20,20,20,20,20,20,20,20,20,20]} ");
            Assert.IsTrue(i3dm.GlbData.Length > 0);
            Assert.IsTrue(i3dm.FeatureTableBinary.Length == 304);
            var stream = new MemoryStream(i3dm.GlbData);
            var glb = SharpGLTF.Schema2.ModelRoot.ReadGLB(stream);
            glb.SaveGLB(@"d:\aaa\instancedwithbatchtable.glb");
        }

        [Test]
        public void InstancedOrientationTest()
        {
            // arrange
            // source: http://vcities.umsl.edu/Cesium1.54/Apps/SampleData/Cesium3DTiles/Instanced/InstancedOrientation/
            var i3dmfile = File.OpenRead(@"testfixtures/instancedOrientation.i3dm");
            Assert.IsTrue(i3dmfile != null);
            // act
            var i3dm = I3dmReader.ReadI3dm(i3dmfile);
            Assert.IsTrue(expectedMagicHeader == i3dm.I3dmHeader.Magic);
            Assert.IsTrue(expectedVersionHeader == i3dm.I3dmHeader.Version);
            Assert.IsTrue(i3dm.I3dmHeader.GltfFormat == 1);
            Assert.IsTrue(i3dm.BatchTableJson.Length >= 0);
            Assert.IsTrue(i3dm.GlbData.Length > 0);
            Assert.IsTrue(i3dm.BatchTableJson == "{\"Height\":[20,20,20,20,20,20,20,20,20,20,20,20,20,20,20,20,20,20,20,20,20,20,20,20,20]} ");
            Assert.IsTrue(i3dm.FeatureTableBinary.Length == 904);
            var stream = new MemoryStream(i3dm.GlbData);
            var glb = SharpGLTF.Schema2.ModelRoot.ReadGLB(stream);
        }

        [Test]
        public void TreeBillboardTest()
        {
            // arrange
            var i3dmfile = File.OpenRead(@"testfixtures/tree_billboard.i3dm");
            Assert.IsTrue(i3dmfile != null);
            // act
            var i3dm = I3dmReader.ReadI3dm(i3dmfile);
            Assert.IsTrue(expectedMagicHeader == i3dm.I3dmHeader.Magic);
            Assert.IsTrue(expectedVersionHeader == i3dm.I3dmHeader.Version);
            Assert.IsTrue(i3dm.I3dmHeader.GltfFormat == 1);
            Assert.IsTrue(i3dm.BatchTableJson.Length >= 0);
            Assert.IsTrue(i3dm.GlbData.Length > 0);
            Assert.IsTrue(i3dm.FeatureTableBinary.Length == 304);
            Assert.IsTrue(i3dm.BatchTableJson == "{\"Height\":[20,20,20,20,20,20,20,20,20,20,20,20,20,20,20,20,20,20,20,20,20,20,20,20,20]} ");
            Assert.IsTrue(i3dm.FeatureTableBinary.Length == 304);
            var stream = new MemoryStream(i3dm.GlbData);
            try
            {
                var glb = SharpGLTF.Schema2.ModelRoot.ReadGLB(stream);
                //Assert.IsTrue(glb.Asset.Version.Major == 2.0);
                //Assert.IsTrue(glb.Asset.Generator == "COLLADA2GLTF");
            }
            catch(LinkException le)
            {
                // we expect linkexception because SharpGLTF does not support KHR_techniques_webgl'
                Assert.IsTrue(le != null);
                Assert.IsTrue(le.Message == "ModelRoot Extensions: KHR_techniques_webgl");
            }
        }

        [Test]
        public void TreeTest()
        {
            // arrange
            var i3dmfile = File.OpenRead(@"testfixtures/tree.i3dm");
            Assert.IsTrue(i3dmfile != null);
            // act
            var i3dm = I3dmReader.ReadI3dm(i3dmfile);
            Assert.IsTrue(expectedMagicHeader == i3dm.I3dmHeader.Magic);
            Assert.IsTrue(expectedVersionHeader == i3dm.I3dmHeader.Version);
            Assert.IsTrue(i3dm.I3dmHeader.GltfFormat == 1);
            Assert.IsTrue(i3dm.BatchTableJson.Length >= 0);
            Assert.IsTrue(i3dm.GlbData.Length > 0);
            Assert.IsTrue(i3dm.FeatureTableBinary.Length == 304);
            Assert.IsTrue(i3dm.BatchTableJson == "{\"Height\":[20,20,20,20,20,20,20,20,20,20,20,20,20,20,20,20,20,20,20,20,20,20,20,20,20]} ");
            Assert.IsTrue(i3dm.FeatureTableJson == "{\"INSTANCES_LENGTH\":25,\"EAST_NORTH_UP\":true,\"POSITION\":{\"byteOffset\":0}}");
            Assert.IsTrue(i3dm.FeatureTableBinary.Length == 304);
            // todo: read the FeatureTableBinary but how?

            var stream = new MemoryStream(i3dm.GlbData);
            var glb = SharpGLTF.Schema2.ModelRoot.ReadGLB(stream);
            Assert.IsTrue(glb.Asset.Version.Major == 2.0);
            Assert.IsTrue(glb.Asset.Generator == "COLLADA2GLTF");
        }


        [Test]
        public void CubeTest()
        {
            // arrange
            var i3dmfile = File.OpenRead(@"testfixtures/cube.i3dm");
            Assert.IsTrue(i3dmfile != null);
            // act
            var i3dm = I3dmReader.ReadI3dm(i3dmfile);

            // assert
            Assert.IsTrue(expectedMagicHeader == i3dm.I3dmHeader.Magic);
            Assert.IsTrue(expectedVersionHeader == i3dm.I3dmHeader.Version);
            Assert.IsTrue(i3dm.I3dmHeader.GltfFormat == 1);
            Assert.IsTrue(i3dm.BatchTableJson.Length >= 0);
            Assert.IsTrue(i3dm.GlbData.Length > 0);
            Assert.IsTrue(i3dm.FeatureTableBinary.Length == 16);
            var expectedFeatureTableBinary = new byte[16];
            Assert.IsTrue(i3dm.FeatureTableBinary.SequenceEqual(expectedFeatureTableBinary));
            Assert.IsTrue(i3dm.FeatureTableJson == "{\"type\":\"Buffer\",\"data\":[123,34,73,78,83,84,65,78,67,69,83,95,76,69,78,71,84,72,34,58,49,44,34,80,79,83,73,84,73,79,78,34,58,123,34,98,121,116,101,79,102,102,115,101,116,34,58,48,125,125,32,32,32,32,32,32]}  ");
            var stream = new MemoryStream(i3dm.GlbData);
            var glb = SharpGLTF.Schema2.ModelRoot.ReadGLB(stream);
            Assert.IsTrue(glb.Asset.Version.Major == 2.0);
            Assert.IsTrue(glb.Asset.Generator == "SharpGLTF 1.0.0");
        }
    }
}