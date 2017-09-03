using StructureMap;
using System.Diagnostics;

namespace Phonebook.WebApi.DependencyResolution
{
    public static class IoC
    {
        public static IContainer Initialize()
        {
            var container = new Container();
            container.Configure(x =>
            {
                x.Scan(scan =>
                {
                    scan.WithDefaultConventions();
                    scan.AssembliesFromApplicationBaseDirectory();
                });
            });

            Debug.WriteLine(container.WhatDoIHave());

            return container;
        }
    }
}