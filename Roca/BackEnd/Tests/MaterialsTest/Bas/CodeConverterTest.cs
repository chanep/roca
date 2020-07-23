using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cno.Roca.BackEnd.Tests.Materials.Bas
{
    /// <summary>
    /// Summary description for CodeConverterTest
    /// </summary>
    [TestClass]
    public class CodeConverterTest
    {
        public CodeConverterTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion


        
        [TestMethod]
        public void CreatePfBasFamilyFileTest()
        {
            string inputFile = @"c:\RocaDocs\BASES BAS\OepFamiliasPf.csv";
            string outputFile = @"c:\RocaDocs\BASES BAS\BasFamiliasPf.csv";
            using (var converter = new CodeConverter())
            {
                converter.CreatePfBasFamilyFile(inputFile, outputFile);
            }
            
        }

        [TestMethod]
        public void CreateValveBasFamilyFileTest()
        {
            string inputFile = @"c:\RocaDocs\BASES BAS\OepFamiliasValve.csv";
            string outputFile = @"c:\RocaDocs\BASES BAS\BasFamiliasValve.csv";
            using (var converter = new CodeConverter())
            {
                converter.CreateValveBasFamilyFile(inputFile, outputFile);
            }
        }

        [TestMethod]
        public void CreateBasCodeFileTest()
        {
            string pfFamilyCodeFile = @"c:\RocaDocs\BASES BAS\OepFamiliasPf.csv";
            string valveFamilyCodeFile = @"c:\RocaDocs\BASES BAS\OepFamiliasValve.csv";
            string oepCodeFile = @"c:\RocaDocs\BASES BAS\OepCodes.csv";
            string outputFile = @"c:\RocaDocs\BASES BAS\BasCodes.csv";
            using (var converter = new CodeConverter())
            {
                converter.CreateBasCodeFile(pfFamilyCodeFile, valveFamilyCodeFile, oepCodeFile, outputFile);
            }
            Console.WriteLine("Finalizado!");
        }

        [TestMethod]
        public void CheckOepPfFamilyFileTest()
        {
            string inputFile = @"c:\RocaDocs\BASES BAS\OepFamiliasPf.csv";
            string outputFile = @"c:\RocaDocs\BASES BAS\OepFamiliasFaltantesPf.csv";
            using (var converter = new CodeConverter())
            {
                converter.CheckOepPfFamilyFile(inputFile, outputFile);
            }
        }

        [TestMethod]
        public void CheckOepValveFamilyFileTest()
        {
            string inputFile = @"c:\RocaDocs\BASES BAS\OepFamiliasValve.csv";
            string outputFile = @"c:\RocaDocs\BASES BAS\OepFamiliasFaltantesValve.csv";
            using (var converter = new CodeConverter())
            {
                converter.CheckOepValveFamilyFile(inputFile, outputFile);
            }
        }

        [TestMethod]
        public void CheckOepCodeFileTest()
        {
            string pfFamilyCodeFile = @"c:\RocaDocs\BASES BAS\OepFamiliasPf.csv";
            string valveFamilyCodeFile = @"c:\RocaDocs\BASES BAS\OepFamiliasValve.csv";
            string oepCodeFile = @"c:\RocaDocs\BASES BAS\OepCodes.csv";
            string outputFile = @"c:\RocaDocs\BASES BAS\OepCodesFaltantes.csv";
            using (var converter = new CodeConverter())
            {
                converter.CheckOepCodeFile(pfFamilyCodeFile, valveFamilyCodeFile, oepCodeFile, outputFile);
            }
        }
          
        
    }
}
