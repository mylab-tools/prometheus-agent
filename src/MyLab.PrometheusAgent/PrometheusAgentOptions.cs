﻿namespace MyLab.PrometheusAgent
{
    public class PrometheusAgentOptions
    {
        public string Config { get; set; }

        public DockerDiscoveryStrategy DockerDiscovery { get; set; } = DockerDiscoveryStrategy.None;
        public string DockerSockPath { get; set; } = "unix:///var/run/docker.sock";
        public int ConfigExpirySec { get; set; }
    }

    public enum DockerDiscoveryStrategy
    {
        None,
        All,
        Include
    }
}
