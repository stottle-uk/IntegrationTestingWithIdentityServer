using IdentityModel.Client;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using MyApiServer;
using System.Net;
using System.Net.Http;
using Xunit;
using System;

namespace MyApiServerTests
{
    public class UnitTest1
    {
        private readonly HttpClient _httpClient;
        private readonly TokenClient _tokenClient;

        public UnitTest1()
        {
            var webhost = new WebHostBuilder()
                .UseUrls("http://*:8000")
                .UseStartup<Startup>();

            var server = new TestServer(webhost);
            _httpClient = server.CreateClient();

            var authority = Environment.GetEnvironmentVariable("IDENTITY_SERVER_AUTHORITY");
            var disco = DiscoveryClient.GetAsync(authority).Result;
            _tokenClient = new TokenClient(disco.TokenEndpoint, "client", "secret");
        }

        [Fact]
        public async void ShouldNotAllowAnonymousUser()
        {
            var result = await _httpClient.GetAsync("http://localhost:8000/api/values");
            Assert.Equal(HttpStatusCode.Unauthorized, result.StatusCode);
        }

        [Fact]
        public async void ShouldReturnValuesForAuthenticatedUser()
        {
            var tokenResponse = _tokenClient.RequestResourceOwnerPasswordAsync("alice", "password", "api1").Result;
            _httpClient.SetBearerToken(tokenResponse.AccessToken);

            var result = await _httpClient.GetStringAsync("http://localhost:8000/api/values");
            Assert.Equal("[\"value1\",\"value2\"]", result);
        }
    }
}