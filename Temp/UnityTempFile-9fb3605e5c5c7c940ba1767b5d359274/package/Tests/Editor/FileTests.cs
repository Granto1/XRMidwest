using System;
using System.IO;
using NUnit.Framework;
using UnityEngine;

namespace UnityEditor.Template.VR.Editor.Tests
{
    [TestFixture]
    class FileTests
    {
        const int k_MaxPathLength = 140;

        [Test]
        public void AssetsFilePathLengthIsBelowMaxLimit()
        {
            string rootPath = Application.dataPath;
            bool filePathLengthExceeded = CheckPathLengthInFolderRecursively(string.Empty, rootPath);
            Assert.That(filePathLengthExceeded, Is.False);
        }

        static string CombineAllowingEmpty(string path1, string path2)
        {
            if (string.IsNullOrEmpty(path1))
                return path2;
            if (string.IsNullOrEmpty(path2))
                return path1;
            return Path.Combine(path1, path2);
        }

        static bool CheckPathLengthInFolderRecursively(string relativeFolder, string absoluteBasePath)
        {
            try
            {
                var maxLimitExceeded = false;
                var fullFolder = CombineAllowingEmpty(absoluteBasePath, relativeFolder);

                foreach (string entry in Directory.GetFileSystemEntries(fullFolder))
                {
                    var fullPath = CombineAllowingEmpty(relativeFolder, Path.GetFileName(entry));

                    if (fullPath.Length > k_MaxPathLength)
                    {
                        maxLimitExceeded = true;
                        Debug.LogError($"{fullPath} is {fullPath.Length} characters, which is longer than the limit of {k_MaxPathLength} characters. You must use shorter names.");
                    }
                }

                foreach (string dir in Directory.GetDirectories(fullFolder))
                {
                    var wasExceeded = CheckPathLengthInFolderRecursively(CombineAllowingEmpty(relativeFolder, Path.GetFileName(dir)), absoluteBasePath);
                    if (wasExceeded)
                        maxLimitExceeded = true;
                }

                return maxLimitExceeded;
            }
            catch (Exception e)
            {
                Debug.LogError("Exception " + e.Message);
            }

            return false;
        }
    }
}