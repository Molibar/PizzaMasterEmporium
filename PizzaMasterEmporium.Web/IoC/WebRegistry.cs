﻿using AutoMapper;
using StructureMap.Configuration.DSL;

namespace PizzaMasterEmporium.Web.IoC
{
    public class WebRegistry : Registry
    {
        public WebRegistry()
        {
            Scan(scan =>
            {
                scan.AddAllTypesOf<Profile>();
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();
            });
        }
    }
}