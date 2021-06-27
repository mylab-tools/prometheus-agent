﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;

namespace MyLab.PrometheusAgent.Tools
{
    class MetricTargetsReferences : ReadOnlyCollection<MetricTargetReference>
    {
        public MetricTargetsReferences(IList<MetricTargetReference> list) : base(list)
        {
        }

        public static MetricTargetsReferences LoadUniqueScrapeConfig(ScrapeConfig scrapeConfig)
        {
            var references = scrapeConfig.Items
                .SelectMany(itm => itm.StaticConfigs)
                .SelectMany(cfg => cfg.Targets)
                .Distinct()
                .Select(endpoint => new MetricTargetReference(
                    endpoint,
                    new HttpClient
                    {
                        BaseAddress = new Uri("http://" + endpoint),
                        Timeout = TimeSpan.FromSeconds(15)
                    }))
                .ToArray();

            return new MetricTargetsReferences(references);
        }
    }

    class MetricTargetReference
    {
        public string Id { get; }
        public HttpClient Client { get; }

        public MetricTargetReference(string id, HttpClient client)
        {
            Id = id;
            Client = client;
        }
    }
}