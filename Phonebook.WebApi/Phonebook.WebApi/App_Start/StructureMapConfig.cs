﻿using System.Web.Http;
using Phonebook.WebApi.DependencyResolution;

namespace Phonebook.WebApi
{
    public class StructureMapConfig
    {
        public static void RegisterResolver()
        {
            var config = GlobalConfiguration.Configuration;
            var container = IoC.Initialize();
            config.DependencyResolver = new StructureMapResolver(container);
        }
    }
}