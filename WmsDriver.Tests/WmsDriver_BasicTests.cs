﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cartomatic.Wms;
using Cartomatic.Wms.WmsDriverExtensions;
using FluentAssertions;
using NUnit.Framework;

namespace WmsDriver.Tests
{
    [TestFixture]
    public class WmsDriver_BasicTests
    {
        [Test]
        public void Constructor_WhenCalled_ShouldAlwaysInitDataContainers()
        {
            var drv = MakeWmsDriver() as FakeWmsDriver;

            drv.SupportedGetFeatureInfoFormats.Should().NotBeNull();
            drv.SupportedGetCapabilitiesFormats.Should().NotBeNull();
            drv.SupportedGetMapFormats.Should().NotBeNull();
            drv.SupportedExceptionFormats.Should().NotBeNull();
            drv.SupportedVersions.Should().NotBeNull();
        }

        [Test]
        public void Handle_Always_CallsPrepareDriver()
        {
            var drv = MakeWmsDriver() ;

            drv.Handle(string.Empty);

            (drv as FakeWmsDriver).PrepareCalled.Should().BeTrue();
        }

        [Test]
        public void Handle_WhenServiceDescriptionNotProvided_CreatesDefaultWmsServiceDescription()
        {
            var drv = MakeWmsDriver();

            drv.ServiceDescription.Should().BeNull();

            drv.Handle(string.Empty);
            
            drv.ServiceDescription.Should().NotBeNull();
        }

        private IWmsDriver MakeWmsDriver()
        {
            return new FakeWmsDriver();
        }

        private class FakeWmsDriver : Cartomatic.Wms.WmsDriver
        {
            public bool PrepareCalled { get; private set; }
            
            protected internal override void PrepareDriver()
            {
                PrepareCalled = true;
                base.PrepareDriver();
            }
        }
    }
}