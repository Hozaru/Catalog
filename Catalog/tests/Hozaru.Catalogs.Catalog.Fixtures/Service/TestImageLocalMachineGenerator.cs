using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hozaru.Catalogs.Catalog.Aggregates;
using Hozaru.Catalogs.Catalog.DomainServices;
using Hozaru.Catalogs.Catalog.DomainServices.Impl;
using Hozaru.Catalogs.Catalog.ValuesObjects;
using Moq;
using NUnit.Framework;

namespace Hozaru.Catalogs.Catalog.Fixtures.Service
{
    [TestFixture]
    public class TestImageLocalMachineGenerator
    {
        private IImageGenerator _imageGenerator;
        private string _filePath;
        private Image _image;
        private Guid _storeId;
        private Guid _productId;
        private Guid _imageId;
        private string _pathDirectoryProduct;

        [TestFixtureSetUp]
        public void Setup()
        {
            _imageGenerator = new ImageLocalMachineGenerator();
            _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"images-src\logo.png");
            _image = Image.FromFile(_filePath);
            _storeId = Guid.NewGuid();
            _productId = Guid.NewGuid();
            _imageId = Guid.NewGuid();
            bool useBaseDirectory = Convert.ToBoolean(ConfigurationManager.AppSettings["FileStorageUseBaseDirectory"]);
            if (useBaseDirectory)
            {
                _pathDirectoryProduct = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, string.Format(@"Images\{0}\Products\{1}\{2}", _storeId, _productId, _imageId));
            }
            else
            {
                var pathFileStorageDirectory = ConfigurationManager.AppSettings["PathFileStorageDirectory"];
                _pathDirectoryProduct = string.Format(@"{0}\{1}\Products\{2}\{3}", pathFileStorageDirectory, _storeId, _productId, _imageId);
            }
        }

        [Test]
        public void Generate_Image_to_local_storage()
        {
            var directoryInfo = _imageGenerator.Generate(_image, _storeId, _productId, _imageId);

            Assert.AreEqual(directoryInfo.FullName, new DirectoryInfo(_pathDirectoryProduct).FullName);
            Assert.AreEqual(directoryInfo.Name, _imageId.ToString());
            Assert.IsTrue(Directory.Exists(_pathDirectoryProduct));
            Assert.IsTrue(File.Exists(string.Format("{0}\\{1}.jpg", directoryInfo.FullName, ImageCombinations.HOME.Name)));
            Assert.IsTrue(File.Exists(string.Format("{0}\\{1}.jpg", directoryInfo.FullName, ImageCombinations.LARGE.Name)));
            Assert.IsTrue(File.Exists(string.Format("{0}\\{1}.jpg", directoryInfo.FullName, ImageCombinations.MASTER.Name)));
            Assert.IsTrue(File.Exists(string.Format("{0}\\{1}.jpg", directoryInfo.FullName, ImageCombinations.MEDIUM.Name)));
            Assert.IsTrue(File.Exists(string.Format("{0}\\{1}.jpg", directoryInfo.FullName, ImageCombinations.SMALL.Name)));
            Assert.IsTrue(File.Exists(string.Format("{0}\\{1}.jpg", directoryInfo.FullName, ImageCombinations.THINKBOX.Name)));
        }

        [TestFixtureTearDown]
        public void Remove_file_storage_directory()
        {
            var pathFileStorageDirectory = ConfigurationManager.AppSettings["PathFileStorageDirectory"];
            System.IO.DirectoryInfo directoryInfo = new DirectoryInfo(pathFileStorageDirectory);
            bool useBaseDirectory = Convert.ToBoolean(ConfigurationManager.AppSettings["FileStorageUseBaseDirectory"]);
            if (useBaseDirectory)
            {
                directoryInfo = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images"));
            }
            else
            {
                var path = string.Format(@"{0}\{1}\Products\{2}\{3}", pathFileStorageDirectory, _storeId, _productId, _imageId);
                directoryInfo = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path));
            }
            foreach (System.IO.FileInfo file in directoryInfo.GetFiles()) file.Delete();
            foreach (System.IO.DirectoryInfo subDirectory in directoryInfo.GetDirectories()) subDirectory.Delete(true);
        }
    }
}
